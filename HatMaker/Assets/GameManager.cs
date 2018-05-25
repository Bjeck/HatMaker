using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int playerCount = 1;

    public List<Player> playersAtStart = new List<Player>(); //this is 4 ppl long. because i'm lazy

    public Orders orders;

    private void Awake()
    {

        int playerCountReverse = 4 - playerCount;

        for (int i = playerCountReverse; i >= playerCount; i--)
        {
            Player p = playersAtStart[i];
            playersAtStart.Remove(p);
            Destroy(p.gameObject);

        }

        orders.players = playersAtStart;
        orders.StartGame();

    }


}
