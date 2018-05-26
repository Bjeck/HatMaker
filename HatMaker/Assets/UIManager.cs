using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject uiOrderObject;

    public RectTransform orderListParent;

    GameManager gamemanager;

    List<UIOrder> ordersInUI = new List<UIOrder>();

	// Use this for initialization
	void Start () {
        gamemanager = GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CreateUIOrder(Order order)
    {
        GameObject g = Instantiate(uiOrderObject);
        UIOrder ord = g.GetComponent<UIOrder>();
        ord.Setup(order);

        //place over customer
        g.transform.position = gamemanager.customerLine.GetCustomerByOrder(order).transform.position + (Vector3.up * 15) + (Vector3.forward * 4);

        ordersInUI.Add(ord);
    }

    public void RemoveUIOrder(Order order)
    {
        UIOrder ord = ordersInUI.Find(x => x.order.player == order.player);
        ordersInUI.Remove(ord);

        //print(ord);
        Destroy(ord.gameObject);
        
    }


}
