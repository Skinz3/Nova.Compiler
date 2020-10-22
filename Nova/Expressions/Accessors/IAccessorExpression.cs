using Antlr4.Runtime;
using Nova.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Expressions.Accessors
{
    public abstract class AccessorExpression : Expression
    {
        public AccessorExpression ParentAccessor
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public abstract AccessorType AccessorType
        {
            get;
        }
        public AccessorExpression(IChild parent, ParserRuleContext context) : base(parent, context)
        {

        }

        

    }
}
