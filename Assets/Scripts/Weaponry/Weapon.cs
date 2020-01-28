using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
	public int Ammo;
	public GameObject player;
	
	int damage;
	
	
	void Start() {
	}

    void Update() {
		rotate();
    }
	
	void rotate() {
		transform.rotation = player.transform.rotation;
		transform.position = player.transform.position;
	}
}
