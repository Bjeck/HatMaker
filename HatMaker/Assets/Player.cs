using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public string controller;
    public Color color;

    Rigidbody rigidbody;

    Vector3 velocity;
    public Hat heldHat;

    public float speed = 5f;

    public float maxVelocityMagnitude = 30f;
    public List<Order> PlayerOrders;

    GameObject objToIntWith;

    Quaternion rotation;
	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
	}

    public void Setup(Color col)
    {
        color = col;
        GetComponent<Renderer>().material.color = col;
    }
	
	// Update is called once per frame
	void Update () {

        velocity = new Vector3(hInput.GetAxis(controller + "Horizontal"), 0 , -hInput.GetAxis(controller + "Vertical"));
        velocity *= speed;

        if(rigidbody.velocity.magnitude < maxVelocityMagnitude)
        {
            rigidbody.AddForce(velocity, ForceMode.VelocityChange);
        }

        transform.position = new Vector3(transform.position.x, 1.022739f, transform.position.z); //magic numbers ftw!!

        //rigidbody.velocity = new Vector3(rigidbody.velocity.x, 1, rigidbody.velocity.z);
        //print(rigidbody.velocity);

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

        TestInteraction();
        DrawLine();

    }

    void DrawLine()
    {
        if (objToIntWith == null)
        {
            return;
        }


        Debug.DrawRay(transform.position, objToIntWith.transform.position);



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
            Interact();
        }

    }

    public void DropHat()
    {
        heldHat.RemoveHatFromPlayer();
        heldHat = null;
    }



    void TestInteraction()
    {
        RaycastHit[] hits;
        hits = Physics.SphereCastAll(transform.position, 4, transform.forward, 3f);

        List<GameObject> objects = new List<GameObject>();




        for (int i = 0; i < hits.Length; i++)
        {
            objects.Add(hits[i].transform.gameObject);
        }


        for (int i = 0; i < objects.Count; i++)
        {
            if (hits[i].collider.CompareTag("Player"))
            {
                objToIntWith = hits[i].collider.gameObject;
            }
            else if (hits[i].collider.CompareTag("Hat"))
            {
                objToIntWith = hits[i].collider.gameObject;
            }
            else if (hits[i].collider.CompareTag("Station"))
            {
                objToIntWith = hits[i].collider.gameObject;
            }
        }

    }


    void Interact()
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
