using Nova.ByteCode;
using Nova.ByteCode.IO;
using Nova.ByteCode.Runtime;
using Nova.Semantics;
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
        public static bool Build(string ouputPath, IEnumerable<NvFile> files)
        {
            ClassesContainer container = new ClassesContainer();

            foreach (var file in files) // foreach fall usings, import recursively
            {
                container.AddRange(file.GetClasses());
            }

            List<SemanticalError> errors = new List<SemanticalError>();

            foreach (var @class in container.GetClasses())
            {
                errors.AddRange(@class.ValidateSemantics(container));
            }

            if (errors.Count > 0)
            {
                foreach (var error in errors)
                {
                    Logger.Write(error.ToString(), LogType.Error);
                }
                return false;
            }


            NovFile novFile = BuildNovFile(container);


            if (File.Exists(ouputPath))
            {
                File.Delete(ouputPath);
            }

            FileStream stream = new FileStream(ouputPath, FileMode.Append);
            CppBinaryWriter writer = new CppBinaryWriter(stream);
            novFile.Serialize(writer);
            writer.Close();
            stream.Close();

            /* tests */
            Console.WriteLine();
            Logger.Write("-------Main method bytecode--------", LogType.Purple);
            novFile.ByteClasses["Nova2"].Methods[0].Meta.Print();
            Logger.Write("-------Main method bytecode--------", LogType.Purple);
            Console.WriteLine();
            Exec.Run(novFile);
            /* end tests */

            return true;
        }

        private static NovFile BuildNovFile(ClassesContainer container)
        {
            NovFile novFile = new NovFile();

            foreach (var @class in container)
            {
                foreach (var @using in @class.Value.Usings)
                {
                    if (!novFile.Usings.Contains(@using))
                    {
                        novFile.Usings.Add(@using);
                    }
                }


                ByteClass byteClass = (ByteClass)@class.Value.GetByteElement(container, null);
                novFile.ByteClasses.Add(byteClass.Name, byteClass);
            }
            return novFile;
        }


    }
}
