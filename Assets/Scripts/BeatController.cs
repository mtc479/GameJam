using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BeatController : MonoBehaviour
{
    [SerializeField] private float bpm;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Intervals[] intervals;

    // Start is called before the first frame update
    void Start()
    {
        audioSource.Play();
    }

    // Update is called once per frame
    private void Update()
    {
        foreach (Intervals interval in intervals)
        {
            float sampledTime = (audioSource.timeSamples / (audioSource.clip.frequency * interval.GetBeatLength(bpm)));
            interval.CheckForNewInterval(sampledTime);
        }
    }

    // SETS BPM
    public void SetBpm(int _bpm)
    {
        bpm = _bpm;
    }

    // INTERVAL CLASS
    [System.Serializable]
    public class Intervals
    {
        [SerializeField] private float steps;
        [SerializeField] private UnityEvent trigger;
        private int lastInterval;

        public float GetBeatLength(float _bpm)
        {
            return 60f / (_bpm * steps);
        }

        public void CheckForNewInterval(float _interval)
        {
            if (Mathf.FloorToInt(_interval) != lastInterval)
            {
                lastInterval = Mathf.FloorToInt(_interval);
                trigger.Invoke();
            }
        }
    }

}
