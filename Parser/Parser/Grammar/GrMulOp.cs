﻿using Scanner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parser.MyTree;

namespace Parser.Grammar
{
    public class GrMulOp : GrammarRule
    {
        public override void execute(Node node)
        {
            // * or /

            // prepare the expected tokens
            Token mulToken = new Token();
            mulToken.TokenType = Scanner.Scanner.SPECIAL_SYMBOLS.T_MULTIPLY.ToString();
            Token divideToken = new Token();
            divideToken.TokenType = Scanner.Scanner.SPECIAL_SYMBOLS.T_DIVIDE.ToString();

            // try to match * and /
            Boolean mulMatched = MatchInput(mulToken);
            Boolean divideMatched = MatchInput(divideToken);

            //if any matched
            if (mulMatched || divideMatched)
            {
                //edit the node text
                node.Text = Parser.getInstance().GetNextToken().tokenValue;
                // advance the operation 
                Parser.getInstance().AdvanceInput();

            }
        }
    }
}