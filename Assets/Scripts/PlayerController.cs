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

    private string _currentDirection;

    public Animator anim;

    private enum State
    {
        normal,
        running,
        jumping
    }

    private State state;

    // Use this for initialization
    private void Start () {
        state = State.normal;
        transform.position = spawner.position;
        rigidBod = GetComponent<Rigidbody2D>();
	}

    private void Update()
    {
        handleInput();
    }

    private void handleInput()
    {
        if(Input.GetAxis(horizAxis) != 0 && state == State.normal)
        {
            state = State.running;
            anim.SetInteger("state", 1);
        }
        else
        {
            if(state == State.running)
            {
                state = State.normal;
                anim.SetInteger("state", 0);
            }
        }
        if (Input.GetAxis(horizAxis) > 0)
        {
            changeDirection("right");
        }
        else
        {
            if(Input.GetAxis(horizAxis) < 0)
            {
                changeDirection("left");
            }
        }

        this.rigidBod.velocity = new Vector2(Mathf.Lerp(0, Input.GetAxis(horizAxis) * speed, 0.8f),
                                                    rigidBod.velocity.y);
        if (Input.GetButtonDown(jumpBt) && (state == State.normal || state == State.running || collideground))
        {
            state = State.jumping;
            rigidBod.AddForce(new Vector2(0f, jumpForce));
            anim.SetInteger("state", 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            if (state == State.jumping)
            {
                state = State.normal;
                anim.SetInteger("state", 0);
            }
            collideground = true;
        }
        else
        {
            if(collision.transform.tag == "Lethal")
            {
                ObstacleController oc = collision.collider.transform.GetComponent<ObstacleController>();
                if(oc != null)
                {
                    if (oc.isPlayer(playerNumber))
                    {
                        respawn(1);
                        oc.changeColor(playerNumber);
                    }
                }
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
        respawn(1);
    }

    public void respawn(int nbDeath)
    {
        this.rigidBod.velocity = new Vector2(0, 0);
        gc.death(playerNumber, nbDeath);
        if(spawner != null)
        {
            this.transform.position = spawner.position;
        }
    }





    //--------------------------------------
    // Flip player sprite for left/right walking
    //--------------------------------------
    void changeDirection(string direction)
    {

        if (_currentDirection != direction)
        {
            if (direction == "right")
            {
                transform.Rotate(0, 180, 0);
                _currentDirection = "right";
            }
            else if (direction == "left")
            {
                transform.Rotate(0, -180, 0);
                _currentDirection = "left";
            }
        }

    }
}
