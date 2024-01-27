using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{

    
    public int attackDamage;
    public int moveSpeed;
    public int jumpForce;
    public bool strumming;
    public bool miniGame;

    public GameObject player;
    public Rigidbody2D rb;
    public static Player instance;

    private bool attacking;
    private float move;


    void Start()
    {
        if(attackDamage == 0)
            attackDamage = 10;

        if(moveSpeed == 0) 
            moveSpeed = 3;

        if (strumming == true)
            strumming = false;

        if (miniGame == true)
            miniGame = false;

        if(jumpForce == 0)
            jumpForce = 5;

        if(move == 0) 
            move = 0;

        if(attacking == true)
            attacking = false;
    }

    void Update()
    {
        move = Input.GetAxis("Horizontal");
        
        if (!miniGame)
        {
            if (Input.GetKeyDown("k"))
                attacking = true;
            var movement = Input.GetAxis("Horizontal");
            transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * moveSpeed;

            if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < 0.001f)
            {
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Enemy" && attacking)
        {
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
}
