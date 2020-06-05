using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scanner;
using Parser.MyTree;
using Parser.UI;
using System.Windows.Forms;

namespace Parser.Grammar
{
    class Controller
    {

        #region Singleton
        private static Controller instance;
        private Controller()
        {
            initGRDict();
        }
        public static Controller getInstance()
        {
            if (instance == null)
                instance = new Controller();
            return instance;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// fill the GRList with onstances of all Grammar Rules
        /// </summary>
        private void initGRDict()
        {
            GRDict.Add(START_TOKEN.T_IDENTIFIER.ToString(), new GrAssignStmt());
            GRDict.Add(START_TOKEN.T_IF.ToString(), new GrIfStmt());
            GRDict.Add(START_TOKEN.T_READ.ToString(), new GrReadStmt());
            GRDict.Add(START_TOKEN.T_REPEAT.ToString(), new GrRepeatStmt());
            GRDict.Add(START_TOKEN.T_WRITE.ToString(), new GrWriteStmt());
        }
        #endregion

        #region Private Attributes 
        private Node HeadNode = new Node();
        private enum START_TOKEN { T_IF, T_REPEAT, T_READ, T_WRITE, T_IDENTIFIER }
        Dictionary<string, GrammarRule> GRDict = new Dictionary<string, GrammarRule>();

        #endregion

        #region Public Methods
        /// <summary>
        /// calls appropriate Grammar Rule for a certain token
        /// Advance input 
        /// </summary>
        /// <param name="token"></param>
        public void MatchExpression(Node node, Token token, GrammarRule GR)
        {
            string grTypyFull = GR.GetType().ToString();
            string grType = grTypyFull.Substring(grTypyFull.LastIndexOf('.') + 1);
           // MessageBox.Show(grType);
            if (grType[0] == 'G')
                grType = grType.Substring(2);
            if (GR.GetType() == typeof(GrExp))
            {
                Node nodeNew = new Node(grType);
                node.AddChild(nodeNew);
               // MessageBox.Show(token.tokenValue);
                //Match Grammar Rule
                GR.execute(nodeNew);
            }
            else
            {
                //node.Text = grType;
                GR.execute(node);
            }
         }

        /// <summary>
        /// Matches Statement to next token 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="GR"></param>
        /// 
        //MatchStatement
        public void MatchStatement(Node node, bool isSibling)
        {

            Node newNode = new Node();
            if (isSibling)
            {
                node.AddSibling(newNode);
            }

            else
            {
                node.AddChild(newNode);
            }

            //match the current token to the next Grammar Rule
            Token nextToken = Parser.getInstance().GetNextToken();
             

            GrammarRule gr;
           
            // try to match the token with some statement
            if (GRDict.TryGetValue(nextToken.TokenType, out gr))
            {
               
               //Parser.getInstance().AdvanceInput();
                
                string grTypeFull = gr.GetType().ToString();
                
                string grType = grTypeFull.Substring(grTypeFull.LastIndexOf('.') + 1);
                
                if (grType[0] == 'G')
                    grType = grType.Substring(2);
               
                // MessageBox.Show(nextToken.tokenValue);
                newNode.Text = grType.Substring(0,grType.Length-4);
                gr.execute(newNode);
            }
            else
            {
                MessageBox.Show( " You are not following the grammar rules.");
                Form1.getInstance().Close();
                Application.Exit();
            }
        }

        public void MatchStatmentSequence(Node node)
        {
            GrStmtSequence stmtSeq = new GrStmtSequence();
            stmtSeq.execute(node);
        }

        public void Done()
        {
            Drawer.getInstance().initAndDraw();
            TreeForm.getInstance().Show();
        }
        #endregion
    }
}