using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafGry
{
    public class Tree<T> where T : IComparable
    {
        public List<Node<T>> Nodes { get; set; }
        public List<Edge<T>> Edges { get; set; }

        public Tree(List<Node<T>> nodes, List<Edge<T>> edges)
        {
            Nodes = nodes;
            Edges = edges;
        }

        public Tree()
        {
            Nodes = new List<Node<T>>();
            Edges = new List<Edge<T>>();
        }

        public Node<T> Root
        {
            get
            {
                foreach (var node in Nodes)
                    if (node.Id == 0)
                        return node;

                return null;
            }
            set
            {
                Node<T> root;

                foreach (var node in Nodes)
                    if (node.Id == 0)
                        root = node;

                root = value;
            }
        }

        public Node<T> NodeParent(Node<T> child)
        {
            foreach (var edge in Edges)
                if (edge.TargetNode.Equals(child))
                    return edge.SourceNode;

            return null;
        }

        public List<Node<T>> NodeChildren(Node<T> parent)
        {
            List<Node<T>> childrenNodes = new List<Node<T>>();

            foreach (var edge in Edges)
                if (edge.SourceNode.Equals(parent))
                    childrenNodes.Add(edge.TargetNode);

            return childrenNodes;
        }

        public Edge<T> GetEdge(Node<T> parent, Node<T> child)
        {
            foreach (var edge in Edges)
                if (edge.SourceNode.Equals(parent) && edge.TargetNode.Equals(child))
                    return edge;

            return null;
        }

        public override string ToString()
        {
            StringBuilder resultString = new StringBuilder("strict digraph G {\n");

            foreach (Edge<T> edge in Edges)
            {
                resultString.AppendLine(edge.ToString());
            }

            resultString.AppendLine("}");

            return resultString.ToString();
        }

        public static Tree<int> GenerateLittleEye(string startingPlayer, int[] protTokens, int[] antTokens, int score)
        {
            Tree<int> result = new Tree<int>();
            int nextNodeId = 0;
            int nextEdgeId = 0;

            void GenerateChildren(Node<int> parent)
            {
                string childrenName = parent.Name == "P" ? "A" : "P";
                int[] tokens = childrenName == "P" ? protTokens : antTokens;

                foreach (int token in tokens)
                {
                    int childrenValue = parent.Value + token;

                    Node<int> child = new Node<int>(childrenName, childrenValue, nextNodeId++);
                    Edge<int> edge = new Edge<int>(parent, child, token, nextEdgeId++);

                    if (childrenValue == score)
                    {
                        child.Name = "Remis";
                        child.Value = 2;
                        child.Score = 2;
                    }
                    else if (childrenValue > score)
                    {
                        bool parentIsProtagonist = parent.Name == "P";

                        child.Name = parentIsProtagonist ? "Przegrana" : "Wygrana";
                        child.Value = parentIsProtagonist ? 1 : 3;
                        child.Score = parentIsProtagonist ? 1 : 3;
                    }
                    else
                    {
                        GenerateChildren(child);
                    }
                    
                    result.Nodes.Add(child);
                    result.Edges.Add(edge);
                }
            }

            Node<int> root = new Node<int>(startingPlayer, 0, nextNodeId++);
            result.Nodes.Add(root);

            GenerateChildren(root);

            return result;
        }

        public Tree<T> MinMax()
        {
            Tree<T> result = new Tree<T>(Nodes, Edges);

            void SelectFromChildren(Node<T> parent)
            {
                List<Node<T>> children = NodeChildren(parent);

                foreach (var child in children)
                {
                    if (!child.HasScore())
                    {
                        SelectFromChildren(child);
                    }
                }

                Node<T> bestNode = parent.Name == "P" ? children.Max() : children.Min();
                parent.Score = bestNode.Score;

                GetEdge(parent, bestNode).Color = "red";
            }

            SelectFromChildren(result.Root);
            return result;
        }
    }
}
