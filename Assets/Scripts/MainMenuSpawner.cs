using UnityEngine;
using System.Collections;

public class MainMenuSpawner : MonoBehaviour {

    public GameObject spawnable;

    public float delay;
    public float proba;
    public float range;

    private float lastChange;

	// Use this for initialization
	void Start () {
        lastChange = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
	    if(Time.time - lastChange >= (delay + (Random.Range(-delay, delay))))
        {
            lastChange = Time.time;
            transform.position = new Vector2(Random.Range(-6.0f, 6.0f), transform.position.y);
            if(Random.Range(0, range) < proba)
            {
                Instantiate(spawnable, transform.position, transform.rotation);
            }
        }
	}
}
