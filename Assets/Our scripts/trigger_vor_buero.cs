using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger_vor_buero : MonoBehaviour
{
	
	public GameObject Teekueche;
	public GameObject Buero;

	void OnTriggerEnter (Collider other)
	{	
		Teekueche.SetActive(false);
		Buero.SetActive(true);
	}
}
