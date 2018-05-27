using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject uiOrderObject;
    public GameObject pointHoverObj;

    public RectTransform orderListParent;

    GameManager gamemanager;

    List<UIOrder> ordersInUI = new List<UIOrder>();

    public Scoreboard scoreboard;

	// Use this for initialization
	void Start () {
        gamemanager = GetComponent<GameManager>();
	}
	
    
    public void CreateUIOrder(Order order)
    {
        GameObject g = Instantiate(uiOrderObject);
        UIOrder ord = g.GetComponent<UIOrder>();
        ord.Setup(order);

        //place over customer
        g.transform.position = gamemanager.customerLine.GetCustomerByOrder(order).transform.position + (Vector3.up * 15) + (Vector3.forward * 4);  //mmmmmmaaagic nuymbers! :D #guesswho

        ordersInUI.Add(ord);
    }

    public void RemoveUIOrder(Order order)
    {
        UIOrder ord = ordersInUI.Find(x => x.order.player == order.player);
        ordersInUI.Remove(ord);

        //print(ord);
        Destroy(ord.gameObject);
        
    }

    public void UpdateScore()
    {
        scoreboard.UpdateScores();
    }

    public void SpawnNumbers(Vector3 pos, int nr)
    {
        GameObject g = Instantiate(pointHoverObj, pos, Quaternion.identity);

        g.GetComponentInChildren<PointHover>().ShowNumber(nr);
    }

}
