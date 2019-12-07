using UnityEngine;

public class GlobalManager : MonoBehaviour {

    // TODO: Check if it should be a singleton (loading a saved game / prefs)
    public bool checkGamepad() {
        bool isGamepad = false;
        foreach (string gamepad in Input.GetJoystickNames()) {
            isGamepad = gamepad != "" ? true : false;
        }
        return isGamepad;
    }
}