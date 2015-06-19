using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Component;

public class Moviment : MonoBehaviour {
	public bool canMove;
	public bool getout;
	public float speed = 2f;
	public GameObject Berco;
	public float timeWaiting = 0;
	public Ship shipData = null;
	public int id;
	
	private bool SimilarPoints (Vector2 orignal, Vector2 target) // Verifica se 2 pontos  (Vector2) sao iguais;
	{
		return (orignal.x <= target.x && orignal.y <= target.y); 
	}

	void Start (){
		GameObject.Find ("RespawnPoint").GetComponent <RespawnShip> ().ships.Add (gameObject.GetComponent <Toggle> ());
		shipData = GameObject.Find("GameController").GetComponent <GameController> ().parameters.ship [id];
	}
	// Update is called once per frame
	void Update () {
		Vector2 pos = transform.position - new Vector3 (2.11f, 0f, 0f); 
		Debug.DrawRay (pos, new Vector2 (-0.5f, 0f), Color.green); 
		RaycastHit2D hit = Physics2D.Raycast (pos, -Vector2.right, 0.5f);
		if (hit.collider == null){ 
			transform.Translate (-speed * Time.deltaTime, 0f, 0f);
		}

		if (getout) {
			gameObject.transform.position = Vector2.MoveTowards (gameObject.transform.position, new Vector2 (-14f, 0f), 2F * Time.deltaTime);
			canMove = false; 
			if (SimilarPoints (transform.position, new Vector2 (-14f, 0f))){
				Berco.GetComponent <Animator> ().SetTrigger("Atracando");
				Berco.GetComponent <Berco> ().empty = false;
				Berco.GetComponent <Berco> ().ship = gameObject.GetComponent <Moviment> ();
				Destroy (gameObject);
			}
		}
		if (!canMove || hit.collider != null)
			timeWaiting += Time.deltaTime;
		//Debug.Log ("-: " + shipData.getCargo ());
//		Debug.Log(":: " + GameController.instance.parameters.ship[0].getCargo ()[1]);
		Debug.Log (":: " + shipData.getCargo ()[1]);
	}
}
