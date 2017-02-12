using UnityEngine;
using System.Collections;

public class ShurikenSoundController : MonoBehaviour {

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
        clicksource = GameObject.Find("MainSourceClicks").GetComponent<AudioSource>();
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
    }
}
