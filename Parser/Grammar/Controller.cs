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
            initGRDict2();
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

        private void initGRDict2()
        {
            //GRDict.Add(START_TOKEN.T_IDENTIFIER.ToString(), new GrAssignStmt());

            GRDict2.Add(SECONDARY_TOKEN.T_ELSE.ToString(), new GrIfStmt());
            GRDict2.Add(SECONDARY_TOKEN.T_END.ToString(), new GrIfStmt());
            GRDict2.Add(SECONDARY_TOKEN.T_UNTIL.ToString(), new GrRepeatStmt());
        }
        #endregion

        #region Private Attributes 
        private Node HeadNode = new Node();
        private enum START_TOKEN { T_IF, T_REPEAT, T_READ, T_WRITE, T_IDENTIFIER }
        private enum SECONDARY_TOKEN { T_ELSE, T_UNTIL, T_END}
        Dictionary<string, GrammarRule> GRDict = new Dictionary<string, GrammarRule>();
        Dictionary<string, GrammarRule> GRDict2 = new Dictionary<string, GrammarRule>();

        #endregion

        #region Public Methods
        /// <summary>
        /// calls appropriate Grammar Rule for a certain token
        /// Advance input 
        /// </summary>
        /// <param name="token"></param>
        /// 
        static string lastTokenType = " ";


        // el token da maloosh ay 30 lazma wlahi w 2arfni w hamoot w ashylo bas m7tag ashylo mn 200 alf 7eta f shylo enta law sm7t
        public Boolean MatchExpression(Node node, Token token, GrammarRule GR)
        {
            string grTypyFull = GR.GetType().ToString();
            string grType = grTypyFull.Substring(grTypyFull.LastIndexOf('.') + 1);
           // MessageBox.Show(grType);
            if (grType[0] == 'G')
                grType = grType.Substring(2);
            
            
           /* MessageBox.Show("MATCH EXPRESSION \ngrtypr:: " + grType + 
                "\ntokenValue::  " + token.tokenValue + "\ntokenType:: " + token.tokenValue 
                + "\nnode::  " + node.Text + "\n GRtype::\t" + GR.GetType() + "\nlasttype\t" + lastTokenType);
            */
            
             //+ "\ntokenValue::  " + token.tokenValue + "\ntokenType:: " + token.tokenValue + "\nnode::  " + node.Text 

            if (GR.GetType() == typeof(GrExp))
            {
                lastTokenType = grType;

                Node nodeNew = new Node(grType);
                node.AddChild(nodeNew);
              
                MessageBox.Show("MATCH EXPRESSION inside:: " +  node.Text);
                
                //Match Grammar Rule
                return GR.execute(nodeNew);

            }
            else
            {
                //MessageBox.Show("elseee");
                //lastTokenType = grType;

                //node.Text = grType;


                return GR.execute(node);


            }

         }

        /// <summary>
        /// Matches Statement to next token 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="GR"></param>
        /// 
        //MatchStatement
        public Boolean MatchStatement(Node node, bool isSibling, bool addNode = true)
        {

            Node newNode = new Node();
            if (addNode)
            {
                if (isSibling)
                {
                    node.AddSibling(newNode);
                }
                else
                {
                    node.AddChild(newNode);
                }
            }
            

            //match the current token to the next Grammar Rule
            Token nextToken = Parser.getInstance().GetNextToken();
             

            GrammarRule gr;

            string extratype = nextToken.TokenType;

            MessageBox.Show("CONTROLLER FIRST ARRIVAL::  " + nextToken.tokenValue);

            // try to match the token with some statement
            if (GRDict.TryGetValue(nextToken.TokenType, out gr))
            {
               
               //Parser.getInstance().AdvanceInput();
                
                string grTypeFull = gr.GetType().ToString();
                
                //clean returned string
                string grType = grTypeFull.Substring(grTypeFull.LastIndexOf('.') + 1);
                
                if (grType[0] == 'G')
                    grType = grType.Substring(2);

                extratype = grType;

                MessageBox.Show("GR:: " + grType);

                

                // MessageBox.Show(nextToken.tokenValue);

                newNode.Text = grType.Substring(0,grType.Length-4);
                return gr.execute(newNode);
            }/*
            else if(GRDict2.TryGetValue(nextToken.TokenType, out gr))
            {
                string grTypeFull = gr.GetType().ToString();

                //clean returned string
                string grType = grTypeFull.Substring(grTypeFull.LastIndexOf('.') + 1);

                MessageBox.Show("GR2 ::  " + grType + grTypeFull);
                newNode.Text = extratype;
                //grType.Substring(0, grType.Length - 4);

                Parser.getInstance().AdvanceInput();
                //Parser.getInstance().AdvanceInput();
                //gr.execute(newNode);
            }*/
            else
            {
                
                MessageBox.Show(extratype.ToString(), "CONTROLLER-MATCHSTATMENT:: You are not following the grammar rules.");
                //MessageBox.Show(" You are not following the grammar rules.");
                //Form1.getInstance().Close();
                //Application.Exit();

                return false;
            }


            /*
             * else if (Parser.getInstance().GetNextToken().TokenType.ToString() == "T_ELSE" )
            //|| Parser.getInstance().GetNextToken().TokenType.ToString() == "T_END")
            {
                MessageBox.Show("else detected");
                newNode.Text = "ELSE";
                
                Parser.getInstance().AdvanceInput();

                GrStmtSequence stm2 = new GrStmtSequence();
                stm2.execute(newNode);

                //newIf.execute(newNode);
                    //grType.Substring(0, grType.Length - 4);
                
                //return;
            }else if (Parser.getInstance().GetNextToken().TokenType.ToString() == "T_END")
            {
                Parser.getInstance().AdvanceInput();
                //GrStmtSequence stm2 = new GrStmtSequence();
                //stm2.execute(newNode);
            }
             */
        }

        public Boolean MatchStatmentSequence(Node node, bool b = true)
        {
            GrStmtSequence stmtSeq = new GrStmtSequence();
            stmtSeq.childFlag = b;
            return stmtSeq.execute(node);
        }

        public void Done()
        {
            Drawer.getInstance().initAndDraw();
            TreeForm.getInstance().Show();
        }
        #endregion
    }
}