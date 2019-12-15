using UnityEngine;

public class GlobalManager : MonoBehaviour {
    public bool isGamepad = false;

    void Update() {
        foreach (string gamepad in Input.GetJoystickNames()) {
            isGamepad = gamepad != "" ? true : false;
        }
    }
}