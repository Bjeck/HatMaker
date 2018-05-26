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
    public IEnumerator WaitingMovement;
    public Quaternion TargetRotation;
    public bool onTheHunt = false;

    public void Start()
    {
        NMA = GetComponent<NavMeshAgent>();
        LookForNowPosition();
    }

    public void OnStartMoving()
    {
        CPC.hasSeated = false;
    }

    public void OnStopMoving()
    {  
        if(TargetTransform == CL.StartingPosition.transform)
        {
            // To late, take points!

            Destroy(this.gameObject);
        }
        else
        {
            CPC.hasSeated = true;
            if(CL.HandlePosition.Contains(CPC))
            {
                CPC.HasReached(); 
            }
        }
    }

    public void GetTheFuckOut(){
        CPC.OccupiedBy = null;
        TargetTransform = CL.StartingPosition.transform;
        NMA.SetDestination(TargetTransform.position);
        CL.UpdateAllCustomerpositions();
    }

    public void GetHim(GameObject G){
        CPC.OccupiedBy = null;
        TargetTransform = G.transform;
        onTheHunt = true;
    }

    public void SetPositionTo(CustomerPositionClass nCPC)
    {
        TargetTransform = nCPC.Position.transform;
        NMA.SetDestination(TargetTransform.position);
    }

    public void LookForNowPosition(){
        if(CPC.OccupiedBy != null){
            CPC.OccupiedBy = null;
        }

        CPC = CL.AskForPosition(gameObject);
        if(CPC != null){
            SetPositionTo(CPC);
        }

    }

    void Update()
    {
        if(NMA.hasPath && !isMoving){
            if(WaitingMovement != null){
                StopCoroutine(WaitingMovement);
                WaitingMovement = null;
            }

            OnStartMoving();
            isMoving = true;
        }
        else
        {
            if (!NMA.hasPath && isMoving)
            {
                WaitingMovement = WaitingRoutine();
                StartCoroutine(WaitingMovement);

                OnStopMoving();
                isMoving = false;
            }
            transform.rotation = Quaternion.Lerp(transform.rotation, TargetRotation, Time.deltaTime*10);
        }

        if(onTheHunt){
            NMA.SetDestination(TargetTransform.position);
        }
    }

    IEnumerator WaitingRoutine()
    {
        while(true)
        {
            TargetRotation = Quaternion.Euler(0, Random.Range(0, 360) , 0);

            yield return new WaitForSeconds(Random.Range(5f,10f));
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().AddPoints(-20);
            CL.HitPlayer(this);
            Destroy(gameObject);
        }
    }

}
