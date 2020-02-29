using Nova.Bytecode.Runtime;
using Nova.ByteCode.Codes;
using Nova.ByteCode.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.ByteCode.Runtime
{
    public class RuntimeContext
    {
        private List<object> Stack
        {
            get;
            set;
        }
        private NovFile NovFile
        {
            get;
            set;
        }
        public Stack<ByteMethod> CallStack
        {
            get;
            private set;
        }
        private ByteClass ExecutingClass
        {
            get;
            set;
        }
        public int StackSize
        {
            get
            {
                return Stack.Count;
            }
        }


        public RuntimeContext(NovFile file)
        {
            this.NovFile = file;
            this.Stack = new List<object>();
            this.CallStack = new Stack<ByteMethod>();
        }

        public RuntimeObject CreateObject(string className)
        {
            RuntimeObject obj = new RuntimeObject(NovFile.ByteClasses[className]);
            return obj;
        }

        #region Function Call
        public void Call(RuntimeObject obj, string methodName, int parametersCount)
        {
            var method = obj.Class.Methods[methodName];
            Call(method, parametersCount);
        }
        public void Call(ByteMethod method, int parametersCount)
        {
            this.ExecutingClass = method.ParentClass;

            CallStack.Push(method);

            object[] loc = new object[method.Meta.LocalsCount];

            for (int i = parametersCount - 1; i >= 0; i--)
            {
                loc[i] = PopStack();
            }

            Exec.Execute(this, loc, method.Meta.Results);
            CallStack.Pop();
        }
        public void Call(string className, string methodName, int paramsCount)
        {
            var method = NovFile.ByteClasses[className].Methods[methodName];
            Call(method, paramsCount);
        }
        public void Call(string methodName, int paramsCount)
        {
            var method = CallStack.Peek().ParentClass.Methods[methodName];
            Call(method, paramsCount);
        }
        #endregion

        #region Fields
        public object Get(string className, string fieldName)
        {
            return NovFile.ByteClasses[className].Fields[fieldName].Value;
        }
        public object Get(string fieldName)
        {
            return ExecutingClass.Fields[fieldName].Value;
        }
        public void Set(string className, string fieldName, object value)
        {
            NovFile.ByteClasses[className].Fields[fieldName].Value = value;
        }
        public void Set(string fieldName, object value)
        {
            ExecutingClass.Fields[fieldName].Value = value;
        }
        #endregion

        #region Initializers
        public void Initialize()
        {
            foreach (var @class in this.NovFile.ByteClasses.Values)
            {
                this.ExecutingClass = @class;

                foreach (var field in @class.Fields.Values)
                {
                    field.Initializer(this);
                }
            }
            if (this.Stack.Count > 0)
            {
                throw new Exception("wrong initializer.");
            }
        }
        #endregion

        #region Stack Management
        public object PopStack()
        {
            object value = Stack[Stack.Count - 1];
            this.Stack.RemoveAt(Stack.Count - 1);
            return value;
        }
        public void PushStack(object value)
        {
            this.Stack.Add(value);
        }
        #endregion
    }
}
