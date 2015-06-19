using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Component;
using Data;

public class GameController : MonoBehaviour {
	public static GameController instance { get; private set;}
	public Parameters parameters;
	public GameObject berthModel;
	public GameObject[] estoqueBars;
	public RawMaterial[] estoqueGlobal;
	void Awake (){
		parameters = new Parameters ("Assets/Data/instancia1.10.15.4.dat");  
		parameters.printData ();
		estoqueGlobal = new RawMaterial [parameters.getNumMaterials ()];
		estoqueGlobal = parameters.material;
	}

	// Use this for initialization
	void Start () {
		GameObject berthContent = GameObject.FindGameObjectWithTag ("BerthContent");
		for (int i = 0; i < parameters.getNumBerths() ; i++){
			GameObject obj = (GameObject) Instantiate (berthModel, berthContent.transform.position, berthContent.transform.rotation);
			obj.transform.parent = berthContent.transform;
			obj.GetComponent <Berco> ().berthData = parameters.berth[i];
		}
		estoqueBars = GameObject.FindGameObjectsWithTag ("EstoqueBar");
		GameObject.Find ("RespawnPoint").GetComponent <RespawnShip> ().totalShips = parameters.getNumShips ();
	}
	
	// Update is called once per frame
	void Update () {
		foreach (GameObject bar in estoqueBars) {
			if (bar.GetComponent <Image> ().fillAmount <= 0.05f){
				bar.GetComponent <Image> ().color = Color.red;
			}else{
				bar.GetComponent <Image> ().color = Color.white;
			}
		}
	}
}
