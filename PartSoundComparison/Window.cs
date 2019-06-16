using System;
using UnityEngine;

    public class Window
    {
		// Набор оконных функций для работы с FFT
		/*
		public static double BlackmannHarris(float n, float frameSize)
        {
            return 0.35875 - (0.48829*Math.Cos((2*Math.PI*n)/(frameSize - 1))) +
                   (0.14128*Math.Cos((4*Math.PI*n)/(frameSize - 1))) - (0.01168*Math.Cos((4*Math.PI*n)/(frameSize - 1)));
        }
		*/
		
		public static double Gauss(float n, float frameSize)
        {	
			float A = (frameSize - 1) / 2;
			float Q = 0.3f;
            return Math.Exp((-1/2) * Math.Pow((n - A)/(Q * A),2));
        }
		
		// 20 - b в преобразовании Кайзера
		
		private static float I01 = 1.0f / I0(20.0f);
		
		private static float I0(float x)
        {	
			float Y = x / 2.0f;
			float T = 0.0000000001f;
			float dE = 1.0f;
			float E = dE;
			float SdE;
			for (int i=1;i<=50;i++){
				dE = dE * Y / i;
				SdE = dE * dE;
				E = E + SdE;
				if (E * T - SdE > 0){
					break;
				}
			}	
			return E;
        }			
		
		public static float Kaiser(int n, int length)
        {	
			//double I01 = 1.0 / I0(b);
			float b = 20.0f;
			float k = -(length/2.0f) + n;
						
			return I0(b * Mathf.Sqrt(1.0f-(2.0f * k / (length-1.0f))*(2.0f * k / (length-1.0f)))) * I01;
        }
		
		public static float Hanning(int n, int frameSize){
			return 0.5f * (0.54f - (1 - 0.54f) - Mathf.Cos((2 * Mathf.PI * n)/(frameSize - 1)));
		}
    }
