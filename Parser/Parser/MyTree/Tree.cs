using Parser.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.MyTree

{
    public class Tree
    {
        public Node HeadNode = new Node();
        List<Node> nodesList = new List<Node>();

        public Tree()
        {
            HeadNode.ownerTree = this;
        }

        public void KeepTrackOfNode(Node n)
        {
            if (n.Level != 0)
            {
                nodesList.Add(n);
            }
        }

        public List<Node> getNodesList()
        {
            return nodesList;
        }

        public void UntieChild(Node node)
        {
            foreach (Node n in nodesList)
            {
                if (node.Equals(n))
                {
                    nodesList.Remove(n);
                    break;
                }
            }
        }
    }
}