using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState { SideScroll, MiniGame}
    public GameState state = GameState.SideScroll;
    [SerializeField] private Player player;
    [SerializeField] private BeatController theBC;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
