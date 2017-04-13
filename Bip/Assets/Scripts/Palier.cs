using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Palier
{
	public int IterationsBeforeNextPalier = 1;

    public List<Iteration> iterations = new List<Iteration>();
}




[System.Serializable]
public class EntityToCreate
{
    public bool typeChosenToEquilibrate = false;
    public EntityScript.Type type;

    public float additionalSpeed = 0;

}
