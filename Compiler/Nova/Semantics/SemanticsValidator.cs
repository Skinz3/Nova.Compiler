using Nova.IO;
using Nova.Lexer;
using Nova.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Semantics
{
    public class SemanticsValidator
    {
        private Dictionary<int, Dictionary<string, string>> DeclaredVariables // <name,type>
        {
            get;
            set;
        }
        public int Deepness
        {
            get;
            set;
        }
        private List<SemanticalError> Errors
        {
            get;
            set;
        }
        private ClassesContainer Container
        {
            get;
            set;
        }
        public SemanticsValidator(ClassesContainer container)
        {
            this.Deepness = 0;
            this.DeclaredVariables = new Dictionary<int, Dictionary<string, string>>();
            this.DeclaredVariables.Add(0, new Dictionary<string, string>());
            this.Errors = new List<SemanticalError>();
            this.Container = container;
        }

        public void DeclareVariable(string name, string type)
        {
            DeclaredVariables[Deepness].Add(name, type);
        }
        public void BlockStart()
        {
            Deepness++;
            DeclaredVariables.Add(Deepness, new Dictionary<string, string>());
        }
        public void BlockEnd()
        {
            DeclaredVariables.Remove(Deepness);
            Deepness--;
        }
        public Variable GetDeclaredVariable(Class parentClass, MemberName name)
        {
            if (name.NoTree())
            {
                if (parentClass.Fields.ContainsKey(name.Raw))
                {
                    return new Variable(name.Raw, parentClass.Fields[name.Raw].Type);
                }

                for (int i = 0; i <= Deepness; i++)
                {
                    if (DeclaredVariables[i].ContainsKey(name.Raw))
                    {
                        return new Variable(name.Raw, DeclaredVariables[i][name.Raw]);
                    }
                }

                return null;

            }
            else
            {
                string root = name.GetRoot();

                var result = GetDeclaredVariable(parentClass, new MemberName(root));

                if (result != null)
                {
                    return result;
                }
                else
                {
                    if (!this.Container.ContainsClass(name.Elements[0]))
                    {
                        return null;
                    }

                    return new Variable(name.Raw, this.Container[name.Elements[0]].Fields[name.Elements[1]].Type);
                }
            }
        }
        public bool IsVariableDeclared(Class parentClass, MemberName name)
        {
            return GetDeclaredVariable(parentClass, name) != null;

        }

        public bool IsTypeDefined(string type)
        {
            return this.Container.ContainsClass(type);
        }

        public void AddError(string message, int lineIndex)
        {
            this.Errors.Add(new SemanticalError(message, lineIndex));
        }
        public IEnumerable<SemanticalError> GetErrors()
        {
            return Errors;
        }

        public Method GetMethod(Class parentClass, MemberName methodName)
        {
            if (methodName.NoTree())
            {
                return parentClass.Methods[methodName.Raw];
            }
            else
            {
                var variable = GetDeclaredVariable(parentClass, new MemberName(methodName.Elements[0]));

                if (variable != null)
                {
                    if (!Container.ContainsClass(variable.Type))
                    {
                        return null;
                    }
                    if (!Container[variable.Type].Methods.ContainsKey(methodName.Elements[1]))
                    {
                        return null;
                    }
                    return Container[variable.Type].Methods[methodName.Elements[1]];
                }
                else
                {
                    if (!Container.ContainsClass(methodName.Elements[0]))
                    {
                        return null;
                    }

                    var @class = Container[methodName.Elements[0]];

                    Method method = null;

                    if (@class.Methods.TryGetValue(methodName.Elements[1], out method))
                    {
                        return method;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
    }
}
