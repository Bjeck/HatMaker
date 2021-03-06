﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HatColorStationController : MonoBehaviour
{
    [SerializeField] HatColorStation colorStation;

    private Collider collider;
    public AudioSource UseSound;

    private void Start()
    {
        collider = GetComponent<Collider>();
        collider.isTrigger = false;
    }

    public void OnInteract(object payload)
    {
        //print("fun");
        UseSound.Play();
        colorStation.OnInteract();
    }
}
