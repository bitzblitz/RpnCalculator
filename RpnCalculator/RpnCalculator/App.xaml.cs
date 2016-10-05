using RpnCalculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;

using Xamarin.Forms;
using System.IO;

namespace RpnCalculator
	{
	public partial class App:Application
		{
		private const string _calcState = "CalculatorState";
		private CalculatorViewModel _cvm = new CalculatorViewModel();

		private string Serialize<T>(T Obj)
			{
			DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
			using(MemoryStream ms = new MemoryStream())
				{
				serializer.WriteObject(ms, Obj);
				return Encoding.UTF8.GetString(ms.ToArray(), 0, (int)ms.Length);
				}
			}

		private T Deserialize<T>(string SObj)
			{
			DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
			using(MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(SObj)))
				{
				return (T)serializer.ReadObject(ms);
				}
			}
		public App()
			{
			InitializeComponent();

			MainPage = new RpnCalculator.MainPage();
			}

		protected override void OnStart()
			{
			if(Application.Current.Properties.ContainsKey(_calcState))
				{
				string state = (string)Application.Current.Properties[_calcState];
				_cvm.SetState(Deserialize<CalculatorState>(state));
				}
			}

		protected override void OnSleep()
			{
			Application.Current.Properties[_calcState] = Serialize(_cvm.GetState());
			}

		protected override void OnResume()
			{
			// Handle when your app resumes
			}

		public CalculatorViewModel GetCalculatorViewModel()
			{
			return _cvm;
			}
		}
	}
