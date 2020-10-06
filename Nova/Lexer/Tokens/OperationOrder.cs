using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Lexer.Tokens
{
    class OperationOrder
    {
        public static int FindLowestPart(Token[] components, int start, int count)
        {
            int idx;

            // Try Binary
            if ((idx = _FindLowestBinary(components, start, count)) != -1)
                return idx;

            // First Is Unary
            if (components[start].Type == TokenType.OperatorBinary)
                return start;

            /* if (components[start + count - 1].Type == ComponentType.BackUnary)
                 return start + count - 1; */

            // If Single Then Return 0
            if (count == 1)
                return start;

            // Fail :(
            return -1;
        }
        public static int _FindLowestBinary(Token[] components, int start, int count)
        {
            foreach (string op in Tokenizer.SortedOperators)
            {
                int idx = LastIndex(components, TokenType.OperatorBinary, op, start, count);
                if (idx != -1) return idx;
            }

            return -1;
        }
        public static int LastIndex(Token[] components, TokenType Type, string Text, int start, int count)
        {
            int i = (start + count - 1);
            while (i >= start)
            {
                var item = components[i];
                if (item.Type == Type && (Text == null || item.Raw == Text))
                    return i;
                i--;
            }
            return -1;
        }
    }
}
