using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

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

    public DoorController[] doors;

    public float secondsEndGame;

    public ScreenShake shaker;

    public int shakeIntensity;
    public float shakeDuration;

    public bool ended = false;

    IEnumerator endGame()
    {
        yield return new WaitForSeconds(secondsEndGame);
        SceneManager.LoadScene(0);
    }

    IEnumerator colorLerp(SpriteRenderer toChange, Color start, Color togo)
    {
        for(int i = 0; i < 15; i++)
        {
            toChange.color = Color.Lerp(start, togo, ((float)i) / ((float)15));
            yield return new WaitForSeconds(0.05f);
        }
    }

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
                ended = true;
                StartCoroutine("endGame");
            }
        }
    }

    public void death(int player, int nbDeath)
    {
        if (shaker != null)
        {
            shaker.StartShake(shakeIntensity, shakeDuration);
        }
        modifyWalls();
        handleDoors();
        for(int i = 0; i < nbDeath; i++)
        {
            incScore(player);
        }
    }

    public void modifyWalls()
    {
        if (!ended)
        {
            StopAllCoroutines();
            GameObject[] walls = GameObject.FindGameObjectsWithTag("Ground");
            Color newCol;
            int r = Random.Range(0, 30);
            if (r < 10)
            {
                r = Random.Range(0, 20);
                if (r < 10)
                {
                    newCol = new Color(0, 1, Random.Range(0.0f, 1.0f));
                }
                else
                {
                    newCol = new Color(0, Random.Range(0.0f, 1.0f), 1);
                }
            }
            else
            {
                if (r < 20)
                {
                    r = Random.Range(0, 20);
                    if (r < 10)
                    {
                        newCol = new Color(1, 0, Random.Range(0.0f, 1.0f));
                    }
                    else
                    {
                        newCol = new Color(Random.Range(0.0f, 1.0f), 0, 1);
                    }
                }
                else
                {
                    r = Random.Range(0, 20);
                    if (r < 10)
                    {
                        newCol = new Color(Random.Range(0.0f, 1.0f), 1, 0);
                    }
                    else
                    {
                        newCol = new Color(1, Random.Range(0.0f, 1.0f), 0);
                    }
                }
            }
            foreach (GameObject wall in walls)
            {
                SpriteRenderer spr = wall.GetComponent<SpriteRenderer>();
                if (spr != null)
                {
                    StartCoroutine(colorLerp(spr, spr.color, newCol));
                }
            }
        }
    }

    public void incScore(int player)
    {
        if(player < nbPlayers && isCounting)
        {
            scores[player]++;
            if(score_displays[player] != null)
            {
                score_displays[player].text = "" + scores[player];
            }
        }
    }

    public void handleDoors()
    {
        int halfNbDoors = ((int)(doors.Length / 2));
        List<int> indices = new List<int>();
        while (indices.Count < halfNbDoors)
        {
            int index = Random.Range(0, doors.Length);
            if (indices.Count == 0 || !indices.Contains(index))
            {
                indices.Add(index);
            }
        }
        for (int i = 0; i < indices.Count; i++)
        {
            int randomIndex = indices[i];
            if(doors[randomIndex] != null)
            {
                doors[randomIndex].act();
            }
        }
    }
}
