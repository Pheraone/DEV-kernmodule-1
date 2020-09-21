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

    public static Coordinate TurnRight(Coordinate a)
    {
        if (a._x == 0)
        {
            a._x = a._y;
            a._y = 0;
        }
        else
        {
            a._y = -a._x;
            a._x = 0;
        }
        return a;
    }

    public static Coordinate TurnLeft(Coordinate a)
    {
        if (a._x == 0)
        {
            a._x = -a._y;
            a._y = 0;
        }
        else
        {
            a._y = a._x;
            a._x = 0;
        }
        return a;
    }

    public static Vector2 ToVector2(Coordinate a)
    {
        Vector2 newVector2 = new Vector2(a._x, a._y);
        return newVector2;
    }
}
