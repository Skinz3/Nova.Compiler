using Nova.Bytecode.Codes;
using Nova.ByteCode.Codes;
using Nova.ByteCode.Generation;
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
    // Node Class: Base for binary tree, holds data for left and right nodes.
    public class StatementNode
    {
        public Statement Value
        {
            get;
            private set;
        }
        private StatementNode Left
        {
            get;
            set;
        }

        private StatementNode Right
        {
            get;
            set;
        }

        private IChild Parent
        {
            get;
            set;
        }
        public string Prefix()
        {
            string res = " " + Value;

            if (this.Left != null)
            {
                res += this.Left.Prefix();

                res += this.Right.Prefix();


            }
            return res;
        }
        public string PostfixStr()
        {
            string res = string.Empty;

            if (this.Left != null)
            {
                res += this.Left.PostfixStr();
                res += this.Right.PostfixStr();
            }
            res += Value;

            return res;
        }
        private Statement[] ComputePostfix()
        {
            List<Statement> result = new List<Statement>();

            if (this.Left != null)
            {
                result.AddRange(this.Left.ComputePostfix());
                result.AddRange(this.Right.ComputePostfix());
            }

            result.Add(this.Value);

            return result.ToArray();
        }

        public bool IsNull()
        {
            return Value == null;
        }
        public bool Single()
        {
            return Right == null && Left == null;
        }
        public string Infix()
        {
            string res = "";
            if (this.Left != null)
            {
                res = res + "(" + Left.Infix() + " " + Value + " " + Right.Infix() + ")";
            }
            else
            {
                res += Value;
            }
            return res;
        }
        public Statement[] Postfix
        {
            get;
            private set;
        }
        public StatementNode(IChild parent, Statement statement, StatementNode left, StatementNode right)
        {
            this.Parent = parent;
            this.Left = left;
            this.Right = right;
            this.Value = statement;
            this.Postfix = this.ComputePostfix();
        }
        public StatementNode(IChild parent, Statement value)
        {
            this.Parent = parent;
            Left = null;
            Right = null;
            Value = value;
        }

        public StatementNode(IChild parent)
        {
            this.Parent = parent;
        }

        public void GenerateBytecode(ClassesContainer container, ByteBlock context)
        {
            if (IsNull())
            {
                context.Instructions.Add(new PushNullCode());
            }
            else
            {
                int i = 0;

                while (i < Postfix.Length)
                {
                    /* var operatorStatement = Postfix[i] as OperatorStatement;

                     if (operatorStatement != null)
                     {
                         OperatorStatement.GenerateBytecode(operatorStatement, Postfix[i - 1], Postfix[i - 2], context);
                     }
                     else
                     { 
                    Postfix[i].GenerateBytecode(container, context);
                     }*/

                    Postfix[i].GenerateBytecode(container, context);
                    i++;
                }

            }
        }

        public bool ValidateSemantics(SemanticsValidator validator)
        {
            if (IsNull())
            {
                return true;

            }
            foreach (var statement in Postfix)
            {
                statement.ValidateSemantics(validator);
            }

            return true;
        }
    }

}
