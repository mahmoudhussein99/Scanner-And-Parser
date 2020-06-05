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

        public bool childFlag = true;

        public void setChildFlag(bool b)
        {
            childFlag = b;
        }
        public override Boolean execute(Node node)
        {
            // match first statement
            Boolean flag=Controller.getInstance().MatchStatement(node, false);
            if (!flag)
            {
                MessageBox.Show("Error in Statement Sequences!");
                return false;
            }
            //MessageBox.Show(Parser.getInstance().GetNextToken().tokenValue);
            //Parser.getInstance().AdvanceInput();
            //MessageBox.Show(Parser.getInstance().GetNextToken().tokenValue);

            GrStatment statment = new GrStatment();

            //statment.execute(node);
            
            // match semicolon 
            Boolean flagSemiColon=MatchSemiColon();


            // if there's no semi colon just return brooo! we just did our job over here
            if(!flagSemiColon)
            {
                return true;
            }
            else
            {
                // recall until we reach the last stmt which is under the constraints doesn't have semicolon at the end 
                return execute(node);
            }



/*            // loop till there is no more tokens  
            while (Parser.getInstance().GetNextToken().tokenValue != "$")
            {
                if (node.GetLastChild() != null)
                {
                    //MessageBox.Show("ana dakhalt");

                    Controller.getInstance().MatchStatement(node.GetLastChild(), true ,childFlag);
                    MatchSemiColon();

                    // handle the if and repeat end of stmt_seq
                    string endCheck = Parser.getInstance().GetNextToken().tokenValue;
                    //MessageBox.Show(endCheck);
                    if (endCheck == "end" || endCheck == "until")
                    {
                        //Parser.getInstance().AdvanceInput();
                        return;
                    }

                }


            }
            
            //  MessageBox.Show(node.Level.ToString());
            // notify done 
            if (node.Level == 0)
            {
                Controller.getInstance().Done();
            }
*/

        }

        private Boolean MatchSemiColon()
        {
            Token expToken = new Token();
            expToken.TokenType = Scanner.Scanner.SPECIAL_SYMBOLS.T_SEMICOLON.ToString();

            Boolean Matched = MatchInput(expToken);
            //MessageBox.Show(Matched.ToString());
            if (Matched)
            {
                Parser.getInstance().AdvanceInput();
                return true;
            }
            return false;
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