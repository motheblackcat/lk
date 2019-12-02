using UnityEngine;
using UnityEngine.UI;

public class BobAxeEvent : MonoBehaviour {
    Image dialogBox;
    GameObject axe;
    bool wasOpened = false;
    bool wasClosed = false;

    void Start() {
        dialogBox = GameObject.Find("DialogBox").GetComponent<Image>();
        axe = Resources.Load("Sweapons/Axe")as GameObject;
    }

    void Update() {
        // TODO: Make a general way to manage quests states
        if (dialogBox.enabled)wasOpened = true;
        wasClosed = wasOpened && !dialogBox.enabled;
        if (wasClosed) {
            GameObject.Find("Player").GetComponent<PlayerSWeapons>().sWeapons.Add(axe);
            wasOpened = false;
            wasClosed = false;
        }
    }
}