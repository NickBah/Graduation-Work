using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PPR_Unit
{
	// Используется в Фонематическом представлении и восприятии
	
	public AudioClip Clip;
	public string AudioPath;
	public string Word;
	public string[] Sounds;
	public string FirstSound;
	
	public PPR_Unit(string pInitializer){
		
		// pInitializer: Имя файла|Написание слова|Первый звук
		
		string[] data = pInitializer.Split('|');
		
		AudioPath = data[0].Trim();
		Word = data[1].Trim();
		
		Sounds = data[2].Trim().Split(' ');

		FirstSound = Sounds[0];

		Clip = Resources.Load<AudioClip>("Words/" + AudioPath);
		
	}
	
}
