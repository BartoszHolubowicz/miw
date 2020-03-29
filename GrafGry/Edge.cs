using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafGry
{
    public class Edge<T> where T : IComparable
    {
        public int Id { get; set; }
        public Node<T> SourceNode { get; set; }
        public Node<T> TargetNode { get; set; }
        public T Value { get; set; }
        public string Color { get; set; }

        public Edge(Node<T> sourceNode, Node<T> targetNode, T value, int id = 0, string color = "black")
        {
            SourceNode = sourceNode;
            TargetNode = targetNode;
            Value = value;
            Id = id;
            Color = color;
        }

        public override string ToString()
        {
            StringBuilder resultString = new StringBuilder();

            resultString.Append($"\t{SourceNode} -> {TargetNode} ");

            resultString.Append($"[label = \"{Value}\"{(Color == "black" ? "" : $" color=\"{Color}\"")}];");

            return resultString.ToString();
        }
    }
}
