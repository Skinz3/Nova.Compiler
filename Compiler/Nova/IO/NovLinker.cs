using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.IO
{
    class NovLinker
    {
        public ClassesContainer CurrentClasses
        {
            get;
            set;
        }
        public ClassesContainer AllClasses
        {
            get;
            set;
        }

        public NovLinker(IEnumerable<NvFile> files)
        {

        }


    }
}
