using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {
	public GameObject player;
	
	Ray ray;	
	RaycastHit hit;
	
    void Start() {
        
    }
	
    void Update() {
        ray = new Ray(transform.position, transform.forward);
		
		if (GetComponent<Weapon>().player.GetComponent<Movement>().firing) {
			Debug.Log("FIRE FIRE");
			if (Physics.Raycast(ray, out hit, 100f)) {
			} else {
			}
		}
    }
}
