using UnityEngine;
using System.Collections;

public class preventOverFlow : MonoBehaviour {
	// Update is called once per frame
	Vector2 pos;
	NavioController _controller;
	
	void Start (){
		_controller = GameObject.FindGameObjectWithTag("GameController").GetComponent <NavioController> ();
	}
	
	void Update () {
		pos = transform.position;
		RaycastHit2D hit = Physics2D.Raycast (pos, -Vector2.right, 2f);
		if (hit.collider != null){
			_controller.canRespawn = false;
		}else {
			_controller.canRespawn = true;
		}
		Debug.DrawRay (transform.position, new Vector2 (-2f, 0f), Color.red);
	}
}
