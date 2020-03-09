using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.VirtualMachine
{
    public enum OpCodes
    {
        Add = 1,
        Comparaison = 2,
        CtorCall = 3,
        Div = 4,
        Jump = 5,
        JumpIfFalse = 6,
        Load = 7,
        LoadGlobal = 8,
        MethodCall = 11,
        Mul = 12,
        Printl = 13,
        PushConst = 14,
        PushInt = 15,
        PushNull = 16,
        Readl = 17,
        Return = 18,
        Store = 19,
        StoreGlobal = 21,
        StructCallMethod = 22,
        StructCreate = 23,
        StructLoadMember = 24,
        StructPushCurrent = 25,
        StructStoreMember = 26,
        Sub = 27
    };
}
