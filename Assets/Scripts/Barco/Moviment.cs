using UnityEngine;
using System.Collections;

public class Moviment : MonoBehaviour {
	public bool canMove;
	public bool atracou;
	public int id;
	public float speed;
	private NavioController _controller;
	private Vector2 orignalPos;
	private bool inicioFila;
	
	void OnTriggerEnter2D (Collider2D coll){
		if (coll.gameObject.tag == "Limit" || coll.gameObject.tag == "Barco"){
			canMove = false;
			if (coll.gameObject.tag == "Limit")
				inicioFila = true;
		}
	}
	
	void OnMouseOver (){
		if (Input.GetKey (KeyCode.Mouse0)){
			canMove = false;
			Vector2 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			transform.position = mousePos;
			_controller.canRespawn = false;
			_controller.idSelected = id; 
			_controller.StopAll ();
			
		}
		if (Input.GetKeyUp (KeyCode.Mouse0) && !atracou){
			transform.position = orignalPos;
			canMove = true;
			_controller.canRespawn = true;
			_controller.StartAll ();
		}
	}
	
//	void OnMouseExit (){
//		if (!atracou){
//			transform.position = orignalPos;
//			canMove = true;
//			_controller.canRespawn = true;
//			_controller.StartAll ();
//		}
//	}
	
	public void Awake (){
		canMove = true;
		atracou = false; 
		inicioFila = false;
		_controller = GameObject.FindGameObjectWithTag("GameController").GetComponent <NavioController> ();
	}
	
	public void Update(){
		if (canMove && !inicioFila){
			gameObject.transform.Translate (-speed * Time.deltaTime, 0f, 0f); 
			orignalPos = transform.position;
		}
	}
}
