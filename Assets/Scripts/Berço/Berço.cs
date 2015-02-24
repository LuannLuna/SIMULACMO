using UnityEngine;
using System.Collections;

public class Berço : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "HitBox"){
			gameObject.GetComponent<Animator> ().SetTrigger("start");
			coll.gameObject.GetComponentInParent<Moviment> ().canMove = false;
		} 	
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
