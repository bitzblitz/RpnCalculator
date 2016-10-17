using System;
using System.Collections.Generic;
using System.Text;

namespace RpnCalculator
	{
		class Calculator
		{
		private const int _max = 12;
		private const double _small = 26;
		private const double _large = 40;
		private const string _exp = "0.000000e+00";

		private bool _fix = false;
		private bool _new = true;
		private double _memory = 0.0;
		private double _xreg = 0.0;
		private string _format = "0.00";
		private string _output = "0.00";

		private CalculatorStack _stack = new CalculatorStack();

		public string Output
			{
			get { return _output; }
			}

		public int StackSize
			{
			get
				{
				return _stack.Count;
				}
			}

		public string Execute(string command)
			{
			switch(command)
				{
			case "0":
			case "1":
			case "2":
			case "3":
			case "4":
			case "5":
			case "6":
			case "7":
			case "8":
			case "9":
				if(_fix)
					{
					// If the user pressed the "FIX" key, then this keypress represents
					// the desired number of decimal places. Update the _format string and
					// refresh the display.
					int digits = Convert.ToInt32(command);
					StringBuilder sb = new StringBuilder("0.");

					for(int i = 0;i < digits;i++)
						sb.Append('0');

					_format = sb.ToString();
					_output = StringifyXRegister();
					}
				else
					{
					if(_new)
						{
						// If this is the first character in a new entry, push the current
						// value of the X register onto the stack and clear the display
						_new = false;
						_stack.Push(_xreg);
						_output = String.Empty;
						}

					// Add the new character to the display and update the X register
					if(_output.Length < _max)
						{
						_output += command;
						_xreg = Convert.ToDouble(_output);
						}
					}
				break;

			case "EXP":
				_xreg = Math.Pow(_stack.Pop(), _xreg);
				_output = StringifyXRegister();
				break;

			case "STO":
				_memory = _xreg;
				_output = StringifyXRegister();
				break;

			case "RCL":
				_stack.Push(_xreg);
				_xreg = _memory;
				_output = StringifyXRegister();
				break;

			case "ENTER":
				_output = StringifyXRegister();
				break;

			case "FIX":
				_fix = !_fix;
				break;

			case "CLX":
				_xreg = 0.0;
				_output = StringifyXRegister();
				_stack.Reset();
				break;

			case "DEL":
				if(_output.Length > 0 && !_new)
					{
					_output = _output.Substring(0, _output.Length - 1);
					_xreg = _output.Length == 0 ? 0.0 : Convert.ToDouble(_output);
					}
				break;

			case "±":
				_xreg = -_xreg;
				_output = StringifyXRegister();
				break;

			case "+":
				_xreg = _stack.Pop() + _xreg;
				_output = StringifyXRegister();
				break;

			case "-":
				_xreg = _stack.Pop() - _xreg;
				_output = StringifyXRegister();
				break;

			case "x":
				_xreg = _stack.Pop() * _xreg;
				_output = StringifyXRegister();
				break;

			case "÷":
				_xreg = _stack.Pop() / _xreg;
				_output = StringifyXRegister();
				break;

			case ".":
				if(_new)
					{
					_new = false;
					_stack.Push(_xreg);
					_output = ".";
					_xreg = 0.0;
					}
				else if(_output.Length < _max && !_output.Contains("."))
					{
					_output += '.';
					_xreg = Convert.ToDouble(_output);
					}
				break;
				}
			return _output;
			}

		private string StringifyXRegister()
			{
			string output = _xreg.ToString(_format);

			// Switch to exponential notation if number is too large
			if(output.Length > _max)
				output = _xreg.ToString(_exp);

			// Switch to exponential notation if number is too small
			if(_xreg != 0.0)
				{
				bool nonzero = false;

				foreach(char ch in output)
					{
					if(ch >= '1' && ch <= '9')
						{
						nonzero = true;
						break;
						}
					}

				if(!nonzero)
					output = _xreg.ToString(_exp);
				}

			// Reset internal state
			_fix = false;
			_new = true;

			// Return the result
			return output;
			}

		public CalculatorState GetCalculatorState()
			{
			return new CalculatorState() {
				Output = _output,
				Format = _format,
				IsNewPending = _new,
				IsFixPending = _fix,
				Memory = _memory,
				XRegister = _xreg,
				Stack = _stack.ToArray()
				};
			}

		public string SetCalculatorState(CalculatorState state)
			{
			_output = state.Output;
			_format = state.Format;
			_new = state.IsNewPending;
			_fix = state.IsFixPending;
			_memory = state.Memory;
			_xreg = state.XRegister;
			_stack.FromArray(state.Stack);
			return _output;
			}
		}
	}