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
    [MenuItem("Vampiris/Assets/Tile Prefab %#t")]
    public static void TilePrefab()
    {
        if (Selection.gameObjects.Length == 0)
        {
            EditorUtility.DisplayDialog("Failed", "Only one GameObject can be selected.", "Ok", "Cool");
            return;
        }
        GameObject selected = Selection.gameObjects.First();
        GameObject copy = selected;//GameObject.Instantiate(selected);

        GameObject tiler = new GameObject("Prefab Tiler");
        tiler.Pos(copy.Pos());
        PrefabTiler scr = tiler.AddComponent<PrefabTiler>();
        scr.obj = copy;
        scr.Trigger();
        Undo.RecordObject(selected, "TilePrefab");
        //selected.SetActive(false);
        //GameObject.DestroyImmediate(selected);
        Selection.activeObject = tiler;
    }
}
