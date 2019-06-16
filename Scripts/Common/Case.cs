using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CaseRegister{
	Capital,
	Upper,
	Lower
}

public class Word{
	public string Origin;
	
	public string TenTwenty;
	public string One;
	public string TwoFour;
	public string ZeroFiveNine;
	
	public Word(string pOrigin){
		Origin = pOrigin;
	}
	
	public string GetInscription(int Count){
		int a = Count % 10;
		int b = Count % 100;
		
		if (b > 10 && b < 20){
			return TenTwenty;
		} else {			
			if (a == 1){
				return One;
			}
			if (a >= 2 && a <= 4){
				return TwoFour;
			}
			if ((a >= 5 && a <= 9) || (a == 0)){
				return ZeroFiveNine;
			}
		}
		return "Error in class Word [Case.cs]";
	}
}

public static class Case {
	
	static List<Word> words = new List<Word>(){
		new Word("Попытка"){TenTwenty = "Попыток", One = "Попытка", TwoFour = "Попытки", ZeroFiveNine = "Попыток"},
		new Word("Попытки"){TenTwenty = "Попыток", One = "Попытку", TwoFour = "Попытки", ZeroFiveNine = "Попыток"}
	};
	
	public static string GetInscription(string pWord, int pCount, CaseRegister pRegister = CaseRegister.Capital){
		for (int i=0;i<words.Count;i++){
			string req = pWord.ToUpper();
			string data = words[i].Origin.ToUpper();
			
			if (String.Compare(req,data) == 0){
				if (pRegister == CaseRegister.Capital){
					return words[i].GetInscription(pCount);
				}
				if (pRegister == CaseRegister.Upper){
					return words[i].GetInscription(pCount).ToUpper();
				}
				return words[i].GetInscription(pCount).ToLower();
			}
		}
		
		return "Не указано произношение для слова [" + pWord + "]";
	}
	
}
