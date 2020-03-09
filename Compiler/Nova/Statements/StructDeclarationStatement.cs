using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Nova.Bytecode.Codes;
using Nova.Bytecode.Enums;
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
        private string StructTypeStr
        {
            get;
            set;
        }
        public StructDeclarationStatement(IParentBlock parent) : base(parent)
        {

        }

        public StructDeclarationStatement(IParentBlock parent, string input, int lineIndex) : base(parent, input, lineIndex)
        {
        }
        public StructDeclarationStatement(IParentBlock parent, string input, int lineIndex, Match match) : base(parent, input, lineIndex)
        {
            this.StructTypeStr = match.Groups[1].Value;
            this.Name = match.Groups[2].Value;
            string parametersStr = match.Groups[3].Value;
            this.CtorParameters = Parser.ParseMethodCallParameters(Parent, LineIndex, parametersStr);
        }

        public override void GenerateBytecode(ClassesContainer container, ByteBlock context)
        {
            int variableId = context.SymbolTable.Bind(Name, StructTypeStr);

            context.Instructions.Add(new StructCreateCode(container.GetClassId(StructTypeStr)));

            GenerateCtorBytecode(container[StructTypeStr].GetCtor(), container, context, CtorParameters);

            context.Instructions.Add(new StoreCode(variableId));

        }

        public static void GenerateCtorBytecode(Method ctorMethod, ClassesContainer container, ByteBlock context, StatementNode[] ctorParams)
        {
            if (ctorMethod == null)
            {
                return;
            }
            foreach (var parameter in ctorParams)
            {
                parameter.GenerateBytecode(container, context);
            }

            context.Instructions.Add(new CtorCallCode(ctorMethod.Id, ctorParams.Length));
        }

        public override void ValidateSemantics(SemanticsValidator validator)
        {
            this.StructType = validator.Container.TryGetClass(StructTypeStr);
            ValidateStructSemantics(this.StructType, CtorParameters, validator, LineIndex);
            validator.DeclareVariable(this.Name, this.StructTypeStr);
        }


        public static void ValidateStructSemantics(Class structType, StatementNode[] ctorParameters, SemanticsValidator validator, int lineIndex)
        {
            if (structType.Type != ContainerType.@struct)
            {
                validator.AddError("Type \"" + structType.ClassName + "\" is not a struct class. Cannot be instantiated", lineIndex);
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
                    validator.AddError("Invalid parameters for ctor \"" + structType + "\"", lineIndex);
                }

            }

        }
    }
}
