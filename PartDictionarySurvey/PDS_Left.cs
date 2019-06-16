using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PDS_Left
{
	public GameObject GameObject;
	
	public bool WasCorrect = false;

	public PDS_LeftController Controller{
		get {
			return GameObject.GetComponent<PDS_LeftController>();
		}
	}
	
	public GameObject ImgObject{
		get {
			// Если есть изображение, если его нет
			if (GameObject.transform.GetChild(0).childCount == 4){
				return GameObject.transform.GetChild(0).GetChild(3).gameObject;
			} else {
				return null;
			}
		}
	}
	
	public GameObject NotReadyImgObject{
		get {
			return GameObject.transform.GetChild(0).GetChild(0).gameObject;
		}
	}
	
	public GameObject CorrectImgObject{
		get {
			return GameObject.transform.GetChild(0).GetChild(1).gameObject;
		}
	}
	
	public GameObject WrongImgObject{
		get {
			return GameObject.transform.GetChild(0).GetChild(2).gameObject;
		}
	}
	
	public Image ImageComponent{
		get {
			return ImgObject.GetComponent<Image>();
		}
	}
	
	public Animator Animator{
		get {
			if (ImgObject != null){
				return ImgObject.GetComponent<Animator>();
			} else {
				return null;
			}
		}
	}
	
	public Animator NotReadyAnimator{
		get {
			return NotReadyImgObject.GetComponent<Animator>();
		}
	}
	
	public Animator CorrectAnimator{
		get {
			return CorrectImgObject.GetComponent<Animator>();
		}
	}
	
	public Animator WrongAnimator{
		get {
			return WrongImgObject.GetComponent<Animator>();
		}
	}
	
	public LineRenderer LineRenderer {
		get {
			return GameObject.GetComponent<LineRenderer>();
		}		
	}
	
	public Vector3 Position {
		get{
			return GameObject.transform.position;
		}
	}
	
	public string FileName{
		get{
			return CorrectPair.FileName;
		}
	}
	
	public string CorrectPairLabel{
		get{
			return CorrectPair.Label;
		}
	}
	
	public PDS_Pair CorrectPair;
}
