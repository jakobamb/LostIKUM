using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger_vor_teekueche : MonoBehaviour
{
	public GameObject Teekueche;
	public GameObject Buero;
	public SchlossDoor Tuer_Buero;
	public SchlossDoor Tuer_Teekueche;

	void OnTriggerEnter (Collider other)
	{
		Teekueche.SetActive(true);
		Buero.SetActive(false);
		Tuer_Buero.locked = true;
		Tuer_Teekueche.locked = false;
	}
}
