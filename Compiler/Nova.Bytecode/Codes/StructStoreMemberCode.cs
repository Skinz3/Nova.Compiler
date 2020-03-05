using Nova.ByteCode.Codes;
using Nova.Utils.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Bytecode.Codes
{
    /*
     * this.Age = value
     */
    public class StructStoreMemberCode : ICode
    {
        public int OpId => 26;

        private int propertyId;

        public StructStoreMemberCode(int propertyId)
        {
            this.propertyId = propertyId;
        }

        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(propertyId);
        }

        public override string ToString()
        {
            return "(" + OpId + ") " + "StructSetMember " + propertyId;
        }
        public int GetSize()
        {
            return 1;
        }
    }
}
