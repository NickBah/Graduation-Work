using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;


public class array_element
{
	public float value;
	public int frequency;
	
	// Чем больше value по сравнению с другими значениями 0-1
	//public float importance;
}	

public class array_helper {
	
	
		// Сортировка по модулю
        public static void InsertionSort(ref array_element[] a)
       {
			int step = 1;
		   
            for (int i = 1; i < a.Length; i+=step)
            {
                array_element current = a[i];
                float current_abs = Math.Abs(a[i].value);
                int j = i;
                while (j > 0 && current_abs > Math.Abs(a[j - 1].value))
                {
                    a[j] = a[j - 1];
                    j-=step;
                }
                a[j] = current;
				
            }
        }	
	
		// Вернуть определенное кол-во элементов массива
        public static array_element[] GetElements(ref float[] a, int count,  bool need_sort = false)
        {	
			array_element[] tmp = new array_element[a.Length];
		
			for (int i = 0; i < tmp.Length; i++){
              tmp[i] = new array_element();
			  tmp[i].value = a[i];
			  tmp[i].frequency = i; // Для сравнения вычислять частоту необязательно. Важна позиция. i * 44100/sound.window_size;
            }
		
			if (need_sort == true){
				InsertionSort(ref tmp);
			}
			
			int result_count = (tmp.Length < count) ? tmp.Length : count;
			
			array_element[] result = new array_element[result_count];
			
			Array.Copy(tmp, 0, result, 0, result_count);
			
			return result;
        }
		
		// Вернуть математическое ожидание
        public static float GetExpectedValue(ref float[] a)
        {	
			float sum = 0.0f;
			
			for (int i = 0; i < a.Length; i++){
				sum += a[i];
			}
			
			return sum / a.Length;
		}
		
		// Вернуть математическое ожидание
        public static float GetExpectedValue(ref array_element[] a)
        {	
			float sum = 0.0f;
			
			for (int i = 0; i < a.Length; i++){
				sum += a[i].frequency;
			}
			
			return sum / a.Length;
		}
		
		// Вернуть набор локальных максимумов
        public static array_element[] GetLocalMaxAbs(ref float[] a)
        {	
			array_element[] result = new array_element[0];
			int count = 0;
			
			//float max = a[0];
			
			int start = 0;//a.Length / 4;
			int finish = a.Length;
			
			if (Mathf.Abs(a[start]) > Mathf.Abs(a[start + 1])){
				  count += 1;
				  Array.Resize(ref result, count);
				  result[count-1] = new array_element();
				  result[count-1].value = a[start];
				  result[count-1].frequency = 0;				
			}
			
			for (int i = start + 1; i < finish-1; i++){
              
			  //if (a[i] > max){
			  //	max = a[i];
			  //}
			  
			  if (Mathf.Abs(a[i]) > Mathf.Abs(a[i-1]) && Mathf.Abs(a[i]) > Mathf.Abs(a[i+1])){
				  count += 1;
				  Array.Resize(ref result, count);
				  result[count-1] = new array_element();
				  result[count-1].value = a[i];
				  result[count-1].frequency = i;
			  }
			}
	
			if (Mathf.Abs(a[a.Length - 1]) > Mathf.Abs(a[a.Length - 2])){
				  count += 1;
				  Array.Resize(ref result, count);
				  result[count-1] = new array_element();
				  result[count-1].value = a[a.Length - 1];
				  result[count-1].frequency = a.Length - 1;				
			}
			
			//if (a[a.Length - 1] > max){
			//	max = a[a.Length - 1];
			//}
			
			//for (int i=0;i<result.Length;i++){
			//	result[i].importance = result[i].value / max;
			//}
			
			return result;
        }		
		
		// Вернуть набор локальных максимумов
        public static array_element[] GetLocalMax(ref float[] a)
        {	
			array_element[] result = new array_element[0];
			int count = 0;
			
			//float max = a[0];
			
			int start = 0;//a.Length / 4;
			int finish = a.Length;
			
			if (a[start] > a[start + 1]){
				  count += 1;
				  Array.Resize(ref result, count);
				  result[count-1] = new array_element();
				  result[count-1].value = a[start];
				  result[count-1].frequency = 0;				
			}
			
			for (int i = start + 1; i < finish-1; i++){
              
			  //if (a[i] > max){
			  //	max = a[i];
			  //}
			  
			  if (a[i] > a[i-1] && a[i] > a[i+1]){
				  count += 1;
				  Array.Resize(ref result, count);
				  result[count-1] = new array_element();
				  result[count-1].value = a[i];
				  result[count-1].frequency = i;
			  }
			}
	
			if (a[a.Length - 1] > a[a.Length - 2]){
				  count += 1;
				  Array.Resize(ref result, count);
				  result[count-1] = new array_element();
				  result[count-1].value = a[a.Length - 1];
				  result[count-1].frequency = a.Length - 1;				
			}
			
			//if (a[a.Length - 1] > max){
			//	max = a[a.Length - 1];
			//}
			
			//for (int i=0;i<result.Length;i++){
			//	result[i].importance = result[i].value / max;
			//}
			
			return result;
        }
}
