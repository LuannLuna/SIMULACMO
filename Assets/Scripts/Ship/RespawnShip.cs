using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class RespawnShip : MonoBehaviour {
	public GameObject ship;
	public Transform pointRespawn;
	public Transform shipArea;
	public double timeToRespawn;
	public bool canRespawn;
	public Vector2 size;
	public List <Toggle> ships;
	private double timer;
	public int totalShips;
	public int numberShips = 0;
	// Use this for initialization
	void Respawn (){
		if (Time.time > timer && numberShips < totalShips){
			GameObject barco = (GameObject) Instantiate (ship, pointRespawn.position, pointRespawn.rotation);
			barco.transform.parent = shipArea;
			barco.gameObject.GetComponent <RectTransform> ().sizeDelta = size; 
			RectTransform transform = gameObject.GetComponent <RectTransform> ();
			barco.gameObject.GetComponent <RectTransform> ().localScale = transform.localScale;
			barco.GetComponent <Image> ().SetNativeSize(); 
			barco.GetComponent <Moviment> ().id = numberShips;
			//ships.Add (barco.GetComponent <Toggle> ());
			numberShips++;
			timer = Time.time + timeToRespawn;
		}
	}
	
	void Awake () {
		ships = GameObject.FindGameObjectWithTag("ToggleGroup").GetComponent <GruopControll> ().buttons;

	}

	// Update is called once per frame
	void Update () {
		Vector2 pos = transform.position - new Vector3 (2.11f, 0f, 0f); 
		Debug.DrawRay (pos, new Vector2 (-0.5f, 0f), Color.red);  
		RaycastHit2D hit = Physics2D.Raycast (pos, -Vector2.right, 0.5f);
		if (hit.collider == null) { 
			Respawn ();
		}
	}
}
