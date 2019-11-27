using UnityEngine;
using UnityEngine.UI;

public class BobAxeEvent : MonoBehaviour {
    Image dialogBox;
    public bool opened = false;
    public bool closed = false;

    void Start() {
        dialogBox = GameObject.Find("DialogBox").GetComponent<Image>();
    }

    void Update() {
        //TODO: Refactor this draft dialog closing detection (add a general way to change states from dialogs in DialogManager)
        if (dialogBox.enabled)opened = true;
        if (opened) {
            if (!dialogBox.enabled)closed = true;
        }
        if (closed) {
            GameObject.FindWithTag("Player").GetComponent<PlayerSWeapons>().sWeapons.Add(Resources.Load("Sweapons/Axe")as GameObject);
        }
    }
}