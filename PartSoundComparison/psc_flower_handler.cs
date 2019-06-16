using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class psc_flower_handler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	public static main MainScript;
 
    void Awake()
    {
        MainScript = GameObject.Find("Main Camera").GetComponent<main>();
    } 
 
    public void OnPointerDown(PointerEventData eventData)
    {
        MainScript.RecordingStarted();
    }
	
 
    public void OnPointerUp(PointerEventData eventData)
    {
		MainScript.RecordingStoped();
    }	
	
}	
