using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCamera : MonoBehaviour
{
    [SerializeField] private Collider2D camZone;
    [SerializeField] private CameraFollow theCam;
    [SerializeField] private Transform leftWall;
    [SerializeField] private Transform rightWall;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("player detected");
            theCam.GetComponent<CameraFollow>().leftBounds = leftWall;
            theCam.leftBounds = leftWall;
            theCam.rightBounds = rightWall;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
