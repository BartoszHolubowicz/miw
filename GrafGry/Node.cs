using System;

namespace GrafGry
{
    public class Node<T>
    {
        public string Name { get; set; }
        public T Value { get; set; }

        public Node(string name, T value)
        {
            Name = name;
            Value = value;
        }

        public override bool Equals(object obj)
        {
            var x = obj as Node<T>;

            if (x == null)
            {
                return false;
            }
            
            return Name == x.Name && Value.Equals(x.Value);    
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            throw new NotImplementedException();
            return base.GetHashCode();
        }

        public static bool operator ==(Node<T> n1, Node<T> n2) => n1.Equals(n2);

        public static bool operator !=(Node<T> n1, Node<T> n2) => !n1.Equals(n2);
    }
}
