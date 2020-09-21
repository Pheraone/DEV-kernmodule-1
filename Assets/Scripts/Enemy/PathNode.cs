
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

//public class PathNode
//{
//    public Vector2 position { get; set; }
//    public float gCost { get; set; }
//    public float hCost { get; set; }
//    public float fCost { get; set; }
//    //not sure about the cPos coordinate...
//    public Coordinate cPos { get; internal set; }
//    //public Transform mytransform {get; set;}
//}

public class PathNode
{ 
    public int gCost;
    public int hCost;
    public int fCost;

    public Coordinate coordinateNode;
    

    public PathNode cameFromNode;
    public PathNode()
    {

    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

    
}
