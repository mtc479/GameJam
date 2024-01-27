using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Juggler : MonoBehaviour
{

    public Rigidbody2D theRB;
    public SpriteRenderer theSR;
    public Animator theAnim;
    public int health = 150;
    public GameObject enemyDamaged;

    public GameObject ball;
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
        if ((theSR.isVisible) && (Player.instance.gameObject.activeInHierarchy))
        {
            // throwing
            if (Vector3.Distance(transform.position, Player.instance.transform.position) < shootRange)
            {
                fireCounter -= Time.deltaTime;
                if (fireCounter <= 0)
                {

                    fireCounter = fireRate;
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
