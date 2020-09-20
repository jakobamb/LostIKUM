using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class TimerManager : MonoBehaviour
{
    public List<Text> _timerTexts = new List<Text>();
    // the start time in minutes
    public int _startTime = 30;
    public float _timeLeft;

    // Start is called before the first frame update
    void Start()
    {
        _timeLeft = _startTime * 60;
    }

    // Update is called once per frame
    void Update()
    {
        _timeLeft = _timeLeft - Time.deltaTime;

        string minutes = Mathf.Floor(_timeLeft / 60).ToString("00");
        string seconds = (_timeLeft % 60).ToString("00");

        string timerString = string.Format("{0}:{1}", minutes, seconds);
        updateClocks(timerString);
    }

    private void updateClocks(string text)
    {
        foreach (Text clock in _timerTexts)
        {
            clock.text = text;
        }
    }
}
