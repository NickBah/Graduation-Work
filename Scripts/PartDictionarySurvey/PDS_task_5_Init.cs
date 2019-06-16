using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PDS_task_5_Init : MonoBehaviour
{

	public Text Stat;
	
	public GameObject MainScreen;
	public GameObject HelpScreen;
	
	public Button HS_BackBtn;
	public Button HS_ExitBtn;
	
	public Button BtnHelp;

	public Text QuestionText;
	
	public Text Middle;
	
	public Animator CorrectAnimator;
	public Animator WrongAnimator;	
	
	public ScrollRect StartScroll;
	public ScrollRect EndScroll;
	
	public GameObject LeftArrowUp;
	public GameObject LeftArrowDown;
	public GameObject RightArrowUp;
	public GameObject RightArrowDown;
	
	public Button FinalButton;
	
	int Index = -1;
	
	int CorrectAttempts = 0;
	int Attempts = 0;
	
	List<PDS_T5_Unit> Questions = new List<PDS_T5_Unit>();
	
	
	int StartOptionsCount = 4;
	int EndOptionsCount = 4;
	Color32 ActiveColor = new Color32(255,216,0,100);
	Color32 NormalColor = new Color32(255,255,255,100);
	
	public void OnPointerUp(PointerEventData eventData){
		Debug.Log(1);
	}
	
    void Awake()
    {
		
		BtnHelp.onClick.RemoveAllListeners();
		BtnHelp.onClick.AddListener(() => BtnHelpClicked());
		
		HS_BackBtn.onClick.RemoveAllListeners();
		HS_BackBtn.onClick.AddListener(() => HS_BackBtnClicked());
		
		HS_ExitBtn.onClick.RemoveAllListeners();
		HS_ExitBtn.onClick.AddListener(() => MM_Controller.HS_ExitBtnClicked());
		
		FinalButton.onClick.RemoveAllListeners();
		FinalButton.onClick.AddListener(() => FinalButtonClicked());
		
		ReadData();	
		Next();
		
		EventTrigger trigger = StartScroll.GetComponent<EventTrigger>( );
		EventTrigger.Entry entry = new EventTrigger.Entry( );
		entry.eventID = EventTriggerType.PointerClick;
		entry.callback.AddListener( ( data ) => { StartScrollControl(); } );
		trigger.triggers.Add( entry );
			
		entry = new EventTrigger.Entry( );
		entry.eventID = EventTriggerType.Drag;
		entry.callback.AddListener( ( data ) => { StartScrollControl(); } );
		trigger.triggers.Add( entry );
		
		entry = new EventTrigger.Entry( );
		entry.eventID = EventTriggerType.PointerUp;
		entry.callback.AddListener( ( data ) => { StartPointerUp(); } );
		trigger.triggers.Add( entry );		

		trigger = EndScroll.GetComponent<EventTrigger>( );
		entry = new EventTrigger.Entry( );
		entry.eventID = EventTriggerType.PointerClick;
		entry.callback.AddListener( ( data ) => { EndScrollControl(); } );
		trigger.triggers.Add( entry );
			
		entry = new EventTrigger.Entry( );
		entry.eventID = EventTriggerType.Drag;
		entry.callback.AddListener( ( data ) => { EndScrollControl(); } );
		trigger.triggers.Add( entry );
		
		entry = new EventTrigger.Entry( );
		entry.eventID = EventTriggerType.PointerUp;
		entry.callback.AddListener( ( data ) => { EndPointerUp(); } );
		trigger.triggers.Add( entry );		

		// Первый запуск
		StartScrollControl();
		EndScrollControl();
    }
	
	void StartPointerUp(){
		int focused = GetStartFocusedElementIndex();
		
		Vector3 p = StartScroll.content.transform.localPosition;	

		int offset = 0;
		if (focused != 0){
			offset = 3 * (focused);
		}
		
		p.y = focused * 140 + offset;
		
		StartScroll.content.transform.localPosition = p;
		
		if (focused == 0){
			Middle.text = Middle.text.Capitalize();
		} else {
			Middle.text = Questions[Index].Middle;
		}
	}
	
	void EndPointerUp(){
		int focused = GetEndFocusedElementIndex();
		
		Vector3 p = EndScroll.content.transform.localPosition;	

		int offset = 0;
		if (focused != 0){
			offset = 3 * (focused);
		}
		
		p.y = focused * 140 + offset;
		
		EndScroll.content.transform.localPosition = p;
	}	
	
	int GetStartFocusedElementIndex(){
		return Convert.ToInt32((StartOptionsCount-1) - Mathf.Round((StartOptionsCount-1) * StartScroll.verticalNormalizedPosition)) ;
	}
	
	int GetEndFocusedElementIndex(){
		return Convert.ToInt32((EndOptionsCount-1) - Mathf.Round((EndOptionsCount-1) * EndScroll.verticalNormalizedPosition)) ;
	}
	
	void StartScrollControl(){
		int focused = GetStartFocusedElementIndex();
		
		for (int i=0;i<StartScroll.content.transform.childCount;i++){
			if (i == focused){
				StartScroll.content.transform.GetChild(i).GetComponent<Animator>().SetBool("Active",true);
				//StartScroll.content.transform.GetChild(i).GetComponent<Image>().color = ActiveColor;
			} else {
				StartScroll.content.transform.GetChild(i).GetComponent<Animator>().SetBool("Active",false);
				//StartScroll.content.transform.GetChild(i).GetComponent<Image>().color = NormalColor;
			}
		}
		
		if (focused == 0){
			LeftArrowUp.SetActive(true);
		} else {
			LeftArrowUp.SetActive(false);
		}
		if (focused == StartOptionsCount-1){
			LeftArrowDown.SetActive(true);			
		} else {
			LeftArrowDown.SetActive(false);	
		}
	}
	
	void EndScrollControl(){
		int focused = GetEndFocusedElementIndex();
		
		for (int i=0;i<EndScroll.content.transform.childCount;i++){
			if (i == focused){
				EndScroll.content.transform.GetChild(i).GetComponent<Animator>().SetBool("Active",true);
				//EndScroll.content.transform.GetChild(i).GetComponent<Image>().color = ActiveColor;
			} else {
				EndScroll.content.transform.GetChild(i).GetComponent<Animator>().SetBool("Active",false);
				//EndScroll.content.transform.GetChild(i).GetComponent<Image>().color = NormalColor;
			}
		}
		
		if (focused == 0){
			RightArrowUp.SetActive(true);		
		} else {
			RightArrowUp.SetActive(false);
		}	
		if (focused == EndOptionsCount-1){
			RightArrowDown.SetActive(true);			
		} else {
			RightArrowDown.SetActive(false);
		}
	}
	
	void FinalButtonClicked(){
		int start_ind = GetStartFocusedElementIndex();
		int end_ind = GetEndFocusedElementIndex();
		
		string start = StartScroll.content.transform.GetChild(start_ind).GetChild(0).GetComponent<Text>().text;
		string end = EndScroll.content.transform.GetChild(end_ind).GetChild(0).GetComponent<Text>().text;
		
		string result = start + Questions[Index].Middle + end;
		
		if (result.Length > Questions[Index].CorrectAnswer.Length){
			result = result.Substring(0,Questions[Index].CorrectAnswer.Length);
		}		
		
		if (String.CompareOrdinal(result.ToUpper(),Questions[Index].CorrectAnswer.ToUpper()) == 0){
			CorrectAnimator.Play("Unhide");
			CorrectAttempts += 1;
			Next();
		} else {
			WrongAnimator.Play("Unhide");
		}
		Attempts += 1;

		Stat.text = CorrectAttempts.ToString() + "\\" + Attempts.ToString();
	}
	
	void NextBtnClicked(){
		Next();
	}
	
	void ReadData(){
		string[] data = read_file.loadFileContent(Application.streamingAssetsPath + "/DictionarySurvey/task_5.txt").Split('\n');
		
		for (int i=0;i<data.Length;i++){
			Questions.Add(new PDS_T5_Unit(data[i]));
		}
		
		Questions.Shake();
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
		MM_Controller.HS_ExitBtnClicked();
	}	
	
	void Next(){
		Index += 1;
		if (Index > Questions.Count - 1){
			Index = 0;
			Questions.Shake();
		}
		
		QuestionText.text = Questions[Index].Question;
			
		Middle.text = Questions[Index].Middle.Capitalize();
			
		for (int i=0;i<EndScroll.content.transform.childCount;i++){
			EndScroll.content.transform.GetChild(i).GetChild(0).GetComponent<Text>().text = Questions[Index].EndOptions[i];
			StartScroll.content.transform.GetChild(i).GetChild(0).GetComponent<Text>().text = Questions[Index].StartOptions[i];
		}
		
		Vector3 p = StartScroll.content.transform.localPosition;
		p.y = 0;
		StartScroll.content.transform.localPosition = p;
		
		p = EndScroll.content.transform.localPosition;
		p.y = 0;
		EndScroll.content.transform.localPosition = p;
		
		StartScrollControl();
		EndScrollControl();
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
