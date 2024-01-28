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

    public ScoreManager theSM;
    //use if parried
    private Vector2 newDirection;

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
            if (other.GetComponent<Player>() != null)
            {
                theSM.UpdateScore(-10);
                print("hit by ball");
            }
        }
        catch (System.Exception e) { }
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
