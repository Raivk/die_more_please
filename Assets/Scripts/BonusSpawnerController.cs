using UnityEngine;
using System.Collections;

public class BonusSpawnerController : MonoBehaviour {

    public GameObject bonus_prefab;

    public float proba;
    public float range;
    public float delay;

    private float lastChange;

    private void Start()
    {
        lastChange = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ((Time.time - lastChange) >= delay)
        {
            lastChange = Time.time;
            if ((Random.Range(0, range)) < proba)
            {
                GameObject go = (GameObject)Instantiate(bonus_prefab, transform.position, transform.rotation);
                BonusController bc = go.GetComponent<BonusController>();
                bc.selected = Random.Range(0, bc.sprites.Length);
                bc.change_sprite();
                Destroy(go, delay - (((float)delay) / ((float)10)));
            }
        }
    }
}
