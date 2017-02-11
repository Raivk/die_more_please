using UnityEngine;
using System.Collections;

public class ExplosionController : MonoBehaviour
{

    public float delay = 0f;

    public AudioClip[] sounds;

    public float explosion_power;

    public float explosion_radius;

    // Use this for initialization
    void Start()
    {
        AudioSource.PlayClipAtPoint(sounds[Random.Range(0, sounds.Length)], Camera.main.transform.position);
        this.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);
    }
}