using CommonZlinkLib.ForceGauge;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ZLINK31E.My.Resources;

namespace ZLINK31E
{
	public class FormGaugeConfig : Form
	{
		private IContainer components;

		[AccessedThroughProperty("ButtonOK")]
		private Button _ButtonOK;

		[AccessedThroughProperty("Label1")]
		private Label _Label1;

		[AccessedThroughProperty("Label2")]
		private Label _Label2;

		[AccessedThroughProperty("GroupBoxComparator")]
		private GroupBox _GroupBoxComparator;

		[AccessedThroughProperty("GroupBoxUnit")]
		private GroupBox _GroupBoxUnit;

		[AccessedThroughProperty("GroupBoxDirection")]
		private GroupBox _GroupBoxDirection;

		[AccessedThroughProperty("TextBoxUpper")]
		private TextBox _TextBoxUpper;

		[AccessedThroughProperty("TextBoxLower")]
		private TextBox _TextBoxLower;

		[AccessedThroughProperty("RadioButtonN")]
		private RadioButton _RadioButtonN;

		[AccessedThroughProperty("RadioButtonK")]
		private RadioButton _RadioButtonK;

		[AccessedThroughProperty("RadioButtonO")]
		private RadioButton _RadioButtonO;

		[AccessedThroughProperty("RadioButtonReverse")]
		private RadioButton _RadioButtonReverse;

		[AccessedThroughProperty("ButtonSetComparator")]
		private Button _ButtonSetComparator;

		[AccessedThroughProperty("ButtonSetUnit")]
		private Button _ButtonSetUnit;

		[AccessedThroughProperty("ButtonSetDirection")]
		private Button _ButtonSetDirection;

		[AccessedThroughProperty("RadioButtonNormal")]
		private RadioButton _RadioButtonNormal;

		private AbstractForceGauge gauge;

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
				_ButtonOK = value;
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

		internal virtual Label Label2
		{
			[DebuggerNonUserCode]
			get
			{
				return _Label2;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_Label2 = value;
			}
		}

		internal virtual GroupBox GroupBoxComparator
		{
			[DebuggerNonUserCode]
			get
			{
				return _GroupBoxComparator;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_GroupBoxComparator = value;
			}
		}

		internal virtual GroupBox GroupBoxUnit
		{
			[DebuggerNonUserCode]
			get
			{
				return _GroupBoxUnit;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_GroupBoxUnit = value;
			}
		}

		internal virtual GroupBox GroupBoxDirection
		{
			[DebuggerNonUserCode]
			get
			{
				return _GroupBoxDirection;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_GroupBoxDirection = value;
			}
		}

		internal virtual TextBox TextBoxUpper
		{
			[DebuggerNonUserCode]
			get
			{
				return _TextBoxUpper;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_TextBoxUpper != null)
				{
					_TextBoxUpper.TextChanged -= TextBoxUpperLower_TextChanged;
				}
				_TextBoxUpper = value;
				if (_TextBoxUpper != null)
				{
					_TextBoxUpper.TextChanged += TextBoxUpperLower_TextChanged;
				}
			}
		}

