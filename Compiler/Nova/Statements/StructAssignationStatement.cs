using Nova.Bytecode.Codes;
using Nova.Bytecode.Enums;
using Nova.Bytecode.Symbols;
using Nova.ByteCode.Codes;
using Nova.ByteCode.Generation;
using Nova.IO;
using Nova.Lexer;
using Nova.Members;
using Nova.Semantics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Nova.Statements
{
    public class StructAssignationStatement : Statement
    {
        public const string REGEX = @"^([a-zA-Z_$][a-zA-Z_._$0-9]*)\s*=>\s*\((.*)\)$";

        private MemberName Target
        {
            get;
            set;
        }
        private StatementNode[] CtorParameters
        {
            get;
            set;
        }
        private Class StructType
        {
            get;
            set;
        }
        public StructAssignationStatement(IParentBlock parent, string input, int lineIndex, Match match) : base(parent, input, lineIndex)
        {
            this.Target = new MemberName(match.Groups[1].Value);
            string parametersStr = match.Groups[2].Value;
            this.CtorParameters = Parser.ParseMethodCallParameters(Parent, LineIndex, parametersStr);
        }

        public override void GenerateBytecode(ClassesContainer container, ByteBlockMetadata context)
        {
            var symInfo = DeduceSymbolCategory(context, Target, this.Parent.ParentClass);

            context.Results.Add(new StructCreateCode(this.StructType.ClassName));

            if (this.StructType.GetCtor() != null)
            {
                StructDeclarationStatement.GenerateCtorBytecode(container, context, CtorParameters);
            }

            AssignationStatement.GenerateAssignation(context, Target, symInfo);
        }

        public override void ValidateSemantics(SemanticsValidator validator)
        {
            if (!validator.IsVariableDeclared(this.Parent.ParentClass, Target))
            {
                validator.AddError("Undefined reference to struct: " + Target.Raw, LineIndex);
            }

            this.StructType = this.ComputeStructType(validator);

            StructDeclarationStatement.ValidateStructSemantics(StructType, CtorParameters, validator, LineIndex);
      
        }
        private Class ComputeStructType(SemanticsValidator validator)
        {
            string type = string.Empty;

            string root = Target.GetRoot();

            Variable symbol = validator.GetDeclaredVariable(this.Parent.ParentClass, new MemberName(root));

            if (symbol != null)
            {
                type = symbol.Type;
            }
            else if (this.Parent.ParentClass.Fields.ContainsKey(root))
            {
                Field field = this.Parent.ParentClass.Fields[root];

                for (int i = 1; i < this.Target.Elements.Length; i++)
                {
                    Class fType = validator.Container.TryGetClass(field.Type);

                    field = fType.Fields[Target.Elements[i]];
                }

                type = field.Type;
            }
            else
            {
                type = validator.Container.TryGetClass(Target.Elements[0]).Fields[Target.Elements[1]].Type;
            }

            return validator.Container.TryGetClass(type);
        }
    }
}
