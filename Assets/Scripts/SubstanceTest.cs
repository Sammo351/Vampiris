using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Substance.Game;


public class SubstanceTest : MonoBehaviour
{
    public SubstanceGraph DirtGraph;
    // Start is called before the first frame update

    public float TargetSnowValue = 0;
    
    public float CurrentSnowValue = 0;
    public float SnowAccumSpeed = 15;

    private void Start()
    {
        StartCoroutine(RunSnow());
    }

    public IEnumerator RunSnow()
    {
        while(true)
        {
            CurrentSnowValue = Mathf.Lerp(CurrentSnowValue, TargetSnowValue, SnowAccumSpeed * 0.3f);
            DirtGraph.SetInputFloat("snow", CurrentSnowValue);
            DirtGraph.QueueForRender();
            DirtGraph.RenderAsync();
            yield return new WaitForSeconds(0.3f);
        }
    }

    public void FixedUpdate()
    {
     
    }
}
