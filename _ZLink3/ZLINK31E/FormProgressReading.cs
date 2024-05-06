using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace ZLINK31E
{
	[DesignerGenerated]
	public class FormProgressReading : Form
	{
		private IContainer components;

		[AccessedThroughProperty("ProgressBar1")]
		private ProgressBar _ProgressBar1;

		[AccessedThroughProperty("Button1")]
		private Button _Button1;

		[AccessedThroughProperty("BackgroundWorker1")]
		private BackgroundWorker _BackgroundWorker1;

		private DataTable temp_table;

		private string file_path;

		private string unit_str;

		private uint timer_interval;

		internal virtual ProgressBar ProgressBar1
		{
			[DebuggerNonUserCode]
			get
			{
				return _ProgressBar1;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_ProgressBar1 = value;
			}
		}

		internal virtual Button Button1
		{
			[DebuggerNonUserCode]
			get
			{
				return _Button1;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_Button1 != null)
				{
					_Button1.Click -= Button1_Click;
				}
				_Button1 = value;
				if (_Button1 != null)
				{
					_Button1.Click += Button1_Click;
				}
			}
		}

		internal virtual BackgroundWorker BackgroundWorker1
		{
			[DebuggerNonUserCode]
			get
			{
				return _BackgroundWorker1;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_BackgroundWorker1 != null)
				{
					_BackgroundWorker1.RunWorkerCompleted -= BackgroundWorker1_RunWorkerCompleted;
					_BackgroundWorker1.ProgressChanged -= BackgroundWorker1_ProgressChanged;
					_BackgroundWorker1.DoWork -= BackgroundWorker1_DoWork;
				}
				_BackgroundWorker1 = value;
				if (_BackgroundWorker1 != null)
				{
					_BackgroundWorker1.RunWorkerCompleted += BackgroundWorker1_RunWorkerCompleted;
					_BackgroundWorker1.ProgressChanged += BackgroundWorker1_ProgressChanged;
					_BackgroundWorker1.DoWork += BackgroundWorker1_DoWork;
				}
			}
		}

		public DataTable Result => temp_table;

		public string UnitStr => unit_str;

		public uint TimerInterval => timer_interval;

		[DebuggerNonUserCode]
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && components != null)
				{
					components.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		[System.Diagnostics.DebuggerStepThrough]
		private void InitializeComponent()
		{
			ProgressBar1 = new System.Windows.Forms.ProgressBar();
			Button1 = new System.Windows.Forms.Button();
			BackgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			SuspendLayout();
			System.Drawing.Point point2 = ProgressBar1.Location = new System.Drawing.Point(12, 12);
			ProgressBar1.Name = "ProgressBar1";
			System.Drawing.Size size2 = ProgressBar1.Size = new System.Drawing.Size(260, 26);
			ProgressBar1.TabIndex = 0;
			Button1.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			point2 = (Button1.Location = new System.Drawing.Point(197, 54));
			Button1.Name = "Button1";
			size2 = (Button1.Size = new System.Drawing.Size(75, 23));
			Button1.TabIndex = 1;
			Button1.Text = "Abort";
			Button1.UseVisualStyleBackColor = true;
			Button1.Visible = false;
			BackgroundWorker1.WorkerReportsProgress = true;
			BackgroundWorker1.WorkerSupportsCancellation = true;
			System.Drawing.SizeF sizeF2 = AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			size2 = (ClientSize = new System.Drawing.Size(291, 96));
			ControlBox = false;
			Controls.Add(Button1);
			Controls.Add(ProgressBar1);
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			Name = "FormProgressReading";
			StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Processing...";
			ResumeLayout(false);
		}

		public FormProgressReading(ref DataTable original, string filename)
		{
			base.Load += FormProgressReading_Load;
			temp_table = original.Clone();
			file_path = filename;
			InitializeComponent();
		}

		private void FormProgressReading_Load(object sender, EventArgs e)
		{
			int num = 0;
			try
			{
				StreamReader streamReader = new StreamReader(file_path);
				while (!streamReader.EndOfStream)
				{
					streamReader.ReadLine();
					num = checked(num + 1);
				}
				streamReader.Close();
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				DialogResult = DialogResult.No;
				Close();
				ProjectData.ClearProjectError();
				return;
			}
			ProgressBar1.Maximum = num;
			BackgroundWorker1.RunWorkerAsync();
		}

		private void Button1_Click(object sender, EventArgs e)
		{
			BackgroundWorker1.CancelAsync();
		}

		private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			//Discarded unreachable code: IL_012f
			int num = 0;
			int num2 = 0;
			checked
			{
				try
				{
					int num3 = 0;
					using (StreamReader streamReader = new StreamReader(file_path))
					{
						string text = streamReader.ReadLine();
						string[] array = text.Split(',');
						unit_str = array[0];
						text = streamReader.ReadLine();
						string[] array2 = text.Split(',');
						timer_interval = (uint)Math.Round(Conversion.Val(array2[0]));
						while (streamReader.Peek() > 0)
						{
							num3++;
							BackgroundWorker1.ReportProgress(0, num3);
							try
							{
								DataRow dataRow = temp_table.NewRow();
								string text2 = streamReader.ReadLine();
								string[] array3 = text2.Split(',');
								dataRow[0] = array3[0];
								dataRow[1] = unit_str;
								temp_table.Rows.Add(dataRow);
								num2++;
							}
							catch (Exception ex)
							{
								ProjectData.SetProjectError(ex);
								Exception ex2 = ex;
								num++;
								ProjectData.ClearProjectError();
							}
							if (BackgroundWorker1.CancellationPending)
							{
								DialogResult = DialogResult.Abort;
								return;
							}
						}
					}
				}
				catch (Exception projectError)
				{
					ProjectData.SetProjectError(projectError);
					DialogResult = DialogResult.No;
					ProjectData.ClearProjectError();
					return;
				}
				DialogResult = DialogResult.OK;
			}
		}

		private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			ProgressBar1.Value = Conversions.ToInteger(e.UserState);
		}

		private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			Close();
		}
	}
}
