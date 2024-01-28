using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 7.5f;
    [SerializeField] private Rigidbody2D theRB;
    [SerializeField] private Transform sprite;
    [SerializeField] private Collider2D theC;
    public Vector3 direction;
    private bool parried = false;
    public ScoreManager theSM;
    //use if parried
    private Vector2 newDirection;
    private LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        transform.up = direction;
        theSM = (ScoreManager)FindFirstObjectByType(typeof(ScoreManager));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        try
        {
            // Reduce Points
            if (other.GetComponent<Player>() != null && !parried)
            {
                theSM.UpdateScore(-10);
                print("hit by ball");
                Destroy(gameObject);
            }
            else if (other.name == "HitSquare" && !parried)
            {
                theSM.UpdateScore(20);
                print("parried");
                // Include enemy layer
                speed = -speed;
                parried = true;
            }
            else if (other.tag == "Enemy" && parried)
            {
                theSM.UpdateScore(10);
                print("enemy hit with ball");
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        catch (System.Exception e) { }
        
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
