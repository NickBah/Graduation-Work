using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PG_task_1_Init : MonoBehaviour
{
	public Button FinalBtn;
	public Button NextBtn;
	public GameObject LeftObjects;
	public GameObject RightObjects;
	
	public Text Stat;
	
	public GameObject MainScreen;
	public GameObject HelpScreen;
	
	public Button HS_BackBtn;
	public Button HS_ExitBtn;
	
	public GameObject Back;
	
	public Button BtnHelp;
	
	List<Vector3> StartPositions = new List<Vector3>();
	
	int Index = -1;
	
	int CorrectAttempts = 0;
	int Attempts = 0;
	
    void Awake()
    {
		PDS_CommonController.RightObjects.Clear();
		PDS_CommonController.LeftObjects.Clear();
		//PDS_CommonController.FinalButton = FinalBtn.gameObject;
							
		FinalBtn.onClick.RemoveAllListeners();
		FinalBtn.onClick.AddListener(() => FinalBtnClicked());
	
		NextBtn.onClick.RemoveAllListeners();
		NextBtn.onClick.AddListener(() => NextBtnClicked());
		PDS_CommonController.NextBtn = NextBtn.gameObject;
		NextBtn.gameObject.SetActive(false);
		
		PDS_CommonController.FinalButton = FinalBtn.gameObject;
		
		BtnHelp.onClick.RemoveAllListeners();
		BtnHelp.onClick.AddListener(() => BtnHelpClicked());
		
		HS_BackBtn.onClick.RemoveAllListeners();
		HS_BackBtn.onClick.AddListener(() => HS_BackBtnClicked());
		
		HS_ExitBtn.onClick.RemoveAllListeners();
		HS_ExitBtn.onClick.AddListener(() => MM_Controller.HS_ExitBtnClicked());	

		Next();		
    }
	
	void Start(){
		//Next();
		
		
	}
	
	void BtnHelpClicked(){
		MainScreen.SetActive(false);
		HelpScreen.SetActive(true);		
	}
	
	void HS_BackBtnClicked(){
		MainScreen.SetActive(true);
		HelpScreen.SetActive(false);		
	}
	
	void HS_ExitBtnClicked(){
		/*
		GameObject g = GameObject.FindWithTag("MainMenuTag");
		if (g != null){
			MM_Controller menu = g.GetComponent<MM_Controller>();
			menu.Screens.SetActive(true);			
			menu.ButtonBack.gameObject.SetActive(true);
		}
		SceneManager.LoadScene("Scenes/MainMenu");
		*/
		MM_Controller.HS_ExitBtnClicked();
	}	
	
	private GameObject GetCorrectPairForInit(GameObject Left, List<GameObject> Right){
		string target = Left.GetComponent<PDS_LeftController>().PDS_Name;
		for (int i=0;i<Right.Count;i++){
			if (Right[i].GetComponent<PDS_RightController>().PDS_Name == target){
				return Right[i];
			}
		}
		return null;
	}
	
	void NextBtnClicked(){
		Next();
		NextBtn.gameObject.SetActive(false);
		FinalBtn.gameObject.SetActive(true);
		FinalBtn.interactable = true;
	}
	
	void Next(){
		// Разорвем все связи
		for (int i=0;i<PDS_CommonController.RightObjects.Count;i++){
			PDS_CommonController.RightObjects[i].Controller.SetVarsToDefault();
		}
		for (int i=0;i<PDS_CommonController.LeftObjects.Count;i++){
			PDS_CommonController.LeftObjects[i].Controller.SetVarsToDefault();
		}		
		
		PDS_CommonController.LeftObjects.Clear();
		PDS_CommonController.RightObjects.Clear();

		PDS_CommonController.RightObjectsDoneTransfer = 0;
		//PDS_CommonController.Attempts = 0;
		
		Index += 1;
		if (Index > LeftObjects.transform.childCount - 1){
			Index = 0;
		}
		
		for (int i=0;i<Back.transform.childCount;i++){
			Back.transform.GetChild(i).gameObject.SetActive(false);
		}
		Back.transform.GetChild(Index).gameObject.SetActive(true);
		
		Transform LObj = LeftObjects.transform.GetChild(Index);
		Transform RObj = RightObjects.transform.GetChild(Index);
		
		for (int i=0;i<LeftObjects.transform.childCount;i++){
			LeftObjects.transform.GetChild(i).gameObject.SetActive(false);
		}
		for (int i=0;i<RightObjects.transform.childCount;i++){
			RightObjects.transform.GetChild(i).gameObject.SetActive(false);
		}
		
		LObj.gameObject.SetActive(true);
		RObj.gameObject.SetActive(true);
		
		List<GameObject> Left = new List<GameObject>();
		List<GameObject> Right = new List<GameObject>();
		
		for (int i=0;i<LObj.childCount;i++){
			Left.Add(LObj.GetChild(i).gameObject);
		}
		
		// Вернем объекты на изначальные места
		if (StartPositions.Count != 0){
			for (int i=0;i<RObj.childCount;i++){
				RObj.GetChild(i).position = StartPositions[i];
			}			
		} else {
			for (int i=0;i<RObj.childCount;i++){
				StartPositions.Add(RObj.GetChild(i).position);
			}
		}
		
		for (int i=0;i<RObj.childCount;i++){
			Right.Add(RObj.GetChild(i).gameObject);
		}		
		
		Left.Shake();
		List<GameObject> RightCorrect = new List<GameObject>();
		
		for (int i=0;i<PDS_CommonController.Count;i++){
			RightCorrect.Add(GetCorrectPairForInit(Left[i],Right));
			int ind = i;
			Left[i].GetComponent<PDS_LeftController>().InspectorIndex = ind;
			Left[i].SetActive(true);
		}
		
		for (int i=PDS_CommonController.Count;i<Left.Count;i++){
			Left[i].SetActive(false);
		}	
		for (int i=0;i<Right.Count;i++){
			Right[i].SetActive(false);
		}		
		for (int i=0;i<RightCorrect.Count;i++){
			RightCorrect[i].SetActive(true);
			int ind = i;
			RightCorrect[i].GetComponent<PDS_RightController>().InspectorIndex = ind;
		}			
			
		for (int i=0;i<PDS_CommonController.Count;i++){
			PDS_CommonController.LeftObjects.Add(new PDS_Left(){GameObject = Left[i]});			
			PDS_CommonController.RightObjects.Add(new PDS_Right(){GameObject = RightCorrect[i]});
			PDS_CommonController.RightObjects[i].TextComponent.text	= PDS_CommonController.RightObjects[i].Controller.PDS_Name;
		}
			
	}
	
	void FinalBtnClicked(){
		// Обработчик нажатия на кнопку "Проверить" в первом задании
		
		// Сброс предыдущих анимационных состояний
		for (int i=0;i<PDS_CommonController.LeftObjects.Count;i++){
			PDS_CommonController.LeftObjects[i].CorrectAnimator.SetBool("Active",false);
			PDS_CommonController.LeftObjects[i].WrongAnimator.SetBool("Active",false);
		}
		
		// Проверка - все ли объекты соединены
		int connected = 0;
		for (int i=0;i<PDS_CommonController.LeftObjects.Count;i++){
			if (PDS_CommonController.LeftObjects[i].Controller.HasPair == true){
				connected += 1;
			}
		}

		// Если не все, то подсветим несоединенные
		if (connected != PDS_CommonController.Count){
			for (int i=0;i<PDS_CommonController.LeftObjects.Count;i++){
				if (PDS_CommonController.LeftObjects[i].Controller.HasPair == false){
					PDS_CommonController.LeftObjects[i].NotReadyAnimator.Play("Unhide");
				}
			}
		} else {
			
			// Если все, то проверим правильность соединения
			int correct = 0;
			for (int i=0;i<PDS_CommonController.LeftObjects.Count;i++){
				// Если верно, то запустим анимационное состояние "Верно"
				if (String.Compare(PDS_CommonController.LeftObjects[i].Controller.PDS_Name,PDS_CommonController.LeftObjects[i].Controller.PairObject.Controller.PDS_Name) == 0){
					PDS_CommonController.LeftObjects[i].CorrectAnimator.SetBool("Active",true);
					PDS_CommonController.LeftObjects[i].CorrectAnimator.Play("Unhide");
					correct += 1;
				} else {
					// Соответственно для случая, если соединение неправильное
					PDS_CommonController.LeftObjects[i].WrongAnimator.SetBool("Active",true);
					PDS_CommonController.LeftObjects[i].WrongAnimator.Play("Unhide");
				}
			}
			
			// Если всё соединено верно
			if (correct == PDS_CommonController.Count){
				
				FinalBtn.interactable = false;
				
				CorrectAttempts += 1;
				
				// Запустим механизм перестановки правых объектов
				for (int i=0;i<PDS_CommonController.LeftObjects.Count;i++){
					int RightObjectIndex = PDS_CommonController.LeftObjects[i].Controller.PairObject.Controller.InspectorIndex;
					
					PDS_CommonController.LeftObjects[i].Controller.HideLine();
					
					PDS_CommonController.RightObjects[RightObjectIndex].Controller.RightPositionY = PDS_CommonController.LeftObjects[i].Position.y;
					PDS_CommonController.RightObjects[RightObjectIndex].Controller.RightPositionX = PDS_CommonController.LeftObjects[i].Position.x;					
					PDS_CommonController.RightObjects[RightObjectIndex].Controller.MoveToRightPlace = true;
				}
				
			}
			
			Attempts += 1;
			
			Stat.text = CorrectAttempts.ToString() + "\\" + Attempts.ToString();
		}		
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
					HS_ExitBtnClicked();
				}
        }
	}
}
