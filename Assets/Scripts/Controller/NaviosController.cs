using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NaviosController : MonoBehaviour {
	#region VariaveisPublicas
		public GameObject navioModelo; // Prefab dos Navios a serem instanciados
		public Transform pointToRespawn; //Ponto para dar Respawn nos navios
		public Transform mar; // Transform do Mar  onde os navios deverao ficar
		public double timeToRespawn; // Tempo de Respawn entre um navio e outro
		public bool canRespawn; //Bool que indica se o controller pode dar respawn ou nao
		public int idBarcoSelecionado;// int que informa qual barco esta sendo selecionado 
		public List <GameObject> barcos = new List<GameObject> (); //Lista de barcos
	#endregion VariaveisPublicas
	
	#region VariaveisPrivadas
		private double timer; //Timer que controlar o tempo de jogo
		private int idBarco; //variavel que da o id de cada barco a ser instanciado
	#endregion VariaveisPrivadas
	
	public void Respawn () // Funcao que ira dar Respawn nos barcos
	{ 
		if (Time.time > timer) //Se o tempo atual e maior que o timer
		{
			timer = Time.time + timeToRespawn; //Atializa o valor do timer com o tempo atual + o tempo para o proximo respawn
			GameObject barco = (GameObject) Instantiate (navioModelo, pointToRespawn.position, pointToRespawn.rotation); // Instancia um novo barco
			barco.GetComponent <NavioMoviment> ().id = idBarco; //Adiciona um id ao barco instanciado
			barco.transform.parent = mar; //Coloca tal barco como sendo "filho" do mar
			barcos.Add (barco); //Adiciona o barco na lista
			idBarco ++; //Incrementa o contador de ids.
			barco.GetComponent <NavioData> ().produto1 = (Random.Range(100, 1000)); // Gera um valor aleatorio para o produto 1
			barco.GetComponent <NavioData> ().produto2 = (Random.Range(100, 1000)); // Gera um valor aleatorio para o produto 2
			barco.GetComponent <NavioData> ().produto3 = (Random.Range(100, 1000)); // Gera um valor aleatorio para o produto 3
			barco.GetComponent <NavioMoviment> ().cargaDescarga = (Random.Range (0, 100) % 5 == 0); // O valor da expressao ira informar se o barco devera descarergar onu carregar no berco
		}
	}

	public void StarAll (){ //Manda todos os barcos a voltarem a andar
		foreach (GameObject barco in barcos) {
			barco.GetComponent <NavioMoviment> ().canMove = true;
		}
	}

	public void StopAll (){//Manda todos os barcos a pararem de andar
		foreach (GameObject barco in barcos) {
			if (barco != null)
				barco.GetComponent <NavioMoviment> ().canMove = false;
		}
	}
	
	public void DesactiveCollider (){//Desativa todos os colliders dos barcos, menos o do barco selecionado
		foreach (GameObject barco in barcos){
			if (barco.GetComponent <NavioMoviment> ().id != idBarcoSelecionado){
				if (barco != null)
					barco.GetComponent <PolygonCollider2D> ().enabled = false;
			}
		}
	}
	
	public void ActiveCollider (){//Aciona todos os colliders dos barcos, menos o do barco selecioando
		foreach (GameObject barco in barcos){
			if (barco.GetComponent <NavioMoviment> ().id != idBarcoSelecionado){
				if (barco != null)
					barco.GetComponent <PolygonCollider2D> ().enabled = true;
			}
		}//Apos todos os barcos terem seus colliders ativados permite que o controlador de respawn em novos barcos
		canRespawn = true;
		timer = Time.time + timeToRespawn; //atualiza o timer devido ao tempo que ele ficou segurando o barco
	}

	private void PreventOverFlow (){ //Cria um raycast a frente do ponto de Respawn para que para de gerar novos barcos se nao tem mais espaço fisicamente na tela
		Vector2 pos = pointToRespawn.position;
		RaycastHit2D hit = Physics2D.Raycast (pos, -Vector2.right, 2f);
		Debug.DrawRay (pos, new Vector2 (-2f, 0f), Color.red);
		if (hit.collider == null && idBarcoSelecionado < 0) {
			canRespawn = true;
		} else {
			canRespawn = false;
		}
	}

	public void RestarFila (float timer){ //Metodo para chamar um corrotina de iniciar a fila
		StartCoroutine (StarBoat(timer));
	}


	private IEnumerator StarBoat (float timer){ //Manda cada barco andar com um delay de um pro outro;
		foreach (GameObject barco in barcos) {
			yield return new WaitForSeconds (timer);
			barco.GetComponent <NavioMoviment> ().canMove = true;
		}
	}

	private void Awake () {
		canRespawn = true; // Iniciamente sempre pode se dar Respawn
		idBarcoSelecionado = -1; //Nao tem nenhuma barco selecionado (valor negativo)
		idBarco = 0; //Inicializa o contador para o id dos barcos  gerados
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
