using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PM_Init : MonoBehaviour
{
	public Button FinalBtn;
	public GameObject ResultPanel;
	public GameObject RestartButton;
	public GameObject[] LeftObj;
	public GameObject[] RightObj;
	
    void Awake()
    {
		for (int i=0;i<LeftObj.Length;i++){
			PM_CommonController.LeftObjects.Add(new PM_Left(){GameObject = LeftObj[i]});			
		}

		for (int i=0;i<RightObj.Length;i++){
			PM_CommonController.RightObjects.Add(new PM_Right(){GameObject = RightObj[i]});
			
		}		
		
		PM_CommonController.ReadData();				
		PM_CommonController.ChooseSomeElements();
				
		PM_CommonController.FinalButton = FinalBtn.gameObject;
		PM_CommonController.ResultPanel = ResultPanel;					
		PM_CommonController.RestartButton = RestartButton;
				
		ResultPanel.SetActive(false);
				
		FinalBtn.onClick.RemoveAllListeners();
		FinalBtn.onClick.AddListener(() => FinalBtnClicked());
		
		RestartButton.GetComponent<Button>().onClick.RemoveAllListeners();
		RestartButton.GetComponent<Button>().onClick.AddListener(() => RestartButtonClicked());		
    }
	
	void FinalBtnClicked(){
		PM_CommonController.CheckResults();
	}

	void RestartButtonClicked(){
		//SceneManager.LoadScene("Scenes/Mapping");		
		
		PM_CommonController.Restart();
		
		PM_CommonController.ChooseSomeElements();
	}
	
	void Update(){
		if (Application.platform == RuntimePlatform.Android){
				if (Input.GetKey(KeyCode.Home))
                {
					
                }
				if (Input.GetKey(KeyCode.Menu)){
					
				}
				// Нажатие кнопки "Назад"
				if (Input.GetKey(KeyCode.Escape)){
					Application.Quit();
				}
        }
	}
}
