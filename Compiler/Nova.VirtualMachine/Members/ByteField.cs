using Nova.Utils;
using Nova.Utils.IO;
using Nova.VirtualMachine.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.VirtualMachine.Members
{
    public class ByteField
    {
        public string Name
        {
            get;
            private set;
        }
        public Modifiers Modifiers
        {
            get;
            private set;
        }
        public ByteBlock ValueBlock
        {
            get;
            set;
        }
        public object Value
        {
            get;
            set;
        }
        public ByteField()
        {
            ValueBlock = new ByteBlock();
        }
        public void Deserialize(CppBinaryReader reader)
        {
            this.Name = reader.ReadString();
            this.Modifiers = (Modifiers)reader.ReadByte();

            ValueBlock.Deserialize(reader);
        }

        public void Initializer(RuntimeContext context)
        {
            Exec.Execute(context, null, this.ValueBlock.Instructions);

            if (context.GetStackSize() == 1)
            {
                this.Value = context.PopStack();
            }
            else
            {
                Logger.Write("Unable to initialize field " + Name + " invalid value.", LogType.Error);
            }
        }
    }
}
