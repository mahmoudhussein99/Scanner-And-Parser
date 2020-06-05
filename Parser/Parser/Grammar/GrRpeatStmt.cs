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
    class GrRepeatStmt : GrammarRule
    {


        //repeat->repeat stmtseq until exp

        Token ex = new Token();
        GrExp exp = new GrExp();
        Boolean match;
        public override void execute(Node node)
        {
            Parser.getInstance().AdvanceInput();
            //MessageBox.Show(Parser.getInstance().GetNextToken().TokenType);
            Controller.getInstance().MatchStatmentSequence(node);
            //Parser.getInstance().AdvanceInput();
           // MessageBox.Show(Parser.getInstance().GetNextToken().TokenType);
            ex.TokenType = Scanner.Scanner.RESERVED_WORDS.T_UNTIL.ToString();
            match = MatchInput(ex);
            //MessageBox.Show(match.ToString());
            if (match == true)
            {
                Parser.getInstance().AdvanceInput();
            }
            // el mafrod error 

            Controller.getInstance().MatchExpression(node, Parser.getInstance().GetNextToken(), exp);

        }
        protected Boolean MatchInput(Token t)
        {
            if (Parser.getInstance().GetNextToken().TokenType == t.TokenType)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}