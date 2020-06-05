using Parser.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parser
{
    public partial class TreeForm : Form
    {
        private TreeForm()
        {
            InitializeComponent();
        }
        private static TreeForm instance = new TreeForm();

        public static TreeForm getInstance()
        {
            if (instance != null)
                instance = new TreeForm();

            return instance;
        }

        private void TreeForm_Load(object sender, EventArgs e)
        {

        }

        private void TreeForm_Paint(object sender, PaintEventArgs e)
        {
            Drawer.getInstance().DrawGraphicalObjects(sender, e);
        }
    }
}
