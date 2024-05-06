using CommonZlinkLib.ForceGauge;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace ZLINK31E
{
	[DesignerGenerated]
	public class FormSelectPort : Form
	{
		private IContainer components;

		[AccessedThroughProperty("ButtonOK")]
		private Button _ButtonOK;

		[AccessedThroughProperty("ButtonCancel")]
		private Button _ButtonCancel;

		[AccessedThroughProperty("SerialPort1")]
		private SerialPort _SerialPort1;

		[AccessedThroughProperty("Label2")]
		private Label _Label2;

		[AccessedThroughProperty("GroupBox1")]
		private GroupBox _GroupBox1;

		[AccessedThroughProperty("LabelDPX")]
		private Label _LabelDPX;

		[AccessedThroughProperty("LabelZ")]
		private Label _LabelZ;

		[AccessedThroughProperty("LabelDPZ")]
		private Label _LabelDPZ;

		[AccessedThroughProperty("LabelDS2")]
		private Label _LabelDS2;

		[AccessedThroughProperty("LabelDPS")]
		private Label _LabelDPS;

		[AccessedThroughProperty("Label1")]
		private Label _Label1;

		[AccessedThroughProperty("ListBoxPorts")]
		private ListBox _ListBoxPorts;

		private string thePortName;

		public GaugeCompatibility Compatibility;

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

		internal virtual SerialPort SerialPort1
		{
			[DebuggerNonUserCode]
			get
			{
				return _SerialPort1;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_SerialPort1 = value;
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

		internal virtual Label LabelDPX
		{
			[DebuggerNonUserCode]
			get
			{
				return _LabelDPX;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_LabelDPX = value;
			}
		}

		internal virtual Label LabelZ
		{
			[DebuggerNonUserCode]
			get
			{
				return _LabelZ;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_LabelZ = value;
			}
		}

		internal virtual Label LabelDPZ
		{
			[DebuggerNonUserCode]
			get
			{
				return _LabelDPZ;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_LabelDPZ = value;
			}
		}

		internal virtual Label LabelDS2
		{
			[DebuggerNonUserCode]
			get
			{
				return _LabelDS2;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_LabelDS2 = value;
			}
		}

		internal virtual Label LabelDPS
		{
			[DebuggerNonUserCode]
			get
			{
				return _LabelDPS;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_LabelDPS = value;
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

		internal virtual ListBox ListBoxPorts
		{
			[DebuggerNonUserCode]
			get
			{
				return _ListBoxPorts;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_ListBoxPorts != null)
				{
					_ListBoxPorts.SelectedIndexChanged -= ListBoxPorts_SelectedIndexChanged;
				}
				_ListBoxPorts = value;
				if (_ListBoxPorts != null)
				{
					_ListBoxPorts.SelectedIndexChanged += ListBoxPorts_SelectedIndexChanged;
				}
			}
		}

		public string PortName => thePortName;

		public FormSelectPort()
		{
			base.Load += FormSelectPort_Load;
			thePortName = null;
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
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ZLINK31E.FormSelectPort));
			ButtonOK = new System.Windows.Forms.Button();
			ButtonCancel = new System.Windows.Forms.Button();
			SerialPort1 = new System.IO.Ports.SerialPort(components);
			Label2 = new System.Windows.Forms.Label();
			GroupBox1 = new System.Windows.Forms.GroupBox();
			LabelDPZ = new System.Windows.Forms.Label();
			LabelDS2 = new System.Windows.Forms.Label();
			LabelDPS = new System.Windows.Forms.Label();
			LabelDPX = new System.Windows.Forms.Label();
			LabelZ = new System.Windows.Forms.Label();
			Label1 = new System.Windows.Forms.Label();
			ListBoxPorts = new System.Windows.Forms.ListBox();
			GroupBox1.SuspendLayout();
			SuspendLayout();
			ButtonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			ButtonOK.Enabled = false;
			ButtonOK.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			System.Drawing.Point point2 = ButtonOK.Location = new System.Drawing.Point(253, 255);
			System.Windows.Forms.Padding padding2 = ButtonOK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			ButtonOK.Name = "ButtonOK";
			System.Drawing.Size size2 = ButtonOK.Size = new System.Drawing.Size(87, 29);
			ButtonOK.TabIndex = 1;
			ButtonOK.Text = "Connect";
			ButtonOK.UseVisualStyleBackColor = true;
			ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			ButtonCancel.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			point2 = (ButtonCancel.Location = new System.Drawing.Point(347, 255));
			padding2 = (ButtonCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4));
			ButtonCancel.Name = "ButtonCancel";
			size2 = (ButtonCancel.Size = new System.Drawing.Size(87, 29));
			ButtonCancel.TabIndex = 1;
			ButtonCancel.Text = "Cancel";
			ButtonCancel.UseVisualStyleBackColor = true;
			Label2.AutoSize = true;
			point2 = (Label2.Location = new System.Drawing.Point(19, 218));
			Label2.Name = "Label2";
			size2 = (Label2.Size = new System.Drawing.Size(59, 15));
			Label2.TabIndex = 2;
			Label2.Text = "Com Port";
			GroupBox1.BackColor = System.Drawing.Color.White;
			GroupBox1.Controls.Add(LabelDPZ);
			GroupBox1.Controls.Add(LabelDS2);
			GroupBox1.Controls.Add(LabelDPS);
			GroupBox1.Controls.Add(LabelDPX);
			GroupBox1.Controls.Add(LabelZ);
			GroupBox1.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			point2 = (GroupBox1.Location = new System.Drawing.Point(205, 54));
			padding2 = (GroupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4));
			GroupBox1.Name = "GroupBox1";
			padding2 = (GroupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4));
			size2 = (GroupBox1.Size = new System.Drawing.Size(229, 184));
			GroupBox1.TabIndex = 3;
			GroupBox1.TabStop = false;
			GroupBox1.Text = "Model";
			LabelDPZ.AutoSize = true;
			LabelDPZ.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			LabelDPZ.ForeColor = System.Drawing.Color.Silver;
			LabelDPZ.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			point2 = (LabelDPZ.Location = new System.Drawing.Point(14, 88));
			LabelDPZ.Name = "LabelDPZ";
			size2 = (LabelDPZ.Size = new System.Drawing.Size(151, 15));
			LabelDPZ.TabIndex = 3;
			LabelDPZ.Text = "DPZ Series or Compatible";
			LabelDS2.AutoSize = true;
			LabelDS2.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			LabelDS2.ForeColor = System.Drawing.Color.Silver;
			LabelDS2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			point2 = (LabelDS2.Location = new System.Drawing.Point(14, 59));
			LabelDS2.Name = "LabelDS2";
			size2 = (LabelDS2.Size = new System.Drawing.Size(151, 15));
			LabelDS2.TabIndex = 3;
			LabelDS2.Text = "DS2 Series or Compatible";
			LabelDPS.AutoSize = true;
			LabelDPS.Font = new System.Drawing.Font("MS UI Gothic", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			LabelDPS.ForeColor = System.Drawing.Color.Silver;
			LabelDPS.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			point2 = (LabelDPS.Location = new System.Drawing.Point(14, 145));
			LabelDPS.Name = "LabelDPS";
			size2 = (LabelDPS.Size = new System.Drawing.Size(73, 12));
			LabelDPS.TabIndex = 3;
			LabelDPS.Text = "DPS Series";
			LabelDPX.AutoSize = true;
			LabelDPX.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			LabelDPX.ForeColor = System.Drawing.Color.Silver;
			LabelDPX.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			point2 = (LabelDPX.Location = new System.Drawing.Point(14, 116));
			LabelDPX.Name = "LabelDPX";
			size2 = (LabelDPX.Size = new System.Drawing.Size(151, 15));
			LabelDPX.TabIndex = 3;
			LabelDPX.Text = "DPX Series or Compatible";
			LabelZ.AutoSize = true;
			LabelZ.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			LabelZ.ForeColor = System.Drawing.Color.Silver;
			LabelZ.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			point2 = (LabelZ.Location = new System.Drawing.Point(14, 29));
			LabelZ.Name = "LabelZ";
			size2 = (LabelZ.Size = new System.Drawing.Size(134, 15));
			LabelZ.TabIndex = 2;
			LabelZ.Text = "Z Series or Compatible";
			Label1.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			point2 = (Label1.Location = new System.Drawing.Point(14, 15));
			Label1.Name = "Label1";
			size2 = (Label1.Size = new System.Drawing.Size(430, 35));
			Label1.TabIndex = 4;
			Label1.Text = "Turn your instrument on before selecting Comm port";
			ListBoxPorts.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			ListBoxPorts.FormattingEnabled = true;
			ListBoxPorts.ItemHeight = 15;
			point2 = (ListBoxPorts.Location = new System.Drawing.Point(16, 54));
			padding2 = (ListBoxPorts.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4));
			ListBoxPorts.Name = "ListBoxPorts";
			size2 = (ListBoxPorts.Size = new System.Drawing.Size(181, 244));
			ListBoxPorts.TabIndex = 5;
			AcceptButton = ButtonOK;
			System.Drawing.SizeF sizeF2 = AutoScaleDimensions = new System.Drawing.SizeF(7f, 15f);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			CancelButton = ButtonCancel;
			size2 = (ClientSize = new System.Drawing.Size(447, 327));
			ControlBox = false;
			Controls.Add(ListBoxPorts);
			Controls.Add(Label1);
			Controls.Add(GroupBox1);
			Controls.Add(Label2);
			Controls.Add(ButtonCancel);
			Controls.Add(ButtonOK);
			Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			padding2 = (Margin = new System.Windows.Forms.Padding(3, 4, 3, 4));
			Name = "FormSelectPort";
			ShowInTaskbar = false;
			StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			Text = "IMADA ZLINK3 - Select Port";
			TopMost = true;
			GroupBox1.ResumeLayout(false);
			GroupBox1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}

		private void FormSelectPort_Load(object sender, EventArgs e)
		{
			SerialPort serialPort = SerialPort1;
			serialPort.BaudRate = 2400;
			serialPort.DataBits = 8;
			serialPort.NewLine = "\r";
			serialPort.Parity = Parity.None;
			serialPort.StopBits = StopBits.One;
			serialPort.Handshake = Handshake.None;
			serialPort.ReadBufferSize = 1024;
			serialPort.ReadTimeout = 500;
			serialPort.WriteTimeout = 100;
			serialPort.WriteBufferSize = 1024;
			serialPort.ReceivedBytesThreshold = 1;
			serialPort = null;
			UseWaitCursor = true;
			string[] portNames = SerialPort.GetPortNames();
			foreach (string text in portNames)
			{
				try
				{
					SerialPort1.PortName = text;
					SerialPort1.Open();
					if (SerialPort1.IsOpen)
					{
						ListBoxPorts.Items.Add(text);
					}
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					ProjectData.ClearProjectError();
				}
				finally
				{
					SerialPort1.Close();
				}
			}
			UseWaitCursor = false;
		}

		private void ListBoxPorts_SelectedIndexChanged(object sender, EventArgs e)
		{
			Cursor cursor = Cursor;
			Cursor = Cursors.WaitCursor;
			try
			{
				thePortName = ListBoxPorts.SelectedItem.ToString();
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				Cursor = cursor;
				ProjectData.ClearProjectError();
				return;
			}
			LabelDPS.ForeColor = Color.Silver;
			LabelDPX.ForeColor = Color.Silver;
			LabelDPZ.ForeColor = Color.Silver;
			LabelDS2.ForeColor = Color.Silver;
			LabelZ.ForeColor = Color.Silver;
			SerialPort serialPort = SerialPort1;
			serialPort.PortName = thePortName;
			serialPort.BaudRate = 2400;
			try
			{
				serialPort.Open();
				serialPort.WriteLine("d");
				string text = serialPort.ReadLine();
				serialPort.DiscardInBuffer();
				serialPort.DiscardOutBuffer();
				serialPort.WriteLine("C");
				text = serialPort.ReadLine();
				if (Operators.CompareString(text, "E", TextCompare: false) == 0)
				{
					Compatibility = GaugeCompatibility.Dps;
					LabelDPS.ForeColor = Color.Black;
					ButtonOK.Enabled = true;
					Cursor = cursor;
					return;
				}
				if (text.Length >= 1)
				{
					Compatibility = GaugeCompatibility.Dpx;
					LabelDPX.ForeColor = Color.Black;
					ButtonOK.Enabled = true;
					Cursor = cursor;
					return;
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				ProjectData.ClearProjectError();
			}
			finally
			{
				serialPort.Close();
			}
			serialPort.BaudRate = 19200;
			try
			{
				serialPort.Open();
				serialPort.WriteLine("d");
				string text2 = serialPort.ReadLine();
				serialPort.DiscardInBuffer();
				serialPort.DiscardOutBuffer();
				serialPort.WriteLine("RF?");
				text2 = serialPort.ReadLine();
				if (text2.StartsWith("Z"))
				{
					Compatibility = GaugeCompatibility.Z;
					LabelZ.ForeColor = Color.Black;
					ButtonOK.Enabled = true;
				}
				else if (text2.StartsWith("S"))
				{
					Compatibility = GaugeCompatibility.Ds2;
					LabelDS2.ForeColor = Color.Black;
					ButtonOK.Enabled = true;
				}
				else
				{
					Compatibility = GaugeCompatibility.Dpz;
					LabelDPZ.ForeColor = Color.Black;
					ButtonOK.Enabled = true;
				}
			}
			catch (Exception ex3)
			{
				ProjectData.SetProjectError(ex3);
				Exception ex4 = ex3;
				Compatibility = GaugeCompatibility.Dpx;
				ProjectData.ClearProjectError();
			}
			finally
			{
				serialPort.Close();
			}
			serialPort = null;
			Cursor = cursor;
		}
	}
}
