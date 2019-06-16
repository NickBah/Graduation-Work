using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PDS_task_2_Init : MonoBehaviour
{
	public Button FinalBtn;
	public GameObject[] LeftObjects;
	public GameObject[] RightObjects;
	
	public Text Stat;
	
	public GameObject MainScreen;
	public GameObject HelpScreen;
	
	public Button HS_BackBtn;
	public Button HS_ExitBtn;
	
	public Button BtnHelp;
	public Button NextBtn;
	
	public Toggle[] Toggles;
	
	List<Vector3> StartPositions = new List<Vector3>();
	
	int Index = -1;
	
	int CorrectAttempts = 0;
	int Attempts = 0;
	
	int Count = 4;
	
	List<PDS_T2_Group> Groups = new List<PDS_T2_Group>();
	
    void Awake()
    {
		
		PDS_CommonController.NextBtn = NextBtn.gameObject;
		
		PDS_CommonController.RightObjects.Clear();
		PDS_CommonController.LeftObjects.Clear();
		//PDS_CommonController.FinalButton = FinalBtn.gameObject;
							
		FinalBtn.onClick.RemoveAllListeners();
		FinalBtn.onClick.AddListener(() => FinalBtnClicked());
	
		PDS_CommonController.FinalButton = FinalBtn.gameObject;
		
		BtnHelp.onClick.RemoveAllListeners();
		BtnHelp.onClick.AddListener(() => BtnHelpClicked());
		
		HS_BackBtn.onClick.RemoveAllListeners();
		HS_BackBtn.onClick.AddListener(() => HS_BackBtnClicked());
		
		HS_ExitBtn.onClick.RemoveAllListeners();
		HS_ExitBtn.onClick.AddListener(() => MM_Controller.HS_ExitBtnClicked());
		
		NextBtn.onClick.RemoveAllListeners();
		NextBtn.onClick.AddListener(() => NextBtnClicked());
		
		for (int i=0;i<RightObjects.Length;i++){
			StartPositions.Add(RightObjects[i].transform.position);
		}		
		
		ReadData();	
		Next();
		
    }
	
	void NextBtnClicked(){
		Next();
		NextBtn.gameObject.SetActive(false);
		FinalBtn.gameObject.SetActive(true);
		FinalBtn.interactable = true;
	}
	
	void ReadData(){
		string[] data = read_file.loadFileContent(Application.streamingAssetsPath + "/DictionarySurvey/task_2.txt").Split('\n');
		
		for (int i=0;i<data.Length;i++){
			string[] line = data[i].Split('|');
			
			PDS_T2_Group Group = new PDS_T2_Group();
			
			List<PDS_T2_Unit> Pairs = new List<PDS_T2_Unit>();
			
			Pairs.Add(new PDS_T2_Unit(pFileName : line[0], pLabel : line[1]));
			Pairs.Add(new PDS_T2_Unit(pFileName : line[2], pLabel : line[3]));
			Pairs.Add(new PDS_T2_Unit(pFileName : line[4], pLabel : line[5]));
			Pairs.Add(new PDS_T2_Unit(pFileName : line[6], pLabel : line[7]));
			
			Group.Pairs = Pairs;
			Group.Options.Add(line[8]);
			Group.Options.Add(line[9]);
			Group.Options.Add(line[10]);
			Group.Options.Add(line[11]);
			Group.Options.Shake();
			Group.CorrectAnswer = line[8];
			
			Groups.Add(Group);
		}
		
		Groups.Shake();
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
		// Разорвем все связи
		for (int i=0;i<PDS_CommonController.RightObjects.Count;i++){
			PDS_CommonController.RightObjects[i].Controller.SetVarsToDefault();
		}
		for (int i=0;i<PDS_CommonController.LeftObjects.Count;i++){
			PDS_CommonController.LeftObjects[i].Controller.SetVarsToDefault();
		}		
		
		// Сброс предыдущих анимационных состояний
		for (int i=0;i<PDS_CommonController.LeftObjects.Count;i++){
			PDS_CommonController.LeftObjects[i].CorrectAnimator.SetBool("Active",false);
			PDS_CommonController.LeftObjects[i].WrongAnimator.SetBool("Active",false);
		}
		for (int i=0;i<Toggles.Length;i++){
			Toggles[i].gameObject.transform.GetChild(2).GetComponent<Animator>().SetBool("Active",false);
			Toggles[i].gameObject.transform.GetChild(3).GetComponent<Animator>().SetBool("Active",false);
			Toggles[i].isOn = false;
		}		
		
		for (int i=0;i<RightObjects.Length;i++){
			RightObjects[i].transform.position = StartPositions[i];
		}		
		
		PDS_CommonController.LeftObjects.Clear();
		PDS_CommonController.RightObjects.Clear();
				
		PDS_CommonController.RightObjectsDoneTransfer = 0;
		//PDS_CommonController.Attempts = 0;
		
		Index += 1;
		if (Index > Groups.Count - 1){
			Index = 0;
		}
		
		Groups[Index].Options.Shake();
		
		List<string> options = new List<string>();
		List<string> names = new List<string>();
		for (int i=0;i<Count;i++){
			options.Add(Groups[Index].Pairs[i].Label);
			names.Add(Groups[Index].Options[i]);
		}
		
		options.Shake();
		names.Shake();
		
		for (int i=0;i<Count;i++){
			PDS_CommonController.LeftObjects.Add(new PDS_Left(){GameObject = LeftObjects[i]});		
			PDS_CommonController.LeftObjects[i].Controller.PDS_Name = Groups[Index].Pairs[i].Label;
			
			PDS_CommonController.LeftObjects[i].ImageComponent.sprite = Groups[Index].Pairs[i].Sprite;
			
			PDS_CommonController.RightObjects.Add(new PDS_Right(){GameObject = RightObjects[i]});
			
			PDS_CommonController.RightObjects[i].TextComponent.text = options[i];
			PDS_CommonController.RightObjects[i].Controller.PDS_Name = options[i];			
		}
		
		for (int i=0;i<Toggles.Length;i++){
			Toggles[i].gameObject.transform.GetChild(1).GetComponent<Text>().text = names[i];
		}
	}
	
	void FinalBtnClicked(){
		// Обработчик нажатия на кнопку "Проверить" во втором задании
		
		// Сброс предыдущих анимационных состояний
		for (int i=0;i<PDS_CommonController.LeftObjects.Count;i++){
			PDS_CommonController.LeftObjects[i].CorrectAnimator.SetBool("Active",false);
			PDS_CommonController.LeftObjects[i].WrongAnimator.SetBool("Active",false);
		}
		for (int i=0;i<Toggles.Length;i++){
			Toggles[i].gameObject.transform.GetChild(2).GetComponent<Animator>().SetBool("Active",false);
			Toggles[i].gameObject.transform.GetChild(3).GetComponent<Animator>().SetBool("Active",false);
		}
		
		// Проверка - все ли объекты соединены
		int connected = 0;
		for (int i=0;i<PDS_CommonController.LeftObjects.Count;i++){
			if (PDS_CommonController.LeftObjects[i].Controller.HasPair == true){
				connected += 1;
			}
		}
		
		bool SomeToggleActive = false;
		for (int i=0;i<Toggles.Length;i++){
			if (Toggles[i].isOn == true){
				SomeToggleActive = true;
				break;
			}
		}
		
		// Если не все, то подсветим несоединенные
		if (connected != Count || SomeToggleActive == false){
			for (int i=0;i<PDS_CommonController.LeftObjects.Count;i++){
				if (PDS_CommonController.LeftObjects[i].Controller.HasPair == false){
					PDS_CommonController.LeftObjects[i].NotReadyAnimator.Play("Unhide");
				}
				if (SomeToggleActive == false){
					for (int j=0;j<Toggles.Length;j++){
						Toggles[j].gameObject.transform.GetChild(4).GetComponent<Animator>().Play("Unhide");
					}					
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
			
			bool ToggleCorrect = false;
			
			for (int i=0;i<Toggles.Length;i++){
				if (Toggles[i].isOn == true){
					if (String.Compare(Toggles[i].gameObject.transform.GetChild(1).GetComponent<Text>().text, Groups[Index].CorrectAnswer) == 0){
						Toggles[i].gameObject.transform.GetChild(2).GetComponent<Animator>().Play("Unhide");
						Toggles[i].gameObject.transform.GetChild(2).GetComponent<Animator>().SetBool("Active",true);
						ToggleCorrect = true;
						break;
					} else {
						Toggles[i].gameObject.transform.GetChild(3).GetComponent<Animator>().Play("Unhide");
						Toggles[i].gameObject.transform.GetChild(3).GetComponent<Animator>().SetBool("Active",true);
						break;
					}
				}
			}			
			
			// Если всё соединено верно
			if (correct == PDS_CommonController.Count && ToggleCorrect == true){
				
				FinalBtn.interactable = false;
				
				CorrectAttempts += 1;
				
				// Запустим механизм перестановки правых объектов
				for (int i=0;i<PDS_CommonController.LeftObjects.Count;i++){
					int RightObjectIndex = PDS_CommonController.LeftObjects[i].Controller.PairObject.Controller.InspectorIndex;
					
					PDS_CommonController.LeftObjects[i].Controller.HideLine();
					
					PDS_CommonController.RightObjects[RightObjectIndex].Controller.RightPositionY = PDS_CommonController.LeftObjects[i].Position.y;
					PDS_CommonController.RightObjects[RightObjectIndex].Controller.RightPositionX = PDS_CommonController.RightObjects[i].Position.x;					
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
