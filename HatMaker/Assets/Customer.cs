using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour {

    private NavMeshAgent NMA;
    private CustomerLine CL;
    public Transform TargetTransform;

    public void Start()
    {
        NMA = GetComponent<NavMeshAgent>();
        CL = GameObject.Find("CustomerLine").GetComponent<CustomerLine>();
        CustomerLine.CustomerPositionClass CPC = CL.AskForPosition(gameObject);
        if(CPC != null){
            TargetTransform = CPC.Position.transform;
            NMA.SetDestination(TargetTransform.position);
        }

    }

    //public void Update(){

       
    //}
}
