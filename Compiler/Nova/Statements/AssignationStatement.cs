using Nova.ByteCode.Codes;
using Nova.ByteCode.Generation;
using Nova.Lexer;
using Nova.Lexer.Tokens;
using Nova.IO;
using Nova.Members;
using Nova.Semantics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Nova.Bytecode.Codes;

namespace Nova.Statements
{
    public class AssignationStatement : Statement
    {
        public static string REGEX = @"^([a-zA-Z_$][a-zA-Z_._$0-9]*)\s*(\+|-|\*|\/)?=\s*(.+)$";

        private MemberName Target
        {
            get;
            set;
        }
        /// <summary>
        /// null -> '='
        /// </summary>
        private char? Operator
        {
            get;
            set;
        }
        private StatementNode Value
        {
            get;
            set;
        }
        public AssignationStatement(IParentBlock parent, string line, int lineIndex, Match match) : base(parent, line, lineIndex)
        {
            this.Target = new MemberName(match.Groups[1].Value);

            if (match.Groups[2].Length > 0)
                this.Operator = match.Groups[2].Value[0];

            string valueStr = match.Groups[3].Value;

            this.Value = StatementTreeBuilder.Build(parent, valueStr, lineIndex);
        }
        public AssignationStatement(IParentBlock parent) : base(parent)
        {

        }


        public override void GenerateBytecode(ByteBlockMetadata context)
        {
            Value.GenerateBytecode(context);

            if (this.Target.IsMemberOfParent())
            {
                int localVariableId = context.GetLocalVariableId(Target.Raw);

                if (localVariableId != -1)
                {
                    context.Results.Add(new StoreCode(localVariableId)); // or load global is the target is not local (we lack informations)
                }
                else
                {
                    context.Results.Add(new StoreStaticMemberCode(Target.Raw));
                }
            }
            else
            {
                context.Results.Add(new StoreStaticCode(Target.Elements[0], Target.Elements[1]));
            }
        }

        public override void ValidateSemantics(SemanticsValidator validator)
        {
            if (!validator.IsVariableDeclared(this.Parent.ParentClass, Target))
            {
                validator.AddError("Undefined reference to \"" + Target + "\"", LineIndex);
            }
            Value.ValidateSemantics(validator);
        }
    }
}
