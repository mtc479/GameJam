using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public ScoreManager theSM;
    [SerializeField] private Transform theT;
    [SerializeField] private Collider2D theC;

    private void OnTriggerEnter2D(Collider2D other)
    {
        try
        {
            // Reduce Points
            if (other.GetComponent<Player>() != null)
            {
                theSM.UpdateScore(-10);
                print("hit by FJ");
                theC.enabled = false;
            }
        }
        catch (System.Exception e) { }
        
    }

    private void OnBecameInvisible()
    {
        if(transform.position.y < 0)
        {
            Destroy(gameObject);
        }
        
    }
}
