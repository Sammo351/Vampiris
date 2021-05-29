using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
public class BuildingController : MonoBehaviour
{
    public bool externalLightsOn = true;
    public bool interalLightsOn = false;
    [FoldoutGroup("Window Materials")]
    public Material windowLit, windowUnlit;
    public List<GameObject> windows = new List<GameObject>();
    public List<LightController> lights = new List<LightController>();

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
        SetLights();
    }
    [Button]
    void FindWindows()
    {
        windows = gameObject.GetDescendants().Where(x => x.tag == "Window").ToList();
    }
    void FindLights()
    {
        lights = gameObject.GetComponentsInChildren<LightController>().ToList();
    }
    void SetLights()
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
    }

}
