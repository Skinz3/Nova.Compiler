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
        public Stack<RuntimeStruct> StructsStack
        {
            get;
            private set;
        }

        public object GetConstant(int constantId)
        {
            return ExecutingClass.GetConstant(constantId);
        }

        private ByteClass ExecutingClass
        {
            get
            {
                return CallStack.Peek().ParentClass;
            }
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
            this.StructsStack = new Stack<RuntimeStruct>();
        }

        public RuntimeStruct CreateObject(string className)
        {
            RuntimeStruct obj = new RuntimeStruct(NovFile.ByteClasses[className]);
            return obj;
        }

        #region Function Call
        public void Call(RuntimeStruct obj, int methodId, int parametersCount)
        {
            this.StructsStack.Push(obj);
            var method = obj.Class.Methods[methodId];
            Call(method, parametersCount);

            this.StructsStack.Pop();
        }
        public void Call(ByteMethod method, int parametersCount)
        {
            CallStack.Push(method);

            object[] loc = new object[method.Meta.LocalsCount];

            for (int i = parametersCount - 1; i >= 0; i--)
            {
                loc[i] = PopStack();
            }

            Exec.Execute(this, loc, method.Meta.Results);
            CallStack.Pop();
        }
        public void Call(string className, int methodId, int paramsCount)
        {
            var method = NovFile.ByteClasses[className].Methods[methodId];
            Call(method, paramsCount);
        }
        public void Call(int methodId, int paramsCount)
        {
            var method = CallStack.Peek().ParentClass.Methods[methodId];
            Call(method, paramsCount);
        }
        #endregion

        #region Fields
        public object Get(string className, int fieldId)
        {
            return NovFile.ByteClasses[className].Fields[fieldId].Value;
        }
        public object Get(int fieldName)
        {
            return ExecutingClass.Fields[fieldName].Value;
        }
        public void Set(string className, int fieldId, object value)
        {
            NovFile.ByteClasses[className].Fields[fieldId].Value = value;
        }
        public void Set(int fieldId, object value)
        {
            ExecutingClass.Fields[fieldId].Value = value;
        }
        #endregion

        #region Initializers
        public void Initialize()
        {
            foreach (var @class in this.NovFile.ByteClasses.Values)
            {
                foreach (var field in @class.Fields)
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

        public object PeekStack()
        {
            return Stack[Stack.Count - 1];
        }

        public void PushStack(object value)
        {
            this.Stack.Add(value);
        }
        public object StackMinus(int minus)
        {
            return this.Stack[Stack.Count - 1 - minus];
        }
        #endregion
    }
}
