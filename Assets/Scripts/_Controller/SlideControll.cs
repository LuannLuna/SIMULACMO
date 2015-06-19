using UnityEngine;
using System.Collections;

public class SlideControll : MonoBehaviour {
	public float speed;
	public Transform target;
	public bool leftRight; // false Left | true Right;
	public bool canMove = false;
	public bool click = false;
	
	public void Over (){
		canMove = true;
	}
	
	public void Exit (){
		canMove = false;
	}
	
	public void Click (){
		click = true;
	}
	
	public void Up (){
		click = false;
	}
	
	void Update (){
		if (canMove){
			if (click && !leftRight && target.position.x <= -0.1f){
				target.Translate (speed * Time.deltaTime, 0f, 0f);
			}
			if (click && leftRight && target.position.x >= -9f){
				target.Translate (-speed * Time.deltaTime, 0f, 0f); 
			}
		}
	}
}
