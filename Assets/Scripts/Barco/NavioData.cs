using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NavioData : MonoBehaviour {
	public float produto1;
	public float produto2;
	public float produto3;

	private Text boxInfo;

	public float tempoEmEspera;

	public void setInfo (bool cargaDescarga){
		boxInfo.text = "\nNavio: " + gameObject.GetComponent <NavioMoviment> ().id +"\nProduto 1: " 
			+ produto1.ToString () + "\nProduto 2: " + produto2.ToString () + "\nProduto 3: " + produto3.ToString ()
			+ "\nTempo em Espera: " + ((int)tempoEmEspera).ToString ();
		if (cargaDescarga) {
			boxInfo.text += "\n\nA Carregar";
		} else {
			boxInfo.text += "\n\nA Descarregar";
		}
	}

	// Use this for initialization
	private void Awake () {
		boxInfo = GameObject.FindGameObjectWithTag ("BoxInfo").GetComponent <Text> ();
		tempoEmEspera = 0;
	}
	
	// Update is called once per frame
	private void Update () {
		if (!gameObject.GetComponent <NavioMoviment> ().canMove) {
			tempoEmEspera += Time.deltaTime;
		}
	}
}
