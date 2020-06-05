using Scanner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parser.MyTree;

namespace Parser.Grammar
{
    public class GrAddOp : GrammarRule
    {
        public override void execute(Node node)
        {

            // + or -

            // prepare the expected tokens
            Token plusToken = new Token();
            plusToken.TokenType = Scanner.Scanner.SPECIAL_SYMBOLS.T_PLUS.ToString();
            Token minusToken = new Token();
            minusToken.TokenType = Scanner.Scanner.SPECIAL_SYMBOLS.T_MINUS.ToString();

            // try to match + and -
            Boolean plusMatched = MatchInput(plusToken);
            Boolean minusMatched = MatchInput(minusToken);

            //if any matched
            if (plusMatched || minusMatched)
            {
                //edit the node text
                node.Text = Parser.getInstance().GetNextToken().tokenValue;

                // advance the operation 
                Parser.getInstance().AdvanceInput();

            }
        }
    }
}