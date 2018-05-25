using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station_Scaler : MonoBehaviour {

    public GameObject SnapPoint;

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Hat")
        {
            Rigidbody ObjRB = col.gameObject.GetComponent<Rigidbody>();
            ObjRB.velocity = Vector3.zero;
            ObjRB.angularVelocity = Vector3.zero;

            Transform ObjTra = col.gameObject.GetComponent<Transform>();
            ObjTra.transform.position = SnapPoint.transform.position;
        }
    }
}
