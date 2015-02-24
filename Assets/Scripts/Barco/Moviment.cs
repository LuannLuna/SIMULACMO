using UnityEngine;
using System.Collections;

public class Moviment : MonoBehaviour {
	public float speed;
	public bool canMove = true;
	// Use this for initialization
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Limit"  || coll.gameObject.tag == "Barco")
			canMove = false;
	}
	void OnMouseOver (){
		if (Input.GetKey(KeyCode.Mouse0)){
			Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition); 	
			transform.position = pos; 
			gameObject.GetComponent<Collider2D> ().isTrigger = true;
		}
		if (Input.GetKeyUp(KeyCode.Mouse0)){
			gameObject.GetComponent<Collider2D> ().isTrigger = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (canMove){
			transform.Translate ( -speed * Time.deltaTime, 0f, 0f);
		}
	}
}
