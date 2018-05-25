using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour {

    private NavMeshAgent NMA; 

    public void Start(){
        NMA = GetComponent<NavMeshAgent>();
    }
}
