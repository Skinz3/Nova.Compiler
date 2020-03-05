using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nova.ByteCode.Enums;
using Nova.Utils.IO;

namespace Nova.ByteCode.Codes
{
    public class ComparaisonCode : ICode
    {
        private OperatorsEnum type;

        public int OpId => 2;

        public ComparaisonCode(OperatorsEnum type)
        {
            this.type = type;
        }

        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write((int)type);
        }

        public override string ToString()
        {
            return "(" + OpId + ") " + "Comparaison" + type;
        }

        public int GetSize()
        {
            return 1;
        }
    }
}
