using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class BirdPunt : MonoBehaviour
{
    public Transform transform;
    public Vector2 acc;
    public float mass;
    public float drag;
    public Vector2 vel;
    public float nextposition;
    public Vector2 pos;
    public Vector2 grav = new Vector2(-30f, -9.8f);
    public bool isrunning = false;


    public void applyForces(Vector2 force)
    {
        // Implement Newton's second law
        acc.y = force.y * mass;
        acc.y += drag * vel.y / mass;
        acc.x = force.x / mass;
        acc.x += drag * vel.x / mass;
    }
    void updatePos(float deltaTime)
    {

        float nextposition = pos.y + vel.y * deltaTime + (0.5f * acc.y * deltaTime);
        if (nextposition > 1f)
        {
            pos.y += vel.y * deltaTime + (0.5f * acc.y * deltaTime);
            vel.y += acc.y * deltaTime;
            pos.x += vel.x * deltaTime + (0.5f * acc.x * deltaTime);
            vel.x += acc.x * deltaTime;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        pos.y = transform.position.y;
        pos.x = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("g"))
        {
            isrunning = !isrunning;
        }
        if (isrunning)
        {
            applyForces(grav);
            updatePos(Time.deltaTime);
        }
        transform.position = pos;
        .

    }
}
