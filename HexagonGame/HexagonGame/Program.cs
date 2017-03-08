using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace HexagonGame{
	public class Edge{
		public int x;
		public int y;
		public int z;

		public Edge(int a, int b, int player){
			x = a;
			y = b;
			z = player;
		}
	}

	public class Hexagon{
		//Instantiate lists
		List<Edge> allEdges;
		List<Edge> player1;
		List<Edge> player2;

		public Hexagon(int v){
			//Create new lists to keep track of edges
			allEdges = new List<Edge>();
			player1 = new List<Edge>();
			player2 = new List<Edge>();
		}

		public void addEdge(int i, int j, int player){

			//Create temp Edge Object
			Edge newEdge = new Edge(i, j, player);

			//Add edge to corresponding list
			if (player == 1)
				player1.Add (newEdge);
			else if (player2 == 2)
				player2.Add (newEdge);

			//Add edge to list of all edges
			allEdges.Add(newEdge);

			Console.WriteLine("Added edge to vertex: (" + i + ", " + j + ") with weight " + wgt);
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
				
			}
			//p.printEdges ();

		}
	}
}
