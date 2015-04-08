using UnityEngine;
using System.Collections;

public class NavioMoviment : MonoBehaviour {
	#region VariaveisPublicas
		public float sizeofRay; // Tamanho do Raycast (Ray que verifica se tem algum objeto a frente do barco)
		public bool canMove; // Bool que informa se o barco pode se mover ou nao
		public float speed; // Velocidade de movimentaçao horizontal do barco
		public Transform _rayPoint; // Posiçao para iniciar o Raycast que verifica se tem objetos a frente do barco
		public bool selected; // Bool que informa se o barco esta sendo selecionado ou nao (Atualmente sem utilizaçao)
		public bool canAtrack; // Bool que informa se o barco pode atracar, se ele poder ele ira para o ponto de saida (Fora da tela)
		public int id; //Id indentificador do Barco, dado pelo Controller que gera cada barco
		public bool backOriginalPos; // Bool que diz quando o barco deve voltar pra ultima posiçao na fila dele. ajuda quando o barco foi movimentado pelo mouse e nao alocado em um berço
		public Animator _berco; //Variavel que ira pegar a animaçao do berco que o mesmo foi alocado para dar Start nela
		public bool destroyItSelf; //Variavel que ira dizer quando o objeto devera se "auto-destruir", esta variavel so mudara quando os dados do objetos ja estiverem sido capturados pelo Berço
		public bool followMousePos; // Variavel que ira informar se o barco devera seguir o mouse, isso auxilia no Drag and Drop
		public bool cargaDescarga; // true para cargar e false para descarga; (Informa se o barco esta para carregar ou descarregar no berço
	#endregion VariaveisPublicas

	#region VariaveisPrivadas
	private Vector2 originalPos; //Vetor que informa a posiçao do Barco, posiçao dentro da fila de atracaçao
	private NaviosController _controller; // Variavel que contem o controlle dos navios, variavel auxilia na passagem de mensagem direta para o script e nao por mensagem de gameobjects
	private Transform exitPoint; //Variavel que guarda a posiçao do ponto de saida da Tela, posiçao que o barco se direcionara quando ele for atracar em um berço
	#endregion VariaveisPrivadas
	
	public void BackImidiatly (){
		transform.position = originalPos;
		followMousePos = false;
		_controller.ActiveCollider ();
		_controller.idBarcoSelecionado = -1;
	}
	
	private void Awake ()
	{ 
		canMove = true; //Inicialmente todo barco pode se mover
		originalPos = gameObject.transform.position; //A posiçao original inicial e a posiçao de criaçao do barco
		selected = false; //Inicialmente nenhum barco esta selecionado 
		followMousePos = false; // Inicialmente nenhum barco deve seguir o mouse
		canAtrack = false; // Inicialmente nenhum barco pode atracar
		backOriginalPos = false; // Inicialmente nenhum barco deve voltar para posiçao de origem pois o mesmo 
		destroyItSelf = false;// Inicialmente nenhum barco de ve se auto destruir
		_controller = GameObject.FindWithTag ("MainCamera").GetComponent <NaviosController> (); // Pega a localizaçao do script controller
		exitPoint = GameObject.Find ("ExitPoint").GetComponent <Transform> (); // Descobre onde esta o posiçao para Exit (saida da Tela)
	}

	private void OnMouseOver () //Detecta quando o Mouse esta sobre o objeto
	{
		gameObject.GetComponent <NavioData> ().setInfo (cargaDescarga); //informa que o mouse esta sobre o objeto para o script NavioData que exibira as informaçoes do navio
		if (Input.GetKey (KeyCode.Mouse0)) { // Capta quando o botao direito do Mouse esta senedo precionado
			selected = true;  
			followMousePos = true; // Informa que o barco pode seguir o Mouse (No Update que movimenta o mouse)
			_controller.StopAll (); // Informa para o controlador dos Navios que nenhum barco pode andar  para que nao teha sobreposiçao de barco na fila
			_controller.idBarcoSelecionado = id; // Informa para o controlador de Navios quem eh o barco que esta sendo selecionado
			_controller.DesactiveCollider (); // Informa ao controlador para desativar o collider dos demais navios a fim de nao se colidirem e nem que o mouse "pegar" mais de um navio
			_controller.canRespawn = false;
			gameObject.GetComponent <SpriteRenderer> ().sortingOrder = 10; // Sobe o barco na ordem de Layers para que ele sempre esteje a frente dos demais navios
		}
		if (Input.GetKeyUp (KeyCode.Mouse0)) { //Capta quando o botao direito do Mouse foi solto
			followMousePos = false; //informa que o barco nao pode mais seguir o mouse
			if (!canAtrack){ // Se ele nao foi alocado em um berço ele devera volar a sua posiçao original na fila
				backOriginalPos = true;
			}
			_controller.ActiveCollider (); //Informa o controllador que todos os Colliders podem ser novamente ativados
			_controller.idBarcoSelecionado = -1; // Valor negativo significa que nenhum barco esta sendo selecionado
			gameObject.GetComponent <SpriteRenderer> ().sortingOrder = 3; // Volta o barco para a sua posicao original nos Layers
		}
	}

