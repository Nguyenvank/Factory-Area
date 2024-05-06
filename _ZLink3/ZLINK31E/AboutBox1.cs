using Microsoft.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using ZLINK31E.My;
using ZLINK31E.My.Resources;

namespace ZLINK31E
{
	[DesignerGenerated]
	public sealed class AboutBox1 : Form
	{
		[AccessedThroughProperty("TableLayoutPanel")]
		private TableLayoutPanel _TableLayoutPanel;

		[AccessedThroughProperty("LabelProductName")]
		private Label _LabelProductName;

		[AccessedThroughProperty("LabelVersion")]
		private Label _LabelVersion;

		[AccessedThroughProperty("LabelCompanyName")]
		private Label _LabelCompanyName;

		[AccessedThroughProperty("TextBoxDescription")]
		private TextBox _TextBoxDescription;

		[AccessedThroughProperty("OKButton")]
		private Button _OKButton;

		[AccessedThroughProperty("LabelCopyright")]
		private Label _LabelCopyright;

		private IContainer components;

		[AccessedThroughProperty("Panel1")]
		private Panel _Panel1;

		[AccessedThroughProperty("PictureBox1")]
		private PictureBox _PictureBox1;

		[AccessedThroughProperty("LogoPictureBox")]
		private PictureBox _LogoPictureBox;

		internal virtual TableLayoutPanel TableLayoutPanel
		{
			[DebuggerNonUserCode]
			get
			{
				return _TableLayoutPanel;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_TableLayoutPanel = value;
			}
		}

		internal virtual Label LabelProductName
		{
			[DebuggerNonUserCode]
			get
			{
				return _LabelProductName;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_LabelProductName = value;
			}
		}

		internal virtual Label LabelVersion
		{
			[DebuggerNonUserCode]
			get
			{
				return _LabelVersion;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_LabelVersion = value;
			}
		}

		internal virtual Label LabelCompanyName
		{
			[DebuggerNonUserCode]
			get
			{
				return _LabelCompanyName;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_LabelCompanyName = value;
			}
		}

		internal virtual TextBox TextBoxDescription
		{
			[DebuggerNonUserCode]
			get
			{
				return _TextBoxDescription;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_TextBoxDescription = value;
			}
		}

		internal virtual Button OKButton
		{
			[DebuggerNonUserCode]
			get
			{
				return _OKButton;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_OKButton != null)
				{
					_OKButton.Click -= OKButton_Click;
				}
				_OKButton = value;
				if (_OKButton != null)
				{
					_OKButton.Click += OKButton_Click;
				}
			}
		}

		internal virtual Label LabelCopyright
		{
			[DebuggerNonUserCode]
			get
			{
				return _LabelCopyright;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_LabelCopyright = value;
			}
		}

		internal virtual Panel Panel1
		{
			[DebuggerNonUserCode]
			get
			{
				return _Panel1;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_Panel1 = value;
			}
		}

		internal virtual PictureBox PictureBox1
		{
			[DebuggerNonUserCode]
			get
			{
				return _PictureBox1;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_PictureBox1 = value;
			}
		}

		internal virtual PictureBox LogoPictureBox
		{
			[DebuggerNonUserCode]
			get
			{
				return _LogoPictureBox;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_LogoPictureBox = value;
			}
		}

