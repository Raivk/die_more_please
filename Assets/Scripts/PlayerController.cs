using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D rigidBod;

    public float speed;

    [HideInInspector]
    public float eff_speed = 0f;

    public float jumpForce;

    private bool collideground = false;

    public Transform spawner;

    public GameController gc;

    public string horizAxis;

    public string jumpBt;

    public int playerNumber;

    private string _currentDirection;

    public Animator anim;

    public GameObject explosion_prefab;

    private bool activated = true;

    public GameObject bloodSplat;

    [HideInInspector]
    public int nextInc = 1;

    private enum State
    {
        normal,
        jumping
    }

    private State state;

    // Use this for initialization
    private void Start () {
        eff_speed = speed;
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
            if(state != State.jumping)
            {
                anim.SetInteger("state", 1);
            }
            changeDirection("right");
        }
        else
        {
            if(Input.GetAxisRaw(horizAxis) == 1)
            {
                if (state != State.jumping)
                {
                    anim.SetInteger("state", 1);
                }
                changeDirection("left");
            }
        }
        if(Input.GetAxisRaw(horizAxis) == 0 && state != State.jumping)
        {
            anim.SetInteger("state", 0);
        }

        this.rigidBod.velocity = new Vector2(Mathf.Lerp(0, Input.GetAxis(horizAxis) * eff_speed, 0.8f),
                                                    rigidBod.velocity.y);
        if (Input.GetButtonDown(jumpBt) && (state == State.normal || collideground))
        {
            state = State.jumping;
            anim.SetInteger("state", 2);
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
                        respawn(nextInc);
                        oc.changeColor(playerNumber);
                    }
                }
                else
                {
                    respawn(nextInc);
                }
            }
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        Rigidbody2D rig = other.GetComponent<Rigidbody2D>();
        if (rig != null)
        {
            Debug.Log("player");
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
        if (activated)
        {
            respawn(nextInc);
        }
    }

    public void respawn(int nbDeath)
    {
        nextInc = 1;
        eff_speed = speed;
        Destroy((GameObject)Instantiate(bloodSplat, transform.position, transform.rotation),1f);
        ((GameObject)Instantiate(explosion_prefab, transform.position, transform.rotation)).layer = (11 - playerNumber);
        this.rigidBod.velocity = new Vector2(0, 0);
        gc.death(playerNumber, nbDeath);
        if(spawner != null)
        {
            this.transform.position = spawner.position;
        }
    }

    void OnApplicationQuit()
    {
        activated = false;
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
