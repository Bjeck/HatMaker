using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandoverPlace : MonoBehaviour {

    GameManager gamemanager;
    CustomerLine customerLine;

	// Use this for initialization
	void Start () {
        gamemanager = GameObject.Find("Managers").GetComponent<GameManager>();
        customerLine = GameObject.Find("CustomerLine").GetComponent<CustomerLine>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hat"))
        {
            Hat h = other.gameObject.GetComponent<Hat>();
            //evaluate and go on
            if(h.connectedPlayer != null)
            {
                gamemanager.orders.EvaluateOrder(h.connectedPlayer, h);

                customerLine.HandoverHatToCustomer(this, h);
            }
        }
    }




}
