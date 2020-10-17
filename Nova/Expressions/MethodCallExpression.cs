using Antlr4.Runtime;
using Nova.Bytecode.Codes;
using Nova.Bytecode.Symbols;
using Nova.ByteCode.Codes;
using Nova.ByteCode.Generation;
using Nova.IO;
using Nova.Lexer;
using Nova.Members;
using Nova.Parser.Accessors;
using Nova.Semantics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Expressions
{
    public class MethodCallExpression : Expression
    {
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
        public List<ExpressionNode> Parameters
        {
            get;
            set;
        }
        public MethodCallExpression(IChild parent, ParserRuleContext context, string methodName) : base(parent, context)
        {
            this.MethodName = new MethodAccessor(methodName);
        }
        private void GenerateStructAccessorBytecode(ByteBlock context, int loadStart)
        {
            for (int i = loadStart; i < MethodName.Elements.Count - 1; i++)
            {
                context.Instructions.Add(new StructLoadMemberCode(MethodName.GetElement<Field>(i).Id));
            }
        }
        public override void GenerateBytecode(ClassesContainer container, ByteBlock context)
        {
            foreach (var parameter in Parameters)
            {
                parameter.GenerateBytecode(container, context);
            }

            switch (this.MethodName.Category)
            {
                case SymbolType.NoSymbol: // should be member function.
                    var target = this.MethodName.GetRoot<Method>();
                    context.Instructions.Add(new MethodCallCode(container.GetClassId(this.Parent.ParentClass.ClassName), target.Id));
                    break;
                case SymbolType.Local: // un struct local.

                    Variable variable = this.MethodName.GetRoot<Variable>();

                    context.Instructions.Add(new LoadCode(context.SymbolTable.GetSymbol(variable.Name).Id));

                    GenerateStructAccessorBytecode(context, 1);

                    Method targetMethod = MethodName.GetLeaf<Method>();
                    context.Instructions.Add(new StructCallMethodCode(targetMethod.Id));
                    break;

                case SymbolType.ClassMember: // un struct de classe

                    Field field = this.MethodName.GetRoot<Field>();

                    context.Instructions.Add(new LoadGlobalCode(container.GetClassId(this.Parent.ParentClass.ClassName), field.Id));

                    GenerateStructAccessorBytecode(context, 1);

                    context.Instructions.Add(new StructCallMethodCode(MethodName.GetLeaf<Method>().Id));

                    break;

                case SymbolType.StructMember:

                    context.Instructions.Add(new StructPushCurrent());

                    GenerateStructAccessorBytecode(context, 0);

                    context.Instructions.Add(new StructCallMethodCode(MethodName.GetLeaf<Method>().Id));

                    break;

                case SymbolType.StaticExternal:

                    if (MethodName.Elements.Count == 2) // Nova.PrintLine()
                    {
                        // (Parent.ParentClass.Methods.ContainsKey(MethodName.GetRoot()))  => optimisation.

                        target = MethodName.GetLeaf<Method>();
                        Class owner = MethodName.GetRoot<Class>();

                        context.Instructions.Add(new MethodCallCode(container.GetClassId(owner.ClassName), target.Id));
                    }
                    else // Nova.humain.method(); where Nova is a static external class
                    {
                        field = this.MethodName.GetElement<Field>(1);

                        Class owner = MethodName.GetElement<Class>(0);
                        context.Instructions.Add(new LoadGlobalCode(container.GetClassId(owner.ClassName), field.Id));

                        GenerateStructAccessorBytecode(context, 2);

                        target = this.MethodName.GetLeaf<Method>();
                        context.Instructions.Add(new StructCallMethodCode(target.Id));

                    }
                    break;
            }


        }

        public override void ValidateSemantics(SemanticsValidator validator) // methode accessible, nombre de parametres corrects.
        {
            if (!MethodName.Validate(validator, this.Parent.ParentClass, ParsingContext))
            {
                return;
            }

            var target = MethodName.GetLeaf<Method>();

            if (target != null)
            {
                if (target.Parameters.Count != Parameters.Count)
                {
                    validator.AddError("Method \"" + target.ToString() + "\" requires " + target.Parameters.Count + " parameters", ParsingContext);
                }
            }

            foreach (var parameter in Parameters)
            {
                parameter.ValidateSemantics(validator);
            }
        }
    }
}
