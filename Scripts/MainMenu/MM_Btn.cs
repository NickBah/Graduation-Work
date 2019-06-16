using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MM_Btn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public GameObject ImgHover;
	public GameObject ImgClicked;
	
	Animator HoverAnimator;
	
	void Start(){
		HoverAnimator = ImgHover.GetComponent<Animator>();
	}
	
	public void OnPointerEnter(PointerEventData eventData){
		HoverAnimator.Play("MM_B_Active");
	}
	
	public void OnPointerExit(PointerEventData eventData){
		HoverAnimator.Play("MM_B_Hide");
	}
	
	
}
