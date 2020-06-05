using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Scanner;
using Parser.MyTree;

namespace Parser.Grammar
{
    class GrSimpleExp : GrammarRule
    {


        //SimpleExp->term{addop term}
        Token nxt = new Token();
        GrAddOp addop = new GrAddOp();
        GrTerm Term = new GrTerm();


        public override Boolean execute(Node node)
        {
            
           Boolean flag= Controller.getInstance().MatchExpression(node, Parser.getInstance().GetNextToken(), Term);
            if(!flag)
            {
                MessageBox.Show("Invalid Term in Simple exp!!");

                return false;
            }
            Token op = Parser.getInstance().GetNextToken();
            string opplus = Scanner.Scanner.SPECIAL_SYMBOLS.T_PLUS.ToString();
            string oppminus = Scanner.Scanner.SPECIAL_SYMBOLS.T_MINUS.ToString();
            Boolean flag1 = true, flag2 = true;
            while ((op.TokenType == opplus || op.TokenType == oppminus)&&flag1&&flag2)
            {
                node.Text = op.TokenType;
               flag1= Controller.getInstance().MatchExpression(node, op, addop);
                if (!flag1 )
                {
                    MessageBox.Show("Invalid AddOp in Simple exp!!");

                    return false;
                }
                flag2 = Controller.getInstance().MatchExpression(node, op, Term);
                if ( !flag2)
                {
                    MessageBox.Show("Invalid Term in Simple exp!!");

                    return false;
                }
                op = Parser.getInstance().GetNextToken();
                //Parser.getInstance().AdvanceInput();

            }

            //mlhash lazma bas lzoom el ta2keed
            if (!flag1 || !flag2)
            {
                return false;
            }
            else
                return true;

            //Parser.getInstance().AdvanceInput();
            

            //simple_expA(node);

        }
        /*
        public void simple_expA(Node node)
        {
            Controller.getInstance().MatchExpression(node, Parser.getInstance().GetNextToken(), Term);

            Token opplus = new Token();
            opplus.TokenType = Scanner.Scanner.SPECIAL_SYMBOLS.T_PLUS.ToString();
            Token oppminus = new Token();
            oppminus.TokenType = Scanner.Scanner.SPECIAL_SYMBOLS.T_MINUS.ToString();

            if (MatchInput(opplus) || MatchInput(oppminus))
            {
                simple_expA_dash(node);

            }
        }

        public void simple_expA_dash(Node node)
        {
            Token op = Parser.getInstance().GetNextToken();
            string opplus = Scanner.Scanner.SPECIAL_SYMBOLS.T_PLUS.ToString();
            string oppminus = Scanner.Scanner.SPECIAL_SYMBOLS.T_MINUS.ToString();

            if (op.TokenType == opplus || op.TokenType == oppminus)
            {
                node.Text = op.TokenType;
                Controller.getInstance().MatchExpression(node, op, addop);
                Controller.getInstance().MatchExpression(node, op, Term);
                //op = Parser.getInstance().GetNextToken();

                simple_expA(node);

            }
        
        }*/
    }
}
