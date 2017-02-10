using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D rigidBod;

    public float speed;

    public float jumpForce;

    private bool collideground = false;

    public Transform spawner;

    public GameController gc;

    public string horizAxis;

    public string jumpBt;

    public int playerNumber;

    private enum State
    {
        normal,
        jumping
    }

    private State state;

    // Use this for initialization
    private void Start () {
        transform.position = spawner.position;
        rigidBod = GetComponent<Rigidbody2D>();
	}

    private void Update()
    {
        handleInput();
    }

    private void handleInput()
    {
        this.rigidBod.velocity = new Vector2(Mathf.Lerp(0, Input.GetAxis(horizAxis) * speed, 0.8f),
                                                    rigidBod.velocity.y);
        if (Input.GetButtonDown(jumpBt) && (state == State.normal || collideground))
        {
            state = State.jumping;
            rigidBod.AddForce(new Vector2(0f, jumpForce));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            if (state == State.jumping)
            {
                state = State.normal;
            }
            collideground = true;
        }
        else
        {
            if(collision.transform.tag == "Lethal")
            {
                respawn();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            collideground = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            collideground = true;
        }
    }

    private void OnBecameInvisible()
    {
        respawn();
    }

    public void respawn()
    {
        this.rigidBod.velocity = new Vector2(0, 0);
        gc.incScore(playerNumber);
        if(spawner != null)
        {
            this.transform.position = spawner.position;
        }
    }
}
