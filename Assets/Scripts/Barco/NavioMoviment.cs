using UnityEngine;
using System.Collections;

public class NavioMoviment : MonoBehaviour {
	public float sizeofRay;
	public bool canMove;
	public float speed;
	public Transform _rayPoint;
	public bool selected;
	public bool canAtrack;
	public int id;
	public bool backOriginalPos;
	public Animator _berco;
	public bool destroyItSelf;
	public bool followMousePos;
	public bool cargaDescarga; // true para cargar e false para descarga;

	private Vector2 originalPos;
	private Controller _controller;
	private Transform exitPoint;

	private void Awake ()
	{
		canMove = true;
		originalPos = gameObject.transform.position;
		selected = false;
		followMousePos = false;
		canAtrack = false;
		backOriginalPos = false;
		destroyItSelf = false;
		_controller = GameObject.FindWithTag ("MainCamera").GetComponent <Controller> ();
		exitPoint = GameObject.Find ("ExitPoint").GetComponent <Transform> ();
	}

	private void OnMouseOver ()
	{
		if (Input.GetKey (KeyCode.Mouse0)) {
			selected = true;
			followMousePos = true;
			_controller.StopAll ();
			_controller.idBarcoSelecionado = id;
		}
		if (Input.GetKeyUp (KeyCode.Mouse0)) {
			followMousePos = false;
			if (!canAtrack){
				backOriginalPos = true;
			}
		}
	}

	private void OnMouseExit ()
	{
		selected = false;
		_controller.idBarcoSelecionado = -1;
	}

	// Use this for initialization
	private void Start ()
	{
	
	}

	private bool SimilarPoints (Vector2 orignal, Vector2 target)
	{
		return (orignal == target);
	}



	// Update is called once per frame
	private void Update ()
	{
		Vector2 pos = _rayPoint.position;
		Debug.DrawRay (pos, new Vector2 (-sizeofRay, 0f), Color.green);
		RaycastHit2D hit = Physics2D.Raycast (pos, -Vector2.right, sizeofRay);
		if (hit.collider == null && canMove) {
			gameObject.transform.Translate (-speed * Time.deltaTime, 0f, 0f);
			originalPos = gameObject.transform.position;
			canMove = true;
		} else if (hit.collider != null) {
			if (hit.collider.gameObject.tag == "Barco" || hit.collider.gameObject.tag == "Limit")
			{
				canMove = false;
			}
		}
		if (followMousePos)
		{
			Vector2 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			canMove = false;
			gameObject.transform.position = mousePos;
		}
		if (backOriginalPos)
		{
			gameObject.transform.position = Vector2.MoveTowards (gameObject.transform.position, originalPos, 1F);
			if (SimilarPoints( gameObject.transform.position, originalPos))
			{
				backOriginalPos = false;
				canMove = true;
				_controller.StarAll ();
			}
		}
		if (destroyItSelf)
		{
			gameObject.transform.position = Vector2.MoveTowards (gameObject.transform.position, exitPoint.position, 2F * Time.deltaTime);
			if (SimilarPoints (gameObject.transform.position, exitPoint.position))
			{
				_berco.SetTrigger("atracando");
				_berco.SetBool("cargaDescarga", cargaDescarga);
				_controller.StarAll ();
				Destroy (gameObject);
			}
		}
	}
}
