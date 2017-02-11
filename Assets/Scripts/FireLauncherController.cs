using UnityEngine;
using System.Collections;

public class FireLauncherController : MonoBehaviour {

    public ParticleSystem pc;

    public Collider2D lethalCol;

    public int range;

    public int proba;

    public float delay;

    private float lastChange;

    private void Start()
    {
        lastChange = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate () {
        if(Time.time - lastChange >= delay) {
            lastChange = Time.time;
            if (Random.Range(0, range) < proba)
            {
                act();
            }
        }
	}

    public void act()
    {
        if (pc.isPlaying)
        {
            lethalCol.enabled = false;
            pc.Stop();
        }
        else
        {
            lethalCol.enabled = true;
            pc.Play();
        }
    }
}
