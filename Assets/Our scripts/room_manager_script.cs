﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class room_manager_script : MonoBehaviour
{
    public GameObject Teekueche;
    public GameObject Overlay;
    public GameObject Schreibtisch;


    // Start is called before the first frame update
    void Start()
    {

        //deactivates Gameobject Teeküche
        Teekueche.SetActive(false);

        Overlay.SetActive(false);
        Schreibtisch.SetActive(true);
    }

}
