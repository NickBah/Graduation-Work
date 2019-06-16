using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PDS_CommonController
{
	public static PDS_Left Left;
	public static PDS_Right Right;	
	
	public static GameObject FinalButton;
	public static GameObject ResultPanel;
	public static GameObject RestartButton;
	public static GameObject NextBtn;
	
	public static int Count = 4;
	
	public static int RightObjectsDoneTransfer = 0;
	public static int Attempts = 0;
	
	public static List<PDS_Right> RightObjects = new List<PDS_Right>();
	public static List<PDS_Left> LeftObjects = new List<PDS_Left>();
	
	public static List<PDS_Pair> Pairs = new List<PDS_Pair>();
	
	public static void AnotherBrickInTheWall(){
		// Еще один правый объект встал на своё место
		RightObjectsDoneTransfer += 1;
		
		if (RightObjectsDoneTransfer == Count){
			FinalScreen();
		}
	}
	
	public static void FinalScreen(){
		FinalButton.SetActive(false);
		
		if (NextBtn != null){
			NextBtn.SetActive(true);
		}		
	}
	
}