using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PDS_Right
{
	public GameObject GameObject;
	
	public GameObject TextObject{
		get {
			return GameObject.transform.GetChild(0).GetChild(0).gameObject;
		}
	}
	
	public PDS_RightController Controller{
		get {
			return GameObject.GetComponent<PDS_RightController>();
		}
	}
	
	public Text TextComponent{
		get {
			return TextObject.GetComponent<Text>();
		}
	}
	
	public Animator Animator{
		get {
			return TextObject.GetComponent<Animator>();
		}
	}
	
	public Vector3 Position {
		get{
			return GameObject.transform.position;
		}
	}
	
	public string Label {
		get {
			return TextComponent.text;
		}
	}
	
}
