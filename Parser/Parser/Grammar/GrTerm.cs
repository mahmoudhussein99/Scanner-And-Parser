using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Scanner;
using Parser.MyTree;

namespace Parser.Grammar
{
    class GrTerm : GrammarRule
    {

        //term-> factor{mulopfactor}
        GrFactor factor = new GrFactor();
        GrMulOp malop = new GrMulOp();
        Token nxt = new Token();
        public override void execute(Node node)
        {
            Controller.getInstance().MatchExpression(node, Parser.getInstance().GetNextToken(), factor);
            Token op = Parser.getInstance().GetNextToken();
            string mul = Scanner.Scanner.SPECIAL_SYMBOLS.T_MULTIPLY.ToString();
            string div = Scanner.Scanner.SPECIAL_SYMBOLS.T_DIVIDE.ToString();
            while (op.TokenType == mul || op.TokenType == div)
            {
                node.Text = op.TokenType;
                Controller.getInstance().MatchExpression(node, op, malop);
                Controller.getInstance().MatchExpression(node, op, factor);
                op = Parser.getInstance().GetNextToken();

            }



        }
    }
}