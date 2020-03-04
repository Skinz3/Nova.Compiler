using Nova.Bytecode.Enums;
using Nova.ByteCode.Enums;
using Nova.Members;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Nova.IO
{
    public class NvFile
    {
        const string USING_PATTERN = "^using \"([a-zA-Z_$][a-zA-Z_$0-9_.\\/]*)\"$";

        const string COMMENT = "//";

        public string Filepath
        {
            get;
            private set;
        }
        public string[] Lines
        {
            get;
            private set;
        }
        public List<string> Usings
        {
            get;
            private set;
        }
        public Dictionary<int, int> Brackets
        {
            get;
            private set;
        }
        private List<Class> Classes
        {
            get;
            set;
        }
        public NvFile(string filePath)
        {
            this.Filepath = filePath;
            this.Usings = new List<string>();
            this.Brackets = new Dictionary<int, int>();
            this.Classes = new List<Class>();
        }

        public bool Read()
        {
            if (!ReadLines())
            {
                Logger.Write("Cannot open file " + Filepath, LogType.Error);
                return false;
            }

            this.RemoveComments();


            if (!ReadBrackets())
            {
                return false;
            }

            return true;
        }
        private void RemoveComments()
        {
            for (int i = 0; i < Lines.Length; i++)
            {
                int index = Lines[i].IndexOf(COMMENT);

                if (index != -1)
                {
                    string line = Lines[i];

                    Lines[i] = line.Substring(0, index);

                    Console.WriteLine(Lines[i]);
                }
            }
        }
        private bool ReadBrackets()
        {
            int currentIndent = 0;

            for (int i = 0; i < this.Lines.Length; i++)
            {
                string line = Lines[i];

                int count = line.Count(x => x == Constants.BRACKET_START_DELIMITER);

                if (count > 0)
                {
                    currentIndent += count;
                    this.Brackets.Add(i, currentIndent);
                }

                count = line.Count(x => x == Constants.BRACKET_END_DELIMITER);

                if (count > 0)
                {
                    currentIndent -= count;
                    this.Brackets.Add(i, currentIndent);
                }
            }

            if (Brackets.Count != 0)
            {
                int lastIndentLevel = Brackets.Last().Value;

                if (lastIndentLevel != 0)
                {
                    Logger.Write("Invalid file brackets. (Last bracket indent level" + lastIndentLevel + ")", LogType.SyntaxicError);
                    return false;
                }
            }
            return true;
        }
        private bool ReadLines()
        {
            try
            {
                this.Lines = File.ReadAllLines(this.Filepath, Encoding.UTF8).Where(x => !string.IsNullOrEmpty(x)).ToArray();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool ReadClasses()
        {
            for (int i = 0; i < Lines.Length; i++)
            {
                Match match = Regex.Match(Lines[i], Class.CLASS_PATTERN);

                if (match.Success)
                {
                    string className = match.Groups[2].Value;

                    ContainerType type = (ContainerType)Enum.Parse(typeof(ContainerType), match.Groups[1].Value);

                    int classStartLine = Parser.FindNextOpenBracket(Lines, i);
                    int classEndLine = Parser.GetBracketCloseIndex(Brackets, classStartLine);

                    i = classEndLine;

                    Class novaClass = new Class(this, className, type, classStartLine, classEndLine);

                    if (!novaClass.BuildMembers())
                    {
                        return false;
                    }

                    this.Classes.Add(novaClass);
                }
                else
                {
                    match = Regex.Match(Lines[i].Trim(), USING_PATTERN);

                    if (match.Success)
                    {
                        Usings.Add(match.Groups[1].Value);
                    }
                    else
                    {
                        Logger.Write("line ignored :" + Lines[i], LogType.Warning);
                    }
                }

            }

            if (Classes.Count == 0)
            {
                Logger.Write("Invalid file (" + this.Filepath + ") no classes found.", LogType.Error);
                return false;
            }



            return true;
        }
        public IEnumerable<Class> GetClasses()
        {
            return Classes;
        }
    }
}
