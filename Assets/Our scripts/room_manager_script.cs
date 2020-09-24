using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class room_manager_script : MonoBehaviour
{
    public GameObject Teekueche;
    public GameObject Buero;
    public GameObject BueroMirrored;
    public GameObject Flur;

    public GameObject Overlay;
    public GameObject Intro1;
    public GameObject Intro2;
    public GameObject OutroFailure;
    public GameObject OutroSuccess;

    //time manager
    public int startTime = 23; //in hours
    public float time;
    //checks if game sections are already triggered
    bool intro2Triggered = false;
    bool mainTriggered = false;
    bool endTriggered = false;
    bool successTriggered = false;
    bool success = false;


    // Start is called before the first frame update
    void Start()
    {
        //time manager
        time = startTime * 1440;

        //deactivates Gameobjects
        Overlay.SetActive(false);
        Teekueche.SetActive(false);
        Buero.SetActive(false);
        BueroMirrored.SetActive(false);
        Flur.SetActive(false);
        OutroFailure.SetActive(false);
        OutroSuccess.SetActive(false);
        Intro1.SetActive(true);
        Intro2.SetActive(false);
    }

    void Update()
    {
        //time manager
        time = time + Time.deltaTime;

        float intro2Time = 10;
        float mainTime = 20;
        float endTime = 1200;
       

        //Intro2
        if(33120 + intro2Time < time && time < 33120 + mainTime && !intro2Triggered){
            Intro1.SetActive(false);
            Intro2.SetActive(true);
            intro2Triggered = true;
        }
        //EscapeRoom
        if(33120 + mainTime < time && time < 33120 + endTime && !mainTriggered ){
            Intro1.SetActive(false);
            Intro2.SetActive(false);
            Buero.SetActive(true);
            Flur.SetActive(true);
            mainTriggered = true;
        }

        //Stop
        if(33120 + endTime < time && !success && !endTriggered){
            Teekueche.SetActive(false);
            Buero.SetActive(false);
            BueroMirrored.SetActive(false);
            Flur.SetActive(false);
            OutroFailure.SetActive(true);
            endTriggered=true;
        }

        if(success && !successTriggered){
            successTriggered = true;
            Teekueche.SetActive(false);
            Buero.SetActive(false);
            Flur.SetActive(false);
            OutroSuccess.SetActive(true);
        }
    }
}
