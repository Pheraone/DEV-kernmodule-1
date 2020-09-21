using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

//public class Pathfinder
//{
//    //Openlist
//    private  List<PathNode> _openList;
//    //ClosedList
//    private  List<PathNode> _closedList;

//    private List<Coordinate> _pathList;
//    public Pathfinder(List<Coordinate> listPara)
//    {
//        _pathList = listPara;
//    }

//    public void FindPath(Vector2 startPosition, Vector2 target)
//    {
//        _openList = new List<PathNode>();
//        _closedList = new List<PathNode>();

//        PathNode currentNode;

//        //!instantiated position? StartNode
//        PathNode startNode = new PathNode { position = startPosition, gCost = 0f, hCost = 0f, fCost = 0f };
//        currentNode = startNode;
//        Debug.Log(currentNode.position);
//        while(currentNode.position != target)
//        {
//            float fcostTemp = 0;
//            foreach(PathNode openPathNode in _closedList)
//            {
//                openPathNode.fCost = Vector2.Distance(openPathNode.position, startPosition) + Vector2.Distance(openPathNode.position, target);
//                if(fcostTemp == 0 || openPathNode.fCost < fcostTemp)
//                {
//                    currentNode = openPathNode;
//                    fcostTemp = openPathNode.fCost;
//                }
//            }

//            //add current to the closed list
//            _openList.Remove(currentNode);
//            _closedList.Add(currentNode);


//            //piece of yikes code
//            List<PathNode> neighbours;
//            neighbours = new List<PathNode>();
//            neighbours.Add(new PathNode { position = new Vector2(currentNode.position.x, currentNode.position.y - 1), gCost = 0f, hCost = 0f, fCost = 0f });
//            neighbours.Add(new PathNode { position = new Vector2(currentNode.position.x, currentNode.position.y + 1), gCost = 0f, hCost = 0f, fCost = 0f });
//            neighbours.Add(new PathNode { position = new Vector2(currentNode.position.x - 1, currentNode.position.y), gCost = 0f, hCost = 0f, fCost = 0f });
//            neighbours.Add(new PathNode { position = new Vector2(currentNode.position.x + 1, currentNode.position.y), gCost = 0f, hCost = 0f, fCost = 0f });
           

//            foreach(PathNode neighbour in neighbours)
//            {
//                neighbour.gCost = Vector2.Distance(neighbour.position, startPosition);
//                neighbour.hCost = Vector2.Distance(neighbour.position, target);

//                //Why?
//                List<Vector2> closedPoss = new List<Vector2>();
//                foreach (PathNode closednodeTemp in _closedList)
//                {
//                    closedPoss.Add(closednodeTemp.position);
//                }

//                //TODO: rewrite this piece of code.
//                //if (Physics2D.OverlapPoint(neighbour.pos, TileLayer) == null || Physics2D.OverlapPoint(neighbour.pos, TileLayer).gameObject == null || closedPoss.Contains(neighbour.pos) || Physics2D.OverlapPoint(neighbour.pos, TileLayer).gameObject.tag != "Walkable")
//                //{
//                //    continue;
//                //}

//                //TODO: check if this is the same as the code above.
//                Coordinate cPos= new Coordinate((int)neighbour.position.x, (int)neighbour.position.y);
//                if (_pathList.Contains(neighbour.cPos) == false || closedPoss.Contains(neighbour.position) == true)
//                {
//                    continue;
//                }

//                List<Vector2> openPoss = new List<Vector2>();
//                foreach (PathNode openNodeTemp in _openList)
//                {
//                    closedPoss.Add(openNodeTemp.position);
//                }

//                if (!openPoss.Contains(neighbour.position))
//                {
//                    //if (neighbour.myTransform == null)
//                    //{
//                    //    neighbour.myTransform = Instantiate(nodePrefab, neighbour.pos, new Quaternion(0, 0, 0, 0)).transform;
//                    //}

//                    neighbour.fCost = Vector2.Distance(neighbour.position, target) + Vector2.Distance(neighbour.position, startPosition);
//                    //neighbour.MyTransform.SetParent(currentNode.MyTransform);
//                    _openList.Add(neighbour);
//                }
//            }

//            if(currentNode.position == target)
//            {
//                foreach(PathNode opens in _openList)
//                {
//                   // Destroy(opens.myTransform.gameObject);
//                }

//                foreach (PathNode closedOnes in _closedList)
//                {
//                    if (closedOnes == currentNode /*|| currentNode.myTransform.IsChildOf(closedOnes.myTransform)*/)
//                    {
//                        continue;
//                    }
//                    //Destroy(closedOnes.myTransform.gameObject);
//                }
//                break;
//            }
//        }
//        //return currentNode.MyTransform; 

//    }
//    //Linked list ipv de transforms...?

//}


////LOOP:
////currentnode = Node in open with the lowest FCost
////+remove current from open
////+add current to closed

////if(currentNode == targetnode)
////{
////return
////}

////+forech(neighbour of the currentNode)
////{
////if(neighbour is not taversable || neighbour is in closedList)
////{
////skip to next neighbour
////}

////if(new path to neighbour is shorter || neighbour is not in openList)
////{
////set fCost of neighbour
////set parent of neighbour to current
////}
////if(neighbour is not in openList)
////{
////add neigbour to open
////}
////}
