﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Parser.MyTree;
using Scanner;

namespace Parser.Grammar
{
    public class GrReadStmt : GrammarRule
    {
        public override Boolean execute(Node node)
        {
            Parser.getInstance().AdvanceInput();
            // match the read identifier
            Token expectedToken = new Token();

            expectedToken.TokenType = Scanner.Scanner.STATES.T_IDENTIFIER.ToString();
            // MessageBox.Show(Parser.getInstance().GetNextToken().tokenValue);

            Boolean matched = MatchInput(expectedToken);
            //MessageBox.Show(matched.ToString());

            if (matched)
            {
                // modify the read node text to add the identifier name
                node.Text = node.Text + "\n{" + Parser.getInstance().GetNextToken().tokenValue + ".ID}";
                //MessageBox.Show(node.Text);
                // advance the identifier
                Parser.getInstance().AdvanceInput();
                return true;
            }
            else
            {
                MessageBox.Show("Invalid Identifier in Read!!");

                return false;
            }
        }
    }
}