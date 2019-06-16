using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MM_LoadingController : MonoBehaviour
{
	public Image F;
	public Image S;
	public Image T;	
	
	bool Reverse = false;
	float reverse_delta = 0.05f;
	
	MM_Controller menu;
	float progress = 0.0f;
	
	void Start(){
		menu = gameObject.transform.parent.GetComponent<MM_Controller>();
	}
	
	public void Reset(){
		F.fillAmount = 0.0f;
		S.fillAmount = 0.0f;
		T.fillAmount = 0.0f;

	}
	
	public void SetReverseOn(){
		Reverse = true;
		progress = 1.0f;
		F.fillAmount = progress;
		S.fillAmount = progress;
		T.fillAmount = progress;		
	}
	
	void Update(){
		if (Reverse == false){
			progress = menu.GetLoadingProgress();
			F.fillAmount = progress;
			S.fillAmount = progress * 0.99f;
			T.fillAmount = progress * 0.98f;
			
			if (progress == 0.9f){
				F.fillAmount = 1.0f;
				S.fillAmount = 1.0f;
				T.fillAmount = 1.0f;		
				
				menu.asyncLoad.allowSceneActivation = true;
			}
		} else {
				progress -= reverse_delta;
			
				F.fillAmount = progress;
				S.fillAmount = progress;
				T.fillAmount = progress;	

				if (progress <= 0){
					Reverse = false;
					menu.ReverseLoadingDone();
				}
		}
	}
	
}
