using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PDS_task_6_Init : MonoBehaviour
{

	public GameObject Blocks;
	public GameObject Places;
	public GameObject Back;
	
	public Button CheckBtn;
		
	public Animator CorrectAnimator;
	public Animator WrongAnimator;	
	
	public Text Stat;
	
	public GameObject MainScreen;
	public GameObject HelpScreen;
	
	public Button HS_BackBtn;
	public Button BtnHelp;
	public Button HS_ExitBtn;
	
	int Index = -1;
		
	int Correct = 0;
	int Wrong = 0;
		
	int Count;
		
	List<Vector3> StartPositions = new List<Vector3>();
		
	void Start(){
		
		Count = Blocks.transform.childCount;
		
		CheckBtn.onClick.RemoveAllListeners();
		CheckBtn.onClick.AddListener(() => CheckBtnClicked());
			
		BtnHelp.onClick.RemoveAllListeners();
		BtnHelp.onClick.AddListener(() => BtnHelpClicked());
		
		HS_BackBtn.onClick.RemoveAllListeners();
		HS_BackBtn.onClick.AddListener(() => HS_BackBtnClicked());
		
		HS_ExitBtn.onClick.RemoveAllListeners();
		HS_ExitBtn.onClick.AddListener(() => MM_Controller.HS_ExitBtnClicked());
			
		Transform b = Blocks.transform.GetChild(0);	
			
		for (int i=0;i<b.childCount;i++){
			StartPositions.Add(b.GetChild(i).position);
		}
		
		Next();
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
					MM_Controller.HS_ExitBtnClicked();
				}
        }
	}

	void BtnHelpClicked(){
		MainScreen.SetActive(false);
		HelpScreen.SetActive(true);		
	}
	
	void HS_BackBtnClicked(){
		MainScreen.SetActive(true);
		HelpScreen.SetActive(false);		
	}

	void Next(){
		
		if (Index != -1){
			// Выключаем предыдущие объекты
			Back.transform.GetChild(Index).gameObject.SetActive(false);
			Blocks.transform.GetChild(Index).gameObject.SetActive(false);
			Places.transform.GetChild(Index).gameObject.SetActive(false);
			
			// Вернем текущий пак на место
			for (int i=0;i<PDS_T6_Unit.Blocks.Count;i++){
				PDS_T6_Unit.Blocks[i].transform.position = StartPositions[i];
			}				
		}
		
		Index += 1;
		if (Index > Count-1){
			Index = 0;
		}	
		
		Back.transform.GetChild(Index).gameObject.SetActive(true);
		
		PDS_T6_Unit.Blocks.Clear();
		PDS_T6_Unit.Places.Clear();
		
		Transform b = Blocks.transform.GetChild(Index);
		Transform p = Places.transform.GetChild(Index);
		
		b.gameObject.SetActive(true);
		p.gameObject.SetActive(true);
		
		StartPositions.Clear();
		
		for (int i=0;i<b.childCount;i++){
			PDS_T6_Unit.Blocks.Add(b.GetChild(i).gameObject);
			PDS_T6_Unit.Places.Add(p.GetChild(i).gameObject);
			
			// Рвем связи
			PDS_T6_Unit.Places[i].GetComponent<PPS_Place>().PairObject = null;
						
			StartPositions.Add(PDS_T6_Unit.Blocks[i].transform.position);				
		}	
	}
	
	void CheckBtnClicked(){
		
		bool Check = true;
		bool Filled = true;
		
		Transform b = Blocks.transform.GetChild(Index);
		Transform p = Places.transform.GetChild(Index);
			
		for (int i=0;i<b.childCount;i++){
			GameObject pair = PDS_T6_Unit.Places[i].GetComponent<PPS_Place>().PairObject;
			
			if (pair == null){
				PDS_T6_Unit.Places[i].transform.GetChild(3).GetComponent<Animator>().Play("Unhide");
				Filled = false;
			}
		}
		if (Filled == true){
			for (int i=0;i<b.childCount;i++){
				if (PDS_T6_Unit.Places[i].GetComponent<PPS_Place>().PairObject != PDS_T6_Unit.Blocks[i]){
					Check = false;
					PDS_T6_Unit.Places[i].transform.GetChild(2).GetComponent<Animator>().Play("Unhide");
				} else {
					PDS_T6_Unit.Places[i].transform.GetChild(1).GetComponent<Animator>().Play("Unhide");
				}
			}
			if (Check == true){
				// Верно
				CorrectAnimator.Play("Unhide");
				Correct += 1;
				Next();
			} else {
				// Неверно
				WrongAnimator.Play("Unhide");
				Wrong += 1;
			}
			
			Stat.text = Correct.ToString() + "\\" + (Correct + Wrong).ToString();
		}
	}
}
