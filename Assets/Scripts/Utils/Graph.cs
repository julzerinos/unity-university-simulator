using System;
using System.Collections;
using System.Collections.Generic;

public class Graph
{

    public Graph(Vertex root) => this.Root = root;

    public Vertex Root { get; private set; }

    public class Vertex
    {

        public List<Vertex> Neighbours { get; private set; }

        public Vertex(List<Vertex> neighbours)
        {
            this.Neighbours = neighbours;
        }
        public Vertex()
        {
            this.Neighbours = new List<Vertex>();
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
