using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PPP_Controller : MonoBehaviour
{
	///////// Inspector /////////////////
	public Button BtnYes;
	public Button BtnNo;
	
	public Button BtnPlay;
	public Button BtnHelp;
	
	public AudioSource Audio;
	
	public Text TargetCharacter;
	public Animator CorrectAnimator;
	public Animator WrongAnimator;
	
	public Text Stat;
	
	public GameObject MainScreen;
	public GameObject HelpScreen;
	
	public Button HS_BackBtn;
	public Button HS_ExitBtn;
	
	/////////////////////////////////////
			
	List<PPR_Unit> Units = new List<PPR_Unit>();
	List<string> Sounds = new List<string>();
	
	int Index = -1;
	int Correct = 0;
	int Wrong = 0;
		
	string Target;
		
	void Start(){
		BtnYes.onClick.RemoveAllListeners();
		BtnYes.onClick.AddListener(() => BtnYesClicked());
		
		BtnNo.onClick.RemoveAllListeners();
		BtnNo.onClick.AddListener(() => BtnNoClicked());		

		BtnPlay.onClick.RemoveAllListeners();
		BtnPlay.onClick.AddListener(() => BtnPlayClicked());

		BtnHelp.onClick.RemoveAllListeners();
		BtnHelp.onClick.AddListener(() => BtnHelpClicked());
		
		HS_BackBtn.onClick.RemoveAllListeners();
		HS_BackBtn.onClick.AddListener(() => HS_BackBtnClicked());
		
		HS_ExitBtn.onClick.RemoveAllListeners();
		HS_ExitBtn.onClick.AddListener(() => MM_Controller.HS_ExitBtnClicked());
		
		ReadData();
				
		// Список всех звуков
		for (int i=0;i<Units.Count;i++){
			for (int j=0;j<Units[i].Sounds.Length;j++){
				//Debug.Log(Sounds.Contains(Units[i].Sounds[j]));
				if (Sounds.Contains(Units[i].Sounds[j]) == false){
					Sounds.Add(Units[i].Sounds[j]);
				}
			}
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
	
	void ReadData(){
		string[] data = read_file.loadFileContent(Application.streamingAssetsPath + "/PhonemicRepresentation/data.txt").Split(new string[]{"\n"},StringSplitOptions.RemoveEmptyEntries);
		
		for (int i=0;i<data.Length;i++){
			Units.Add(new PPR_Unit(data[i]));
		}
		
		Units.Shake();
	}
	
	void Next(){
		Index += 1;
		
		if (Index > Units.Count-1){
			Index = 0;
			Units.Shake();
		}
		
		//Debug.Log(Sounds.Count);
		
		Target = Sounds[UnityEngine.Random.Range(0,Sounds.Count-1)];
		
		TargetCharacter.text = Target;
		
		BtnYes.interactable = false;
		BtnNo.interactable = false;
		
		/*
		BtnStart.interactable = false;
		BtnMiddle.interactable = false;
		BtnEnd.interactable = false;		
		*/	
	}
		
	void BtnYesClicked(){
		
		if (Array.IndexOf(Units[Index].Sounds, Target) != -1){
			CorrectAnimator.Play("Unhide");
			Correct += 1;
		} else {
			WrongAnimator.Play("Unhide");
			Wrong += 1;
		}
		
		Stat.text = Correct.ToString() + "\\" + (Correct + Wrong).ToString();
		
		Next();
	}
		
	void BtnNoClicked(){
		
		if (Array.IndexOf(Units[Index].Sounds, Target) == -1){
			CorrectAnimator.Play("Unhide");
			Correct += 1;
		} else {
			Wrong += 1;
			WrongAnimator.Play("Unhide");
		}		
		
		Stat.text = Correct.ToString() + "\\" + (Correct + Wrong).ToString();
		
		Next();
	}
		
	void BtnHelpClicked(){
		MainScreen.SetActive(false);
		HelpScreen.SetActive(true);		
	}
	
	void HS_BackBtnClicked(){
		MainScreen.SetActive(true);
		HelpScreen.SetActive(false);		
	}
	

	void BtnPlayClicked(){
		Audio.clip = Units[Index].Clip;
		Audio.Play();

		BtnYes.interactable = true;
		BtnNo.interactable = true;
		/*
		BtnStart.interactable = false;
		BtnMiddle.interactable = false;
		BtnEnd.interactable = false;		
		*/			
	}
}
