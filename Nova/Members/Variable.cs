using Nova.IO;
using Nova.Semantics;
using Nova.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Members
{
    public class Variable : IAccessible
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

        public Class GetContextualClass(SemanticsValidator validator)
        {
            return validator.Container.TryGetClass(validator.GetLocal(Name).Type);
        }
    }
}
