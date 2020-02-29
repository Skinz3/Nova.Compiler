using Nova.Bytecode.Runtime;
using Nova.ByteCode.Codes;
using Nova.ByteCode.Runtime;
using Nova.Utils.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Bytecode.Codes
{
    public class StructLocalGetCode : ICode
    {
        public int TypeId => 18;

        public int variableId;

        public string propertyName;

        public StructLocalGetCode(int variableId,string propertyName)
        {
            this.variableId = variableId;
            this.propertyName = propertyName;
        }
        public void Compute(RuntimeContext context, ref object[] locals, ref int index)
        {
            RuntimeStruct obj = (RuntimeStruct)locals[variableId];
            context.PushStack(obj.Get(propertyName));
            index++;
        }
        public override string ToString()
        {
            return "(" + TypeId + ") " + "ObjectLoadCode " + variableId + " " + variableId;
        }
        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(variableId);
            writer.Write(propertyName);
        }
    }
}
