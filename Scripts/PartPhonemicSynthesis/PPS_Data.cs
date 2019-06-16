using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PPS_Data 
{
	public List<string> Words = new List<string>();
	public List<char> Letters = new List<char>();
	
	public PPS_Data(string pInitializer){
		string[] data = pInitializer.Split('|');
		
		for (int i=0;i<data.Length;i++){
			Words.Add(data[i].Trim().ToLower());
		}
		
		for (int i=0;i<Words[0].Length;i++){
			Letters.Add(Words[0][i]);
		}
	}
	
	public bool CorrectWord(string pWord){
		for (int i=0;i<Words.Count;i++){			
			if (String.Compare(pWord.ToLower(),Words[i]) == 0){
				return true;
			}
		}			
		return false;
	}
}
