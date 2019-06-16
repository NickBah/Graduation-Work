using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Extensions {
	public static List<T> Shake<T>(this List<T> data){

		var temp = data[0];
		
		for (int i = data.Count - 1; i >= 1; i--)
		{
		  int j = UnityEngine.Random.Range(0,i + 1);
		  temp = data[j];
		  data[j] = data[i];
		  data[i] = temp;
		}
		return data;
	  }

	  public static void Print<T>(this List<T> data){
		
		string s = "";
		
		for (int i = 0; i < data.Count; i++)
		{
		  s += data[i].ToString();
		  if (i != data.Count-1){
			s += " ";
		  }
		}
		Debug.Log(s);
	  }
	  
	public static PM_Pair GetRightPair(this List<PM_Pair> data, string FileName){
		for (int i=0;i<data.Count;i++){
			if (String.Compare(data[i].FileName, FileName) == 0){
				return data[i];
			}
		}
		return null;
	}

	public static PDS_Pair GetRightPair(this List<PDS_Pair> data, string FileName){
		for (int i=0;i<data.Count;i++){
			if (String.Compare(data[i].FileName, FileName) == 0){
				return data[i];
			}
		}
		return null;
	}
	
	public static string Capitalize(this string s){

		if (s.Length == 0 || s == null){
			return s;
		}
		
		char f = Convert.ToChar(s[0].ToString().ToUpper());	
		string others = s.Substring(1,s.Length-1);
		
		return f + others;
	}
}
