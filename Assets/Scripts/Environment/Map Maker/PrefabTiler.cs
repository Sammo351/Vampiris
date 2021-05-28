using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class PrefabTiler : MonoBehaviour
{
    public GameObject obj;
    public Vector2Int size = Vector2Int.one;
    public float scale = 1;
    public float initialSize = 1.5f;
    public bool autoRotate = true;
    public bool liveBuild = false;


    void OnValidate()
    {
        if (liveBuild)
        {
            Debug.Log("Validated");
            Trigger();
        }
    }
    public void Trigger()
    {
        gameObject.KillChildren(true);
        Build();
    }
    void Build()
    {

        Vector3 p = gameObject.Pos() + new Vector3(initialSize, 0, initialSize) * .5f;
        for (int i = 0; i < size.x; i++)
        {
            float x = i * initialSize * scale;
            for (int j = 0; j < size.y; j++)
            {
                float z = j * initialSize * scale;
                Quaternion rot = autoRotate ? Quaternion.Euler(0, Random.Range(0, 3) * 90, 0) : Quaternion.identity;
                GameObject temp = GameObject.Instantiate(obj, new Vector3(p.x + x, p.y, p.z + z), rot);
                temp.transform.localScale = Vector3.one * scale;
                temp.transform.SetParent(transform, true);
            }
        }
    }
    [Button]
    public void MakeStatic()
    {
        gameObject.name = "Tiled " + obj.name;
        DestroyImmediate(this);
    }
}
