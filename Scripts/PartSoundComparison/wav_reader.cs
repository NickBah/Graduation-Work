using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;


// Класс, описывающий информацию, содержащуюся в заголовке звукового файла
[StructLayout(LayoutKind.Sequential)]
public class wav_header {
	
	public UInt32 ChunkId;
		
	public UInt32 ChunkSize;
	
	public UInt32 Format;
		
	public UInt32 Subchunk1Id;
	public UInt32 Subchunk1Size;
	
	public UInt16 AudioFormat;
	
	// Количество каналов
	public UInt16 NumChannels;
	
	// Частота дискретизации
	public UInt32 SampleRate;
	
	// sampleRate * numChannels * bitsPerSample/8
	public UInt32 ByteRate;
	
	// numChannels * bitsPerSample/8
	// Количество байт для одного сэмпла, включая все каналы.
	public UInt16 BlockAlign;	
	
	// Так называемая "глубиная" или точность звучания. 8 бит, 16 бит и т.д.
	public UInt16 BitsPerSample;	
	 
	
	public UInt32 Subchunk2Id;
	
	public UInt32 Subchunk2Size;
	
}

public class wav_reader {
	private static int bits_per_sample = -1;
	private static long durationMSeconds = -1;
	private static long size = -1;
	private static int frequency = -1;
	
	// Кол-во битов, которым кодируется один фрагмент звука
	public static int BitsPerSample(){
		return bits_per_sample;
	}

	// Длительность в милисекундах
	public static long DurationMSeconds(){
		return durationMSeconds;
	}
	
	// Определить - к какому временному интервалу относится данный фрагмент
	public static long GetTimeByPlace(int index, int pSize){
		return Convert.ToInt64((index * durationMSeconds) / pSize);
	}
	
	// Частота дискретизации в файле
	public static int GetFrequency(){
		return frequency;
	}	
	
	// Сейчас работает для Аудио стандарта PCM
	public static float[] ParseWav(byte[] pData, string fileName){
			wav_header header = new wav_header();		
			int header_size = Marshal.SizeOf(header);
			
			// Прочитаем заголовок
			var buffer = new byte[header_size];
		
			Array.Copy(pData,0,buffer,0,header_size);
		
			// Чтобы не считывать каждое значение заголовка по отдельности,
			// воспользуемся выделением unmanaged блока памяти
			var headerPtr = Marshal.AllocHGlobal(header_size);
		
			// Копируем считанные байты из файла в выделенный блок памяти
			Marshal.Copy(buffer, 0, headerPtr, header_size);
		
			// Преобразовываем указатель на блок памяти к нашей структуре
			Marshal.PtrToStructure(headerPtr, header);
		
			/*
			Debug.Log(String.Format("header.Subchunk2Size: {0}",header.Subchunk2Size));
			Debug.Log("Sample rate: " + header.SampleRate.ToString());
			Debug.Log("Channels: " + header.NumChannels.ToString());
			Debug.Log("Bits per sample: " + header.BitsPerSample.ToString());			
			*/
			
			
			bits_per_sample = header.BitsPerSample;
			durationMSeconds = Convert.ToInt64(1.0f * header.Subchunk2Size / (header.BitsPerSample / 8.0f) / header.NumChannels / header.SampleRate * 1000.0f);		
			size = Convert.ToInt64(header.Subchunk2Size);
			frequency = Convert.ToInt32(header.SampleRate);
			
			// Освобождаем unmanaged память, выделенную под заголовок файла
			Marshal.FreeHGlobal( headerPtr );
			
			int step = header.BitsPerSample / 8;
			
			float max = -1.0f;
			if (step == 2){
				max = Convert.ToSingle(Int16.MaxValue);
			}

			if (step != 2 && step != 4){
				Debug.LogError(String.Format("wav_reader.cs Файл [{0}] - неподдерживаемый BitsPerSample [{1}]",fileName,header.BitsPerSample));
			}
			
			//Debug.Log(String.Format("step: {0}",step));
			//Debug.Log(String.Format("max: {0}",max));
			float[] data;

			// Для моно
			if (header.NumChannels == 1){
			
				data = new float[(pData.Length - header_size) / step];		
			
				float tmp = -1.0f;
			
				for (int i = 0; i < data.Length; i++){
					// Если содержится как Int16
					if (step == 2){
						tmp = (BitConverter.ToInt16(pData, header_size + i * step)) / max;
					}
					// Если содержится как float
					if (step == 4){
						tmp = (BitConverter.ToSingle(pData, header_size + i * step));// / max;
					}				
					data[i] = tmp;
				}
				
			} else {
				// Для стерео. Будем сводить всё в моно
				data = new float[(pData.Length - header_size) / step];
				
				float left = -1.0f;
				float right = -1.0f;
				
				for (int i = 0; i < data.Length - 2; i+=2){
					// Если содержится как Int16
					if (step == 2){
						left = (BitConverter.ToInt16(pData, header_size + i * step )) / max;
						right = (BitConverter.ToInt16(pData, header_size + i * step + step)) / max;
					}
					
					// Если содержится как float
					if (step == 4){
						left = BitConverter.ToSingle(pData, header_size + i * step );
						right = BitConverter.ToSingle(pData, header_size + i * step + step);						
					}				
					data[i] = (left + right) / 2;
				}		

				// Сдвинем элементы влево
				for (int i = 1; i < data.Length-2; i+=2){
					data[i] = data[i + 1];
				}
				
			}
		
			return data;
	}

}
