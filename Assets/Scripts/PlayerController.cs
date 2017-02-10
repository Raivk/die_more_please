using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D rigidBod;

    public float speed;

    public float jumpForce;

    private bool collideground = false;

    public Transform spawner;

    protected enum State
    {
        normal,
        jumping
    }

    protected State state;

    // Use this for initialization
    protected void Start () {
        transform.position = spawner.position;
        rigidBod = GetComponent<Rigidbody2D>();
	}

    protected void handleInput(string horizontal, string jump)
    {
        this.rigidBod.velocity = new Vector2(Mathf.Lerp(0, Input.GetAxis(horizontal) * speed, 0.8f),
                                                    rigidBod.velocity.y);
        if (Input.GetButtonDown(jump) && (state == State.normal || collideground))
        {
            state = State.jumping;
            rigidBod.AddForce(new Vector2(0f, jumpForce));
        }
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            if (state == State.jumping)
            {
                state = State.normal;
            }
            collideground = true;
        }
    }

    protected void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            collideground = false;
        }
    }

    protected void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            collideground = true;
        }
    }

    protected void OnBecameInvisible()
    {
        respawn();
    }

    protected void respawn()
    {
        this.rigidBod.velocity = new Vector2(0, 0);
        if(spawner != null)
        {
            this.transform.position = spawner.position;
        }
    }
}
