using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station_Scaler : MonoBehaviour {

    [Header("#### Settings")]
    public GameObject ConnectedObject;
    private float ScaleProportion = 1f;
    private float OriginalScale = 1f;
    public float MaxScale = 1f;
    public float MinScale = 0.1f;
    public float DistanceMultiplier = 0.2f;
    private bool hasClicked = false;
    private Player P;
    public float Cooldown = 5f;
    [Range(0f,1f)]
    private float Cooldowntime = 0;

    [Header("#### Children")]
    public GameObject SnapPoint;
    public GameObject Handle;
    private Vector3 HandleStartpos;
    public GameObject PullStation;

    void Awake(){
        HandleStartpos = Handle.transform.position;
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Hat" && ConnectedObject == null)
        {
            ConnectedObject = col.gameObject;  
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Hat")
        {
            ConnectedObject = null;
        }
    }

    public void InteractionWithHandle()
    {
        //print("Interact");
        P = Handle.GetComponent<Interact>().PlayerObject;
        if(ConnectedObject != null)
        {
            hasClicked = true;
            OriginalScale = ConnectedObject.transform.localScale.x;
            ConnectedObject.GetComponent<Collider>().enabled = false;
        }
        else
        {
            print("Scaler: No Hat");
        }
    }

    void Update()
    {
        if(hasClicked)
        {
            Handle.transform.position = P.transform.position;
            ScaleProportion = Vector3.Distance(PullStation.transform.position, Handle.transform.position)*DistanceMultiplier;
            float NS = ScaleProportion * OriginalScale;

            if(NS > MaxScale)
            {
                NS = MaxScale;
            }
            if(NS < MinScale){
                NS = MinScale;
            }

            Vector3 NS2 = new Vector3(NS, NS, ConnectedObject.transform.localScale.z);
            ConnectedObject.transform.localScale = NS2;

            if (hInput.GetButtonUp(P.controller + "Use"))
            {
                hasClicked = false;

                StartCoroutine(CooldownRoutine());
            }
        }
        else
        {
            Handle.transform.position = Vector3.Lerp(Handle.transform.position, HandleStartpos, Time.deltaTime * 2);
        }
    }

    IEnumerator CooldownRoutine()
    {
        
        ConnectedObject.GetComponent<Collider>().enabled = true;
        ConnectedObject.GetComponent<Rigidbody>().isKinematic = false;
        ScaleProportion = 1f;
        OriginalScale = 1f;

        ConnectedObject.GetComponent<Hat>().enabled = true;
        ConnectedObject = null;
        P = null;

        GetComponent<Collider>().enabled = false;
        for (float t = 0; t < 1; t += Time.deltaTime / Cooldown)
        {
            Cooldowntime = t;
            yield return null;
        }
        GetComponent<Collider>().enabled = true;
        yield return new WaitForEndOfFrame();
    }

}
