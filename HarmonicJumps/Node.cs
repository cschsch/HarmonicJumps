using System.Collections.Generic;

namespace HarmonicJumps
{
    public class Node<T> where T : class
    {
        public T Value { get; set; }
        public Node<T> Parent { get; set; }
        public int Depth => Parent is null
                    ? 0
                    : 1 + Parent.Depth;

        public IEnumerable<T> GetParents()
        {
            var parent = Parent;

            while (parent != default)
            {
                yield return parent.Value;
                parent = parent.Parent;
            }
        }
    }
}
