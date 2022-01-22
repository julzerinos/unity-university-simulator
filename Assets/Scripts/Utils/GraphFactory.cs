using System;
using System.Linq;

public class GraphFactory
{

    // steps
    // 1. init all n vertices
    // 2. each should contain [1, 4] neighbours

    static public Graph GenerateGraph(int nodes)
    {
        var rand = new Random();


        var vertices = new Graph.Vertex[nodes];
        for (var i = 0; i < nodes; i++)
        {
            vertices[i] = new Graph.Vertex();
        }

        var graph = new Graph(vertices[0]);

        for (var i = 0; i < nodes; i++)
        {
            var vertex = vertices[i];
            var maxNeighboursToAdd = 4 - vertex.GetNeightourCount();
            if (maxNeighboursToAdd > 0)
            {
                // add neighbours
                var neighboursToAdd = rand.Next(2, maxNeighboursToAdd);
                vertex.Neighbours.AddRange(Array.FindAll(vertices, e => e.GetNeightourCount() < 4).Take(neighboursToAdd));
            }

        }
        return graph;
    }


    static public Graph CircularGraph(int nodes)
    {
        var vertices = new Graph.Vertex[nodes];
        for (var i = 0; i < nodes; i++)
        {
            vertices[i] = new Graph.Vertex();
        }
        var graph = new Graph(vertices[0]);

        for (var i = 0; i < nodes - 1; i++)
        {
            vertices[i].AddNeighbour(vertices[i + 1]);
        }
        vertices[nodes - 1].AddNeighbour(vertices[0]);
        return graph;
    }



}
