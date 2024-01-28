using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class Player : MonoBehaviour
{

    public float jumpVelocity;
    public Vector2 velocity;
    public Vector2 velocity2;
    public float grav;
    public LayerMask wallMask;
    public LayerMask floorMask;
    public LayerMask enemyMask;

    public int attackDamage;
    public bool strumming;
    public bool miniGame;

    public Transform[] respawnPoints;
    private int respawnDestination;

    public static Player instance;

    private bool attacking;

    private bool walk, walk_left, walk_right, jump, attack;

    public float KBForce;
    public float KBCounter;
    public float KBTotalTime;

    public bool KnockFromRight;
    public float KBCooldown;
    public float KBmin = 0.0f;
    
    [SerializeField]
    private GameObject hitSquare;
    [SerializeField]
    private GameObject playerObject;

    public CameraFollow cam;

    public enum PlayerState
    {
        jumping,
        idle,
        walking,
        attacking
    }

    public static PlayerState playerState = PlayerState.idle;

    private bool grounded = false;

    private bool strum = true;

    private ScoreManager theSM;
    [SerializeField] OnTime theOT;

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

        respawnDestination = 0;

        Fall();

        theSM = theOT.theSM;
    }

    void Update()
    {
        CheckPlayerInput();
        UpdatePlayerPos();
        UpdateAnimState();
        //if (KBCooldown >= 0)
        //{
        //    KBCooldown -= Time.deltaTime;
        //    if (KBCooldown == 0)
        //    {
        //        KBCooldown = 0;
        //    }
        //}
        //if (KBCounter > 0)
        //{
        //    KBCounter -= Time.deltaTime;
        //    if (KBCounter < 0)
        //    {
        //        KBCounter = 0;
        //    }
        //}
    }

    // Kill Player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "KillZone")
        {
            this.transform.position = respawnPoints[respawnDestination-1].position;
        }

        if (collision.tag == "spawn" && respawnDestination == 0)
        {
            respawnDestination = 1;
            collision.gameObject.SetActive(false);
        }
        else if (collision.tag == "spawn" && respawnDestination == 1)
        {
            respawnDestination = 2;
            collision.gameObject.SetActive(false);
        }
        else if (collision.tag == "spawn" && respawnDestination == 2)
        {
            respawnDestination = 3;
            collision.gameObject.SetActive(false);
        }
        else if (collision.tag == "spawn" && respawnDestination == 3)
        {
            respawnDestination = 4;
            collision.gameObject.SetActive(false);
        }

        if (collision.tag == "levelend")
        {
            cam.LevelEnd(true);
            collision.gameObject.SetActive(false);
        }

    }

    void CheckAttack(Vector3 pos, float scale)
    {
        if (attack && !strum)
        {
            hitSquare.SetActive(true);

            Invoke("SetFalse", 1.0f);

            CheckEnemyRays(pos, scale);
        }
    }

    void SetFalse() { hitSquare.SetActive(false); }

    void UpdatePlayerPos()
    {
        Vector3 pos = transform.localPosition;
        Vector3 scale = transform.localScale;
        CheckAttack(pos, scale.x);
        
            if (walk)
            {
                if (walk_left)
                {
                    pos.x -= velocity.x * Time.deltaTime;

                    scale.x = -2;
                }

                if (walk_right)
                {
                    pos.x += velocity.x * Time.deltaTime;

                    scale.x = 2;
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

        
        if (KnockFromRight == true && KBCooldown > 0)
        {
            Debug.Log("kb trigger");
            velocity2 = new Vector2(-KBForce, KBForce);
            velocity2 *= Time.deltaTime;
            pos += new Vector3(velocity2.x, velocity2.y, pos.z);
            
        }
        if (KnockFromRight == false && KBCooldown > 0)
        {
            Debug.Log("kb trigger");
            velocity2 = new Vector2(KBForce, KBForce);
            velocity2 *= Time.deltaTime;
            pos += new Vector3(velocity2.x, velocity2.y, pos.z);
            
        }

        if (velocity.y <= 0)
            pos = CheckFloorRays(pos);

        if (velocity.y >= 0)
            pos = CheckCeilingRays(pos);

        transform.localPosition = pos;
        transform.localScale = scale;

    }

    public void KnockedBackCoolDown()
    {
        if(KBCounter > 0)
        {
            KBCounter--;
        }

        if (KBCooldown > 0)
        {
            KBCooldown--;
        }
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
        bool input_left = Input.GetKey(KeyCode.A);
        bool input_right = Input.GetKey(KeyCode.D);
        bool input_space = Input.GetKeyDown(KeyCode.Space);
        bool input_k = Input.GetKeyDown(KeyCode.K);

        walk = input_left || input_right;
        walk_left = input_left && !walk_right;
        walk_right = !input_left && input_right;
        jump = input_space;
        attack = input_k;
         
    }

    void CheckEnemyRays(Vector3 pos, float direction)
    {
        Vector2 enemyVec = new Vector2(pos.x + direction * 1f, pos.y);

        RaycastHit2D enemyRay = Physics2D.Raycast(enemyVec, new Vector2(direction, 0), velocity.x * Time.deltaTime, enemyMask);
        if (enemyRay.collider != null)
        {
            // For Enemies
            if (enemyRay.collider.tag == "Enemy")
            {
                Debug.Log("Hit enemy");

                if (theOT.CheckTime())
                {
                    Destroy(enemyRay.collider.gameObject);
                }

            }

            // For Birds
            if (enemyRay.collider.tag == "Bird")
            {
                Debug.Log("Hit bird");

                enemyRay.collider.gameObject.GetComponent<BirdPunt>().isrunning = true;


            }
        }
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

    public void SwapState()
    {
        strum = !strum;
    }
}
