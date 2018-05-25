using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum HatTypeName
{
    Fedora, Bowler, Western, Cap, TopHat
}

public enum HatAccessory
{
    Feather, Sword, Monocle, Plane, Pipe, 
}


public class Hattributes
{
    public HatTypeName type;
    public Vector3 size;
    public Color color;
    public HatAccessory[] accessories;
}

public class Hat : MonoBehaviour {

    SpringJoint springjoint;
    Player connectedPlayer;
    Collider col;
    public Hattributes hattributes;
    

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
        }


        if(springjoint == null && connectedPlayer != null)
        {
            OnSpringBreak();
        }
    }
    
    public void RemoveHatFromPlayer()
    {
        if(connectedPlayer != null){
            connectedPlayer.heldHat = null;
        }

        Destroy(springjoint);
        springjoint = null;
        connectedPlayer = null;

    }

    void OnSpringBreak()
    {
        connectedPlayer = null;
    }

    public void OnInteract(object player)
    {
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
