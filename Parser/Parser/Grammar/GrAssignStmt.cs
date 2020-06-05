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
    class GrAssignStmt : GrammarRule
    {
        public override void execute(Node node)
        {
            //assign->id := exp
            //Parser.getInstance().AdvanceInput();
            //MessageBox.Show(Parser.getInstance().GetNextToken().tokenValue);
            Token next = Parser.getInstance().GetNextToken();
            Token ex = new Token();
            Token nxt = new Token();
            GrExp exp = new GrExp();
            ex.TokenType = Scanner.Scanner.STATES.T_IDENTIFIER.ToString();


            Boolean match = MatchInput(ex);
           // MessageBox.Show(match.ToString());
            if (match == true)
            {
               // node.Text = node.Text + "        {" + next.tokenValue + ".ID}";
                Parser.getInstance().AdvanceInput();//get current next token 
                
            }

            ex.TokenType = Scanner.Scanner.STATES.T_ASSIGN.ToString();
            match = MatchInput(ex);
           // MessageBox.Show(match.ToString());
            if (match == true)
            {
                Parser.getInstance().AdvanceInput();
                if(Parser.getInstance().GetNextToken().tokenValue == next.tokenValue)
                {
                    node.Text = node.Text;// + "        {" + next.tokenValue + ".ID}";
                    Parser.getInstance().AdvanceInput();
                    Parser.getInstance().AdvanceInput();

                }
            }
            //MessageBox.Show(Parser.getInstance().GetNextToken().TokenType);
            node.Text = node.Text + "        {" + next.tokenValue + ".ID}";
            Controller.getInstance().MatchExpression(node, Parser.getInstance().GetNextToken(), exp);


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