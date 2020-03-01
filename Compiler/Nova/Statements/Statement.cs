using Nova.ByteCode.Codes;
using Nova.ByteCode.Generation;
using Nova.ByteCode.IO;
using Nova.Lexer;
using Nova.IO;
using Nova.Members;
using Nova.Semantics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nova.Bytecode.Symbols;
using Nova.Bytecode.Enums;

namespace Nova.Statements
{
    public abstract class Statement
    {
        public string Input
        {
            get;
            private set;
        }
        protected IParentBlock Parent
        {
            get;
            private set;
        }
        public int LineIndex
        {
            get;
            private set;
        }

        public Statement(IParentBlock parent, string input, int lineIndex)
        {
            this.Parent = parent;
            this.Input = input;
            this.LineIndex = lineIndex;
        }
        public Statement(IParentBlock parent)
        {
            this.Parent = parent;
        }
        public virtual bool ValidateSyntax()
        {
            return true;
        }

        public override string ToString()
        {
            return string.Format("({0}) {1}", this.GetType().Name, Input);
        }

        public virtual int GetLineSkip()
        {
            return 1;
        }
        /*
         * Cette classe n'a rien a faire ici ! 
         */
        protected SymbolType DeduceSymbolCategory(ByteBlockMetadata context, MemberName name, Class parentClass)
        {
            Symbol localSym = context.SymbolTable.GetLocal(name.GetRoot());

            if (localSym != null)
            {
                return SymbolType.Local;
            }
            else if (parentClass.Fields.ContainsKey(name.GetRoot()))
            {
                switch (parentClass.Type)
                {
                    case ContainerType.@class:
                        return SymbolType.ClassMember;
                    case ContainerType.@struct:
                        return SymbolType.StructMember;
                }

            }
            else
            {
                return SymbolType.StaticExternal;
            }

            throw new Exception("Unknown symbol type.");
        }

        public abstract void GenerateBytecode(ClassesContainer container,ByteBlockMetadata context);

        public abstract void ValidateSemantics(SemanticsValidator validator);

    }
}
