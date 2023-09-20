using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MMTS_Lab1.Models
{
    public class DecomposedNode
    {
        public string? Name { get; set; }
        public List<int> InsideNodes { get; set; }
        public List<DecomposedNode>? DecomposedNodes { get; set; }

        public DecomposedNode(string name, List<int> insideNodes) 
        {
            Name = name;
            InsideNodes = insideNodes;
        }

        public void AddDecomposedNode(DecomposedNode decomposedNode)
        {
            if (decomposedNode.Name == Name)
                return;
            
            else if (DecomposedNodes == null)
                DecomposedNodes = new List<DecomposedNode>() { decomposedNode };

            else if (DecomposedNodes.Contains(decomposedNode))
                return;

            else
                DecomposedNodes.Add(decomposedNode);
        }
    }
}
