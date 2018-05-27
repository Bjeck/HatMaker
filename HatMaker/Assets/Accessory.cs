using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accessory : MonoBehaviour {

    [SerializeField] HatAccessory accessoryType;
    BoxCollider collider;
    Rigidbody rigid;

	// Use this for initialization
	void Start () {
        collider = GetComponent<BoxCollider>();
        rigid = GetComponent<Rigidbody>();
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name != "Base" && !collision.gameObject.CompareTag("Station") && collision.gameObject.name != "Ground") //this is best code wow
        {
            //print("Hit " + collision.gameObject.name);
            transform.SetParent(collision.transform);
            Destroy(collider);
            Destroy(rigid);

            if (collision.gameObject.CompareTag("Hat"))
            {
                collision.gameObject.GetComponent<Hat>().hattributes.accessories.Add(accessoryType);
            }
        }
    }

}
