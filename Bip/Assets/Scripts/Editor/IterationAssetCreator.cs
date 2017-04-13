using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class IterationAssetCreator
{
    [MenuItem("Assets/Create/New Iteration Object")]
    public static void CreateAsset()
    {
        Iteration newIteration = ScriptableObject.CreateInstance<Iteration>();

        newIteration.timeBeforeNextIteration = 10;

        newIteration.skipToNextPalier = false;


        AssetDatabase.CreateAsset(newIteration, AssetDatabase.GetAssetPath(Selection.activeObject) + "/New Iteration.asset");
        AssetDatabase.SaveAssets();

        Selection.activeObject = newIteration;
    }



}


public class CreatePattern
{

    [MenuItem("GameObject/Patterns/Circle", false, 0)]
    public static void Circle()
    {
        GameObject go = Object.Instantiate<GameObject>(AssetDatabase.LoadAssetAtPath<GameObject>("Objects/Prefabs/Circle.prefab"));

        Selection.activeTransform = go.transform;
    }

}