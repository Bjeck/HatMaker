using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public List<Color> playerColors = new List<Color>();

    public GameObject playerPrefab;

    int maxPlayerCount = 4;

    public static int playerCount = 4;

    public List<Player> playersAtStart = new List<Player>(); 

    public Orders orders;
    public CustomerLine customerLine;

    private void Start()
    {
        for (int i = 0; i < playerCount; i++)
        {
            GameObject g = Instantiate(playerPrefab, transform.position, Quaternion.identity);
            Player p = g.GetComponent<Player>();
            p.controller = "Pad" + (i+1);
            p.Setup(playerColors[i]);
            playersAtStart.Add(p);
        }


        orders.players = playersAtStart;
        orders.StartGame();

    }





}
