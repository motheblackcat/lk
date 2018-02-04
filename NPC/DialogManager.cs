using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {
	Text dialogText;
    public TextAsset arthDialogs;
	
	void Start () {
		dialogText = GameObject.Find("Text").GetComponent<Text>();
        dialogText.text = arthDialogs.text;
	}
	
	void Update () {
        checkDialogBox();
	}

    void textLineReader() {
        var lines = dialogText.text.Split("\n"[0]);
        Debug.Log(lines);
        foreach(var line in lines) {
            Debug.Log(line);
        }
    }

    // Make it so any NPC can activate the box with the correct language
    void checkDialogBox() {
        if (GameObject.Find("Bartender").GetComponent<Animator>().GetBool("talk")) {
            GameObject.Find("DialogBox").GetComponent<Image>().enabled = true;
            GameObject.Find("Text").GetComponent<Text>().enabled = true;
            GameObject.Find("ButtonA").GetComponent<Image>().enabled = true;
        } else {
            GameObject.Find("DialogBox").GetComponent<Image>().enabled = false;            
            GameObject.Find("Text").GetComponent<Text>().enabled = false;
            GameObject.Find("ButtonA").GetComponent<Image>().enabled = false;
        }
    }
}
