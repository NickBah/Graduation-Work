using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PDS_T5_Unit
{
	public string Question;
	
	public string Middle;
	public List<string> StartOptions = new List<string>();
	public List<string> EndOptions = new List<string>();
	
	public string CorrectAnswer;
	
	public PDS_T5_Unit(string Init){
		string[] line = Init.Split('|');
		
		Question = line[0];
		CorrectAnswer = line[1];
		
		Middle = line[2];
		
		StartOptions.Add(line[3]);
		StartOptions.Add(line[4]);
		StartOptions.Add(line[5]);
		StartOptions.Add(line[6]);
		
		EndOptions.Add(line[7]);
		EndOptions.Add(line[8]);
		EndOptions.Add(line[9]);
		EndOptions.Add(line[10]);
	}
}
