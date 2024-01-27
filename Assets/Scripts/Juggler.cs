using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Juggler : MonoBehaviour
{

    public Rigidbody2D theRB;
    public SpriteRenderer theSR;
    public Animator theAnim;
    public int health = 1;
    public GameObject enemyDamaged;
    public GameObject player;

    public Ball ball;
    public Transform firePoint;
    public float fireRate;
    private float fireCounter;
    public float shootRange;
    bool thrown = false;

    // Start is called before the first frame update
    void Start()
    {
        fireCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if ((theSR.isVisible) && (player.gameObject.activeInHierarchy))
        {
            // throwing
            if (Vector3.Distance(transform.position, player.transform.position) < shootRange)
            {
                if (fireCounter == 0 && !thrown)
                {
                    thrown = true;
                    Vector3 direction = player.transform.position - transform.position;
                    direction.Normalize();
                    ball.direction = direction;
                    //theAnim.SetTrigger("isThrowing");
                    Instantiate(ball, firePoint.position, firePoint.rotation);

                }
            }
        }
        else
        {
            theRB.velocity = Vector2.zero;
        }

    }

    public void CountDown()
    {
        if (fireCounter != 7)
        {
            fireCounter++;
        }
        else
        {
            thrown = false;
            fireCounter = 0;
        }
        
    }

    public void DamageEnemy()
    {
        health -= 1;
        Instantiate(enemyDamaged, transform.position, transform.rotation);

        if (health <= 0)
        {
            theAnim.SetBool("isDead", true);
        }
    }


    public void Die()
    {
        Destroy(gameObject);
    }

}
