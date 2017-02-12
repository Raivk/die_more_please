using UnityEngine;
using System.Collections;

public class BonusController : MonoBehaviour {

    public Sprite[] sprites;

    public int selected;

    public SpriteRenderer spr;

    public float slow_duration;

    public float slow_factor;

    public float speed_duration;

    public float speed_factor;

    private GameController gc;

    public AudioClip sound;

    private AudioSource mainSource;

    IEnumerator slow(PlayerController who)
    {
        who.eff_speed = who.speed * slow_factor;
        yield return new WaitForSeconds(slow_duration);
        who.eff_speed = who.speed;
        Destroy(gameObject);
    }

    IEnumerator speedHero(PlayerController who)
    {
        who.eff_speed = who.speed * speed_factor;
        yield return new WaitForSeconds(speed_duration);
        who.eff_speed = who.speed;
        Destroy(gameObject);
    }

	// Use this for initialization
	void Start () {
        mainSource = GameObject.Find("MainSourceClicks").GetComponent<AudioSource>();
        gc = GameObject.Find("HUD").GetComponent<GameController>();
        if(selected < sprites.Length)
        {
            spr.sprite = sprites[selected];
        }
        else
        {
            selected = 0;
            spr.sprite = sprites[selected];
        }
	}

    public void change_sprite()
    {
        if (selected < sprites.Length)
        {
            spr.sprite = sprites[selected];
        }
        else
        {
            selected = 0;
            spr.sprite = sprites[selected];
        }
    }

    public void act(PlayerController who)
    {
        try
        {
            mainSource.PlayOneShot(sound);
        }
        catch
        {
            //NOTHING
        }
        switch (selected)
        {
            case 1:
                slowOther(who);
                break;
            case 2:
                speedUp(who);
                break;
            case 3:
                closeAll(who);
                break;
            case 4:
                openAll(who);
                break;
            case 5:
                mines(who);
                break;
            case 6:
                repaint(who);
                break;
            default:
                tripleScore(who);
                break;
        }
    }

    private void tripleScore(PlayerController who)
    {
        who.nextInc = 3;
        Destroy(gameObject);
    }

    private void slowOther(PlayerController who)
    {
        this.GetComponent<Collider2D>().enabled = false;
        this.spr.enabled = false;
        if(who.playerNumber == 0)
        {
            who = GameObject.Find("p2").GetComponent<PlayerController>();
        }
        else
        {
            who = GameObject.Find("p1").GetComponent<PlayerController>();
        }
        StartCoroutine(slow(who));
    }

    private void speedUp(PlayerController who)
    {
        this.GetComponent<Collider2D>().enabled = false;
        this.spr.enabled = false;
        StartCoroutine(speedHero(who));
    }

    private void closeAll(PlayerController who)
    {
        foreach(DoorController dc in gc.doors){
            dc.close();
        }
        Destroy(gameObject);
    }

    private void openAll(PlayerController who)
    {
        foreach (DoorController dc in gc.doors)
        {
            dc.open();
        }
        Destroy(gameObject);
    }

    private void mines(PlayerController who)
    {
        foreach (Transform child in GameObject.Find("Mine_Spawners").transform)
        {
            try
            {
                child.GetComponent<MineSpawner>().Spawn();
                child.GetComponent<MineSpawner>().Spawn();
            }
            catch
            {
                //NOTHING
            }
        }
        Destroy(gameObject);
    }

    private void repaint(PlayerController who)
    {
        foreach (Transform child in GameObject.Find("ObstacleLayer").transform)
        {
            try
            {
                child.GetComponent<ObstacleController>().changeColor(1 - who.playerNumber);
            }
            catch
            {
                //NOTHING
            }
        }
        Destroy(gameObject);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController pc = collision.transform.GetComponent<PlayerController>();
        if (pc != null)
        {
            act(pc);
        }
    }
}
