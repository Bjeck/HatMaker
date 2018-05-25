using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Order
{
    public Player player;
    public Hattributes hattributes;
}

public class Orders : MonoBehaviour {

    List<Player> players = new List<Player>();

    List<Order> orders = new List<Order>();

    //order with hattributes and player
    public float sizeThreshold = 1f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void StartGame()
    {
        foreach(Player p in players)
        {
            GiveNewOrder(p);
        }
    }


    public void GiveNewOrder(Player player)
    {

    }


    public void EvaluateOrder(Player player, Hat hat)
    {
        Order thisOrder = orders.Find(x => x.player == player);
        if (IsTypeCorrect(thisOrder, hat))
        {
            if (IsSizeCorrect(thisOrder, hat))
            {

            }
        }
    }

    bool IsTypeCorrect(Order order, Hat hat)
    {
        if(order.hattributes.type == hat.hattributes.type)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool IsSizeCorrect(Order order, Hat hat)
    {
        if(Vector3.Distance(order.hattributes.size,hat.hattributes.size) < sizeThreshold)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //bool IsColorCorrect(Order order, Hat hat)
    //{
    //    if (Vector3.Distance(order.hattributes.color, hat.hattributes.color) < sizeThreshold)
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}


}
