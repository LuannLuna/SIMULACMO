using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Berco : MonoBehaviour {
	public bool empty;
	public float capacidadeCargaDescarga;
	public bool atracou;
	public bool manutencao;
	public GameObject manutencaoIcon;

	private NavioMoviment navioAtracado;
	private NavioData navioData;
	private NaviosController _controll;
	private Estoque _estoque;
	private Animator _anim;
	private Text boxInfo;

	private float sumCarga;
	private float timer;
	// Use this for initialization
	public void GetManutencaoOnOff (){
		manutencaoIcon.SetActive (manutencao);
		gameObject.GetComponent <BoxCollider2D> ().enabled = !manutencao;
		_anim.SetBool ("manutencao", manutencao); 
	}
	
	void Start ()
	{
		empty = true; 
		atracou = false;
		_anim = gameObject.GetComponent <Animator> ();
		_controll = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent <NaviosController> ();
		_estoque =  GameObject.FindGameObjectWithTag ("MainCamera").GetComponent <Estoque> ();
		boxInfo = GameObject.FindGameObjectWithTag ("BoxInfo").GetComponent <Text> ();
		GetManutencaoOnOff ();
	}

	private void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "Barco" && empty) {
			navioAtracado = coll.gameObject.GetComponent <NavioMoviment> ();
			navioData = coll.gameObject.GetComponent <NavioData> ();
			navioAtracado._berco = _anim;
			navioAtracado.destroyItSelf = true;
			coll.gameObject.GetComponent <NavioMoviment> ().followMousePos = false;
			empty = false;
			Destroy (coll.gameObject.GetComponent <PolygonCollider2D> ());
			_controll.barcos.Remove(coll.gameObject);
			sumCarga = navioData.produto1 + navioData.produto2 + navioData.produto3;
			timer = sumCarga / capacidadeCargaDescarga;
			_controll.ActiveCollider ();
			_controll.idBarcoSelecionado = -1;
		} else {
			coll.gameObject.GetComponent <NavioMoviment> ().BackImidiatly ();
		}
		Debug.Log (coll.gameObject.name);
	}
 
	private void OnMouseOver (){
		boxInfo.text = "Berço " + gameObject.name [gameObject.name.Length -1];
		if (empty) {
			boxInfo.text += "\tDisponivel";
		} else {
			if (navioAtracado.cargaDescarga){
				boxInfo.text += "\nCarga Total a ser Carregada: " + sumCarga;
			} else {
				boxInfo.text += "\nCarga Total a ser Descarregada: " + sumCarga;
			}
		}
		boxInfo.text += "\nCapacidade de Carga/Descarga: " + capacidadeCargaDescarga.ToString () + " Ton/H";
	}

	private void Descarregando (float timer){
		//Produto 1
		if (navioData.produto1 > 0 && navioData.produto1 > capacidadeCargaDescarga){
			navioData.produto1 -= timer * Time.deltaTime;
			_estoque.produto1 += timer * Time.deltaTime;
		}else if (navioData.produto1 > 0 && navioData.produto1 < capacidadeCargaDescarga){
			_estoque.produto1 += navioData.produto1;
			navioData.produto1 = 0f;
		}
		//Produto 2
		if (navioData.produto2 > 0 && navioData.produto2 > capacidadeCargaDescarga){
			navioData.produto2 -= timer * Time.deltaTime;
			_estoque.produto2 += timer * Time.deltaTime;
		}else if (navioData.produto2 > 0 && navioData.produto2 < capacidadeCargaDescarga){
			_estoque.produto2 += navioData.produto2;
			navioData.produto2 = 0f;
		}
		//Produto 3
		if (navioData.produto3 > 0 && navioData.produto3 > capacidadeCargaDescarga){
			navioData.produto3 -= timer * Time.deltaTime;
			_estoque.produto3 += timer * Time.deltaTime;
		}else if (navioData.produto3 > 0 && navioData.produto3 < capacidadeCargaDescarga){
			_estoque.produto3 += navioData.produto3;
			navioData.produto3 = 0f;
		}
	}



	private void Carregando (float timer){
		//Produto 1
		if (navioData.produto1 > 0 && navioData.produto1 > capacidadeCargaDescarga){
			navioData.produto1 -= timer * Time.deltaTime;
			_estoque.produto1 -= timer * Time.deltaTime;
		}else if (navioData.produto1 > 0 && navioData.produto1 < capacidadeCargaDescarga){
			_estoque.produto1 -= navioData.produto1;
			navioData.produto1 = 0f;
		}
		//Produto 2
		if (navioData.produto2 > 0 && navioData.produto2 > capacidadeCargaDescarga){
			navioData.produto2 -= timer * Time.deltaTime;
			_estoque.produto2 -= timer * Time.deltaTime;
		}else if (navioData.produto2 > 0 && navioData.produto2 < capacidadeCargaDescarga){
			_estoque.produto2 -= navioData.produto2;
			navioData.produto2 = 0f;
		}
		//Produto 3
		if (navioData.produto3 > 0 && navioData.produto3 > capacidadeCargaDescarga){
			navioData.produto3 -= timer * Time.deltaTime;
			_estoque.produto3 -= timer * Time.deltaTime;
		}else if (navioData.produto3 > 0 && navioData.produto3 < capacidadeCargaDescarga){
			_estoque.produto3 -= navioData.produto3;
			navioData.produto3 = 0f;
		}
	}

	// Update is called once per frame
	private void Update () {
		if (atracou) {
			timer -= Time.deltaTime;
			if (!navioAtracado.cargaDescarga) {
				Descarregando (timer);
			} else {
				Carregando (timer);
			}

			if (timer <= 0) {
				_anim.SetTrigger ("saindo");
				atracou = false;
				empty = true;
				if (!navioAtracado.cargaDescarga) {
					if (navioData.produto1 > 0) _estoque.produto1 += navioData.produto1;
					if (navioData.produto2 > 0) _estoque.produto2 += navioData.produto2;
					if (navioData.produto3 > 0) _estoque.produto3 += navioData.produto3;
				}else{
					if (navioData.produto1 > 0) _estoque.produto1 -= navioData.produto1;
					if (navioData.produto2 > 0) _estoque.produto2 -= navioData.produto2;
					if (navioData.produto3 > 0) _estoque.produto3 -= navioData.produto3;
				}
				_estoque.fixEstoqueValue ();
			}
		}
	}
}
	