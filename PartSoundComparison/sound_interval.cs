using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interval_time {
	// Время от начала композиции - начало интервала
	public long start;
	
	// Конец описываемого интервала
	public long end;		
}

public class sound_interval {

	// Временной промежуток, на котором расположен фрагмент
	public interval_time time = new interval_time();
	
	// Содержит информацию о спектре окна
	public Spectrum spectrum = new Spectrum();
	 
	public void SetTime(long start, long end){
		time.start = start;
		time.end = end;
	}
	
	//////////////////////////////////////////////////////////
	
	private static int AmplitudeFrequencyOffset = 10; // 10
	private static float AmplitudeRatioDeviation = 10.0f; // 10.0f;
	
	private static int PhaseFrequencyOffset = 2; // 15;
	private static float PhaseRatioDeviation = 1000.0f; // 1000.0f;	
	
	private static float Compare(ref array_element[] a_peaks, ref array_element[] b_peaks, int frequency_offset, float ratio_deviation,float middle_a, float middle_b){

		// Нулевые интервалы не должны участвовать в формировании нашего мнения
		float a_average = 0.0f;
		float b_average = 0.0f;
		float sum = 0.0f;
		int count = 0;
		for (int i=0;i<a_peaks.Length;i++){		
			if (a_peaks[i].value != 0){
				sum += a_peaks[i].value;
				count += 1;
			}
		}
		if (count != 0){
			a_average = sum / count;
		}
		
		sum = 0.0f;
		count = 0;
		
		for (int i=0;i<b_peaks.Length;i++){		
			if (b_peaks[i].value != 0){
				sum += b_peaks[i].value;
				count += 1;
			}
		}	
		
		if (count != 0){
			b_average = sum / count;
		}
		
		if (b_average == 0 && a_average == 0){
			return -1.0f;
		}		

		if (b_average == 0 || a_average == 0){
			return 0.0f;
		}
			
		int less_length = (a_peaks.Length > b_peaks.Length) ? b_peaks.Length : a_peaks.Length;

		int match = 0;
		/*
		float m;
		if (a_peaks.Length > b_peaks.Length){
			m = a_peaks.Length / b_peaks.Length * 0.25f;
		} else {
			m = b_peaks.Length / a_peaks.Length * 0.25f;
		}
		*/
		int a_freq, a_index;
		int b_freq, b_index;
		
		int b_target_index = -1;
		
		int offset;
		int min_offset_freq;
		int min_offset_index;
		
		float denominator = (middle_b == 0) ? 1 : middle_b;
		
		float start_ratio = Mathf.Abs(middle_a / denominator);
		float ratio;

		for (int i=0;i<a_peaks.Length;i++){
			a_freq = a_peaks[i].frequency;
			a_index = i;
			offset = 0;
			min_offset_freq = Int32.MaxValue;
			min_offset_index = Int32.MaxValue;
			
			for (int j=0;j<b_peaks.Length;j++){
				b_freq = b_peaks[j].frequency;
				b_index = j;
				
				offset = Math.Abs(a_freq - b_freq);
				
				if (offset < min_offset_freq){
					min_offset_freq = offset;
					min_offset_index = Math.Abs(a_index - b_index);
					b_target_index = b_index;
				}
			}
			
			if (b_peaks[b_target_index].value == 0 && Mathf.Abs(a_peaks[a_index].value) > 1){
				continue;
			} else {
				
				denominator = (b_peaks[b_target_index].value == 0) ? 1 : b_peaks[b_target_index].value;

				if (min_offset_index <= frequency_offset && Math.Abs((a_peaks[a_index].value / denominator) - start_ratio) <= ratio_deviation){
					match += 1;
				}

			}			
			
		}

		return Convert.ToSingle(match) / Convert.ToSingle(a_peaks.Length) * 100;		
	}
	
