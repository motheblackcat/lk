using UnityEngine;

public class GlobalManager : MonoBehaviour {
    public bool isGamepad = false;

    // TODO: Check if it should be a singleton (loading a saved game / prefs)
    void Update() {
        foreach (string gamepad in Input.GetJoystickNames()) {
            isGamepad = gamepad != "" ? true : false;
        }
    }
}