	private void OnMouseExit () // Detecta quando o mouse sai de cima do objeto
	{
		selected = false; // Informa que o barco nao esta mais sendo selecionado
		_controller.idBarcoSelecionado = -1;  // Valor negativo significa que nenhum barco esta sendo selecionado
	}

	private bool SimilarPoints (Vector2 orignal, Vector2 target) // Verifica se 2 pontos  (Vector2) sao iguais;
	{
		return (orignal == target);
	}



	// Update is called once per frame
	private void Update ()
	{
		Vector2 pos = _rayPoint.position; // Armazena a posicao do Ray em um Vector 2
		Debug.DrawRay (pos, new Vector2 (-sizeofRay, 0f), Color.green); // Desenha um RayCast na posicao do Ray
		RaycastHit2D hit = Physics2D.Raycast (pos, -Vector2.right, sizeofRay); // Cria o Ray fisicamente
		if (hit.collider == null && canMove) { // Verifica se o Ray nao colidiu com ninguem e o barco pode andar
			gameObject.transform.Translate (-speed * Time.deltaTime, 0f, 0f); // Caso sim movimenta o navio no eixo x
			originalPos = gameObject.transform.position; // Atualiza a posicao original do barco
			canMove = true; // Se ele esta andando ele pode continuar a andar
		} else if (hit.collider != null) { // se ele colidiu com algo
			if (hit.collider.gameObject.tag == "Barco" || hit.collider.gameObject.tag == "Limit") // Caso seja um outro barco ou o limit ele nao pode mais andar
			{
				canMove = false;
			}
		}
		if (followMousePos) // se ele poder seguir o mouse
		{
			Vector2 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition); // Pega a posicao do mouse e a converte em uma posicao no mundo
			canMove = false; //informa que o barco nao pode andar (no eixo X)
			gameObject.transform.position = mousePos; // Atualiza a posicao do objeto para a posicao do mouse;
		}
		if (backOriginalPos) // Se o barco deve voltar pra posicao original
		{
			gameObject.transform.position = Vector2.MoveTowards (gameObject.transform.position, originalPos, 1F); //manda o objecto ir em direçao a posicao de origem na fila
			if (SimilarPoints( gameObject.transform.position, originalPos)) // Verifica se ele ja chegou na posicao de origem
			{
				backOriginalPos = false; // Se sim ele para de andar 
				canMove = true; // Pode voltar a andar
				_controller.StarAll (); // informa ao controllador para restartar os barcos (pois todos os barcos param quando 1 eh selecionado)
			}
		}
		if (destroyItSelf) // Se o barco pode se auto destruir
		{
			gameObject.transform.position = Vector2.MoveTowards (gameObject.transform.position, exitPoint.position, 2F * Time.deltaTime); // Movimenta o barco ate o posiçao de "Exit"
			if (SimilarPoints (gameObject.transform.position, exitPoint.position)) //Verifica se ele ja chegou na posicao
			{
				_berco.SetTrigger("atracando"); //Se sim manda o berco que ele atracou iniciar a animaçao de chegada de Naivo
				_berco.SetBool("cargaDescarga", cargaDescarga); //Informa ao animator se a animaçao do navio deve ser de carga ou descarga
				_berco.gameObject.GetComponent <Berco> ().atracou = true; //Informa ao berco que o navio ja atracou
//				_controller.barcos.Remove(id);
				//_controller.barcos.Remove (gameObject); //informa ao controller que ele devera ser removido da lista de navios
				_controller.RestarFila (1.5F); // Manda a fila Restartar com 1.5s de Delay de 1 pro outro
				Destroy (gameObject); // Destroi o gameoject
			}
		}
	}
}
