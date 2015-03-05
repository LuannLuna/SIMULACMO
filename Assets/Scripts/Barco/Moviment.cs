using UnityEngine;
using System.Collections;

public class Moviment : MonoBehaviour {
	public bool canMove;
	public bool atracou;
	public int id;
	public float speed;
	private NavioController _controller;
	public Vector2 orignalPos;
	private bool inicioFila;
	public Transform pointToAtrack;
	public Transform berco;
	public bool canAtrack;
	private bool step1;
	private bool step2;
	private Transform secondPoint;
	private bool reStart;
	public Transform son;
	public float sizeOfRay;
	
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
			_controller.canRespawn = true;
			_controller.StartAll ();
		}
	}
	
	public void BackToOriginalPos (){ 
		if (!atracou){
			transform.position = orignalPos;
			canMove = true;
			_controller.canRespawn = true;
			_controller.StartAll ();
		}
	}
	
	public void Awake (){
		canMove = true;
		atracou = false; 
		inicioFila = false;
		_controller = GameObject.FindGameObjectWithTag("GameController").GetComponent <NavioController> ();
		pointToAtrack = GameObject.Find("PointToAtrack").GetComponent <Transform> ();
		secondPoint = GameObject.Find("SecondPoint").GetComponent <Transform> ();
		canAtrack = false;
		step1 = false;
		step2 = false;
		reStart = false;
	}
	
	void GoToBerco(){
		if (canAtrack){	
			transform.position = Vector2.MoveTowards(transform.position, pointToAtrack.position, speed * Time.deltaTime * 2);
			Debug.Log ("Pos: " + transform.position + " - " + pointToAtrack.position); 
		}
		
		if (transform.position == pointToAtrack.position){
			step1 = true;
			canAtrack = false;
			transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y); 
		}
		
		if (step1){
			transform.position = Vector2.MoveTowards(transform.position, secondPoint.position, speed * Time.deltaTime * 2);
		}
		
		if (transform.position == secondPoint.position){
			step1 = false;
			step2 = true;
		}
		if (step2){
			transform.position = Vector2.MoveTowards(transform.position, berco.position, speed * Time.deltaTime * 2);
			Debug.Log ("Pos: " + transform.position + " + " + pointToAtrack.position); 
		}
		if (transform.position == pointToAtrack.position && !reStart){
			_controller.StartAll ();
			reStart = true;  
		}
	}
	
	public void Update(){
		
		Vector2 pos = son.position;
		Debug.DrawRay (pos, new Vector2 (-sizeOfRay, 0f), Color.green);
		RaycastHit2D hit = Physics2D.Raycast (pos, -Vector2.right, sizeOfRay);
		if (hit.collider == null && canMove){
			gameObject.transform.Translate (-speed * Time.deltaTime, 0f, 0f); 
			orignalPos = transform.position;
			canMove = true;
		}else if (hit.collider != null){
			if (hit.collider.gameObject.tag == "Barco" || hit.collider.gameObject.tag == "Limit"){
				canMove = false;
			}
			Debug.Log(hit.collider.gameObject.tag);
		}
		GoToBerco();
	}
}
