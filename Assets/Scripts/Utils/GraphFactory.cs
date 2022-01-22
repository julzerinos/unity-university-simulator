using System;
using System.Linq;

public class GraphFactory
{

    // steps
    // 1. init all n vertices
    // 2. each should contain [1, 4] neighbours

    static public Graph GenerateGraph(int nodes)
    {
        Random rand = new Random();

        var vertices = new Graph.Vertex[nodes];
        for (int i = 0; i < nodes; i++)
        {
            vertices[i] = new Graph.Vertex();
        }

        Graph graph = new Graph(vertices[0]);

        for (int i = 0; i < nodes; i++)
        {
            var vertex = vertices[i];
            var maxNeighboursToAdd = 4 - vertex.GetNeightourCount();
            if (maxNeighboursToAdd > 0)
            {
                // add neighbours
                int neighboursToAdd = rand.Next(1, maxNeighboursToAdd);
                vertex.Neighbours.AddRange(Array.FindAll(vertices, e => e.GetNeightourCount() < 4).Take(neighboursToAdd));
            }

        }
        return graph;
    }
}
