using RpnCalculator.PageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RpnCalculator.PageModels
	{
	public class CalculatorCommand:ICommand
		{
		public event EventHandler CanExecuteChanged;

		private CalculatorPageModel _vm;

		public CalculatorCommand(CalculatorPageModel VM)
			{
			_vm = VM;
			}

		public bool CanExecute(object parameter)
			{
			string p = parameter as string;
			if(!".0123456789 ENTER FIX RCL CLX".Contains(p) && _vm.StackSize == 0)
				return false;
			return true;
			}

		public void Execute(object parameter)
			{
			_vm.Execute((string)parameter);
			UpdateEnable();
			}

		public void UpdateEnable()
			{
			if(CanExecuteChanged != null)
				CanExecuteChanged(this, new EventArgs());
			}
		}
	}
