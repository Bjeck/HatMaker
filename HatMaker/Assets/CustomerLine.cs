using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CustomerLine : MonoBehaviour {

    [System.Serializable]
    public class CustomerPositionClass
    {
        public GameObject Position;
        public GameObject OccupiedBy;

        public CustomerPositionClass(GameObject Pos, GameObject Who)
        {
            Position = Pos;
            OccupiedBy = Who;
        }
    }

    [Header("Customer Positions")]
    public List<CustomerPositionClass> HandlePosition;

    [Header("Customer Positions")]
    public List<CustomerPositionClass> WaitingPosition;

    public CustomerPositionClass AskForPosition(GameObject customer){

        int GetPosition = 99;
        for (int i = 0; i < HandlePosition.Count; i++)
        {
            if (HandlePosition[i].OccupiedBy == null)
            {
                GetPosition = i;
                break;
            }
        }

        if(GetPosition != 99){
            return HandlePosition[GetPosition];
        }else{
            return null;
        }
    }

}
