using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OnTime : MonoBehaviour
{
    public bool onTime = false;
    public bool slow = false;
    [SerializeField] private ScoreManager theSM;
    public bool minigame = false;
    private bool koolDown = false;
    private int spam;
    public enum TimeState { onTime, late, offTime, early}
    TimeState beatTime = TimeState.onTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) 
        {
            spam++;
            if(!koolDown)
            {
                koolDown = true;
                if (beatTime == TimeState.onTime)
                {
                    print("great!");
                    theSM.UpdateScore(10);
                }
                else if (beatTime == TimeState.late)
                {
                    print("late!");
                    theSM.UpdateScore(5);
                }
                else if (beatTime == TimeState.early)
                {
                    print("early!");
                    theSM.UpdateScore(5);
                }
                else if (beatTime == TimeState.offTime)
                {
                    print("miss!");
                    theSM.UpdateScore(-10);
                }
            }
            

        };
    }


    public void OpenWindow()
    {
        switch (beatTime)
        {
            case TimeState.onTime:
                beatTime = TimeState.late;
                break;
            case TimeState.late:
                beatTime = TimeState.offTime;
                break;
            case TimeState.offTime:
                beatTime = TimeState.early;
                break;
            case TimeState.early:
                koolDown = false;
                beatTime = TimeState.onTime;                
                if(spam > 3)
                {
                    theSM.UpdateScore(-100);
                }
                spam = 0;
                break;

        }
    }
}
