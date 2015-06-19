using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NavioData : MonoBehaviour {
	//Variaveis com a quantidade de cada produto
	public float produto1;
	public float produto2;
	public float produto3;
	
	//Variavel com o Text do Box onde as informaçoes serao exibidas
	private Text boxInfo;

	public float tempoEmEspera; //Tempo que o barco ja esta em espera
	

	public void setInfo (bool cargaDescarga){
		/*Pega a informaçao do navio e exibe na Text, informando a quandidade de produto que o memso carrega
		se ele vem para carregar ou descarregar e o tempo que ele ja esta em espera*/
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
		/*Pega o text para exibiçao das informaçoes e inicializa o tempo de espera como sendo 0*/
		boxInfo = GameObject.FindGameObjectWithTag ("BoxInfo").GetComponent <Text> ();
		tempoEmEspera = 0;
	}
	
	// Update is called once per frame
	private void Update () {
		if (!gameObject.GetComponent <NavioMoviment> ().canMove) {
			tempoEmEspera += Time.deltaTime; // Se o navio nao pode mais andar (como esperando na fila) atualiza o valor do tempo em espera
		}
	}
}
