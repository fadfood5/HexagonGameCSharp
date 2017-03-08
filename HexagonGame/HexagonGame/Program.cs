using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace HexagonGame{
	public class Edge{
		public Vertex x;
		public Vertex y;
		public int z;

		public Edge(Vertex a, Vertex b, int player){
			x = a;
			y = b;
			z = player;
		}
	}

	public class Vertex{
		public int i;

		public Vertex(int a){
			i = a;
		}
	}

	public class Hexagon{
		//Instantiate lists
		List<Vertex> vertices;
		List<Edge> allEdges;
		List<Edge> player1;
		List<Edge> player2;

		public Hexagon(int v){
			//Create new lists to keep track of edges
			vertices = new List<Vertex>();
			allEdges = new List<Edge>();
			player1 = new List<Edge>();
			player2 = new List<Edge>();
		}

		public void addEdge(Vertex i, Vertex j, int player){

			//Create temp Edge Object
			Edge newEdge = new Edge(i, j, player);

			//Add edge to corresponding list
			if (player == 1)
				player1.Add (newEdge);
			else if (player == 2)
				player2.Add (newEdge);

			//Add edge to list of all edges
			allEdges.Add(newEdge);

			Console.WriteLine("Added edge to vertex: (" + i + ", " + j + ")");
		}

		public void addVertex(int i){

			//Create temp Edge Object
			Vertex newVertex = new Vertex(i);

			//Add vertex to list of all vertices
			vertices.Add(newVertex);

			Console.WriteLine("Added vertex: " + i);
		}

		public void printEdges(){
			int count = 0;
			foreach(Edge item in allEdges){
				Console.Write ("(" + item.x + ", " + item.y + ")");
				count++;
			}
		}
		public void printEdges(int player){
			int count = 0;
			if (player == 1) {
				Console.Write ("Player 1: ");
				foreach (Edge item in player1) {
					Console.Write ("(" + item.x + ", " + item.y + ")");
					count++;
				}
			} else if (player == 2) {
				Console.Write ("Player 2: ");
				foreach (Edge item in player2) {
					Console.Write ("(" + item.x + ", " + item.y + ")");
					count++;
				}
			}
		}

		public static void Main(string[] args){
			Hexagon p = new Hexagon (6);
			for (int i = 1; i < 7; i++) {
				p.addVertex (i);
			}
			//p.printEdges ();

		}
	}
}
