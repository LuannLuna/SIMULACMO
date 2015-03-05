using UnityEngine;
using System.Collections;

public class Berço : MonoBehaviour {
	public bool empty;
	private Animator _animation;
	GameObject _barco;
	private NavioController _controller;
	
	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Barco" && empty){
			_barco = coll.gameObject;
			Moviment _move = _barco.GetComponent <Moviment> ();
			_move.canAtrack = true;
			_move.berco = gameObject.transform;
			Destroy(_barco.GetComponent <BoxCollider2D> ());
			empty = false;
			_controller.barcos.Remove(_move.id);
			
		}else if (coll.gameObject.tag == "Barco" && !empty){
			coll.gameObject.GetComponent <Moviment> ().BackToOriginalPos(); 
		}
	}
	
	void Update(){
		if (_barco !=null){
			if (_barco.GetComponent <Transform> ().position == this.transform.position){
				_animation.SetTrigger ("start");
				_controller.StartAll ();
				_barco = null;
			}
		}	
	}
	
	void Awake (){
		empty = true;
		_animation = gameObject.GetComponent <Animator> ();
		_controller = GameObject.FindGameObjectWithTag("GameController").GetComponent <NavioController> ();
	}
}
