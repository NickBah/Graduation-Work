using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class recording_manager : MonoBehaviour { //, IPointerUpHandler, IPointerDownHandler  {

/*

	public GameObject AudSource;

	public GameObject ResultSlider;
	public GameObject ResultMark;	
	public GameObject NextButton;	
	
	
	// Частота дискретизации для записи
	int minFrequency, maxFrequency;
	
	AudioSource audio_ref;
	
	string current_device = "";
	
	Coroutine vis_recording_ref;
	
	// Use this for initialization
	void Start () {
		
		gameObject.GetComponent<Image>().color = new Color32(0,0,0,255);
		
		string[] devices = Microphone.devices;
		
		audio_ref = AudSource.GetComponent<AudioSource>();		
		
		if (devices.Length > 0){
			current_device = devices[0];		
					
			Microphone.GetDeviceCaps(current_device, out minFrequency, out maxFrequency);
			
			if(minFrequency == 0 && maxFrequency == 0)  
            {                 
                maxFrequency = 44100;  
            }
			
		} else {
			// Микрофон не обнаружен

		}	

		ResultSlider.SetActive (false);
		ResultMark.SetActive (false);	
		NextButton.SetActive (false);		
	}
	
	void recording_processing(){
		
		ResultSlider.SetActive (false);
		ResultMark.SetActive (false);		
		
		audio_ref.clip = Microphone.Start(current_device, false, 3, maxFrequency);	
		
		
	}
	
	void recording_stop(){
		Microphone.End(current_device);
		
		//audio_ref.Play();
		
		
		float[] samples = new float[audio_ref.clip.samples];
		
		audio_ref.clip.GetData(samples,0);
		
		//Debug.LogWarning("Запись. Кол-во каналов: " + audio_ref.clip.channels.ToString());
		//Debug.LogWarning("Запись. Частота: " + audio_ref.clip.frequency.ToString());
		//Debug.LogWarning("Запись. Длина: " + audio_ref.clip.length.ToString());

		
		
		
		gameObject.GetComponent<sound_manager>().StartComparison(samples);
		
	}
	
	IEnumerator vis_recording(){
			
			Vector3 start_scale = gameObject.transform.localScale;
			
			float min_scale = start_scale.x;
			float current_scale = start_scale.x;
			float max_scale = 1.07f;
			
			float delta_scale = 0.01f;
			bool scale_enhanced = false;

			while (true){
				
					if (scale_enhanced == false){
						if (current_scale < max_scale){
							current_scale += delta_scale;
						} else {
							scale_enhanced = true;
						}
					} else {
						if (current_scale > min_scale){	
							current_scale -= delta_scale;
						} else {
							scale_enhanced = false;
						}
					}
					
					gameObject.transform.localScale = new Vector3(current_scale,current_scale,start_scale.z);
					
					yield return new WaitForSeconds(0.02f);
			}
				
	}
	
	public void OnPointerDown (UnityEngine.EventSystems.PointerEventData e) {
		//Debug.Log("Запись началась");
		
		gameObject.GetComponent<Image>().color = new Color32(255,216,0,255);

		vis_recording_ref = StartCoroutine(vis_recording());
			
		recording_processing(); 
	}	
	
	public void OnPointerUp (UnityEngine.EventSystems.PointerEventData e) {
		//Debug.Log("Запись Закончилась");
		gameObject.GetComponent<Image>().color = new Color32(0,0,0,255);
		recording_stop(); 
		StopCoroutine(vis_recording_ref);	
	}	

	
	
	// Update is called once per frame
	void Update () {
		
	}
	
	*/
}
