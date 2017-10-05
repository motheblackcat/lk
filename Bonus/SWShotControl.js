// This script control the secondary weapon throwing system.
var item : GameObject;
var throwSound : AudioClip;
var fireRate = 0.5;
var nextFire : float;
var swDamage : float;

function Update () {
    var shotSpawn = GetComponent(Transform);
    var audioSource = GetComponent(AudioSource);

    var playerSprite = GameObject.FindWithTag("Player").GetComponent(SpriteRenderer);

    if (playerSprite.flipX) {
        transform.localPosition.x = -0.5;
    }
    else {
        transform.localPosition.x = 0.5;
    }

    if (Input.GetButtonDown("SWeapon") && Time.time > nextFire && !GameObject.Find("UICanvas").GetComponent(PauseMenu).paused)
    {
        audioSource.PlayOneShot (throwSound);
        nextFire = Time.time + fireRate;
        Instantiate(item, shotSpawn.position, shotSpawn.rotation);
    }
}