using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class meta {

  public static void store_list_of_files(){
		string path = Application.streamingAssetsPath + "/Sounds/";
	  
		string[] letters = FileManager.GetDirsList(path + "/Letters/ru/");	
		FileManager.WriteFile(path + "/Letters/ru_list.txt",letters);	  
		
		for (int i=0;i<letters.Length;i++){
			string[] options = FileManager.GetFilesList(path + "/Letters/ru/" + letters[i],".wav");
			FileManager.WriteFile(path + "/Letters/ru/" + letters[i] + "/options_list.txt",options);
		}
		
  }
	
}
