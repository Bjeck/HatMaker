using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum HatTypeName
{
    Fedora, Bowler, TopHat, WideHat
}

public enum HatAccessory
{
    Feather, Sword, Glasses, Eyes, Flower, Gear 
}

[System.Serializable]
public class Hattributes
{
    public HatTypeName type;
    public Vector3 size;
    public Color color;
    public List<HatAccessory> accessories = new List<HatAccessory>();
}

public class Hat : MonoBehaviour {

    public Player connectedPlayer;
    SphereCollider col;
    public Hattributes hattributes;
    public Rigidbody RB;
    Quaternion rot;

    void Start(){
        RB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(connectedPlayer != null)
        {
            float dist = Vector3.Distance(connectedPlayer.transform.position, transform.position);
            if(connectedPlayer.rigidbody.velocity.magnitude > 5f){
                transform.position = Vector3.Lerp(transform.position, connectedPlayer.transform.position + connectedPlayer.rigidbody.velocity * 5, Time.deltaTime * 3.5f);
            }
            if (dist > col.radius)
            {
                transform.position = col.ClosestPoint(transform.position);
            }
        }
        transform.position = new Vector3(transform.position.x, 1.7f, transform.position.z);

        rot.eulerAngles = new Vector3(90, 0, 0);
        transform.rotation = rot;
    }
    
    public void RemoveHatFromPlayer()
    {
        this.GetComponent<Collider>().isTrigger = false; 
        RB.velocity = Vector3.zero;
        RB.angularVelocity = Vector3.zero;

        if(connectedPlayer != null){
            connectedPlayer.heldHat = null;
        }
        connectedPlayer = null;
    }

    public void RemoveHatFromPlayer_Kicked()
    {
        if (connectedPlayer != null)
        {
            connectedPlayer.heldHat = null;
        }
        connectedPlayer = null;
    }

    public void OnInteract(object player)
    {
        if(connectedPlayer == null){
            connectedPlayer = (Player)player;
            this.GetComponent<Collider>().isTrigger = true; 

            if (connectedPlayer != null)
            {
                col = connectedPlayer.GetComponent<SphereCollider>();
            }
        }else{
            RemoveHatFromPlayer_Kicked();
        }
    }
}
