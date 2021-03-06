﻿using UnityEngine;
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

    public TextBlinker tb;

    private bool activated = false;

    public GameObject menuQuit;

    private bool menuActivated = false;

    public AudioSource music;


    IEnumerator changeLev(int level)
    {
        float fadeTime = Camera.main.GetComponent<Fading>().BeginFade(1);
        StartCoroutine(FadeOut(music, fadeTime));
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(level);
    }

    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }


    public void stopGame()
    {
        GameObject.Find("p1").GetComponent<PlayerController>().deactivate();
        GameObject.Find("p2").GetComponent<PlayerController>().deactivate();
        Time.timeScale = 1;
        StartCoroutine(changeLev(1));
    }

    IEnumerator endGame()
    {
        GameObject.Find("p1").GetComponent<PlayerController>().deactivate();
        GameObject.Find("p2").GetComponent<PlayerController>().deactivate();
        yield return new WaitForSeconds(secondsEndGame);
        StartCoroutine(changeLev(1));
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuActivated = !menuActivated;
            menuQuit.SetActive(menuActivated);
            if (menuActivated)
            {
                GameObject.Find("p1").GetComponent<PlayerController>().deactivate();
                GameObject.Find("p2").GetComponent<PlayerController>().deactivate();
                Time.timeScale = 0;
            }
            else
            {
                GameObject.Find("p1").GetComponent<PlayerController>().activate();
                GameObject.Find("p2").GetComponent<PlayerController>().activate();
                Time.timeScale = 1;
            }
        }
        //TIMER
        if (isCounting)
        {
            timeLeft -= Time.deltaTime;
            timer.text = "" + Mathf.Floor(timeLeft / 60).ToString("00") + " : " + (timeLeft % 60).ToString("00");
            if(timeLeft < 25 && !activated)
            {
                activated = true;
                tb.activate();
            }
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
