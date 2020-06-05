using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scanner;
using Parser.Grammar;
using Parser.MyTree;

namespace Parser
{
    class Parser
    {
        #region Singleton 
        private static Parser instance;
        private Parser() { }
        public static Parser getInstance()
        {
            if (instance == null)
                instance = new Parser();
            return instance;
        }
        #endregion

        #region Private Attributes
        private List<Token> tokensList;
        private Token currentToken;
        private int currentTokenIndex = 0;
        public Tree parserTree = new Tree();
        #endregion

        #region Public Methods
        /// <summary>
        /// main entry point
        /// calls get tokensList 
        /// calls controller
        /// </summary>
        public void init(string ipProgram)
        {
            
            Scanner.Scanner scanner = new Scanner.Scanner();
            tokensList = scanner.getListOfTokens(ipProgram);

            currentToken = tokensList[currentTokenIndex];

            GrStmtSequence stmtSeq = new GrStmtSequence();
            stmtSeq.execute(parserTree.HeadNode);
            Node node = parserTree.HeadNode;
        }

        /// <summary>
        /// return current token
        /// </summary>
        /// <returns></returns>
        public Token GetNextToken()
        {
            return tokensList[currentTokenIndex];
        }
        /// <summary>
        /// currentToken ++ 
        /// </summary>
        public void AdvanceInput()
        {
            currentTokenIndex++;
            currentToken = tokensList[currentTokenIndex];
        }
        #endregion

    }
}