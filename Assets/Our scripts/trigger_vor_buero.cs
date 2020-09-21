using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger_vor_buero : MonoBehaviour
{
	
	public GameObject Teekueche;
	public GameObject Buero;
	public SchlossDoor Tuer_Buero;
	public SchlossDoor Tuer_Teekueche;


	void OnTriggerEnter (Collider other)
	{	
		if (other.gameObject.CompareTag("Player")) {
			Teekueche.SetActive(false);
			Buero.SetActive(true);
			Tuer_Buero.locked = false;
			Tuer_Teekueche.locked = true;
			Debug.Log("Bürotrigger Collided!");
		}
	}
	
}
