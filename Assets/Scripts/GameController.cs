using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

    public int nbPlayers;

    private int[] scores;

    public Text[] score_displays;

    public float game_time;

    private float timeLeft;

    public Text timer;

    public Image panelVictory;

    public Text textVictory;

    private bool isCounting = true;

    // Use this for initialization
    void Start () {
        scores = new int[nbPlayers];
        scores[0] = 0;
        scores[1] = 0;
        score_displays[0].text = "" + scores[0];
        score_displays[1].text = "" + scores[1];

        timeLeft = game_time;
    }

    private void Update()
    {
        //TIMER
        if (isCounting)
        {
            timeLeft -= Time.deltaTime;
            timer.text = "" + Mathf.Floor(timeLeft / 60).ToString("00") + " : " + (timeLeft % 60).ToString("00");
            if (timeLeft < 0)
            {
                isCounting = false;
                timer.text = "00 : 00";
                panelVictory.enabled = true;
                textVictory.enabled = true;
                if (scores[0] == scores[1])
                {
                    textVictory.text = "It's a draw !";
                }
                else
                {
                    if (scores[0] > scores[1])
                    {
                        textVictory.text = "Player 1 won !";
                    }
                    else
                    {
                        textVictory.text = "Player 2 won !";
                    }
                }

            }
        }
    }

    public void incScore(int player)
    {
        if(player < nbPlayers && isCounting)
        {
            scores[player]++;
            score_displays[player].text = "" + scores[player];
        }
    }
}
