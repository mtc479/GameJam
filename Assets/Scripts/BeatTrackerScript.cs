using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatTrackerScript : MonoBehaviour
{
    [SerializeField] SpriteRenderer theSR;
    [SerializeField] private Sprite[] images = new Sprite[4];
    private int currentSprite = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextBeat()
    {
        print("beat");
        if(currentSprite < images.Length)
        {
            theSR.sprite = images[currentSprite];
            currentSprite++;
        }
        else
        {
            currentSprite = 0;
            theSR.sprite = images[currentSprite];
            currentSprite++;
        }
    }
}
