using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Nova.Bytecode.Enums;
using Nova.Bytecode.Symbols;
using Nova.ByteCode.Enums;
using Nova.Members;
using Nova.Semantics;

namespace Nova.Parser.Accessors
{
    public class MethodAccessor : Accessor
    {
        public MethodAccessor(string raw) : base(raw)
        {
        }
        protected override SymbolType DeduceSymbolCategory(SemanticsValidator context, Class parentClass)
        {
            if (context.IsLocalDeclared(this.GetRoot()))
            {
                return SymbolType.Local;
            }
            else if (parentClass.Fields.ContainsKey(this.GetRoot()))
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
                if (this.ElementsStr.Length == 1)
                {
                    if (parentClass.Methods.ContainsKey(this.Raw) && parentClass.Type == ContainerType.@struct)
                    {
                        return SymbolType.StructMember;
                    }
                  
                    return SymbolType.NoSymbol;
                }
                else
                {
                    return SymbolType.StaticExternal;
                }
            }

            throw new Exception("Unknown symbol type.");
        }
        public override bool Validate(SemanticsValidator validator, Class parentClass, ParserRuleContext context)
        {
            this.Category = DeduceSymbolCategory(validator, parentClass);
            string currentType = null;
            int loadStart = 0;

            switch (Category)
            {
                case SymbolType.NoSymbol: // must be member method.
                    currentType = parentClass.ClassName;
                    break;

                case SymbolType.Local:

                    var variable = validator.GetLocal(GetRoot());

                    if (variable == null)
                    {
                        validator.AddError("Undefined reference to locale \"" + this.Raw + "\"", context);
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

                    Class targetedClass = validator.Container.TryGetClass(this.GetRoot());

                    if (targetedClass == null)
                    {
                        validator.AddError("Undefined reference to method \"" + this.Raw + "\"", context);
                        return false;
                    }
                    if (ElementsStr.Length == 2 && targetedClass.Type == ContainerType.@struct)
                    {
                        validator.AddError("Cannot call struct method without target.", context);
                        return false;
                    }

                    loadStart = 1;
                    currentType = targetedClass.ClassName;

                    this.Elements.Add(targetedClass);

                    break;
            }

            Field field = null;

            for (int i = loadStart; i < this.ElementsStr.Length - 1; i++)
            {
                Class targetClass = validator.Container.TryGetClass(currentType);

                if (!targetClass.Fields.TryGetValue(this.ElementsStr[i], out field))
                {
                    validator.AddError("Type \"" + targetClass.ClassName + "\" has no member \"" + this.ElementsStr[i] + "\"", context);
                    return false;
                }
                if (field.Modifiers == ModifiersEnum.@private && field.ParentClass != parentClass)
                {
                    validator.AddError("Unable to access private field \"" + field.Name + "\" from class \"" + parentClass.ClassName + "\"", context);
                    return false;
                }

                this.Elements.Add(field);
                currentType = field.Type;
            }

            var owner = validator.Container.TryGetClass(currentType);

            Method method = null;

            if (!owner.Methods.TryGetValue(this.GetLeaf(), out method))
            {
                validator.AddError("Type \"" + currentType + "\" has no member \"" + this.GetLeaf() + "\"()", context);
                return false;
            }
            if (method.Modifiers == ModifiersEnum.@private && method.ParentClass != parentClass)
            {
                validator.AddError("Unable to access private method \"" + method.Name + "\" from class \"" + parentClass.ClassName + "\"", context);
                return false;
            }

            this.Elements.Add(method);
            return true;
        }
    }
}
