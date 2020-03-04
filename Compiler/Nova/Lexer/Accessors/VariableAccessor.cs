using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nova.Bytecode.Symbols;
using Nova.Members;
using Nova.Semantics;

namespace Nova.Lexer.Accessors
{
    public class VariableAccessor : Accessor
    {
        public VariableAccessor(string raw) : base(raw)
        {

        }

        public override bool Validate(SemanticsValidator validator, Class parentClass, int lineIndex)
        {
            this.Category = DeduceSymbolCategory(validator, parentClass);
           
            string currentType = null;
            int loadStart = 0;

            switch (Category)
            {
                case SymbolType.NoSymbol:
                    validator.AddError("Undefined reference to locale \"" + this.Raw + "\"", lineIndex);
                    return false;
                case SymbolType.Local:

                    var variable = validator.GetLocal(GetRoot());

                    if (variable == null)
                    {
                        validator.AddError("Undefined reference to locale \"" + this.Raw + "\"", lineIndex);
                        return false;
                    }

                    currentType = variable.Type;
                    loadStart = 1;

                    Elements.Add(variable);

                    break;
                case SymbolType.ClassMember:

                    var target = parentClass.Fields[this.GetRoot()];

                    Elements.Add(target);

                    currentType = target.Type;
                    loadStart = 1;

                    break;
                case SymbolType.StructMember:

                    loadStart = 0;
                    currentType = parentClass.ClassName;

                    break;
                case SymbolType.StaticExternal:

                    Class targetClass = validator.Container.TryGetClass(this.GetRoot());

                    if (targetClass == null)
                    {
                        validator.AddError("Undefined reference to variable \"" + this.Raw + "\"", lineIndex);
                        return false;
                    }
                    Field targetField = null;

                    if (!targetClass.Fields.TryGetValue(this.ElementsStr[1], out targetField))
                    {
                        validator.AddError("Type \"" + targetClass.ClassName + "\" has no member \"" + this.ElementsStr[1] + "\"", lineIndex);
                        return false;
                    }

                    this.Elements.Add(targetClass);
                    this.Elements.Add(targetField);

                    loadStart = 2;
                    currentType = targetField.Type;

                    break;
            }

            Field field = null;

            for (int i = loadStart; i < this.ElementsStr.Length; i++)
            {
                Class targetClass = validator.Container.TryGetClass(currentType);

                if (targetClass == null)
                {
                    validator.AddError("Not implemented error (VariableAccessor.cs).", lineIndex);
                    return false;
                }
                if (!targetClass.Fields.TryGetValue(this.ElementsStr[i], out field))
                {
                    validator.AddError("Type \"" + targetClass.ClassName + "\" has no member \"" + this.ElementsStr[i] + "\"", lineIndex);
                    return false;
                }

                this.Elements.Add(field);
                currentType = field.Type;
            }

            return true;
        }
    }
}
