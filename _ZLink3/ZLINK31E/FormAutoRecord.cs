using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ZLINK31E
{
	public class FormAutoRecord : Form
	{
		private IContainer components;

		[AccessedThroughProperty("ButtonCancel")]
		private Button _ButtonCancel;

		[AccessedThroughProperty("ButtonOK")]
		private Button _ButtonOK;

		[AccessedThroughProperty("CheckBoxTrigger")]
		private CheckBox _CheckBoxTrigger;

		[AccessedThroughProperty("LabelTrigger")]
		private Label _LabelTrigger;

		[AccessedThroughProperty("TextBoxTrigger")]
		private TextBox _TextBoxTrigger;

		[AccessedThroughProperty("TextBoxInterval")]
		private TextBox _TextBoxInterval;

		[AccessedThroughProperty("Label1")]
		private Label _Label1;

		[AccessedThroughProperty("Label3")]
		private Label _Label3;

		[AccessedThroughProperty("CheckBoxFinishTime")]
		private CheckBox _CheckBoxFinishTime;

		[AccessedThroughProperty("LabelFinishTime")]
		private Label _LabelFinishTime;

		[AccessedThroughProperty("TextBoxFinishTime")]
		private TextBox _TextBoxFinishTime;

		[AccessedThroughProperty("Label4")]
		private Label _Label4;

		[AccessedThroughProperty("Label5")]
		private Label _Label5;

		[AccessedThroughProperty("Label6")]
		private Label _Label6;

		public int FinishTime;

		public bool IsFinishTimerEnable;

		private const int SEC_OF_DAY = 86400;

		internal virtual Button ButtonCancel
		{
			[DebuggerNonUserCode]
			get
			{
				return _ButtonCancel;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_ButtonCancel = value;
			}
		}

		internal virtual Button ButtonOK
		{
			[DebuggerNonUserCode]
			get
			{
				return _ButtonOK;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_ButtonOK != null)
				{
					_ButtonOK.Click -= ButtonOK_Click;
				}
				_ButtonOK = value;
				if (_ButtonOK != null)
				{
					_ButtonOK.Click += ButtonOK_Click;
				}
			}
		}

		internal virtual CheckBox CheckBoxTrigger
		{
			[DebuggerNonUserCode]
			get
			{
				return _CheckBoxTrigger;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_CheckBoxTrigger != null)
				{
					_CheckBoxTrigger.CheckedChanged -= CheckBoxTrigger_CheckedChanged;
					_CheckBoxTrigger.Validating -= TextBoxTrigger_Validating;
				}
				_CheckBoxTrigger = value;
				if (_CheckBoxTrigger != null)
				{
					_CheckBoxTrigger.CheckedChanged += CheckBoxTrigger_CheckedChanged;
					_CheckBoxTrigger.Validating += TextBoxTrigger_Validating;
				}
			}
		}

		internal virtual Label LabelTrigger
		{
			[DebuggerNonUserCode]
			get
			{
				return _LabelTrigger;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_LabelTrigger = value;
			}
		}

		internal virtual TextBox TextBoxTrigger
		{
			[DebuggerNonUserCode]
			get
			{
				return _TextBoxTrigger;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_TextBoxTrigger != null)
				{
					_TextBoxTrigger.Validating -= TextBoxTrigger_Validating;
					_TextBoxTrigger.TextChanged -= TextBoxTrigger_TextChanged;
				}
				_TextBoxTrigger = value;
				if (_TextBoxTrigger != null)
				{
					_TextBoxTrigger.Validating += TextBoxTrigger_Validating;
					_TextBoxTrigger.TextChanged += TextBoxTrigger_TextChanged;
				}
			}
		}

		internal virtual TextBox TextBoxInterval
		{
			[DebuggerNonUserCode]
			get
			{
				return _TextBoxInterval;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_TextBoxInterval != null)
				{
					_TextBoxInterval.Validating -= TextBoxTrigger_Validating;
					_TextBoxInterval.TextChanged -= TextBoxInterval_TextChanged;
				}
				_TextBoxInterval = value;
				if (_TextBoxInterval != null)
				{
					_TextBoxInterval.Validating += TextBoxTrigger_Validating;
					_TextBoxInterval.TextChanged += TextBoxInterval_TextChanged;
				}
			}
		}

		internal virtual Label Label1
		{
			[DebuggerNonUserCode]
			get
			{
				return _Label1;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_Label1 = value;
			}
		}

		internal virtual Label Label3
		{
			[DebuggerNonUserCode]
			get
			{
				return _Label3;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_Label3 = value;
			}
		}

		internal virtual CheckBox CheckBoxFinishTime
		{
			[DebuggerNonUserCode]
			get
			{
				return _CheckBoxFinishTime;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_CheckBoxFinishTime != null)
				{
					_CheckBoxFinishTime.CheckedChanged -= CheckBoxFinishTime_CheckedChanged;
					_CheckBoxFinishTime.Validating -= TextBoxTrigger_Validating;
				}
				_CheckBoxFinishTime = value;
				if (_CheckBoxFinishTime != null)
				{
					_CheckBoxFinishTime.CheckedChanged += CheckBoxFinishTime_CheckedChanged;
					_CheckBoxFinishTime.Validating += TextBoxTrigger_Validating;
				}
			}
		}

		internal virtual Label LabelFinishTime
		{
			[DebuggerNonUserCode]
			get
			{
				return _LabelFinishTime;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_LabelFinishTime = value;
			}
		}

		internal virtual TextBox TextBoxFinishTime
		{
			[DebuggerNonUserCode]
			get
			{
				return _TextBoxFinishTime;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_TextBoxFinishTime != null)
				{
					_TextBoxFinishTime.TextChanged -= TextBoxFinishTime_TextChanged;
				}
				_TextBoxFinishTime = value;
				if (_TextBoxFinishTime != null)
				{
					_TextBoxFinishTime.TextChanged += TextBoxFinishTime_TextChanged;
				}
			}
		}

		internal virtual Label Label4
		{
			[DebuggerNonUserCode]
			get
			{
				return _Label4;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_Label4 = value;
			}
		}

		internal virtual Label Label5
		{
			[DebuggerNonUserCode]
			get
			{
				return _Label5;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_Label5 = value;
			}
		}

		internal virtual Label Label6
		{
			[DebuggerNonUserCode]
			get
			{
				return _Label6;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_Label6 = value;
			}
		}

		public AutoRecordConfig Config
		{
			get
			{
				AutoRecordConfig autoRecordConfig = new AutoRecordConfig();
				autoRecordConfig.Interval = (float)Conversion.Val(TextBoxInterval.Text);
				autoRecordConfig.FinishTime = checked((uint)Math.Round(Conversion.Val(TextBoxFinishTime.Text)));
				autoRecordConfig.TriggerValue = (float)Conversion.Val(TextBoxTrigger.Text);
				autoRecordConfig.IsFinishTimeEnable = CheckBoxFinishTime.Checked;
				autoRecordConfig.IsTriggerEnable = CheckBoxTrigger.Checked;
				return autoRecordConfig;
			}
		}

		public FormAutoRecord()
		{
			base.Load += FormTrigger_Load;
			InitializeComponent();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		[System.Diagnostics.DebuggerStepThrough]
		private void InitializeComponent()
		{
			ButtonCancel = new System.Windows.Forms.Button();
			ButtonOK = new System.Windows.Forms.Button();
			CheckBoxTrigger = new System.Windows.Forms.CheckBox();
			LabelTrigger = new System.Windows.Forms.Label();
			TextBoxTrigger = new System.Windows.Forms.TextBox();
			TextBoxInterval = new System.Windows.Forms.TextBox();
			Label1 = new System.Windows.Forms.Label();
			Label3 = new System.Windows.Forms.Label();
			Label6 = new System.Windows.Forms.Label();
			CheckBoxFinishTime = new System.Windows.Forms.CheckBox();
			LabelFinishTime = new System.Windows.Forms.Label();
			TextBoxFinishTime = new System.Windows.Forms.TextBox();
			Label4 = new System.Windows.Forms.Label();
			Label5 = new System.Windows.Forms.Label();
			SuspendLayout();
			ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			ButtonCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			ButtonCancel.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			System.Drawing.Point point2 = ButtonCancel.Location = new System.Drawing.Point(17, 225);
			ButtonCancel.Name = "ButtonCancel";
			System.Drawing.Size size2 = ButtonCancel.Size = new System.Drawing.Size(75, 23);
			ButtonCancel.TabIndex = 5;
			ButtonCancel.Text = "Cancel";
			ButtonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			ButtonOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			ButtonOK.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			point2 = (ButtonOK.Location = new System.Drawing.Point(161, 225));
			ButtonOK.Name = "ButtonOK";
			size2 = (ButtonOK.Size = new System.Drawing.Size(75, 23));
			ButtonOK.TabIndex = 6;
			ButtonOK.Text = "Apply";
			CheckBoxTrigger.FlatStyle = System.Windows.Forms.FlatStyle.System;
			CheckBoxTrigger.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			point2 = (CheckBoxTrigger.Location = new System.Drawing.Point(12, 67));
			CheckBoxTrigger.Name = "CheckBoxTrigger";
			size2 = (CheckBoxTrigger.Size = new System.Drawing.Size(168, 24));
			CheckBoxTrigger.TabIndex = 2;
			CheckBoxTrigger.Text = "Enable Trigger";
			LabelTrigger.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			point2 = (LabelTrigger.Location = new System.Drawing.Point(18, 89));
			LabelTrigger.Name = "LabelTrigger";
			size2 = (LabelTrigger.Size = new System.Drawing.Size(74, 21));
			LabelTrigger.TabIndex = 3;
			LabelTrigger.Text = "Trigger Value";
			LabelTrigger.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			TextBoxTrigger.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			TextBoxTrigger.ImeMode = System.Windows.Forms.ImeMode.Disable;
			point2 = (TextBoxTrigger.Location = new System.Drawing.Point(115, 88));
			TextBoxTrigger.Name = "TextBoxTrigger";
			size2 = (TextBoxTrigger.Size = new System.Drawing.Size(94, 21));
			TextBoxTrigger.TabIndex = 3;
			TextBoxTrigger.Text = "0";
			TextBoxInterval.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			TextBoxInterval.ImeMode = System.Windows.Forms.ImeMode.Disable;
			point2 = (TextBoxInterval.Location = new System.Drawing.Point(117, 30));
			TextBoxInterval.Name = "TextBoxInterval";
			size2 = (TextBoxInterval.Size = new System.Drawing.Size(94, 21));
			TextBoxInterval.TabIndex = 4;
			TextBoxInterval.Text = "0.5";
			Label1.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			point2 = (Label1.Location = new System.Drawing.Point(20, 29));
			Label1.Name = "Label1";
			size2 = (Label1.Size = new System.Drawing.Size(91, 20));
			Label1.TabIndex = 3;
			Label1.Text = "Record Interval";
			Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			Label3.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			point2 = (Label3.Location = new System.Drawing.Point(217, 31));
			Label3.Name = "Label3";
			size2 = (Label3.Size = new System.Drawing.Size(32, 16));
			Label3.TabIndex = 3;
			Label3.Text = "Sec.";
			Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			Label6.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			Label6.ForeColor = System.Drawing.SystemColors.Highlight;
			point2 = (Label6.Location = new System.Drawing.Point(20, 110));
			Label6.Name = "Label6";
			size2 = (Label6.Size = new System.Drawing.Size(216, 18));
			Label6.TabIndex = 3;
			Label6.Text = "With no sign / \".\" for decimal point";
			Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			CheckBoxFinishTime.FlatStyle = System.Windows.Forms.FlatStyle.System;
			CheckBoxFinishTime.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			point2 = (CheckBoxFinishTime.Location = new System.Drawing.Point(12, 139));
			CheckBoxFinishTime.Name = "CheckBoxFinishTime";
			size2 = (CheckBoxFinishTime.Size = new System.Drawing.Size(168, 24));
			CheckBoxFinishTime.TabIndex = 2;
			CheckBoxFinishTime.Text = "Enable Recording Timer";
			LabelFinishTime.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			point2 = (LabelFinishTime.Location = new System.Drawing.Point(18, 161));
			LabelFinishTime.Name = "LabelFinishTime";
			size2 = (LabelFinishTime.Size = new System.Drawing.Size(45, 16));
			LabelFinishTime.TabIndex = 3;
			LabelFinishTime.Text = "Timer";
			LabelFinishTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			TextBoxFinishTime.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			TextBoxFinishTime.ImeMode = System.Windows.Forms.ImeMode.Disable;
			point2 = (TextBoxFinishTime.Location = new System.Drawing.Point(115, 160));
			TextBoxFinishTime.Name = "TextBoxFinishTime";
			size2 = (TextBoxFinishTime.Size = new System.Drawing.Size(94, 21));
			TextBoxFinishTime.TabIndex = 3;
			TextBoxFinishTime.Text = "0";
			Label4.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			point2 = (Label4.Location = new System.Drawing.Point(217, 161));
			Label4.Name = "Label4";
			size2 = (Label4.Size = new System.Drawing.Size(32, 16));
			Label4.TabIndex = 3;
			Label4.Text = "Sec.";
			Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			Label5.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			Label5.ForeColor = System.Drawing.SystemColors.Highlight;
			point2 = (Label5.Location = new System.Drawing.Point(30, 182));
			Label5.Name = "Label5";
			size2 = (Label5.Size = new System.Drawing.Size(216, 18));
			Label5.TabIndex = 3;
			Label5.Text = "Up to 300000 seconds";
			Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			AcceptButton = ButtonOK;
			size2 = (AutoScaleBaseSize = new System.Drawing.Size(6, 14));
			CancelButton = ButtonCancel;
			size2 = (ClientSize = new System.Drawing.Size(263, 267));
			Controls.Add(TextBoxFinishTime);
			Controls.Add(TextBoxTrigger);
			Controls.Add(Label5);
			Controls.Add(Label6);
			Controls.Add(LabelFinishTime);
			Controls.Add(LabelTrigger);
			Controls.Add(CheckBoxFinishTime);
			Controls.Add(CheckBoxTrigger);
			Controls.Add(ButtonOK);
			Controls.Add(ButtonCancel);
			Controls.Add(TextBoxInterval);
			Controls.Add(Label1);
			Controls.Add(Label4);
			Controls.Add(Label3);
			Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			Name = "FormAutoRecord";
			ShowInTaskbar = false;
			Text = "IMADA ZLINK3 - Auto Recording";
			TopMost = true;
			ResumeLayout(false);
			PerformLayout();
		}

		public FormAutoRecord(AutoRecordConfig init)
			: this()
		{
			TextBoxInterval.Text = init.Interval.ToString();
			TextBoxFinishTime.Text = init.FinishTime.ToString();
			TextBoxTrigger.Text = init.TriggerValue.ToString();
			CheckBoxFinishTime.Checked = init.IsFinishTimeEnable;
			CheckBoxTrigger.Checked = init.IsTriggerEnable;
		}

		private void ButtonOK_Click(object sender, EventArgs e)
		{
		}

		private void TextBoxTrigger_TextChanged(object sender, EventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			textBox.Text = Regex.Match(textBox.Text, "^[0-9]*\\.?[0-9]*").ToString();
			textBox.Select(textBox.Text.Length, 0);
			isValid();
		}

		private void TextBoxDataNum_TextChanged(object sender, EventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			textBox.Text = Regex.Match(textBox.Text, "^[0-9]*").ToString();
			textBox.Select(textBox.Text.Length, 0);
			isValid();
		}

		private void TextBoxFinishTime_TextChanged(object sender, EventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			textBox.Text = Regex.Match(textBox.Text, "^[0-9]*").ToString();
			textBox.Select(textBox.Text.Length, 0);
			isValid();
		}

		private void TextBoxInterval_TextChanged(object sender, EventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			textBox.Text = Regex.Match(textBox.Text, "^[0-9]*\\.?[0-9]{0,2}").ToString();
			textBox.Select(textBox.Text.Length, 0);
			if (Conversion.Val(textBox.Text) < 0.1)
			{
				ButtonOK.Enabled = false;
			}
			else
			{
				ButtonOK.Enabled = true;
			}
			isValid();
		}

		private void isValid()
		{
			try
			{
				if (CheckBoxTrigger.Checked)
				{
					if ((float)Conversion.Val(TextBoxTrigger.Text) <= 0f)
					{
						TextBoxTrigger.BackColor = Color.Yellow;
						ButtonOK.Enabled = false;
						return;
					}
					TextBoxTrigger.BackColor = SystemColors.Window;
				}
				float num = (float)Conversion.Val(TextBoxInterval.Text);
				if ((num < 0.02f) | (num > 86400f))
				{
					TextBoxInterval.BackColor = Color.Yellow;
					ButtonOK.Enabled = false;
					return;
				}
				TextBoxInterval.BackColor = SystemColors.Window;
				if (CheckBoxFinishTime.Checked)
				{
					int num2 = checked((int)Math.Round(Conversion.Val(TextBoxFinishTime.Text)));
					if ((num2 < 1) | (num2 > 300000) | ((float)num2 < num))
					{
						TextBoxFinishTime.BackColor = Color.Yellow;
						ButtonOK.Enabled = false;
						return;
					}
					TextBoxFinishTime.BackColor = SystemColors.Window;
				}
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				ButtonOK.Enabled = false;
				ProjectData.ClearProjectError();
				return;
			}
			ButtonOK.Enabled = true;
		}

		private void TextBoxTrigger_Validating(object sender, CancelEventArgs e)
		{
			isValid();
		}

		private void FormTrigger_Load(object sender, EventArgs e)
		{
			if (CheckBoxFinishTime.Checked)
			{
				LabelFinishTime.Enabled = true;
				TextBoxFinishTime.Enabled = true;
			}
			else
			{
				LabelFinishTime.Enabled = false;
				TextBoxFinishTime.Enabled = false;
			}
			if (CheckBoxTrigger.Checked)
			{
				LabelTrigger.Enabled = true;
				TextBoxTrigger.Enabled = true;
			}
			else
			{
				LabelTrigger.Enabled = false;
				TextBoxTrigger.Enabled = false;
			}
			isValid();
		}

		private void CheckBoxTrigger_CheckedChanged(object sender, EventArgs e)
		{
			if (CheckBoxTrigger.Checked)
			{
				LabelTrigger.Enabled = true;
				TextBoxTrigger.Enabled = true;
				isValid();
			}
			else
			{
				LabelTrigger.Enabled = false;
				TextBoxTrigger.Enabled = false;
			}
		}

		private void CheckBoxFinishTime_CheckedChanged(object sender, EventArgs e)
		{
			if (CheckBoxFinishTime.Checked)
			{
				LabelFinishTime.Enabled = true;
				TextBoxFinishTime.Enabled = true;
				isValid();
			}
			else
			{
				LabelFinishTime.Enabled = false;
				TextBoxFinishTime.Enabled = false;
			}
		}
	}
}
