﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject uiOrderObject;

    public RectTransform orderListParent;

    List<UIOrder> ordersInUI = new List<UIOrder>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CreateUIOrder(Order order)
    {
        GameObject g = Instantiate(uiOrderObject,orderListParent);
        UIOrder ord = g.GetComponent<UIOrder>();
        ord.Setup(order);
        ordersInUI.Add(ord);
    }

    public void RemoveUIOrder(Order order)
    {
        UIOrder ord = ordersInUI.Find(x => x.order.player == order.player);
        ordersInUI.Remove(ord);

        print(ord);
        Destroy(ord.gameObject);
        
    }


}
