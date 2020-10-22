using Nova.Bytecode.IO;
using Nova.ByteCode.IO;
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
            NvFile file = OpenNvFile(InputFilePath);

            if (file == null)
            {
                return false;
            }

            if (!CreateContainer(file))
            {
                return false;
            }

            List<SemanticalError> errors = new List<SemanticalError>();

            foreach (var @class in Container.GetClasses())
            {
                errors.AddRange(@class.ValidateSemantics(Container));
            }

            if (errors.Count > 0)
            {
                foreach (var error in errors)
                {
                    Logger.Write(error.ToString(), LogType.SemanticError);
                }
                return false;
            }

            BuildNovFile();

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
        public bool ComputeEntryPoint() // rien a faire ici?
        {
            int i = 0;
            foreach (var @class in Result.ByteClasses)
            {
                int j = 0;
                foreach (var @method in @class.Methods)
                {
                    if (method.IsMainPointEntry())
                    {
                        if (Result.MainPointEntry == null)
                        {
                            Result.MainPointEntry = new MainPointEntry(i, j);
                        }
                        else
                        {
                            return false;
                        }
                    }
                    j++;
                }
                i++;
            }

            return true;
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
            this.Result = new NovFile();

            foreach (var @class in Container)
            {
                ByteClass byteClass = (ByteClass)@class.Value.GetByteElement(Result, Container, null);
                Result.ByteClasses.Add(byteClass);
            }

            if (!ComputeEntryPoint())
            {
                Result = null;
            }

        }

    }
}
