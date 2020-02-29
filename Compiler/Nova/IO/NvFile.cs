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
        const string USING_PATTERN = "^using (\\w.+)$";

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

            foreach (SearchResult res in Parser.Search(Lines, USING_PATTERN, 1))
            {
                this.Usings.Add(res.Value);
            }


            if (!ReadBrackets())
            {
                return false;
            }

            return true;
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
                this.Lines = File.ReadAllLines(this.Filepath);
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

                    Class novaClass = new Class(this, className, type, classStartLine, classEndLine);

                    if (!novaClass.BuildMembers())
                    {
                        return false;
                    }

                    this.Classes.Add(novaClass);
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
