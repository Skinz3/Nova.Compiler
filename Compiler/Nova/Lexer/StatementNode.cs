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

        private IParentBlock Parent
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
        public StatementNode(IParentBlock parent, Statement statement, StatementNode left, StatementNode right)
        {
            this.Parent = parent;
            this.Left = left;
            this.Right = right;
            this.Value = statement;
            this.Postfix = this.ComputePostfix();
        }
        public StatementNode(IParentBlock parent, Statement value)
        {
            this.Parent = parent;
            Left = null;
            Right = null;
            Value = value;
        }

        public StatementNode(IParentBlock parent)
        {
            this.Parent = parent;
        }

        public void GenerateBytecode(ByteBlockMetadata context)
        {
            if (IsNull())
            {
                context.Results.Add(new PushConstCode(null));
            }
            else
            {
                foreach (var statement in Postfix)
                {
                    statement.GenerateBytecode(context);
                }
            }
        }

        public bool ValidateSemantics(SemanticsValidator validator)
        {
            foreach (var statement in Postfix)
            {
                statement.ValidateSemantics(validator);
            }

            return true;
        }
    }

}
