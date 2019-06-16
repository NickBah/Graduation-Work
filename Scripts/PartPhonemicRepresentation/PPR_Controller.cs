using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PPR_Controller : MonoBehaviour
{
	///////// Inspector /////////////////
	public Toggle[] Toggles;
	
	public Button BtnPlay;
	public Button BtnHelp;
	
	public AudioSource Audio;
	
	public Text Stat;
	
	public GameObject MainScreen;
	public GameObject HelpScreen;
	
	public Button HS_BackBtn;
	public Button HS_ExitBtn;
	
	public Button CheckBtn;
	
	/////////////////////////////////////
			
	List<PPR_Unit> Units = new List<PPR_Unit>();
	List<string> Same = new List<string>();
	List<string> Others = new List<string>();
	
	List<int> indexes = new List<int>();
	
	// Кол-во верных слов, которое надо найти
	int N = 0;
	
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
		HS_ExitBtn.onClick.AddListener(() => MM_Controller.HS_ExitBtnClicked());
		
		CheckBtn.onClick.RemoveAllListeners();
		CheckBtn.onClick.AddListener(() => CheckBtnClicked());
		
		for (int i=0;i<Toggles.Length;i++){
			indexes.Add(i);
		}		
		
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
				
		// Найдем слова с таким же первым звуком
		Same.Clear();
		Others.Clear();
		for (int i=0;i<Units.Count;i++){
			if (i != Index){
				if (String.Compare(Units[Index].FirstSound, Units[i].FirstSound) == 0){
					Same.Add(Units[i].Word);
				} else {
					Others.Add(Units[i].Word);
				}
			}
		}
		
		// Выберем несколько
		Same.Shake();
		Others.Shake();
		
		if (Same.Count == 0){
			Debug.LogError("Отсутствуют слова, начинающиеся на тот же звук, что и [" + Units[Index].Word + "]. Необходимо добавить такие.");
			return;
		}
		
		N = UnityEngine.Random.Range(1,Same.Count);
		indexes.Shake();
		for (int i=0;i<N;i++){
			Toggles[indexes[i]].transform.GetChild(1).GetComponent<Text>().text = Same[i];
		}
		
		for (int i=N;i<Toggles.Length;i++){
			Toggles[indexes[i]].transform.GetChild(1).GetComponent<Text>().text = Others[i];
		}		
		
		for (int i=0;i<Toggles.Length;i++){
			Toggles[i].interactable = false;
			Toggles[i].isOn = false;
		}
		
		CheckBtn.interactable = false;
		
		/*
		BtnStart.interactable = false;
		BtnMiddle.interactable = false;
		BtnEnd.interactable = false;		
		*/	
	}
	
	void PlayAnimation(bool Correct){
		if (Correct == true){
			//CorrectAnimator.Play("Unhide");
		} else {
			//WrongAnimator.Play("Unhide");
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
	
	void CheckBtnClicked(){
		string s;
		
		bool Done = true;
		int count = 0;
		
		for (int i=0;i<Toggles.Length;i++){
			s = Toggles[i].transform.GetChild(1).GetComponent<Text>().text;
			
			if (Same.Contains(s) && Toggles[i].isOn == true){
				Toggles[i].transform.GetChild(2).GetComponent<Animator>().Play("Unhide");
				count += 1;
			}
			if (Others.Contains(s) && Toggles[i].isOn == true){
				Toggles[i].transform.GetChild(3).GetComponent<Animator>().Play("Unhide");
				Done = false;
			}
		}
		
		if (count == N && Done == true){
			Correct += 1;
			Next();
		} else {
			Wrong += 1;
		}
		
		Stat.text = Correct.ToString() + "\\" + (Correct + Wrong).ToString();
	}
	

	void BtnPlayClicked(){
		//Units[Index].Clip
		
		Audio.clip = Units[Index].Clip;
		Audio.Play();
		
		for (int i=0;i<Toggles.Length;i++){
			Toggles[i].interactable = true;
		}
		
		CheckBtn.interactable = true;
		
		/*
		BtnStart.interactable = true;
		BtnMiddle.interactable = true;
		BtnEnd.interactable = true;		
		*/
	}
}
