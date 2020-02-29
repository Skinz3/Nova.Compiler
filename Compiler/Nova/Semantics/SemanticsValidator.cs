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
        private Dictionary<int, List<string>> DeclaredVariables
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
            this.DeclaredVariables = new Dictionary<int, List<string>>();
            this.DeclaredVariables.Add(0, new List<string>());
            this.Errors = new List<SemanticalError>();
            this.Container = container;
        }

        public void DeclareVariable(string name)
        {
            DeclaredVariables[Deepness].Add(name);
        }
        public void BlockStart()
        {
            Deepness++;
            DeclaredVariables.Add(Deepness, new List<string>());
        }
        public void BlockEnd()
        {
            DeclaredVariables.Remove(Deepness);
            Deepness--;
        }
        public bool IsVariableDeclared(Class parentClass, MemberName name)
        {
            if (name.IsMemberOfParent())
            {
                if (parentClass.Fields.ContainsKey(name.Raw))
                {
                    return true;
                }

                for (int i = 0; i <= Deepness; i++)
                {
                    if (DeclaredVariables[i].Contains(name.Raw))
                    {
                        return true;
                    }
                }
                return false;

            }
            else
            {
                string root = name.GetRoot();

                if (IsVariableDeclared(parentClass, new MemberName(root)))
                {
                    throw new Exception("Runtime classes properties are not handled.");
                }
                else
                {
                    return this.Container[name.Elements[0]].Fields.ContainsKey(name.Elements[1]);
                }
            }

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
            if (methodName.IsMemberOfParent())
            {
                return parentClass.Methods[methodName.Raw];
            }
            else
            {
                if (IsVariableDeclared(parentClass, new MemberName(methodName.Elements[0])))
                {
                    // obj member
                    return null;
                }
                else
                {
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
