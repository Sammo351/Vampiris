using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;
using UnityEditor;
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
    public List<Object> lowerLayersTwo = new List<Object>();
    [FoldoutGroup("Layers")]
    public List<Object> lowerLayersThree = new List<Object>();
    [FoldoutGroup("Layers")]
    public List<Object> midLayersTwo = new List<Object>();
    [FoldoutGroup("Layers")]
    public List<Object> midLayersThree = new List<Object>();
    [FoldoutGroup("Layers")]
    public List<Object> upperLayersTwo = new List<Object>();
    [FoldoutGroup("Layers")]
    public List<Object> upperLayersThree = new List<Object>();
    List<GameObject> layers = new List<GameObject>();

    GameObject tempParent;
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
        //KillChildren();
        //Build();
    }
    [Button]
    void Build()
    {
        KillChildren();
        tempParent = new GameObject("Building");
        tempParent.Pos(transform.position);
        tempParent.transform.rotation = transform.rotation;
        tempParent.transform.SetParent(transform);
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

        List<Object> midLayers = width == HouseWidth.Two ? midLayersTwo : midLayersThree;
        GameObject prevFloor = layers[0];
        for (int i = 1; i < floors; i++)
        {
            float topOfLastObject = prevFloor.transform.Find("Max_Height").transform.position.y;
            Vector3 angle = new Vector3(0, 90 * Random.Range(0, 4), 0);

            GameObject layer = (GameObject)PrefabUtility.InstantiatePrefab(midLayers.Any());//, transform.position.AdjustY(baseScale + (floorScale * (i - 1))), Quaternion.Euler(angle));




            Vector3 centreOfPrev = prevFloor.transform.Find("Center_Base").transform.position;
            Vector3 centreOfThis = layer.transform.Find("Center_Base").transform.position;
            layer.transform.RotateAround(centreOfThis, transform.up, angle.y + transform.rotation.eulerAngles.y);
            Vector3 differenceBetweenBothCentres = centreOfThis - centreOfPrev;
            layer.Pos(layer.Pos().AdjustX(-differenceBetweenBothCentres.x).AdjustZ(-differenceBetweenBothCentres.z).SetY(topOfLastObject));

            layer.transform.SetParent(tempParent.transform);
            layer.AddComponent<BoxCollider>();
            layers.Add(layer);


            prevFloor = layer;
        }
    }
    void BuildFloor()
    {
        List<Object> possibleLayers = width == HouseWidth.Two ? lowerLayersTwo : lowerLayersThree;
        GameObject layer = (GameObject)PrefabUtility.InstantiatePrefab(possibleLayers.Any());
        layer.Pos(transform.position);
        //layer.transform.RotateAround(centreOfThis, transform.up, angle.y + transform.rotation.eulerAngles.y);
        layer.transform.SetParent(tempParent.transform);
        layer.transform.rotation = transform.rotation;
        layer.AddComponent<BoxCollider>();
        layers.Add(layer);
    }
    void BuildRoof()
    {
        GameObject prevFloor = layers.Last();
        float topOfLastObject = prevFloor.transform.Find("Max_Height").transform.position.y;

        Vector3 angle = new Vector3(0, 90 * Random.Range(0, 4), 0);
        List<Object> possibleLayers = width == HouseWidth.Two ? upperLayersTwo : upperLayersThree;
        GameObject layer = (GameObject)PrefabUtility.InstantiatePrefab(possibleLayers.Any());

        Vector3 centreOfPrev = prevFloor.transform.Find("Center_Base").transform.position;
        Vector3 centreOfThis = layer.transform.Find("Center_Base").transform.position;
        layer.transform.RotateAround(centreOfThis, transform.up, angle.y + transform.rotation.eulerAngles.y);
        Vector3 differenceBetweenBothCentres = centreOfThis - centreOfPrev;
        layer.Pos(layer.Pos().AdjustX(-differenceBetweenBothCentres.x).AdjustZ(-differenceBetweenBothCentres.z).SetY(topOfLastObject));

        // layer.Pos(transform.position.SetY(topOfLastObject));
        // layer.transform.rotation = Quaternion.Euler(angle + transform.rotation.eulerAngles);
        layer.transform.SetParent(tempParent.transform);

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
            //GameObject prefab = (GameObject)PrefabUtility.InstantiatePrefab(gameObject);//, transform.position, transform.rotation);
            //prefab.Pos(transform.position);
            //prefab.transform.rotation = transform.rotation;
            UnityEditor.Undo.RecordObject(transform, "Created new building");
            tempParent.name = buildingName;
            tempParent.transform.SetParent(null);
            BuildingController controller = tempParent.AddComponent<BuildingController>();
            controller.windowLit = windowLit;
            controller.windowUnlit = windowUnlit;
            controller.FindWindows();
            controller.SetLights();
            transform.position += transform.forward * 8;
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
