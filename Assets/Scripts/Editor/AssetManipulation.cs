using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
public class AssetManipulation
{
    [MenuItem("Vampiris/Assets/Random Rotation")]
    public static void RandomRotate()
    {
        List<GameObject> selected = Selection.gameObjects.ToList();
        Undo.RecordObjects(selected.ToArray(), "Random Rotation");
        selected.ForEach((x) =>
        {
            int angle = Random.Range(0, 4) * 90;
            x.transform.Rotate(Vector3.up * angle, Space.Self);
        });
    }
}
