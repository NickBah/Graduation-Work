using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class common {
	// Таким образом, записи, чей размер больше 1073741824 сэмплов, будут урезаться 
	public static int getThePowerOf2(int input){
		for (int i=0;i<30;i++){
			double border = Math.Pow(2,i+1);
			if (border >= input){
				return Convert.ToInt32(border);
			}
		}
		
		return Convert.ToInt32(Math.Pow(2,30));
	}	
}
