using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public float jumpVelocity;
    public Vector2 velocity;
    public float grav;
    public LayerMask wallMask;
    public LayerMask floorMask;

    public int attackDamage;
    public bool strumming;
    public bool miniGame;

    public static Player instance;

    private bool attacking;

    private bool walk, walk_left, walk_right, jump;

    public enum PlayerState
    {
        jumping,
        idle,
        walking,
        strumming
    }

    public static PlayerState playerState = PlayerState.idle;

    private bool grounded = false;


    void Start()
    {
        if(attackDamage == 0)
            attackDamage = 10;

        if (strumming == true)
            strumming = false;

        if (miniGame == true)
            miniGame = false;

        if(attacking == true)
            attacking = false;

        Fall();
    }

    void Update()
    {
        CheckPlayerInput();
        UpdatePlayerPos();
        UpdateAnimState();
        
        if (!miniGame)
        {
            if (Input.GetKeyDown("k"))
                attacking = true;
        }
        if (strumming)
        {
            miniGame = true;
            Strum();
        }
    }

    void Parry()
    {

    }

    void Strum()
    {
        Debug.Log("Mini Game running");
        if (Input.GetKeyDown("j"))
            miniGame = false;
        if (!miniGame)
            strumming = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "KillZone")
        {
            SceneManager.LoadScene("GameOver");
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Bard" && Input.GetKeyDown("k"))
            strumming = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Bard")
            strumming = false;
    }

    void UpdatePlayerPos()
    {
        Vector3 pos = transform.localPosition;
        Vector3 scale = transform.localScale;

        if (walk)
        {
            if (walk_left)
            {
                pos.x -= velocity.x * Time.deltaTime;

                scale.x = -1;
            }

            if (walk_right)
            {
                pos.x += velocity.x * Time.deltaTime;

                scale.x = 1;
            }
            pos = CheckWallRays(pos, scale.x);
        }

        if (jump && playerState != PlayerState.jumping)
        {
            playerState = PlayerState.jumping;

            velocity = new Vector2(velocity.x, jumpVelocity);
        }

        if (playerState == PlayerState.jumping)
        {
            pos.y += velocity.y * Time.deltaTime;

            velocity.y -= grav * Time.deltaTime;
        }

        if (velocity.y <= 0)
            pos = CheckFloorRays(pos);

        if (velocity.y >= 0)
            pos = CheckCeilingRays(pos);

        transform.localPosition = pos;
        transform.localScale = scale;
    }

    void UpdateAnimState()
    {
        if (grounded && !walk)
        {
            GetComponent<Animator>().SetBool("isJumping", false);
            GetComponent<Animator>().SetBool("isRunning", false);
        }
        if (grounded && walk)
        {
            GetComponent<Animator>().SetBool("isJumping", false);
            GetComponent<Animator>().SetBool("isRunning", true);
        }
        if (playerState == PlayerState.jumping)
        {
            GetComponent<Animator>().SetBool("isJumping", true);
            GetComponent<Animator>().SetBool("isRunning", false);
        }
    }

    void CheckPlayerInput()
    {
        bool input_left = Input.GetKey(KeyCode.LeftArrow);
        bool input_right = Input.GetKey(KeyCode.RightArrow);
        bool input_space = Input.GetKeyDown(KeyCode.Space);

        walk = input_left || input_right;
        walk_left = input_left && !walk_right;
        walk_right = !input_left && input_right;
        jump = input_space;
    }

    Vector3 CheckWallRays(Vector3 pos, float direction)
    {
        Vector2 originTop = new Vector2(pos.x + direction * .4f, pos.y + 1f - 0.2f);
        Vector2 originMiddle = new Vector2(pos.x + direction * .4f, pos.y);
        Vector2 originBottom = new Vector2(pos.x + direction * .4f, pos.y - 1f + 0.2f);

        RaycastHit2D wallTop = Physics2D.Raycast(originTop, new Vector2(direction, 0), velocity.x * Time.deltaTime, wallMask);
        RaycastHit2D wallMiddle = Physics2D.Raycast(originMiddle, new Vector2(direction, 0), velocity.x * Time.deltaTime, wallMask);
        RaycastHit2D wallBottom = Physics2D.Raycast(originBottom, new Vector2(direction, 0), velocity.x * Time.deltaTime, wallMask);
            if (wallTop.collider != null || wallMiddle.collider != null || wallBottom.collider != null)
            {
                pos.x -= velocity.x * Time.deltaTime * direction;
            }
        return pos;
    }

    Vector3 CheckFloorRays(Vector3 pos)
    {

        Vector2 originleft = new Vector2(pos.x - 0.5f + 0.2f, pos.y - 1f);
        Vector2 originMiddle = new Vector2(pos.x, pos.y - 1f);
        Vector2 originlRight = new Vector2(pos.x - 0.5f - 0.2f, pos.y - 1f);

        RaycastHit2D floorLeft = Physics2D.Raycast(originleft, Vector2.down, velocity.y * Time.deltaTime, floorMask);
        RaycastHit2D floorMiddle = Physics2D.Raycast(originMiddle, Vector2.down, velocity.y * Time.deltaTime, floorMask);
        RaycastHit2D floorRight = Physics2D.Raycast(originlRight, Vector2.down, velocity.y * Time.deltaTime, floorMask);

            if (floorLeft.collider != null || floorMiddle.collider != null || floorRight.collider != null)
            {
                RaycastHit2D hitRay = floorRight;

                if (floorLeft)
                {
                    hitRay = floorLeft;
                }
                else if (floorMiddle)
                {
                    hitRay = floorMiddle;
                }
                else if (floorRight)
                {
                    hitRay = floorRight;
                }

                playerState = PlayerState.idle;

                grounded = true;

                velocity.y = 0;

                pos.y = hitRay.collider.bounds.center.y + hitRay.collider.bounds.size.y / 2 + 1;
            }
        else if (playerState != PlayerState.jumping) Fall();
        return pos;
    }

    Vector3 CheckCeilingRays(Vector3 pos)
    {
        Vector2 originleft = new Vector2(pos.x - 0.5f + 0.2f, pos.y + 1f);
        Vector2 originMiddle = new Vector2(pos.x, pos.y + 1f);
        Vector2 originlRight = new Vector2(pos.x - 0.5f - 0.2f, pos.y + 1f);

        RaycastHit2D ceilLeft = Physics2D.Raycast(originleft, Vector2.up, velocity.y * Time.deltaTime, floorMask);
        RaycastHit2D ceilMiddle = Physics2D.Raycast(originMiddle, Vector2.up, velocity.y * Time.deltaTime, floorMask);
        RaycastHit2D ceilRight = Physics2D.Raycast(originlRight, Vector2.up, velocity.y * Time.deltaTime, floorMask);

            if (ceilLeft.collider != null || ceilMiddle.collider != null || ceilRight.collider != null)
            {
                RaycastHit2D hitRay = ceilRight;

                if (ceilLeft)
                {
                    hitRay = ceilLeft;
                }
                else if (ceilMiddle)
                {
                    hitRay = ceilMiddle;
                }
                else if (ceilRight)
                {
                    hitRay = ceilRight;
                }

                pos.y = hitRay.collider.bounds.center.y - hitRay.collider.bounds.size.y / 2 - 1;

                Fall();
            }
        return pos;
    }

    void Fall()
    {
        velocity.y = 0;

        playerState = PlayerState.jumping;

        grounded = false;
    }
}
