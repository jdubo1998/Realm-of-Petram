using UnityEngine;
using UnityEngine.EventSystems;

public class AimJoystick : Joystick {
	Vector2 joystickPosition = Vector2.zero;
    private Camera cam = new Camera();
	
	float down_T = -1.0f;
	float tap_T = 0.0f;
	bool fire2 = false;
	
    void Start() {
        joystickPosition = RectTransformUtility.WorldToScreenPoint(cam, background.position);
    }
	
	public void fire() {
		if (fire2) {
			fire2 = false;
			Debug.Log("Fire 2");
		} else {
			Debug.Log("Fire 1");
		}
	}

    public override void OnDrag(PointerEventData eventData) {
        Vector2 direction = eventData.position - joystickPosition;
        inputVector = (direction.magnitude > background.sizeDelta.x / 2f) ? direction.normalized : direction / (background.sizeDelta.x / 2f);
        ClampJoystick();
        handle.anchoredPosition = (inputVector * background.sizeDelta.x / 2f) * handleLimit;
    }
	
	public override void OnPointerDown(PointerEventData eventData) {
        OnDrag(eventData);
		tap_T = Time.time;
    }

    public override void OnPointerUp(PointerEventData eventData) {
        inputVector = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
		if ((Time.time - tap_T) < .2) {
			if (down_T == -1.0f) {
				down_T = Time.time;
			} else if ((Time.time - down_T) < .3) {
				down_T = -1.0f;
				fire2 = true;
			} else {
				down_T = -1.0f;
			}
		}
    }
}
