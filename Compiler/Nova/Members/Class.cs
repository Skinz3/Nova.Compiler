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
using Nova.Bytecode.Enums;
using Nova.Lexer.Accessors;
using Nova.Utils;

namespace Nova.Members
{
    public class Class : IByteData, IAccessible
    {
        public const string CLASS_PATTERN = @"^\s*(class|struct)\s+(\w+)$";

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
        public ContainerType Type
        {
            get;
            private set;
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

        public Class(NvFile file, string className, ContainerType type, int startIndex, int endIndex)
        {
            this.File = file;
            this.ClassName = className;
            this.StartIndex = startIndex;
            this.EndIndex = endIndex;
            this.Methods = new Dictionary<string, Method>();
            this.Fields = new Dictionary<string, Field>();
            this.Type = type;
        }
        public Class()
        {
            this.Methods = new Dictionary<string, Method>();
            this.Fields = new Dictionary<string, Field>();
        }


        private Method AddMethod(string name, int methodId, ModifiersEnum modifiers, string returnType, string parametersStr, int i)
        {
            if (Methods.ContainsKey(name))
            {
                Logger.Write("Duplicate method \"" + name + "\" line " + (i + 1), LogType.Error);
                return null;
            }
            List<Variable> parameters = Parser.ParseMethodDeclarationParameters(parametersStr);
            int startIndex = Parser.FindNextOpenBracket(this.File.Lines, i);
            int endIndex = Parser.GetBracketCloseIndex(this.File.Brackets, startIndex);

            if (startIndex - i > 1)
            {
                Logger.Write("Method declaration \"" + name + "\" must be followed by his body", LogType.Error);
                return null;
            }

            string endLine = this.File.Lines[endIndex];

            if (endLine.Trim().Any(x => x != Constants.BRACKET_END_DELIMITER))
            {
                Logger.Write("The end of a method body must only contains a bracket.", LogType.Error);
                return null;
            }
            if (startIndex == -1)
            {
                Logger.Write("Invalid method body for member \"" + name + "\" line  " + (i + 1), LogType.Error);
                return null;
            }
            Method method = new Method(this, methodId, name, modifiers, returnType, parameters, startIndex + 1, endIndex);

            if (!method.BuildStatements())
            {
                return null;
            }

            Methods.Add(name, method);
            return method;
        }
        public bool BuildMembers()
        {
            int methodId = 0;

            int fieldId = 0;

            string bracketOpenDel = Constants.BRACKET_START_DELIMITER.ToString();

            for (int i = StartIndex; i < EndIndex; i++)
            {
                string line = File.Lines[i].Trim();

                Match match = Regex.Match(line, Method.METHOD_PATTERN);

                if (match.Success)
                {
                    ModifiersEnum modifiers = (ModifiersEnum)Enum.Parse(typeof(ModifiersEnum), match.Groups[1].Value);

                    string returnType = match.Groups[2].Value;
                    string methodName = match.Groups[3].Value;
                    string parametersStr = match.Groups[4].Value;

                    var method = AddMethod(methodName, methodId, modifiers, returnType, parametersStr, i);

                    if (method == null)
                    {
                        return false;
                    }

                    i = method.EndIndex;

                    methodId++;

                }
                else
                {
                    match = Regex.Match(line, Field.FIELD_PATTERN);

                    if (match.Success)
                    {
                        ModifiersEnum modifiers = (ModifiersEnum)Enum.Parse(typeof(ModifiersEnum), match.Groups[1].Value);

                        string fieldType = match.Groups[2].Value;
                        string fieldName = match.Groups[3].Value;
                        string valueStr = match.Groups[5].Value;

                        if (Fields.ContainsKey(fieldName))
                        {
                            Logger.Write("Duplicate field \"" + fieldName + "\" line " + (i + 1), LogType.Error);
                            return false;
                        }
                        Field field = new Field(this, fieldId, modifiers, new Variable(fieldName, fieldType), valueStr, i);

                        if (!field.Build())
                        {
                            return false;
                        }

                        Fields.Add(field.Name, field);
                        fieldId++;
                    }
                    else
                    {
                        match = Regex.Match(line, Method.CTOR_PATTERN);

                        if (match.Success)
                        {
                            if (this.Type == ContainerType.@class)
                            {
                                Logger.Write("Classe \"" + this.ClassName + "\" cant have constructor. Define a struct instead.", LogType.Error);
                                return false;
                            }
                            ModifiersEnum modifiers = ModifiersEnum.ctor;
                            string returnType = string.Empty;
                            string methodName = match.Groups[1].Value;
                            string parametersStr = match.Groups[2].Value;

                            if (methodName != this.ClassName)
                            {
                                Logger.Write("Invalid constructor \"" + methodName + "\" in \"" + this.ClassName + "\". A constructor must have the same name as its class", LogType.Error);
                                return false;
                            }

                            Method method = AddMethod(methodName, methodId, modifiers, returnType, parametersStr, i);
                            if (method == null)
                            {
                                return false;
                            }

                            i = method.EndIndex;
                            methodId++;
                        }
                        else
                        {
                            if (line != string.Empty && line != bracketOpenDel)
                            {
                                Logger.Write("line ignored " + line, LogType.Warning);
                            }
                        }
                    }
                }
            }
            return true;
        }

        public override string ToString()
        {
            return ClassName;
        }
        public Method GetCtor()
        {
            return Methods.Values.FirstOrDefault(x => x.Modifiers == ModifiersEnum.ctor);
        }

        public IByteElement GetByteElement(ClassesContainer container, IByteElement parent)
        {
            ByteClass byteClass = new ByteClass(this.ClassName);

            foreach (var method in this.Methods)
            {
                byteClass.Methods.Add((ByteMethod)method.Value.GetByteElement(container, byteClass));
            }


            foreach (var field in this.Fields)
            {
                byteClass.Fields.Add((ByteField)field.Value.GetByteElement(container, byteClass));
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
