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

namespace Nova.Statements
{
    public class MethodCallStatement : Statement
    {
        public const string REGEX = @"^([a-zA-Z_$][a-zA-Z_._$0-9]*)\((.*)\)$";

        /// <summary>
        /// Nom de la methode.
        /// </summary>
        private MemberName MethodName
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
            this.MethodName = new MemberName(match.Groups[1].Value);
            string parametersStr = match.Groups[2].Value;
            this.Parameters = Parser.ParseMethodCallParameters(parent, lineIndex, parametersStr);
        }
        public MethodCallStatement(IParentBlock parent) : base(parent)
        {

        }

        public MethodCallStatement(IParentBlock parent, string line, int lineIndex, string methodName, string parametersStr) : base(parent, line, lineIndex)
        {
            this.MethodName = new MemberName(methodName);
            this.Parameters = Parser.ParseMethodCallParameters(parent, lineIndex, parametersStr);
        }
        public MethodCallStatement(IParentBlock parent, string line, int lineIndex, string methodName, StatementNode[] parameters) : base(parent, line, lineIndex)
        {
            this.MethodName = new MemberName(methodName);
            this.Parameters = parameters;
        }
        public override void GenerateBytecode(ClassesContainer container, ByteBlockMetadata context)
        {
            foreach (var parameter in Parameters)
            {
                parameter.GenerateBytecode(container,context);
            }
            var symInfo = DeduceSymbolCategory(context, MethodName, this.Parent.ParentClass);

            switch (symInfo)
            {
                case SymbolType.Local: // un struct local.

                    context.Results.Add(new LoadCode(context.SymbolTable.GetLocal(this.MethodName.GetRoot()).Id));

                    for (int i = 1; i < MethodName.Elements.Length - 1; i++)
                    {
                        context.Results.Add(new StructLoadMemberCode(MethodName.Elements[i]));
                    }

                    context.Results.Add(new StructCallMethodCode(MethodName.GetLeaf(), Parameters.Length));


                    break;
                case SymbolType.ClassMember: // un struct de classe

                    context.Results.Add(new LoadStaticMemberCode(MethodName.GetRoot()));

                    for (int i = 1; i < MethodName.Elements.Length - 1; i++)
                    {
                        context.Results.Add(new StructLoadMemberCode(MethodName.Elements[i]));
                    }

                    context.Results.Add(new StructCallMethodCode(MethodName.GetLeaf(), Parameters.Length));

                    break;

                case SymbolType.StructMember:

                    context.Results.Add(new StructPushCurrent());

                    for (int i = 0; i < MethodName.Elements.Length - 1; i++)
                    {
                        context.Results.Add(new StructLoadMemberCode(MethodName.Elements[i]));
                    }

                    context.Results.Add(new StructCallMethodCode(MethodName.GetLeaf(), Parameters.Length));
                    break;

                case SymbolType.StaticExternal:

                    if (MethodName.Elements.Length == 2)
                    {
                        if (Parent.ParentClass.Methods.ContainsKey(MethodName.GetRoot()))
                        {
                            context.Results.Add(new MethodCallCode(MethodName.GetRoot(), Parameters.Length));
                        }
                        else
                        {
                            context.Results.Add(new MethodCallStaticCode(MethodName.Elements[0], MethodName.Elements[1], Parameters.Length));
                        }
                    }
                    else
                    {
                        context.Results.Add(new LoadStaticCode(MethodName.Elements[0], MethodName.Elements[1]));

                        for (int i = 2; i < MethodName.Elements.Length-1; i++)
                        {
                            context.Results.Add(new StructLoadMemberCode(MethodName.Elements[i]));
                        }
                        context.Results.Add(new StructCallMethodCode(MethodName.GetLeaf(), Parameters.Length));

                    }
                    break;
            }

        
          
        }

        public override void ValidateSemantics(SemanticsValidator validator) // methode accessible, nombre de parametres corrects.
        {
            return;
            var target = validator.GetMethod(this.Parent.ParentClass, this.MethodName);

            if (target == null)
            {
                validator.AddError("Undefined reference to method : \"" + this.MethodName.Raw + "()\"", LineIndex);
                return;
            }

            if (target.Parameters.Count != Parameters.Length)
            {
                validator.AddError("Method \"" + target.ToString() + "\" requires " + target.Parameters.Count + " parameters", LineIndex);
            }

            foreach (var parameter in Parameters)
            {
                parameter.ValidateSemantics(validator);
            }
        }
    }
}
