using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Estoque : MonoBehaviour {
	public float produto1;
	public float produto2;
	public float produto3;

	public Text textProduto1;
	public Text textProduto2;
	public Text textProduto3;

	private float minProduto1;
	private float minProduto2;
	private float minProduto3;

	// Use this for initialization
	void Awake () {
		minProduto1 = produto1 / 3;
		minProduto2 = produto2 / 3;
		minProduto1 = produto3 / 3;
	}

	public void fixEstoqueValue (){
		produto1 = (int)produto1;
		produto2 = (int)produto2;
		produto3 = (int)produto3;
	}
	
	// Update is called once per frame
	void Update () {
		textProduto1.text = produto1.ToString();
		textProduto2.text = produto2.ToString();
		textProduto3.text = produto3.ToString();
	}
}
