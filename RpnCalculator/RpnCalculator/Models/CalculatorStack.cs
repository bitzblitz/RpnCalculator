using System;
using System.Collections.Generic;

namespace RpnCalculator
	{
	internal class CalculatorStack:Stack<double>
		{
		internal void Reset()
			{
			Clear();
			}

		public void FromArray(double[] Stack)
			{
			Clear();
			foreach(double ele in Stack)
				Push(ele);
			}
		}
	}