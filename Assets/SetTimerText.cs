using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetTimerText : MonoBehaviour
{
    
    // The starttime in hh
    public room_manager_script timer;
    private float time;

    public GameObject terminText;
    public GameObject uhrzeitText;

    private Text termText;
    private Text uhrText;

    // Start is called before the first frame update
    void Start()
    {
        time = timer.time;
        termText = terminText.GetComponent<Text>();
        uhrText = uhrzeitText.GetComponent<Text>();

        termText.fontSize = 2;
        termText.alignment = TextAnchor.UpperLeft;
        termText.text = "23:20   Büro\nNachtwache + Sp.Matrix init.\n\n00:30   Büro\nTablette vorbereiten\n\n02:30   H130\nGTA5 mit Dr.Prof. S";
        uhrText.fontSize = 2;
        uhrText.alignment = TextAnchor.LowerRight;

    }

    // Update is called once per frame
    void Update()
    {
        time = timer.time;
        string hours = Mathf.Floor(time / 1440).ToString("00");
        string minutes = (Mathf.Floor(time / 60)%24).ToString("00");
        string seconds = Mathf.Floor(time%60).ToString("00");
        string timerString = string.Format("{0}:{1}:{2}", hours, minutes, seconds);
        uhrText.text = timerString;
    }
}

