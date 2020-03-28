using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafGry
{
    public class Edge<T>
    {
        public Node<T> SourceNode { get; set; }
        public Node<T> TargetNode { get; set; }
        public T Value { get; set; }

        public Edge(Node<T> sourceNode, Node<T> targetNode, T value)
        {
            SourceNode = sourceNode;
            TargetNode = targetNode;
            Value = value;
        }

        public override string ToString()
        {
            StringBuilder resultString = new StringBuilder();

            resultString.Append($"\t\"{SourceNode.Name};\\n");
            resultString.Append($"{SourceNode.Value}\" -> ");

            resultString.Append($"\"{TargetNode.Name};\\n");
            resultString.Append($"{TargetNode.Value}\" ");

            resultString.Append($"[label = \"{Value}\"];");

            return resultString.ToString();
        }
    }
}
