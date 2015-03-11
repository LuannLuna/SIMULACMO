using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Controller : MonoBehaviour {
	public GameObject navioModelo;
	public Transform pointToRespawn;
	public Transform mar;
	public double timeToRespawn;
	public bool canRespawn;
	public int idBarcoSelecionado;

	public Dictionary <int, GameObject> barcos = new Dictionary<int, GameObject> ();

	private double timer;
	private int idBarco;
	private int i;
	
	public void Respawn ()
	{
		if (Time.time > timer)
		{
			timer = Time.time + timeToRespawn;
			GameObject barco = (GameObject) Instantiate (navioModelo, pointToRespawn.position, pointToRespawn.rotation);
			barco.GetComponent <NavioMoviment> ().id = idBarco;
			barco.transform.parent = mar;
			barcos.Add (idBarco, barco);
			idBarco ++;
			bool cargaDescarga = (Random.Range (0, 100) % 5 == 0);
			barco.GetComponent <NavioMoviment> ().cargaDescarga = cargaDescarga;
		}
	}

	public void StarAll (){
		foreach (var barco in barcos) {
			barco.Value.GetComponent <NavioMoviment> ().canMove = true;
		}
	}

	public void StopAll (){
		foreach (var barco in barcos) {
			barco.Value.GetComponent <NavioMoviment> ().canMove = false;
		}
	}

	private void PreventOverFlow (){
		Vector2 pos = pointToRespawn.position;
		RaycastHit2D hit = Physics2D.Raycast (pos, -Vector2.right, 2f);
		Debug.DrawRay (pos, new Vector2 (-2f, 0f), Color.red);
		if (hit.collider == null) {
			canRespawn = true;
		} else {
			canRespawn = false;
		}
	}

	public void RestarFila (){
		StartCoroutine (StarBoat());
	}


	private IEnumerator StarBoat (){
		yield return new WaitForSeconds (1.5f);
		if (barcos[i] != null && i < barcos.Count){
			barcos[i].GetComponent <NavioMoviment> ().canMove = true;
			i++;
			StarBoat ();
		}else if (i >= barcos.Count){
			i = 0;
		}
	}

	private void Awake () {
		canRespawn = true;
		idBarcoSelecionado = -1;
		idBarco = 0;
	}

	// Update is called once per frame
	private void Update () {
		if (canRespawn)
		{
			Respawn();
		}
		PreventOverFlow ();
	}
}
