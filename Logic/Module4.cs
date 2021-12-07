using MathModMudules.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathModMudules.Logic
{
    class Module4 : BasicVM
    {
        private List<int> selectionList;
        private Graph graph;
        private string output;

        private int selectedItem;
        private int selectedItem2;
        public List<int> SelectionList { get => selectionList; set => SetField(ref selectionList, value); }
        public string Output { get => output; set => SetField(ref output, value); }
        public int SelectedItem { get => selectedItem; set { SetField(ref selectedItem, value); Output = SearchAlgo.dijkstra(graph.GetMatrix(), value - 1, selectedItem2 - 1); } }
        public int SelectedItem2 { get => selectedItem2; set { SetField(ref selectedItem2, value); Output = SearchAlgo.dijkstra(graph.GetMatrix(), selectedItem - 1, value - 1); } }


        public Module4()
        {
            selectedItem = 1;
            selectedItem2 = 1;
            graph = new Graph();

            SelectionList = new List<int>() {1,2,3,4,5,6,7,8,9,10};


            var vertex0 = new Vertex(0);
            var vertex1 = new Vertex(1);
            var vertex2 = new Vertex(2);
            var vertex3 = new Vertex(3);
            var vertex4 = new Vertex(4);
            var vertex5 = new Vertex(5);
            var vertex6 = new Vertex(6);
            var vertex7 = new Vertex(7);
            var vertex8 = new Vertex(8);
            var vertex9 = new Vertex(9);

            graph.AddEdgeUndirected(vertex0, vertex1, 8);
            graph.AddEdgeUndirected(vertex0, vertex2, 8);
            graph.AddEdgeUndirected(vertex0, vertex4, 19);

            graph.AddEdgeUndirected(vertex1, vertex3, 4);
            graph.AddEdgeUndirected(vertex1, vertex4, 9);

            graph.AddEdgeUndirected(vertex2, vertex4, 4);
            graph.AddEdgeUndirected(vertex2, vertex5, 12);

            graph.AddEdgeUndirected(vertex3, vertex6, 12);
            graph.AddEdgeUndirected(vertex3, vertex4, 4);

            graph.AddEdgeUndirected(vertex4, vertex5, 5);
            graph.AddEdgeUndirected(vertex4, vertex6, 3);
            graph.AddEdgeUndirected(vertex4, vertex8, 12);
            graph.AddEdgeUndirected(vertex4, vertex7, 4);

            graph.AddEdgeUndirected(vertex5, vertex7, 11);

            graph.AddEdgeUndirected(vertex6, vertex8, 11);

            graph.AddEdgeUndirected(vertex7, vertex8, 5);
            graph.AddEdgeUndirected(vertex7, vertex9, 7);
            graph.AddEdgeUndirected(vertex8, vertex9, 3);

            Output = SearchAlgo.dijkstra(graph.GetMatrix(), SelectedItem, SelectedItem2);
        }

    }

}
