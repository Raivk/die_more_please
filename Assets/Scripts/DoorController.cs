using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour {

    private bool openState;

    public Transform door;

    public int nbTicks;

    public float delay_between_ticks;

    public float maxSize;

    public Collider2D col;

    IEnumerator closeAnim()
    {
        for(int i = 0; i < nbTicks; i++)
        {
            door.localScale = new Vector3(door.localScale.x, Mathf.Lerp(0f, maxSize, (((float)(i + 1)) / nbTicks)), door.localScale.z);
            yield return new WaitForSeconds(delay_between_ticks);
        }
    }

    IEnumerator openAnim()
    {
        for (int i = 0; i < nbTicks; i++)
        {
            door.localScale = new Vector3(door.localScale.x, Mathf.Lerp(maxSize, 0f, (((float)(i + 1)) / nbTicks)), door.localScale.z);
            yield return new WaitForSeconds(delay_between_ticks);
        }
    }


    // Use this for initialization
    void Start () {
        openState = false;
        open();
	}
	
    public void act()
    {
        if (openState)
        {
            close();
        }
        else
        {
            open();
        }
    }

	private void close()
    {
        openState = false;
        StopAllCoroutines();
        door.localScale = new Vector3(door.localScale.x, 0f, door.localScale.z);
        StartCoroutine("closeAnim");
        col.enabled = true;
    }

    private void open()
    {
        openState = true;
        StopAllCoroutines();
        door.localScale = new Vector3(door.localScale.x, maxSize, door.localScale.z);
        StartCoroutine("openAnim");
        col.enabled = false;
    }
}
