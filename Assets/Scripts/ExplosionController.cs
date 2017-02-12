using UnityEngine;
using System.Collections;

public class ExplosionController : MonoBehaviour
{

    public float delay = 0f;

    public AudioClip[] sounds;

    public float explosion_power;

    public float explosion_radius;

    private AudioSource mainSource;

    // Use this for initialization
    void Start()
    {
        mainSource = GameObject.Find("MainSourceExplosions").GetComponent<AudioSource>();
        mainSource.Stop();
        mainSource.PlayOneShot(sounds[Random.Range(0, sounds.Length)]);
        this.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Lethal")
        {
            Rigidbody2D torepulse = collision.gameObject.GetComponent<Rigidbody2D>();
            if(torepulse != null)
            {
                Vector2 heading = collision.transform.position - transform.position;
                torepulse.AddForce((heading / heading.magnitude) * 27, ForceMode2D.Impulse);
            }
        }
    }
}