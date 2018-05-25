using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interact : MonoBehaviour {

    public UnityEvent TriggerObject;

    public void OnInteract(GameObject G){
        
        TriggerObject.Invoke();
    }

}
