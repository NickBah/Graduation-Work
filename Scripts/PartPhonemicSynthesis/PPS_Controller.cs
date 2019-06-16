using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PPS_Controller : MonoBehaviour
{

	public GameObject[] Blocks;
	public GameObject[] Places;
	
	public Button CheckBtn;
		
	public Animator CorrectAnimator;
	public Animator WrongAnimator;	
	
	public Text Stat;
	
	public GameObject MainScreen;
	public GameObject HelpScreen;
	
	public Button HS_BackBtn;
	public Button BtnHelp;
	public Button HS_ExitBtn;
	
	List<PPS_Data> Data = new List<PPS_Data>();
		
	int Index = -1;
		
	int Correct = 0;
	int Wrong = 0;
		
	Vector3[] StartPositions;
		
	void Start(){
		PPS_Common.Blocks = Blocks;
		PPS_Common.Places = Places;
		
		CheckBtn.onClick.RemoveAllListeners();
		CheckBtn.onClick.AddListener(() => CheckBtnClicked());
			
		BtnHelp.onClick.RemoveAllListeners();
		BtnHelp.onClick.AddListener(() => BtnHelpClicked());
		
		HS_BackBtn.onClick.RemoveAllListeners();
		HS_BackBtn.onClick.AddListener(() => HS_BackBtnClicked());
		
		HS_ExitBtn.onClick.RemoveAllListeners();
		HS_ExitBtn.onClick.AddListener(() => MM_Controller.HS_ExitBtnClicked());
			
		StartPositions = new Vector3[Blocks.Length];
		for (int i=0;i<Blocks.Length;i++){
			StartPositions[i] = Blocks[i].transform.position;
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

	void BtnHelpClicked(){
		MainScreen.SetActive(false);
		HelpScreen.SetActive(true);		
	}
	
	void HS_BackBtnClicked(){
		MainScreen.SetActive(true);
		HelpScreen.SetActive(false);		
	}
	
	void ReadData(){
		string[] data = read_file.loadFileContent(Application.streamingAssetsPath + "/PhonemicSynthesis/data.txt").Split(new string[]{"\n"},StringSplitOptions.RemoveEmptyEntries);
		
		for (int i=0;i<data.Length;i++){
			Data.Add(new PPS_Data(data[i]));
		}
		
		Data.Shake();		
	}
	
	void Next(){
		Index += 1;
		if (Index > Data.Count-1){
			Index = 0;
			Data.Shake();
		}	
		
		Data[Index].Letters.Shake();
		
		for (int i=0;i<Blocks.Length;i++){
			Blocks[i].transform.GetChild(0).GetComponent<Text>().text = Data[Index].Letters[i].ToString();
		}
		
		// Рвем связи
		for (int i=0;i<Places.Length;i++){
			Places[i].GetComponent<PPS_Place>().PairObject = null;
		}	
		
		for (int i=0;i<Blocks.Length;i++){
			Blocks[i].transform.position = StartPositions[i];
		}		
	}
	
	void CheckBtnClicked(){
		string word = "";
		
		for (int i=0;i<Places.Length;i++){
			GameObject p = Places[i].GetComponent<PPS_Place>().PairObject;
			
			if (p == null){
				WrongAnimator.Play("Unhide");		
				return;
			}
			word += p.transform.GetChild(0).GetComponent<Text>().text;					
		}
		
		if (Data[Index].CorrectWord(word) == true){
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
