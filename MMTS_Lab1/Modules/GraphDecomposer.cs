using MMTS_Lab1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMTS_Lab1.Modules
{
    public static class GraphDecomposer
    {
        public static void PrintDecomposedGrpah(List<DecomposedNode> graph)
        {
            foreach (DecomposedNode node in graph)
            {
                Console.WriteLine("----------");

                Console.Write($"Node name: {node.Name}; All nodes that are assigned to {node.Name}: ");
                node.InsideNodes.ForEach(n => Console.Write(n + "; "));

                Console.WriteLine();

                Console.Write($"G({node.Name}) = ");

                if (node.DecomposedNodes == null)
                {
                    Console.Write(0);
                }
                else
                {
                    node.DecomposedNodes?.Select(n => n.Name).ToList().ForEach(n => Console.Write(n + "; "));
                }

                Console.WriteLine();

                Console.WriteLine("----------");
            }
        }

        public static List<DecomposedNode> DecomposeGraph(List<Node> nodes)
        {
            var decomposedGraph = new List<DecomposedNode>();

            var nodesCopy = nodes.GetRange(0, nodes.Count);
            var forbiddenNodes = new List<int>();
            int i = 1;

            // Make decomposed nodes
            while (nodesCopy.Count > 0)
            {
                var node = nodesCopy.First();

                var Ri = new List<int>();
                var Qi = new List<int>();

                Ri.FormRi(node, forbiddenNodes);
                Qi.FormQi(node, forbiddenNodes);

                var result = Ri.Intersect(Qi).ToList();
                forbiddenNodes.AddRange(result);

                var decomposedNode = new DecomposedNode($"V{i}", result);
                i++;

                decomposedGraph.Add(decomposedNode);

                nodesCopy.RemoveAll(n => result.Contains(n.Name));
            }

            // Connect all decomposed nodes
            foreach (var node in nodes)
            {
                var decomposedNode = decomposedGraph.Where(d => d.InsideNodes.Contains(node.Name)).First();

                if (node.NextNodes != null)
                {
                    foreach (var nextNode in node.NextNodes)
                    {
                        var neededDecomposedNode = decomposedGraph.Where(d => d.InsideNodes.Contains(nextNode.Name)).First();

                        decomposedNode.AddDecomposedNode(neededDecomposedNode);
                    }
                }
            }

            return decomposedGraph;
        }

        // Forming R(i) set
        private static void FormRi(this List<int> Ri, Node node, List<int> forbiddenNodes)
        {
            if (!forbiddenNodes.Contains(node.Name) && !Ri.Contains(node.Name))
            {
                Ri.Add(node.Name);

                if (node.NextNodes != null)
                {
                    foreach (var nextNode in node.NextNodes)
                    {
                        Ri.FormRi(nextNode, forbiddenNodes);
                    }
                }
            } 
        }

        // Forming Q(i) set
        private static void FormQi(this List<int> Qi, Node node, List<int> forbiddenNodes)
        {
            if (!forbiddenNodes.Contains(node.Name) && !Qi.Contains(node.Name))
            {
                Qi.Add(node.Name);

                if (node.PreviousNodes != null)
                {
                    foreach (var nextNode in node.PreviousNodes)
                    {
                        Qi.FormQi(nextNode, forbiddenNodes);
                    }
                }
            }
        }
    }
}