	private static float ComparePhases(ref float[] a, ref float[] b){
		array_element[] a_peaks = array_helper.GetLocalMaxAbs(ref a);
		array_element[] b_peaks = array_helper.GetLocalMaxAbs(ref b);
		
		float a_middle = a[a.Length / 2];
		float b_middle = b[b.Length / 2];
		
		return Compare(ref a_peaks, ref b_peaks, PhaseFrequencyOffset, PhaseRatioDeviation,a_middle,b_middle);
	}
	
	private static float CompareAmplitudes(ref float[] a, ref float[] b){
		array_element[] a_peaks = array_helper.GetLocalMax(ref a);
		array_element[] b_peaks = array_helper.GetLocalMax(ref b);		
		
		float a_middle = a[a.Length / 2];
		float b_middle = b[b.Length / 2];
				
		return Compare(ref a_peaks, ref b_peaks, AmplitudeFrequencyOffset, AmplitudeRatioDeviation,a_middle,b_middle);
	}
	
	// Сравнить два звуковых интервала 
	public static float CompareIntervalsFirstTime(sound_interval a, sound_interval b, string pathForCache){

		float ampl_match = CompareAmplitudes(ref a.spectrum.amplitude, ref b.spectrum.amplitude);
		float phases_match = ComparePhases(ref a.spectrum.phase, ref b.spectrum.phase);
		
		UnityEngine.Debug.Log(String.Format("Совпадение интервалов по амплитуде: {0}",ampl_match));
		UnityEngine.Debug.Log(String.Format("Совпадение интервалов по фазе: {0}",phases_match));

		return (phases_match + ampl_match) / 2;

	}
	
	// Сравнить два звуковых интервала - один из них уже представлен ранее сохраненными пиками
	public static float CompareIntervals(int[] a_frequency, sound_interval b){
		array_element[] b_peaks = b.spectrum.local_max;		
		
		// Нулевые интервалы не должны участвовать в формировании нашего мнения
		float b_average = 0.0f;
		float sum = 0.0f;
		int count = 0;
		
		if (a_frequency.Length == 0){
			return 0.0f;
		}
		
		for (int i=0;i<b_peaks.Length;i++){		
			if (b_peaks[i].value != 0){
				sum += b_peaks[i].value;
				count += 1;
			}
		}	
		
		if (count != 0){
			b_average = sum / count;
		}
		
		if (b_average == 0){
			return -1.0f;
		}	
			
		int less_length = (a_frequency.Length > b_peaks.Length) ? b_peaks.Length : a_frequency.Length;

		//FileManager.WriteFile(Application.streamingAssetsPath + "/interval_comparison/" + b.time.start.ToString() + "_b_val.txt",Application.streamingAssetsPath + "/interval_comparison/" + b.time.start.ToString() + "_b_fre.txt",b_peaks);	
				

		int match = 0;

		float m;
		if (a_frequency.Length > b_peaks.Length){
			m = a_frequency.Length / b_peaks.Length * 0.05f;
		} else {
			m = b_peaks.Length / a_frequency.Length * 0.05f;
		}
		
		float max_frequency_offset = 10;

		float frequency_offset;
		
		//int[] used_indexes = new int[0];

		for (int i=0;i<less_length;i++){
		  frequency_offset = Math.Abs(a_frequency[i] - b_peaks[i].frequency);
		  //UnityEngine.Debug.Log(String.Format("Смещение частоты: {0}",frequency_offset));
		  if (frequency_offset < max_frequency_offset + m * i){

			  //Array.Resize(ref used_indexes, used_indexes.Length + 1);
			  //used_indexes[used_indexes.Length - 1] = i;
			  
			  match += 1;
		  }
	 
		}

		

		//UnityEngine.Debug.Log(String.Format("Совпадение: {0}",(Convert.ToSingle(match) / Convert.ToSingle(less_length)) * 100));
		
		return Convert.ToSingle(match) / Convert.ToSingle(less_length) * 100;
	}
	
	static bool IfIndexUsed(ref int[] indexes, int target){
		return Array.IndexOf(indexes,target) == -1;
	}	
}

