using Antlr4.Runtime;
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
        public string RawType
        {
            get;
            set;
        }
        public NovaType Type
        {
            get;
            set;
        }
        private ParserRuleContext Context
        {
            get;
            set;
        }
        public Variable(string name, string type, ParserRuleContext context)
        {
            this.Name = name;
            this.RawType = type;
            this.Context = context;
        }

        public void ValidateTypes(SemanticsValidator validator)
        {
            this.Type = validator.Container.TypeManager.GetTypeInstance(RawType);

            if (this.Type == null)
            {
               // validator.AddError("Unknown variable type " + RawType, Context);
            }

        }

        public override string ToString()
        {
            return RawType + " " + Name;
        }

        public Class GetContextualClass(SemanticsValidator validator)
        {
            return validator.Container.TryGetClass(validator.GetLocal(Name).RawType);
        }
    }
}
