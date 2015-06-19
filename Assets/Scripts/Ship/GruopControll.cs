using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class GruopControll : MonoBehaviour {
	public Toggle selected;
	public List <Toggle> buttons;
	
	public void GetSelected (Toggle button){
		selected = button;
	}
	
	public void DesSelected (){
		selected = null;
	}
	
	public void DeselectedAll (){
		foreach (Toggle button in buttons){
			if (button != null)
				if (button != selected){
					button.isOn = false;
				}
		}
	}
	
	public void Awake (){
		buttons = new List<Toggle> ();
	}
	
	public void AddButton (Toggle button){
		buttons.Add(button);
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log("number: " + buttons.Count);
	}
}
