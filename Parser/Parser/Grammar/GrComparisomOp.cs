using Scanner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parser.MyTree;

namespace Parser.Grammar
{
    public class GrComparisonOp : GrammarRule
    {
        public override void execute(Node node)
        {
            // < or =

            // prepare the expected tokens
            Token lessThanToken = new Token();
            lessThanToken.TokenType = Scanner.Scanner.SPECIAL_SYMBOLS.T_LESSTHAN.ToString();
            Token equalToken = new Token();
            equalToken.TokenType = Scanner.Scanner.SPECIAL_SYMBOLS.T_EQUALTO.ToString();

            // try to match < and =
            Boolean lessThanMatched = MatchInput(lessThanToken);
            Boolean equalMatched = MatchInput(equalToken);

            //if any matched
            if (lessThanMatched || equalMatched)
            {
                //edit the node text
                node.Text = Parser.getInstance().GetNextToken().tokenValue;
                // advance the comparison 
                Parser.getInstance().AdvanceInput();

            }

        }
    }
}