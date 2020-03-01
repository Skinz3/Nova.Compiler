using Nova;
using Nova.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Nova.Statements
{
    public class StatementBuilder
    {
        public static Statement Build(IParentBlock parent, string line, int lineIndex)
        {
            line = line.Trim();

            if (line == string.Empty)
            {
                return new EmptyStatement(parent, lineIndex);
            }

            Match match = null;

            match = Regex.Match(line, ObjectAssignationStatement.REGEX);

            if (match.Success)
                return new ObjectAssignationStatement(parent, line, lineIndex, match);

            match = Regex.Match(line, AssignationStatement.REGEX);

            if (match.Success)
                return new AssignationStatement(parent, line, lineIndex, match);

            match = Regex.Match(line, ConstBoolStatement.REGEX);

            if (match.Success)
                return new ConstBoolStatement(parent, line, lineIndex, match);

            match = Regex.Match(line, ConstInt32Statement.REGEX);

            if (match.Success)
                return new ConstInt32Statement(parent, line, lineIndex, match);

            match = Regex.Match(line, ConstStringStatement.REGEX);

            if (match.Success)
                return new ConstStringStatement(parent, line, lineIndex, match);

            match = Regex.Match(line, ReturnStatement.REGEX);

            if (match.Success)
                return new ReturnStatement(parent, line, lineIndex, match);

            match = Regex.Match(line, ObjectDeclarationStatement.REGEX);

            if (match.Success)
                return new ObjectDeclarationStatement(parent, line, lineIndex, match);

            match = Regex.Match(line, DeclarationStatement.REGEX);

            if (match.Success)
                return new DeclarationStatement(parent, line, lineIndex, match);

            match = Regex.Match(line, WhileStatement.REGEX);

            if (match.Success)
                return new WhileStatement(parent, line, lineIndex, match);

            match = Regex.Match(line, IfStatement.IF_REGEX);

            if (match.Success)
                return new IfStatement(parent, line, lineIndex, match);

            match = Regex.Match(line, MethodCallStatement.REGEX);

            if (match.Success)
                return new MethodCallStatement(parent, line, lineIndex, match);

            match = Regex.Match(line, VariableNameStatement.REGEX);

            if (match.Success)
                return new VariableNameStatement(parent, line, lineIndex);

            match = Regex.Match(line, NativeStatement.REGEX);

            if (match.Success)
                return new NativeStatement(parent, line, lineIndex, match);

            throw new Exception("Unknown Statement"); // SyntaxicValidator.AddError()

            //    return new UnknownStatement(parent, line, lineIndex);
        }
    }
}
