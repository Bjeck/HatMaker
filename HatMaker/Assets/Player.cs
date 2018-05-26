using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] GameObject linePrefab;
    Transform line;

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

    public int Points { get; private set; }

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
            if(line != null)
            {
                Destroy(line.gameObject);
            }
            return;
        }

        if(line == null)
        {
            line = (Instantiate(linePrefab, transform).transform);
            line.localScale = Vector3.one;
            line.position = transform.position;
        }

        line.LookAt(objToIntWith.transform.position);
        float dist = Vector3.Distance(transform.position, objToIntWith.transform.position) / 2f;
        line.localScale = new Vector3(0.2f, 0.2f, dist );
        line.position = transform.position;
        line.position += line.forward * dist;

        
        //Debug.DrawRay(transform.position, objToIntWith.transform.position-transform.position);
        
    }



    public void AddPoints(int pointsToAdd)
    {
        Points += pointsToAdd;
    }


    void TryUse()
    {
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


        objToIntWith = null;
        
        for (int i = 0; i < hits.Length; i++)
        {
            
            if(hits[i].transform.gameObject != gameObject || !hits[i].transform.IsChildOf(transform))
            {
                objects.Add(hits[i].transform.gameObject);
            }
        }

        //print(objects.Count);


        for (int i = 0; i < objects.Count; i++)
        {
            //print(objects[i].name);
            if (objects[i].CompareTag("Ground"))
            {
                continue;
            }
            else if (objects[i].CompareTag("Player") && (hits[i].transform.gameObject != gameObject || !hits[i].transform.IsChildOf(transform)))
            {
                objToIntWith = objects[i];
            }
            else if (objects[i].CompareTag("Hat"))
            {
                if(heldHat != null)
                {
                    if (objects[i] != heldHat.gameObject)
                    {
                        objToIntWith = objects[i];
                    }
                }
                else
                {
                    objToIntWith = objects[i];
                }
            }
            else if (objects[i].CompareTag("Station"))
            {
                objToIntWith = objects[i];
            }
        }
    }


    void Interact()
    {
        if(objToIntWith == null)
        {
            return;
        }

        objToIntWith.SendMessage("OnInteract", this, SendMessageOptions.DontRequireReceiver);
        if (objToIntWith.CompareTag("Hat"))
        {
            //is a hat. bind.
            heldHat = objToIntWith.GetComponent<Hat>();
        }

    }


}
