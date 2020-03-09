using Nova.Utils;
using Nova.Utils.IO;
using Nova.VirtualMachine.Members;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.VirtualMachine.IO
{
    public class NovFile
    {
        const string HEADER = "NovaEX";

        private string Filename
        {
            get;
            set;
        }
        private MainPointEntry MainPointEntry
        {
            get;
            set;
        }
        public List<ByteClass> Classes
        {
            get;
            private set;
        }
        public NovFile(string filename)
        {
            this.Filename = filename;
            this.MainPointEntry = new MainPointEntry();
            this.Classes = new List<ByteClass>();
        }
        public bool Deserialize()
        {
            FileStream stream = new FileStream(Filename, FileMode.Open);
            CppBinaryReader reader = new CppBinaryReader(stream);

            if (reader.ReadString() != HEADER)
            {
                Logger.Write("Corrupted .nov file: Wrong header.", LogType.Error);
                return false;
            }

            MainPointEntry.Deserialize(reader);

            int classesCount = reader.ReadInt32();

            for (int i = 0; i < classesCount; i++)
            {
                ByteClass byteClass = new ByteClass();
                byteClass.Deserialize(reader);
                Classes.Add(byteClass);

            }


            stream.Close();
            reader.Close();


            return true;
        }

        public ByteMethod GetMainMethod()
        {
            return Classes[MainPointEntry.ClassIndex].Methods[MainPointEntry.MethodIndex];

        }
    }
}
