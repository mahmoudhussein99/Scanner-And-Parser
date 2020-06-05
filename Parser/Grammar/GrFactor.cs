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
    public class GrFactor : GrammarRule
    {
        public override Boolean execute(Node node)
        {
            // (exp)  |  number | identifier 

            // get the next token 
            Token nextToken = new Token();
            nextToken = Parser.getInstance().GetNextToken();

            Token expToken = new Token();

           // MessageBox.Show(nextToken.TokenType);
            switch (nextToken.TokenType)
            {

                case "T_NUMBER":
                    // create a child node for the number
                     Node numChild = new Node();
                    // node.Text = nextToken.TokenType;
                     numChild.Text =  nextToken.tokenValue;
                     node.AddChild(numChild);
                    //node.Text = (nextToken.TokenType + "         " + nextToken.tokenValue);
                    //advance the number
                    Parser.getInstance().AdvanceInput();                 
                    break;

                case "T_IDENTIFIER":
                    // create a child node for the identifier
                    Node idChild = new Node();
                    //node.Text = nextToken.TokenType;
                    idChild.Text= nextToken.tokenValue;
                    node.AddChild(idChild);
                    //node.Text = (nextToken.TokenType + "         {" + nextToken.tokenValue +"}");

                    // advance the identifier
                    Parser.getInstance().AdvanceInput();
                    break;

                case "T_LEFTBRACKET":
                    // advance the left barcket
                    Parser.getInstance().AdvanceInput();

                    // match the exp
                    Boolean flag=Controller.getInstance().MatchExpression(node, nextToken, new GrExp());

                    //match the right bracket
                    if (!flag)
                        return false;
                    Parser.getInstance().AdvanceInput();

                    expToken.TokenType = "T_RIGHTBRACKET";
                    Boolean matched = MatchInput(expToken);
                    
                    if (matched)
                    {
                        MessageBox.Show("right bracket detected");

                        //advance the right bracket
                        Parser.getInstance().AdvanceInput();
                    }
                    else
                    {
                        MessageBox.Show(" FACTOR:: You are not following the grammar rules.");
                        Form1.getInstance().Close();
                        //Application.Exit();
                    }
                    
                    /*
                    //check the case of GrFactor having only one child
                    if (node.Children.Count == 1)
                    {
                        // remove this child from the list of children and from the tree list
                        node.Text = node.Children[0].Text;
                        Parser.getInstance().parserTree.UntieChild(node.Children[0]);
                        node.Children.Clear();
                    }
                    */
                    break;
                default:
                    MessageBox.Show("Invalid Factor");
                    return false;
            }
            return true;


        }
    }
}