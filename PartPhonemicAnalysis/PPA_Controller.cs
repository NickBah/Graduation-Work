using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PPA_Controller : MonoBehaviour
{
	///////// Inspector /////////////////
	public Button BtnStart;
	public Button BtnMiddle;
	public Button BtnEnd;
	
	public Text TextCharacter;
	
	public Button BtnPlay;
	public Button BtnHelp;
	
	public AudioSource Audio;
	
	public Animator CorrectAnimator;
	public Animator WrongAnimator;
	
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
		BtnStart.onClick.RemoveAllListeners();
		BtnStart.onClick.AddListener(() => BtnStartClicked());

		BtnMiddle.onClick.RemoveAllListeners();
		BtnMiddle.onClick.AddListener(() => BtnMiddleClicked());
		
		BtnEnd.onClick.RemoveAllListeners();
		BtnEnd.onClick.AddListener(() => BtnEndClicked());

		BtnPlay.onClick.RemoveAllListeners();
		BtnPlay.onClick.AddListener(() => BtnPlayClicked());

		BtnHelp.onClick.RemoveAllListeners();
		BtnHelp.onClick.AddListener(() => BtnHelpClicked());
		
		HS_BackBtn.onClick.RemoveAllListeners();
		HS_BackBtn.onClick.AddListener(() => HS_BackBtnClicked());
		
		HS_ExitBtn.onClick.RemoveAllListeners();
		HS_ExitBtn.onClick.AddListener(() => MM_Controller.HS_ExitBtnClicked());
		
		CorrectAnimator.SetBool("Active",false);
		WrongAnimator.SetBool("Active",false);
		
		ReadData();
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
		string[] data = read_file.loadFileContent(Application.streamingAssetsPath + "/PhonemicAnalysis/data.txt").Split(new string[]{"\n"},StringSplitOptions.RemoveEmptyEntries);
		
		for (int i=0;i<data.Length;i++){
			Units.Add(new PPA_Unit(data[i]));
		}
		
		Units.Shake();
	}
	
	void Next(){
		CorrectAnimator.SetBool("Active",false);
		WrongAnimator.SetBool("Active",false);
		
		Index += 1;
		
		if (Index > Units.Count-1){
			Index = 0;
			Units.Shake();
		}
		
		TextCharacter.text = Units[Index].Character;
		
		BtnStart.interactable = false;
		BtnMiddle.interactable = false;
		BtnEnd.interactable = false;		
	}
	
	void PlayAnimation(bool Correct){
		if (Correct == true){
			CorrectAnimator.SetBool("Active",true);
			CorrectAnimator.Play("Unhide");
		} else {
			WrongAnimator.SetBool("Active",true);
			WrongAnimator.Play("Unhide");
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
	
	void BtnStartClicked(){
		
		
		if (Units[Index].IsCorrect(PPA_Phoneme_Position.Start) == true){
			PlayAnimation(true);			
			Correct += 1;			
		} else {
			PlayAnimation(false);		
			Wrong += 1;
		}
		
		Stat.text = Correct.ToString() + "\\" + (Correct + Wrong).ToString();
		
		Next();
	}
	
	void BtnMiddleClicked(){
		if (Units[Index].IsCorrect(PPA_Phoneme_Position.Middle) == true){
			PlayAnimation(true);
			Correct += 1;
		} else {
			PlayAnimation(false);
			Wrong += 1;
		}
		
		Stat.text = Correct.ToString() + "\\" + (Correct + Wrong).ToString();
		
		Next();
	}
	
	void BtnEndClicked(){
		if (Units[Index].IsCorrect(PPA_Phoneme_Position.End) == true){
			PlayAnimation(true);
			Correct += 1;
		} else {
			PlayAnimation(false);
			Wrong += 1;
		}		
		
		Stat.text = Correct.ToString() + "\\" + (Correct + Wrong).ToString();
		
		Next();
	}
	
	void BtnPlayClicked(){
		//Units[Index].Clip
		
		Audio.clip = Units[Index].Clip;
		Audio.Play();
		
		BtnStart.interactable = true;
		BtnMiddle.interactable = true;
		BtnEnd.interactable = true;		
	}
}
