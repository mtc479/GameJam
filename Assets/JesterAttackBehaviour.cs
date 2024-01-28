using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JesterAttackBehaviour : MonoBehaviour
{
    public Player playerMovement;
    public bool canHit;
   
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                if (playerMovement.KBCooldown <= 0)
                {
                    playerMovement.KBCounter = playerMovement.KBTotalTime;
                playerMovement.KBCooldown = 3.0f;
                    if (collision.transform.position.x <= transform.position.x)
                    {
                        playerMovement.KnockFromRight = true;
                    }
                    if (collision.transform.position.x > transform.position.x)
                    {
                        playerMovement.KnockFromRight = false;
                    }

                    //make take damagep pls

                    Debug.Log("hit");
                }
            }
        }


}
