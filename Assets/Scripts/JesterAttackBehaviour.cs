using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JesterAttackBehaviour : MonoBehaviour
{
    public Player playerMovement;
    public ScoreManager theSM;
    public bool canHit = true;

    public float swingRate;
    private float swingCounter;



    private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
            Debug.Log("swing");
            // knockback stuff
            if (playerMovement.KBCooldown == 0)
                {
                Debug.Log("in kb");
                //playerMovement.KBCounter = 2;
                playerMovement.KBCooldown = 2;
                if (collision.transform.position.x <= transform.position.x)
                    {
                        playerMovement.KnockFromRight = true;
                    }
                    if (collision.transform.position.x > transform.position.x)
                    {
                        playerMovement.KnockFromRight = false;
                    }

                //make take damagep pls
                theSM.UpdateScore(-20);
                canHit = false;
                swingCounter = 4;
                Debug.Log("hit");
                }
            }
        }

    public void CountDown()
    {
        if (swingCounter > 0)
        {
            swingCounter--;
        }
        else
        {
            canHit = true;
        }

    }


}
