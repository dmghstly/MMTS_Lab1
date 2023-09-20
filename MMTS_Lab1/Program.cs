using MMTS_Lab1.Modules;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;

using (StreamReader r = new StreamReader("GraphConfiguration.json"))
{
    var json = r.ReadToEnd();
    JObject incomingObject = JObject.Parse(json);

    try
    {
        var incomingNodes = incomingObject["nodes"].ToObject<List<JsonNode>>();

        var nodes = GraphFormatter.FormatGraph(incomingNodes);
        var decomposedNodes = GraphDecomposer.DecomposeGraph(nodes);

        GraphDecomposer.PrintDecomposedGrpah(decomposedNodes);
    }
    catch
    {
        throw new Exception("Some problems during retrieving nodes accured. Check if your configuration file is correctly initiated. All nodes should be of integer value.");
    };
}

public class JsonNode
{
    public int name { get; set; }
    public List<int>? nextNodes { get; set; }
}
