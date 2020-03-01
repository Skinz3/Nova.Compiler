using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Nova.Bytecode.Codes;
using Nova.Bytecode.Enums;
using Nova.Bytecode.Runtime;
using Nova.ByteCode.Codes;
using Nova.ByteCode.Generation;
using Nova.IO;
using Nova.Lexer;
using Nova.Members;
using Nova.Semantics;

namespace Nova.Statements
{
    public class StructDeclarationStatement : Statement
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
        public Class StructType
        {
            get;
            private set;
        }
        public StructDeclarationStatement(IParentBlock parent) : base(parent)
        {

        }

        public StructDeclarationStatement(IParentBlock parent, string input, int lineIndex) : base(parent, input, lineIndex)
        {
        }
        public StructDeclarationStatement(IParentBlock parent, string input, int lineIndex, Match match) : base(parent, input, lineIndex)
        {
            this.Type = match.Groups[1].Value;
            this.Name = match.Groups[2].Value;

            string parametersStr = match.Groups[3].Value;

            this.CtorParameters = Parser.ParseMethodCallParameters(Parent, LineIndex, parametersStr);
        }

        public override void GenerateBytecode(ClassesContainer container, ByteBlockMetadata context)
        {
            int variableId = context.SymbolTable.Bind(Name, Type);

            context.Results.Add(new StructCreateCode(Type));

            if (container[Type].GetCtor() != null)
            {
                GenerateCtorBytecode(container, context, CtorParameters);
            }


            context.Results.Add(new StoreCode(variableId));

        }

        public static void GenerateCtorBytecode(ClassesContainer container, ByteBlockMetadata context, StatementNode[] ctorParams)
        {
            foreach (var parameter in ctorParams)
            {
                parameter.GenerateBytecode(container, context);
            }

            context.Results.Add(new CtorCallCode(ctorParams.Length));
        }

        public override void ValidateSemantics(SemanticsValidator validator)
        {
            this.StructType = validator.Container.TryGetClass(Type);
            ValidateStructSemantics(this.StructType, CtorParameters, validator, LineIndex);
            validator.DeclareVariable(this.Name, this.Type);
        }


        public static void ValidateStructSemantics(Class structType, StatementNode[] ctorParameters, SemanticsValidator validator, int lineIndex)
        {
            if (structType == null)
            {
                validator.AddError("Unknown type : \"" + structType.ClassName + "\"", lineIndex);
            }
            else if (structType.Type != ContainerType.@struct)
            {
                validator.AddError("Type: \"" + structType.ClassName + "\" is not a struct class. Cannot be instantiated", lineIndex);
            }
            else
            {
                Method ctor = structType.GetCtor();

                if (ctor == null)
                {
                    if (ctorParameters.Length > 0)
                    {
                        validator.AddError("Invalid constructor call for type :\"" + structType + "\"", lineIndex);
                    }
                }
                else if (ctor.Parameters.Count != ctorParameters.Length)
                {
                    validator.AddError("Invalid numbers of parameters for type :\"" + structType + "\"", lineIndex);
                }

            }

        }
    }
}
