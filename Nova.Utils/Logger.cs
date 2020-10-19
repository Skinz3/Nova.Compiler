using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Utils
{
    public enum LogType
    {
        None = -1,
        Log = 0,
        Debug = 1,
        SemanticError = 2,
        SyntaxicError = 3,
        Error = 5,
        LogImportant = 6,
        Success = 7,
        Warning = 8,
        ProgramOutput = 9,
        Color1,
        Color2,
    }
    public class Logger
    {
        private const ConsoleColor COLOR_1 = ConsoleColor.Magenta;
        private const ConsoleColor COLOR_2 = ConsoleColor.DarkMagenta;

        private static LogType[] NoPrefix = new LogType[]
        {
            LogType.None,
            LogType.Color1,
            LogType.Color2,
        };
        private static Dictionary<LogType, ConsoleColor> Colors = new Dictionary<LogType, ConsoleColor>()
        {
            { LogType.None,ConsoleColor.Gray },
            { LogType.Log,           ConsoleColor.Gray },
            { LogType.Debug,         ConsoleColor.DarkGray },
            { LogType.LogImportant,  ConsoleColor.White },

            { LogType.Error,         ConsoleColor.Red },
            { LogType.SemanticError, ConsoleColor.Red },
            { LogType.SyntaxicError, ConsoleColor.Red },

            { LogType.Warning,       ConsoleColor.Yellow},

            { LogType.Success,       ConsoleColor.Green },
            { LogType.ProgramOutput, ConsoleColor.Magenta },

            { LogType.Color1,        ConsoleColor.DarkMagenta },
            { LogType.Color2,        ConsoleColor.DarkMagenta }
        };

        public static void Write(object value, LogType state = LogType.None)
        {
            if (!NoPrefix.Contains(state))
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(state + " > ");
            }

            WriteColored(value, Colors[state]);
        }
        private static void WriteColored(object value, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(value);
        }
        public static void NewLine()
        {
            Console.WriteLine(Environment.NewLine);
        }
    }
}
