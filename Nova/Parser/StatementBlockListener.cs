using Antlr4.Runtime.Misc;
using Nova.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Parser
{
    public class StatementBlockListener : NovaParserBaseListener
    {
        private IStatementBlock Block
        {
            get;
            set;
        }

        public StatementBlockListener(IStatementBlock block)
        {
            this.Block = block;
        }

        public override void EnterStatement([NotNull] NovaParser.StatementContext context)
        {
            base.EnterStatement(context);
        }
        public override void EnterBlock([NotNull] NovaParser.BlockContext context)
        {
            base.EnterBlock(context);
        }
    }
}
