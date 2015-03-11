using UnityEngine;
using System.Collections;

public class Berco : MonoBehaviour {
	public bool empty;
	NavioMoviment navioAtracado;
	Controller _controll;

	private Animator _anim;
	// Use this for initialization
	void Awake ()
	{
		empty = true; 
		_anim = gameObject.GetComponent <Animator> ();
		_controll = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent <Controller> ();
	}

	private void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "Barco" && empty) {
			navioAtracado = coll.gameObject.GetComponent <NavioMoviment> ();
			navioAtracado._berco = _anim;
			navioAtracado.destroyItSelf = true;
			coll.gameObject.GetComponent <NavioMoviment> ().followMousePos = false;
			empty = false;
			Destroy (coll.gameObject.GetComponent <PolygonCollider2D> ());
		} else {
			coll.gameObject.GetComponent <NavioMoviment> ().backOriginalPos = true;
		}
		Debug.Log (coll.gameObject.name);
	}
	
	// Update is called once per frame
	private void Update () {
	
	}
}