		internal virtual TextBox TextBoxLower
		{
			[DebuggerNonUserCode]
			get
			{
				return _TextBoxLower;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_TextBoxLower != null)
				{
					_TextBoxLower.TextChanged -= TextBoxUpperLower_TextChanged;
				}
				_TextBoxLower = value;
				if (_TextBoxLower != null)
				{
					_TextBoxLower.TextChanged += TextBoxUpperLower_TextChanged;
				}
			}
		}

		internal virtual RadioButton RadioButtonN
		{
			[DebuggerNonUserCode]
			get
			{
				return _RadioButtonN;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_RadioButtonN = value;
			}
		}

		internal virtual RadioButton RadioButtonK
		{
			[DebuggerNonUserCode]
			get
			{
				return _RadioButtonK;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_RadioButtonK = value;
			}
		}

		internal virtual RadioButton RadioButtonO
		{
			[DebuggerNonUserCode]
			get
			{
				return _RadioButtonO;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_RadioButtonO = value;
			}
		}

		internal virtual RadioButton RadioButtonReverse
		{
			[DebuggerNonUserCode]
			get
			{
				return _RadioButtonReverse;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_RadioButtonReverse = value;
			}
		}

		internal virtual Button ButtonSetComparator
		{
			[DebuggerNonUserCode]
			get
			{
				return _ButtonSetComparator;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_ButtonSetComparator != null)
				{
					_ButtonSetComparator.Click -= ButtonSetComparator_Click;
				}
				_ButtonSetComparator = value;
				if (_ButtonSetComparator != null)
				{
					_ButtonSetComparator.Click += ButtonSetComparator_Click;
				}
			}
		}

		internal virtual Button ButtonSetUnit
		{
			[DebuggerNonUserCode]
			get
			{
				return _ButtonSetUnit;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_ButtonSetUnit != null)
				{
					_ButtonSetUnit.Click -= ButtonSetUnit_Click;
				}
				_ButtonSetUnit = value;
				if (_ButtonSetUnit != null)
				{
					_ButtonSetUnit.Click += ButtonSetUnit_Click;
				}
			}
		}

		internal virtual Button ButtonSetDirection
		{
			[DebuggerNonUserCode]
			get
			{
				return _ButtonSetDirection;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_ButtonSetDirection != null)
				{
					_ButtonSetDirection.Click -= ButtonSetDirection_Click;
				}
				_ButtonSetDirection = value;
				if (_ButtonSetDirection != null)
				{
					_ButtonSetDirection.Click += ButtonSetDirection_Click;
				}
			}
		}

		internal virtual RadioButton RadioButtonNormal
		{
			[DebuggerNonUserCode]
			get
			{
				return _RadioButtonNormal;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_RadioButtonNormal = value;
			}
		}

		private FormGaugeConfig()
		{
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
			ButtonOK = new System.Windows.Forms.Button();
			GroupBoxComparator = new System.Windows.Forms.GroupBox();
			Label1 = new System.Windows.Forms.Label();
			ButtonSetComparator = new System.Windows.Forms.Button();
			TextBoxUpper = new System.Windows.Forms.TextBox();
			TextBoxLower = new System.Windows.Forms.TextBox();
			Label2 = new System.Windows.Forms.Label();
			GroupBoxUnit = new System.Windows.Forms.GroupBox();
			RadioButtonN = new System.Windows.Forms.RadioButton();
			ButtonSetUnit = new System.Windows.Forms.Button();
			RadioButtonK = new System.Windows.Forms.RadioButton();
			RadioButtonO = new System.Windows.Forms.RadioButton();
			GroupBoxDirection = new System.Windows.Forms.GroupBox();
			RadioButtonReverse = new System.Windows.Forms.RadioButton();
			ButtonSetDirection = new System.Windows.Forms.Button();
			RadioButtonNormal = new System.Windows.Forms.RadioButton();
			GroupBoxComparator.SuspendLayout();
			GroupBoxUnit.SuspendLayout();
			GroupBoxDirection.SuspendLayout();
			SuspendLayout();
			ButtonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			ButtonOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			ButtonOK.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			System.Drawing.Point point2 = ButtonOK.Location = new System.Drawing.Point(235, 228);
			ButtonOK.Name = "ButtonOK";
			System.Drawing.Size size2 = ButtonOK.Size = new System.Drawing.Size(75, 23);
			ButtonOK.TabIndex = 10;
			ButtonOK.Text = "Close";
			GroupBoxComparator.Controls.Add(Label1);
			GroupBoxComparator.Controls.Add(ButtonSetComparator);
			GroupBoxComparator.Controls.Add(TextBoxUpper);
			GroupBoxComparator.Controls.Add(TextBoxLower);
			GroupBoxComparator.Controls.Add(Label2);
			GroupBoxComparator.FlatStyle = System.Windows.Forms.FlatStyle.System;
			GroupBoxComparator.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			point2 = (GroupBoxComparator.Location = new System.Drawing.Point(8, 8));
			GroupBoxComparator.Name = "GroupBoxComparator";
			size2 = (GroupBoxComparator.Size = new System.Drawing.Size(302, 72));
			GroupBoxComparator.TabIndex = 2;
			GroupBoxComparator.TabStop = false;
			GroupBoxComparator.Text = "Comparator";
			Label1.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			point2 = (Label1.Location = new System.Drawing.Point(6, 14));
			Label1.Name = "Label1";
			size2 = (Label1.Size = new System.Drawing.Size(53, 23));
			Label1.TabIndex = 1;
			Label1.Text = "Value1";
			Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			ButtonSetComparator.FlatStyle = System.Windows.Forms.FlatStyle.System;
			ButtonSetComparator.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			point2 = (ButtonSetComparator.Location = new System.Drawing.Point(215, 36));
			ButtonSetComparator.Name = "ButtonSetComparator";
			size2 = (ButtonSetComparator.Size = new System.Drawing.Size(75, 23));
			ButtonSetComparator.TabIndex = 2;
			ButtonSetComparator.Text = "Apply";
			TextBoxUpper.BackColor = System.Drawing.SystemColors.Window;
			TextBoxUpper.ImeMode = System.Windows.Forms.ImeMode.Disable;
			point2 = (TextBoxUpper.Location = new System.Drawing.Point(64, 16));
			TextBoxUpper.MaxLength = 5;
			TextBoxUpper.Name = "TextBoxUpper";
			size2 = (TextBoxUpper.Size = new System.Drawing.Size(128, 21));
			TextBoxUpper.TabIndex = 0;
			TextBoxLower.ImeMode = System.Windows.Forms.ImeMode.Disable;
			point2 = (TextBoxLower.Location = new System.Drawing.Point(64, 40));
			TextBoxLower.MaxLength = 5;
			TextBoxLower.Name = "TextBoxLower";
			size2 = (TextBoxLower.Size = new System.Drawing.Size(128, 21));
			TextBoxLower.TabIndex = 1;
			Label2.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			point2 = (Label2.Location = new System.Drawing.Point(6, 38));
			Label2.Name = "Label2";
			size2 = (Label2.Size = new System.Drawing.Size(53, 23));
			Label2.TabIndex = 1;
			Label2.Text = "Value2";
			Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			GroupBoxUnit.Controls.Add(RadioButtonN);
			GroupBoxUnit.Controls.Add(ButtonSetUnit);
			GroupBoxUnit.Controls.Add(RadioButtonK);
			GroupBoxUnit.Controls.Add(RadioButtonO);
			GroupBoxUnit.FlatStyle = System.Windows.Forms.FlatStyle.System;
			GroupBoxUnit.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			point2 = (GroupBoxUnit.Location = new System.Drawing.Point(8, 88));
			GroupBoxUnit.Name = "GroupBoxUnit";
			size2 = (GroupBoxUnit.Size = new System.Drawing.Size(137, 134));
			GroupBoxUnit.TabIndex = 2;
			GroupBoxUnit.TabStop = false;
			GroupBoxUnit.Text = "Unit";
			RadioButtonN.FlatStyle = System.Windows.Forms.FlatStyle.System;
			RadioButtonN.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			point2 = (RadioButtonN.Location = new System.Drawing.Point(6, 44));
			RadioButtonN.Name = "RadioButtonN";
			size2 = (RadioButtonN.Size = new System.Drawing.Size(100, 20));
			RadioButtonN.TabIndex = 4;
			RadioButtonN.Text = "N System";
			ButtonSetUnit.FlatStyle = System.Windows.Forms.FlatStyle.System;
			ButtonSetUnit.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			point2 = (ButtonSetUnit.Location = new System.Drawing.Point(56, 105));
			ButtonSetUnit.Name = "ButtonSetUnit";
			size2 = (ButtonSetUnit.Size = new System.Drawing.Size(75, 23));
			ButtonSetUnit.TabIndex = 6;
			ButtonSetUnit.Text = "Apply";
			RadioButtonK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			RadioButtonK.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			point2 = (RadioButtonK.Location = new System.Drawing.Point(6, 18));
			RadioButtonK.Name = "RadioButtonK";
			size2 = (RadioButtonK.Size = new System.Drawing.Size(100, 20));
			RadioButtonK.TabIndex = 3;
			RadioButtonK.Text = "kg System";
			RadioButtonO.FlatStyle = System.Windows.Forms.FlatStyle.System;
			RadioButtonO.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			point2 = (RadioButtonO.Location = new System.Drawing.Point(6, 70));
			RadioButtonO.Name = "RadioButtonO";
			size2 = (RadioButtonO.Size = new System.Drawing.Size(100, 20));
			RadioButtonO.TabIndex = 5;
			RadioButtonO.Text = "lb System";
			GroupBoxDirection.Controls.Add(RadioButtonReverse);
			GroupBoxDirection.Controls.Add(ButtonSetDirection);
			GroupBoxDirection.Controls.Add(RadioButtonNormal);
			GroupBoxDirection.FlatStyle = System.Windows.Forms.FlatStyle.System;
			GroupBoxDirection.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			point2 = (GroupBoxDirection.Location = new System.Drawing.Point(151, 88));
			GroupBoxDirection.Name = "GroupBoxDirection";
			size2 = (GroupBoxDirection.Size = new System.Drawing.Size(159, 134));
			GroupBoxDirection.TabIndex = 2;
			GroupBoxDirection.TabStop = false;
			GroupBoxDirection.Text = "Display Direction";
			RadioButtonReverse.FlatStyle = System.Windows.Forms.FlatStyle.System;
			RadioButtonReverse.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			point2 = (RadioButtonReverse.Location = new System.Drawing.Point(6, 48));
			RadioButtonReverse.Name = "RadioButtonReverse";
			size2 = (RadioButtonReverse.Size = new System.Drawing.Size(104, 24));
			RadioButtonReverse.TabIndex = 8;
			RadioButtonReverse.Text = "Reverse";
			ButtonSetDirection.FlatStyle = System.Windows.Forms.FlatStyle.System;
			ButtonSetDirection.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			point2 = (ButtonSetDirection.Location = new System.Drawing.Point(78, 105));
			ButtonSetDirection.Name = "ButtonSetDirection";
			size2 = (ButtonSetDirection.Size = new System.Drawing.Size(75, 23));
			ButtonSetDirection.TabIndex = 9;
			ButtonSetDirection.Text = "Apply";
			RadioButtonNormal.FlatStyle = System.Windows.Forms.FlatStyle.System;
			RadioButtonNormal.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			point2 = (RadioButtonNormal.Location = new System.Drawing.Point(6, 18));
			RadioButtonNormal.Name = "RadioButtonNormal";
			size2 = (RadioButtonNormal.Size = new System.Drawing.Size(104, 24));
			RadioButtonNormal.TabIndex = 7;
			RadioButtonNormal.Text = "Normal";
			size2 = (AutoScaleBaseSize = new System.Drawing.Size(6, 14));
			CancelButton = ButtonOK;
			size2 = (ClientSize = new System.Drawing.Size(320, 261));
			Controls.Add(GroupBoxComparator);
			Controls.Add(ButtonOK);
			Controls.Add(GroupBoxUnit);
			Controls.Add(GroupBoxDirection);
			Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			MinimizeBox = false;
			Name = "FormGaugeConfig";
			ShowInTaskbar = false;
			Text = "IMADA ZLINK3 - Instrument";
			TopMost = true;
			GroupBoxComparator.ResumeLayout(false);
			GroupBoxComparator.PerformLayout();
			GroupBoxUnit.ResumeLayout(false);
			GroupBoxDirection.ResumeLayout(false);
			ResumeLayout(false);
		}

		public FormGaugeConfig(AbstractForceGauge theGauge)
			: this()
		{
			gauge = theGauge;
			switch (gauge.Series)
			{
			case GaugeCompatibility.Dpx:
			case GaugeCompatibility.Dps:
				GroupBoxDirection.Enabled = false;
				break;
			default:
				switch (gauge.GetDisplayDirection())
				{
				case DisplayDirection.Normal:
					RadioButtonNormal.Checked = true;
					break;
				case DisplayDirection.Reverse:
					RadioButtonReverse.Checked = true;
					break;
				}
				break;
			}
			GaugeCompatibility series = gauge.Series;
			if (series == GaugeCompatibility.Dps)
			{
				GroupBoxComparator.Enabled = false;
			}
			else
			{
				Comparator comparator = gauge.GetComparator();
				TextBoxLower.Text = comparator.Lower;
				TextBoxUpper.Text = comparator.Upper;
			}
			gauge.ClearBuffer();
			string value = gauge.GetValue();
			if (value.Length >= 6)
			{
				switch (value[6])
				{
				case 'M':
					break;
				case 'K':
					RadioButtonK.Checked = true;
					break;
				case 'N':
					RadioButtonN.Checked = true;
					break;
				case 'L':
				case 'O':
					RadioButtonO.Checked = true;
					break;
				}
			}
		}

		protected void TextBoxUpperLower_TextChanged(object sender, EventArgs e)
		{
			if (gauge.Series == GaugeCompatibility.Dpx)
			{
				TextBox textBox = (TextBox)sender;
				textBox.Text = Regex.Match(textBox.Text, "[-\\+]?[0-9]{0,4}").ToString();
				textBox.Select(textBox.Text.Length, 0);
			}
			else
			{
				TextBox textBox2 = (TextBox)sender;
				textBox2.Text = Regex.Match(textBox2.Text, "[0-9]{0,4}").ToString();
				textBox2.Select(textBox2.Text.Length, 0);
			}
		}

		private void ButtonSetComparator_Click(object sender, EventArgs e)
		{
			gauge.SetComparator(new Comparator(TextBoxLower.Text, TextBoxUpper.Text));
		}

		private void ButtonSetUnit_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show(Resources.CAUTION_CHANGE_UNIT, Resources.CAUTION, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
			{
				if (RadioButtonK.Checked)
				{
					gauge.SelectUnit(1);
				}
				else if (RadioButtonN.Checked)
				{
					gauge.SelectUnit(2);
				}
				else if (RadioButtonO.Checked)
				{
					gauge.SelectUnit(3);
				}
			}
		}

		private void ButtonSetDirection_Click(object sender, EventArgs e)
		{
			if (RadioButtonNormal.Checked)
			{
				gauge.SelectDisplayDirection(DisplayDirection.Normal);
			}
			else if (RadioButtonReverse.Checked)
			{
				gauge.SelectDisplayDirection(DisplayDirection.Reverse);
			}
		}
	}
}
