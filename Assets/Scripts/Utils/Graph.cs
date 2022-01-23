namespace Utils
{
    public class Graph<T>
    {

        public Graph(T root) => Root = root;

        public T Root { get; private set; }

        // public class Vertex
        // {
        //     public Room Room { get; set; }
        //
        //     public List<Vertex> Neighbours { get; private set; }
        //
        //     public Vertex(List<Vertex> neighbours)
        //     {
        //         Neighbours = neighbours;
        //     }
        //     public Vertex()
        //     {
        //         Neighbours = new List<Vertex>();
        //     }
        //
        //
        //     public void AddNeighbour(Vertex node)
        //     {
        //         Neighbours.Add(node);
        //     }
        //
        //     public int GetNeightourCount()
        //     {
        //         return Neighbours.Count;
        //     }
        //
        // }


    }
}
