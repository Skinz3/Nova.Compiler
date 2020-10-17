using Nova.Bytecode.Codes;
using Nova.ByteCode.Codes;
using Nova.ByteCode.Generation;
using Nova.Expressions;
using Nova.IO;
using Nova.Members;
using Nova.Semantics;
using Nova.Statements;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Lexer
{
    /*
     * bad name
     */
    public class ExpressionNode : IChild
    {
        /*
         * Postfix
         */
        private List<Expression> Tree
        {
            get;
            set;
        }
        public IChild Parent
        {
            get;
            private set;
        }

        public Class ParentClass => Parent.ParentClass;

        public bool Empty => Tree.Count == 0;

        public ExpressionNode(IChild parent, Expression value)
        {
            this.Parent = parent;
            Tree = new List<Expression>() { value };
        }

        public ExpressionNode(IChild parent)
        {
            this.Parent = parent;
            Tree = new List<Expression>();
        }

        public void GenerateBytecode(ClassesContainer container, ByteBlock context)
        {
            if (Empty)
            {
                context.Instructions.Add(new PushNullCode());
            }
            else
            {
                int i = 0;

                while (i < Tree.Count)
                {
                    Tree[i].GenerateBytecode(container, context);
                    i++;
                }

            }
        }
        public void Add(Expression expression)
        {
            this.Tree.Insert(0, expression);
        }
        public bool ValidateSemantics(SemanticsValidator validator)
        {
            if (Empty)
            {
                return true;

            }
            foreach (var expression in Tree)
            {
                expression.ValidateSemantics(validator);
            }

            return true;
        }
    }

}
