using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger_vor_teekueche : MonoBehaviour
{
	public GameObject Teekueche;
	public GameObject Buero;

	void OnTriggerEnter (Collider other)
	{
		Teekueche.SetActive(true);
		Buero.SetActive(false);
	}
}
