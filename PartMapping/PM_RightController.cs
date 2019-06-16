using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PM_RightController : MonoBehaviour, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
	bool LinkReady = false;
	
	public int InspectorIndex;
	
	//public GameObject Pair;
	public PM_Left PairObject = new PM_Left();
	
	public PM_Right Object;
	public bool HasPair = false;
	
	public Animator animator;
	
	public float RightPositionY;
	public bool MoveToRightPlace = false;
	public bool RightPlaceDone = false;
	
	bool DeltaCalculated = false;
	float delta;
	
    // Start is called before the first frame update
    void Start()
    {
		Object = PM_CommonController.RightObjects[InspectorIndex];
        animator = Object.Animator;
    }

    // Update is called once per frame
    void Update()
    {
        if (MoveToRightPlace == true && RightPlaceDone == false){
			if (DeltaCalculated == false){
				delta = (transform.position.y > RightPositionY) ? -0.04f : 0.04f;
				DeltaCalculated = true;
			}
			
			if (Mathf.Abs(transform.position.y - RightPositionY) < 0.05f){
				RightPlaceDone = true;
				PM_CommonController.AnotherBrickInTheWall();
			} else {
				Vector3 p = transform.position;
				transform.position = new Vector3(p.x,p.y + delta, p.z);
			}
		}
		
    }
	
	public void OnPointerEnter(PointerEventData eventData){
		
		if (PM_CommonController.Left != null){
			LinkReady = true;			
		}
		PM_CommonController.Right = Object;
	}
	
	public void OnPointerExit(PointerEventData eventData){
		LinkReady = false;
		PM_CommonController.Right = null;
	}	
	
	public void OnPointerUp(PointerEventData eventData){
		// Установим связь
		if (LinkReady == true){
			LinkReady = false;
			PairObject = PM_CommonController.Left;
			HasPair = true;
		}
		
	}
	
	public void SetVarsToDefault(){
		MoveToRightPlace = false;
		RightPlaceDone = false;		
		DeltaCalculated = false;	
		HasPair = false;
		LinkReady = false;
		Object.Animator.SetBool("HasLink",false);
	}
}
