using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Estoque : MonoBehaviour {

	/*	Variaveis floats que guardam a quantidade de protudo em estoque*/
	public float produto1;
	public float produto2;
	public float produto3;
	
	/* Variaveis Text que irao exibir o valor dos floats*/
	public Text textProduto1;
	public Text textProduto2;
	public Text textProduto3;

	/*Variaveis que guardam a quantidade minima de cada produtos no estoque*/
	private float minProduto1;
	private float minProduto2;
	private float minProduto3;

	// Use this for initialization
	void Awake () { //Inicializa toda quantidade minima como sendo 1/3 da quantidade inicial do estoque
		minProduto1 = produto1 / 3;
		minProduto2 = produto2 / 3;
		minProduto1 = produto3 / 3;
	}

	public void fixEstoqueValue (){ //Transforma os valores demasiadamente quebrados em valores inteiros
		produto1 = (int)produto1;
		produto2 = (int)produto2;
		produto3 = (int)produto3;
	}
	
	// Update is called once per frame
	void Update () {
		//Joga no Text o valor de cada produto
		textProduto1.text = produto1.ToString();
		textProduto2.text = produto2.ToString();
		textProduto3.text = produto3.ToString();
	}
}
