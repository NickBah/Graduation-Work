using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PDS_T4_Unit
{
	public string ColorName;
	public Color32 Color;
	public List<Color32> Options = new List<Color32>();
	
	public PDS_T4_Unit(string pColorName, string r, string g, string b){
		ColorName = pColorName;
		Color = new Color32(Convert.ToByte(r),Convert.ToByte(g),Convert.ToByte(b),Convert.ToByte(255));
	}
	
	public static bool Equal(Color32 a, Color32 b){
		return ((a.r == b.r) && (a.g == b.g) && (a.b == b.b));
	}
}
