using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Station_HatCannon : MonoBehaviour {

    [Header("#### Settings")]
    public List<Hat> HatTypes; 

    [Range(0,1)]
    public float PipelineTimer = 0f;
    public float WaitTime = 1f;

    [System.Serializable]
    public class Hat
    {
        public HatTypeName HatType;
        public GameObject Prefab;

        public Hat(HatTypeName Type, GameObject GameObject)
        {
            HatType = Type;
            Prefab = GameObject;
        }
    }

    public List<Hat> HatPipeline;

    public GameObject InstantiatePosition;
    public Text HatSelectorText;
    public Text HatPipelineText;

    private int HatIncrementor = 0;

    void Start()
    {
        StartCoroutine(Pipeline());
        HatSelectorText.text = HatTypes[HatIncrementor].HatType.ToString();
    }

    IEnumerator Pipeline()
    {
        while(true)
        {
            if(HatPipeline.Count > 0){

                string HatName = HatPipeline[0].HatType.ToString();
                HatPipelineText.text = "Making a " + HatName;

                for (float t = 0; t < 1; t += Time.deltaTime / WaitTime)
                {
                    PipelineTimer = t;
                    yield return null;
                }

                GameObject newG = (Instantiate(HatPipeline[0].Prefab, InstantiatePosition.transform.position, Quaternion.identity)) as GameObject;

                newG.GetComponent<Rigidbody>().AddForce(InstantiatePosition.transform.up*6, ForceMode.Impulse);
                HatPipeline.RemoveAt(0);

                yield return new WaitForEndOfFrame();
            }
            else
            {
                HatPipelineText.text = "Ready!";
            }

            yield return new WaitForEndOfFrame();
        }
    }

    [ContextMenu("IncrementHat")]
    public void IncrementHat(){
        HatIncrementor = (HatIncrementor + 1) % HatTypes.Count;
        HatSelectorText.text = HatTypes[HatIncrementor].HatType.ToString();
    }

    [ContextMenu("Add Hat")]
    public void AddHat()
    {
        HatTypeName HT = HatTypes[HatIncrementor].HatType;
        GameObject G = HatTypes[HatIncrementor].Prefab;
        Hat newHat = new Hat(HT, G);
        HatPipeline.Add(newHat);
    }

    public void OnInteract(object player)
    {
        // Selection
    }


}
