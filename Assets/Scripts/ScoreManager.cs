using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int score;
    [SerializeField] private UnityEvent subZero;
    public AudioSource audio;
    [SerializeField] private Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        UpdateScore(0);
    }

    public void UpdateScore(int modifier)
    {
        print("updating score");
        score += modifier;
        print(score.ToString());
        scoreText.text = score.ToString();
    }

    // Update is called once per frame
    /*void Update()
    {
        if(audio.time > audio.clip.length && score <= 0)
        {
            subZero.Invoke();
        }
    }*/
}
