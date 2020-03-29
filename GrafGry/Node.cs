using System;
using System.Collections.Generic;

namespace GrafGry
{
    public class Node<T> : IComparable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public T Value { get; set; }
        public int? Score { get; set; }

        public Node(string name, T value, int id = 0)
        {
            Id = id;
            Name = name;
            Value = value;
            Score = null;
        }

        public bool IsLeaf()
        {
            return Name == "Wygrana" ||
                Name == "Przegrana" ||
                Name == "Remis";
        }

        public bool HasScore() => Score != null;

        public override string ToString() => $"\"{Name};\\n{Value}{(Score != null ? $";\\nwynik={Score}" : "")}\"";

        public override bool Equals(object obj)
        {
            var x = obj as Node<T>;

            if (x == null)
            {
                return false;
            }

            return Name == x.Name && Value.Equals(x.Value) && Id == x.Id;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            throw new NotImplementedException();
            return base.GetHashCode();
        }

        public int CompareTo(object obj)
        {
            var node = obj as Node<T>;
            if (node != null)
            {
                if (Score == null || node.Score == null)
                    throw new InvalidOperationException("Can't compare nulls");

                int score1 = (int)Score;
                int score2 = (int)node.Score;
                return score1.CompareTo(score2);
            }

            throw new ArgumentException("Object is not Node");
        }
    }
}
