using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Nova.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Parser.Errors
{
    public class NovaParsingErrorHandler : IAntlrErrorListener<IToken>
    {
        public int ErrorsCount
        {
            get;
            set;
        }
        public void SyntaxError([NotNull] IRecognizer recognizer, [Nullable] IToken offendingSymbol, int line, int charPositionInLine, [NotNull] string msg, [Nullable] RecognitionException e)
        {
            Logger.Write(line + ":" + charPositionInLine + " " + msg, LogType.SyntaxicError);
            ErrorsCount++;
        }
    }
}
