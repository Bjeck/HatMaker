using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public string controller;

    Rigidbody rigidbody;

    Vector3 velocity;
    public Hat heldHat;

    public float speed = 5f;

    public float maxVelocityMagnitude = 30f;
    public List<Order> PlayerOrders;

    Quaternion rotation;
	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        velocity = new Vector3(hInput.GetAxis(controller + "Horizontal"), 0 , -hInput.GetAxis(controller + "Vertical"));
        velocity *= speed;

        if(rigidbody.velocity.magnitude < maxVelocityMagnitude)
        {
            rigidbody.AddForce(velocity, ForceMode.VelocityChange);
        }


        if(rigidbody.velocity.magnitude > 0.5f)
        {
            transform.LookAt(transform.position + rigidbody.velocity);
        //rigidbody.angularVelocity = Vector3.zero;
            rotation.eulerAngles = new Vector3(0, transform.rotation.eulerAngles.y, 0);

            transform.rotation = rotation;
        }


        if (hInput.GetButtonDown(controller + "Use"))
        {
            TryUse();
        }

    }

    void TryUse()
    {
        //print("try use " + heldHat);
        if (heldHat != null)
        {
            DropHat();

            return;
        }
        else
        {
            PickupHat();
        }

    }

    public void DropHat()
    {
        heldHat.RemoveHatFromPlayer();
        heldHat = null;
    }

    void PickupHat()
    {
        RaycastHit[] hits;
        hits = Physics.SphereCastAll(transform.position, 4, transform.forward, 3f);

        for (int i = 0; i < hits.Length; i++)
        {
            hits[i].transform.gameObject.SendMessage("OnInteract", this, SendMessageOptions.DontRequireReceiver);
            if (hits[i].collider.CompareTag("Hat"))
            {
                //is a hat. bind.
                heldHat = hits[i].transform.gameObject.GetComponent<Hat>();
            }
        }
    }


}
