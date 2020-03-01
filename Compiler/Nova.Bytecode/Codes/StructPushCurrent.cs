﻿using Nova.Bytecode.Runtime;
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
    /*
     * human.Name = "value"
     */
    public class StructPushCurrent : ICode
    {
        public int TypeId => 20;

        public StructPushCurrent()
        {
        }
        public void Compute(RuntimeContext context,ref object[] locals, ref int index)
        {
            context.PushStack(context.StructsStack.Peek());
            index++;
        }

        public void Serialize(CppBinaryWriter writer)
        {
        }
        public override string ToString()
        {
            return "(" + TypeId + ") " + "StructPushCurrent";
        }
    }
}