using Nova.IO;
using Nova.Lexer;
using Nova.Lexer.Tokens;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Nova.ByteCode.IO;
using Nova.Semantics;
using Nova.ByteCode.Enums;

namespace Nova.Members
{
    public class Class : IByteData
    {
        public const string CLASS_PATTERN = @"^\s*class\s+(\w+)$";

        public string ClassName
        {
            get;
            private set;
        }
        public NvFile File
        {
            get;
            private set;
        }
        public List<string> Usings
        {
            get;
            private set;
        }
        private int StartIndex
        {
            get;
            set;
        }
        private int EndIndex
        {
            get;
            set;
        }
        private ModifiersEnum Modifiers
        {
            get;
            set;
        }
        public Dictionary<string, Method> Methods
        {
            get;
            private set;
        }
        public Dictionary<string, Field> Fields
        {
            get;
            private set;
        }

        public Class(NvFile file, string className, ModifiersEnum modifiers, int startIndex, int endIndex)
        {
            this.File = file;
            this.ClassName = className;
            this.StartIndex = startIndex;
            this.EndIndex = endIndex;
            this.Methods = new Dictionary<string, Method>();
            this.Fields = new Dictionary<string, Field>();
            this.Usings = file.Usings;
            this.Modifiers = modifiers;
        }
        public Class()
        {
            this.Methods = new Dictionary<string, Method>();
            this.Fields = new Dictionary<string, Field>();
            this.Usings = new List<string>();
        }



        public bool BuildMembers()
        {
            for (int i = StartIndex; i < EndIndex; i++)
            {
                string line = File.Lines[i].Trim();

                Match methodMatch = Regex.Match(line, Method.METHOD_PATTERN);

                if (methodMatch.Success)
                {
                    ModifiersEnum modifiers = (ModifiersEnum)Enum.Parse(typeof(ModifiersEnum), methodMatch.Groups[1].Value);

                    if (methodMatch.Groups[2].Value == "static")
                    {
                        modifiers |= ModifiersEnum.@static;
                    }

                    string returnType = methodMatch.Groups[3].Value;
                    string methodName = methodMatch.Groups[4].Value;
                    string parametersStr = methodMatch.Groups[5].Value;

                    if (Methods.ContainsKey(methodName))
                    {
                        Logger.Write("Duplicate method \"" + methodName + "\" line " + i, LogType.Error);
                    }
                    List<Variable> parameters = Parser.ParseMethodDeclarationParameters(parametersStr);
                    int startIndex = Parser.FindNextOpenBracket(this.File.Lines, i);
                    int endIndex = Parser.GetBracketCloseIndex(this.File.Brackets, startIndex);

                    Method method = new Method(this, methodName, modifiers, returnType, parameters, startIndex + 1, endIndex);

                    if (!method.BuildStatements())
                    {
                        return false;
                    }

                    Methods.Add(methodName, method);
                }
                else
                {
                    Match fieldMatch = Regex.Match(line, Field.FIELD_PATTERN);

                    if (fieldMatch.Success)
                    {
                        ModifiersEnum modifiers = (ModifiersEnum)Enum.Parse(typeof(ModifiersEnum), fieldMatch.Groups[1].Value);

                        if (fieldMatch.Groups[2].Value == "static")
                        {
                            modifiers |= ModifiersEnum.@static;
                        }

                        string fieldType = fieldMatch.Groups[3].Value;
                        string fieldName = fieldMatch.Groups[4].Value;
                        string valueStr = fieldMatch.Groups[6].Value;

                        if (Fields.ContainsKey(fieldName))
                        {
                            Logger.Write("Duplicate field \"" + fieldName + "\" line " + i, LogType.Error);
                            return false;
                        }
                        Field field = new Field(this, modifiers, new Variable(fieldName, fieldType), valueStr, i);

                        if (!field.Build())
                        {
                            return false;
                        }

                        Fields.Add(field.Name, field);
                    }
                }
            }
            return true;
        }

        public bool HasMain()
        {
            if (!Methods.ContainsKey(Constants.MAIN_METHOD_NAME))
            {
                return false;
            }
            else
            {
                Method method = Methods[Constants.MAIN_METHOD_NAME];
                return method.Modifiers == (ModifiersEnum.@public | ModifiersEnum.@static) && method.Parameters.Count == 0;
            }
        }

        public override string ToString()
        {
            return ClassName;
        }

        public IByteElement GetByteElement(IByteElement parent)
        {
            ByteClass byteClass = new ByteClass(this.ClassName);

            foreach (var method in this.Methods)
            {
                byteClass.Methods.Add(method.Key, (ByteMethod)method.Value.GetByteElement(byteClass));
            }

            foreach (var field in this.Fields)
            {
                byteClass.Fields.Add(field.Key, (ByteField)field.Value.GetByteElement(byteClass));
            }


            return byteClass;

        }

        public IEnumerable<SemanticalError> ValidateSemantics(ClassesContainer container)
        {
            List<SemanticalError> errors = new List<SemanticalError>();

            foreach (var field in this.Fields)
            {
                errors.AddRange(field.Value.ValidateSemantics(container));
            }
            foreach (var method in this.Methods)
            {
                errors.AddRange(method.Value.ValidateSemantics(container));
            }

            return errors;
        }
    }
}
