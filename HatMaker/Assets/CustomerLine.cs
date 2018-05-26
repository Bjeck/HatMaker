using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class CustomerPositionClass
{
    public GameObject Position;
    public GameObject OccupiedBy;
    public bool hasSeated = false;

    public CustomerPositionClass(GameObject Pos, GameObject Who, bool seated)
    {
        Position = Pos;
        OccupiedBy = Who;
        hasSeated = seated;
    }

    public void HasReached()
    {
        Orders O = GameObject.Find("Managers").GetComponent<GameManager>().orders;
        Player P = O.CheckForPlayerWithoutOrder();
        if(P != null){
            Debug.Log("Found player without order");
            O.GiveNewOrder(P, OccupiedBy.GetComponent<Customer>());
        }else{
            Debug.Log("No player without order");
        }
    }
}

public class CustomerLine : MonoBehaviour {
    
    [Header("Customer Positions")]
    public List<CustomerPositionClass> HandlePosition;
    public List<CustomerPositionClass> WaitingPosition;

    public CustomerPositionClass AskForPosition(GameObject customer){

        CustomerPositionClass AssignedPlace = null;
        for (int i = 0; i < HandlePosition.Count; i++)
        {
            if (HandlePosition[i].OccupiedBy == null)
            {
                AssignedPlace = HandlePosition[i];
                break;
            }
        }
        if (AssignedPlace == null)
        {
            for (int i = 0; i < WaitingPosition.Count; i++)
            {
                if (WaitingPosition[i].OccupiedBy == null)
                {
                    AssignedPlace = WaitingPosition[i];
                    break;
                }
            } 
        }

        if(AssignedPlace != null){
            AssignedPlace.OccupiedBy = customer;

            return AssignedPlace;
        }else{
            return null;
        }
    }

    [Header("Customer Spawning")]
    public GameObject CustomerPrefab;
    public GameObject StartingPosition;
    public float SpawningRate = 30f;
    public float SpawningVariability = 10f;
    public float Timer = 0f;

    void Start(){
        StartCoroutine(CustomerSpawner());
    }

    public IEnumerator CustomerSpawner()
    {
        SpawnCustomers(GameManager.playerCount);
        while(true)
        {
            float waitTime = SpawningRate + Random.Range(0,SpawningVariability);
            for (float t = 0; t < 1; t += Time.deltaTime / waitTime)
            {
                Timer = waitTime-(waitTime*t);
                yield return null;
            }
            SpawnCustomers(1);

            yield return new WaitForEndOfFrame();
        }
    }

    public void SpawnCustomers(int num)
    {
        for (int i = 0; i < num; i++){
            GameObject newCustomer = (Instantiate(CustomerPrefab, StartingPosition.transform.position, Quaternion.identity)) as GameObject;
            newCustomer.GetComponent<Customer>().CL = this;
        }
    }
}
