using System.Runtime.Serialization;

namespace RpnCalculator
	{
	[DataContract]
	public class CalculatorState
		{
		[DataMember]
		public string Output { get; set; }
		[DataMember]
		public string Format { get; set; }
		[DataMember]
		public bool IsNewPending { get; set; }
		[DataMember]
		public bool IsFixPending { get; set; }
		[DataMember]
		public double Memory { get; set; }
		[DataMember]
		public double XRegister { get; set; }
		[DataMember]
		public double[] Stack { get; set; }
		}
	}