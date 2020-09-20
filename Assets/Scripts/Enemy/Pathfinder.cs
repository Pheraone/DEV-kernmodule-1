using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Pathfinder
{
    //Openlist
    //ClosedList

    //LOOP:
    //currentnode = Node in open with the lowest FCost
    //remove current from open
    //add current to closed

    //if(currentNode == targetnode)
    //{
        //return
    //}

    //forech(neighbour of the currentNode)
    //{
        //if(neighbour is not taversable || neighbour is in closedList)
            //{
                //skip to next neighbour
            //}

        //if(new path to neighbour is shorter || neighbour is not in openList)
            //{
                //set fCost of neighbour
                //set parent of neighbour to current
            //}
        //if(neighbour is not in openList)
            //{
                //add neigbour to open
            //}
    //}

}
