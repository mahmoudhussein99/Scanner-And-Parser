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
        public override void execute(Node node)
        {
            // (exp)  or  number or identifier 

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
                    Controller.getInstance().MatchExpression(node, nextToken, new GrExp());
                    //match the right bracket
                    expToken.TokenType = "T_RIGHTBRACKET";
                    Boolean matched = MatchInput(expToken);
                    if (matched)
                    {
                        //advance the right bracket
                        Parser.getInstance().AdvanceInput();
                    }

                    //check the case of GrFactor having only one child
                    if (node.Children.Count == 1)
                    {
                        // remove this child from the list of children and from the tree list
                        node.Text = node.Children[0].Text;
                        Parser.getInstance().parserTree.UntieChild(node.Children[0]);
                        node.Children.Clear();
                    }
                    break;
            }


        }
    }
}