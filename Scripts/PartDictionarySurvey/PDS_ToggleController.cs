using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PDS_ToggleController : MonoBehaviour, IPointerClickHandler
{

	public Toggle[] Toggles;
	public int Index;
	
	public void OnPointerClick(PointerEventData eventData){
		for (int i=0;i<Toggles.Length;i++){
			if (i != Index){
				Toggles[i].isOn = false;
			}
		}
	}
}
