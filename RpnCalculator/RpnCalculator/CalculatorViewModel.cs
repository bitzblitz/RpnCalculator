using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RpnCalculator
	{
	public class CalculatorViewModel:INotifyPropertyChanged
		{
		public event PropertyChangedEventHandler PropertyChanged;

		private Calculator _calc = new Calculator();
		private CalculatorCommand _calcCommand;

		public CalculatorViewModel()
			{
			_calcCommand = new CalculatorCommand(this);
			}

		public string Output
			{
			get
				{
				return _calc.Output;
				}
			set
				{
				if(PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(nameof(Output)));
				}
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
			get
				{
				return _calc.StackSize;
				}
			}

		internal void Execute(string parameter)
			{
			Output = _calc.Execute(parameter);
			if(PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(nameof(StackSize)));
			}

		public void SetState(CalculatorState State)
			{
			Output = _calc.SetCalculatorState(State);
			if(PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(nameof(StackSize)));
			_calcCommand.UpdateEnable();
			}

		public CalculatorState GetState()
			{
			return _calc.GetCalculatorState();
			}
		}
	}
