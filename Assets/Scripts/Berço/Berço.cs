using UnityEngine;
using System.Collections;

public class Berço : MonoBehaviour {
	public bool empty;
	private Animator _animation;
	
	void OnTriggerEnter2D(Collider2D coll) {
		
	}
	
	void Awake (){
		empty = true;
		_animation = gameObject.GetComponent <Animator> ();
	}
}
