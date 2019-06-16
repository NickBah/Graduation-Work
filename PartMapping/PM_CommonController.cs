using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PM_CommonController
{
	public static PM_Left Left;
	public static PM_Right Right;	
	
	public static GameObject FinalButton;
	public static GameObject ResultPanel;
	public static GameObject RestartButton;
	
	public static int Count = 4;
	
	static int RightObjectsDoneTransfer = 0;
	static int Attempts = 0;
	
	public static List<PM_Right> RightObjects = new List<PM_Right>();
	public static List<PM_Left> LeftObjects = new List<PM_Left>();
	
	public static List<PM_Pair> Pairs = new List<PM_Pair>();
	
	public static void ReadData(){
		string[] data = read_file.loadFileContent(Application.streamingAssetsPath + "/Mapping/data.txt").Split('\n');
		
		for (int i=0;i<data.Length;i++){
			string[] line = data[i].Split('|');
			Pairs.Add(new PM_Pair(){FileName = line[0], Label = line[1]});
		}
	}
	
	public static void ChooseSomeElements(){	
		List<string> Imgs = new List<string>();
		List<string> Lbls = new List<string>();
		List<int> indexes = new List<int>();
		
		for (int i=0;i<Pairs.Count;i++){
			indexes.Add(i);
		}
		
		indexes.Shake();
		
		for (int i=0;i<Count;i++){
			int index = indexes[i];
			Imgs.Add(Pairs[index].FileName);
			Lbls.Add(Pairs[index].Label);
		}		
		
		Imgs.Shake();
		Lbls.Shake();
		
		for (int i=0;i<Count;i++){
			LeftObjects[i].ImageComponent.sprite = Resources.Load<Sprite>(PM_Pair.Folder + "/" + Imgs[i]);
			LeftObjects[i].CorrectPair = Pairs.GetRightPair(Imgs[i]);
			
			RightObjects[i].TextComponent.text = Lbls[i];
		}			

	}
	
	public static void CheckResults(){
		// Обработчик нажатия на кнопку "Проверить"
		
		// Сброс предыдущих анимационных состояний
		for (int i=0;i<LeftObjects.Count;i++){
			LeftObjects[i].CorrectAnimator.SetBool("Active",false);
			LeftObjects[i].WrongAnimator.SetBool("Active",false);
		}		
		
		// Проверка - все ли объекты соединены
		int connected = 0;
		for (int i=0;i<LeftObjects.Count;i++){
			if (LeftObjects[i].Controller.HasPair == true){
				connected += 1;
			}
		}
			
		// Если не все, то подсветим несоединенные
		if (connected != Count){
			for (int i=0;i<LeftObjects.Count;i++){
				if (LeftObjects[i].Controller.HasPair == false){
					LeftObjects[i].NotReadyAnimator.Play("Unhide");
				}
			}
		} else {
			Attempts += 1;
			
			// Если все, то проверим правильность соединения
			int correct = 0;
			for (int i=0;i<LeftObjects.Count;i++){
				// Если верно, то запустим анимационное состояние "Верно"
				if (String.Compare(LeftObjects[i].CorrectPairLabel,LeftObjects[i].Controller.PairObject.Label) == 0){
					LeftObjects[i].CorrectAnimator.SetBool("Active",true);
					LeftObjects[i].CorrectAnimator.Play("Unhide");
					correct += 1;
				} else {
					// Соответственно для случая, если соединение неправильное
					LeftObjects[i].WrongAnimator.SetBool("Active",true);
					LeftObjects[i].WrongAnimator.Play("Unhide");
				}
			}
			
			// Если всё соединено верно
			if (correct == Count){
				FinalButton.SetActive(false);
				
				// Запустим механизм перестановки правых объектов
				for (int i=0;i<LeftObjects.Count;i++){
					int RightObjectIndex = LeftObjects[i].Controller.PairObject.Controller.InspectorIndex;
					
					LeftObjects[i].Controller.HideLine();
					
					RightObjects[RightObjectIndex].Controller.RightPositionY = LeftObjects[i].Position.y;
					RightObjects[RightObjectIndex].Controller.MoveToRightPlace = true;
				}
				
			}
		}
	}
	
	public static void AnotherBrickInTheWall(){
		// Еще один правый объект встал на своё место
		RightObjectsDoneTransfer += 1;
		
		if (RightObjectsDoneTransfer == Count){
			FinalScreen();
		}
	}
	
	public static void FinalScreen(){
		ResultPanel.SetActive(true);
		
		if (Attempts == 1){
			ResultPanel.transform.GetChild(0).GetComponent<Text>().text = "Вы справились за " + Attempts.ToString() + " " + Case.GetInscription("Попытки",Attempts,CaseRegister.Lower) + "! Отличный результат!";	
		} else {
			ResultPanel.transform.GetChild(0).GetComponent<Text>().text = "Вы справились за " + Attempts.ToString() + " " + Case.GetInscription("Попытки",Attempts,CaseRegister.Lower) + ".";	
		}
		
		RestartButton.SetActive(true);
	}
	
	public static void Restart(){
		ResultPanel.SetActive(false);		
		RestartButton.SetActive(false);
		FinalButton.SetActive(true);		
		
		Attempts = 0;
		RightObjectsDoneTransfer = 0;	
		
		// Разорвем все связи
		for (int i=0;i<RightObjects.Count;i++){
			RightObjects[i].Controller.SetVarsToDefault();
		}
		for (int i=0;i<LeftObjects.Count;i++){
			LeftObjects[i].Controller.SetVarsToDefault();
		}
		
		
	}
}