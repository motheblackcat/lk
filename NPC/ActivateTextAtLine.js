var theText : TextAsset;
var startLine : int;
var endLine : int;
var requireButtonPress : boolean;
private var waitForPress : boolean;
var hasBeenPressed : boolean;

function Update() {
    var textBoxManager = GameObject.Find("TextBoxManager").GetComponent(NPCDialog);
    if(waitForPress && Input.GetAxis("Vertical") >= 0.1) {
        if (!hasBeenPressed) {
        textBoxManager.ReloadScript(theText);
        textBoxManager.currentLine = startLine;
        textBoxManager.endAtLine = endLine;
        textBoxManager.EnableTextBox();
        hasBeenPressed = true;
        }        
    }
    if (textBoxManager.currentLine > textBoxManager.endAtLine) {
        hasBeenPressed = false;
    }
    if (textBoxManager.isTyping) {
        GetComponent(Animator).SetBool("talk", true);
    }
    else {
        GetComponent(Animator).SetBool("talk", false);
    }
}

function OnTriggerEnter2D (other : Collider2D) {
    var textBoxManager = GameObject.Find("TextBoxManager").GetComponent(NPCDialog);
    if(other.tag == "Player") {
        if(requireButtonPress) {
            waitForPress = true;
            return;
        }
        textBoxManager.ReloadScript(theText);
        textBoxManager.currentLine = startLine;
        textBoxManager.endAtLine = endLine;
        textBoxManager.EnableTextBox();
    }
}

function OnTriggerExit2D(other : Collider2D) {
    if(other.tag =="Player") {
        waitForPress = false;
    }
}