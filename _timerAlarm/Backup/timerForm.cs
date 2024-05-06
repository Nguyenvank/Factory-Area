using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Threading;
using System.Timers;
using System.IO;
using System.Reflection;


namespace timerAlarm
{
	public class TimerForm : System.Windows.Forms.Form
	{
		private WaveLib.WaveOutPlayer m_Player;
		private WaveLib.WaveFormat m_Format;
		private Stream m_AudioStream; 
		private System.Windows.Forms.MenuItem menuItemReset;
		private System.Windows.Forms.MenuItem menuItemOpen;
		private System.Windows.Forms.TextBox timerInput;
		private System.Windows.Forms.Button StartButton;
		private System.Windows.Forms.Button ResetButton;
		private System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.ContextMenu notifyMenu;
		private System.ComponentModel.IContainer components;
		private System.Timers.Timer timerClock = new System.Timers.Timer();
		private int clockTime = 0;
		private int alarmTime = 0;

		public TimerForm()
		{
			InitializeComponent();
			InitializeTimer();
			InitializeSound();
			InitializeNotifyMenu();
		}

		protected override void Dispose( bool disposing )
		{
			CloseSound();

			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TimerForm));
			this.timerInput = new System.Windows.Forms.TextBox();
			this.StartButton = new System.Windows.Forms.Button();
			this.ResetButton = new System.Windows.Forms.Button();
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.notifyMenu = new System.Windows.Forms.ContextMenu();
			this.SuspendLayout();
			// 
			// timerInput
			// 
			this.timerInput.Location = new System.Drawing.Point(12, 13);
			this.timerInput.Name = "timerInput";
			this.timerInput.Size = new System.Drawing.Size(50, 20);
			this.timerInput.TabIndex = 0;
			this.timerInput.Text = "00:00:00";
			// 
			// StartButton
			// 
			this.StartButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.StartButton.Location = new System.Drawing.Point(75, 11);
			this.StartButton.Name = "StartButton";
			this.StartButton.TabIndex = 1;
			this.StartButton.Text = "Start";
			this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
			// 
			// ResetButton
			// 
			this.ResetButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.ResetButton.Location = new System.Drawing.Point(161, 11);
			this.ResetButton.Name = "ResetButton";
			this.ResetButton.TabIndex = 2;
			this.ResetButton.Text = "Reset";
			this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
			// 
			// notifyIcon
			// 
			this.notifyIcon.ContextMenu = this.notifyMenu;
			this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
			this.notifyIcon.Text = "Alarm Timer";
			this.notifyIcon.Visible = true;
			this.notifyIcon.DoubleClick += new System.EventHandler(this.menuItemOpen_Click);
			// 
			// TimerForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(247, 46);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.ResetButton,
																		  this.StartButton,
																		  this.timerInput});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "TimerForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Alarm Timer";
			this.Resize += new System.EventHandler(this.TimerForm_Resized);
			this.ResumeLayout(false);

		}
		#endregion

		public void InitializeTimer()
		{
			this.timerClock.Elapsed += new ElapsedEventHandler(OnTimer);
			this.timerClock.Interval = 1000;
			this.timerClock.Enabled = true;
		}

		private void InitializeSound()
		{
			try
			{
				// get a reference to the current assembly
				Assembly a = Assembly.GetExecutingAssembly();
				WaveLib.WaveStream S = new WaveLib.WaveStream( a.GetManifestResourceStream( "timerAlarm.alarm.wav" ) );
				if (S.Length <= 0)
					throw new Exception("Invalid WAV file");
				m_Format = S.Format;
				if (m_Format.wFormatTag != (short)WaveLib.WaveFormats.Pcm && m_Format.wFormatTag != (short)WaveLib.WaveFormats.Float)
					throw new Exception("Olny PCM files are supported");

				m_AudioStream = S;
			}
			catch(Exception e)
			{
				CloseSound();
				MessageBox.Show("InitializeSound(): " + e.Message );
			}
		}

		private void InitializeNotifyMenu()
		{
			try
			{
				this.menuItemReset = new MenuItem("Reset Alarm");
				menuItemReset.Click += new System.EventHandler(this.menuItemReset_Click);

				this.menuItemOpen = new MenuItem("Open Alarm Timer");
				menuItemOpen.Click += new System.EventHandler(this.menuItemOpen_Click);


				this.notifyMenu.MenuItems.Clear();
				this.notifyMenu.MenuItems.Add( this.menuItemOpen );
				this.notifyMenu.MenuItems.Add( this.menuItemReset );
			}
			catch(Exception e)
			{
				MessageBox.Show("InitializeNotifyMenu(): " + e.Message );
			}
		}

		[STAThread]
		static void Main() 
		{
			Application.Run(new TimerForm());
		}

		private void TimerForm_Resized(object sender, System.EventArgs e)
		{
			if( this.WindowState == FormWindowState.Minimized )
			{
				this.Hide();
			}
		}

		private void menuItemReset_Click(object sender, System.EventArgs e)
		{
			ResetButton_Click( null, null );
		}

		private void menuItemOpen_Click(object sender, System.EventArgs e)
		{
			this.Show();
			this.WindowState = FormWindowState.Normal;
		}

		private void StartButton_Click(object sender, System.EventArgs e)
		{
			this.clockTime = 0;
			inputToSeconds( this.timerInput.Text );
		}

		private void ResetButton_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.clockTime = 0;
				this.alarmTime = 0;
				this.timerInput.Text = "00:00:00";
				StopSound();
			}
			catch( Exception ex )
			{
				MessageBox.Show("ResetButton_Click(): " + ex.Message );
			}
		}

		public void OnTimer(Object source, ElapsedEventArgs e)
		{
			try
			{
				this.clockTime++;
				int countdown = this.alarmTime - this.clockTime ;
				if( this.alarmTime != 0 )
				{
					this.timerInput.Text = secondsToTime(countdown);
				}

				//Sound Alarm
				if( this.clockTime == this.alarmTime )
				{
					PlaySound();
				}
			}
			catch( Exception ex )
			{
				MessageBox.Show("OnTimer(): " + ex.Message );
			}		
		}

		private void inputToSeconds( string timerInput )
		{
			try
			{
				string[] timeArray = new string[3];
				int minutes = 0;
				int hours = 0;
				int seconds = 0;
				int occurence = 0;
				int length = 0;

				occurence = timerInput.LastIndexOf(":");
				length = timerInput.Length;

				//Check for invalid input
				if( occurence == -1 || length != 8 )
				{
					MessageBox.Show("Invalid Time Format.");
					ResetButton_Click( null, null );
				}
				else
				{
					timeArray = timerInput.Split(':');

					seconds = Convert.ToInt32( timeArray[2] );
					minutes = Convert.ToInt32( timeArray[1] );
					hours = Convert.ToInt32( timeArray[0] );

					this.alarmTime += seconds;
					this.alarmTime += minutes*60;
					this.alarmTime += (hours*60)*60;
				}
			}
			catch( Exception e )
			{
				MessageBox.Show("inputToSeconds(): " + e.Message );
			}
		}

		public string secondsToTime( int seconds )
		{
			int minutes = 0;
			int hours = 0;

			while( seconds >= 60 )
			{
				minutes += 1;
				seconds -= 60;
			}
			while( minutes >= 60 )
			{
				hours += 1;
				minutes -= 60;
			}

			string strHours = hours.ToString();
			string strMinutes = minutes.ToString();
			string strSeconds = seconds.ToString();

			if( strHours.Length < 2 )	strHours = "0" + strHours;
			if( strMinutes.Length < 2 )	strMinutes = "0" + strMinutes;
			if( strSeconds.Length < 2 )	strSeconds = "0" + strSeconds;

			return strHours + ":" + strMinutes + ":" + strSeconds;
		}

		private void CloseSound()
		{
			this.StopSound();
			if (m_AudioStream != null)
				try
				{
					m_AudioStream.Close();
				}
				finally
				{
					m_AudioStream = null;
				}
		}

		private void StopSound()
		{
			if (m_Player != null)
				try
				{
					m_Player.Dispose();
				}
				finally
				{
					m_Player = null;
				}
		}

		private void PlaySound()
		{
			this.StopSound();
			if (m_AudioStream != null)
			{
				m_AudioStream.Position = 0;
				m_Player = new WaveLib.WaveOutPlayer(-1, m_Format, 16384, 3, new WaveLib.BufferFillEventHandler(Filler));
			}
		}

		private void Filler(IntPtr data, int size)
		{
			byte[] b = new byte[size];
			if (m_AudioStream != null)
			{
				int pos = 0;
				while (pos < size)
				{
					int toget = size - pos;
					int got = m_AudioStream.Read(b, pos, toget);
					if (got < toget)
						m_AudioStream.Position = 0; // loop if the file ends
					pos += got;
				}
			}
			else
			{
				for (int i = 0; i < b.Length; i++)
					b[i] = 0;
			}
			System.Runtime.InteropServices.Marshal.Copy(b, 0, data, size);
		}

	}
}
