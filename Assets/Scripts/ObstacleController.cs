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

    public SpriteRenderer[] renderers;

    public float nbTicks;
    public float delay_between_ticks;

    IEnumerator swapColor(Color togo)
    {
        Color from = renderers[0].color;
        for (int i = 0; i < nbTicks; i++) {
            foreach(SpriteRenderer sp in renderers)
            {
                sp.color = Color.Lerp(from, togo, (((float)i) / ((float)nbTicks)));
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
        StartCoroutine(swapColor(neutralColor));
	}
	
    public void changeColor(int col)
    {
        StopAllCoroutines();
        if (col == 0)
        {
            state = State.p2;
            gameObject.layer = 11;
            StartCoroutine(swapColor(p2Color));
        }
        else
        {
            if(col == 1)
            {
                state = State.p1;
                gameObject.layer = 10;
                StartCoroutine(swapColor(p1Color));
            }
            else
            {
                state = State.neutral;
                gameObject.layer = 0;
                StartCoroutine(swapColor(neutralColor));
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
