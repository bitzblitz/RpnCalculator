using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RpnCalculator
	{
	[ImplementPropertyChanged]
	public class CalculatorViewModel
		{
		private Calculator _calc = new Calculator();
		private CalculatorCommand _calcCommand;

		public CalculatorViewModel()
			{
			_calcCommand = new CalculatorCommand(this);
			}

		public string Output
			{
			get;
			set;
			}

		public ICommand CalculatorCommand
			{
			get
				{
				return _calcCommand;
				}
			}

		public int StackSize
			{
			get;
			protected set;
			}

		internal void Execute(string parameter)
			{
			Output = _calc.Execute(parameter);
			StackSize=_calc.StackSize;
			}

		public void SetState(CalculatorState State)
			{
			Output = _calc.SetCalculatorState(State);
			StackSize = _calc.StackSize;
			_calcCommand.UpdateEnable();
			}

		public CalculatorState GetState()
			{
			return _calc.GetCalculatorState();
			}
		}
	}
