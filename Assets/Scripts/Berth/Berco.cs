using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Component;

public class Berco : MonoBehaviour {
	public bool empty;
	public GruopControll group;
	public float timeFull = 0;
	public Berth berthData;
	public Moviment ship;
	double velocidade;
	// Use this for initialization
	void Start () {
		group = GameObject.FindGameObjectWithTag("ToggleGroup").GetComponent <GruopControll> ();
		empty = true;
		velocidade = berthData.getBerthSpeed ();
	}

	public void Atracacao (){
		if (empty && group.selected != null){
			group.selected.gameObject.GetComponent <Moviment> ().canMove = false;
			group.selected.gameObject.GetComponent <BoxCollider2D> ().enabled = false;
			Destroy(group.selected.gameObject.GetComponent <Toggle> ());
			group.buttons.Remove(group.selected);
			group.selected.gameObject.GetComponent <Moviment> ().getout = true; 
			group.selected.gameObject.GetComponent <Moviment> ().Berco = gameObject;
			group.selected = null; 

		}
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponent <Button> ().interactable = empty;
		if (!empty) {
			timeFull += Time.deltaTime;
			for (int i = 0 ; i < GameController.instance.parameters.getNumMaterials() ; i++){

			}
		}
	}
}
