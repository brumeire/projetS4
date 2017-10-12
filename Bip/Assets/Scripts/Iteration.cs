using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Iteration
{
    public float timeBeforeNextIteration = 10;

    public bool skipToNextPalier = false;

    public bool loadLast = false;

    public List<Pattern> patternsToSpawn = new List<Pattern>();

}
