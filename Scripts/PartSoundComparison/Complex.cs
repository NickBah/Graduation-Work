using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


	public struct Complex
		{
			private double m_real;
			private double m_imag;
	 
			#region Конструкторы
	 
			public Complex(double re)
			{
				m_real = re;
				m_imag = 0.0f;
			}
	 
			public Complex(double re, double im)
			{
				m_real = re;
				m_imag = im;
			}
	 
			public Complex(Complex x)
			{
				m_real = x.Real;
				m_imag = x.Imag;
			}
			#endregion
	 
			public double Real
			{
				get { return m_real; }
				set { m_real = value; }
			}
	 
			public double Imag
			{
				get { return m_imag; }
				set { m_imag = value; }
			}
	 
			public double Abs
			{
				get { return Math.Sqrt(m_imag * m_imag + m_real * m_real); }
			}
	 
			public double Arg
			{
				get { return Math.Atan(m_imag / m_real); }
			}
	 
			/// <summary>
			/// Получить комплексно-сопряженное число
			/// </summary>
			public Complex GetConjugate()
			{
				return new Complex(m_real, -m_imag);
			}
	 
			public override string ToString()
			{
				string res = "";
	 
				if (m_real != 0.0f)
				{
					res = m_real.ToString();
				}
	 
				if (m_imag != 0.0f)
				{
					if (m_imag > 0)
					{
						res += "+";
					}
	 
					res += m_imag.ToString() + "i";
				}
	 
				return res;
			}
	 
			#region Перегруженные операторы сложения
			public static Complex operator +(Complex c1, Complex c2)
			{
				return new Complex(c1.Real + c2.Real, c1.Imag + c2.Imag);
			}
	 
			public static Complex operator +(Complex c1, double c2)
			{
				return new Complex(c1.Real + c2, c1.Imag);
			}
	 
			public static Complex operator +(double c1, Complex c2)
			{
				return new Complex(c1 + c2.Real, c2.Imag);
			}
			#endregion
	 
	 
			#region Перегруженные операторы вычитания
			public static Complex operator -(Complex c1, Complex c2)
			{
				return new Complex(c1.Real - c2.Real, c1.Imag - c2.Imag);
			}
	 
			public static Complex operator -(Complex c1, double c2)
			{
				return new Complex(c1.Real - c2, c1.Imag);
			}
	 
			public static Complex operator -(double c1, Complex c2)
			{
				return new Complex(c1 - c2.Real, -c2.Imag);
			}
			#endregion
	 
	 
			#region Перегруженные операторы умножения
			public static Complex operator *(Complex c1, Complex c2)
			{
				return new Complex(c1.Real * c2.Real - c1.Imag * c2.Imag,
					c1.Real * c2.Imag + c1.Imag * c2.Real);
			}
	 
			public static Complex operator *(Complex c1, double c2)
			{
				return new Complex(c1.Real * c2, c1.Imag * c2);
			}
	 
			public static Complex operator *(double c1, Complex c2)
			{
				return new Complex(c1 * c2.Real, c1 * c2.Imag);
			}
			#endregion
	 
	 
			#region Перегруженные операторы деления
			public static Complex operator /(Complex c1, Complex c2)
			{
				double Denominator = c2.Real * c2.Real + c2.Imag * c2.Imag;
				return new Complex((c1.Real * c2.Real + c1.Imag * c2.Imag) / Denominator,
					(c2.Real * c1.Imag - c2.Imag * c1.Real) / Denominator);
			}
	 
			public static Complex operator /(Complex c1, double c2)
			{
				return new Complex(c1.Real / c2, c1.Imag / c2);
			}
	 
			public static Complex operator /(double c1, Complex c2)
			{
				double Denominator = c2.Real * c2.Real + c2.Imag * c2.Imag;
				return new Complex((c1 * c2.Real) / Denominator, (-c2.Imag * c1) / Denominator);
			}
			#endregion
	 
			public static bool operator ==(Complex c1, Complex c2)
			{
				return c1.Real == c2.Real && c1.Imag == c2.Imag;
			}
	 
			public static bool operator !=(Complex c1, Complex c2)
			{
				return c1.Real != c2.Real || c1.Imag != c2.Imag;
			}
	 
			public override bool Equals(object obj)
			{
				return this == (Complex)obj;
			}
	 
			public override int GetHashCode()
			{
				return m_real.GetHashCode() + m_imag.GetHashCode();
			}
		
}
