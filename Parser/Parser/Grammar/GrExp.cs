﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Scanner;
using Parser.MyTree;

namespace Parser.Grammar
{
    class GrExp : GrammarRule
    {
        //exp->sexp[compop sexp]
        string compop1 = Scanner.Scanner.SPECIAL_SYMBOLS.T_LESSTHAN.ToString();
        string compop2 = Scanner.Scanner.SPECIAL_SYMBOLS.T_EQUALTO.ToString();

        Boolean match;
        Token nxt = new Token();
        GrSimpleExp sexp = new GrSimpleExp();

        public override void execute(Node node)
        {


            //MessageBox.Show(Parser.getInstance().GetNextToken().TokenType);
            Controller.getInstance().MatchExpression(node, Parser.getInstance().GetNextToken(), sexp);

            if (Parser.getInstance().GetNextToken().TokenType == compop1)
            {
                node.Text = "<";
                Controller.getInstance().MatchExpression(node, Parser.getInstance().GetNextToken(), new GrComparisonOp());
                Controller.getInstance().MatchExpression(node, Parser.getInstance().GetNextToken(), sexp);
            }
            if(Parser.getInstance().GetNextToken().TokenType == compop2)
            {
                node.Text = "=";
                // MessageBox.Show(Parser.getInstance().GetNextToken().TokenType);
                Controller.getInstance().MatchExpression(node, Parser.getInstance().GetNextToken(), new GrComparisonOp());
                Controller.getInstance().MatchExpression(node, Parser.getInstance().GetNextToken(), sexp);
                // Parser.getInstance().AdvanceInput();
            }

        }
    }
}