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
        public override Boolean execute(Node node)
        {
            Parser.getInstance().AdvanceInput();
            //MessageBox.Show(Parser.getInstance().GetNextToken().TokenType);
            Boolean flag=Controller.getInstance().MatchStatmentSequence(node);
            if (!flag)
            {
                MessageBox.Show("Invalid Statements sequence in Repeat-Until");
                return false;
            }
            //Parser.getInstance().AdvanceInput();
           // MessageBox.Show(Parser.getInstance().GetNextToken().TokenType);
            ex.TokenType = Scanner.Scanner.RESERVED_WORDS.T_UNTIL.ToString();
            match = MatchInput(ex);
            //MessageBox.Show(match.ToString());
            if (match == true)
            {
                Parser.getInstance().AdvanceInput();
            }
            else
            {
                MessageBox.Show("Until Not Found in Repeat-Until!");
                return false;

            }
            // el mafrod error 

            Boolean flagExp=Controller.getInstance().MatchExpression(node, Parser.getInstance().GetNextToken(), exp);
            if (!flagExp)
            {
                MessageBox.Show("Error in Exp in Repeat-Until!");
                return false;

            }
            else
            {
                return true;
            }
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