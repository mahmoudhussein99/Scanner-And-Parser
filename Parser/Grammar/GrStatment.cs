
using Scanner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parser.MyTree;
using System.Windows.Forms;

namespace Parser.Grammar
{
    public class GrStatment : GrammarRule
    {
        public override Boolean execute(Node node)
        {
            return Controller.getInstance().MatchStatement(node, false);
            // match semicolon 
            //MatchSemiColon(node);
            /*

            // loop till there is no more tokens  
            while (Parser.getInstance().GetNextToken().tokenValue != "$")
            {
                if (node.GetLastChild() != null)
                {
                    //MessageBox.Show("ana dakhalt");

                    Controller.getInstance().MatchStatement(node.GetLastChild(), true);

                    MatchSemiColon(node);

                    // handle the if and repeat end of stmt_seq
                    string endCheck = Parser.getInstance().GetNextToken().tokenValue;
                    //MessageBox.Show(endCheck);
                    if (endCheck == "end" || endCheck == "until") return;

                }
                

            }*/

        }
        /*
        private void MatchSemiColon(Node node)
        {
            Token expToken = new Token();
            expToken.TokenType = Scanner.Scanner.SPECIAL_SYMBOLS.T_SEMICOLON.ToString();

            Boolean Matched = MatchInput(expToken);
            //MessageBox.Show(Matched.ToString());
            if (Matched)
            {
                Parser.getInstance().AdvanceInput();
            }
            else
            {
               
                if (Parser.getInstance().GetNextToken() != null)
                {
                    MessageBox.Show("no semi-colon");
                }
            }
        }
        */
    }
}