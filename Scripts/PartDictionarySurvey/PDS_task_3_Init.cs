using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PDS_task_3_Init : MonoBehaviour
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
	
	List<PDS_T3_Unit> Questions = new List<PDS_T3_Unit>();
	
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
		string a = Buttons[ind].transform.GetChild(0).GetComponent<Text>().text;
		
		if (String.Compare(a,Questions[Index].CorrectAnswer) == 0){
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
		string[] data = read_file.loadFileContent(Application.streamingAssetsPath + "/DictionarySurvey/task_3.txt").Split('\n');
		
		for (int i=0;i<data.Length;i++){
			string[] line = data[i].Split('|');
			
			PDS_T3_Unit Question = new PDS_T3_Unit();
			
			Question.Question = line[0];
			Question.Options.Add(line[1]);
			Question.Options.Add(line[2]);
			Question.Options.Add(line[3]);
			Question.Options.Add(line[4]);
			Question.Options.Add(line[5]);
			Question.Options.Add(line[6]);
			Question.Options.Shake();
			Question.CorrectAnswer = line[1];
			
			Questions.Add(Question);
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
		
		Questions[Index].Options.Shake();
		
		for (int i=0;i<Buttons.Length;i++){
			Buttons[i].transform.GetChild(0).GetComponent<Text>().text = Questions[Index].Options[i];
		}
		
		QuestionText.text = Questions[Index].Question;
			
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
