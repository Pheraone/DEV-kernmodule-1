using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Coordinate
{
    public int _x, _y;

    public Coordinate(int x, int y)
    {
        this._x = x;
        this._y = y;
    }

    public static Coordinate operator +(Coordinate a, Coordinate b)
    {
        a._x += b._x;
        a._y += b._y;
        return a;
    }

    public static Coordinate operator -(Coordinate a, Coordinate b)
    {
        a._x -= b._x;
        a._y -= b._y;
        return a;
    }

    public static Coordinate operator *(Coordinate a, int b)
    {
        a._x *= b;
        a._y *= b;
        return a;
    }

    public static bool operator !=(Coordinate a, Coordinate b)
    {
        if (a._x == b._x && a._y == b._y)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public static bool operator ==(Coordinate a, Coordinate b)
    {
        if (a._x == b._x && a._y == b._y)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
