using UnityEngine;
using System.Collections;

public class PortalController : MonoBehaviour {

    public PortalController destination;

    public Transform exit;

    public SpriteGlow sprg;

    public int basicOutlineWidth;

    public ScreenShake sc;

    IEnumerator shine()
    {
        for(int i = 0; i < 10; i++)
        {
            sprg.OutlineWidth++;
            yield return new WaitForSeconds(0.015f);
        }
        for (int i = 0; i < 10; i++)
        {
            sprg.OutlineWidth--;
            yield return new WaitForSeconds(0.015f);
        }
        sprg.OutlineWidth = basicOutlineWidth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.layer == 9 || collision.collider.gameObject.layer == 10 || collision.collider.gameObject.layer == 11)
        {
            if(sc != null)
            {
                sc.StartShake(70, 0.1f);
            }
            StartCoroutine(shine());
            destination.StartCoroutine(destination.shine());
            collision.collider.transform.position = destination.exit.position;
        }
    }

}
