using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour {

    private NavMeshAgent NMA;
    public CustomerLine CL;
    public Transform TargetTransform;
    public CustomerPositionClass CPC;
    public bool isMoving = false;

    public void Start()
    {
        NMA = GetComponent<NavMeshAgent>();
        CPC = CL.AskForPosition(gameObject);
        if(CPC != null)
        {
            TargetTransform = CPC.Position.transform;
            NMA.SetDestination(TargetTransform.position);
        }
    }

    public void OnStartMoving()
    {
        CPC.hasSeated = false;
    }

    public void OnStopMoving()
    {
        CPC.hasSeated = true;
        CPC.HasReached();
    }

    public void GetTheFuckOut(){
        TargetTransform = CL.StartingPosition.transform;
        NMA.SetDestination(TargetTransform.position);
    }

    void Update()
    {
        if(NMA.hasPath && !isMoving){
            print("Start to Move");
            OnStartMoving();
            isMoving = true;
        }
        else
        {
            if (!NMA.hasPath && isMoving)
            {
                print("Stopping");
                OnStopMoving();
                isMoving = false;
            }

        }
    }

}
