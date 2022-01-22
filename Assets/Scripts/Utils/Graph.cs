using System;
using System.Collections;
using System.Collections.Generic;

public class Graph
{

    public Graph(Vertex root) => Root = root;

    public Vertex Root { get; private set; }

    public class Vertex
    {
        public Room room;
        
        public List<Vertex> Neighbours { get; private set; }

        public Vertex(List<Vertex> neighbours)
        {
            Neighbours = neighbours;
        }
        public Vertex()
        {
            Neighbours = new List<Vertex>();
        }


        public void AddNeighbour(Vertex node)
        {
            Neighbours.Add(node);
        }

        public int GetNeightourCount()
        {
            return Neighbours.Count;
        }

    }


}
