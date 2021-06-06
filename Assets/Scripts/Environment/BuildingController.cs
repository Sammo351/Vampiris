using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
public class BuildingController : MonoBehaviour
{
    public bool externalLightsOn = true;
    public bool interalLightsOn = false;
    public bool chimneySmoke = true;
    [FoldoutGroup("Window Materials")]
    public Material windowLit, windowUnlit;
    public List<GameObject> windows = new List<GameObject>();
    public List<LightController> lights = new List<LightController>();

    // Start is called before the first frame update
    void Start()
    {

    }

    [FoldoutGroup("Window Materials")]
    [Button]
    void LoadMaterials()
    {
        windowLit = Resources.Load<Material>("Medieval_Windows_Light_2k");
        windowUnlit = Resources.Load<Material>("Medieval_Windows_2k");
    }
    // Update is called once per frame
    void Update()
    {

    }
    void OnValidate()
    {
        if (windowLit == null || windowUnlit == null)
        {
            LoadMaterials();
        }
        SetLights();
        SetSmoke();

    }
    [Button]
    public void FindWindows()
    {
        windows = gameObject.GetDescendants().Where(x => x.tag == "Window").ToList();
        List<GameObject> windowParents = gameObject.GetDescendantsByName("Windows");

        for (int i = 0; i < windowParents.Count; i++)
        {
            windows.AddRange(windowParents[i].GetComponentsInChildren<MeshRenderer>().Select(x => x.gameObject).Where(x => !windows.Contains(x)));

        }



    }
    void FindLights()
    {
        lights = gameObject.GetComponentsInChildren<LightController>().ToList();
    }
    public void SetLights()
    {

        for (int i = 0; i < windows.Count; i++)
        {
            windows[i].GetComponent<Renderer>().material = interalLightsOn ? windowLit : windowUnlit;
        }
        FindLights();
        for (int i = 0; i < lights.Count; i++)
        {
            LightController lightController = lights[i];
            lightController.SetDependants(externalLightsOn);
        }
        List<BuildingController> controllers = GetComponentsInChildren<BuildingController>().ToList();
        controllers.ForEach(x =>
        {
            if (x != this)
            {
                x.interalLightsOn = interalLightsOn;
                x.externalLightsOn = externalLightsOn;
                x.SetLights();
            }
        });
    }
    void SetSmoke()
    {
        List<ChimneyController> controllers = GetComponentsInChildren<ChimneyController>().ToList();
        controllers.ForEach(x => x.SetSmoke(chimneySmoke));
    }
    public void UpdateAll()
    {
        SetSmoke();
        SetLights();

    }

}
