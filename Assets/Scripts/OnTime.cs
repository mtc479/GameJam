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
    private int quarter = 1;
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
            if (beatTime == TimeState.onTime) {
                print("great!");
                theSM.UpdateScore(10);
            } else if (beatTime == TimeState.late)
            {
                print("late!");
                theSM.UpdateScore(5);
            } else if(beatTime == TimeState.early)
            {
                print("early!");
                theSM.UpdateScore(5);
            }
            else if (beatTime == TimeState.offTime)
            {
                print("miss!");
                theSM.UpdateScore(-10);
            }

        };
    }


    public void OpenWindow()
    {
        switch (beatTime)
        {
            case TimeState.onTime:
                beatTime = TimeState.late;
                print("late");
                break;
            case TimeState.late:
                beatTime = TimeState.offTime;
                print("off");
                break;
            case TimeState.offTime:
                beatTime = TimeState.early;
                print("early");
                break;
            case TimeState.early:
                beatTime = TimeState.onTime;
                print("on");
                break;

                //StartCoroutine(Window());
        }
    }

    //IEnumerator Window()
    //{
    //    yield return new WaitForSeconds(0.200f);
    //    onTime = true;
    //    yield return new WaitForSeconds(0.135f);
    //    onTime = false;
    //    slow = true;
    //    yield return new WaitForSeconds(0.135f);
    //    slow = false;
    //}
}
