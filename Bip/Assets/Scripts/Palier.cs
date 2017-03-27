using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Palier {

    public float timeBeforeNext;

    public float globalSize = 1;
	//public bool changeSpeed;
	public float speedChange = 0;

    public List<EntityToCreate> entitiesToSpawn = new List<EntityToCreate>();


}

[System.Serializable]
public class EntityToCreate
{
    public bool typeChosenToEquilibrate = false;
    public EntityScript.Type type;

    public float additionalSpeed;

    //public bool changeSize;
    //public float size;
}
