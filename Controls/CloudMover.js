var speed = 1.0;

function Update() {
    transform.Translate(Vector2.left * speed * Time.deltaTime);
}