using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMTS_Lab1.Models
{
    public class Node
    {
        public int Name {  get; set; }
        public List<Node>? NextNodes { get; set; }
        public List<Node>? PreviousNodes { get; set; }

        public Node(int name) 
        { 
            Name = name;
        }

        public void AddNextNodes(List<Node>? nodes)
        {
            if (nodes == null)
                NextNodes = new List<Node>();
            else
                NextNodes = new List<Node>(nodes);
        }

        public void AddPreviousNodes(Node node)
        {
            if (PreviousNodes == null)
                PreviousNodes = new List<Node>() { node };
            else
                PreviousNodes.Add(node);
        }
    }
}
