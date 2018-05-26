using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreboard : MonoBehaviour {

    GameManager gamemanager;
    public List<ScorePlayer> scoreplayers = new List<ScorePlayer>();

    [SerializeField] GameObject scoreplayerprefab;

	// Use this for initialization
	void Start () {
        gamemanager = GameObject.Find("Managers").GetComponent<GameManager>();

        Setup();


    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public void Setup()
    {
        foreach(Player p in gamemanager.playersAtStart)
        {
            ScorePlayer sp = Instantiate(scoreplayerprefab, transform).GetComponent<ScorePlayer>();
            sp.playercolor.color = p.color;
            sp.scoreText.text = "" + 0;
            scoreplayers.Add(sp);
        }
    }


    public void UpdateScores()
    {
        for (int i = 0; i < scoreplayers.Count; i++)
        {
            scoreplayers[i].scoreText.text = "" + gamemanager.orders.players[i].Points;

        }
    }
}
