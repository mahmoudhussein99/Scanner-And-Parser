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
    public class GrIfStmt : GrammarRule
    {
        public override Boolean execute(Node node)
        {
            // if exp then stmtseq [else stmtseq] end
            Parser.getInstance().AdvanceInput();
            // match exp

            Boolean flag = Controller.getInstance().MatchExpression(node, Parser.getInstance().GetNextToken(), new GrExp());
            if (!flag)
                return false;

            // match the then token  
            Token thenToken = new Token();
            thenToken.TokenType = Scanner.Scanner.RESERVED_WORDS.T_THEN.ToString();
            Boolean thenMatched = MatchInput(thenToken);
            //MessageBox.Show(thenMatched.ToString());
            if (thenMatched)
            {
                // advance the then token 
                Parser.getInstance().AdvanceInput();
                // match stmt_seq
                Boolean statementFlag=Controller.getInstance().MatchStatmentSequence(node);
                if (!statementFlag)
                {
                    return false;
                }
                // get the next token 
                Token nextToken = Parser.getInstance().GetNextToken();
                // if there is else part advance the else token then match stmt_seq
                if (nextToken.tokenValue == Scanner.Scanner.RESERVED_WORDS.T_ELSE.ToString())
                {
                    // advance the else token
                    Parser.getInstance().AdvanceInput();
                    // match the else stmt_seq
                    Boolean elseFlag=Controller.getInstance().MatchStatmentSequence(node, false);
                    if (!elseFlag)
                        return false;
                }

                //get the next token which must be end 
                // nextToken = Parser.getInstance().GetNextToken();
                if (nextToken.TokenType == Scanner.Scanner.RESERVED_WORDS.T_END.ToString())
                {
                    //advance the end token 
                    Parser.getInstance().AdvanceInput();
                }
                else
                {
                    MessageBox.Show("NO END IFFFF", "Invalid If Statement");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Expected then found " + Parser.getInstance().GetNextToken().tokenValue, "Invalid If Statement");
                return false;
            }
            return true;
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