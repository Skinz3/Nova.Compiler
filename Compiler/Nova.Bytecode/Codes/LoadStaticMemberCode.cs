﻿using Nova.ByteCode.Codes;
using Nova.ByteCode.Runtime;
using Nova.Utils.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Bytecode.Codes
{
    public class LoadStaticMemberCode : ICode
    {
        public int OpId => 9;

        private int fieldId; // fieldId (symbolTable)

        public LoadStaticMemberCode(int fieldId)
        {
            this.fieldId = fieldId;
        }


        public void Compute(RuntimeContext context, object[] locals, ref int index)
        {
            context.PushStack(context.Get(fieldId));
            index++;
        }
        public override string ToString()
        {
            return "(" + OpId + ") " + "LoadStaticMember " + fieldId;
        }
        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(fieldId);
        }
        public int GetSize()
        {
            return 1;
        }
    }
}
