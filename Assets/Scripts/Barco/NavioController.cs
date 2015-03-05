using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NavioController : MonoBehaviour {
	
	public GameObject barcoModelo;
	public Transform mar;
	public Transform pointToRespawn;
	public double timeToRespawn;
	private double timer;
	public bool canRespawn; 
	private int id;
	public int idSelected;
	private int i = 0;
	
	public Dictionary <int, GameObject> barcos = new Dictionary<int, GameObject>	();
	
	public void Awake (){
		canRespawn = true;
		idSelected = -1;
	}
	
	public void Respawn (){
		if (Time.time > timer){
			timer = Time.time + timeToRespawn;
			GameObject barco = (GameObject)	Instantiate (barcoModelo, pointToRespawn.position, pointToRespawn.rotation);
			barco.GetComponent<Moviment> ().id = id;
			barco.transform.parent = mar;
			barcos.Add (id, barco);
			id ++;
		} 
	}
	
	 public void StopAll (){
	 	foreach (var barco in barcos){
	 		barco.Value.GetComponent <Moviment> ().canMove = false;
//	 		if (barco.Value.GetComponent <Moviment> ().id != idSelected){
//	 			barco.Value.GetComponent <BoxCollider2D> ().enabled = false;
//	 		}
	 	}
	 } 
	 
	 public void StartAll (){
	 	foreach (var barco in barcos){
	 		barco.Value.GetComponent <Moviment> ().canMove = true;
//			if (barco.Value.GetComponent <Moviment> ().id != idSelected){
//				barco.Value.GetComponent <BoxCollider2D> ().enabled = true;
//			}
	 	}
	 }
	
	public void RestarFila (){
		StartCoroutine (StarBoat());
	}
	
	IEnumerator StarBoat (){
		yield return new WaitForSeconds (1.5f);
		if (barcos[i] != null && i < barcos.Count){
			barcos[i].GetComponent <Moviment> ().canMove = true;
			i++;
			StarBoat ();
		}else if (i >= barcos.Count){
			i = 0;
		}
	}
	
	void Update (){
		if (canRespawn){
			Respawn();
		}
	}
}