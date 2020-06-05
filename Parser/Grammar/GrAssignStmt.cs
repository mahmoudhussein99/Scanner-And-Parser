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
        public override Boolean execute(Node node)
        {
            //assign->id := exp
            //Parser.getInstance().AdvanceInput();
            //MessageBox.Show(Parser.getInstance().GetNextToken().tokenValue);
            Token next = Parser.getInstance().GetNextToken();
            Token ex = new Token();
            Token ex2 = new Token();
            GrExp exp = new GrExp();
            ex.TokenType = Scanner.Scanner.STATES.T_IDENTIFIER.ToString();

            // mlhash lazma bgnyh keda keda e7na asln law msh 7aga 3arfnha bn7otha identifier f faks
            Boolean match = MatchInput(ex);

           //MessageBox.Show("ASSIGN STMNT 1::\t" + match.ToString() + "\ntokenValue::  " + next.tokenValue + "\ntokenType:: " 
           //     + next.tokenValue + "\nnode::  " + node.Text);

            if (match == true)
            {
               // node.Text = node.Text + "        {" + next.tokenValue + ".ID}";
                Parser.getInstance().AdvanceInput(); //get current next token 
                //Parser.getInstance().AdvanceInput();
            }

            //MessageBox.Show("ASSIGN STMNT 2::\t" + match.ToString() + "\ntokenValue::  " + next.tokenValue + "\ntokenType:: "
            //   + next.tokenValue + "\nnode::  " + node.Text);

            ex2.TokenType = Scanner.Scanner.STATES.T_ASSIGN.ToString();
            match = MatchInput(ex2);
           //MessageBox.Show("ASSIGN STMNT 3 ::\t"+match.ToString());
            if (MatchInput(ex2))
            {
                Parser.getInstance().AdvanceInput();
                /*
                if (Parser.getInstance().GetNextToken().tokenValue == next.tokenValue)
                {
                    node.Text = node.Text;// + "        {" + next.tokenValue + ".ID}";
                    Parser.getInstance().AdvanceInput();
                    Parser.getInstance().AdvanceInput();
                }
                */

                node.Text = node.Text + "        {" + next.tokenValue + ".ID}";
                return Controller.getInstance().MatchExpression(node, Parser.getInstance().GetNextToken(), exp);

                

            }else
            {
                MessageBox.Show("expected ':=' but found " + Parser.getInstance().GetNextToken().tokenValue, "Invalid Assignment Statment!");
                return false;
            }
            //return true;
            //MessageBox.Show(Parser.getInstance().GetNextToken().TokenType);
            //node.Text = node.Text + "        {" + next.tokenValue + ".ID}";
            //Controller.getInstance().MatchExpression(node, Parser.getInstance().GetNextToken(), exp);


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