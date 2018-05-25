using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station_Scaler : MonoBehaviour {

    public GameObject SnapPoint;
    public GameObject PointWhenFinish;
    public GameObject ConnectedObject;
    public float ScaleProportion = 1f;
    public float OriginalScale = 1f;
    public float MaxScale = 1f;
    public float MinScale = 0.1f;
    public float DistanceMultiplier = 0.2f;
    public bool hasClicked = false;
    private Player P;
    public float Cooldown = 5f;
    [Range(0f,1f)]
    public float Cooldowntime = 0;

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Hat" && ConnectedObject == null)
        {
            Hat H = col.GetComponent<Hat>();
            Rigidbody ObjRB = H.GetComponent<Rigidbody>();
            ObjRB.velocity = Vector3.zero;
            ObjRB.angularVelocity = Vector3.zero;
            ObjRB.isKinematic = true;

            Transform ObjTra = H.GetComponent<Transform>();
            ObjTra.transform.position = SnapPoint.transform.position;
            ObjTra.transform.rotation = Quaternion.identity;

            H.RemoveHatFromPlayer();
            H.enabled = false;

            ConnectedObject = col.gameObject;  
        }
    }

    public void OnInteract(object G)
    {
        //print("Interact");
        P = (G as Player);
        if(ConnectedObject != null)
        {
            hasClicked = true;
            OriginalScale = ConnectedObject.transform.localScale.x;
            ConnectedObject.GetComponent<Collider>().enabled = false;
        }else{
            print("Scaler: No Hat");
        }
    }

    void Update()
    {
        if(hasClicked)
        {
            ScaleProportion = Vector3.Distance(P.transform.position, this.transform.position)*DistanceMultiplier;
            float NS = ScaleProportion * OriginalScale;

            if(NS > MaxScale){
                NS = MaxScale;
            }
            if(NS < 0f){
                NS = MinScale;
            }

            Vector3 NS2 = new Vector3(NS, NS, NS);
            ConnectedObject.transform.localScale = NS2;

            if (hInput.GetButtonUp(P.controller + "Use"))
            {
                hasClicked = false;

                StartCoroutine(CooldownRoutine());
            }
        }
    }

    IEnumerator CooldownRoutine()
    {
        ConnectedObject.transform.position = PointWhenFinish.transform.position;
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