		[DebuggerNonUserCode]
		public AboutBox1()
		{
			base.Load += AboutBox1_Load;
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
			TableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			LabelProductName = new System.Windows.Forms.Label();
			LabelVersion = new System.Windows.Forms.Label();
			LabelCopyright = new System.Windows.Forms.Label();
			LabelCompanyName = new System.Windows.Forms.Label();
			TextBoxDescription = new System.Windows.Forms.TextBox();
			OKButton = new System.Windows.Forms.Button();
			Panel1 = new System.Windows.Forms.Panel();
			PictureBox1 = new System.Windows.Forms.PictureBox();
			LogoPictureBox = new System.Windows.Forms.PictureBox();
			TableLayoutPanel.SuspendLayout();
			Panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)PictureBox1).BeginInit();
			((System.ComponentModel.ISupportInitialize)LogoPictureBox).BeginInit();
			SuspendLayout();
			TableLayoutPanel.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			TableLayoutPanel.BackColor = System.Drawing.Color.Transparent;
			TableLayoutPanel.ColumnCount = 1;
			TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100f));
			TableLayoutPanel.Controls.Add(LabelProductName, 0, 0);
			TableLayoutPanel.Controls.Add(LabelVersion, 0, 1);
			TableLayoutPanel.Controls.Add(LabelCopyright, 0, 2);
			TableLayoutPanel.Controls.Add(LabelCompanyName, 0, 3);
			TableLayoutPanel.Controls.Add(TextBoxDescription, 0, 4);
			TableLayoutPanel.Controls.Add(OKButton, 0, 5);
			System.Drawing.Point point2 = TableLayoutPanel.Location = new System.Drawing.Point(261, 14);
			System.Windows.Forms.Padding padding2 = TableLayoutPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			TableLayoutPanel.Name = "TableLayoutPanel";
			TableLayoutPanel.RowCount = 6;
			TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.04348f));
			TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.04348f));
			TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.04348f));
			TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.04348f));
			TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34.78261f));
			TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.04348f));
			System.Drawing.Size size2 = TableLayoutPanel.Size = new System.Drawing.Size(222, 266);
			TableLayoutPanel.TabIndex = 0;
			LabelProductName.Dock = System.Windows.Forms.DockStyle.Fill;
			LabelProductName.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			point2 = (LabelProductName.Location = new System.Drawing.Point(7, 0));
			padding2 = (LabelProductName.Margin = new System.Windows.Forms.Padding(7, 0, 3, 0));
			size2 = (LabelProductName.MaximumSize = new System.Drawing.Size(0, 20));
			LabelProductName.Name = "LabelProductName";
			size2 = (LabelProductName.Size = new System.Drawing.Size(212, 20));
			LabelProductName.TabIndex = 0;
			LabelProductName.Text = "製品名";
			LabelProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			LabelVersion.Dock = System.Windows.Forms.DockStyle.Fill;
			LabelVersion.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			point2 = (LabelVersion.Location = new System.Drawing.Point(7, 34));
			padding2 = (LabelVersion.Margin = new System.Windows.Forms.Padding(7, 0, 3, 0));
			size2 = (LabelVersion.MaximumSize = new System.Drawing.Size(0, 20));
			LabelVersion.Name = "LabelVersion";
			size2 = (LabelVersion.Size = new System.Drawing.Size(212, 20));
			LabelVersion.TabIndex = 0;
			LabelVersion.Text = "バ\u30fcジョン";
			LabelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			LabelCopyright.Dock = System.Windows.Forms.DockStyle.Fill;
			LabelCopyright.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			point2 = (LabelCopyright.Location = new System.Drawing.Point(7, 68));
			padding2 = (LabelCopyright.Margin = new System.Windows.Forms.Padding(7, 0, 3, 0));
			size2 = (LabelCopyright.MaximumSize = new System.Drawing.Size(0, 20));
			LabelCopyright.Name = "LabelCopyright";
			size2 = (LabelCopyright.Size = new System.Drawing.Size(212, 20));
			LabelCopyright.TabIndex = 0;
			LabelCopyright.Text = "著作権";
			LabelCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			LabelCompanyName.Dock = System.Windows.Forms.DockStyle.Fill;
			LabelCompanyName.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			point2 = (LabelCompanyName.Location = new System.Drawing.Point(7, 102));
			padding2 = (LabelCompanyName.Margin = new System.Windows.Forms.Padding(7, 0, 3, 0));
			size2 = (LabelCompanyName.MaximumSize = new System.Drawing.Size(0, 20));
			LabelCompanyName.Name = "LabelCompanyName";
			size2 = (LabelCompanyName.Size = new System.Drawing.Size(212, 20));
			LabelCompanyName.TabIndex = 0;
			LabelCompanyName.Text = "会社名";
			LabelCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			TextBoxDescription.Dock = System.Windows.Forms.DockStyle.Fill;
			TextBoxDescription.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			point2 = (TextBoxDescription.Location = new System.Drawing.Point(7, 140));
			padding2 = (TextBoxDescription.Margin = new System.Windows.Forms.Padding(7, 4, 3, 4));
			TextBoxDescription.Multiline = true;
			TextBoxDescription.Name = "TextBoxDescription";
			TextBoxDescription.ReadOnly = true;
			TextBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			size2 = (TextBoxDescription.Size = new System.Drawing.Size(212, 84));
			TextBoxDescription.TabIndex = 0;
			TextBoxDescription.TabStop = false;
			TextBoxDescription.Text = "説明:\r\n\r\n(ランタイムに、ラベルのテキストはアプリケ\u30fcションのアセンブリ情報に置き換えられます。\r\nプロジェクト デザイナの [アプリケ\u30fcション] ペインで、アプリケ\u30fcションのアセンブリ情報をカスタマイズします。)";
			TextBoxDescription.Visible = false;
			OKButton.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			OKButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			OKButton.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			point2 = (OKButton.Location = new System.Drawing.Point(132, 236));
			padding2 = (OKButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4));
			OKButton.Name = "OKButton";
			size2 = (OKButton.Size = new System.Drawing.Size(87, 26));
			OKButton.TabIndex = 0;
			OKButton.Text = "OK(&O)";
			Panel1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			Panel1.BackColor = System.Drawing.Color.Transparent;
			Panel1.Controls.Add(PictureBox1);
			Panel1.Controls.Add(LogoPictureBox);
			point2 = (Panel1.Location = new System.Drawing.Point(14, 12));
			padding2 = (Panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4));
			Panel1.Name = "Panel1";
			size2 = (Panel1.Size = new System.Drawing.Size(240, 268));
			Panel1.TabIndex = 2;
			PictureBox1.Image = ZLINK31E.My.Resources.Resources.ロゴ_ZLINK3;
			point2 = (PictureBox1.Location = new System.Drawing.Point(0, 42));
			padding2 = (PictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4));
			PictureBox1.Name = "PictureBox1";
			size2 = (PictureBox1.Size = new System.Drawing.Size(240, 80));
			PictureBox1.TabIndex = 4;
			PictureBox1.TabStop = false;
			LogoPictureBox.BackColor = System.Drawing.Color.Transparent;
			LogoPictureBox.Image = ZLINK31E.My.Resources.Resources.imadalogo;
			point2 = (LogoPictureBox.Location = new System.Drawing.Point(24, 150));
			padding2 = (LogoPictureBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4));
			LogoPictureBox.Name = "LogoPictureBox";
			size2 = (LogoPictureBox.Size = new System.Drawing.Size(191, 75));
			LogoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			LogoPictureBox.TabIndex = 3;
			LogoPictureBox.TabStop = false;
			System.Drawing.SizeF sizeF2 = AutoScaleDimensions = new System.Drawing.SizeF(7f, 15f);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackgroundImage = ZLINK31E.My.Resources.Resources.relf3;
			size2 = (ClientSize = new System.Drawing.Size(497, 294));
			Controls.Add(Panel1);
			Controls.Add(TableLayoutPanel);
			Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			padding2 = (Margin = new System.Windows.Forms.Padding(3, 4, 3, 4));
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "AboutBox1";
			padding2 = (Padding = new System.Windows.Forms.Padding(10));
			ShowInTaskbar = false;
			StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "ZLINK3 - About";
			TableLayoutPanel.ResumeLayout(false);
			TableLayoutPanel.PerformLayout();
			Panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)PictureBox1).EndInit();
			((System.ComponentModel.ISupportInitialize)LogoPictureBox).EndInit();
			ResumeLayout(false);
		}

		private void AboutBox1_Load(object sender, EventArgs e)
		{
			string arg = (Operators.CompareString(MyProject.Application.Info.Title, "", TextCompare: false) == 0) ? Path.GetFileNameWithoutExtension(MyProject.Application.Info.AssemblyName) : MyProject.Application.Info.Title;
			Text = $"About {arg}";
			LabelProductName.Text = MyProject.Application.Info.ProductName;
			LabelVersion.Text = $"Ver. {MyProject.Application.Info.Version.ToString()}e";
			LabelCopyright.Text = MyProject.Application.Info.Copyright;
			LabelCompanyName.Text = MyProject.Application.Info.CompanyName;
			TextBoxDescription.Text = "Force Analizing Software\rwith RS-232C interface.";
		}

		private void OKButton_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
