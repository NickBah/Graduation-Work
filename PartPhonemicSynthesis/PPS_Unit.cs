using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PPS_Unit : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public void OnBeginDrag(PointerEventData eventData)
    {
		PPS_Common.ActiveBlock = gameObject;
		PPS_Common.DraggingDone = false;		
    }	
	
	public void OnDrag(PointerEventData eventData){
		Vector3 p = Camera.main.ScreenToWorldPoint(eventData.position);
		p.z = 0;
		//Debug.Log(p);
		
		gameObject.transform.position = p;
		PPS_Common.Dragging = true;
		
	}
	
	public void OnEndDrag(PointerEventData eventData){	
		PPS_Common.DraggingDone = true;	
		PPS_Common.Dragging = false;			
	}
	
}
