using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;
public class HouseBuilder : MonoBehaviour
{
    public enum HouseWidth { Two = 2, Three = 3 };
    public bool buildOnPlace = true;
    [ShowIf("buildOnPlace")]
    public bool randomiseOnPlace = true;
    [FoldoutGroup("Values")]
    [Min(1)]
    [MaxValue(4)]
    public int floors = 2;
    [FoldoutGroup("Values")]
    [EnumToggleButtons]
    public HouseWidth width = HouseWidth.Two;
    [FoldoutGroup("Values")]
    public float floorScale = 3.36f;
    [FoldoutGroup("Values")]
    public float baseScale = 4.6f;
    [FoldoutGroup("Materials")]
    public Material windowLit, windowUnlit;
    [FoldoutGroup("Layers")]
    public List<GameObject> lowerLayersTwo = new List<GameObject>();
    [FoldoutGroup("Layers")]
    public List<GameObject> lowerLayersThree = new List<GameObject>();
    [FoldoutGroup("Layers")]
    public List<GameObject> midLayersTwo = new List<GameObject>();
    [FoldoutGroup("Layers")]
    public List<GameObject> midLayersThree = new List<GameObject>();
    [FoldoutGroup("Layers")]
    public List<GameObject> upperLayersTwo = new List<GameObject>();
    [FoldoutGroup("Layers")]
    public List<GameObject> upperLayersThree = new List<GameObject>();
    List<GameObject> layers = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnValidate()
    {
        KillChildren();
        Build();
    }
    [Button]
    void Build()
    {
        KillChildren();
        layers = new List<GameObject>();
        BuildFloor();
        BuildLayers();
        BuildRoof();

    }
    [Button]
    void KillChildren()
    {
        gameObject.KillChildren(true);
    }
    void BuildLayers()
    {

        List<GameObject> midLayers = width == HouseWidth.Two ? midLayersTwo : midLayersThree;
        for (int i = 1; i < floors; i++)
        {
            Vector3 angle = new Vector3(0, 90 * Random.Range(0, 4), 0);
            GameObject layer = Instantiate(midLayers.Any(), transform.position.AdjustY(baseScale + (floorScale * (i - 1))), Quaternion.Euler(angle));
            layer.transform.SetParent(transform);
            layer.AddComponent<BoxCollider>();
            layers.Add(layer);
        }
    }
    void BuildFloor()
    {
        List<GameObject> possibleLayers = width == HouseWidth.Two ? lowerLayersTwo : lowerLayersThree;
        GameObject layer = Instantiate(possibleLayers.Any(), transform.position, Quaternion.identity);
        layer.transform.SetParent(transform);
        layer.AddComponent<BoxCollider>();
        layers.Add(layer);
    }
    void BuildRoof()
    {
        List<GameObject> possibleLayers = width == HouseWidth.Two ? upperLayersTwo : upperLayersThree;
        GameObject layer = Instantiate(possibleLayers.Any(), transform.position.AdjustY(baseScale + (floorScale * (layers.Count - 1))), Quaternion.identity);
        layer.transform.SetParent(transform);

        layers.Add(layer);
    }
    [Space(50)]
    [InlineButton("MakePrefab", "Place")]
    [PropertyOrder(1)]
    public string buildingName = "Buliding";

    void MakePrefab()
    {
        if (buildingName.Length > 0)
        {
            GameObject prefab = GameObject.Instantiate(gameObject, transform.position, transform.rotation);
            UnityEditor.Undo.RecordObject(prefab, "Created new building");
            prefab.name = buildingName;
            BuildingController controller = prefab.AddComponent<BuildingController>();
            DestroyImmediate(prefab.GetComponent<HouseBuilder>());
            controller.windowLit = windowLit;
            controller.windowUnlit = windowUnlit;
            controller.FindWindows();
            controller.SetLights();
            transform.position += new Vector3(0, 0, 1) * 8;
            if (buildOnPlace)
            {
                if (randomiseOnPlace)
                {
                    width = (HouseWidth)(Random.Range(0, 1) + 2);
                    floors = Random.Range(1, 5);
                }
                Build();
            }
        }
    }

}
