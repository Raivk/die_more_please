using UnityEngine;
using System.Collections;

public class ObstacleController : MonoBehaviour {

    private enum State
    {
        neutral,
        p1,
        p2
    }

    private State state;

    public Color neutralColor;
    public Color p1Color;
    public Color p2Color;

    [ColorUsage(true, true, 0f, 8f, 0.125f, 3f)]
    public Color glow_neutral = Color.white;

    [ColorUsage(true, true, 0f, 8f, 0.125f, 3f)]
    public Color glow_p1 = Color.white;

    [ColorUsage(true, true, 0f, 8f, 0.125f, 3f)]
    public Color glow_p2 = Color.white;

    public SpriteRenderer[] renderers;

    public float nbTicks;
    public float delay_between_ticks;

    IEnumerator swapColor(Color togo, Color glow)
    {
        Color from = renderers[0].color;
        for (int i = 0; i < nbTicks; i++) {
            foreach(SpriteRenderer sp in renderers)
            {
                sp.color = Color.Lerp(from, togo, (((float)i) / ((float)nbTicks)));
                try
                {
                    sp.GetComponent<SpriteGlow>().GlowColor = glow;
                }
                catch
                {
                    //NOTHING
                }
            }
            yield return new WaitForSeconds(delay_between_ticks);
        }
    }

    private bool activated = true;

    private void OnApplicationQuit()
    {
        activated = false;
    }

    private void OnBecameInvisible()
    {
        if (activated)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        state = State.neutral;
        StartCoroutine(swapColor(neutralColor, glow_neutral));
	}
	
    public void changeColor(int col)
    {
        StopAllCoroutines();
        if (col == 0)
        {
            state = State.p2;
            gameObject.layer = 11;
            StartCoroutine(swapColor(p2Color, glow_p2));
        }
        else
        {
            if(col == 1)
            {
                state = State.p1;
                gameObject.layer = 10;
                StartCoroutine(swapColor(p1Color, glow_p1));
            }
            else
            {
                state = State.neutral;
                gameObject.layer = 0;
                StartCoroutine(swapColor(neutralColor, glow_neutral));
            }
        }
    }

    public bool isPlayer(int pl)
    {
        if((pl == 0 && (state == State.neutral || state == State.p1)) || (pl == 1 && (state == State.neutral || state == State.p2)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
