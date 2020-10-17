using Nova.ByteCode.Generation;
using Nova.IO;
using Nova.Semantics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Members
{
    public interface ISemanticMember
    {
        void GenerateBytecode(ClassesContainer container, ByteBlock context);

        void ValidateSemantics(SemanticsValidator validator);
    }
}
