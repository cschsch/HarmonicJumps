using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace HarmonicJumps
{
    public class Harmonizer
    {
        public int MaxDepth { get; }

        public Harmonizer(int maxDepth)
        {
            MaxDepth = maxDepth;
        }

        public static IEnumerable<Key> Next(Key key, FindOptions options = FindOptions.RepeatSameKey)
        {
            yield return key - 7;
            yield return key - 2;
            yield return key - 1;
            if(options.HasFlag(FindOptions.RepeatSameKey)) yield return key;
            yield return -key;
            yield return key + 1;
            yield return key + 2;
            yield return key + 7;

            if(key.Signature == Signature.Minor)
            {
                yield return -key - 1;
                yield return -key + 3;
                yield return -key - 4;
            }
            else if(key.Signature == Signature.Major)
            {
                yield return -key + 1;
                yield return -key - 3;
                yield return -key + 4;
            }
        }

        public IEnumerable<ICollection<Key>> Find(Key start, Key end, FindOptions options = FindOptions.Default)
        {
            if (start.Equals(end)) throw new ArgumentException($"Source and target key are the same.");
            var root = new Node<Key> { Value = start };
            var paths = Next(start, options)
                .Select(key => new[] { new Node<Key> { Value = key, Parent = root } })
                .AsParallel()
                .Select(child => FindInternal(new Queue<Node<Key>>(child), end, 1, options));

            return paths.Aggregate(
                () => Enumerable.Empty<ICollection<Key>>(),
                (paths, nextResult) => paths.Concat(nextResult),
                (paths, otherPaths) => paths.Concat(otherPaths),
                result => result);
        }

        private IEnumerable<ICollection<Key>> FindInternal(Queue<Node<Key>> nodesToProcess, Key end, int currentDepth, FindOptions options)
        {
            if (currentDepth > MaxDepth || !nodesToProcess.Any()) yield break;

            var node = nodesToProcess.Dequeue();

            if (node.Value.Equals(end))
            {
                yield return new[] { node.Value }.Concat(node.GetParents()).Reverse().ToArray();
            }
            else
            {
                var children = Next(node.Value, options)
                    .Where(key => options.HasFlag(FindOptions.RepeatSameKey) || !node.GetParents().Contains(key))
                    .Select(key => new Node<Key> { Value = key, Parent = node });

                foreach (var child in children)
                {
                    nodesToProcess.Enqueue(child);
                }
            }

            foreach(var seq in FindInternal(nodesToProcess, end, node.Depth, options))
            {
                yield return seq;
            }
        }
    }
}
