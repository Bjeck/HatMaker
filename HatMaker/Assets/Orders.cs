using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class Order
{
    public Player player;
    public Hattributes hattributes;
    public Customer OrderCustomer;
    public float timelimit;
    public float timer;
}

public class Orders : MonoBehaviour {

    public List<Player> players = new List<Player>();
    public UIManager uimanager;
    public List<Order> orders = new List<Order>();

    //order with hattributes and player
    public float sizeThreshold = 1f;

	
	// Update is called once per frame
	void Update () {

        for (int i = 0; i < orders.Count; i++)
        {
            orders[i].timer -= Time.deltaTime;
            if(orders[i].timer < 0)
            {
                ExpireOrder(orders[i]);
            }
        }
	}

    public Player CheckForPlayerWithoutOrder(){

        //print(players.Count);
        if(players.Count > orders.Count())
        {
            for (int i = 0; i < players.Count; i++)
            {
                if(players[i].PlayerOrders.Count < 1){
                    //print("Giving order to "+players[i].name);
                    return players[i];
                }
            }
        }
      
        //print("All players have orders.");
        return null;
    }

    public void StartGame()
    {
       
        //foreach(Player p in players)
        //{
        //    GiveNewOrder(p);
       // }
    }


    public void GiveNewOrder(Player player, Customer CustomerObj)
    {
        Order order = new Order();
        order.player = player;
        order.hattributes = GenerateRandomHattribute();
        order.OrderCustomer = CustomerObj;
        order.timelimit = Random.Range(30f, 40f);
        order.timer = order.timelimit;
        orders.Add(order);
        uimanager.CreateUIOrder(order);
        player.PlayerOrders.Add(order);
    }


    public void ExpireOrder(Customer customer)
    {
        if(orders.Exists(x=>x.OrderCustomer == customer))
        {
            ExpireOrder(orders.Find(x => x.OrderCustomer == customer));
        }
    }

    public void ExpireOrder(Order order)
    {
        //print("expire");
        order.OrderCustomer.GetTheFuckOut();
        order.player.PlayerOrders.Remove(order);
        orders.Remove(order);

        //do UI Things
        uimanager.RemoveUIOrder(order);

        //take points away from player

        //GiveNewOrder(order.player);
    }


    Hattributes GenerateRandomHattribute()
    {
        Hattributes hattributes = new Hattributes();

        hattributes.size = Vector3.one * Random.Range(1.5f, 5f);

        hattributes.color = Random.ColorHSV();

        int r = Random.Range(0, 4); //amount of hat types
        hattributes.type = (HatTypeName)r;

        return hattributes;
    }
    

    public void EvaluateOrder(Player player, Hat hat)
    {
        Order thisOrder = orders.Find(x => x.player == player);

        int totalScore = 0;

        if (IsTypeCorrect(thisOrder, hat))
        {
            totalScore += 100;
        }
        totalScore -= SizeEvaluation(thisOrder, hat);
        totalScore -= ColorEvaluation(thisOrder, hat);

        if(totalScore < 0)
        {
            totalScore = 0;
        }

        print("Evaluated hat " + hat + " with points " + totalScore+ "   " + IsTypeCorrect(thisOrder, hat) + "  "  + SizeEvaluation(thisOrder, hat) + "  " + ColorEvaluation(thisOrder, hat));

        player.AddPoints(totalScore);

        player.DropHat();

        //Destroy(hat.gameObject);
        
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

    int SizeEvaluation(Order order, Hat hat)
    {
        return (int)Vector3.Distance(order.hattributes.size, hat.hattributes.size);
    }

    int ColorEvaluation(Order order, Hat hat)
    {
        Color hatcol = hat.hattributes.color;
        Color orderCol = order.hattributes.color;

        float rDif = Mathf.Abs(hatcol.r - orderCol.r);
        float gDif = Mathf.Abs(hatcol.g - orderCol.g);
        float bDif = Mathf.Abs(hatcol.b - orderCol.b);

        float totaldiff = rDif + gDif + bDif;

        return (int)totaldiff;
    }


}
