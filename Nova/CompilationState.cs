using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova
{
    public enum CompilationState
    {
        Parsing,
        TypeLink,
        SemanticalValidation,
        BytecodeGeneration,
        End,
    }
}
