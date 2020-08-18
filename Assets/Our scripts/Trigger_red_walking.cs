using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_red_walking : MonoBehaviour
{
	
	public GameObject Teekueche;
	
	void OnTriggerEnter (Collider other)
	{	
		Teekueche.SetActive(true);
	}
}
