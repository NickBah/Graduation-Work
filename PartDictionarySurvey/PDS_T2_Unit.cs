using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PDS_T2_Unit
{
	public string FileName;
	public Sprite Sprite;
	public string Label;
	
	public PDS_T2_Unit(string pFileName, string pLabel){
		FileName = pFileName;
		Label = pLabel;
		Sprite = Resources.Load<Sprite>("DictionarySurvey/" + FileName);
	}
}
