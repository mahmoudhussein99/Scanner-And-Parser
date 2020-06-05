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
    public class GrammarRule
    {

        /// <summary>
        /// handles the logic of matching the expected token to the input(next token)
        /// </summary>
        /// <param name="t"></param>
        /// returns true on valid consumption and returns false otherwise
        public virtual Boolean execute(Node node) {
            return true;
        }
        protected Boolean MatchInput(Token t)
        {
            //MessageBox.Show("hull", Parser.getInstance().GetNextToken().ToString());
            /*if(Parser.getInstance().GetNextToken().ToString() == ")")
            {

                MessageBox.Show("hello2");
            }*/
            
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