using Antlr4.Runtime;
using Nova.IO;
using Nova.Lexer;

using Nova.Members;
using Nova.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Semantics
{
    public class SemanticsValidator
    {
        private Dictionary<int, Dictionary<string, Variable>> DeclaredVariables // <name,type>
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
        public ClassesContainer Container
        {
            get;
            private set;
        }
        private Class CurrentClass
        {
            get;
            set;
        }

        public SemanticsValidator(Class currentClass, ClassesContainer container)
        {
            this.CurrentClass = currentClass;
            this.Flush();
            this.Errors = new List<SemanticalError>();
            this.Container = container;
        }

        public void Flush()
        {
            this.Deepness = 0;
            this.DeclaredVariables = new Dictionary<int, Dictionary<string, Variable>>();
            this.DeclaredVariables.Add(0, new Dictionary<string, Variable>());
        }
        public void DeclareVariable(Variable variable)
        {
            DeclaredVariables[Deepness].Add(variable.Name, variable);
        }
        public void BlockStart()
        {
            Deepness++;
            DeclaredVariables.Add(Deepness, new Dictionary<string, Variable>());
        }
        public void BlockEnd()
        {
            DeclaredVariables.Remove(Deepness);
            Deepness--;
        }
        public bool IsLocalDeclared(string name)
        {
            return GetLocal(name) != null;
        }
        public Variable GetLocal(string name)
        {
            for (int i = 0; i <= Deepness; i++)
            {
                if (DeclaredVariables[i].ContainsKey(name))
                {
                    return DeclaredVariables[i][name];
                }
            }
            return null;
        }
        public void AddError(string message, ParserRuleContext context)
        {
            this.Errors.Add(new SemanticalError(CurrentClass.File.Filepath, message, context));
        }
        public IEnumerable<SemanticalError> GetErrors()
        {
            return Errors;
        }
    }
}
