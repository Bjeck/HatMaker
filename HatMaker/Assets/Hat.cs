using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hat : MonoBehaviour {

    SpringJoint springjoint;
    Player connectedPlayer;
    Collider col;
    

    private void Start()
    {
        springjoint = GetComponent<SpringJoint>();
    }

    private void Update()
    {
        if(connectedPlayer != null)
        {
            Vector3 closestPoint = col.ClosestPoint(transform.position);
            transform.position = closestPoint;

         //   if (Vector3.Distance(connectedPlayer.transform.position, transform.position) > maxDistance)
         //   {
               // transform.position = connectedPlayer.transform.position + Vector3.ClampMagnitude((connectedPlayer.transform.position - transform.position), maxDistance);
          //  }
        }


        if(springjoint == null && connectedPlayer != null)
        {
            OnSpringBreak();
        }
    }
    
    void OnSpringBreak()
    {
        connectedPlayer = null;
    }

    public void OnInteract(object player)
    {
        print("i'm a hat and I want to interact with you!");




        connectedPlayer = (Player)player;

        if(connectedPlayer != null)
        {
            if(springjoint == null)
            {
                SpringJoint joint = gameObject.AddComponent<SpringJoint>();

                joint.damper = 40;
                joint.spring = 100;
                joint.breakForce = 1800f;
                springjoint = joint;
            }

            springjoint.connectedBody = connectedPlayer.GetComponent<Rigidbody>();
            springjoint.connectedAnchor = connectedPlayer.transform.position + connectedPlayer.transform.forward;
            col = connectedPlayer.GetComponent<SphereCollider>();

        }


    }
}
