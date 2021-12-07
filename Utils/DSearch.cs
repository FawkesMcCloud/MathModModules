
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathModMudules.Utils
{
    public class Edge
    {
        public Edge(Vertex source, Vertex destination, int weight)
        {
            Source = source;
            Destination = destination;
            Weight = weight;
        }

        public int Weight { get; set; }
        public Vertex Source { get; set; }
        public Vertex Destination { get; set; }
    }

    public class Vertex
    {
        public Vertex(int key)
        {
            Key = key;
        }

        public int Key { get; set; }
    }

    class Node
    {
        int vertex, weight;

        public Node(int vertex, int weight)
        {
            this.vertex = vertex;
            this.weight = weight;
        }

        public int Vertex { get => vertex; set => vertex = value; }
        public int Weight { get => weight; set => weight = value; }
    }

    public class SearchAlgo{
    private static readonly int NO_PARENT = -1;

    public static string dijkstra(int[,] adjacencyMatrix, int startVertex, int endVertex)
    {
        int nVertices = adjacencyMatrix.GetLength(0);


        int[] shortestDistances = new int[nVertices];
        bool[] added = new bool[nVertices];

        for (int vertexIndex = 0; vertexIndex < nVertices;
                                            vertexIndex++)
        {
            shortestDistances[vertexIndex] = int.MaxValue;
            added[vertexIndex] = false;
        }

        shortestDistances[startVertex] = 0;

        int[] parents = new int[nVertices];

        parents[startVertex] = NO_PARENT;

        for (int i = 1; i < nVertices; i++)
        {

            int nearestVertex = -1;
            int shortestDistance = int.MaxValue;
            for (int vertexIndex = 0;
                    vertexIndex < nVertices;
                    vertexIndex++)
            {
                if (!added[vertexIndex] &&
                    shortestDistances[vertexIndex] <
                    shortestDistance)
                {
                    nearestVertex = vertexIndex;
                    shortestDistance = shortestDistances[vertexIndex];
                }
            }

            added[nearestVertex] = true;


            for (int vertexIndex = 0;
                    vertexIndex < nVertices;
                    vertexIndex++)
            {
                int edgeDistance = adjacencyMatrix[nearestVertex, vertexIndex];

                if (edgeDistance > 0
                    && ((shortestDistance + edgeDistance) <
                        shortestDistances[vertexIndex]))
                {
                    parents[vertexIndex] = nearestVertex;
                    shortestDistances[vertexIndex] = shortestDistance +
                                                    edgeDistance;
                }
            }
        }

            if (startVertex == endVertex)
                return $"{startVertex +1} -> {endVertex +1} Длина: 0";
            else
                return printSolution(startVertex, shortestDistances, parents, endVertex);

    }
    private static string printSolution(int startVertex, int[] distances, int[] parents, int endVertex)
    {
        int nVertices = distances.Length;
        return $"{startVertex +1} -> {endVertex + 1} Длина:{distances[endVertex]} Путь:{printPath(endVertex,parents)} ";
    }


    private static string printPath(int currentVertex,
                                int[] parents)
    {
        if (currentVertex == NO_PARENT)
            return "";
        return printPath(parents[currentVertex], parents) + $"{currentVertex + 1} ";
    }

}

public class Graph
    {
        private Dictionary<Vertex, List<Edge>> adjList;

        public Graph()
        {
            adjList = new Dictionary<Vertex, List<Edge>>();
        }

        public Dictionary<Vertex, List<Edge>> AdjList
        {
            get
            {
                return adjList;
            }
        }
        public void AddEdgeUndirected(Vertex source, Vertex destination, int weight)
        {
            if (adjList.ContainsKey(source))
            {
                adjList[source].Add(new Edge(source, destination, weight));
            }
            else
            {
                adjList.Add(source, new List<Edge> { new Edge(source, destination, weight) });
            }

            if (adjList.ContainsKey(destination))
            {
                adjList[destination].Add(new Edge(destination, source, weight));
            }
            else
            {
                adjList.Add(destination, new List<Edge> { new Edge(destination, source, weight) });
            }
        }

        int minDistance(int[] dist, bool[] sptSet)
        {
            int min = int.MaxValue, min_index = -1;

            for (int v = 0; v < dist.GetLength(0); v++)
                if (sptSet[v] == false && dist[v] <= min)
                {
                    min = dist[v];
                    min_index = v;
                }

            return min_index;
        }

        void dijkstra(int[,] graph, int src)
        {
            int size = graph.GetLength(0);

            int[] dist = new int[size];

            bool[] sptSet = new bool[size];

            for (int i = 0; i < size; i++)
            {
                dist[i] = int.MaxValue;
                sptSet[i] = false;
            }
            dist[src] = 0;

            for (int count = 0; count < size - 1; count++)
            {
                int u = minDistance(dist, sptSet);

                sptSet[u] = true;


                for (int v = 0; v < size; v++)
                    if (!sptSet[v] && graph[u, v] != 0 && dist[u] != int.MaxValue && dist[u] + graph[u, v] < dist[v])
                        dist[v] = dist[u] + graph[u, v];
            }

        }


        public int[,] GetMatrix()
        {
            int[,] matrix = new int[adjList.Count, adjList.Count];
            foreach (var v in adjList)
                foreach (var e in v.Value)
                {
                    matrix[e.Source.Key, e.Destination.Key] = e.Weight;
                    matrix[e.Destination.Key, e.Source.Key] = e.Weight;
                }
                    


            return matrix;
        }

        public void Print()
        {
            foreach (var v in adjList)
            {
                Console.WriteLine($"Node {v.Key.Key}:");
                foreach (var e in v.Value)
                {
                    Console.WriteLine($"\t\t to {e.Destination.Key}");
                }
            }
                
                   
        }
    }

    

}
