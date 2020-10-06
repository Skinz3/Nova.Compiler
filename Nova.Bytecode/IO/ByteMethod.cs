using Nova.ByteCode.Codes;
using Nova.ByteCode.Enums;
using Nova.ByteCode.Generation;
using Nova.Utils;
using Nova.Utils.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.ByteCode.IO
{
    public class ByteMethod : IByteElement
    {
        public string Name
        {
            get;
            set;
        }
        public ByteBlock Meta
        {
            get;
            set;
        }
        public ByteClass ParentClass
        {
            get;
            set;
        }
        public ModifiersEnum Modifiers
        {
            get;
            set;
        }
        public int ParametersCount
        {
            get;
            set;
        }
        public ByteMethod(string name, ModifiersEnum modifiers, int parametersCount, ByteClass parentClass)
        {
            this.Name = name;
            this.Modifiers = modifiers;
            this.Meta = new ByteBlock(parentClass);
            this.ParentClass = parentClass;
            this.ParametersCount = parametersCount;
        }

        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(Name);
            writer.Write((byte)Modifiers);
            writer.Write(ParametersCount);
            Meta.Serialize(writer);
        }

        public bool IsMainPointEntry()
        {
            return Modifiers == ModifiersEnum.@public && Name == Constants.MAIN_METHOD_NAME && ParametersCount == 0;
        }

        
    }
}
