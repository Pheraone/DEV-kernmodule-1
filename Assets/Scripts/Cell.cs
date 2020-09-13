using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : ICell
{
    private GameObject _thisCell;
    public Coordinate Position { get; set; }
    private int _cost;

    public int Cost
    {
        get
        {
            return _cost ;
        }
        set
        {
            //set a colour to identify walkable(blue) and non-walkable(black) cells
            if (value > 0) _thisCell.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.5f);
            else 
            if (value == 0) _thisCell.GetComponent<SpriteRenderer>().color = new Color(0, 0, 1, 0.5f);
            else _thisCell.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.5f);

            _cost = value;
        }
    }

    public Cell(Coordinate coordinate, int cost)
    {
        Position = coordinate;
        _thisCell = new GameObject();
        _thisCell.transform.position = new Vector3(coordinate.x, coordinate.y, -0.1f);
        _thisCell.name = "Cell " + coordinate.x + ", " + coordinate.y;
        _thisCell.AddComponent<SpriteRenderer>();
        _thisCell.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Cell");
        Cost = cost;
    }
}