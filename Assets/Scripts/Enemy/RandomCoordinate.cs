using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCoordinate
{
    public ILevelGenerator _ILevelGenerator;
    List<Coordinate> _path;
    public Coordinate _randCor = new Coordinate();
    public RandomCoordinate(ILevelGenerator para)
    {
        _ILevelGenerator = para;
    }

    public Coordinate GetRandomCoordinate()
    { 
        //FIXME does not return coordinate at the moment.
        _path = _ILevelGenerator.Path;
        _randCor = _path[Random.Range(0, _path.Count - 1)];
        Debug.Log("Random coordinaat is"+ _randCor._x + " " + _randCor._y );
        return _randCor;
    } 

}
