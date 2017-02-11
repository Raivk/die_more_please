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
        if (Input.GetAxisRaw(horizAxis) == -1)
        {
            anim.SetInteger("state", 1);
            changeDirection("right");
        }
        else
        {
            if(Input.GetAxisRaw(horizAxis) == 1)
            {
                anim.SetInteger("state", 1);
                changeDirection("left");
            }
        }
        if(Input.GetAxisRaw(horizAxis) == 0)
        {
            anim.SetInteger("state", 0);
        }

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
                transform.eulerAngles = new Vector3(0, 180, 0);
                _currentDirection = "right";
            }
            else if (direction == "left")
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                _currentDirection = "left";
            }
        }

    }
}
