using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cell_v1 : MonoBehaviour
{

}

public interface ICell
{
    Coordinate _coordinate { get; set; }
    string _name { get; set; }
}