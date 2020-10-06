using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nova.Members;
using Nova.Statements;

namespace Nova.Lexer.Tokens
{
    public class VectorCreateToken : Token
    {
        public override TokenType Type
        {
            get => TokenType.VectorCreate;
            set => throw new InvalidOperationException();
        }
        public VectorCreateToken(string raw) : base(raw)
        {

        }

        public override Statement GetStatement(IParentBlock parent, int lineIndex)
        {
            return new VectorCreationStatement(parent, this.Raw, lineIndex);
        }

        public static VectorCreateToken Parse(string input, ref int index)
        {
            string raw = string.Empty;

            index++;

            while (index < input.Length && input[index] != Tokenizer.VECTOR_CLOSE[0])
            {
                raw += input[index];
                index++;
            }

            if (input[index] != Tokenizer.VECTOR_CLOSE[0]) // [1,2,3  where is ']' ?
            {
                throw new Exception();
            }

            index++;

            return new VectorCreateToken(raw);
        }
    }
}
