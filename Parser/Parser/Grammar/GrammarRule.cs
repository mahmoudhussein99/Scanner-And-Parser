using Scanner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parser.MyTree;

namespace Parser.Grammar
{
    public class GrammarRule
    {

        /// <summary>
        /// handles the logic of matching the expected token to the input(next token)
        /// </summary>
        /// <param name="t"></param>
        public virtual void execute(Node node) { }
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