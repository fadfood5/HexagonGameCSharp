using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace HexagonGame{
	//Global variables
	public class Globals{
        public int currentPlayer;
        public int AIPlayer;
        public List<Vertex> vertices;
        public List<Edge> allPossibleMoves;
        public List<Edge> allEdges;
        public List<Edge> HumanPlayer;
        public List<Edge> AIPlayerEdges;
    }

//    public static int currentPlayer = 2;

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
		List<Edge> HumanPlayer;
		List<Edge> AIPlayer;

		public Hexagon(int v){
			//Create new lists to keep track of edges
			vertices = new List<Vertex>();
			allEdges = new List<Edge>();
            HumanPlayer = new List<Edge>();
            AIPlayer = new List<Edge>();
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
                HumanPlayer.Add (newEdge);
			else if (player == 2)
                AIPlayer.Add (newEdge);

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
				foreach (Edge item in HumanPlayer) {
					Console.Write ("(" + item.x + ", " + item.y + ")");
					count++;
				}
			} else if (player == 2) {
				Console.Write ("Player 2: ");
				foreach (Edge item in AIPlayer) {
					Console.Write ("(" + item.x + ", " + item.y + ")");
					count++;
				}
			}
		}
		public bool checkIfLoss(Globals globalValue){
			foreach (Edge item in AIPlayer)
            {
                foreach (Edge item2 in AIPlayer)
                {
                    if (item2 != item && item.y == item2.x)
                    {
                        foreach(Edge item3 in AIPlayer)
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

        public static bool checkMoveLookahead(Edge possibleMove, Globals global)
        {
            foreach(Edge item in global.AIPlayerEdges)
            {
                foreach(Edge item2 in global.AIPlayerEdges)
                {
                    if(item2 != item)
                    {
                        if(item.y == item2.x && item2.y == possibleMove.x && possibleMove.y == item.x)
                        {
                            return true;
                        }
                        else if (item.x == item2.y && item2.x == possibleMove.y && possibleMove.x == item.y)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

            
        //uses lookahead function to determine if this is a good move to make
        public static void CalculateMove(int nodeOne, Globals global)
        {
            //at end, will contain all legal moves
            List<Edge> possibleMoves = new List<Edge>();
            int count = 0;
            foreach(Vertex vertex in global.vertices)
            {
                Edge possibleMove = new Edge(nodeOne, vertex.i, global.AIPlayer);
                foreach(Edge item in global.AIPlayerEdges)
                {
                    if(possibleMove == item)
                    {
                        count++;
                    }
                }
                if (count == 0)
                {
                    //if possible Move makes us lose, ignore it
                    if (!checkMoveLookahead(possibleMove, global))
                    {
                        possibleMoves.Add(possibleMove);
                    }
                }
            }
        }

        public static void AIPlayerMove(Globals global)
        {
            int firstNode = 0;
            int secondNode = 0;
            int count = 0;
            if(global.currentPlayer == global.AIPlayer)
            {
                foreach (Vertex vertex in global.vertices)
                {

                    foreach (Edge edgeItem in global.allEdges)
                    {
                        if (vertex.i == edgeItem.x || vertex.i == edgeItem.y)
                        {
                            count++;
                        }

                    }

                    if (count == 0)
                    {
                        if (firstNode == 0)
                        {
                            firstNode = vertex.i;
                        }
                        else
                            secondNode = vertex.i;

                    }

                    if(firstNode != 0 && secondNode != 0)
                    {
                        //make move with firstNode and secondNode
                        Edge edgeMove = new HexagonGame.Edge(firstNode, secondNode, global.AIPlayer);
                        global.allEdges.Add(edgeMove);
                        global.AIPlayerEdges.Add(edgeMove);
                    }

                    else if (firstNode != 0 || secondNode == 0 )
                    {
                        //choose a vertex we already picked, using lookahead function
                        CalculateMove(firstNode, global);
                    }


                    else if (firstNode == 0 && secondNode == 0)
                    {
                        //use lookahead function, find optimal move
                    }
                }
            }

        }

        public static void Main(string[] args){
			Hexagon p = new Hexagon (6);
            bool gameFinished = false;
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

            int currentPlayer = global.currentPlayer; 
            int AIPlayer = global.AIPlayer;

            while(!gameFinished)
            {
                //HumanPlayerMove();
                //switch currentPlayer;
                AIPlayerMove(global);
            }
			//p.printEdges ();
		}



    }
}
