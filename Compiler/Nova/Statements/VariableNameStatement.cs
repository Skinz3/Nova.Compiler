using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nova.Bytecode.Codes;
using Nova.ByteCode.Codes;
using Nova.ByteCode.Generation;
using Nova.Lexer;
using Nova.IO;
using Nova.Members;
using Nova.Semantics;

namespace Nova.Statements
{
    public class VariableNameStatement : Statement
    {
        public const string REGEX = @"^([a-zA-Z_$][a-zA-Z_$0-9]*)\s*$";

        private MemberName Name
        {
            get;
            set;
        }
        public VariableNameStatement(IParentBlock parent, string input, int lineIndex) : base(parent, input, lineIndex)
        {
            this.Name = new MemberName(input);
        }

        public VariableNameStatement(IParentBlock parent) : base(parent)
        {

        }

        public override void GenerateBytecode(ByteBlockMetadata context)
        {
            if (this.Name.IsMemberOfParent())
            {
                int localVariableId = context.GetLocalVariableId(Name.Raw);

                if (localVariableId != -1)
                {
                    context.Results.Add(new LoadCode(localVariableId)); // or load global is the target is not local (we lack informations)
                }
                else
                {
                    context.Results.Add(new LoadMemberCode(Name.Raw));
                }
            }
            else
            {
                int objId = context.GetLocalVariableId(Name.GetRoot());

                if (objId == -1)
                {
                    context.Results.Add(new LoadStaticCode(Name.GetRoot(), Name.Elements[1]));

                }
                else
                {
                    context.Results.Add(new ObjectLoadCode(objId, Name.Elements[1]));
                }
            }
        }
        public override void ValidateSemantics(SemanticsValidator validator)
        {
            if (!validator.IsVariableDeclared(this.Parent.ParentClass, Name))
            {
                validator.AddError("Undefined reference to \"" + Name.Raw + "\"", LineIndex);
            }
        }
    }
}
