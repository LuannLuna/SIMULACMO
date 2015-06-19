using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GetGroup : MonoBehaviour {
	public GruopControll group;
	Toggle button;
	// Use this for initialization
	void Start () {
		button = gameObject.GetComponent <Toggle> ();
		GameObject parent = GameObject.FindGameObjectWithTag("ToggleGroup");
		gameObject.GetComponent <Toggle> ().group = parent.GetComponent <ToggleGroup> ();
		group = parent.GetComponent <GruopControll> ();
		group.AddButton (gameObject.GetComponent <Toggle> ());
	}
	
	public void IsSelected (){
		if (button.isOn) 
			group.GetSelected (button); 
		else
			group.DesSelected ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
