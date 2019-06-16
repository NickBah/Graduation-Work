using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PDS_task_4_Init : MonoBehaviour
{
	public Button[] Buttons;
	
	public Text Stat;
	
	public GameObject MainScreen;
	public GameObject HelpScreen;
	
	public Button HS_BackBtn;
	public Button HS_ExitBtn;
	
	public Button BtnHelp;

	public Text QuestionText;
	
	public Animator CorrectAnimator;
	public Animator WrongAnimator;	
	
	int Index = -1;
	
	int CorrectAttempts = 0;
	int Attempts = 0;
	
	List<PDS_T4_Unit> Questions = new List<PDS_T4_Unit>();
	
    void Awake()
    {
		
		BtnHelp.onClick.RemoveAllListeners();
		BtnHelp.onClick.AddListener(() => BtnHelpClicked());
		
		HS_BackBtn.onClick.RemoveAllListeners();
		HS_BackBtn.onClick.AddListener(() => HS_BackBtnClicked());
		
		HS_ExitBtn.onClick.RemoveAllListeners();
		HS_ExitBtn.onClick.AddListener(() => MM_Controller.HS_ExitBtnClicked());
		
		for (int i=0;i<Buttons.Length;i++){
			int ind = i;
			Buttons[i].onClick.RemoveAllListeners();
			Buttons[i].onClick.AddListener(() => AnswerBtnClicked(ind));			
		}
		
		ReadData();	
		Next();
		
    }
	
	void AnswerBtnClicked(int ind){
		Color32 c = Buttons[ind].GetComponent<Image>().color;
		
		if (PDS_T4_Unit.Equal(Questions[Index].Color, c) == true){
			CorrectAnimator.Play("Unhide");
			CorrectAttempts += 1;
		} else {
			WrongAnimator.Play("Unhide");
		}
		Attempts += 1;
		
		Next();
		
		Stat.text = CorrectAttempts.ToString() + "\\" + Attempts.ToString();
	}
	
	void NextBtnClicked(){
		Next();
	}
	
	void ReadData(){
		string[] data = read_file.loadFileContent(Application.streamingAssetsPath + "/DictionarySurvey/task_4.txt").Split('\n');
		
		for (int i=0;i<data.Length;i++){
			string[] line = data[i].Split('|');
			
			Questions.Add(new PDS_T4_Unit(line[0].Trim(),line[1].Trim(),line[2].Trim(),line[3].Trim()));
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
		
		List<Color32> options = new List<Color32>();
		for (int i=0;i<Questions.Count;i++){
			if (i != Index){
				options.Add(Questions[i].Color);
			}
		}
		
		Questions[Index].Options = options;
		Questions[Index].Options.Shake();
		
		Questions[Index].Options[UnityEngine.Random.Range(0,3)] = Questions[Index].Color;	
		
		for (int i=0;i<Buttons.Length;i++){
			Buttons[i].gameObject.GetComponent<Image>().color = Questions[Index].Options[i];
		}
		
		QuestionText.text = Questions[Index].ColorName;
			
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
