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
    public class GrReadStmt : GrammarRule
    {
        public override void execute(Node node)
        {
            Parser.getInstance().AdvanceInput();
            // match the read identifier
            Token expectedToken = new Token();

            expectedToken.TokenType = Scanner.Scanner.STATES.T_IDENTIFIER.ToString();
           // MessageBox.Show(Parser.getInstance().GetNextToken().tokenValue);
            
            Boolean matched = MatchInput(expectedToken);
            //MessageBox.Show(matched.ToString());
            
            if (matched)
            {
                // modify the read node text to add the identifier name
                node.Text = node.Text + "\n{" + Parser.getInstance().GetNextToken().tokenValue + ".ID}";
               
                // advance the identifier
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