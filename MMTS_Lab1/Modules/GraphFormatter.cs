using MMTS_Lab1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MMTS_Lab1.Modules
{
    public static class GraphFormatter
    {
        public static List<Node> FormatGraph(List<JsonNode>? incomingNodes)
        {
            if (incomingNodes == null)
                throw new Exception("In order to work with this program you need to initialize nodes in GraphConfiguration.json file");

            var nodes = new List<Node>();

            // Node initialization
            foreach (var incomingNode in incomingNodes)
            {
                if (nodes.Any(n => n.Name == incomingNode.name))
                    throw new Exception($"Node with name {incomingNode.name} is already in list. " +
                        $"Check your configuration file, maybe you named some nodes with the same name.");

                nodes.Add(new Node(incomingNode.name));
            }

            // Add next nodes
            foreach (var incomingNode in incomingNodes)
            {
                var nextNodes = incomingNode.nextNodes;

                if (nextNodes != null)
                {
                    var formedList = new List<Node>();

                    foreach (var node in nextNodes)
                    {
                        var assignedNode = nodes.Where(n => n.Name == node).SingleOrDefault();

                        if (assignedNode == null)
                            throw new Exception("You cannot assign a node as a next node if it is not defined in configuration file.");

                        formedList.Add(assignedNode);
                    }

                    var neededNode = nodes.Where(n => n.Name == incomingNode.name).SingleOrDefault();

                    neededNode?.AddNextNodes(formedList);
                }              
            }

            // Add previous nodes
            foreach (var incomingNode in incomingNodes)
            {
                var nextNodes = incomingNode.nextNodes;

                if (nextNodes != null)
                {
                    var assignedNode = nodes.Where(n => n.Name == incomingNode.name).SingleOrDefault();

                    if (assignedNode != null)
                    {
                        foreach (var node in nextNodes)
                        {
                            var neededNode = nodes.Where(n => n.Name == node).SingleOrDefault();

                            neededNode?.AddPreviousNodes(assignedNode);
                        }
                    }                   
                }
            }

            return nodes;
        }
    }
}
