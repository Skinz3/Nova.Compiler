using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Nova.Bytecode.Codes;
using Nova.Bytecode.Runtime;
using Nova.ByteCode.Codes;
using Nova.ByteCode.Generation;
using Nova.IO;
using Nova.Lexer;
using Nova.Members;
using Nova.Semantics;

namespace Nova.Statements
{
    public class ObjectDeclarationStatement : Statement
    {
        public static string REGEX = @"^([a-zA-Z_$][a-zA-Z_$0-9]*)\s+([a-zA-Z_$][a-zA-Z_$0-9]*)\s*=>\s*\((.*)\)$";

        private string Type
        {
            get;
            set;
        }
        private string Name
        {
            get;
            set;
        }
        public StatementNode[] CtorParameters
        {
            get;
            set;
        }
        public ObjectDeclarationStatement(IParentBlock parent) : base(parent)
        {

        }

        public ObjectDeclarationStatement(IParentBlock parent, string input, int lineIndex) : base(parent, input, lineIndex)
        {
        }
        public ObjectDeclarationStatement(IParentBlock parent, string input, int lineIndex, Match match) : base(parent, input, lineIndex)
        {
            this.Type = match.Groups[1].Value;
            this.Name = match.Groups[2].Value;

            string parametersStr = match.Groups[3].Value;

            this.CtorParameters = Parser.ParseMethodCallParameters(Parent, LineIndex, parametersStr);
        }

        public override void GenerateBytecode(ByteBlockMetadata context)
        {
            int variableId = context.BindVariable(Name);

            context.Results.Add(new CreateStoreObjectCode(Type, variableId));

            foreach (var param in CtorParameters)
            {
                param.GenerateBytecode(context);
            }

        }

        public override void ValidateSemantics(SemanticsValidator validator)
        {
            if (!validator.IsTypeDefined(this.Type))
            {
                validator.AddError("Unknown type : \"" + this.Type + "\"", LineIndex);
            }
            validator.DeclareVariable(this.Name);
        }
    }
}
