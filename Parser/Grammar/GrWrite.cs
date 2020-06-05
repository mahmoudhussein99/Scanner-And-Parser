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
    public class GrWriteStmt : GrammarRule
    {
        public override Boolean execute(Node node)
        {
            // match the write exp
            Parser.getInstance().AdvanceInput();
            GrExp exp = new GrExp();
            Boolean flag=Controller.getInstance().MatchExpression(node, Parser.getInstance().GetNextToken(), new GrExp());
            if (!flag)
            {
                MessageBox.Show("Error in parsing error after write!");
            }
            
            return flag;
        }
    }
}