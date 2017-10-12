using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Palier
{
	public int IterationsBeforeNextPalier = 1;

    public bool loopPalier = false;

    public List<Iteration> iterations = new List<Iteration>();
}



public enum MovementTypes
{
    Rotation,
    Linéaire
}


