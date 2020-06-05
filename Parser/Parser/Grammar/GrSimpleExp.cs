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
    class GrSimpleExp : GrammarRule
    {


        //SimpleExp->term{addop term}
        Token nxt = new Token();
        GrAddOp addop = new GrAddOp();
        GrTerm Term = new GrTerm();


        public override void execute(Node node)
        {

            Controller.getInstance().MatchExpression(node, Parser.getInstance().GetNextToken(), Term);
            Token op = Parser.getInstance().GetNextToken();
            string opplus = Scanner.Scanner.SPECIAL_SYMBOLS.T_PLUS.ToString();
            string oppminus = Scanner.Scanner.SPECIAL_SYMBOLS.T_MINUS.ToString();
            while (op.TokenType == opplus || op.TokenType == oppminus)
            {
                node.Text = op.TokenType;
                Controller.getInstance().MatchExpression(node, op, addop);
                Controller.getInstance().MatchExpression(node, op, Term);
                op = Parser.getInstance().GetNextToken();

            }

        }
    }
}
