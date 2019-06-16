using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PM_LeftController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public bool HasPair = false;	
	public PM_Right PairObject = new PM_Right();	
	public PM_Left Object;	
	public int InspectorIndex;
	
	LineRenderer line;	
	Animator animator;
	
    // Start is called before the first frame update
    void Start()
    {
		Object = PM_CommonController.LeftObjects[InspectorIndex];
		
        line = Object.LineRenderer;
		
		Vector3 p = Object.Position;
		p.z = -1;
		
		line.SetPosition(0,p);
		
		animator = Object.Animator;
				
		HideLine();
    }
	
	public void HideLine(){
		line.startWidth = 0;
		line.endWidth = 0;
		//line.SetPosition(1,line.GetPosition(0));
	}
	
	
	public void UnhideLine(){
		line.startWidth = 0.15f;
		line.endWidth = 0.15f;
	}	

    public void OnBeginDrag(PointerEventData eventData)
    {
		PM_CommonController.Left = Object;
		UnhideLine();
    }
	
	public void OnDrag(PointerEventData eventData){
		//Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 p = Camera.main.ScreenToWorldPoint(eventData.position);
		
		p.z = -1;
		line.SetPosition(1,p);
		
	}
	
	public float Distance(Vector3 a, Vector3 b){
		return Mathf.Sqrt((a.x - b.x)*(a.x - b.x) + (a.y - b.y)*(a.y - b.y));
	}
	
	public PM_Right IntersectSomeRightObject(){
		
		PM_Right obj;		
		Vector3 linePos = line.GetPosition(1);
			
		for (int i=0;i<PM_CommonController.RightObjects.Count;i++){
			obj = PM_CommonController.RightObjects[i];
			
			if (Distance(linePos,obj.Position) < 0.8f){
				return obj;
			}
		}
		return null;
	}
	
	public void OnEndDrag(PointerEventData eventData){	
		PM_Right obj = PM_CommonController.Right;
		
		if (obj != null){
			
			// Если у данного левого объекта уже есть связь - удалим
			if (HasPair == true){			
				PairObject.Controller.HasPair = false;
			}
						
			PairObject = obj;
			
			Vector3 p = PairObject.Position;
			p.z = -1;
			
			line.SetPosition(1,p);
			PM_RightController right = PairObject.Controller;
						
			// Если у правого элемента уже есть связь, то удалим ее
			if (right.HasPair == true){
				PM_LeftController prev = right.PairObject.Controller;
				prev.HideLine();
				prev.animator.SetBool("HasLink",false);
				prev.HasPair = false;		
				//prev.PairObject = null;
				
			}
			right.PairObject = Object;			
			right.animator.SetBool("HasLink",true);			
			right.HasPair = true;			
			animator.SetBool("HasLink",true);	

			HasPair = true;
		} else {
			HideLine();
			
			// Если сейчас есть связь, то своим промахом мы ее удалим
			if (HasPair == true){
				PM_RightController right = PairObject.Controller;
				
				right.animator.SetBool("HasLink",false);
				
				right.HasPair = false;
			}
			
			animator.SetBool("HasLink",false);			

			HasPair = false;
		}			
	}
	
	public void SetVarsToDefault(){
		HasPair = false;
		HideLine();
		Object.CorrectAnimator.SetBool("Active",false);
		Object.WrongAnimator.SetBool("Active",false);
		Object.Animator.SetBool("HasLink",false);
	}
	
}
