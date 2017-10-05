// This script apply parallax to the backgrounds.
var backgrounds : Transform[];
private var parScales : float[];
var smoothing = 1f;
private var cam : Transform;
private var prevCamPos : Vector3;

function Awake() {
    cam = Camera.main.transform;
}

function Start () {
    prevCamPos = cam.position;
    parScales = new float[backgrounds.Length];
    for (var i = 0; i < backgrounds.Length; i++) {
        parScales[i] = backgrounds[i].position.z*-1;
    }
}

function Update () {
    for (var i = 0; i < backgrounds.Length; i++) {
        var parallax : float = (prevCamPos.x - cam.position.x) * parScales[i];
        var backgroundTargetPosX : float = backgrounds[i].position.x + parallax;
        var backgroundTargetPos : Vector3 = new Vector3 (backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);
        backgrounds[i].position = Vector3.Lerp (backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
    }
    
    prevCamPos = cam.position;
}