using Nova.Bytecode.IO;
using Nova.ByteCode.IO;
using Nova.Members;
using Nova.Semantics;
using Nova.Types;
using Nova.Utils;
using Nova.Utils.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.IO
{
    public class NovBuilder
    {
        public static string STANDARD_LIBRARY_PATH = Path.Combine(Environment.CurrentDirectory, "STD/");

        public CompilationState State
        {
            get;
            private set;
        }
        private ClassesContainer Container
        {
            get;
            set;
        }
        private string OutputPath
        {
            get;
            set;
        }
        private string InputFilePath
        {
            get;
            set;
        }
        private NovFile Result
        {
            get;
            set;
        }
        public NovBuilder(string inputPath, string outputPath)
        {
            this.InputFilePath = inputPath;
            this.OutputPath = outputPath;
        }

        public bool Build()
        {
            State = CompilationState.Parsing;

            NvFile file = OpenNvFile(InputFilePath);

            if (file == null)
            {
                return false;
            }

            if (!CreateContainer(file))
            {
                return false;
            }

            State = CompilationState.TypeLink;

            List<SemanticalError> errors = new List<SemanticalError>();

            foreach (var @class in Container.GetClasses())
            {
                Container.TypeManager.Register(@class, false);
            }

            foreach (var @class in Container.GetClasses())
            {
                errors.AddRange(@class.ValidateTypes(Container));
            }

            foreach (var @class in Container.GetClasses())
            {
                errors.AddRange(@class.ValidateSemantics(Container));
            }

            State = CompilationState.SemanticalValidation;

            if (errors.Count > 0)
            {
                foreach (var error in errors)
                {
                    Logger.Write(error.ToString(), LogType.SemanticError);
                }
                return false;
            }


            State = CompilationState.BytecodeGeneration;

            BuildNovFile();

            State = CompilationState.End;

            if (Result == null)
            {
                return false;
            }

            return true;
        }

        /* Tests */
        public void PrintMainByteCode()
        {
            Result.GetMainMethod().Print();
        }



        private static NvFile OpenNvFile(string path)
        {
            NvFile result = new NvFile(path);

            if (!result.Read())
            {
                return null;
            }

            return result;
        }
        private bool CreateContainer(NvFile mainFile)
        {
            this.Container = new ClassesContainer();
            return CreateContainerRecursively(mainFile, new List<string>());

        }
        private bool CreateContainerRecursively(NvFile file, List<string> usings)
        {
            foreach (var @class in file.Classes)
            {
                if (Container.ContainsClass(@class.ClassName))
                {
                    Logger.Write("Redefinition of class: \"" + @class.ClassName + "\"", LogType.Error);
                    return false;
                }
                this.Container.Add(@class);
            }

            foreach (var @using in file.Usings)
            {
                if (!usings.Contains(@using.Value))
                {
                    string path = string.Empty;

                    if (@using.Type == UsingType.Ref)
                    {
                        path = Path.Combine(Path.GetDirectoryName(InputFilePath), @using.Value);
                    }
                    else if (@using.Type == UsingType.Std)
                    {
                        path = Path.Combine(STANDARD_LIBRARY_PATH, @using.Value + Constants.NovaSourceFileExtension);
                    }
                    else
                    {
                        throw new Exception();
                    }

                    NvFile nvFile = OpenNvFile(path);

                    if (nvFile == null)
                    {
                        return false;
                    }

                    usings.Add(@using.Value);

                    return CreateContainerRecursively(nvFile, usings);
                }
            }
            return true;
        }
        public void Save()
        {
            if (File.Exists(OutputPath))
            {
                File.Delete(OutputPath);
            }

            FileStream stream = new FileStream(OutputPath, FileMode.Append);
            CppBinaryWriter writer = new CppBinaryWriter(stream);
            this.Result.Serialize(writer);
            writer.Close();
            stream.Close();

        }

        private void BuildNovFile()
        {

            Method mainEntryPoint = Container.ComputeEntryPoint();

            if (mainEntryPoint != null)
            {
                this.Result = new NovFile();

                this.Result.MainPointEntry = new MainPointEntry(Container.GetClassId(mainEntryPoint.ParentClass), mainEntryPoint.Id);

                foreach (var @class in Container)
                {
                    ByteClass byteClass = (ByteClass)@class.Value.GetByteElement(Result, Container, null);
                    Result.ByteClasses.Add(byteClass);
                }
            }
            else
            {
                Logger.Write("Invalid or multiple program entry point.", LogType.Error);
            }

        }

    }
}
