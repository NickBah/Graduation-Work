using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Задание 11
public class PPA_11_Controller : MonoBehaviour
{
	///////// Inspector /////////////////

	public InputField InpFirst;
	public InputField InpSecond;
	
	public Button BtnPlay;
	public Button BtnCheck;
	public Button BtnHelp;
	
	public AudioSource Audio;

	public Animator LeftNotReadyAnimator;
	public Animator LeftCorrectAnimator;
	public Animator LeftWrongAnimator;
	
	public Animator RightNotReadyAnimator;
	public Animator RightCorrectAnimator;
	public Animator RightWrongAnimator;
	
	public Text Stat;
	
	public GameObject MainScreen;
	public GameObject HelpScreen;
	
	public Button HS_BackBtn;
	public Button HS_ExitBtn;
	
	/////////////////////////////////////
			
	List<PPA_Unit> Units = new List<PPA_Unit>();
	int Index = -1;
	int Correct = 0;
	int Wrong = 0;
		
	void Start(){

		BtnPlay.onClick.RemoveAllListeners();
		BtnPlay.onClick.AddListener(() => BtnPlayClicked());

		BtnHelp.onClick.RemoveAllListeners();
		BtnHelp.onClick.AddListener(() => BtnHelpClicked());
		
		HS_BackBtn.onClick.RemoveAllListeners();
		HS_BackBtn.onClick.AddListener(() => HS_BackBtnClicked());

		HS_ExitBtn.onClick.RemoveAllListeners();
		HS_ExitBtn.onClick.AddListener(() => HS_ExitBtnClicked());
		
		BtnCheck.onClick.RemoveAllListeners();
		BtnCheck.onClick.AddListener(() => BtnCheckClicked());
		
		LeftCorrectAnimator.SetBool("Active",false);
		LeftWrongAnimator.SetBool("Active",false);
		RightCorrectAnimator.SetBool("Active",false);
		RightWrongAnimator.SetBool("Active",false);		
		
		ReadData();
		Next();
	}
	
	void ReadData(){
		string[] data = read_file.loadFileContent(Application.streamingAssetsPath + "/PhonemicAnalysis/data.txt").Split(new string[]{"\n"},StringSplitOptions.RemoveEmptyEntries);
		
		for (int i=0;i<data.Length;i++){
			Units.Add(new PPA_Unit(data[i]));
		}
		
		Units.Shake();
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
	
	void Next(){
		LeftCorrectAnimator.SetBool("Active",false);
		LeftWrongAnimator.SetBool("Active",false);
		RightCorrectAnimator.SetBool("Active",false);
		RightWrongAnimator.SetBool("Active",false);
		
		Index += 1;
		
		if (Index > Units.Count-1){
			Index = 0;
			Units.Shake();
		}
		
		InpFirst.text = "";
		InpSecond.text = "";
		
		BtnCheck.interactable = false;
		/*
		BtnStart.interactable = false;
		BtnMiddle.interactable = false;
		BtnEnd.interactable = false;
		*/		
	}
	
	void BtnCheckClicked(){
		
		bool Done = true;
		
		if (InpFirst.text.Length == 0){
			LeftNotReadyAnimator.Play("Unhide");
			Done = false;
		}
		if (InpSecond.text.Length == 0){
			RightNotReadyAnimator.Play("Unhide");
			Done = false;
		}
	
		if (Done == false){
			return;
		}
		
		int v = -1;
		int c = -1;
		
		try{
			v = Convert.ToInt32(InpFirst.text);		
		} catch (Exception e){
			LeftNotReadyAnimator.Play("Unhide");
			Done = false;
		}
		try{
			c = Convert.ToInt32(InpSecond.text);	
		} catch (Exception e){
			RightNotReadyAnimator.Play("Unhide");
			Done = false;
		}
		
		if (Done == false){
			return;
		}
		
		if (v == Units[Index].VowelCount){
			// Верно
			LeftCorrectAnimator.SetBool("Active",true);
			LeftCorrectAnimator.Play("Unhide");			
		} else {
			// Неверно
			LeftWrongAnimator.SetBool("Active",true);
			LeftWrongAnimator.Play("Unhide");
			Done = false;		
		}
		
		if (c == Units[Index].ConsonantCount){
			// Верно
			RightCorrectAnimator.SetBool("Active",true);
			RightCorrectAnimator.Play("Unhide");					
		} else {
			// Неверно
			RightWrongAnimator.SetBool("Active",true);
			RightWrongAnimator.Play("Unhide");			
			Done = false;
		}		

		if (Done == true){
			Correct += 1;			
			Next();
		} else {
			Wrong += 1;
		}
		Stat.text = Correct.ToString() + "\\" + (Correct + Wrong).ToString();
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
	
	void BtnPlayClicked(){
		//Units[Index].Clip
		
		Audio.clip = Units[Index].Clip;
		Audio.Play();
		
		BtnCheck.interactable = true;
		

		/*
		BtnStart.interactable = true;
		BtnMiddle.interactable = true;
		BtnEnd.interactable = true;	
		*/		
	}
}
