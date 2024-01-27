using System.Collections;
using System.Collections.Generic;
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
    }

    void Update()
    {
        move = Input.GetAxis("Horizontal");
        
        if (!miniGame)
        {
            if (Input.GetKeyDown("k"))
                Attack();
            var movement = Input.GetAxis("Horizontal");
            transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * moveSpeed;

            if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < 0.001f)
            {
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }

        }
        if (strumming) 
            Strum();

    }

    void Parry()
    {

    }

    void Attack()
    {
        Debug.Log("Attack!");
    }

    void Strum()
    {

        if(!miniGame)
            strumming = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Bard")
        {
            if (Input.GetKeyDown("k"))
                strumming = true;
        }
    }
}
