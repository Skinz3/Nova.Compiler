using Nova.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Members
{
    public class Variable 
    {
        public string Name
        {
            get;
            set;
        }
        public string Type
        {
            get;
            set;
        }
        public Variable(string name, string type)
        {
            this.Name = name;
            this.Type = type;
        }
        public Variable()
        {

        }

        public override string ToString()
        {
            return Type + " " + Name;
        }
    }
}
