using Scanner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parser.MyTree;

namespace Parser.Grammar
{
    public class GrWriteStmt : GrammarRule
    {
        public override void execute(Node node)
        {
            // match the write exp
            Parser.getInstance().AdvanceInput();
            GrExp exp = new GrExp();
            Controller.getInstance().MatchExpression(node, Parser.getInstance().GetNextToken(), new GrExp());

        }
    }
}