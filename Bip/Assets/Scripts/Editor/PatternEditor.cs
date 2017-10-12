using UnityEditor;
using UnityEngine;
using System.Collections;
using EasyEditor;

[Groups("Circle", "Triangle", "Square", "Barre")]
[CustomEditor(typeof(Pattern))]
public class PatternEditor : EasyEditorBase
{


    public override void OnInspectorGUI()
    {

        Pattern pattern = (Pattern)target;

        /*if (ite.useResourcesBags)
            ShowGroup("Resources Bags");

        else
            HideGroup("Resources Bags");

    */


        if (pattern.chooseRandomType)
        {
            HideRenderer("type"); 
        }
        else
        {
            ShowRenderer("type");
        }


        if (pattern.randomStartPosition)
        {
            HideRenderer("startPosition");
        }
        else
        {
            ShowRenderer("startPosition");
        }


        //HideRenderer("circleMovement");
        //HideRenderer("squareMovement");
        HideRenderer("halfCircleMovement");
        //HideRenderer("triangleMovement");
        //HideRenderer("barMovement");

        HideGroup("Square");
        HideGroup("Triangle");
        HideGroup("Circle");
        HideGroup("Barre");

        switch (pattern.objectToSpawn)
        {
            case Pattern.Type.Square:
                ShowGroup("Square");
                break;

            case Pattern.Type.Triangle:
                ShowGroup("Triangle");
                break;

            case Pattern.Type.Circle:
                ShowGroup("Circle");
                break;

            case Pattern.Type.Bar:
                ShowGroup("Barre");
                break;

            case Pattern.Type.HalfCircle:
                ShowRenderer("halfCircleMovement");
                break;

        }


        base.OnInspectorGUI();

    }
}