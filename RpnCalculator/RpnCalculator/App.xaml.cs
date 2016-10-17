using RpnCalculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;

using Xamarin.Forms;
using System.IO;
using Xamarin.Forms.Xaml;
using RpnCalculator.PageModels;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace RpnCalculator
	{
public partial class App:Application
		{
		private const string _calcState = "CalculatorState";

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

			MainPage = FreshMvvm.FreshPageModelResolver.ResolvePageModel<CalculatorPageModel>();  // dependency injection and MVVM plumbing.
			}

		protected override void OnStart()
			{
			if(Application.Current.Properties.ContainsKey(_calcState))
				{
				string state = (string)Application.Current.Properties[_calcState];
				GetCalculatorViewModel().SetState(Deserialize<CalculatorState>(state));
				}
			}

		protected override void OnSleep()
			{
			Application.Current.Properties[_calcState] = Serialize(GetCalculatorViewModel().GetState());
			}

		protected override void OnResume()
			{
			// Handle when your app resumes
			}

		public CalculatorPageModel GetCalculatorViewModel()
			{
			return (CalculatorPageModel)MainPage.BindingContext;
			}
		}
	}
