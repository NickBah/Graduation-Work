using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PDS_RightController : MonoBehaviour, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
	bool LinkReady = false;
	
	public int InspectorIndex;
	
	//public GameObject Pair;
	public PDS_Left PairObject = new PDS_Left();
	
	public PDS_Right Object;
	public bool HasPair = false;
	
	public Animator animator;
	
	public float RightPositionY;
	public float RightPositionX;
	public bool MoveToRightPlace = false;
	public bool RightPlaceDone = false;
	
	public string PDS_Name;
	
	bool DeltaCalculated = false;
	float delta;
	float deltaX;
	
    // Start is called before the first frame update
    void Start()
    {
		Object = PDS_CommonController.RightObjects[InspectorIndex];
        animator = Object.Animator;
    }

    // Update is called once per frame
    void Update()
    {
        if (MoveToRightPlace == true && RightPlaceDone == false){
			if (DeltaCalculated == false){
				delta = (transform.position.y > RightPositionY) ? -0.04f : 0.04f;
				deltaX = (transform.position.x > RightPositionX) ? -0.04f : 0.04f;
				DeltaCalculated = true;
			}
			
			if (Mathf.Abs(transform.position.y - RightPositionY) < 0.05f && Mathf.Abs(transform.position.x - RightPositionX) < 0.05f){
				RightPlaceDone = true;
				PDS_CommonController.AnotherBrickInTheWall();
			} else {
				Vector3 p = transform.position;
				if (Mathf.Abs(transform.position.y - RightPositionY) > 0.05f){
					p.y += delta;
				}
				if (Mathf.Abs(transform.position.x - RightPositionX) > 0.05f){
					p.x += deltaX;
				}				
				transform.position = p;
			}
		}
		
    }
	
	public void OnPointerEnter(PointerEventData eventData){
		
		if (PDS_CommonController.Left != null){
			LinkReady = true;			
		}
		PDS_CommonController.Right = Object;
	}
	
	public void OnPointerExit(PointerEventData eventData){
		LinkReady = false;
		PDS_CommonController.Right = null;
	}	
	
	public void OnPointerUp(PointerEventData eventData){
		// Установим связь
		if (LinkReady == true){
			LinkReady = false;
			PairObject = PDS_CommonController.Left;
			HasPair = true;
		}
		
	}
	
	public void SetVarsToDefault(){
		MoveToRightPlace = false;
		RightPlaceDone = false;		
		DeltaCalculated = false;	
		HasPair = false;
		LinkReady = false;
		if (Object != null && Object.Animator != null){
			Object.Animator.SetBool("HasLink",false);
		}
	}
}
