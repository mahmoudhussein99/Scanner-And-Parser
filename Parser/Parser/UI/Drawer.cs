using Microsoft.SqlServer.Server;
using Parser.MyTree;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


namespace Parser.UI
{
    public class Drawer
    {

        public void initAndDraw()
        {
            doneParsing = true;
            CreateAndDrawGObjects();
        }

        #region Drawing Properties
        private const int G_NODE_WIDTH = 50;
        private const int G_NODE_HEIGHT = 50;
        private Pen DRAWING_PEN = Pens.Blue;
        private Brush TEXT_BRUSH = Brushes.Blue;
        private Brush FILL_BRUSH = Brushes.Bisque;
        private Font TEXT_FONT = new Font(SystemFonts.IconTitleFont, FontStyle.Regular );
        private StringFormat sformat = new StringFormat();
        
        

        #endregion

        #region Private Attributes

        private bool doneParsing = false;
        private Parser parserInstance = Parser.getInstance();
        private Dictionary<int, List<Node>> nodesLevelsMap = new Dictionary<int, List<Node>>();
        private Dictionary<Rectangle, string> nodesList = new Dictionary<Rectangle, string>();
        private List<KeyValuePair<Point, Point>> edgesList = new List<KeyValuePair<Point, Point>>();
        #endregion

        #region Public Attributes
        public List<Node> value = new List<Node>();
        public int HeightForm, WidthForm, NumberOfLevel, CountN = 0, key3;
        #endregion

        #region Singleton
        private Drawer() { }
        private static Drawer instance;
        public static Drawer getInstance()
        {
            if (instance == null)
                instance = new Drawer();
            return instance;
        }
        #endregion

        private void CreateAndDrawGObjects()
        {
            GroupNodesByLevel();
            CreateGNodes();
            CreateGEdges();
        }

        /// <summary>
        /// this should called by onPaint event in the drawing form 
        /// itt actually draws
        /// </summary>
        public void DrawGraphicalObjects(object sender, PaintEventArgs e)
        {

            if (doneParsing && nodesList.Count != 0)
            {
                DrawEdges(e);
                foreach (Rectangle rect in nodesList.Keys)
                {
                    
                    e.Graphics.DrawRectangle(DRAWING_PEN, rect);
                    e.Graphics.FillRectangle(FILL_BRUSH, rect);
                    sformat.Alignment = StringAlignment.Center;
                    sformat.LineAlignment = StringAlignment.Center;
                    e.Graphics.DrawString(nodesList[rect], TEXT_FONT, TEXT_BRUSH, rect,sformat);
                }
            }
        }

        #region private internal functions

        /// <summary>
        /// fill Dict 
        /// groups nodes by levels in a dictionary to be able to draw nodes by levels
        /// 
        /// input: Tree
        /// output: nodes grouped by level in the nodesLevelsDict
        /// 
        /// Assignee: Menna
        /// </summary>
        private void GroupNodesByLevel()
        {
            foreach (Node node in parserInstance.parserTree.getNodesList())
            {
                if (nodesLevelsMap.ContainsKey(node.Level))
                    nodesLevelsMap[node.Level].Add(node);
                else
                    nodesLevelsMap[node.Level] = new List<Node> { node };
            }
        }

        /// <summary>
        /// * calculates position of each node based on its level and the no.of nodes that are in its same level
        /// * adds the calculated position point to the (Position) attribute in (Node) class
        /// * creates a graphic object (rectangle or ellipse) for the node and add it to (nodesList) in this classs
        /// 
        /// input: nodesLevelsMap (in this class) 
        /// output: calculated position & created GObjects
        /// 
        /// Note: hab2a an2elha fl private ba3d ma n5allas 34an a3rf a7ottelek btoo3k f region 3 b3daha :D 
        /// </summary>
        private void CreateGNodes()
        {
            NumberOfLevel = nodesLevelsMap.Count;

            HeightForm = TreeForm.getInstance().ClientRectangle.Height;
            WidthForm = TreeForm.getInstance().ClientRectangle.Width;

            foreach (var kvp in nodesLevelsMap)
            {
                value = kvp.Value;
                key3 = kvp.Key;
                foreach (Node v in value)
                {
                    CountN++;
                    v.position.Y = (((key3 * (HeightForm / NumberOfLevel))/2) + ((HeightForm / NumberOfLevel) / 2));
                    //v.position.Y = 0;
                    v.position.X = (((WidthForm / value.Count) * CountN) / 2) + CountN;
                   // v.position.Y = 50;
                    AddGnode(v);
                }
                CountN = 0;
            }
        }

        private void AddGnode(Node n)
        {

            Rectangle rect = new Rectangle(n.position.X, n.position.Y, G_NODE_WIDTH, G_NODE_HEIGHT);
            nodesList[rect] = n.Text;
         
        }


        private void DrawEdges(PaintEventArgs e)
        {
            Point start, end;

            foreach (KeyValuePair<Point, Point> pointsPair in edgesList)
            {
                start = new Point(pointsPair.Key.X + (G_NODE_WIDTH / 2),
                                  pointsPair.Key.Y + (G_NODE_HEIGHT / 2));

                end = new Point(pointsPair.Value.X + (G_NODE_WIDTH / 2),
                                  pointsPair.Value.Y + (G_NODE_HEIGHT / 2));

                e.Graphics.DrawLine(DRAWING_PEN, start, end);
            }
        }

        private void CreateGEdges()
        {
            List<Node> parsedNodes = parserInstance.parserTree.getNodesList();
            ConnectRecursive(parsedNodes);
        }

        private void ConnectRecursive(List<Node> parsedNodes)
        {
            foreach (Node node in parsedNodes)
            {
                ConnectChildrenNodes(node);
                ConnectSiblingNodes(node);
            }
        }

        private void Connect(Node start, Node end)
        {
            KeyValuePair<Point, Point> pair = new KeyValuePair<Point, Point>(start.position, end.position);
            edgesList.Add(pair);
        }


        #region Edges Cre(start.Text + "\n"+end.Text);   /// <summary>
        /// makes use of Recursive function and list of nodes to create linkage between them 
        /// adds calculated edges to (edgesList)
        /// 
        /// </summary>
        private void ConnectChildrenNodes(Node parentNode)
        {
            if (parentNode.isLeaf)
                return;

            //create edges between the node and each of its children
            foreach (Node childNode in parentNode.Children)
            {
                Connect(parentNode, childNode);
            }

        }

        private void ConnectSiblingNodes(Node parentNode)
        {
            if (parentNode.isLonely)
                return;

            //create edges between node and its first sibling 
            Connect(parentNode, parentNode.Siblings[0]);
            //create edges between sibling and the next silbling
            List<Node> sibs = parentNode.Siblings;
            for (int i = 1; i < sibs.Count; i++)
            {
                Connect(sibs[i - 1], sibs[i]);
            }
        }


        #endregion
        #endregion
    }
}