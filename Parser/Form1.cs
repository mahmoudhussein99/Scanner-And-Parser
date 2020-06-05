using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Scanner
{
    public partial class Form1 : Form
    {
        #region singlton 
        private Form1()
        {
            InitializeComponent();
        }

        private static Form1 instance = new Form1();

        public static Form1 getInstance()
        {
            if (instance != null)
                instance = new Form1();

            return instance;
        }
        #endregion 

        string code;
        Scanner scanner = new Scanner();
        List<Token> tokens = new List<Token>();


        private void UpdateTable()
        {

            code = codeLinesTextBox.Text;
            
            tokens = scanner.getListOfTokens(code);

            foreach (Token token in tokens)
            {
                DataGridView.Rows.Add(token.TokenType, token.tokenValue);
            }
            Parser.Parser.getInstance().init(code);

        }

        private void Go_Click(object sender, EventArgs e)
        {

            //eftkr enk hena kol mra tfdy el tree
            DataGridView.Rows.Clear();
            tokens.Clear();
            Parser.Parser.getInstance().parserTree = new Parser.MyTree.Tree();
            UpdateTable();
            if (scanner.bracesMismatchErrorCheck() == 1)
            {
                MessageBox.Show("Error UnMatched Braces", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (scanner.bracketsMismatchErrorCheck() == 1)
            {
                MessageBox.Show("Error UnMatched Brackets", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (scanner.quotationsMismatchErrorCheck() == 1)
            {
                MessageBox.Show("Error UnMatched Quotations", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (scanner.isIllegal() == 1)
            {
                MessageBox.Show("Illegal Characters are entered!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            if (scanner.isNumberError() == 1)
            {
                MessageBox.Show("You cannot type a Number with a digit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void codeLinesTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}