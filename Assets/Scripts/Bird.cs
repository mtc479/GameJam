using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Bird : MonoBehaviour
{
    private Vector3 vel = new Vector3(21f, 20.40f, 0f);
    private Vector3 accel;
    private float mass = 2f;
    private float vely = 0, velx = 0;
    private bool hit = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            hit = true;
        }

        if (hit)
        {
            ApplyForces();

            vely += transform.position.y + vel.y * Time.deltaTime + ((0.5f) * accel.y * Time.deltaTime * Time.deltaTime);

            vely += accel.y * Time.deltaTime;

            velx += transform.position.x + vel.x * Time.deltaTime + ((0.5f) * accel.x * Time.deltaTime * Time.deltaTime);

            velx += accel.x * Time.deltaTime;

            transform.position = new Vector3(velx, vely, transform.position.z);
        }
     
        
    }



    private void ApplyForces()
    {
        Vector3 force = new Vector3(0f, -9.8f, 0f);
        accel.y = force.y * mass;
        accel.y += -0.2f * vel.y / mass;
        accel.x = force.x * mass;
        accel.x += -0.2f * vel.x / mass;
    }

}
