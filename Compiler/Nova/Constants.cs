using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Nova
{
    public class Constants
    {
        public const string INTERMEDIATE_LANGUAGE_FILE_EXTENSION = ".nov";

        public const string SOURCE_CODE_FILE_EXTENSION = ".nv";

        public const string DEFAULT_OUTPUT_PATH = "output.nov";

        public const string NOV_FILE_HEADER = "NEXEC";

        public const char BRACKET_START_DELIMITER = '{';

        public const char BRACKET_END_DELIMITER = '}';

        public const string MAIN_METHOD_NAME = "Main";

        public static Assembly ASSEMBLY = Assembly.GetExecutingAssembly();
    }
}
