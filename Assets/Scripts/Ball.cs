using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 7.5f;
    public Rigidbody2D theRB;
    public Transform sprite;
    private Vector3 direction;
    private Vector2 newDirection;

    // Start is called before the first frame update
    void Start()
    {
        direction = Player.instance.transform.position - transform.position;
        direction.Normalize();
        transform.up = direction;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
        try
        {
            // Reduce Points
        }
        catch (System.Exception e) { }

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
