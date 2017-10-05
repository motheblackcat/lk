// This script manage the Secondary Weapon Bonuses.
import UnityEngine.UI;

var pickupSound : AudioClip;
private var bonusIcon : Image;
private var swIcons : Image[];

var axe : GameObject;
var dagger : GameObject;

function Start() {
    swIcons = new Image[2];
    swIcons[0] = GameObject.Find("AxeIcon").GetComponent(Image);
    swIcons[1] = GameObject.Find("DaggerIcon").GetComponent(Image);
}

function OnTriggerEnter2D(other : Collider2D) {    
    var shotControl = GameObject.Find("ShotSpawn").GetComponent(SWShotControl);
    var audioSource = GetComponent(AudioSource);
    var bonusSprite = GetComponent(SpriteRenderer);

    if (other.gameObject.tag == "Player") {
        audioSource.PlayOneShot(pickupSound);
        shotControl.enabled = true;
        bonusSprite.enabled = false;              
        for (var icons : Image in swIcons) {
            icons.enabled = false;
        }

        if (tag == "AxeBonus") {
            bonusIcon = swIcons[0];
            bonusIcon.enabled = true;
            shotControl.item = axe;
            shotControl.swDamage = 5; 
        }

        if (tag == "DaggerBonus") {
            bonusIcon = swIcons[1];
            bonusIcon.enabled = true;
            shotControl.item = dagger;
            shotControl.swDamage = 0.5;
        }

        yield WaitForSeconds(0.5);
        Destroy(gameObject);
    }
}