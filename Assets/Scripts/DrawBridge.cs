using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBridge : MonoBehaviour
{
    [SerializeField] private GameObject bridgeUp;
    [SerializeField] private GameObject bridgeDown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bird")
        {
            DrawBridgeOpen();
            Destroy(other.gameObject);
        }

        if (other.tag == "Player")
        {

        }
    }

    private void DrawBridgeOpen()
    {
        Debug.Log("Open the bridge");
        bridgeUp.SetActive(false);
        bridgeDown.SetActive(true);
    }
}
