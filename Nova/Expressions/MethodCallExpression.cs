using Antlr4.Runtime;
using Nova.Bytecode.Codes;
using Nova.Bytecode.Symbols;
using Nova.ByteCode.Codes;
using Nova.ByteCode.Generation;
using Nova.Expressions.Accessors;
using Nova.IO;
using Nova.Lexer;
using Nova.Members;
using Nova.Semantics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Expressions
{
    public class MethodCallExpression : AccessorExpression
    {
        public override AccessorType AccessorType => AccessorType.Method;

        private AccessorTree AccessorTree
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

        public MethodCallExpression(IChild parent, ParserRuleContext context) : base(parent, context)
        {

        }

        public override void GenerateBytecode(ClassesContainer container, ByteBlock context)
        {
            AccessorTree.GenerateBytecode(container, context);
        }

        public override void ValidateSemantics(SemanticsValidator validator) // methode accessible, nombre de parametres corrects.
        {
            AccessorTree = new AccessorTree(this, false);
            AccessorTree.ValidateSemantics(validator);
        }

        public override string ToString()
        {
            return base.ToString() + " {" + Name + "}";
        }


    }
}
