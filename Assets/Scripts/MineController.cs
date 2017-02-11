using UnityEngine;
using System.Collections;

public class MineController : MonoBehaviour {

    public ExplosionController explosion;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 10 || collision.gameObject.layer == 11 || collision.gameObject.layer == 9)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
