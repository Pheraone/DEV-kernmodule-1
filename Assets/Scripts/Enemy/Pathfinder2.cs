using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Mathematics;
using UnityEngine;

public class Pathfinder2

{
    private const int _straightMoveCost = 10;
    private const int _diagonalMoveCost = 14;

    //Openlist
    private List<PathNode> _openList;
    //ClosedList
    private List<PathNode> _closedList;

    public ILevelGenerator _iLevelGenerator;

    private PathNode _pathNode = new PathNode();

    

    private PathNode _startNode = new PathNode();
    private PathNode _endNode = new PathNode();
    private PathNode _current = new PathNode();


    public Pathfinder2(ILevelGenerator para)
    {
        _iLevelGenerator = para;
        //_pathNode.coordinateNode = pathCoordinate;
        
    }



    public List<PathNode> FindPath(Coordinate start, Coordinate end)
    { 

        _startNode.coordinateNode = start;
        _endNode.coordinateNode = end;

        _openList = new List<PathNode>();
        _closedList = new List<PathNode>();

        
        foreach(Coordinate pathCoordinate in _iLevelGenerator.Path)
        {
            _pathNode.coordinateNode = pathCoordinate;
            _pathNode.gCost = int.MaxValue;
            _pathNode.CalculateFCost();
            _pathNode.cameFromNode = null;
        }

        _startNode.gCost = 0;
        _startNode.hCost = CalculateDistanceCost(_startNode, _endNode);
        _startNode.CalculateFCost();

        while (_openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostNode(_openList);
            if (currentNode == _endNode)
            {
                //reach final node
                return CalculatePath(_endNode);
            }

            //currentNode had been checked so we move it to the list of calculated posibilities.
            _openList.Remove(currentNode);
            _closedList.Add(currentNode);

            foreach(PathNode neighbourNode in GetNeighbourList(currentNode))
            {
                if (_closedList.Contains(neighbourNode)) continue;

                int tentativeCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
                if(tentativeCost < neighbourNode.gCost)
                {
                    neighbourNode.cameFromNode = currentNode;
                    neighbourNode.gCost = tentativeCost;
                    neighbourNode.hCost = CalculateDistanceCost(neighbourNode, _endNode);
                    neighbourNode.CalculateFCost();

                    if (!_openList.Contains(neighbourNode))
                    {
                        _openList.Add(neighbourNode);
                    }
                }
            }

        }

        return null;

    }

    private List<PathNode> GetNeighbourList(PathNode currentNode)
    {
        
        List<PathNode> neighbourList = new List<PathNode>();
        {
           
            PathNode tempNode = new PathNode();
            foreach (Coordinate direction in Direction.DirectionVectors)
            {
                tempNode.coordinateNode = currentNode.coordinateNode + direction;
                neighbourList.Add(tempNode);
            }

        }
        return neighbourList;
    }


    private List<PathNode> CalculatePath(PathNode _endNode)
    {
        List<PathNode> path = new List<PathNode>();
        path.Add(_endNode);
        PathNode currentNode = _endNode;
        while(currentNode.cameFromNode != null)
        {
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
        }
        
        path.Reverse();
        Debug.Log(path);
        return path;
    }

    private int CalculateDistanceCost(PathNode a, PathNode b)
    {
        int xDistance = Mathf.Abs(a.coordinateNode._x - b.coordinateNode._x);
        int yDistance = Mathf.Abs(a.coordinateNode._y - b.coordinateNode._y);
        int remaining = Mathf.Abs(xDistance - yDistance);

        return _diagonalMoveCost * Mathf.Min(xDistance - yDistance ) + _straightMoveCost * remaining;
    }

    private PathNode GetLowestFCostNode(List<PathNode> pathNodeList)
    {
        PathNode lowestFCostNode = pathNodeList[0];
        for (int i = 1; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = pathNodeList[i];
            }
        }
        return lowestFCostNode;
    }
}
