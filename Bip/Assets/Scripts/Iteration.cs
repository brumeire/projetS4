using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Iteration : ScriptableObject
{
    public float timeBeforeNextIteration = 10;

    public bool skipToNextPalier = false;

    public bool loadLast = false;

    public List<GameObject> patternsToSpawn = new List<GameObject>();

}
