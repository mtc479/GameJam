using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugglerController : MonoBehaviour
{

    public Rigidbody2D theRB;
    public float moveSpeed;
    public SpriteRenderer theSR;
    public Animator theAnim;
    public int health = 150;
    public GameObject enemyDamaged;

    public float rangeToChasePlayer;
    private Vector3 moveDirection;

    public bool shouldShoot;
    public GameObject bullet;
    public Transform firePoint;
    public float fireRate;
    private float fireCounter;
    public float shootRange;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ((theSR.isVisible) && (PlayerController.instance.gameObject.activeInHierarchy))
        {
            // Moving
            // Check if player is within range
            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChasePlayer)
            {
                moveDirection = PlayerController.instance.transform.position - transform.position;
                shouldShoot = true;
            }
            else
            {
                moveDirection = Vector3.zero;
            }

            moveDirection.Normalize();
            theRB.velocity = moveDirection * moveSpeed;


            // shooting
            if (shouldShoot && Vector3.Distance(transform.position, PlayerController.instance.transform.position) < shootRange)
            {
                fireCounter -= Time.deltaTime;
                if (fireCounter <= 0)
                {

                    fireCounter = fireRate;
                    theAnim.SetTrigger("isShooting");
                    Instantiate(bullet, firePoint.position, firePoint.rotation);

                }
            }
        }
        else
        {
            theRB.velocity = Vector2.zero;
        }

        // ANIMATIONS

        // moving animation
        if (moveDirection != Vector3.zero)
        {
            theAnim.SetBool("isMoving", true);
        }
        else
        {
            theAnim.SetBool("isMoving", false);
        }

        if (moveDirection.x < 0) { theSR.flipX = true; } else if (moveDirection.x > 0) { theSR.flipX = false; }

    }

    public void DamageEnemy(int damage)
    {
        health -= damage;
        Instantiate(enemyDamaged, transform.position, transform.rotation);

        if (health <= 0)
        {
            theAnim.SetBool("isDead", true);
            moveSpeed = 0;
        }
    }


    public void Die()
    {
        Destroy(gameObject);
    }

}
