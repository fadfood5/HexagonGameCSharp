using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace HexagonGame{
	//Global variables
	public class Globals{ 
		public int currentPlayer;
	}

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

		public void changePlayer(Globals global){
			if (global.currentPlayer == 1) {
				global.currentPlayer = 2;
				Console.WriteLine ("Player changed to 2");
			} else {
				global.currentPlayer = 1;
				Console.WriteLine ("Player changed to 1");
			}
		}

		public void addEdge(Edge i, int player){

			//Create temp Edge Object
			Edge newEdge = new Edge(i.x, i.y, player);

			//Add edge to corresponding list
			if (player == 1)
				player1.Add (newEdge);
			else if (player == 2)
				player2.Add (newEdge);

			//Add edge to list of all edges
			allEdges.Add(newEdge);

			Console.WriteLine("Added edge to vertex: (" + i.x + ", " + i.y + ")");
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
		public int checkIfEdgeExists(Edge ed){
			foreach (Edge item in allEdges) {
				Console.WriteLine ("Edge: " + item.x + ", " + item.y);
				if ((item.x == ed.x && item.y == ed.y) || (item.x == ed.y && item.y == ed.x)) {
					Console.WriteLine ("Edge already exists! Make another move");
					return 0;
				}
			}
			Console.WriteLine ("Edge does not exist.");
			return 1;
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
		public bool checkIfLoss(Globals globalValue){
			foreach (Edge item in player2)
            {
                foreach (Edge item2 in player2)
                {
                    if (item2 != item && item.y == item2.x)
                    {
                        foreach(Edge item3 in player2)
                        {
                            if(item3 != item2 && item3 != item && item2.y == item3.x)
                            {
                                if (item3.y == item.x)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }

            }

            return false;
            
		}

		public static void Main(string[] args){
			Hexagon p = new Hexagon (6);
			for (int i = 1; i < 7; i++) {
				p.addVertex (i);
			}

			Console.WriteLine ("Which player starts first? (1 or 2)");
			int player = Console.Read();
			Globals global = new Globals ();
			if (player == 1)
				global.currentPlayer = 1;
			else if (player == 2)
				global.currentPlayer = 2;
			Edge ed = new Edge (0, 1, 1);
			p.addEdge (ed, 1);
			p.checkIfEdgeExists (ed);
			//p.printEdges ();
		}
	}
}
