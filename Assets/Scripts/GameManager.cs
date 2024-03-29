using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState { SideScroll, MiniGame}
    public GameState state = GameState.SideScroll;
    [SerializeField] private Player player;
    [SerializeField] private BeatController theBC;
    private bool started = false;
    private Object template;
    // Start is called before the first frame update
    void Start()
    {
        theBC.audioSource.Stop();
        theBC.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && !started)
        {
            theBC.audioSource.PlayDelayed(0.4f);
            theBC.enabled = true;
            started = true;
        }

        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    theBC.audioSource.PlayDelayed(0.4f);
        //    theBC.enabled = true;
        //}

        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    ChangeClock();
        //}
    }

    //private void ChangeClock()
    //{
    //    clock = !clock;
    //    if (clock)
    //    {
    //        print("turning on clock");
    //        theBC.audioSource.PlayDelayed(0.0f);
    //        theBC.enabled = true;
            
    //    }
    //    else
    //    {
    //        print("turning off clock");
    //        theBC.audioSource.Stop();
    //        theBC.enabled = false;
    //    }
    //}
}
