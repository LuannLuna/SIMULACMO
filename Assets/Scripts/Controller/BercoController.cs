using UnityEngine;
using System.Collections;

public class BercoController : MonoBehaviour {
	public GameObject[] bercos; //Array com os berços
	
	private int numberOfManutencao; //Numero maximo de berços que podem estar em manuteçao ao mesmo tempo
	// Use this for initialization
	void Awake () {
		foreach (GameObject berco in bercos){
			Berco _bercoScript = berco.GetComponent <Berco> (); //Pega o script do berço
			/*O resultado a expressao ira informar se o berço esta ou nao em manutencao*/
			if (Random.Range (0, 100) % 2 == 0 && numberOfManutencao < 2){
				_bercoScript.manutencao = true;
				numberOfManutencao ++;
			}else {
				_bercoScript.manutencao = false; 
			}
			_bercoScript.capacidadeCargaDescarga = Random.Range (10, 100); //Da ao berço a sua capacidade de carga descarga aleatoriamente
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
