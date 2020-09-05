using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Coordinate
{
    public int x, y;

    public Coordinate(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public static Coordinate operator +(Coordinate a, Coordinate b)
    {
        a.x += b.x;
        a.y += b.y;
        return a;
    }

    public static Coordinate operator -(Coordinate a, Coordinate b)
    {
        a.x -= b.x;
        a.y -= b.y;
        return a;
    }

    public static Coordinate operator *(Coordinate a, int b)
    {
        a.x *= b;
        a.y *= b;
        return a;
    }

    public static bool operator !=(Coordinate a, Coordinate b)
    {
        if (a.x == b.x && a.y == b.y)
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
        if (a.x == b.x && a.y == b.y)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
