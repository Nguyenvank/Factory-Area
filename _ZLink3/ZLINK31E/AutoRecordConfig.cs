namespace ZLINK31E
{
	public class AutoRecordConfig
	{
		public bool IsFinishTimeEnable;

		public bool IsTriggerEnable;

		public uint FinishTime;

		public float TriggerValue;

		public float Interval;

		public AutoRecordConfig()
		{
			IsFinishTimeEnable = false;
			IsTriggerEnable = false;
			FinishTime = 10u;
			TriggerValue = 1f;
			Interval = 0.05f;
		}
	}
}
