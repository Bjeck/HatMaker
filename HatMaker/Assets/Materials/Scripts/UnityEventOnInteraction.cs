using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventOnInteraction : MonoBehaviour
{
    public UnityEvent OnInteraction;
    public AudioSource ClickSound;

    public void OnInteract(object payload)
    {
        if(ClickSound != null){
            ClickSound.Play();
        }
        OnInteraction.Invoke();
    }
}
