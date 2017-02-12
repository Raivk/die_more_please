using UnityEngine;
using System.Collections;

public class MineController : MonoBehaviour {

    public ExplosionController explosion;

    private bool activated = true;

    public AudioClip[] sounds;

    private AudioSource clicksource;

    private bool canplay = true;

    public float delay;

    IEnumerator wait()
    {
        canplay = false;
        yield return new WaitForSeconds(delay);
        canplay = true;
    }

    private void Start()
    {
        clicksource = GameObject.Find("MainSourceMineClicks").GetComponent<AudioSource>();
    }

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        try
        {
            if (canplay)
            {
                StartCoroutine(wait());
                clicksource.PlayOneShot(sounds[Random.Range(0, sounds.Length)]);
            }
        }
        catch
        {
            //NOTHING
        }
        if (collision.gameObject.layer == 10 || collision.gameObject.layer == 11 || collision.gameObject.layer == 9)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
