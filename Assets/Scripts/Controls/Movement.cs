using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour {
	public Joystick moveJS;
	public AimJoystick aimJS;
	public GameObject player;
	public GameObject configManager;
	
	Player Pprop;
	settingsConfig cfg;
	
	/* Floats for calculations */
	float smooth = 1.0f;
	float delta = 0.0f;
	float angle = 0;
	float aim2_R;
	
	float aimLock_T = 1.0f;
	
	Quaternion q;
	
	void Start() {
		q = player.transform.rotation;
		Pprop = player.GetComponent<Player>();
		cfg = configManager.GetComponent<settingsConfig>();
		
		aim2_R = cfg.aim2_T;
	}
	
	void Update() {
		delta = Time.deltaTime;
		aimLock_T -= delta;
		
		if (aimJS.Horizontal != 0 || aimJS.Vertical != 0) {
			aimLock_T = 1.0f;
			// Aims in direction of aiming joystick
			aim(aimJS, true);
		} else if ((moveJS.Horizontal != 0 || moveJS.Vertical != 0) && aimLock_T < 0) {
			// Aims in the direction of forward movement
			aim(moveJS, false);
		}
		
		move();
	}
	
	private void aim(Joystick js, bool firing) {
		angle = Mathf.Atan(js.Vertical / js.Horizontal) * Mathf.Rad2Deg;
		
		/* Angle of the JoyStick translated to the angle the player is looking in */
		if (js.Horizontal < 0 && js.Vertical > 0) {
			angle = (90 + angle) + 90;
		} else if (js.Horizontal < 0 && js.Vertical < 0) {
			angle += 180;
		} else if (js.Horizontal > 0 && js.Vertical < 0){
			angle = (90 + angle) + 270;
		}
			
		angle = 90-angle;
			
		q = Quaternion.Euler(0,angle, 0);
		
		// Rotates the player to the correct angle
		player.transform.rotation = Quaternion.Slerp(player.transform.rotation, q, 5.0f);
				
		if (firing) {
			aimJS.fire();
		}
	}
	
	private void move() {
		player.transform.Translate(Vector3.right * delta * 
				Pprop.moveSpeed * moveJS.Horizontal * smooth, Space.World);
		player.transform.Translate(Vector3.forward * delta * 
				Pprop.moveSpeed * moveJS.Vertical * smooth, Space.World);
	}
	
	/* Used for the interaction button. */
	public void interaction(int type) {
		switch(type){
			case 1: // Inveoked when a player jumps over an object, ends up on same level ground.
				Debug.Log("Hurdle Object");
				break;
			case 2: // Invoked when a player climbs onto and object, ends up on higher level ground.
				Debug.Log("Climbed Object");
				break;
			case 3: // Invoked when a player picks up ammo, or weaponry.
				Debug.Log("Picked Up Object");
				break;
			default: // Invoked when their is nothing to interact with.
				Debug.Log("No Interaction");
				break;
		}
		
	}
}
