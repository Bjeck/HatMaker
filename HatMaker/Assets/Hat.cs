using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hat : MonoBehaviour {

    SpringJoint springjoint;
    Player connectedPlayer;
    public float maxDistance = 10f;

    private void Start()
    {
        springjoint = GetComponent<SpringJoint>();
    }

    private void Update()
    {
       
    }

    public void OnInteract(object player)
    {
        print("i'm a hat and I want to interact with you!");

        connectedPlayer = (Player)player;

        if(connectedPlayer != null)
        {
            springjoint.connectedBody = connectedPlayer.GetComponent<Rigidbody>();
            springjoint.connectedAnchor = connectedPlayer.transform.position + connectedPlayer.transform.forward;


        }


    }
}
