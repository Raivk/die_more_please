using UnityEngine;
using System.Collections;

public class MineSpawner : MonoBehaviour {

    public GameObject mine_prefab;

    public float proba;
    public float range;
    public float delay;

    private float lastChange;

    public float force_range_min;
    public float force_range_max;

    private void Start()
    {
        lastChange = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate () {
	    if((Time.time - lastChange) >= delay)
        {
            lastChange = Time.time;
            if ((Random.Range(0, range)) < proba)
            {
                GameObject go = (GameObject)Instantiate(mine_prefab, transform.position, transform.rotation);
                    go.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(force_range_min, force_range_max), Random.Range(force_range_min, force_range_max)), ForceMode2D.Impulse);
                
            }
        }
	}
}
