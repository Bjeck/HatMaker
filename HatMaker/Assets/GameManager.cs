using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject playerPrefab;

    int maxPlayerCount = 4;

    public static int playerCount = 1;

    public List<Player> playersAtStart = new List<Player>(); 

    public Orders orders;

    private void Start()
    {
        for (int i = 0; i < maxPlayerCount; i++)
        {
            GameObject g = Instantiate(playerPrefab, transform.position, Quaternion.identity);
            Player p = g.GetComponent<Player>();
            p.controller = "Pad" + (i+1);
            playersAtStart.Add(p);
        }

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
