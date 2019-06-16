using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
	

// Спектр окна
public class Spectrum {
	// Набор значений, полученный путем FFT
	public Complex[] fft_result = new Complex[0];
	
	// Массив амплитуд (модуль комплексного числа)
	public float[] amplitude = new float[0];
	
	// Массив фаз (тангенс комплексного числа)
	public float[] phase = new float[0];	
			
	// Массив частот 
	public float[] frequences = new float[0];
	
	// В представлении децибеллов
	public float[] db = new float[0];
		
	// Локальные максимумы амлитуды
	public array_element[] local_max = new array_element[0];
		
	public void SetSpectrumData(Complex[] p, float sound_frequency){
		fft_result = p;
	
		frequences = new float[p.Length];
	
		for (int i=0;i<p.Length;i++){
			frequences[i] = i * sound_frequency / p.Length;
		}
	
		// Возьмем половину. Дальше центра - симметричное отображение первой половины
		amplitude = new float[p.Length / 2];
			
		//float max = 0.0f;
		for (int i=0;i<amplitude.Length;i++){
			//amplitude[i] = Convert.ToSingle(p[i].Abs / sound.window_size);
			amplitude[i] = Convert.ToSingle(p[i].Abs) / 2.0f;
			
			//if (Mathf.Abs(amplitude[i]) > max){
			//	max = Mathf.Abs(amplitude[i]);
			//}
		}
		// Нормализуем
		for (int i=0;i<amplitude.Length;i++){		
			//amplitude[i] /= amplitude.Length;			
		}

		phase = new float[p.Length];
		
		float PI = Mathf.PI;
		float PI2 = PI * 2;
		float degree_coeff = 180.0f / PI;
		
		for (int i=0;i<phase.Length;i++){
			//phase[i] = Convert.ToSingle(Math.Atan2(p[i].Imag,p[i].Real));	
			
			phase[i] = Convert.ToSingle(Math.Atan(p[i].Imag / p[i].Real));	
			
			//преобразуем косинус в синус. M_PI2 = пи/2, M_PI = пи
			//в результате фаза будет в диапазоне от -пи/2 до +пи/2
			phase[i] += PI2;
			if (phase[i] > PI){
				phase[i] -= PI2;
			}		

			// Преобразуем радианы в градусы
			phase[i] = phase[i] * degree_coeff;			
		}
					
		
		// Возьмем половину. Дальше центра - симметричное отображение первой половины
		db = new float[p.Length / 2];
		
		for (int i=0;i<db.Length;i++){
			float tmp = Convert.ToSingle((p[i].Real * p[i].Real + p[i].Imag * p[i].Imag));
			if (tmp != 0){
				db[i] = 10.0f * Mathf.Log(tmp,10);
			} else {
				db[i] = 0.0f;
			}
		}
		
	}
	
	public void SetLocalMax(){
		local_max = array_helper.GetLocalMax(ref amplitude);
	}
	
}
