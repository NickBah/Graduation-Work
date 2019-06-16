using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PPS_Place : MonoBehaviour
{
	bool LinkReady = false;

	public GameObject PairObject;
	
	public void OnMouseEnter(){
		if (PPS_Common.ActiveBlock != null){
			LinkReady = true;			
		}
		PPS_Common.ActivePlace = gameObject;
	}
	
	public void OnMouseExit(){
		LinkReady = false;
		PPS_Common.ActivePlace = null;
		
		if (PPS_Common.Dragging == true){
			
			if (PPS_Common.ActiveBlock == PairObject){
				// Рвем связь
				PairObject = null;
				//Debug.Log(String.Format("[{0}] разорвал связь с [{1}]",gameObject.name,PPS_Common.ActiveBlock.name));
			}
			//PPS_Common.ActiveBlock = null;
		}
	}	
	
	void Update(){
		if (PPS_Common.DraggingDone == true){
			
			// Установим связь
			if (LinkReady == true){
				LinkReady = false;
							
				//Debug.Log(String.Format("[{0}] поймал [{1}]",gameObject.name,PPS_Common.ActiveBlock.name));
						
				// Если уже имеет связь, то предыдущего - прочь
				if (PairObject != null){
					PPS_Unit prev = PairObject.GetComponent<PPS_Unit>();
					//prev.PairObject = null;
					Vector3 p = PairObject.transform.position;
					PairObject.transform.position  = new Vector3(p.x, p.y + 2, p.z);
				}
				
				PairObject = PPS_Common.ActiveBlock;
				Vector3 pos = PairObject.transform.position;
				PairObject.transform.position  = transform.position;
				
				//PairObject.GetComponent<PPS_Unit>().PairObject = null;
				
				PPS_Common.ActiveBlock = null;
			}			
		}
		
	}

	
}
