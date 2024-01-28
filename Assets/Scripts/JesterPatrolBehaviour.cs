using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class JesterPatrolBehaviour : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float moveSpeed;
    public int patrolDestination;

    public Transform playerTransform;
    public bool isChasing;
    public float chaseDistance;
    
    
        
    
    

    // Update is called once per frame
    void Update()
    {
        Vector3 scale = transform.localScale;

        if (isChasing)
        {
            if (transform.position.x > playerTransform.position.x)
            {
                transform.position += Vector3.left * (moveSpeed +2f) * Time.deltaTime;
                scale.x *= (-1);
                transform.localScale = scale;
            }
            
            if (transform.position.x < playerTransform.position.x)
            {
                transform.position += Vector3.right * (moveSpeed +2f) * Time.deltaTime;
                scale.x *= (-1);
                transform.localScale = scale;
            }
            
        }
        else
        {
            if (Vector2.Distance(transform.position, playerTransform.position) < chaseDistance)
            {
                isChasing = true;
            }
           
            
        }
        if (isChasing)
        {
            if (transform.position.x > playerTransform.position.x)
            {
                scale.x = (-1);
                transform.localScale = scale;
            }

            else if(transform.position.x < playerTransform.position.x)
            {
                scale.x = (1);
                transform.localScale = scale;
            }

        }
        if (Vector2.Distance(transform.position, playerTransform.position) > chaseDistance)
        {
            isChasing = false;
        }
        if (!isChasing)
        {
            if (patrolDestination == 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, moveSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, patrolPoints[0].position) < .02f)
                {
                    patrolDestination = 1;
                }
            }

            if (patrolDestination == 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, moveSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, patrolPoints[1].position) < .02f)
                {
                    patrolDestination = 0;
                }

            }
        }
        
    }
}
