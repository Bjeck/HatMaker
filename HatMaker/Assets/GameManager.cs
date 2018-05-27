using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public List<Color> playerColors = new List<Color>();

    public GameObject playerPrefab;

    int maxPlayerCount = 4;

    public static int playerCount = 1;

    public UIManager ui;
    public Dropdown drop;

    public List<Player> playersAtStart = new List<Player>(); 

    public Orders orders;
    public CustomerLine customerLine;

    [Header("#### Order Timing")]
    public float OrderTime = 60f;
    public float OrderVariation = 30f;
    public AudioSource Victory;

    private void Start()
    {
        ui = GetComponent<UIManager>();

    }


    public void SetPlayerCount()
    {
        playerCount = drop.value + 1;
    }


    public void StartGame()
    {

        for (int i = 0; i < playerCount; i++)
        {
            GameObject g = Instantiate(playerPrefab, transform.position, Quaternion.identity);
            Player p = g.GetComponent<Player>();
            p.controller = "Pad" + (i + 1);
            p.Setup(playerColors[i]);
            playersAtStart.Add(p);
        }


        orders.players = playersAtStart;
        orders.StartGame();

    }


    public void EndGame()
    {
        Victory.Play();
        Camera.main.gameObject.GetComponent<CameraControl>().MoveToScoreBoard();

    }





}
