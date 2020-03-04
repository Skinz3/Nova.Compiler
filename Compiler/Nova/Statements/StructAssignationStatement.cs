using Nova.Bytecode.Codes;
using Nova.Bytecode.Enums;
using Nova.Bytecode.Symbols;
using Nova.ByteCode.Codes;
using Nova.ByteCode.Generation;
using Nova.IO;
using Nova.Lexer;
using Nova.Lexer.Accessors;
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

        private VariableAccessor Target
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
            this.Target = new VariableAccessor(match.Groups[1].Value);
            string parametersStr = match.Groups[2].Value;
            this.CtorParameters = Parser.ParseMethodCallParameters(Parent, LineIndex, parametersStr);
        }

        public override void GenerateBytecode(ClassesContainer container, ByteBlockMetadata context)
        {
            context.Results.Add(new StructCreateCode(container.GetClassId(this.StructType.ClassName)));

            StructDeclarationStatement.GenerateCtorBytecode(this.StructType.GetCtor(), container, context, CtorParameters);

            AssignationStatement.GenerateAssignation(container,context, Target);
        }

        public override void ValidateSemantics(SemanticsValidator validator)
        {
            this.Target.Validate(validator, this.Parent.ParentClass, LineIndex);

            switch (this.Target.Category)
            {
                case SymbolType.NoSymbol:
                    throw new NotImplementedException();
                case SymbolType.Local:
                    Variable variable = this.Target.GetRoot<Variable>();
                    this.StructType = validator.Container.TryGetClass(variable.Name);
                    break;
                case SymbolType.ClassMember:
                case SymbolType.StructMember:
                    Field field = this.Target.GetRoot<Field>();
                    this.StructType = validator.Container.TryGetClass(field.Type);
                    break;
                case SymbolType.StaticExternal:
                    field = this.Target.GetElement<Field>(1);
                    this.StructType = validator.Container.TryGetClass(field.Type);
                    break;
            }

            StructDeclarationStatement.ValidateStructSemantics(StructType, CtorParameters, validator, LineIndex);

        }
    }
}
