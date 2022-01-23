using System.Collections.Generic;

namespace Utils
{
    public class RoomGraphFactory
    {
        // steps
        // 1. init all n vertices
        // 2. each should contain [1, 4] neighbours

        // static public Graph<T> GenerateGraph(int nodes)
        // {
        //     var rand = new Random();
        //
        //
        //     var vertices = new Graph<T>.Vertex[nodes];
        //     for (var i = 0; i < nodes; i++)
        //     {
        //         vertices[i] = new Graph.Vertex();
        //     }
        //
        //     var graph = new Graph(vertices[0]);
        //
        //     for (var i = 0; i < nodes; i++)
        //     {
        //         var vertex = vertices[i];
        //         var maxNeighboursToAdd = 4 - vertex.GetNeightourCount();
        //         if (maxNeighboursToAdd > 0)
        //         {
        //             // add neighbours
        //             var neighboursToAdd = rand.Next(2, maxNeighboursToAdd);
        //             vertex.Neighbours.AddRange(Array.FindAll(vertices, e => e.GetNeightourCount() < 4).Take(neighboursToAdd));
        //         }
        //
        //     }
        //     return graph;
        // }


        public static Graph<Room> CircularGraph(List<Room> objects)
        {
            var nodes = objects.Count;
            var vertices = new Room[nodes];
            for (var i = 0; i < nodes; i++)
            {
                vertices[i] = objects[i];
            }

            var graph = new Graph<Room>(vertices[0]);
            vertices[0].Neighbours.Add(vertices[1]);

            for (var i = 1; i < nodes - 1; i++)
            {
                vertices[i].Neighbours.Add(vertices[i + 1]);
                vertices[i+1].Neighbours.Add(vertices[i]);
            }

            vertices[nodes - 1].Neighbours.Add(vertices[1]);
            vertices[1].Neighbours.Add(vertices[nodes-1]);
            return graph;
        }
    }
}
