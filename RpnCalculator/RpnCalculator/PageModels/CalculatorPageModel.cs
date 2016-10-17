using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RpnCalculator.PageModels
	{
	public class CalculatorPageModel:FreshMvvm.FreshBasePageModel
		{
		private Calculator _calc = new Calculator();
		private CalculatorCommand _calcCommand;

		public CalculatorPageModel()
			{
			_calcCommand = new CalculatorCommand(this);
			}

		public override void Init(object initData)
			{
			base.Init(initData);
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
