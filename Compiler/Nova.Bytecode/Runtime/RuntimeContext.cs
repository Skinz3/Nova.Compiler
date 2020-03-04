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

        public RuntimeStruct CreateObject(int classId)
        {
            RuntimeStruct obj = new RuntimeStruct(NovFile.ByteClasses[classId]);
            return obj;
        }

        #region Function Call
        public void Call(RuntimeStruct obj, int methodId)
        {
            this.StructsStack.Push(obj);
            var method = obj.Class.Methods[methodId];
            Call(method);

            this.StructsStack.Pop();
        }
        public void Call(ByteMethod method)
        {
            CallStack.Push(method);

            object[] loc = new object[method.Meta.LocalsCount];

            for (int i = method.ParametersCount - 1; i >= 0; i--)
            {
                loc[i] = PopStack();
            }

            Exec.Execute(this, loc, method.Meta.Results);
            CallStack.Pop();
        }
        public void MainEntryPoint()
        {
            var method = NovFile.GetMainEntryPoint();
            Call(method);
        }
        public void Call(int classId, int methodId)
        {
            var method = NovFile.ByteClasses[classId].Methods[methodId];
            Call(method);
        }
        public void Call(int methodId)
        {
            var method = CallStack.Peek().ParentClass.Methods[methodId];
            Call(method);
        }
        #endregion

        #region Fields
        public object Get(int classId, int fieldId)
        {
            return NovFile.ByteClasses[classId].Fields[fieldId].Value;
        }
        public object Get(int fieldName)
        {
            return ExecutingClass.Fields[fieldName].Value;
        }
        public void Set(int classId, int fieldId, object value)
        {
            NovFile.ByteClasses[classId].Fields[fieldId].Value = value;
        }
        public void Set(int fieldId, object value)
        {
            ExecutingClass.Fields[fieldId].Value = value;
        }
        #endregion

        #region Initializers
        public void Initialize()
        {
            foreach (var @class in this.NovFile.ByteClasses)
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
