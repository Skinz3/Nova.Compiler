using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.IO
{
    public class NOVFile
    {
        private string Filepath
        {
            get;
            set;
        }
        public NOVFile(string filepath)
        {
            this.Filepath = filepath;
            this.Deserialize();
        }

        private void Deserialize()
        {
            FileStream stream = new FileStream(Filepath, FileMode.Open);
            BinaryReader reader = new BinaryReader(stream);

            Console.WriteLine(reader.ReadInt32());

            stream.Close();
            reader.Close();
        }
    }
}
