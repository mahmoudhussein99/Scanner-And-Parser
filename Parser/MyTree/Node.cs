using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Parser.MyTree
{
    public class Node
    {
        #region Constructors
        public Node(string txt)
        {
            Children = new List<Node>();
            Siblings = new List<Node>();
            Level = 0;
            isLeaf = true;
            isLonely = true;
            this.Text = txt;
        }
        public Node()
        {
            Children = new List<Node>();
            Siblings = new List<Node>();
            Level = 0;
            isLeaf = true;
            isLonely = true;
        }

        #endregion

        #region Properties 
        public List<Node> Children { get; }
        public List<Node> Siblings { get; }

        public Tree ownerTree { get; set; }
        public Node Parent { get; set; }

        public int Level { get; set; }
        public bool isLeaf { get; set; }
        public bool isLonely { get; set; }

        public string Text { get; set; }
        #endregion

        #region Public Atttributes
        public Point position = new Point();
        #endregion

        #region Public Functions
        #region Adding Child
        public void AddChild(Node n)
        {
            Children.Add(n);

            isLeaf = false;
            n.Level = this.Level + 1;
            n.Parent = this;

            addOwnerTree(n);
        }

        public Node GetLastChild()
        {
            if (Children.Count != 0)
            {
                return Children.Last();
            }
            return null;
        }

        public void AddChild(string text)
        {
            Node temp = new Node(text);
            AddChild(temp);
        }
        #endregion
        #region Add Sibling 
        public void AddSibling(Node n)
        {
            Siblings.Add(n);
            isLonely = false;
            n.Level = this.Level;

            addOwnerTree(n);
        }
        public void AddSibling(string txt)
        {
            Node tmp = new Node(txt);
            Siblings.Add(tmp);
        }
        #endregion
        #endregion

        #region Private Functions
        private void addOwnerTree(Node n)
        {
            if (n.ownerTree == null)
            {
                n.ownerTree = this.ownerTree;
            }
            ownerTree.KeepTrackOfNode(n);
        }
        #endregion
    }
}