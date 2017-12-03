using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {
	GameObject enemy;
	public int pushX = 10;
	public int pushY = 10;
	Image healthBar;
	public bool isDead;

	void Start() {
		healthBar = GameObject.Find("Content").GetComponent<Image>();
	}

	void Update() {
		if(healthBar.fillAmount <= 0) {
			StartCoroutine(Death());
		}
	}

	// TODO: Fix x push, only y push is working
	void PushBack(GameObject enemy) {
		if (enemy.transform.position.x > transform.position.x) {
			GetComponent<Rigidbody2D>().AddForce(new Vector2(-pushX, pushY), ForceMode2D.Impulse);
		} else {
			GetComponent<Rigidbody2D>().AddForce(new Vector2(pushX, pushY), ForceMode2D.Impulse);			
		}
	}
	
	IEnumerator Death() {
		isDead = true;
		GetComponent<PlayerControl>().canMove = false;
		yield return new WaitForSeconds(3);
		SceneManager.LoadScene("Scene_1_RoadtoForest");
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Enemy") {
			PushBack(other.gameObject);
			healthBar.fillAmount -= 0.25f;
		}
	}
}
