using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station_HatCannon : MonoBehaviour {

    public float PipelineTimer = 0f;
    public float WaitTime = 1f;

    public enum HatType{
        Large, Medium, Small
    }
    public List<HatType> HatPipeline = new List<HatType>();

    void Start()
    {
        StartCoroutine(Pipeline());
    }

    IEnumerator Pipeline(){
        while(true)
        {
            if(HatPipeline.Count > 0){
                



                yield return new WaitForSeconds(1f);
            }


            yield return new WaitForEndOfFrame();
        }
    }

    public void AddHat()
    {
        
    }

}
