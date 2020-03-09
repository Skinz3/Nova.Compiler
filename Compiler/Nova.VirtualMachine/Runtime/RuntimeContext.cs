using Nova.VirtualMachine.IO;
using Nova.VirtualMachine.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.VirtualMachine.Runtime
{
    public class RuntimeContext
    {
        public static Null NULL_VALUE = new Null();

        public NovFile NovFile
        {
            get;
            set;
        }
        private List<object> Stack
        {
            get;
            set;
        }
        private List<RuntimeStruct> StructStack
        {
            get;
            set;
        }
        private List<ByteMethod> CallStack
        {
            get;
            set;
        }
        public RuntimeContext(NovFile file)
        {
            this.NovFile = file;
            this.Stack = new List<object>();
            this.StructStack = new List<RuntimeStruct>();
            this.CallStack = new List<ByteMethod>();
        }

        public void Initialize()
        {
            foreach (var byteClass in NovFile.Classes)
            {
                foreach (var field in byteClass.Fields)
                {
                    field.Initializer(this);
                }
            }

        }
        public void CallMain()
        {
            ByteMethod method = this.NovFile.GetMainMethod();
            Call(method);
        }

        public object GetConstant(int index)
        {
            return this.GetExecutingClass().Constants[index];
        }

        private ByteClass GetExecutingClass()
        {
            return CallStack[CallStack.Count - 1].Parent;
        }
        public void Call(int classId, int methodId)
        {
            ByteMethod method = this.NovFile.Classes[classId].Methods[methodId];
            Call(method);
        }


        public void Call(RuntimeStruct st, int methodId)
        {
            StructStack.Add(st);

            ByteMethod method = st.TypeClass.Methods[methodId];
            Call(method);

            StructStack.RemoveAt(StructStack.Count - 1);
        }

        private void Call(ByteMethod method)
        {
            CallStack.Add(method); // we push call stack

            object[] locales = new object[method.ParametersCount];

            for (int i = method.ParametersCount - 1; i >= 0; i--)
            {
                locales[i] = PopStack();
            }

            Exec.Execute(this, locales, method.Block.Instructions);

            CallStack.RemoveAt(CallStack.Count - 1);
        }

        public int GetStackSize()
        {
            return Stack.Count;
        }

        public object PopStack()
        {
            int size = GetStackSize();
            object value = Stack[size - 1];
            Stack.RemoveAt(size - 1);
            return value;
        }
        public void PushStack(object value)
        {
            Stack.Add(value);
        }
    }
}
