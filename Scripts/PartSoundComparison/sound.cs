using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class sound {
	// Ширина скользящего окна. Чем меньше, тем эффективнее трудятся маленькие алгоритмы. Степень 2
	public static int window_size = 8192; // 1024
	
	public static int cover = window_size / 2;
	
	// Сопадением считается это кол-во процентов от сравнения интервалов
	public static int success_match = 60;	
	
	// Частота дискретизации записи
	public static int record_freq = 44100;
		
	public sound_interval[] intervals = new sound_interval[0];

	public string name = System.String.Empty;	
	public string path = System.String.Empty;
	public string type = System.String.Empty;
	
	// Частота дискретизации данного звука
	public int SamplingFrequency = -1;
	
	/////////////////////////////////////////////////////////////////
	
	// Возвращает степень совпадения звуков в процентах
	// Отрабатывает один раз, создает .txt файл с признаками
	// В следующий раз сравнение идет по нему
	public static float CompareFirstTime(sound a, sound b, string pathForCache){
		//Debug.Log(String.Format("[sound.cs][Compare] Сравниваем [{2}][{0}] и [{3}][{1}]", a.name, b.name, a.type, b.type));

		//Debug.Log(String.Format("[{0}][{1}]",a.intervals.Length,b.intervals.Length));	

		int count = (a.intervals.Length > b.intervals.Length) ? b.intervals.Length : a.intervals.Length;
	
		// Если образец и запись слишком сильно отличаются по длине
		if (b.intervals.Length / a.intervals.Length < 0.7f){
			//Debug.Log(String.Format("Совпадение звуков [{0}]%. Образец и запись слишком сильно отличаются по длине",0.0f));
			return 0.0f;
		}
			
		Debug.Log(String.Format("[sound.cs][Compare] Кол-во интервалов [{0}]",count));	
			
		//Debug.Log(String.Format("[sound.cs][Compare] Длина одного интервала [{0}]",a.intervals[0].spectrum.amplitude.Length));	
			
		float match = 0.0f;
		int first_interval_index = count;
			
		int wrong_intervals_count = 0;
		int right_intervals_count = 0;
		int zero_intervals_count = 0;
		
		sound_interval a_first = a.intervals[0];		
		
		// Найдем интервал во втором звуке, который больше всего похож на первый интервал первого звука
		for (int i=0;i<count;i++){
			match = sound_interval.CompareIntervalsFirstTime(a_first,b.intervals[i],pathForCache);
					
			if (match == 100){
				right_intervals_count += 1;
			}
			
			if (match == -1){
				zero_intervals_count += 1;
			}
			
			if (match >= success_match){
				first_interval_index = i;
				break;
			}
			
			wrong_intervals_count += 1;
			
			/*
			// Если прошло достаточное кол-во несовпадающих интервалов, то дальше не проверяем
			if (i >= count/4){
				//Debug.Log(String.Format("Совпадение звуков [{0}]%. Слишком много интервалов просмотрено в поисках первого совпадения",0.0f));
				return 0.0f;
			}
			*/
		}
		
		float sum = match;
		int relevant_count = 1;
		
		for (int i=first_interval_index+1;i<count;i++){
			match = sound_interval.CompareIntervalsFirstTime(a.intervals[i],b.intervals[i],pathForCache);
			if (match != 0 && match != -1){
				sum += match;
				relevant_count += 1;
			}
			
			if (match == 100){
				right_intervals_count += 1;
			}

			if (match == -1){
				zero_intervals_count += 1;
			}			
			
			if (match < success_match){
				wrong_intervals_count += 1;
			}
		}
		/*
		Debug.Log(String.Format("Кол-во заведомо неверных интервалов [{0}]",wrong_intervals_count));
		Debug.Log(String.Format("Кол-во полностью совпадающих интервалов [{0}]",right_intervals_count));
		Debug.Log(String.Format("Кол-во нулевых интервалов [{0}]",zero_intervals_count));
		*/
		//Debug.Log(String.Format("Совпадение звуков [{0}]%",sum / relevant_count ));
		
		return sum / relevant_count;
	}	
	
	// Возвращает степень совпадения звуков в процентах. Берет данные для сравнения из предварительно созданного текстового файла
	public static float CompareCache(string fileName, sound record){
		string[] loaded_intervals = read_file.loadFileContent(fileName).Split(new string[]{"###"},StringSplitOptions.RemoveEmptyEntries);
		
		int count = (loaded_intervals.Length > record.intervals.Length) ? record.intervals.Length : loaded_intervals.Length;
		
		//Debug.Log(String.Format("record.intervals.Length [{0}]",record.intervals.Length));
		//Debug.Log(String.Format("loaded_intervals.Length [{0}]",loaded_intervals.Length));
		
		// Если образец и запись слишком сильно отличаются по длине
		if (record.intervals.Length / loaded_intervals.Length < 0.7f){
			//Debug.Log(String.Format("Совпадение звуков [{0}]%. Образец и запись слишком сильно отличаются по длине",0.0f));
			return 0.0f;
		}		
		
		// Распарсим сохраненные данные
		int[][] data = new int[count][];
		
		for (int i=0;i<count;i++){
			string[] s = loaded_intervals[i].Split(new string[]{"\n"},StringSplitOptions.RemoveEmptyEntries);
			
			//Debug.Log(String.Format("Размер массива строк [{0}] = [{1}]",i,s.Length));
			
			data[i] = new int[s.Length];
			for (int j=0;j<data[i].Length;j++){
				data[i][j] = Convert.ToInt32(s[j]);
			}
		}	
	
		float match = 0.0f;
		int first_interval_index = count;
			
		int wrong_intervals_count = 0;
		int right_intervals_count = 0;
		int zero_intervals_count = 0;
		
		// Найдем интервал в записи, который больше всего похож на первый интервал образца
		for (int i=0;i<count;i++){
			match = sound_interval.CompareIntervals(data[0],record.intervals[i]);
					
			if (match == 100){
				right_intervals_count += 1;
			}
			
			if (match == -1){
				zero_intervals_count += 1;
			}
			
			if (match >= success_match){
				first_interval_index = i;
				break;
			}
			
			wrong_intervals_count += 1;
			
			// Если прошло достаточное кол-во несовпадающих интервалов, то дальше не проверяем
			if (i >= count/4){
				//Debug.Log(String.Format("Совпадение звуков [{0}]%. Слишком много интервалов просмотрено в поисках первого совпадения",0.0f));
				return 0.0f;
			}		
		}
		
		float sum = match;
		int relevant_count = 1;
		
		for (int i=first_interval_index+1;i<count;i++){
			match = sound_interval.CompareIntervals(data[i],record.intervals[i]);
			if (match != 0 && match != -1){
				sum += match;
				relevant_count += 1;
			}
			
			if (match == 100){
				right_intervals_count += 1;
			}

			if (match == -1){
				zero_intervals_count += 1;
			}			
			
			if (match < success_match){
				wrong_intervals_count += 1;
			}
		}
		/*
		Debug.Log(String.Format("Кол-во заведомо неверных интервалов [{0}]",wrong_intervals_count));
		Debug.Log(String.Format("Кол-во полностью совпадающих интервалов [{0}]",right_intervals_count));
		Debug.Log(String.Format("Кол-во нулевых интервалов [{0}]",zero_intervals_count));
		*/
		//Debug.Log(String.Format("Кэш. Совпадение звуков [{0}]%",sum / relevant_count ));
		
		
		return sum / relevant_count;
	}
	
	// Шумоподавление. Используется до преобразования Фурье
	public void Filter(ref float[] a){
		float coeff = Convert.ToSingle((2 * Math.PI) / 180); 
		for (int i=1;i<a.Length;i++){
			a[i] = (a[i] - 0.9f * a[i-1]) * (0.54f - 0.46f * Mathf.Cos((i-6) * coeff));
		}
	}
	
	// Выборка данных по АЧХ - только более-менее сильный сигнал
	public void RelevantData(ref float[] a, float coeff = 0.1f){
		// Ширина выборки
		int width = 500;
			
		float[] values = new float[Convert.ToInt32(a.Length / width) - 1];
		
		float sum = 0.0f;			
		float average = 0.0f;
		int first_relevant_index = -1;
		int last_relevant_index = -1;		
		
		int relevant_count = 0;
		
		for (int i = 0; i < values.Length; i++){
				values[i] = a[i*width];
				if (values[i] != 0.0f){
					sum += Math.Abs(values[i]);
					relevant_count += 1;
				}
        }
		average = sum / relevant_count;
			
		float edge = Math.Abs(average) * coeff;		
		
		for (int i = 0; i < values.Length; i++){
			// Если больше порогового значения и не окружен нулями
			if (Math.Abs(values[i]) > edge){
				
				int zero_check_area = 10;
				int zero_check_start = (i - zero_check_area < 0) ? 0 : i - zero_check_area;
				int zero_check_end = (i + zero_check_area > values.Length-1) ? values.Length-1 : i + zero_check_area;
				int zero_count = 0;
				
				for (int j = zero_check_start; j < zero_check_end; j++){
					if (values[j] == 0){
						zero_count += 1;
					}
				}
				
				if (Math.Abs(zero_check_area * 2 - zero_count) > zero_check_area){
				
					if (first_relevant_index == -1){
						first_relevant_index = i * width;
					}
					
				}
				
				last_relevant_index = i * width;
			}
        }	
		
		// На основании выборки и ее среднего значения
		
		int start = (first_relevant_index == -1) ? 0 : first_relevant_index;
		int end = (last_relevant_index == -1) ? a.Length : last_relevant_index;
			
        for (int i = start; i < end; i++){
			a[i - start] = a[i];
        }                
			
		Array.Resize(ref a, end - start);		
	}
	
	public void EvaluateIntervals(string test_name = ""){
			byte[] file_info = read_file.loadFileContentBytes(path + "/" + name);
			
			float[] file_info_float = wav_reader.ParseWav(file_info,path + name);
			
			RelevantData(ref file_info_float);
			string p = "G:\\Program Files\\Unity\\Projects\\fqw_new_start\\comparison_intervals\\" + test_name + "\\";
			
			//FileManager.WriteFile(p + "/ачх/Образец до фильтра.txt",file_info_float);
			
			Filter(ref file_info_float);
			
			
			
			//FileManager.WriteFile(Application.streamingAssetsPath + "/out/Образец после фильтра.txt",file_info_float);
			
			// Для разбиения на интервалы, в будущем, будет удобно чтобы массив являлся степеню 2
			Array.Resize(ref file_info_float, common.getThePowerOf2(file_info_float.Length));		
				
			//int count = file_info_float.Length / window_size * cover + 1;
			
			
			//intervals = new sound_interval[count];
			
			SamplingFrequency = wav_reader.GetFrequency();

			
			/*
			Complex[] full_data_array = new Complex[file_info_float.Length];
			for(int j = 0; j < full_data_array.Length; j++) {
				full_data_array[j] = new Complex(file_info_float[j],0);
			}
			
			Complex[] full_fft_result = FFT.fft(full_data_array);
			
			float[] full_ampl = new float[full_fft_result.Length / 2];
			for(int j = 0; j < full_ampl.Length; j++) {
				full_ampl[j] = Convert.ToSingle(full_fft_result[j].Abs) / 2.0f;
			}

			FileManager.WriteFile(p + "/амплитуда всего образца/result without anything.txt",full_ampl);	
			
			for(int j = 1; j < full_ampl.Length; j++) {
				// К каждому отсчету применяем функцию Кайзера
				full_ampl[j] *= Window.Kaiser(j, full_ampl.Length);		
			}
			
			FileManager.WriteFile(p + "/амплитуда всего образца/result kaiser after fft.txt",full_ampl);
			
			
			// С оконной функцией до fft
			for(int j = 0; j < full_data_array.Length; j++) {
				double counting = file_info_float[j] * Window.Gauss(j, full_data_array.Length);
				full_data_array[j] = new Complex(file_info_float[j],0);
			}
			
			full_fft_result = FFT.fft(full_data_array);
			
			for(int j = 0; j < full_ampl.Length; j++) {
				full_ampl[j] = Convert.ToSingle(full_fft_result[j].Abs) / 2.0f;;
			}

			FileManager.WriteFile(p + "/амплитуда всего образца/result gauss window before fft.txt",full_ampl);			

			for(int j = 0; j < full_ampl.Length; j++) {
				full_ampl[j] = Convert.ToSingle(full_fft_result[j].Abs);
			}

			//snd_intrvl.spectrum.amplitude[0] = 0.0f;
			for(int j = 1; j < full_ampl.Length; j++) {
				// К каждому отсчету применяем функцию Кайзера
				full_ampl[j] *= Window.Kaiser(j, full_ampl.Length);		
			}
			
			FileManager.WriteFile(p + "/амплитуда всего образца/result gauss window before fft + kaiser after.txt",full_ampl);
			*/
			
			// Разделим запись на фрагменты (окна) и применим к ним оконную функцию
		
			bool stop = false;
			
			int i = 0;
			
			// Разделим запись на фрагменты (окна)
			//for(int i= 0;i < count; i++) {
			while (stop == false){
				
				int start_interval_data_index = i * window_size;
				int end_interval_data_index = i * window_size + window_size;
				
				sound_interval snd_intrvl = new sound_interval();
				
				Complex[] data_array = new Complex[window_size];
				for(int j = 0; j < window_size; j++) {
					int index;
					if (i == 0){
					  index = i * window_size + j;
					} else {
					  index = (i * window_size + j - cover < 0) ? i * window_size + j : i * window_size + j - cover;
					}	

					if (index == file_info_float.Length-1){
					  stop = true;
					}
					
					if (index > file_info_float.Length-1){
					  stop = true;
					  break;
					}  						
					
					// К каждому отсчету применяем оконную функцию
					double counting = file_info_float[index];// * Window.Gauss(j, window_size);
					data_array[j] = new Complex(counting,0.0);
				}
				Complex[] fft_result_complex = FFT.fft(data_array);
								
				snd_intrvl.spectrum.SetSpectrumData(fft_result_complex, SamplingFrequency);
	
				//test
				//float[] test = new float[snd_intrvl.spectrum.amplitude];
				//for(int j = 0; j < test.Length; j++) { 
				//	test[j] = fft_result_complex[j].Real;
				//}
				
				
				
				//FileManager.WriteFile(p + "ampl_before_kaiser/" +  i.ToString() + "_example_value.txt",snd_intrvl.spectrum.amplitude);	
				
				//FileManager.WriteFile(p + "phase/" +  i.ToString() + "_example_value.txt",snd_intrvl.spectrum.phase);		
							
				snd_intrvl.spectrum.amplitude[0] = 0.0f;
				for(int j = 1; j < snd_intrvl.spectrum.amplitude.Length; j++) {
					// К каждому отсчету применяем функцию Кайзера
					snd_intrvl.spectrum.amplitude[j] *= Window.Kaiser(j, snd_intrvl.spectrum.amplitude.Length);		
				}
				
				for(int j = 0; j < snd_intrvl.spectrum.amplitude.Length; j++) {
				// К каждому отсчету применяем окно Хэннинга
					snd_intrvl.spectrum.amplitude[j] *= Window.Hanning(j, snd_intrvl.spectrum.amplitude.Length);		
				}	
				
				FileManager.WriteFile(p + "/ampl_after_kaiser/" + i.ToString() + "_example.txt",snd_intrvl.spectrum.amplitude);
				FileManager.WriteFile(p + "/phase/" + i.ToString() + "_example.txt",snd_intrvl.spectrum.phase);
				
				snd_intrvl.spectrum.SetLocalMax();
	
				//FileManager.WriteFile(Application.streamingAssetsPath + "/interval_comparison/" + i.ToString() + "_example_value.txt",Application.streamingAssetsPath + "/interval_comparison/" + i.ToString() + "_example_frequency.txt",snd_intrvl.spectrum.local_max);	
	
				//FileManager.WriteFile(p + "local_max/" + i.ToString() + "_example_value.txt",p + "local_max/" + i.ToString() + "_example_frequency.txt",snd_intrvl.spectrum.local_max);
			
				//FileManager.WriteFile(p + i.ToString() + "_example_value.txt",p + i.ToString() + "_example_frequency.txt",snd_intrvl.spectrum.local_max);	
			
	
				snd_intrvl.SetTime(wav_reader.GetTimeByPlace(start_interval_data_index,file_info_float.Length), wav_reader.GetTimeByPlace(end_interval_data_index,file_info_float.Length));
				
				Array.Resize(ref intervals, i+1);	
				
				intervals[i] = snd_intrvl;	
				
				i += 1;
			}
	}
	
	
	public void EvaluateIntervalsRecord(ref float[] data){
		
		SamplingFrequency = record_freq;
		
		RelevantData(ref data,0.0000001f);
		
		//FileManager.WriteFile(Application.streamingAssetsPath + "/out/Запись до фильтра.txt",data);
		
		Filter(ref data);
		
		//FileManager.WriteFile(Application.streamingAssetsPath + "/out/Запись после фильтра.txt",data);
		
		// Для разбиения на интервалы, в будущем, будет удобно чтобы массив являлся степеню 2
		Array.Resize(ref data, common.getThePowerOf2(data.Length));

		//int count = data.Length / window_size * cover + 1;

		intervals = new sound_interval[0];
		
		bool stop = false;
		
		int i = 0;
		
		// Разделим запись на фрагменты (окна)
		//for(int i= 0;i < count; i++) {
		while (stop == false){
				
			int start_interval_data_index = i * window_size;
			int end_interval_data_index = i * window_size + window_size;
				
			sound_interval snd_intrvl = new sound_interval();
				
			Complex[] data_array = new Complex[window_size];
			for(int j = 0; j < window_size; j++) {
				
				int index;
				if (i == 0){
				  index = i * window_size + j;
				} else {
				  index = (i * window_size + j - cover < 0) ? i * window_size + j : i * window_size + j - cover;
				}	

				if (index == data.Length-1){
				  stop = true;
				}
				
				if (index > data.Length-1){
				  stop = true;
				  break;
				}  				
				
				// К каждому отсчету применяем оконную функцию
				double counting = data[index] * Window.Gauss(j, window_size);					
				data_array[j] = new Complex(counting,0.0f);
			}
				
			Complex[] fft_result_complex = FFT.fft(data_array);
			
			snd_intrvl.spectrum.SetSpectrumData(fft_result_complex, SamplingFrequency);
			snd_intrvl.SetTime(wav_reader.GetTimeByPlace(start_interval_data_index,data.Length), wav_reader.GetTimeByPlace(end_interval_data_index,data.Length));
			
			string p = "G:\\Program Files\\Unity\\Projects\\fqw_new_start\\comparison_intervals\\";
			
			//FileManager.WriteFile(p + "phase/" +  i.ToString() + "_record_value.txt",snd_intrvl.spectrum.phase);
			
			//FileManager.WriteFile(p + "/ampl_before_kaiser/" +  i.ToString() + "_record_value.txt",snd_intrvl.spectrum.amplitude);	
			
			
			snd_intrvl.spectrum.amplitude[0] = 0.0f;
			for(int j = 1; j < snd_intrvl.spectrum.amplitude.Length; j++) {
				// К каждому отсчету применяем функцию Кайзера
				snd_intrvl.spectrum.amplitude[j] *= Window.Kaiser(j, snd_intrvl.spectrum.amplitude.Length);		
			}

			for(int j = 0; j < snd_intrvl.spectrum.amplitude.Length; j++) {
			// К каждому отсчету применяем окно Хэннинга
				snd_intrvl.spectrum.amplitude[j] *= Window.Hanning(j, snd_intrvl.spectrum.amplitude.Length);		
			}			
			
			FileManager.WriteFile(p + "/ampl_after_kaiser/" + i.ToString() + "_record.txt",snd_intrvl.spectrum.amplitude);						
			FileManager.WriteFile(p + "/phase/" + i.ToString() + "_record.txt",snd_intrvl.spectrum.phase);
			
			snd_intrvl.spectrum.SetLocalMax();
			
			
			
			//FileManager.WriteFile(p + "local_max/" + i.ToString() + "_record_value.txt",p + "local_max/" + i.ToString() + "_record_frequency.txt",snd_intrvl.spectrum.local_max);	
		
			Array.Resize(ref intervals, i+1);	
			
			intervals[i] = snd_intrvl;
			
			i += 1;
		}		
	}
}

