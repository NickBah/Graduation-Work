using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PPA_Phoneme_Position {
	Start,
	Middle,
	End
}

public class PPA_Unit
{
	private static char[] Consonant = new char[]{'Б', 'В', 'Г', 'Д', 'Ж', 'З', 'Й', 'К', 'Л', 'М', 'Н', 'П', 'Р', 'С', 'Т', 'Ф', 'Х', 'Ч', 'Ц', 'Ш', 'Щ'};
	private static char[] Vowel = new char[]{'А', 'Е', 'Ё', 'И', 'О', 'У', 'Ы', 'Э', 'Ю', 'Я'};
	
	public AudioClip Clip;
	public string AudioPath;
	public string Character;
	public string Word;
	public PPA_Phoneme_Position Position;
	
	// Кол-во гласных
	public int VowelCount = 0;
	
	// Кол-во согласных
	public int ConsonantCount = 0;	
	
	// Кол-во звуков в слове
	public int Sounds;
	
	public bool IsCorrect(PPA_Phoneme_Position p){
		return p == Position;
	}
	
	public PPA_Unit(string pInitializer){
		
		// pInitializer: Имя файла|Написание слова|Искомый символ|Позиция символа|Кол-во звуков
		
		string[] data = pInitializer.Split('|');
		
		AudioPath = data[0].Trim();
		Word = data[1].Trim();
		Character = data[2].Trim();
		
		int p = Convert.ToInt32(data[3]);
		
		if (p == 0){
			Position = PPA_Phoneme_Position.Start;
		} else {
			if (p == 1){
				Position = PPA_Phoneme_Position.Middle;
			} else {
				Position = PPA_Phoneme_Position.End;
			}
		}
		
		Sounds = Convert.ToInt32(data[4]);
		
		Clip = Resources.Load<AudioClip>("Words/" + AudioPath);
		
		CalculateVowel();
		CalculateConsonant();
	}
	
	private void CalculateVowel(){
		string s = Word.ToUpper();
		for (int i=0;i<s.Length;i++){
			char a = s[i];
			if (Array.IndexOf(Vowel, a) != -1){
				VowelCount += 1;
			}
		}
	}
	
	private void CalculateConsonant(){
		string s = Word.ToUpper();
		for (int i=0;i<s.Length;i++){
			char a = s[i];
			if (Array.IndexOf(Consonant, a) != -1){
				ConsonantCount += 1;
			}
		}
	}	
}
