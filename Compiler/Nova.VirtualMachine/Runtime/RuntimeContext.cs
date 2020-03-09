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
        public List<MethodCall> CallStack
        {
            get;
            set;
        }
        public RuntimeContext(NovFile file)
        {
            this.NovFile = file;
            this.Stack = new List<object>();
            this.StructStack = new List<RuntimeStruct>();
            this.CallStack = new List<MethodCall>();
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

        public object GetConstant(int index)
        {
            return this.GetExecutingClass().Constants[index];
        }

        private ByteClass GetExecutingClass()
        {
            return CallStack[CallStack.Count - 1].Method.Parent;
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
