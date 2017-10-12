using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyEditor;

[System.Serializable]
public class Pattern : ScriptableObject
{
    public enum Type
    {
        Circle,
        Bar,
        Triangle,
        Square,
        HalfCircle
    }
    
    public Type objectToSpawn;

    public bool chooseRandomType = false;
    public EntityScript.Type type;



    //public PatternCircle circleMovement;
    //public PatternSquare squareMovement;
    //public PatternHalfCircle halfCircleMovement;
    //public PatternTriangle triangleMovement;
    //public PatternBar barMovement;


    public bool randomStartPosition;
    public Vector3 startPosition;


    [Inspector(group = "Circle")]
    public PatternCircle circleMovement;
    public float circleRayonMax;
    public float circleRayonMin;
    public float circleBreathSpeed; //vitesse de respiration des entités
    public float circleRotationSpeed; //vitesse de rotation des entités
    public int circleEntityNb;


    [Inspector(group = "Triangle")]
    public TriangleSizes triangleSize;
    public PatternTriangle triangleMovement;
    public float triangleSpeed;


    [Inspector(group = "Square")]
    public SquareSizes squareSize;
    public PatternSquare squareMovement;
    public float squareSpeed;

    [Inspector(group = "Barre")]
    public PatternBar barMovement;
    public float barreSpeed;



}

public enum PatternCircle
{
    Rotation,
    Other
}

public enum PatternSquare
{
    RandomToCenter,
    LineToCenter,
    TwoMoves
}

public enum PatternTriangle
{
    Line,
    Arc
}

public enum PatternBar
{
    Move,
    Slide
}

public enum PatternHalfCircle
{
    Rotation,
    Movement
}

public enum SquareSizes
{
    s2x2,
    s3x3,
    s5x5
}

public enum TriangleSizes
{
    s2,
    s3,
    s4
}