using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    ILevelGenerator _levelGeneration;

    // Start is called before the first frame update
    void Start()
    {
        _levelGeneration = new LevelGeneration() as ILevelGenerator;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
