using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public string controller;

    Rigidbody rigidbody;

    Vector3 velocity;

    public float speed = 5f;


	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        velocity = new Vector3(hInput.GetAxis(controller + "Horizontal"), hInput.GetAxis(controller + "Vertical"),0);
        velocity *= speed;
        rigidbody.velocity = velocity;

	}
}
