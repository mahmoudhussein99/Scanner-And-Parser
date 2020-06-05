using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Parser.MyTree;
using Scanner;

namespace Parser.Grammar
{
    public class GrStmtSequence : GrammarRule
    {
        public override void execute(Node node)
        {
            // match first statement
            Controller.getInstance().MatchStatement(node, false);
            //MessageBox.Show(Parser.getInstance().GetNextToken().tokenValue);
            //Parser.getInstance().AdvanceInput();
            //MessageBox.Show(Parser.getInstance().GetNextToken().tokenValue);
            // match semicolon 
            MatchSemiColon();
            // loop till there is no more tokens  
            while (Parser.getInstance().GetNextToken().tokenValue != "$")
            {
                if (node.GetLastChild() != null)
                {
                    //MessageBox.Show("ana dakhalt");

                    Controller.getInstance().MatchStatement(node.GetLastChild(), true);
                    MatchSemiColon();

                    // handle the if and repeat end of stmt_seq
                    string endCheck = Parser.getInstance().GetNextToken().tokenValue;
                    //MessageBox.Show(endCheck);
                    if (endCheck == "end" || endCheck == "until") return;

                }

            }
          //  MessageBox.Show(node.Level.ToString());
            // notify done 
            if (node.Level == 0)
            {
                Controller.getInstance().Done();
            }


        }

        private void MatchSemiColon()
        {
            Token expToken = new Token();
            expToken.TokenType = Scanner.Scanner.SPECIAL_SYMBOLS.T_SEMICOLON.ToString();

            Boolean Matched = MatchInput(expToken);
            //MessageBox.Show(Matched.ToString());
            if (Matched)
            {
                Parser.getInstance().AdvanceInput();
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