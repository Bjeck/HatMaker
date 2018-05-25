using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventOnInteraction : MonoBehaviour
{
    public UnityEvent OnInteraction;

    public void OnInteract(object payload)
    {
        OnInteraction.Invoke();
    }
}
