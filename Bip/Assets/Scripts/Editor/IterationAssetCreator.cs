using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class IterationAssetCreator
{
    [MenuItem("Assets/Create/New Pattern Object")]
    public static void CreateAsset()
    {
        Pattern newPattern = ScriptableObject.CreateInstance<Pattern>();

        //newIteration.timeBeforeNextIteration = 10;

        //newIteration.skipToNextPalier = false;


        AssetDatabase.CreateAsset(newPattern, AssetDatabase.GetAssetPath(Selection.activeObject) + "/New Pattern.asset");
        AssetDatabase.SaveAssets();

        Selection.activeObject = newPattern;
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