using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interact : MonoBehaviour {

    public Player PlayerObject;
    public UnityEvent TriggerObject;

    public void OnInteract(Object G){

        PlayerObject = (G as Player);
        //print("Assigning");
        TriggerObject.Invoke();
    }

}
