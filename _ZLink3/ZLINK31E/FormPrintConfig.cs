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
	[DesignerGenerated]
	public class FormPrintConfig : Form
	{
		private IContainer components;

		[AccessedThroughProperty("ButtonOK")]
		private Button _ButtonOK;

		[AccessedThroughProperty("ButtonCancel")]
		private Button _ButtonCancel;

		[AccessedThroughProperty("Label1")]
		private Label _Label1;

		[AccessedThroughProperty("CheckBoxBand")]
		private CheckBox _CheckBoxBand;

		[AccessedThroughProperty("GroupBox1")]
		private GroupBox _GroupBox1;

		[AccessedThroughProperty("TextBoxValue2")]
		private TextBox _TextBoxValue2;

		[AccessedThroughProperty("TextBoxValue1")]
		private TextBox _TextBoxValue1;

		[AccessedThroughProperty("Label2")]
		private Label _Label2;

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

		internal virtual CheckBox CheckBoxBand
		{
			[DebuggerNonUserCode]
			get
			{
				return _CheckBoxBand;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_CheckBoxBand = value;
			}
		}

		internal virtual GroupBox GroupBox1
		{
			[DebuggerNonUserCode]
			get
			{
				return _GroupBox1;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_GroupBox1 = value;
			}
		}

		internal virtual TextBox TextBoxValue2
		{
			[DebuggerNonUserCode]
			get
			{
				return _TextBoxValue2;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_TextBoxValue2 != null)
				{
					_TextBoxValue2.TextChanged -= TextBoxValue1_TextChanged;
				}
				_TextBoxValue2 = value;
				if (_TextBoxValue2 != null)
				{
					_TextBoxValue2.TextChanged += TextBoxValue1_TextChanged;
				}
			}
		}

		internal virtual TextBox TextBoxValue1
		{
			[DebuggerNonUserCode]
			get
			{
				return _TextBoxValue1;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_TextBoxValue1 != null)
				{
					_TextBoxValue1.TextChanged -= TextBoxValue1_TextChanged;
				}
				_TextBoxValue1 = value;
				if (_TextBoxValue1 != null)
				{
					_TextBoxValue1.TextChanged += TextBoxValue1_TextChanged;
				}
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

		[DebuggerNonUserCode]
		public FormPrintConfig()
		{
			InitializeComponent();
		}

		[DebuggerNonUserCode]
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
			ButtonCancel = new System.Windows.Forms.Button();
			Label1 = new System.Windows.Forms.Label();
			CheckBoxBand = new System.Windows.Forms.CheckBox();
			GroupBox1 = new System.Windows.Forms.GroupBox();
			TextBoxValue2 = new System.Windows.Forms.TextBox();
			TextBoxValue1 = new System.Windows.Forms.TextBox();
			Label2 = new System.Windows.Forms.Label();
			GroupBox1.SuspendLayout();
			SuspendLayout();
			ButtonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			ButtonOK.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			System.Drawing.Point point2 = ButtonOK.Location = new System.Drawing.Point(160, 159);
			System.Windows.Forms.Padding padding2 = ButtonOK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			ButtonOK.Name = "ButtonOK";
			System.Drawing.Size size2 = ButtonOK.Size = new System.Drawing.Size(87, 29);
			ButtonOK.TabIndex = 0;
			ButtonOK.Text = "Apply";
			ButtonOK.UseVisualStyleBackColor = true;
			ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			ButtonCancel.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			point2 = (ButtonCancel.Location = new System.Drawing.Point(36, 159));
			padding2 = (ButtonCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4));
			ButtonCancel.Name = "ButtonCancel";
			size2 = (ButtonCancel.Size = new System.Drawing.Size(87, 29));
			ButtonCancel.TabIndex = 1;
			ButtonCancel.Text = "Cancel";
			ButtonCancel.UseVisualStyleBackColor = true;
			Label1.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			point2 = (Label1.Location = new System.Drawing.Point(7, 46));
			Label1.Name = "Label1";
			size2 = (Label1.Size = new System.Drawing.Size(71, 22));
			Label1.TabIndex = 2;
			Label1.Text = "Line 1";
			Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			CheckBoxBand.AutoSize = true;
			CheckBoxBand.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			point2 = (CheckBoxBand.Location = new System.Drawing.Point(7, 22));
			padding2 = (CheckBoxBand.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4));
			CheckBoxBand.Name = "CheckBoxBand";
			size2 = (CheckBoxBand.Size = new System.Drawing.Size(185, 24));
			CheckBoxBand.TabIndex = 3;
			CheckBoxBand.Text = "Print Upper/Lower Lines";
			CheckBoxBand.UseVisualStyleBackColor = true;
			GroupBox1.Controls.Add(TextBoxValue2);
			GroupBox1.Controls.Add(TextBoxValue1);
			GroupBox1.Controls.Add(CheckBoxBand);
			GroupBox1.Controls.Add(Label2);
			GroupBox1.Controls.Add(Label1);
			GroupBox1.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			point2 = (GroupBox1.Location = new System.Drawing.Point(14, 15));
			padding2 = (GroupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4));
			GroupBox1.Name = "GroupBox1";
			padding2 = (GroupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4));
			size2 = (GroupBox1.Size = new System.Drawing.Size(233, 125));
			GroupBox1.TabIndex = 4;
			GroupBox1.TabStop = false;
			GroupBox1.Text = "Upper / Lower Bars";
			point2 = (TextBoxValue2.Location = new System.Drawing.Point(96, 78));
			padding2 = (TextBoxValue2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4));
			TextBoxValue2.Name = "TextBoxValue2";
			size2 = (TextBoxValue2.Size = new System.Drawing.Size(116, 21));
			TextBoxValue2.TabIndex = 5;
			point2 = (TextBoxValue1.Location = new System.Drawing.Point(96, 46));
			padding2 = (TextBoxValue1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4));
			TextBoxValue1.Name = "TextBoxValue1";
			size2 = (TextBoxValue1.Size = new System.Drawing.Size(116, 21));
			TextBoxValue1.TabIndex = 4;
			Label2.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			point2 = (Label2.Location = new System.Drawing.Point(7, 78));
			Label2.Name = "Label2";
			size2 = (Label2.Size = new System.Drawing.Size(71, 22));
			Label2.TabIndex = 2;
			Label2.Text = "Line 2";
			Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			AcceptButton = ButtonOK;
			System.Drawing.SizeF sizeF2 = AutoScaleDimensions = new System.Drawing.SizeF(7f, 15f);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			CancelButton = ButtonCancel;
			size2 = (ClientSize = new System.Drawing.Size(267, 208));
			Controls.Add(GroupBox1);
			Controls.Add(ButtonCancel);
			Controls.Add(ButtonOK);
			Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			padding2 = (Margin = new System.Windows.Forms.Padding(3, 4, 3, 4));
			Name = "FormPrintConfig";
			Text = "IMADA ZLINK3 - Printing";
			GroupBox1.ResumeLayout(false);
			GroupBox1.PerformLayout();
			ResumeLayout(false);
		}

		private void TextBoxValue1_TextChanged(object sender, EventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			textBox.Text = Regex.Match(textBox.Text, "[+-]?[0-9]*\\.?[0-9]*").ToString();
			textBox.Select(textBox.Text.Length, 0);
		}
	}
}
