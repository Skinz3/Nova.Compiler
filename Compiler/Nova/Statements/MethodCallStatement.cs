using Nova.IO;
using Nova.Lexer;
using Nova.Lexer.Tokens;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Nova.Members;
using Nova.ByteCode.Codes;
using Nova.ByteCode.Generation;
using Nova.Semantics;
using Nova.Bytecode.Codes;
using Nova.Bytecode.Symbols;
using Nova.Lexer.Accessors;

namespace Nova.Statements
{
    public class MethodCallStatement : Statement
    {
        public const string REGEX = @"^([a-zA-Z_$][a-zA-Z_._$0-9]*)\((.*)\)$";

        /// <summary>
        /// Nom de la methode.
        /// </summary>
        private Accessor MethodName
        {
            get;
            set;
        }
        /// <summary>
        /// Paramètres passés a la méthodes (liste d'Lexer)
        /// Verifier qu'il s'agit bien de statement valide pour une methode. (ne retourne pas void). (analyse sémantique)
        /// </summary>
        private StatementNode[] Parameters
        {
            get;
            set;
        }
        public MethodCallStatement(IParentBlock parent, string line, int lineIndex) : base(parent, line, lineIndex)
        {

        }
        public MethodCallStatement(IParentBlock parent, string line, int lineIndex, Match match) : base(parent, line, lineIndex)
        {
            this.MethodName = new MethodAccessor(match.Groups[1].Value);
            string parametersStr = match.Groups[2].Value;
            this.Parameters = Parser.ParseMethodCallParameters(parent, lineIndex, parametersStr);
        }
        public MethodCallStatement(IParentBlock parent) : base(parent)
        {

        }

        public MethodCallStatement(IParentBlock parent, string line, int lineIndex, string methodName, string parametersStr) : base(parent, line, lineIndex)
        {
            this.MethodName = new MethodAccessor(methodName);
            this.Parameters = Parser.ParseMethodCallParameters(parent, lineIndex, parametersStr);
        }
        public MethodCallStatement(IParentBlock parent, string line, int lineIndex, string methodName, StatementNode[] parameters) : base(parent, line, lineIndex)
        {
            this.MethodName = new MethodAccessor(methodName);
            this.Parameters = parameters;
        }
        private void GenerateStructAccessorBytecode(ByteBlockMetadata context, int loadStart)
        {
            for (int i = loadStart; i < MethodName.Elements.Count - 1; i++)
            {
                context.Results.Add(new StructLoadMemberCode(MethodName.GetElement<Field>(i).Id));
            }
        }
        public override void GenerateBytecode(ClassesContainer container, ByteBlockMetadata context)
        {
            foreach (var parameter in Parameters)
            {
                parameter.GenerateBytecode(container, context);
            }

            switch (this.MethodName.Category)
            {
                case SymbolType.NoSymbol: // should be member function.
                    var target = this.MethodName.GetRoot<Method>();
                    context.Results.Add(new MethodCallCode(target.Id, Parameters.Length));
                    break;
                case SymbolType.Local: // un struct local.

                    Variable variable = this.MethodName.GetRoot<Variable>();

                    context.Results.Add(new LoadCode(context.SymbolTable.GetSymbol(variable.Name).Id));

                    GenerateStructAccessorBytecode(context, 1);

                    Method targetMethod = MethodName.GetLeaf<Method>();
                    context.Results.Add(new StructCallMethodCode(targetMethod.Id, Parameters.Length));
                    break;

                case SymbolType.ClassMember: // un struct de classe

                    Field field = this.MethodName.GetRoot<Field>();

                    context.Results.Add(new LoadStaticMemberCode(field.Id));

                    GenerateStructAccessorBytecode(context, 1);

                    context.Results.Add(new StructCallMethodCode(MethodName.GetLeaf<Method>().Id, Parameters.Length));

                    break;

                case SymbolType.StructMember:

                    context.Results.Add(new StructPushCurrent());

                    GenerateStructAccessorBytecode(context, 0);

                    context.Results.Add(new StructCallMethodCode(MethodName.GetLeaf<Method>().Id, Parameters.Length));

                    break;

                case SymbolType.StaticExternal:

                    if (MethodName.Elements.Count == 2) // Nova.PrintLine()
                    {
                        // (Parent.ParentClass.Methods.ContainsKey(MethodName.GetRoot()))  => optimisation.

                        target = MethodName.GetLeaf<Method>();
                        Class owner = MethodName.GetRoot<Class>();

                        context.Results.Add(new MethodCallStaticCode(owner.ClassName, target.Id, Parameters.Length));
                    }
                    else // Nova.humain.method(); where Nova is a static external class
                    {
                        field = this.MethodName.GetElement<Field>(1);

                        Class owner = MethodName.GetElement<Class>(0);
                        context.Results.Add(new LoadStaticCode(owner.ClassName, field.Id));

                        GenerateStructAccessorBytecode(context, 2);

                        target = this.MethodName.GetLeaf<Method>();
                        context.Results.Add(new StructCallMethodCode(target.Id, Parameters.Length));

                    }
                    break;
            }


        }

        public override void ValidateSemantics(SemanticsValidator validator) // methode accessible, nombre de parametres corrects.
        {
            MethodName.Validate(validator, this.Parent.ParentClass, LineIndex);

            /* if (target.Parameters.Count != Parameters.Length)
             {
                 validator.AddError("Method \"" + target.ToString() + "\" requires " + target.Parameters.Count + " parameters", LineIndex);
             }
             */
            foreach (var parameter in Parameters)
            {
                parameter.ValidateSemantics(validator);
            }
        }
    }
}
