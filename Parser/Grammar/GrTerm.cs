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
    class GrTerm : GrammarRule
    {

        //term-> factor{mulopfactor}
        GrFactor factor = new GrFactor();
        GrMulOp malop = new GrMulOp();
        Token nxt = new Token();
        public override Boolean execute(Node node)
        {
            //Controller.getInstance().MatchExpression(node, Parser.getInstance().GetNextToken(), factor);

            //termA(node, factor);

            Boolean flag=Controller.getInstance().MatchExpression(node, Parser.getInstance().GetNextToken(), factor);
            if (!flag)
            {
                MessageBox.Show("Invalid Term!!");
                return false;
            }


            Token op = Parser.getInstance().GetNextToken();
            string mul = Scanner.Scanner.SPECIAL_SYMBOLS.T_MULTIPLY.ToString();
            string div = Scanner.Scanner.SPECIAL_SYMBOLS.T_DIVIDE.ToString();
            
            Boolean flag1 = true, flag2 = true;
            while ((op.TokenType == mul || op.TokenType == div) && flag1 && flag2)
            {
                node.Text = op.TokenType;
                flag1 = Controller.getInstance().MatchExpression(node, op, malop);
                if (!flag1)
                {
                    MessageBox.Show("Invalid Term!!");

                    return false;
                }
                flag2 = Controller.getInstance().MatchExpression(node, op, factor);
                if (!flag2)
                {
                    MessageBox.Show("Invalid Term!!");

                    return false;
                }
                op = Parser.getInstance().GetNextToken();
                //Parser.getInstance().AdvanceInput();

            }

            //mlhash lazma bas lzoom el ta2keed
            if (!flag1 || !flag2)
            {
                MessageBox.Show("Invalid Term!!");

                return false;
            }
            else
                return true;

        }
        /*
        private void termA(Node node, GrFactor factor)
        {
            Controller.getInstance().MatchExpression(node, Parser.getInstance().GetNextToken(), factor);

            Token mul = new Token();
            mul.TokenType = Scanner.Scanner.SPECIAL_SYMBOLS.T_MULTIPLY.ToString();
            Token div = new Token();
            div.TokenType = Scanner.Scanner.SPECIAL_SYMBOLS.T_DIVIDE.ToString();

            if (MatchInput(mul) || MatchInput(div))
            {
                termA_dash(node, factor);

            }
            
        }


        private void termA_dash(Node node, GrFactor factor)
        {
            Token op = Parser.getInstance().GetNextToken();
            string mul = Scanner.Scanner.SPECIAL_SYMBOLS.T_MULTIPLY.ToString();
            string div = Scanner.Scanner.SPECIAL_SYMBOLS.T_DIVIDE.ToString();

            if (op.TokenType == mul || op.TokenType == div)
            {
                node.Text = op.TokenType;
                Controller.getInstance().MatchExpression(node, op, malop);
                Controller.getInstance().MatchExpression(node, op, factor);
                //op = Parser.getInstance().GetNextToken();

                //Controller.getInstance().MatchExpression(node, Parser.getInstance().GetNextToken(), factor);

                termA(node, factor);

                //op = Parser.getInstance().GetNextToken();

            }


        }
        */

    }
}