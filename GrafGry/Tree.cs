using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafGry
{
    public class Tree<T>
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

        public Node<T> NodeParent(Node<T> child)
        {
            foreach (var edge in Edges)
                if (edge.TargetNode == child)
                    return edge.SourceNode;

            return null;
        }

        public List<Node<T>> NodeChildren(Node<T> parent)
        {
            List<Node<T>> childrenNodes = new List<Node<T>>();

            foreach (var edge in Edges)
                if (edge.SourceNode == parent)
                    childrenNodes.Add(edge.TargetNode);

            return childrenNodes;
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

            void GenerateChildren(Node<int> parent)
            {
                string childrenName = parent.Name == "P" ? "A" : "P";
                int[] tokens = childrenName == "P" ? protTokens : antTokens;

                foreach (int token in tokens)
                {
                    int childrenValue = parent.Value + token;

                    Node<int> child = new Node<int>(childrenName, childrenValue);
                    Edge<int> edge = new Edge<int>(parent, child, token);

                    if (childrenValue == score)
                    {
                        child.Name = "Remis";
                        child.Value = 2;
                    }
                    else if (childrenValue > score)
                    {
                        bool parentIsProtagonist = parent.Name == "P";

                        child.Name = parentIsProtagonist ? "Przegrana" : "Wygrana";
                        child.Value = parentIsProtagonist ? 1 : 3;
                    }
                    else
                    {
                        GenerateChildren(child);
                    }
                    
                    result.Nodes.Add(child);
                    result.Edges.Add(edge);
                }
            }

            Node<int> root = new Node<int>(startingPlayer, 0);
            result.Nodes.Add(root);

            GenerateChildren(root);

            return result;
        }
    }
}
