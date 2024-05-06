using CommonZlinkLib;
using CommonZlinkLib.ForceGauge;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using ZLINK31E.My;
using ZLINK31E.My.Resources;

namespace ZLINK31E
{
	[DesignerGenerated]
	public class FormMain : Form
	{
		private delegate void DelegateUpdate(string message);

		private delegate void updateMonitorDelegate();

		private IContainer components;

		[AccessedThroughProperty("TabControlMain")]
		private TabControl _TabControlMain;

		[AccessedThroughProperty("TabPageIndependent")]
		private TabPage _TabPageIndependent;

		[AccessedThroughProperty("TabPageRealTime")]
		private TabPage _TabPageRealTime;

		[AccessedThroughProperty("MenuStripMain")]
		private MenuStrip _MenuStripMain;

		[AccessedThroughProperty("ToolStripMenuItemFile")]
		private ToolStripMenuItem _ToolStripMenuItemFile;

		[AccessedThroughProperty("ToolStripMenuItemQuit")]
		private ToolStripMenuItem _ToolStripMenuItemQuit;

		[AccessedThroughProperty("ToolStripMenuItemSoftwareConfigMenu")]
		private ToolStripMenuItem _ToolStripMenuItemSoftwareConfigMenu;

		[AccessedThroughProperty("ToolStripMenuItemConnection")]
		private ToolStripMenuItem _ToolStripMenuItemConnection;

		[AccessedThroughProperty("ToolStripMenuItemAuto")]
		private ToolStripMenuItem _ToolStripMenuItemAuto;

		[AccessedThroughProperty("ToolStripMenuItemHelp")]
		private ToolStripMenuItem _ToolStripMenuItemHelp;

		[AccessedThroughProperty("ToolStripMenuItemHelpContents")]
		private ToolStripMenuItem _ToolStripMenuItemHelpContents;

		[AccessedThroughProperty("ToolStripMenuItemAbout")]
		private ToolStripMenuItem _ToolStripMenuItemAbout;

		[AccessedThroughProperty("DataGridViewValue")]
		private DataGridView _DataGridViewValue;

		[AccessedThroughProperty("DataSetValue")]
		private DataSet _DataSetValue;

		[AccessedThroughProperty("DataTableValue")]
		private DataTable _DataTableValue;

		[AccessedThroughProperty("DataColumnValue")]
		private DataColumn _DataColumnValue;

		[AccessedThroughProperty("ToolStripStatusLabelConnection")]
		private ToolStripStatusLabel _ToolStripStatusLabelConnection;

		[AccessedThroughProperty("StatusStripMain")]
		private StatusStrip _StatusStripMain;

		[AccessedThroughProperty("ToolStrip2")]
		private ToolStrip _ToolStrip2;

		[AccessedThroughProperty("ToolStripButtonFirst1")]
		private ToolStripButton _ToolStripButtonFirst1;

		[AccessedThroughProperty("ToolStripButtonGet")]
		private ToolStripButton _ToolStripButtonGet;

		[AccessedThroughProperty("ToolStripButtonMemoryRequest")]
		private ToolStripButton _ToolStripButtonMemoryRequest;

		[AccessedThroughProperty("ToolStripButtonClear1")]
		private ToolStripButton _ToolStripButtonClear1;

		[AccessedThroughProperty("ToolStripButton1")]
		private ToolStripButton _ToolStripButton1;

		[AccessedThroughProperty("ToolStripButton2")]
		private ToolStripButton _ToolStripButton2;

		[AccessedThroughProperty("ToolStripButton3")]
		private ToolStripButton _ToolStripButton3;

		[AccessedThroughProperty("ToolStripButton4")]
		private ToolStripButton _ToolStripButton4;

		[AccessedThroughProperty("ToolStripButtonMemoryClear")]
		private ToolStripButton _ToolStripButtonMemoryClear;

		[AccessedThroughProperty("ToolStripMenuItemSave")]
		private ToolStripMenuItem _ToolStripMenuItemSave;

		[AccessedThroughProperty("ImageList1")]
		private ImageList _ImageList1;

		[AccessedThroughProperty("DataColumnUnit")]
		private DataColumn _DataColumnUnit;

		[AccessedThroughProperty("ToolStripMenuItemPrint")]
		private ToolStripMenuItem _ToolStripMenuItemPrint;

		[AccessedThroughProperty("ToolStripMenuItemSaveCSV")]
		private ToolStripMenuItem _ToolStripMenuItemSaveCSV;

		[AccessedThroughProperty("SaveFileDialogCSV")]
		private SaveFileDialog _SaveFileDialogCSV;

		[AccessedThroughProperty("ToolStripDropDownButtonSaveCsv")]
		private ToolStripDropDownButton _ToolStripDropDownButtonSaveCsv;

		[AccessedThroughProperty("ToolStripMenuItemCsvSave")]
		private ToolStripMenuItem _ToolStripMenuItemCsvSave;

		[AccessedThroughProperty("ToolStripMenuItemCsvAdd")]
		private ToolStripMenuItem _ToolStripMenuItemCsvAdd;

		[AccessedThroughProperty("OpenFileDialogCsvAdd")]
		private OpenFileDialog _OpenFileDialogCsvAdd;

		[AccessedThroughProperty("ToolStripStatusLabelModel")]
		private ToolStripStatusLabel _ToolStripStatusLabelModel;

		[AccessedThroughProperty("ToolStripSeparator1")]
		private ToolStripSeparator _ToolStripSeparator1;

		[AccessedThroughProperty("ToolStripMenuItemAddCsv")]
		private ToolStripMenuItem _ToolStripMenuItemAddCsv;

		[AccessedThroughProperty("ToolStripMenuItemGaugeConfig")]
		private ToolStripMenuItem _ToolStripMenuItemGaugeConfig;

		[AccessedThroughProperty("ToolStripMenuItemConfig")]
		private ToolStripMenuItem _ToolStripMenuItemConfig;

		[AccessedThroughProperty("ToolStripStatusLabelStatus")]
		private ToolStripStatusLabel _ToolStripStatusLabelStatus;

		[AccessedThroughProperty("ContextMenuStripUpDown")]
		private ContextMenuStrip _ContextMenuStripUpDown;

		[AccessedThroughProperty("ToolStripMenuItemUpDown")]
		private ToolStripMenuItem _ToolStripMenuItemUpDown;

		[AccessedThroughProperty("ToolStripMenuItemOpen")]
		private ToolStripMenuItem _ToolStripMenuItemOpen;

		[AccessedThroughProperty("OpenFileDialogCsvRead")]
		private OpenFileDialog _OpenFileDialogCsvRead;

		[AccessedThroughProperty("ButtonTrackI")]
		private Button _ButtonTrackI;

		[AccessedThroughProperty("ButtonZeroI")]
		private Button _ButtonZeroI;

		[AccessedThroughProperty("ButtonPeakI")]
		private Button _ButtonPeakI;

		[AccessedThroughProperty("Panel1")]
		private Panel _Panel1;

		[AccessedThroughProperty("ButtonGet")]
		private Button _ButtonGet;

		[AccessedThroughProperty("Label1")]
		private Label _Label1;

		[AccessedThroughProperty("Label2")]
		private Label _Label2;

		[AccessedThroughProperty("LabelNumber")]
		private Label _LabelNumber;

		[AccessedThroughProperty("Label3")]
		private Label _Label3;

		[AccessedThroughProperty("LabelMinimum")]
		private Label _LabelMinimum;

		[AccessedThroughProperty("Label4")]
		private Label _Label4;

		[AccessedThroughProperty("LabelMaximum")]
		private Label _LabelMaximum;

		[AccessedThroughProperty("LabelAverage")]
		private Label _LabelAverage;

		[AccessedThroughProperty("BackgroundWorkerRealTime")]
		private BackgroundWorker _BackgroundWorkerRealTime;

		[AccessedThroughProperty("PrintDocument1")]
		private PrintDocument _PrintDocument1;

		[AccessedThroughProperty("ToolStripMenuItemPreview")]
		private ToolStripMenuItem _ToolStripMenuItemPreview;

		[AccessedThroughProperty("PrintPreviewDialog1")]
		private PrintPreviewDialog _PrintPreviewDialog1;

		[AccessedThroughProperty("ToolStrip1")]
		private ToolStrip _ToolStrip1;

		[AccessedThroughProperty("ToolStripButtonStart")]
		private ToolStripButton _ToolStripButtonStart;

		[AccessedThroughProperty("ToolStripButtonStop")]
		private ToolStripButton _ToolStripButtonStop;

		[AccessedThroughProperty("ToolStripSeparator4")]
		private ToolStripSeparator _ToolStripSeparator4;

		[AccessedThroughProperty("ToolStripButtonFirst")]
		private ToolStripButton _ToolStripButtonFirst;

		[AccessedThroughProperty("ToolStripButtonAll")]
		private ToolStripButton _ToolStripButtonAll;

		[AccessedThroughProperty("ToolStripComboBoxYaxis")]
		private ToolStripComboBox _ToolStripComboBoxYaxis;

		[AccessedThroughProperty("ToolStripComboBoxXaxis")]
		private ToolStripComboBox _ToolStripComboBoxXaxis;

		[AccessedThroughProperty("ToolStripSeparator2")]
		private ToolStripSeparator _ToolStripSeparator2;

		[AccessedThroughProperty("ToolStripButtonPlus")]
		private ToolStripButton _ToolStripButtonPlus;

		[AccessedThroughProperty("ToolStripButtonBoth")]
		private ToolStripButton _ToolStripButtonBoth;

		[AccessedThroughProperty("ToolStripButtonMinus")]
		private ToolStripButton _ToolStripButtonMinus;

		[AccessedThroughProperty("ToolStripButtonReverse")]
		private ToolStripButton _ToolStripButtonReverse;

		[AccessedThroughProperty("ToolStripSeparator3")]
		private ToolStripSeparator _ToolStripSeparator3;

		[AccessedThroughProperty("ToolStripButtonClear")]
		private ToolStripButton _ToolStripButtonClear;

		[AccessedThroughProperty("Panel2")]
		private Panel _Panel2;

		[AccessedThroughProperty("Panel3")]
		private Panel _Panel3;

		[AccessedThroughProperty("GroupBox1")]
		private GroupBox _GroupBox1;

		[AccessedThroughProperty("TableLayoutPanel2")]
		private TableLayoutPanel _TableLayoutPanel2;

		[AccessedThroughProperty("Label7")]
		private Label _Label7;

		[AccessedThroughProperty("LabelTrigerTitle")]
		private Label _LabelTrigerTitle;

		[AccessedThroughProperty("LabelTriggerValue")]
		private Label _LabelTriggerValue;

		[AccessedThroughProperty("LabelInterval")]
		private Label _LabelInterval;

		[AccessedThroughProperty("LabelPeak")]
		private Label _LabelPeak;

		[AccessedThroughProperty("ButtonTrack")]
		private Button _ButtonTrack;

		[AccessedThroughProperty("LabelUnit")]
		private Label _LabelUnit;

		[AccessedThroughProperty("ButtonZero")]
		private Button _ButtonZero;

		[AccessedThroughProperty("ButtonPeak")]
		private Button _ButtonPeak;

		[AccessedThroughProperty("LabelValue")]
		private Label _LabelValue;

		[AccessedThroughProperty("PictureBoxChart")]
		private PictureBox _PictureBoxChart;

		[AccessedThroughProperty("PanelCorner")]
		private Panel _PanelCorner;

		[AccessedThroughProperty("PictureBoxScaleY")]
		private PictureBox _PictureBoxScaleY;

		[AccessedThroughProperty("PictureBoxScaleX")]
		private PictureBox _PictureBoxScaleX;

		[AccessedThroughProperty("ToolStripMenuItemPrintConfig")]
		private ToolStripMenuItem _ToolStripMenuItemPrintConfig;

		[AccessedThroughProperty("PrintDialog1")]
		private PrintDialog _PrintDialog1;

		[AccessedThroughProperty("Label5")]
		private Label _Label5;

		[AccessedThroughProperty("ToolStripStatusLabelWaiting")]
		private ToolStripStatusLabel _ToolStripStatusLabelWaiting;

		[AccessedThroughProperty("ToolTip1")]
		private ToolTip _ToolTip1;

		[AccessedThroughProperty("ToolStripSeparator5")]
		private ToolStripSeparator _ToolStripSeparator5;

		[AccessedThroughProperty("ToolStripLabel1")]
		private ToolStripLabel _ToolStripLabel1;

		[AccessedThroughProperty("ToolStripSeparator6")]
		private ToolStripSeparator _ToolStripSeparator6;

		[AccessedThroughProperty("ToolStripLabel2")]
		private ToolStripLabel _ToolStripLabel2;

		[AccessedThroughProperty("ValueDataGridViewTextBoxColumn")]
		private DataGridViewTextBoxColumn _ValueDataGridViewTextBoxColumn;

		[AccessedThroughProperty("Unit")]
		private DataGridViewTextBoxColumn _Unit;

		[AccessedThroughProperty("LabelFinishTime")]
		private Label _LabelFinishTime;

		[AccessedThroughProperty("LabelFinishTimeTitle")]
		private Label _LabelFinishTimeTitle;

		[AccessedThroughProperty("LabelProcessing")]
		private Label _LabelProcessing;

		[AccessedThroughProperty("gauge")]
		private AbstractForceGauge _gauge;

		private Dictionary<string, string> m_unit_strings;

		private Statistics stats;

		private DelegateUpdate update_method;

		private string[] unit_strings;

		private AutoRecordConfig autoConfig;

		private ulong finishTimeCounter;

		private ulong finishCounterLimit;

		private string TempFilePath;

		private const uint MONITOR_UPDATE_RATE = 100u;

		[AccessedThroughProperty("timerMonitor")]
		private MultimediaTimer _timerMonitor;

		[AccessedThroughProperty("timerGet")]
		private MultimediaTimer _timerGet;

		private ScaleInfo currentScale;

		private Stack<ScaleInfo> scaleHistory;

		private Color chartBackColor;

		private Color scaleBackColor;

		private Pen graphPen;

		private Pen scalePen;

		private Pen origin_pen;

		private float scaleFontRectWidth;

		private float scaleFontRectHeight;

		private Font scaleFont;

		private Brush scaleFontBrush;

		private Brush scaleFontOriginBrush;

		private const float REALTIMEscaleXStep = 1f;

		private const float REALTIMEscaleXStart = 0f;

		private const float REALTIMEscaleYCenter = 0f;

		private const float REALTIMEscaleYStep = 1f;

		private int x;

		private float plotStep;

		private Bitmap backBuffer;

		private Bitmap subBuffer;

		private Bitmap scaleBuffer;

		private Bitmap backScalex;

		private Bitmap backScaley;

		private Graphics backG;

		private Graphics frontG;

		private bool isFirstLap;

		private const int MonitorUpdateDivision = 100;

		private PointF[] graphPoints;

		private bool isScaleChanging;

		private Point selectionStartPosition;

		private Rectangle selection;

		private Pen selectionPen;

		private HatchBrush selectionBrush;

		private Brush pointBrush;

		private const float GraphPointSize = 4f;

		private const float GraphPointSizeBig = 6f;

		private Brush pointBrushBig;

		private Pen verticalPen;

		private bool isScaleDragging;

		private PointF dragOriginPoint;

		private const float TOLERANCE = 10f;

		private Font loadFont;

		private SolidBrush loadBrush;

		private string updatingValue;

		private int indicatedPointNumber;

		private int markedPointNumber0;

		private int markedPointNumber1;

		private string issueCommand;

		private BinaryWriter temp_file_stream;

		private bool isCounterFinished;

		private int graphStartNumberOfDetail;

		private Color bar1Color;

		private Color bar2Color;

		private bool isPrintBars;

		private float upperLowerBarValue1;

		private float upperLowerBarValue2;

		private Color printBackColor;

		private Color printScalecolor;

		private StaticLocalInitFlag _0024STATIC_0024BackgroundWorkerRealTime_DoWork_002420211C12819_0024firstError_0024Init;

		private StaticLocalInitFlag _0024STATIC_0024updateMonitorGraph_00242001_0024monitor_points_0024Init;

		private StaticLocalInitFlag _0024STATIC_0024getScalewhole_00242001280D1_0024log10_2_under_0024Init;

		private int _0024STATIC_0024BackgroundWorkerRealTime_DoWork_002420211C12819_0024error_count;

		private StaticLocalInitFlag _0024STATIC_0024getScalewhole_00242001280D1_0024log10_5_under_0024Init;

		private double _0024STATIC_0024getScalewhole_00242001280D1_0024log10_2_under;

		private double _0024STATIC_0024getScalewhole_00242001280D1_0024log10_5_under;

		private bool _0024STATIC_0024BackgroundWorkerRealTime_DoWork_002420211C12819_0024firstError;

		private float[] _0024STATIC_0024updateMonitorGraph_00242001_0024monitor_points;

		private StaticLocalInitFlag _0024STATIC_0024BackgroundWorkerRealTime_DoWork_002420211C12819_0024error_count_0024Init;

		internal virtual TabControl TabControlMain
		{
			[DebuggerNonUserCode]
			get
			{
				return _TabControlMain;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_TabControlMain != null)
				{
					_TabControlMain.Selecting -= TabControlMain_Selecting;
					_TabControlMain.SelectedIndexChanged -= TabControlMain_SelectedIndexChanged;
				}
				_TabControlMain = value;
				if (_TabControlMain != null)
				{
					_TabControlMain.Selecting += TabControlMain_Selecting;
					_TabControlMain.SelectedIndexChanged += TabControlMain_SelectedIndexChanged;
				}
			}
		}

		internal virtual TabPage TabPageIndependent
		{
			[DebuggerNonUserCode]
			get
			{
				return _TabPageIndependent;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_TabPageIndependent = value;
			}
		}

		internal virtual TabPage TabPageRealTime
		{
			[DebuggerNonUserCode]
			get
			{
				return _TabPageRealTime;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_TabPageRealTime = value;
			}
		}

		internal virtual MenuStrip MenuStripMain
		{
			[DebuggerNonUserCode]
			get
			{
				return _MenuStripMain;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_MenuStripMain = value;
			}
		}

		internal virtual ToolStripMenuItem ToolStripMenuItemFile
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripMenuItemFile;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_ToolStripMenuItemFile = value;
			}
		}

		internal virtual ToolStripMenuItem ToolStripMenuItemQuit
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripMenuItemQuit;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_ToolStripMenuItemQuit != null)
				{
					_ToolStripMenuItemQuit.Click -= ToolStripMenuItemQuit_Click;
				}
				_ToolStripMenuItemQuit = value;
				if (_ToolStripMenuItemQuit != null)
				{
					_ToolStripMenuItemQuit.Click += ToolStripMenuItemQuit_Click;
				}
			}
		}

		internal virtual ToolStripMenuItem ToolStripMenuItemSoftwareConfigMenu
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripMenuItemSoftwareConfigMenu;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_ToolStripMenuItemSoftwareConfigMenu = value;
			}
		}

		internal virtual ToolStripMenuItem ToolStripMenuItemConnection
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripMenuItemConnection;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_ToolStripMenuItemConnection != null)
				{
					_ToolStripMenuItemConnection.Click -= ToolStripMenuItemConnection_Click;
				}
				_ToolStripMenuItemConnection = value;
				if (_ToolStripMenuItemConnection != null)
				{
					_ToolStripMenuItemConnection.Click += ToolStripMenuItemConnection_Click;
				}
			}
		}

		internal virtual ToolStripMenuItem ToolStripMenuItemAuto
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripMenuItemAuto;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_ToolStripMenuItemAuto != null)
				{
					_ToolStripMenuItemAuto.Click -= ToolStripMenuItemAuto_Click;
				}
				_ToolStripMenuItemAuto = value;
				if (_ToolStripMenuItemAuto != null)
				{
					_ToolStripMenuItemAuto.Click += ToolStripMenuItemAuto_Click;
				}
			}
		}

		internal virtual ToolStripMenuItem ToolStripMenuItemHelp
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripMenuItemHelp;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_ToolStripMenuItemHelp = value;
			}
		}

		internal virtual ToolStripMenuItem ToolStripMenuItemHelpContents
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripMenuItemHelpContents;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_ToolStripMenuItemHelpContents != null)
				{
					_ToolStripMenuItemHelpContents.Click -= ToolStripMenuItemHelpContents_Click;
				}
				_ToolStripMenuItemHelpContents = value;
				if (_ToolStripMenuItemHelpContents != null)
				{
					_ToolStripMenuItemHelpContents.Click += ToolStripMenuItemHelpContents_Click;
				}
			}
		}

		internal virtual ToolStripMenuItem ToolStripMenuItemAbout
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripMenuItemAbout;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_ToolStripMenuItemAbout != null)
				{
					_ToolStripMenuItemAbout.Click -= ToolStripMenuItemAbout_Click;
				}
				_ToolStripMenuItemAbout = value;
				if (_ToolStripMenuItemAbout != null)
				{
					_ToolStripMenuItemAbout.Click += ToolStripMenuItemAbout_Click;
				}
			}
		}

		internal virtual DataGridView DataGridViewValue
		{
			[DebuggerNonUserCode]
			get
			{
				return _DataGridViewValue;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_DataGridViewValue != null)
				{
					_DataGridViewValue.RowsAdded -= DataGridViewValue_RowsAdded;
				}
				_DataGridViewValue = value;
				if (_DataGridViewValue != null)
				{
					_DataGridViewValue.RowsAdded += DataGridViewValue_RowsAdded;
				}
			}
		}

		internal virtual DataSet DataSetValue
		{
			[DebuggerNonUserCode]
			get
			{
				return _DataSetValue;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_DataSetValue = value;
			}
		}

		internal virtual DataTable DataTableValue
		{
			[DebuggerNonUserCode]
			get
			{
				return _DataTableValue;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_DataTableValue = value;
			}
		}

		internal virtual DataColumn DataColumnValue
		{
			[DebuggerNonUserCode]
			get
			{
				return _DataColumnValue;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_DataColumnValue = value;
			}
		}

		internal virtual ToolStripStatusLabel ToolStripStatusLabelConnection
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripStatusLabelConnection;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_ToolStripStatusLabelConnection = value;
			}
		}

		internal virtual StatusStrip StatusStripMain
		{
			[DebuggerNonUserCode]
			get
			{
				return _StatusStripMain;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_StatusStripMain = value;
			}
		}

		internal virtual ToolStrip ToolStrip2
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStrip2;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_ToolStrip2 = value;
			}
		}

		internal virtual ToolStripButton ToolStripButtonFirst1
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripButtonFirst1;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_ToolStripButtonFirst1 != null)
				{
					_ToolStripButtonFirst1.Click -= ToolStripButtonFirst1_Click;
				}
				_ToolStripButtonFirst1 = value;
				if (_ToolStripButtonFirst1 != null)
				{
					_ToolStripButtonFirst1.Click += ToolStripButtonFirst1_Click;
				}
			}
		}

		internal virtual ToolStripButton ToolStripButtonGet
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripButtonGet;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_ToolStripButtonGet != null)
				{
					_ToolStripButtonGet.Click -= ToolStripButtonGet_Click;
				}
				_ToolStripButtonGet = value;
				if (_ToolStripButtonGet != null)
				{
					_ToolStripButtonGet.Click += ToolStripButtonGet_Click;
				}
			}
		}

		internal virtual ToolStripButton ToolStripButtonMemoryRequest
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripButtonMemoryRequest;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_ToolStripButtonMemoryRequest != null)
				{
					_ToolStripButtonMemoryRequest.Click -= ToolStripButtonRequestMemory_Click;
				}
				_ToolStripButtonMemoryRequest = value;
				if (_ToolStripButtonMemoryRequest != null)
				{
					_ToolStripButtonMemoryRequest.Click += ToolStripButtonRequestMemory_Click;
				}
			}
		}

		internal virtual ToolStripButton ToolStripButtonClear1
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripButtonClear1;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_ToolStripButtonClear1 != null)
				{
					_ToolStripButtonClear1.Click -= ToolStripButtonClear1_Click;
				}
				_ToolStripButtonClear1 = value;
				if (_ToolStripButtonClear1 != null)
				{
					_ToolStripButtonClear1.Click += ToolStripButtonClear1_Click;
				}
			}
		}

		internal virtual ToolStripButton ToolStripButton1
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripButton1;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_ToolStripButton1 = value;
			}
		}

		internal virtual ToolStripButton ToolStripButton2
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripButton2;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_ToolStripButton2 = value;
			}
		}

		internal virtual ToolStripButton ToolStripButton3
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripButton3;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_ToolStripButton3 = value;
			}
		}

		internal virtual ToolStripButton ToolStripButton4
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripButton4;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_ToolStripButton4 = value;
			}
		}

		internal virtual ToolStripButton ToolStripButtonMemoryClear
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripButtonMemoryClear;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_ToolStripButtonMemoryClear != null)
				{
					_ToolStripButtonMemoryClear.Click -= ToolStripButtonMemoryClear_Click;
				}
				_ToolStripButtonMemoryClear = value;
				if (_ToolStripButtonMemoryClear != null)
				{
					_ToolStripButtonMemoryClear.Click += ToolStripButtonMemoryClear_Click;
				}
			}
		}

		internal virtual ToolStripMenuItem ToolStripMenuItemSave
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripMenuItemSave;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_ToolStripMenuItemSave = value;
			}
		}

		internal virtual ImageList ImageList1
		{
			[DebuggerNonUserCode]
			get
			{
				return _ImageList1;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_ImageList1 = value;
			}
		}

		internal virtual DataColumn DataColumnUnit
		{
			[DebuggerNonUserCode]
			get
			{
				return _DataColumnUnit;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_DataColumnUnit = value;
			}
		}

		internal virtual ToolStripMenuItem ToolStripMenuItemPrint
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripMenuItemPrint;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_ToolStripMenuItemPrint != null)
				{
					_ToolStripMenuItemPrint.Click -= ToolStripMenuItemPrint_Click;
				}
				_ToolStripMenuItemPrint = value;
				if (_ToolStripMenuItemPrint != null)
				{
					_ToolStripMenuItemPrint.Click += ToolStripMenuItemPrint_Click;
				}
			}
		}

		internal virtual ToolStripMenuItem ToolStripMenuItemSaveCSV
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripMenuItemSaveCSV;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_ToolStripMenuItemSaveCSV != null)
				{
					_ToolStripMenuItemSaveCSV.Click -= ToolStripMenuItemCsvSave_Click;
				}
				_ToolStripMenuItemSaveCSV = value;
				if (_ToolStripMenuItemSaveCSV != null)
				{
					_ToolStripMenuItemSaveCSV.Click += ToolStripMenuItemCsvSave_Click;
				}
			}
		}

		internal virtual SaveFileDialog SaveFileDialogCSV
		{
			[DebuggerNonUserCode]
			get
			{
				return _SaveFileDialogCSV;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_SaveFileDialogCSV = value;
			}
		}

		internal virtual ToolStripDropDownButton ToolStripDropDownButtonSaveCsv
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripDropDownButtonSaveCsv;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_ToolStripDropDownButtonSaveCsv = value;
			}
		}

		internal virtual ToolStripMenuItem ToolStripMenuItemCsvSave
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripMenuItemCsvSave;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_ToolStripMenuItemCsvSave != null)
				{
					_ToolStripMenuItemCsvSave.Click -= ToolStripMenuItemCsvSave_Click;
				}
				_ToolStripMenuItemCsvSave = value;
				if (_ToolStripMenuItemCsvSave != null)
				{
					_ToolStripMenuItemCsvSave.Click += ToolStripMenuItemCsvSave_Click;
				}
			}
		}

		internal virtual ToolStripMenuItem ToolStripMenuItemCsvAdd
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripMenuItemCsvAdd;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_ToolStripMenuItemCsvAdd != null)
				{
					_ToolStripMenuItemCsvAdd.Click -= ToolStripMenuItemCsvAdd_Click;
				}
				_ToolStripMenuItemCsvAdd = value;
				if (_ToolStripMenuItemCsvAdd != null)
				{
					_ToolStripMenuItemCsvAdd.Click += ToolStripMenuItemCsvAdd_Click;
				}
			}
		}

		internal virtual OpenFileDialog OpenFileDialogCsvAdd
		{
			[DebuggerNonUserCode]
			get
			{
				return _OpenFileDialogCsvAdd;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_OpenFileDialogCsvAdd = value;
			}
		}

		internal virtual ToolStripStatusLabel ToolStripStatusLabelModel
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripStatusLabelModel;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_ToolStripStatusLabelModel = value;
			}
		}

		internal virtual ToolStripSeparator ToolStripSeparator1
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripSeparator1;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_ToolStripSeparator1 = value;
			}
		}

		internal virtual ToolStripMenuItem ToolStripMenuItemAddCsv
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripMenuItemAddCsv;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_ToolStripMenuItemAddCsv != null)
				{
					_ToolStripMenuItemAddCsv.Click -= ToolStripMenuItemCsvAdd_Click;
				}
				_ToolStripMenuItemAddCsv = value;
				if (_ToolStripMenuItemAddCsv != null)
				{
					_ToolStripMenuItemAddCsv.Click += ToolStripMenuItemCsvAdd_Click;
				}
			}
		}

		internal virtual ToolStripMenuItem ToolStripMenuItemGaugeConfig
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripMenuItemGaugeConfig;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_ToolStripMenuItemGaugeConfig = value;
			}
		}

		internal virtual ToolStripMenuItem ToolStripMenuItemConfig
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripMenuItemConfig;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_ToolStripMenuItemConfig != null)
				{
					_ToolStripMenuItemConfig.Click -= ToolStripMenuItemConfig_Click;
				}
				_ToolStripMenuItemConfig = value;
				if (_ToolStripMenuItemConfig != null)
				{
					_ToolStripMenuItemConfig.Click += ToolStripMenuItemConfig_Click;
				}
			}
		}

		internal virtual ToolStripStatusLabel ToolStripStatusLabelStatus
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripStatusLabelStatus;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_ToolStripStatusLabelStatus = value;
			}
		}

		internal virtual ContextMenuStrip ContextMenuStripUpDown
		{
			[DebuggerNonUserCode]
			get
			{
				return _ContextMenuStripUpDown;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_ContextMenuStripUpDown != null)
				{
					_ContextMenuStripUpDown.Opened -= ContextMenuStripUpDown_Opened;
				}
				_ContextMenuStripUpDown = value;
				if (_ContextMenuStripUpDown != null)
				{
					_ContextMenuStripUpDown.Opened += ContextMenuStripUpDown_Opened;
				}
			}
		}

		internal virtual ToolStripMenuItem ToolStripMenuItemUpDown
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripMenuItemUpDown;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_ToolStripMenuItemUpDown != null)
				{
					_ToolStripMenuItemUpDown.Click -= ToolStripMenuItemUpDown_Click;
				}
				_ToolStripMenuItemUpDown = value;
				if (_ToolStripMenuItemUpDown != null)
				{
					_ToolStripMenuItemUpDown.Click += ToolStripMenuItemUpDown_Click;
				}
			}
		}

		internal virtual ToolStripMenuItem ToolStripMenuItemOpen
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripMenuItemOpen;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_ToolStripMenuItemOpen != null)
				{
					_ToolStripMenuItemOpen.Click -= ToolStripMenuItemOpen_Click;
				}
				_ToolStripMenuItemOpen = value;
				if (_ToolStripMenuItemOpen != null)
				{
					_ToolStripMenuItemOpen.Click += ToolStripMenuItemOpen_Click;
				}
			}
		}

		internal virtual OpenFileDialog OpenFileDialogCsvRead
		{
			[DebuggerNonUserCode]
			get
			{
				return _OpenFileDialogCsvRead;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_OpenFileDialogCsvRead = value;
			}
		}

		internal virtual Button ButtonTrackI
		{
			[DebuggerNonUserCode]
			get
			{
				return _ButtonTrackI;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_ButtonTrackI != null)
				{
					_ButtonTrackI.Click -= ButtonTrack_ClickI;
				}
				_ButtonTrackI = value;
				if (_ButtonTrackI != null)
				{
					_ButtonTrackI.Click += ButtonTrack_ClickI;
				}
			}
		}

		internal virtual Button ButtonZeroI
		{
			[DebuggerNonUserCode]
			get
			{
				return _ButtonZeroI;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_ButtonZeroI != null)
				{
					_ButtonZeroI.Click -= ButtonZero_ClickI;
				}
				_ButtonZeroI = value;
				if (_ButtonZeroI != null)
				{
					_ButtonZeroI.Click += ButtonZero_ClickI;
				}
			}
		}

		internal virtual Button ButtonPeakI
		{
			[DebuggerNonUserCode]
			get
			{
				return _ButtonPeakI;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_ButtonPeakI != null)
				{
					_ButtonPeakI.Click -= ButtonPeak_ClickI;
				}
				_ButtonPeakI = value;
				if (_ButtonPeakI != null)
				{
					_ButtonPeakI.Click += ButtonPeak_ClickI;
				}
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

		internal virtual Button ButtonGet
		{
			[DebuggerNonUserCode]
			get
			{
				return _ButtonGet;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_ButtonGet != null)
				{
					_ButtonGet.Click -= ToolStripButtonGet_Click;
				}
				_ButtonGet = value;
				if (_ButtonGet != null)
				{
					_ButtonGet.Click += ToolStripButtonGet_Click;
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

		internal virtual Label LabelNumber
		{
			[DebuggerNonUserCode]
			get
			{
				return _LabelNumber;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_LabelNumber = value;
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

		internal virtual Label LabelMinimum
		{
			[DebuggerNonUserCode]
			get
			{
				return _LabelMinimum;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_LabelMinimum = value;
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

		internal virtual Label LabelMaximum
		{
			[DebuggerNonUserCode]
			get
			{
				return _LabelMaximum;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_LabelMaximum = value;
			}
		}

		internal virtual Label LabelAverage
		{
			[DebuggerNonUserCode]
			get
			{
				return _LabelAverage;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_LabelAverage = value;
			}
		}

		internal virtual BackgroundWorker BackgroundWorkerRealTime
		{
			[DebuggerNonUserCode]
			get
			{
				return _BackgroundWorkerRealTime;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_BackgroundWorkerRealTime != null)
				{
					_BackgroundWorkerRealTime.RunWorkerCompleted -= BackgroundWorkerRealTime_RunWorkerCompleted;
					_BackgroundWorkerRealTime.DoWork -= BackgroundWorkerRealTime_DoWork;
				}
				_BackgroundWorkerRealTime = value;
				if (_BackgroundWorkerRealTime != null)
				{
					_BackgroundWorkerRealTime.RunWorkerCompleted += BackgroundWorkerRealTime_RunWorkerCompleted;
					_BackgroundWorkerRealTime.DoWork += BackgroundWorkerRealTime_DoWork;
				}
			}
		}

		internal virtual PrintDocument PrintDocument1
		{
			[DebuggerNonUserCode]
			get
			{
				return _PrintDocument1;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_PrintDocument1 != null)
				{
					_PrintDocument1.PrintPage -= PrintDocument1_PrintPage;
				}
				_PrintDocument1 = value;
				if (_PrintDocument1 != null)
				{
					_PrintDocument1.PrintPage += PrintDocument1_PrintPage;
				}
			}
		}

		internal virtual ToolStripMenuItem ToolStripMenuItemPreview
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripMenuItemPreview;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_ToolStripMenuItemPreview != null)
				{
					_ToolStripMenuItemPreview.Click -= ToolStripMenuItemPreview_Click;
				}
				_ToolStripMenuItemPreview = value;
				if (_ToolStripMenuItemPreview != null)
				{
					_ToolStripMenuItemPreview.Click += ToolStripMenuItemPreview_Click;
				}
			}
		}

		internal virtual PrintPreviewDialog PrintPreviewDialog1
		{
			[DebuggerNonUserCode]
			get
			{
				return _PrintPreviewDialog1;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_PrintPreviewDialog1 = value;
			}
		}

		internal virtual ToolStrip ToolStrip1
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStrip1;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_ToolStrip1 = value;
			}
		}

		internal virtual ToolStripButton ToolStripButtonStart
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripButtonStart;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_ToolStripButtonStart != null)
				{
					_ToolStripButtonStart.Click -= ToolStripButtonStart_Click;
				}
				_ToolStripButtonStart = value;
				if (_ToolStripButtonStart != null)
				{
					_ToolStripButtonStart.Click += ToolStripButtonStart_Click;
				}
			}
		}

		internal virtual ToolStripButton ToolStripButtonStop
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripButtonStop;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_ToolStripButtonStop != null)
				{
					_ToolStripButtonStop.Click -= ToolStripButtonStop_Click;
				}
				_ToolStripButtonStop = value;
				if (_ToolStripButtonStop != null)
				{
					_ToolStripButtonStop.Click += ToolStripButtonStop_Click;
				}
			}
		}

		internal virtual ToolStripSeparator ToolStripSeparator4
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripSeparator4;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_ToolStripSeparator4 = value;
			}
		}

		internal virtual ToolStripButton ToolStripButtonFirst
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripButtonFirst;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_ToolStripButtonFirst != null)
				{
					_ToolStripButtonFirst.Click -= ToolStripButtonFirst_Click;
				}
				_ToolStripButtonFirst = value;
				if (_ToolStripButtonFirst != null)
				{
					_ToolStripButtonFirst.Click += ToolStripButtonFirst_Click;
				}
			}
		}

		internal virtual ToolStripButton ToolStripButtonAll
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripButtonAll;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_ToolStripButtonAll != null)
				{
					_ToolStripButtonAll.Click -= ToolStripButtonAll_Click;
				}
				_ToolStripButtonAll = value;
				if (_ToolStripButtonAll != null)
				{
					_ToolStripButtonAll.Click += ToolStripButtonAll_Click;
				}
			}
		}

		internal virtual ToolStripComboBox ToolStripComboBoxYaxis
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripComboBoxYaxis;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_ToolStripComboBoxYaxis != null)
				{
					_ToolStripComboBoxYaxis.KeyDown -= ToolStripComboBoxYaxis_KeyDown;
					_ToolStripComboBoxYaxis.TextChanged -= ToolStripComboBox_TextChanged;
					_ToolStripComboBoxYaxis.SelectedIndexChanged -= ToolStripComboBoxYaxis_SelectedIndexChanged;
				}
				_ToolStripComboBoxYaxis = value;
				if (_ToolStripComboBoxYaxis != null)
				{
					_ToolStripComboBoxYaxis.KeyDown += ToolStripComboBoxYaxis_KeyDown;
					_ToolStripComboBoxYaxis.TextChanged += ToolStripComboBox_TextChanged;
					_ToolStripComboBoxYaxis.SelectedIndexChanged += ToolStripComboBoxYaxis_SelectedIndexChanged;
				}
			}
		}

		internal virtual ToolStripComboBox ToolStripComboBoxXaxis
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripComboBoxXaxis;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_ToolStripComboBoxXaxis != null)
				{
					_ToolStripComboBoxXaxis.KeyDown -= ToolStripComboBoxXaxis_KeyDown;
					_ToolStripComboBoxXaxis.TextChanged -= ToolStripComboBox_TextChanged;
					_ToolStripComboBoxXaxis.SelectedIndexChanged -= ToolStripComboBoxXaxis_SelectedIndexChanged;
				}
				_ToolStripComboBoxXaxis = value;
				if (_ToolStripComboBoxXaxis != null)
				{
					_ToolStripComboBoxXaxis.KeyDown += ToolStripComboBoxXaxis_KeyDown;
					_ToolStripComboBoxXaxis.TextChanged += ToolStripComboBox_TextChanged;
					_ToolStripComboBoxXaxis.SelectedIndexChanged += ToolStripComboBoxXaxis_SelectedIndexChanged;
				}
			}
		}

		internal virtual ToolStripSeparator ToolStripSeparator2
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripSeparator2;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_ToolStripSeparator2 = value;
			}
		}

		internal virtual ToolStripButton ToolStripButtonPlus
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripButtonPlus;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_ToolStripButtonPlus != null)
				{
					_ToolStripButtonPlus.Click -= ToolStripButtonPlus_Click;
				}
				_ToolStripButtonPlus = value;
				if (_ToolStripButtonPlus != null)
				{
					_ToolStripButtonPlus.Click += ToolStripButtonPlus_Click;
				}
			}
		}

		internal virtual ToolStripButton ToolStripButtonBoth
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripButtonBoth;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_ToolStripButtonBoth != null)
				{
					_ToolStripButtonBoth.Click -= ToolStripButtonBoth_Click;
				}
				_ToolStripButtonBoth = value;
				if (_ToolStripButtonBoth != null)
				{
					_ToolStripButtonBoth.Click += ToolStripButtonBoth_Click;
				}
			}
		}

		internal virtual ToolStripButton ToolStripButtonMinus
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripButtonMinus;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_ToolStripButtonMinus != null)
				{
					_ToolStripButtonMinus.Click -= ToolStripButtonMinus_Click;
				}
				_ToolStripButtonMinus = value;
				if (_ToolStripButtonMinus != null)
				{
					_ToolStripButtonMinus.Click += ToolStripButtonMinus_Click;
				}
			}
		}

		internal virtual ToolStripButton ToolStripButtonReverse
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripButtonReverse;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_ToolStripButtonReverse != null)
				{
					_ToolStripButtonReverse.Click -= ToolStripButtonReverse_Click;
				}
				_ToolStripButtonReverse = value;
				if (_ToolStripButtonReverse != null)
				{
					_ToolStripButtonReverse.Click += ToolStripButtonReverse_Click;
				}
			}
		}

		internal virtual ToolStripSeparator ToolStripSeparator3
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripSeparator3;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_ToolStripSeparator3 = value;
			}
		}

		internal virtual ToolStripButton ToolStripButtonClear
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripButtonClear;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_ToolStripButtonClear != null)
				{
					_ToolStripButtonClear.Click -= ToolStripButtonClear_Click;
				}
				_ToolStripButtonClear = value;
				if (_ToolStripButtonClear != null)
				{
					_ToolStripButtonClear.Click += ToolStripButtonClear_Click;
				}
			}
		}

		internal virtual Panel Panel2
		{
			[DebuggerNonUserCode]
			get
			{
				return _Panel2;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_Panel2 = value;
			}
		}

		internal virtual Panel Panel3
		{
			[DebuggerNonUserCode]
			get
			{
				return _Panel3;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_Panel3 = value;
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

		internal virtual TableLayoutPanel TableLayoutPanel2
		{
			[DebuggerNonUserCode]
			get
			{
				return _TableLayoutPanel2;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_TableLayoutPanel2 = value;
			}
		}

		internal virtual Label Label7
		{
			[DebuggerNonUserCode]
			get
			{
				return _Label7;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_Label7 = value;
			}
		}

		internal virtual Label LabelTrigerTitle
		{
			[DebuggerNonUserCode]
			get
			{
				return _LabelTrigerTitle;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_LabelTrigerTitle = value;
			}
		}

		internal virtual Label LabelTriggerValue
		{
			[DebuggerNonUserCode]
			get
			{
				return _LabelTriggerValue;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_LabelTriggerValue = value;
			}
		}

		internal virtual Label LabelInterval
		{
			[DebuggerNonUserCode]
			get
			{
				return _LabelInterval;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_LabelInterval = value;
			}
		}

		internal virtual Label LabelPeak
		{
			[DebuggerNonUserCode]
			get
			{
				return _LabelPeak;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_LabelPeak = value;
			}
		}

		internal virtual Button ButtonTrack
		{
			[DebuggerNonUserCode]
			get
			{
				return _ButtonTrack;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_ButtonTrack != null)
				{
					_ButtonTrack.Click -= ButtonTrack_Click;
				}
				_ButtonTrack = value;
				if (_ButtonTrack != null)
				{
					_ButtonTrack.Click += ButtonTrack_Click;
				}
			}
		}

		internal virtual Label LabelUnit
		{
			[DebuggerNonUserCode]
			get
			{
				return _LabelUnit;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_LabelUnit = value;
			}
		}

		internal virtual Button ButtonZero
		{
			[DebuggerNonUserCode]
			get
			{
				return _ButtonZero;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_ButtonZero != null)
				{
					_ButtonZero.Click -= ButtonZero_Click;
				}
				_ButtonZero = value;
				if (_ButtonZero != null)
				{
					_ButtonZero.Click += ButtonZero_Click;
				}
			}
		}

		internal virtual Button ButtonPeak
		{
			[DebuggerNonUserCode]
			get
			{
				return _ButtonPeak;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_ButtonPeak != null)
				{
					_ButtonPeak.Click -= ButtonPeak_Click;
				}
				_ButtonPeak = value;
				if (_ButtonPeak != null)
				{
					_ButtonPeak.Click += ButtonPeak_Click;
				}
			}
		}

		internal virtual Label LabelValue
		{
			[DebuggerNonUserCode]
			get
			{
				return _LabelValue;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_LabelValue = value;
			}
		}

		internal virtual PictureBox PictureBoxChart
		{
			[DebuggerNonUserCode]
			get
			{
				return _PictureBoxChart;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_PictureBoxChart != null)
				{
					_PictureBoxChart.Paint -= PictureBoxChart_Paint;
					_PictureBoxChart.SizeChanged -= PictureBoxChart_SizeChanged;
					_PictureBoxChart.MouseMove -= PictureBoxChart_MouseMove;
					_PictureBoxChart.MouseUp -= PictureBoxChart_MouseUp;
					_PictureBoxChart.MouseDown -= PictureBoxChart_MouseDown;
				}
				_PictureBoxChart = value;
				if (_PictureBoxChart != null)
				{
					_PictureBoxChart.Paint += PictureBoxChart_Paint;
					_PictureBoxChart.SizeChanged += PictureBoxChart_SizeChanged;
					_PictureBoxChart.MouseMove += PictureBoxChart_MouseMove;
					_PictureBoxChart.MouseUp += PictureBoxChart_MouseUp;
					_PictureBoxChart.MouseDown += PictureBoxChart_MouseDown;
				}
			}
		}

		internal virtual Panel PanelCorner
		{
			[DebuggerNonUserCode]
			get
			{
				return _PanelCorner;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_PanelCorner = value;
			}
		}

		internal virtual PictureBox PictureBoxScaleY
		{
			[DebuggerNonUserCode]
			get
			{
				return _PictureBoxScaleY;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_PictureBoxScaleY != null)
				{
					_PictureBoxScaleY.Paint -= PictureBoxScaleY_Paint;
				}
				_PictureBoxScaleY = value;
				if (_PictureBoxScaleY != null)
				{
					_PictureBoxScaleY.Paint += PictureBoxScaleY_Paint;
				}
			}
		}

		internal virtual PictureBox PictureBoxScaleX
		{
			[DebuggerNonUserCode]
			get
			{
				return _PictureBoxScaleX;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_PictureBoxScaleX != null)
				{
					_PictureBoxScaleX.Paint -= PictureBoxScaleX_Paint;
				}
				_PictureBoxScaleX = value;
				if (_PictureBoxScaleX != null)
				{
					_PictureBoxScaleX.Paint += PictureBoxScaleX_Paint;
				}
			}
		}

		internal virtual ToolStripMenuItem ToolStripMenuItemPrintConfig
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripMenuItemPrintConfig;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_ToolStripMenuItemPrintConfig != null)
				{
					_ToolStripMenuItemPrintConfig.Click -= ToolStripMenuItemPrintConfig_Click;
				}
				_ToolStripMenuItemPrintConfig = value;
				if (_ToolStripMenuItemPrintConfig != null)
				{
					_ToolStripMenuItemPrintConfig.Click += ToolStripMenuItemPrintConfig_Click;
				}
			}
		}

		internal virtual PrintDialog PrintDialog1
		{
			[DebuggerNonUserCode]
			get
			{
				return _PrintDialog1;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_PrintDialog1 = value;
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

		internal virtual ToolStripStatusLabel ToolStripStatusLabelWaiting
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripStatusLabelWaiting;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_ToolStripStatusLabelWaiting = value;
			}
		}

		internal virtual ToolTip ToolTip1
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolTip1;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_ToolTip1 = value;
			}
		}

		internal virtual ToolStripSeparator ToolStripSeparator5
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripSeparator5;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_ToolStripSeparator5 = value;
			}
		}

		internal virtual ToolStripLabel ToolStripLabel1
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripLabel1;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_ToolStripLabel1 = value;
			}
		}

		internal virtual ToolStripSeparator ToolStripSeparator6
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripSeparator6;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_ToolStripSeparator6 = value;
			}
		}

		internal virtual ToolStripLabel ToolStripLabel2
		{
			[DebuggerNonUserCode]
			get
			{
				return _ToolStripLabel2;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_ToolStripLabel2 = value;
			}
		}

		internal virtual DataGridViewTextBoxColumn ValueDataGridViewTextBoxColumn
		{
			[DebuggerNonUserCode]
			get
			{
				return _ValueDataGridViewTextBoxColumn;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_ValueDataGridViewTextBoxColumn = value;
			}
		}

		internal virtual DataGridViewTextBoxColumn Unit
		{
			[DebuggerNonUserCode]
			get
			{
				return _Unit;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_Unit = value;
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

		internal virtual Label LabelFinishTimeTitle
		{
			[DebuggerNonUserCode]
			get
			{
				return _LabelFinishTimeTitle;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_LabelFinishTimeTitle = value;
			}
		}

		internal virtual Label LabelProcessing
		{
			[DebuggerNonUserCode]
			get
			{
				return _LabelProcessing;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				_LabelProcessing = value;
			}
		}

		internal virtual AbstractForceGauge gauge
		{
			[DebuggerNonUserCode]
			get
			{
				return _gauge;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_gauge != null)
				{
					_gauge.DataRecieved -= gauge_DataRecieved;
				}
				_gauge = value;
				if (_gauge != null)
				{
					_gauge.DataRecieved += gauge_DataRecieved;
				}
			}
		}

		internal virtual MultimediaTimer timerMonitor
		{
			[DebuggerNonUserCode]
			get
			{
				return _timerMonitor;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_timerMonitor != null)
				{
					_timerMonitor.Elapsed -= TimerMonitor_Elapsed;
				}
				_timerMonitor = value;
				if (_timerMonitor != null)
				{
					_timerMonitor.Elapsed += TimerMonitor_Elapsed;
				}
			}
		}

		internal virtual MultimediaTimer timerGet
		{
			[DebuggerNonUserCode]
			get
			{
				return _timerGet;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			[DebuggerNonUserCode]
			set
			{
				if (_timerGet != null)
				{
					_timerGet.Elapsed -= TimerGet_Elapsed;
				}
				_timerGet = value;
				if (_timerGet != null)
				{
					_timerGet.Elapsed += TimerGet_Elapsed;
				}
			}
		}

		public FormMain()
		{
			base.FormClosing += FormMain_FormClosing;
			base.Load += FormMain_Load;
			base.Shown += FormMain_Shown;
			stats = new Statistics();
			unit_strings = new string[3]
			{
				"kg/g",
				"N",
				"lb/oz"
			};
			autoConfig = new AutoRecordConfig();
			finishTimeCounter = 0uL;
			finishCounterLimit = 0uL;
			currentScale = new ScaleInfo();
			scaleHistory = new Stack<ScaleInfo>();
			chartBackColor = Color.Azure;
			scaleBackColor = Color.LemonChiffon;
			graphPen = new Pen(Color.Blue);
			scalePen = Pens.Chartreuse;
			origin_pen = Pens.Red;
			scaleFontRectWidth = 100f;
			scaleFontRectHeight = 20f;
			scaleFont = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point);
			scaleFontBrush = Brushes.Black;
			scaleFontOriginBrush = Brushes.Red;
			isScaleChanging = false;
			selectionPen = new Pen(Color.Blue);
			selectionBrush = new HatchBrush(HatchStyle.Percent25, Color.Blue, Color.Transparent);
			pointBrush = Brushes.DarkMagenta;
			pointBrushBig = Brushes.DarkMagenta;
			verticalPen = Pens.Turquoise;
			isScaleDragging = false;
			loadFont = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point);
			loadBrush = new SolidBrush(Color.DarkBlue);
			updatingValue = "+00.00NT";
			indicatedPointNumber = -1;
			markedPointNumber0 = -1;
			markedPointNumber1 = -1;
			issueCommand = "D";
			isCounterFinished = false;
			graphStartNumberOfDetail = -1;
			bar1Color = Color.Orange;
			bar2Color = Color.Pink;
			printBackColor = Color.Azure;
			printScalecolor = Color.FromArgb(255, 221, 255, 222);
			_0024STATIC_0024BackgroundWorkerRealTime_DoWork_002420211C12819_0024firstError_0024Init = new StaticLocalInitFlag();
			_0024STATIC_0024updateMonitorGraph_00242001_0024monitor_points_0024Init = new StaticLocalInitFlag();
			_0024STATIC_0024getScalewhole_00242001280D1_0024log10_2_under_0024Init = new StaticLocalInitFlag();
			_0024STATIC_0024getScalewhole_00242001280D1_0024log10_5_under_0024Init = new StaticLocalInitFlag();
			_0024STATIC_0024BackgroundWorkerRealTime_DoWork_002420211C12819_0024error_count_0024Init = new StaticLocalInitFlag();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ZLINK31E.FormMain));
			TabControlMain = new System.Windows.Forms.TabControl();
			TabPageIndependent = new System.Windows.Forms.TabPage();
			Panel1 = new System.Windows.Forms.Panel();
			ButtonGet = new System.Windows.Forms.Button();
			Label1 = new System.Windows.Forms.Label();
			ButtonTrackI = new System.Windows.Forms.Button();
			Label2 = new System.Windows.Forms.Label();
			ButtonZeroI = new System.Windows.Forms.Button();
			LabelNumber = new System.Windows.Forms.Label();
			ButtonPeakI = new System.Windows.Forms.Button();
			Label3 = new System.Windows.Forms.Label();
			LabelMinimum = new System.Windows.Forms.Label();
			Label4 = new System.Windows.Forms.Label();
			LabelMaximum = new System.Windows.Forms.Label();
			LabelAverage = new System.Windows.Forms.Label();
			DataGridViewValue = new System.Windows.Forms.DataGridView();
			ValueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
			DataSetValue = new System.Data.DataSet();
			DataTableValue = new System.Data.DataTable();
			DataColumnValue = new System.Data.DataColumn();
			DataColumnUnit = new System.Data.DataColumn();
			ToolStrip2 = new System.Windows.Forms.ToolStrip();
			ContextMenuStripUpDown = new System.Windows.Forms.ContextMenuStrip(components);
			ToolStripMenuItemUpDown = new System.Windows.Forms.ToolStripMenuItem();
			ToolStripButtonFirst1 = new System.Windows.Forms.ToolStripButton();
			ToolStripButtonGet = new System.Windows.Forms.ToolStripButton();
			ToolStripButtonClear1 = new System.Windows.Forms.ToolStripButton();
			ToolStripButtonMemoryRequest = new System.Windows.Forms.ToolStripButton();
			ToolStripButtonMemoryClear = new System.Windows.Forms.ToolStripButton();
			ToolStripDropDownButtonSaveCsv = new System.Windows.Forms.ToolStripDropDownButton();
			ToolStripMenuItemCsvSave = new System.Windows.Forms.ToolStripMenuItem();
			ToolStripMenuItemCsvAdd = new System.Windows.Forms.ToolStripMenuItem();
			TabPageRealTime = new System.Windows.Forms.TabPage();
			Panel2 = new System.Windows.Forms.Panel();
			LabelProcessing = new System.Windows.Forms.Label();
			Panel3 = new System.Windows.Forms.Panel();
			Label5 = new System.Windows.Forms.Label();
			GroupBox1 = new System.Windows.Forms.GroupBox();
			TableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			LabelFinishTimeTitle = new System.Windows.Forms.Label();
			LabelFinishTime = new System.Windows.Forms.Label();
			Label7 = new System.Windows.Forms.Label();
			LabelTrigerTitle = new System.Windows.Forms.Label();
			LabelTriggerValue = new System.Windows.Forms.Label();
			LabelInterval = new System.Windows.Forms.Label();
			LabelPeak = new System.Windows.Forms.Label();
			ButtonTrack = new System.Windows.Forms.Button();
			LabelUnit = new System.Windows.Forms.Label();
			ButtonZero = new System.Windows.Forms.Button();
			ButtonPeak = new System.Windows.Forms.Button();
			LabelValue = new System.Windows.Forms.Label();
			PictureBoxChart = new System.Windows.Forms.PictureBox();
			PanelCorner = new System.Windows.Forms.Panel();
			PictureBoxScaleY = new System.Windows.Forms.PictureBox();
			PictureBoxScaleX = new System.Windows.Forms.PictureBox();
			ToolStrip1 = new System.Windows.Forms.ToolStrip();
			ToolStripButtonStart = new System.Windows.Forms.ToolStripButton();
			ToolStripButtonStop = new System.Windows.Forms.ToolStripButton();
			ToolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			ToolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			ToolStripButtonAll = new System.Windows.Forms.ToolStripButton();
			ToolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			ToolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			ToolStripComboBoxYaxis = new System.Windows.Forms.ToolStripComboBox();
			ToolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			ToolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
			ToolStripComboBoxXaxis = new System.Windows.Forms.ToolStripComboBox();
			ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			ToolStripButtonPlus = new System.Windows.Forms.ToolStripButton();
			ToolStripButtonBoth = new System.Windows.Forms.ToolStripButton();
			ToolStripButtonMinus = new System.Windows.Forms.ToolStripButton();
			ToolStripButtonReverse = new System.Windows.Forms.ToolStripButton();
			ToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			ToolStripButtonClear = new System.Windows.Forms.ToolStripButton();
			ImageList1 = new System.Windows.Forms.ImageList(components);
			MenuStripMain = new System.Windows.Forms.MenuStrip();
			ToolStripMenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
			ToolStripMenuItemSave = new System.Windows.Forms.ToolStripMenuItem();
			ToolStripMenuItemSaveCSV = new System.Windows.Forms.ToolStripMenuItem();
			ToolStripMenuItemAddCsv = new System.Windows.Forms.ToolStripMenuItem();
			ToolStripMenuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
			ToolStripMenuItemPrintConfig = new System.Windows.Forms.ToolStripMenuItem();
			ToolStripMenuItemPreview = new System.Windows.Forms.ToolStripMenuItem();
			ToolStripMenuItemPrint = new System.Windows.Forms.ToolStripMenuItem();
			ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			ToolStripMenuItemQuit = new System.Windows.Forms.ToolStripMenuItem();
			ToolStripMenuItemSoftwareConfigMenu = new System.Windows.Forms.ToolStripMenuItem();
			ToolStripMenuItemConnection = new System.Windows.Forms.ToolStripMenuItem();
			ToolStripMenuItemAuto = new System.Windows.Forms.ToolStripMenuItem();
			ToolStripMenuItemGaugeConfig = new System.Windows.Forms.ToolStripMenuItem();
			ToolStripMenuItemConfig = new System.Windows.Forms.ToolStripMenuItem();
			ToolStripMenuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
			ToolStripMenuItemHelpContents = new System.Windows.Forms.ToolStripMenuItem();
			ToolStripMenuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
			StatusStripMain = new System.Windows.Forms.StatusStrip();
			ToolStripStatusLabelConnection = new System.Windows.Forms.ToolStripStatusLabel();
			ToolStripStatusLabelModel = new System.Windows.Forms.ToolStripStatusLabel();
			ToolStripStatusLabelStatus = new System.Windows.Forms.ToolStripStatusLabel();
			ToolStripStatusLabelWaiting = new System.Windows.Forms.ToolStripStatusLabel();
			SaveFileDialogCSV = new System.Windows.Forms.SaveFileDialog();
			OpenFileDialogCsvAdd = new System.Windows.Forms.OpenFileDialog();
			OpenFileDialogCsvRead = new System.Windows.Forms.OpenFileDialog();
			BackgroundWorkerRealTime = new System.ComponentModel.BackgroundWorker();
			PrintDocument1 = new System.Drawing.Printing.PrintDocument();
			PrintPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
			PrintDialog1 = new System.Windows.Forms.PrintDialog();
			ToolStripButton1 = new System.Windows.Forms.ToolStripButton();
			ToolStripButton2 = new System.Windows.Forms.ToolStripButton();
			ToolStripButton3 = new System.Windows.Forms.ToolStripButton();
			ToolStripButton4 = new System.Windows.Forms.ToolStripButton();
			ToolTip1 = new System.Windows.Forms.ToolTip(components);
			TabControlMain.SuspendLayout();
			TabPageIndependent.SuspendLayout();
			Panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)DataGridViewValue).BeginInit();
			((System.ComponentModel.ISupportInitialize)DataSetValue).BeginInit();
			((System.ComponentModel.ISupportInitialize)DataTableValue).BeginInit();
			ToolStrip2.SuspendLayout();
			ContextMenuStripUpDown.SuspendLayout();
			TabPageRealTime.SuspendLayout();
			Panel2.SuspendLayout();
			Panel3.SuspendLayout();
			GroupBox1.SuspendLayout();
			TableLayoutPanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)PictureBoxChart).BeginInit();
			((System.ComponentModel.ISupportInitialize)PictureBoxScaleY).BeginInit();
			((System.ComponentModel.ISupportInitialize)PictureBoxScaleX).BeginInit();
			ToolStrip1.SuspendLayout();
			MenuStripMain.SuspendLayout();
			StatusStripMain.SuspendLayout();
			SuspendLayout();
			TabControlMain.Controls.Add(TabPageIndependent);
			TabControlMain.Controls.Add(TabPageRealTime);
			TabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
			TabControlMain.ImageList = ImageList1;
			System.Drawing.Point point2 = TabControlMain.Location = new System.Drawing.Point(0, 26);
			System.Windows.Forms.Padding padding2 = TabControlMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			TabControlMain.Name = "TabControlMain";
			TabControlMain.SelectedIndex = 0;
			System.Drawing.Size size2 = TabControlMain.Size = new System.Drawing.Size(807, 533);
			TabControlMain.TabIndex = 0;
			TabPageIndependent.BackColor = System.Drawing.Color.Transparent;
			TabPageIndependent.BackgroundImage = ZLINK31E.My.Resources.Resources.relf3;
			TabPageIndependent.Controls.Add(Panel1);
			TabPageIndependent.Controls.Add(DataGridViewValue);
			TabPageIndependent.Controls.Add(ToolStrip2);
			TabPageIndependent.ImageIndex = 3;
			point2 = (TabPageIndependent.Location = new System.Drawing.Point(4, 24));
			padding2 = (TabPageIndependent.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4));
			TabPageIndependent.Name = "TabPageIndependent";
			padding2 = (TabPageIndependent.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4));
			size2 = (TabPageIndependent.Size = new System.Drawing.Size(799, 505));
			TabPageIndependent.TabIndex = 0;
			TabPageIndependent.Text = "Individual";
			TabPageIndependent.UseVisualStyleBackColor = true;
			Panel1.BackColor = System.Drawing.SystemColors.ControlLight;
			Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			Panel1.Controls.Add(ButtonGet);
			Panel1.Controls.Add(Label1);
			Panel1.Controls.Add(ButtonTrackI);
			Panel1.Controls.Add(Label2);
			Panel1.Controls.Add(ButtonZeroI);
			Panel1.Controls.Add(LabelNumber);
			Panel1.Controls.Add(ButtonPeakI);
			Panel1.Controls.Add(Label3);
			Panel1.Controls.Add(LabelMinimum);
			Panel1.Controls.Add(Label4);
			Panel1.Controls.Add(LabelMaximum);
			Panel1.Controls.Add(LabelAverage);
			Panel1.Dock = System.Windows.Forms.DockStyle.Top;
			point2 = (Panel1.Location = new System.Drawing.Point(300, 29));
			padding2 = (Panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4));
			Panel1.Name = "Panel1";
			size2 = (Panel1.Size = new System.Drawing.Size(496, 266));
			Panel1.TabIndex = 10;
			ButtonGet.BackColor = System.Drawing.SystemColors.ControlLightLight;
			ButtonGet.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			point2 = (ButtonGet.Location = new System.Drawing.Point(100, 209));
			padding2 = (ButtonGet.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4));
			ButtonGet.Name = "ButtonGet";
			size2 = (ButtonGet.Size = new System.Drawing.Size(77, 46));
			ButtonGet.TabIndex = 3;
			ButtonGet.Text = "Get";
			ButtonGet.UseVisualStyleBackColor = false;
			Label1.AutoSize = true;
			point2 = (Label1.Location = new System.Drawing.Point(14, 14));
			Label1.Name = "Label1";
			size2 = (Label1.Size = new System.Drawing.Size(51, 15));
			Label1.TabIndex = 0;
			Label1.Text = "Average";
			Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			ButtonTrackI.BackColor = System.Drawing.SystemColors.ControlLight;
			point2 = (ButtonTrackI.Location = new System.Drawing.Point(100, 164));
			padding2 = (ButtonTrackI.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4));
			ButtonTrackI.Name = "ButtonTrackI";
			size2 = (ButtonTrackI.Size = new System.Drawing.Size(77, 39));
			ButtonTrackI.TabIndex = 9;
			ButtonTrackI.Text = "Realtime";
			ButtonTrackI.UseVisualStyleBackColor = false;
			Label2.AutoSize = true;
			point2 = (Label2.Location = new System.Drawing.Point(14, 48));
			Label2.Name = "Label2";
			size2 = (Label2.Size = new System.Drawing.Size(28, 15));
			Label2.TabIndex = 0;
			Label2.Text = "Max";
			Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			point2 = (ButtonZeroI.Location = new System.Drawing.Point(16, 209));
			padding2 = (ButtonZeroI.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4));
			ButtonZeroI.Name = "ButtonZeroI";
			size2 = (ButtonZeroI.Size = new System.Drawing.Size(77, 46));
			ButtonZeroI.TabIndex = 8;
			ButtonZeroI.Text = "Zero";
			ButtonZeroI.UseVisualStyleBackColor = true;
			LabelNumber.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			point2 = (LabelNumber.Location = new System.Drawing.Point(73, 115));
			LabelNumber.Name = "LabelNumber";
			size2 = (LabelNumber.Size = new System.Drawing.Size(269, 34));
			LabelNumber.TabIndex = 0;
			LabelNumber.Text = "---";
			LabelNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			ButtonPeakI.BackColor = System.Drawing.SystemColors.ControlLight;
			point2 = (ButtonPeakI.Location = new System.Drawing.Point(16, 164));
			padding2 = (ButtonPeakI.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4));
			ButtonPeakI.Name = "ButtonPeakI";
			size2 = (ButtonPeakI.Size = new System.Drawing.Size(77, 39));
			ButtonPeakI.TabIndex = 7;
			ButtonPeakI.Text = "Peak";
			ButtonPeakI.UseVisualStyleBackColor = false;
			Label3.AutoSize = true;
			point2 = (Label3.Location = new System.Drawing.Point(14, 81));
			Label3.Name = "Label3";
			size2 = (Label3.Size = new System.Drawing.Size(26, 15));
			Label3.TabIndex = 0;
			Label3.Text = "Min";
			Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			LabelMinimum.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			point2 = (LabelMinimum.Location = new System.Drawing.Point(73, 81));
			LabelMinimum.Name = "LabelMinimum";
			size2 = (LabelMinimum.Size = new System.Drawing.Size(269, 34));
			LabelMinimum.TabIndex = 0;
			LabelMinimum.Text = "---";
			LabelMinimum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			Label4.AutoSize = true;
			point2 = (Label4.Location = new System.Drawing.Point(14, 115));
			Label4.Name = "Label4";
			size2 = (Label4.Size = new System.Drawing.Size(52, 15));
			Label4.TabIndex = 0;
			Label4.Text = "Number";
			Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			LabelMaximum.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			point2 = (LabelMaximum.Location = new System.Drawing.Point(73, 48));
			LabelMaximum.Name = "LabelMaximum";
			size2 = (LabelMaximum.Size = new System.Drawing.Size(269, 34));
			LabelMaximum.TabIndex = 0;
			LabelMaximum.Text = "---";
			LabelMaximum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			LabelAverage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			point2 = (LabelAverage.Location = new System.Drawing.Point(73, 14));
			LabelAverage.Name = "LabelAverage";
			size2 = (LabelAverage.Size = new System.Drawing.Size(269, 34));
			LabelAverage.TabIndex = 0;
			LabelAverage.Text = "---";
			LabelAverage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			DataGridViewValue.AllowUserToAddRows = false;
			DataGridViewValue.AllowUserToDeleteRows = false;
			DataGridViewValue.AutoGenerateColumns = false;
			DataGridViewValue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			DataGridViewValue.Columns.AddRange(ValueDataGridViewTextBoxColumn, Unit);
			DataGridViewValue.DataMember = "Table1";
			DataGridViewValue.DataSource = DataSetValue;
			DataGridViewValue.Dock = System.Windows.Forms.DockStyle.Left;
			point2 = (DataGridViewValue.Location = new System.Drawing.Point(3, 29));
			padding2 = (DataGridViewValue.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4));
			DataGridViewValue.Name = "DataGridViewValue";
			DataGridViewValue.ReadOnly = true;
			DataGridViewValue.RowHeadersVisible = false;
			DataGridViewValue.RowTemplate.Height = 21;
			size2 = (DataGridViewValue.Size = new System.Drawing.Size(297, 472));
			DataGridViewValue.TabIndex = 0;
			ValueDataGridViewTextBoxColumn.DataPropertyName = "Value";
			ValueDataGridViewTextBoxColumn.HeaderText = "Value";
			ValueDataGridViewTextBoxColumn.Name = "ValueDataGridViewTextBoxColumn";
			ValueDataGridViewTextBoxColumn.ReadOnly = true;
			ValueDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Unit.DataPropertyName = "Unit";
			Unit.HeaderText = "Unit";
			Unit.Name = "Unit";
			Unit.ReadOnly = true;
			Unit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			DataSetValue.DataSetName = "NewDataSet";
			DataSetValue.Locale = new System.Globalization.CultureInfo("");
			DataSetValue.RemotingFormat = System.Data.SerializationFormat.Binary;
			DataSetValue.Tables.AddRange(new System.Data.DataTable[1]
			{
				DataTableValue
			});
			DataTableValue.Columns.AddRange(new System.Data.DataColumn[2]
			{
				DataColumnValue,
				DataColumnUnit
			});
			DataTableValue.Locale = new System.Globalization.CultureInfo("");
			DataTableValue.RemotingFormat = System.Data.SerializationFormat.Binary;
			DataTableValue.TableName = "Table1";
			DataColumnValue.Caption = "Value";
			DataColumnValue.ColumnName = "Value";
			DataColumnUnit.ColumnName = "Unit";
			ToolStrip2.BackColor = System.Drawing.SystemColors.Control;
			ToolStrip2.ContextMenuStrip = ContextMenuStripUpDown;
			ToolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			ToolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[6]
			{
				ToolStripButtonFirst1,
				ToolStripButtonGet,
				ToolStripButtonClear1,
				ToolStripButtonMemoryRequest,
				ToolStripButtonMemoryClear,
				ToolStripDropDownButtonSaveCsv
			});
			point2 = (ToolStrip2.Location = new System.Drawing.Point(3, 4));
			ToolStrip2.Name = "ToolStrip2";
			size2 = (ToolStrip2.Size = new System.Drawing.Size(793, 25));
			ToolStrip2.TabIndex = 6;
			ToolStrip2.Text = "ToolStrip2";
			ContextMenuStripUpDown.Items.AddRange(new System.Windows.Forms.ToolStripItem[1]
			{
				ToolStripMenuItemUpDown
			});
			ContextMenuStripUpDown.Name = "ContextMenuStripUpDown";
			size2 = (ContextMenuStripUpDown.Size = new System.Drawing.Size(101, 26));
			ToolStripMenuItemUpDown.Name = "ToolStripMenuItemUpDown";
			size2 = (ToolStripMenuItemUpDown.Size = new System.Drawing.Size(100, 22));
			ToolStripMenuItemUpDown.Text = "上へ";
			ToolStripButtonFirst1.Image = (System.Drawing.Image)resources.GetObject("ToolStripButtonFirst1.Image");
			ToolStripButtonFirst1.ImageTransparentColor = System.Drawing.Color.Magenta;
			ToolStripButtonFirst1.Name = "ToolStripButtonFirst1";
			size2 = (ToolStripButtonFirst1.Size = new System.Drawing.Size(54, 22));
			ToolStripButtonFirst1.Text = "First";
			ToolStripButtonFirst1.ToolTipText = "Show from first";
			ToolStripButtonGet.Image = (System.Drawing.Image)resources.GetObject("ToolStripButtonGet.Image");
			ToolStripButtonGet.ImageTransparentColor = System.Drawing.Color.Magenta;
			ToolStripButtonGet.Name = "ToolStripButtonGet";
			size2 = (ToolStripButtonGet.Size = new System.Drawing.Size(49, 22));
			ToolStripButtonGet.Text = "Get";
			ToolStripButtonGet.ToolTipText = "Get a data";
			ToolStripButtonClear1.Image = (System.Drawing.Image)resources.GetObject("ToolStripButtonClear1.Image");
			ToolStripButtonClear1.ImageTransparentColor = System.Drawing.Color.Magenta;
			ToolStripButtonClear1.Name = "ToolStripButtonClear1";
			size2 = (ToolStripButtonClear1.Size = new System.Drawing.Size(58, 22));
			ToolStripButtonClear1.Text = "Clear";
			ToolStripButtonClear1.ToolTipText = "Clear all the data";
			ToolStripButtonMemoryRequest.Image = (System.Drawing.Image)resources.GetObject("ToolStripButtonMemoryRequest.Image");
			ToolStripButtonMemoryRequest.ImageTransparentColor = System.Drawing.Color.Magenta;
			ToolStripButtonMemoryRequest.Name = "ToolStripButtonMemoryRequest";
			size2 = (ToolStripButtonMemoryRequest.Size = new System.Drawing.Size(109, 22));
			ToolStripButtonMemoryRequest.Text = "Read Memory";
			ToolStripButtonMemoryRequest.ToolTipText = "Read memorized data from the instrument";
			ToolStripButtonMemoryClear.Image = (System.Drawing.Image)resources.GetObject("ToolStripButtonMemoryClear.Image");
			ToolStripButtonMemoryClear.ImageTransparentColor = System.Drawing.Color.Magenta;
			ToolStripButtonMemoryClear.Name = "ToolStripButtonMemoryClear";
			size2 = (ToolStripButtonMemoryClear.Size = new System.Drawing.Size(110, 22));
			ToolStripButtonMemoryClear.Text = "Clear Memory";
			ToolStripButtonMemoryClear.ToolTipText = "Clear memory of instrument";
			ToolStripDropDownButtonSaveCsv.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2]
			{
				ToolStripMenuItemCsvSave,
				ToolStripMenuItemCsvAdd
			});
			ToolStripDropDownButtonSaveCsv.Image = (System.Drawing.Image)resources.GetObject("ToolStripDropDownButtonSaveCsv.Image");
			ToolStripDropDownButtonSaveCsv.ImageTransparentColor = System.Drawing.Color.Magenta;
			ToolStripDropDownButtonSaveCsv.Name = "ToolStripDropDownButtonSaveCsv";
			size2 = (ToolStripDropDownButtonSaveCsv.Size = new System.Drawing.Size(66, 22));
			ToolStripDropDownButtonSaveCsv.Text = "Save";
			ToolStripDropDownButtonSaveCsv.ToolTipText = "Save data to file";
			ToolStripMenuItemCsvSave.Image = ZLINK31E.My.Resources.Resources.saveAs_2_p;
			ToolStripMenuItemCsvSave.Name = "ToolStripMenuItemCsvSave";
			size2 = (ToolStripMenuItemCsvSave.Size = new System.Drawing.Size(150, 22));
			ToolStripMenuItemCsvSave.Text = "Save as CSV";
			ToolStripMenuItemCsvAdd.Image = ZLINK31E.My.Resources.Resources.saveAs_1_g;
			ToolStripMenuItemCsvAdd.Name = "ToolStripMenuItemCsvAdd";
			size2 = (ToolStripMenuItemCsvAdd.Size = new System.Drawing.Size(150, 22));
			ToolStripMenuItemCsvAdd.Text = "Add to CSV";
			TabPageRealTime.BackColor = System.Drawing.Color.Transparent;
			TabPageRealTime.Controls.Add(Panel2);
			TabPageRealTime.Controls.Add(ToolStrip1);
			TabPageRealTime.ImageIndex = 0;
			point2 = (TabPageRealTime.Location = new System.Drawing.Point(4, 24));
			padding2 = (TabPageRealTime.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4));
			TabPageRealTime.Name = "TabPageRealTime";
			padding2 = (TabPageRealTime.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4));
			size2 = (TabPageRealTime.Size = new System.Drawing.Size(799, 505));
			TabPageRealTime.TabIndex = 1;
			TabPageRealTime.Text = "Realtime";
			TabPageRealTime.UseVisualStyleBackColor = true;
			Panel2.Controls.Add(LabelProcessing);
			Panel2.Controls.Add(Panel3);
			Panel2.Controls.Add(PictureBoxChart);
			Panel2.Controls.Add(PanelCorner);
			Panel2.Controls.Add(PictureBoxScaleY);
			Panel2.Controls.Add(PictureBoxScaleX);
			Panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			point2 = (Panel2.Location = new System.Drawing.Point(3, 30));
			padding2 = (Panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4));
			Panel2.Name = "Panel2";
			size2 = (Panel2.Size = new System.Drawing.Size(793, 471));
			Panel2.TabIndex = 10;
			LabelProcessing.BackColor = System.Drawing.Color.LightGray;
			LabelProcessing.Font = new System.Drawing.Font("MS UI Gothic", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
			LabelProcessing.ForeColor = System.Drawing.Color.Blue;
			point2 = (LabelProcessing.Location = new System.Drawing.Point(187, 198));
			LabelProcessing.Name = "LabelProcessing";
			size2 = (LabelProcessing.Size = new System.Drawing.Size(419, 74));
			LabelProcessing.TabIndex = 17;
			LabelProcessing.Text = "Processing ....";
			LabelProcessing.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			LabelProcessing.Visible = false;
			Panel3.BackColor = System.Drawing.SystemColors.Control;
			Panel3.Controls.Add(Label5);
			Panel3.Controls.Add(GroupBox1);
			Panel3.Controls.Add(LabelPeak);
			Panel3.Controls.Add(ButtonTrack);
			Panel3.Controls.Add(LabelUnit);
			Panel3.Controls.Add(ButtonZero);
			Panel3.Controls.Add(ButtonPeak);
			Panel3.Controls.Add(LabelValue);
			Panel3.Dock = System.Windows.Forms.DockStyle.Top;
			point2 = (Panel3.Location = new System.Drawing.Point(0, 0));
			padding2 = (Panel3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4));
			Panel3.Name = "Panel3";
			size2 = (Panel3.Size = new System.Drawing.Size(793, 114));
			Panel3.TabIndex = 15;
			Label5.AutoSize = true;
			point2 = (Label5.Location = new System.Drawing.Point(183, 40));
			Label5.Name = "Label5";
			size2 = (Label5.Size = new System.Drawing.Size(29, 15));
			Label5.TabIndex = 9;
			Label5.Text = "Unit";
			GroupBox1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			GroupBox1.AutoSize = true;
			GroupBox1.BackColor = System.Drawing.Color.Transparent;
			GroupBox1.Controls.Add(TableLayoutPanel2);
			point2 = (GroupBox1.Location = new System.Drawing.Point(490, 9));
			padding2 = (GroupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4));
			GroupBox1.Name = "GroupBox1";
			padding2 = (GroupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4));
			size2 = (GroupBox1.Size = new System.Drawing.Size(297, 100));
			GroupBox1.TabIndex = 8;
			GroupBox1.TabStop = false;
			GroupBox1.Text = "Auto recording properties";
			TableLayoutPanel2.AutoSize = true;
			TableLayoutPanel2.ColumnCount = 2;
			TableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.83871f));
			TableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.16129f));
			TableLayoutPanel2.Controls.Add(LabelFinishTimeTitle, 0, 2);
			TableLayoutPanel2.Controls.Add(LabelFinishTime, 0, 2);
			TableLayoutPanel2.Controls.Add(Label7, 0, 0);
			TableLayoutPanel2.Controls.Add(LabelTrigerTitle, 0, 1);
			TableLayoutPanel2.Controls.Add(LabelTriggerValue, 1, 1);
			TableLayoutPanel2.Controls.Add(LabelInterval, 1, 0);
			TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			point2 = (TableLayoutPanel2.Location = new System.Drawing.Point(3, 18));
			padding2 = (TableLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4));
			TableLayoutPanel2.Name = "TableLayoutPanel2";
			TableLayoutPanel2.RowCount = 3;
			TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333f));
			TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333f));
			TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25f));
			size2 = (TableLayoutPanel2.Size = new System.Drawing.Size(291, 78));
			TableLayoutPanel2.TabIndex = 0;
			LabelFinishTimeTitle.AutoSize = true;
			LabelFinishTimeTitle.Dock = System.Windows.Forms.DockStyle.Fill;
			point2 = (LabelFinishTimeTitle.Location = new System.Drawing.Point(3, 52));
			LabelFinishTimeTitle.Name = "LabelFinishTimeTitle";
			size2 = (LabelFinishTimeTitle.Size = new System.Drawing.Size(95, 26));
			LabelFinishTimeTitle.TabIndex = 4;
			LabelFinishTimeTitle.Text = "Recording Time";
			LabelFinishTimeTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			LabelFinishTime.AutoSize = true;
			LabelFinishTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			LabelFinishTime.Dock = System.Windows.Forms.DockStyle.Fill;
			point2 = (LabelFinishTime.Location = new System.Drawing.Point(104, 52));
			LabelFinishTime.Name = "LabelFinishTime";
			size2 = (LabelFinishTime.Size = new System.Drawing.Size(184, 26));
			LabelFinishTime.TabIndex = 3;
			LabelFinishTime.Text = "---";
			LabelFinishTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			Label7.AutoSize = true;
			Label7.Dock = System.Windows.Forms.DockStyle.Fill;
			point2 = (Label7.Location = new System.Drawing.Point(3, 0));
			Label7.Name = "Label7";
			size2 = (Label7.Size = new System.Drawing.Size(95, 26));
			Label7.TabIndex = 1;
			Label7.Text = "Interval";
			Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			LabelTrigerTitle.AutoSize = true;
			LabelTrigerTitle.Dock = System.Windows.Forms.DockStyle.Fill;
			point2 = (LabelTrigerTitle.Location = new System.Drawing.Point(3, 26));
			LabelTrigerTitle.Name = "LabelTrigerTitle";
			size2 = (LabelTrigerTitle.Size = new System.Drawing.Size(95, 26));
			LabelTrigerTitle.TabIndex = 1;
			LabelTrigerTitle.Text = "Trigger";
			LabelTrigerTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			LabelTriggerValue.AutoSize = true;
			LabelTriggerValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			LabelTriggerValue.Dock = System.Windows.Forms.DockStyle.Fill;
			point2 = (LabelTriggerValue.Location = new System.Drawing.Point(104, 26));
			LabelTriggerValue.Name = "LabelTriggerValue";
			size2 = (LabelTriggerValue.Size = new System.Drawing.Size(184, 26));
			LabelTriggerValue.TabIndex = 2;
			LabelTriggerValue.Text = "---";
			LabelTriggerValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			LabelInterval.AutoSize = true;
			LabelInterval.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			LabelInterval.Dock = System.Windows.Forms.DockStyle.Fill;
			point2 = (LabelInterval.Location = new System.Drawing.Point(104, 0));
			LabelInterval.Name = "LabelInterval";
			size2 = (LabelInterval.Size = new System.Drawing.Size(184, 26));
			LabelInterval.TabIndex = 2;
			LabelInterval.Text = "---";
			LabelInterval.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			LabelPeak.Font = new System.Drawing.Font("MS UI Gothic", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			LabelPeak.ForeColor = System.Drawing.SystemColors.ControlText;
			point2 = (LabelPeak.Location = new System.Drawing.Point(3, 11));
			LabelPeak.Name = "LabelPeak";
			size2 = (LabelPeak.Size = new System.Drawing.Size(79, 29));
			LabelPeak.TabIndex = 2;
			LabelPeak.Text = "Peak";
			LabelPeak.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			ButtonTrack.BackColor = System.Drawing.SystemColors.ControlLight;
			point2 = (ButtonTrack.Location = new System.Drawing.Point(344, 9));
			padding2 = (ButtonTrack.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4));
			ButtonTrack.Name = "ButtonTrack";
			size2 = (ButtonTrack.Size = new System.Drawing.Size(77, 39));
			ButtonTrack.TabIndex = 3;
			ButtonTrack.Text = "Realtime";
			ButtonTrack.UseVisualStyleBackColor = false;
			LabelUnit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			LabelUnit.Font = new System.Drawing.Font("MS UI Gothic", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
			point2 = (LabelUnit.Location = new System.Drawing.Point(182, 66));
			LabelUnit.Name = "LabelUnit";
			size2 = (LabelUnit.Size = new System.Drawing.Size(71, 34));
			LabelUnit.TabIndex = 3;
			LabelUnit.Text = "---";
			LabelUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			point2 = (ButtonZero.Location = new System.Drawing.Point(260, 54));
			padding2 = (ButtonZero.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4));
			ButtonZero.Name = "ButtonZero";
			size2 = (ButtonZero.Size = new System.Drawing.Size(77, 46));
			ButtonZero.TabIndex = 3;
			ButtonZero.Text = "Zero";
			ButtonZero.UseVisualStyleBackColor = true;
			ButtonPeak.BackColor = System.Drawing.SystemColors.ControlLight;
			point2 = (ButtonPeak.Location = new System.Drawing.Point(260, 9));
			padding2 = (ButtonPeak.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4));
			ButtonPeak.Name = "ButtonPeak";
			size2 = (ButtonPeak.Size = new System.Drawing.Size(77, 39));
			ButtonPeak.TabIndex = 3;
			ButtonPeak.Text = "Peak";
			ButtonPeak.UseVisualStyleBackColor = false;
			LabelValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			LabelValue.Font = new System.Drawing.Font("MS UI Gothic", 24f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
			point2 = (LabelValue.Location = new System.Drawing.Point(5, 40));
			LabelValue.Name = "LabelValue";
			size2 = (LabelValue.Size = new System.Drawing.Size(171, 60));
			LabelValue.TabIndex = 0;
			LabelValue.Text = "----";
			LabelValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			PictureBoxChart.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			PictureBoxChart.BackColor = System.Drawing.SystemColors.Window;
			point2 = (PictureBoxChart.Location = new System.Drawing.Point(98, 121));
			padding2 = (PictureBoxChart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4));
			PictureBoxChart.Name = "PictureBoxChart";
			size2 = (PictureBoxChart.Size = new System.Drawing.Size(691, 301));
			PictureBoxChart.TabIndex = 14;
			PictureBoxChart.TabStop = false;
			ToolTip1.SetToolTip(PictureBoxChart, "Right click + Drag: Scroll\r\nLeft click + Drag: Zoom\r\nDouble click: Back zoom");
			PanelCorner.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			point2 = (PanelCorner.Location = new System.Drawing.Point(3, 429));
			padding2 = (PanelCorner.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4));
			PanelCorner.Name = "PanelCorner";
			size2 = (PanelCorner.Size = new System.Drawing.Size(87, 38));
			PanelCorner.TabIndex = 16;
			PictureBoxScaleY.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			point2 = (PictureBoxScaleY.Location = new System.Drawing.Point(3, 121));
			padding2 = (PictureBoxScaleY.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4));
			PictureBoxScaleY.Name = "PictureBoxScaleY";
			size2 = (PictureBoxScaleY.Size = new System.Drawing.Size(87, 301));
			PictureBoxScaleY.TabIndex = 12;
			PictureBoxScaleY.TabStop = false;
			PictureBoxScaleX.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			point2 = (PictureBoxScaleX.Location = new System.Drawing.Point(98, 429));
			padding2 = (PictureBoxScaleX.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4));
			PictureBoxScaleX.Name = "PictureBoxScaleX";
			size2 = (PictureBoxScaleX.Size = new System.Drawing.Size(691, 38));
			PictureBoxScaleX.TabIndex = 13;
			PictureBoxScaleX.TabStop = false;
			ToolStrip1.BackColor = System.Drawing.SystemColors.Control;
			ToolStrip1.ContextMenuStrip = ContextMenuStripUpDown;
			ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			ToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[18]
			{
				ToolStripButtonStart,
				ToolStripButtonStop,
				ToolStripSeparator4,
				ToolStripButtonFirst,
				ToolStripButtonAll,
				ToolStripSeparator5,
				ToolStripLabel1,
				ToolStripComboBoxYaxis,
				ToolStripSeparator6,
				ToolStripLabel2,
				ToolStripComboBoxXaxis,
				ToolStripSeparator2,
				ToolStripButtonPlus,
				ToolStripButtonBoth,
				ToolStripButtonMinus,
				ToolStripButtonReverse,
				ToolStripSeparator3,
				ToolStripButtonClear
			});
			point2 = (ToolStrip1.Location = new System.Drawing.Point(3, 4));
			ToolStrip1.Name = "ToolStrip1";
			size2 = (ToolStrip1.Size = new System.Drawing.Size(793, 26));
			ToolStrip1.TabIndex = 9;
			ToolStrip1.Text = "ToolStrip1";
			ToolStripButtonStart.Image = ZLINK31E.My.Resources.Resources.rec;
			ToolStripButtonStart.ImageTransparentColor = System.Drawing.Color.Magenta;
			ToolStripButtonStart.Name = "ToolStripButtonStart";
			size2 = (ToolStripButtonStart.Size = new System.Drawing.Size(58, 23));
			ToolStripButtonStart.Text = "Start";
			ToolStripButtonStart.ToolTipText = "Start recording";
			ToolStripButtonStop.Enabled = false;
			ToolStripButtonStop.Image = (System.Drawing.Image)resources.GetObject("ToolStripButtonStop.Image");
			ToolStripButtonStop.ImageTransparentColor = System.Drawing.Color.Magenta;
			ToolStripButtonStop.Name = "ToolStripButtonStop";
			size2 = (ToolStripButtonStop.Size = new System.Drawing.Size(55, 23));
			ToolStripButtonStop.Text = "Stop";
			ToolStripButtonStop.ToolTipText = "Stop recording";
			ToolStripSeparator4.Name = "ToolStripSeparator4";
			size2 = (ToolStripSeparator4.Size = new System.Drawing.Size(6, 26));
			ToolStripButtonFirst.Image = (System.Drawing.Image)resources.GetObject("ToolStripButtonFirst.Image");
			ToolStripButtonFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
			ToolStripButtonFirst.Name = "ToolStripButtonFirst";
			size2 = (ToolStripButtonFirst.Size = new System.Drawing.Size(54, 23));
			ToolStripButtonFirst.Text = "First";
			ToolStripButtonFirst.ToolTipText = "Show from first data";
			ToolStripButtonAll.Image = ZLINK31E.My.Resources.Resources.select_1;
			ToolStripButtonAll.ImageTransparentColor = System.Drawing.Color.Magenta;
			ToolStripButtonAll.Name = "ToolStripButtonAll";
			size2 = (ToolStripButtonAll.Size = new System.Drawing.Size(78, 23));
			ToolStripButtonAll.Text = "Show All";
			ToolStripButtonAll.ToolTipText = "Show whole of the graphic chart";
			ToolStripSeparator5.Name = "ToolStripSeparator5";
			size2 = (ToolStripSeparator5.Size = new System.Drawing.Size(6, 26));
			ToolStripLabel1.BackColor = System.Drawing.SystemColors.ControlDark;
			ToolStripLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			ToolStripLabel1.Name = "ToolStripLabel1";
			size2 = (ToolStripLabel1.Size = new System.Drawing.Size(16, 23));
			ToolStripLabel1.Text = "Y";
			ToolStripComboBoxYaxis.AutoSize = false;
			ToolStripComboBoxYaxis.AutoToolTip = true;
			ToolStripComboBoxYaxis.Items.AddRange(new object[10]
			{
				"1",
				"10",
				"100",
				"1000",
				"2",
				"20",
				"200",
				"5",
				"50",
				"500"
			});
			ToolStripComboBoxYaxis.MergeAction = System.Windows.Forms.MergeAction.Insert;
			ToolStripComboBoxYaxis.Name = "ToolStripComboBoxYaxis";
			size2 = (ToolStripComboBoxYaxis.Size = new System.Drawing.Size(93, 26));
			ToolStripComboBoxYaxis.Sorted = true;
			ToolStripComboBoxYaxis.Text = "Y";
			ToolStripComboBoxYaxis.ToolTipText = "1 resolution of Y";
			ToolStripSeparator6.Name = "ToolStripSeparator6";
			size2 = (ToolStripSeparator6.Size = new System.Drawing.Size(6, 26));
			ToolStripLabel2.BackColor = System.Drawing.SystemColors.ControlDark;
			ToolStripLabel2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			ToolStripLabel2.Name = "ToolStripLabel2";
			size2 = (ToolStripLabel2.Size = new System.Drawing.Size(16, 23));
			ToolStripLabel2.Text = "X";
			ToolStripComboBoxXaxis.AutoSize = false;
			ToolStripComboBoxXaxis.AutoToolTip = true;
			ToolStripComboBoxXaxis.Items.AddRange(new object[5]
			{
				"1",
				"10",
				"100",
				"5",
				"60"
			});
			ToolStripComboBoxXaxis.MergeAction = System.Windows.Forms.MergeAction.Insert;
			ToolStripComboBoxXaxis.Name = "ToolStripComboBoxXaxis";
			size2 = (ToolStripComboBoxXaxis.Size = new System.Drawing.Size(93, 26));
			ToolStripComboBoxXaxis.Sorted = true;
			ToolStripComboBoxXaxis.Text = "X";
			ToolStripComboBoxXaxis.ToolTipText = "1 resolution of X";
			ToolStripSeparator2.Name = "ToolStripSeparator2";
			size2 = (ToolStripSeparator2.Size = new System.Drawing.Size(6, 26));
			ToolStripButtonPlus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			ToolStripButtonPlus.Image = ZLINK31E.My.Resources.Resources.plus;
			ToolStripButtonPlus.ImageTransparentColor = System.Drawing.Color.Magenta;
			ToolStripButtonPlus.Name = "ToolStripButtonPlus";
			size2 = (ToolStripButtonPlus.Size = new System.Drawing.Size(23, 23));
			ToolStripButtonPlus.Text = "ToolStripButton5";
			ToolStripButtonPlus.ToolTipText = "Plus graph";
			ToolStripButtonBoth.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			ToolStripButtonBoth.Image = ZLINK31E.My.Resources.Resources.both;
			ToolStripButtonBoth.ImageTransparentColor = System.Drawing.Color.Magenta;
			ToolStripButtonBoth.Name = "ToolStripButtonBoth";
			size2 = (ToolStripButtonBoth.Size = new System.Drawing.Size(23, 23));
			ToolStripButtonBoth.Text = "ToolStripButton6";
			ToolStripButtonBoth.ToolTipText = "Both graph";
			ToolStripButtonMinus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			ToolStripButtonMinus.Image = ZLINK31E.My.Resources.Resources.minus;
			ToolStripButtonMinus.ImageTransparentColor = System.Drawing.Color.Magenta;
			ToolStripButtonMinus.Name = "ToolStripButtonMinus";
			size2 = (ToolStripButtonMinus.Size = new System.Drawing.Size(23, 23));
			ToolStripButtonMinus.Text = "ToolStripButton7";
			ToolStripButtonMinus.ToolTipText = "Minus graph";
			ToolStripButtonReverse.Image = ZLINK31E.My.Resources.Resources.redo_y;
			ToolStripButtonReverse.ImageTransparentColor = System.Drawing.Color.Magenta;
			ToolStripButtonReverse.Name = "ToolStripButtonReverse";
			size2 = (ToolStripButtonReverse.Size = new System.Drawing.Size(75, 23));
			ToolStripButtonReverse.Text = "Reverse";
			ToolStripButtonReverse.ToolTipText = "Reverse + and -";
			ToolStripSeparator3.Name = "ToolStripSeparator3";
			size2 = (ToolStripSeparator3.Size = new System.Drawing.Size(6, 26));
			ToolStripButtonClear.Image = (System.Drawing.Image)resources.GetObject("ToolStripButtonClear.Image");
			ToolStripButtonClear.ImageTransparentColor = System.Drawing.Color.Magenta;
			ToolStripButtonClear.Name = "ToolStripButtonClear";
			size2 = (ToolStripButtonClear.Size = new System.Drawing.Size(58, 23));
			ToolStripButtonClear.Text = "Clear";
			ToolStripButtonClear.ToolTipText = "Clear all the data";
			ImageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("ImageList1.ImageStream");
			ImageList1.TransparentColor = System.Drawing.Color.Transparent;
			ImageList1.Images.SetKeyName(0, "t2.ico");
			ImageList1.Images.SetKeyName(1, "歯車3.ico");
			ImageList1.Images.SetKeyName(2, "plus1.ico");
			ImageList1.Images.SetKeyName(3, "文書-2.ico");
			ImageList1.Images.SetKeyName(4, "connect.ico");
			ImageList1.Images.SetKeyName(5, "con_off_r.ico");
			MenuStripMain.Font = new System.Drawing.Font("メイリオ", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			MenuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[4]
			{
				ToolStripMenuItemFile,
				ToolStripMenuItemSoftwareConfigMenu,
				ToolStripMenuItemGaugeConfig,
				ToolStripMenuItemHelp
			});
			point2 = (MenuStripMain.Location = new System.Drawing.Point(0, 0));
			MenuStripMain.Name = "MenuStripMain";
			padding2 = (MenuStripMain.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2));
			size2 = (MenuStripMain.Size = new System.Drawing.Size(807, 26));
			MenuStripMain.TabIndex = 1;
			MenuStripMain.Text = "MenuStrip1";
			ToolStripMenuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[7]
			{
				ToolStripMenuItemSave,
				ToolStripMenuItemOpen,
				ToolStripMenuItemPrintConfig,
				ToolStripMenuItemPreview,
				ToolStripMenuItemPrint,
				ToolStripSeparator1,
				ToolStripMenuItemQuit
			});
			ToolStripMenuItemFile.Name = "ToolStripMenuItemFile";
			size2 = (ToolStripMenuItemFile.Size = new System.Drawing.Size(40, 22));
			ToolStripMenuItemFile.Text = "File";
			ToolStripMenuItemSave.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2]
			{
				ToolStripMenuItemSaveCSV,
				ToolStripMenuItemAddCsv
			});
			ToolStripMenuItemSave.Image = (System.Drawing.Image)resources.GetObject("ToolStripMenuItemSave.Image");
			ToolStripMenuItemSave.Name = "ToolStripMenuItemSave";
			size2 = (ToolStripMenuItemSave.Size = new System.Drawing.Size(169, 22));
			ToolStripMenuItemSave.Text = "Save";
			ToolStripMenuItemSaveCSV.Image = ZLINK31E.My.Resources.Resources.saveAs_2_p;
			ToolStripMenuItemSaveCSV.Name = "ToolStripMenuItemSaveCSV";
			size2 = (ToolStripMenuItemSaveCSV.Size = new System.Drawing.Size(166, 22));
			ToolStripMenuItemSaveCSV.Text = "Save as CSV ...";
			ToolStripMenuItemAddCsv.Image = ZLINK31E.My.Resources.Resources.saveAs_1_g;
			ToolStripMenuItemAddCsv.Name = "ToolStripMenuItemAddCsv";
			size2 = (ToolStripMenuItemAddCsv.Size = new System.Drawing.Size(166, 22));
			ToolStripMenuItemAddCsv.Text = "Add to CSV ...";
			ToolStripMenuItemOpen.Image = (System.Drawing.Image)resources.GetObject("ToolStripMenuItemOpen.Image");
			ToolStripMenuItemOpen.Name = "ToolStripMenuItemOpen";
			size2 = (ToolStripMenuItemOpen.Size = new System.Drawing.Size(169, 22));
			ToolStripMenuItemOpen.Text = "Open";
			ToolStripMenuItemPrintConfig.Image = ZLINK31E.My.Resources.Resources.win_div_3b_b;
			ToolStripMenuItemPrintConfig.Name = "ToolStripMenuItemPrintConfig";
			size2 = (ToolStripMenuItemPrintConfig.Size = new System.Drawing.Size(169, 22));
			ToolStripMenuItemPrintConfig.Text = "Print Config ...";
			ToolStripMenuItemPreview.Image = ZLINK31E.My.Resources.Resources.win_search_y;
			ToolStripMenuItemPreview.Name = "ToolStripMenuItemPreview";
			size2 = (ToolStripMenuItemPreview.Size = new System.Drawing.Size(169, 22));
			ToolStripMenuItemPreview.Text = "Print Preview ...";
			ToolStripMenuItemPrint.Image = ZLINK31E.My.Resources.Resources.print_none_p;
			ToolStripMenuItemPrint.Name = "ToolStripMenuItemPrint";
			size2 = (ToolStripMenuItemPrint.Size = new System.Drawing.Size(169, 22));
			ToolStripMenuItemPrint.Text = "Print Report ...";
			ToolStripSeparator1.Name = "ToolStripSeparator1";
			size2 = (ToolStripSeparator1.Size = new System.Drawing.Size(166, 6));
			ToolStripMenuItemQuit.Image = (System.Drawing.Image)resources.GetObject("ToolStripMenuItemQuit.Image");
			ToolStripMenuItemQuit.Name = "ToolStripMenuItemQuit";
			size2 = (ToolStripMenuItemQuit.Size = new System.Drawing.Size(169, 22));
			ToolStripMenuItemQuit.Text = "Quit";
			ToolStripMenuItemSoftwareConfigMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2]
			{
				ToolStripMenuItemConnection,
				ToolStripMenuItemAuto
			});
			ToolStripMenuItemSoftwareConfigMenu.Name = "ToolStripMenuItemSoftwareConfigMenu";
			size2 = (ToolStripMenuItemSoftwareConfigMenu.Size = new System.Drawing.Size(73, 22));
			ToolStripMenuItemSoftwareConfigMenu.Text = "Software";
			ToolStripMenuItemConnection.Image = (System.Drawing.Image)resources.GetObject("ToolStripMenuItemConnection.Image");
			ToolStripMenuItemConnection.Name = "ToolStripMenuItemConnection";
			size2 = (ToolStripMenuItemConnection.Size = new System.Drawing.Size(180, 22));
			ToolStripMenuItemConnection.Text = "Connection ...";
			ToolStripMenuItemAuto.Image = (System.Drawing.Image)resources.GetObject("ToolStripMenuItemAuto.Image");
			ToolStripMenuItemAuto.Name = "ToolStripMenuItemAuto";
			size2 = (ToolStripMenuItemAuto.Size = new System.Drawing.Size(180, 22));
			ToolStripMenuItemAuto.Text = "Auto Recording ...";
			ToolStripMenuItemGaugeConfig.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1]
			{
				ToolStripMenuItemConfig
			});
			ToolStripMenuItemGaugeConfig.Name = "ToolStripMenuItemGaugeConfig";
			size2 = (ToolStripMenuItemGaugeConfig.Size = new System.Drawing.Size(92, 22));
			ToolStripMenuItemGaugeConfig.Text = "Instruments";
			ToolStripMenuItemConfig.Image = (System.Drawing.Image)resources.GetObject("ToolStripMenuItemConfig.Image");
			ToolStripMenuItemConfig.Name = "ToolStripMenuItemConfig";
			size2 = (ToolStripMenuItemConfig.Size = new System.Drawing.Size(147, 22));
			ToolStripMenuItemConfig.Text = "Configure ...";
			ToolStripMenuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2]
			{
				ToolStripMenuItemHelpContents,
				ToolStripMenuItemAbout
			});
			ToolStripMenuItemHelp.Name = "ToolStripMenuItemHelp";
			size2 = (ToolStripMenuItemHelp.Size = new System.Drawing.Size(46, 22));
			ToolStripMenuItemHelp.Text = "Help";
			ToolStripMenuItemHelpContents.Image = (System.Drawing.Image)resources.GetObject("ToolStripMenuItemHelpContents.Image");
			ToolStripMenuItemHelpContents.Name = "ToolStripMenuItemHelpContents";
			size2 = (ToolStripMenuItemHelpContents.Size = new System.Drawing.Size(202, 22));
			ToolStripMenuItemHelpContents.Text = "Index";
			ToolStripMenuItemAbout.Image = (System.Drawing.Image)resources.GetObject("ToolStripMenuItemAbout.Image");
			ToolStripMenuItemAbout.Name = "ToolStripMenuItemAbout";
			size2 = (ToolStripMenuItemAbout.Size = new System.Drawing.Size(202, 22));
			ToolStripMenuItemAbout.Text = "About this software...";
			StatusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[4]
			{
				ToolStripStatusLabelConnection,
				ToolStripStatusLabelModel,
				ToolStripStatusLabelStatus,
				ToolStripStatusLabelWaiting
			});
			point2 = (StatusStripMain.Location = new System.Drawing.Point(0, 559));
			StatusStripMain.Name = "StatusStripMain";
			padding2 = (StatusStripMain.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0));
			size2 = (StatusStripMain.Size = new System.Drawing.Size(807, 23));
			StatusStripMain.TabIndex = 2;
			ToolStripStatusLabelConnection.Name = "ToolStripStatusLabelConnection";
			size2 = (ToolStripStatusLabelConnection.Size = new System.Drawing.Size(55, 18));
			ToolStripStatusLabelConnection.Text = "Connect";
			ToolStripStatusLabelModel.Name = "ToolStripStatusLabelModel";
			size2 = (ToolStripStatusLabelModel.Size = new System.Drawing.Size(42, 18));
			ToolStripStatusLabelModel.Text = "Model";
			ToolStripStatusLabelStatus.Name = "ToolStripStatusLabelStatus";
			size2 = (ToolStripStatusLabelStatus.Size = new System.Drawing.Size(46, 18));
			ToolStripStatusLabelStatus.Text = "Status";
			ToolStripStatusLabelWaiting.Name = "ToolStripStatusLabelWaiting";
			size2 = (ToolStripStatusLabelWaiting.Size = new System.Drawing.Size(0, 18));
			SaveFileDialogCSV.DefaultExt = "csv";
			SaveFileDialogCSV.Filter = "CSV File|*.csv";
			SaveFileDialogCSV.Title = "CSVファイル保存";
			OpenFileDialogCsvAdd.DefaultExt = "csv";
			OpenFileDialogCsvAdd.FileName = "OpenFileDialogCsvAdd";
			OpenFileDialogCsvAdd.Filter = "CSV File|*.csv";
			OpenFileDialogCsvAdd.Title = "CSVファイル追記";
			OpenFileDialogCsvRead.Filter = "CSV File|*.csv";
			OpenFileDialogCsvRead.Title = "CSVファイルを開く";
			BackgroundWorkerRealTime.WorkerSupportsCancellation = true;
			PrintDocument1.DocumentName = "ZLINK3 - Report";
			size2 = (PrintPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0));
			size2 = (PrintPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0));
			size2 = (PrintPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300));
			PrintPreviewDialog1.Document = PrintDocument1;
			PrintPreviewDialog1.Enabled = true;
			PrintPreviewDialog1.Icon = (System.Drawing.Icon)resources.GetObject("PrintPreviewDialog1.Icon");
			PrintPreviewDialog1.Name = "PrintPreviewDialog1";
			PrintPreviewDialog1.Visible = false;
			PrintDialog1.Document = PrintDocument1;
			PrintDialog1.UseEXDialog = true;
			ToolStripButton1.Image = (System.Drawing.Image)resources.GetObject("ToolStripButton1.Image");
			ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			ToolStripButton1.Name = "ToolStripButton1";
			size2 = (ToolStripButton1.Size = new System.Drawing.Size(49, 22));
			ToolStripButton1.Text = "先頭";
			ToolStripButton2.Image = (System.Drawing.Image)resources.GetObject("ToolStripButton2.Image");
			ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
			ToolStripButton2.Name = "ToolStripButton2";
			size2 = (ToolStripButton2.Size = new System.Drawing.Size(49, 22));
			ToolStripButton2.Text = "開始";
			ToolStripButton3.Image = (System.Drawing.Image)resources.GetObject("ToolStripButton3.Image");
			ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
			ToolStripButton3.Name = "ToolStripButton3";
			size2 = (ToolStripButton3.Size = new System.Drawing.Size(49, 22));
			ToolStripButton3.Text = "停止";
			ToolStripButton4.Image = (System.Drawing.Image)resources.GetObject("ToolStripButton4.Image");
			ToolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
			ToolStripButton4.Name = "ToolStripButton4";
			size2 = (ToolStripButton4.Size = new System.Drawing.Size(49, 22));
			ToolStripButton4.Text = "クリア";
			System.Drawing.SizeF sizeF2 = AutoScaleDimensions = new System.Drawing.SizeF(7f, 15f);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			size2 = (ClientSize = new System.Drawing.Size(807, 582));
			Controls.Add(TabControlMain);
			Controls.Add(StatusStripMain);
			Controls.Add(MenuStripMain);
			DoubleBuffered = true;
			Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			padding2 = (Margin = new System.Windows.Forms.Padding(3, 4, 3, 4));
			size2 = (MaximumSize = new System.Drawing.Size(989, 866));
			size2 = (MinimumSize = new System.Drawing.Size(744, 591));
			Name = "FormMain";
			Text = "IMADA ZLINK 3";
			TabControlMain.ResumeLayout(false);
			TabPageIndependent.ResumeLayout(false);
			TabPageIndependent.PerformLayout();
			Panel1.ResumeLayout(false);
			Panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)DataGridViewValue).EndInit();
			((System.ComponentModel.ISupportInitialize)DataSetValue).EndInit();
			((System.ComponentModel.ISupportInitialize)DataTableValue).EndInit();
			ToolStrip2.ResumeLayout(false);
			ToolStrip2.PerformLayout();
			ContextMenuStripUpDown.ResumeLayout(false);
			TabPageRealTime.ResumeLayout(false);
			TabPageRealTime.PerformLayout();
			Panel2.ResumeLayout(false);
			Panel3.ResumeLayout(false);
			Panel3.PerformLayout();
			GroupBox1.ResumeLayout(false);
			GroupBox1.PerformLayout();
			TableLayoutPanel2.ResumeLayout(false);
			TableLayoutPanel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)PictureBoxChart).EndInit();
			((System.ComponentModel.ISupportInitialize)PictureBoxScaleY).EndInit();
			((System.ComponentModel.ISupportInitialize)PictureBoxScaleX).EndInit();
			ToolStrip1.ResumeLayout(false);
			ToolStrip1.PerformLayout();
			MenuStripMain.ResumeLayout(false);
			MenuStripMain.PerformLayout();
			StatusStripMain.ResumeLayout(false);
			StatusStripMain.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}

		//protected override void Finalize()
		//{
		//	base.Finalize();
		//}

		private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DataTableValue.Rows.Count <= 0)
			{
				return;
			}
			switch (MessageBox.Show(Resources.CAUTION_DOSAVE, Resources.CAUTION, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
			{
			case DialogResult.Abort:
			case DialogResult.Retry:
			case DialogResult.Ignore:
				break;
			case DialogResult.Yes:
				if (SaveFileDialogCSV.ShowDialog() == DialogResult.OK)
				{
					saveAsCsv(SaveFileDialogCSV.FileName);
				}
				else
				{
					e.Cancel = true;
				}
				break;
			case DialogResult.Cancel:
				e.Cancel = true;
				break;
			}
		}

		private void FormMain_Load(object sender, EventArgs e)
		{
			timerMonitor = new MultimediaTimer(100u);
			timerGet = new MultimediaTimer(50u);
		}

		private void FormMain_Shown(object sender, EventArgs e)
		{
			openPortSelectModel();
			TabControlMain.SelectTab(TabPageIndependent);
		}

		private void disableOperation()
		{
			ToolStripMenuItemGaugeConfig.Enabled = false;
			TabControlMain.Enabled = false;
			timerMonitor.Enabled = false;
			timerGet.Enabled = false;
			ToolStripMenuItemAuto.Enabled = false;
			ToolStripMenuItemGaugeConfig.Enabled = false;
			ButtonPeak.Enabled = false;
			ButtonZero.Enabled = false;
			ToolStripStatusLabelModel.Image = Resources.question;
			ToolStripStatusLabelModel.Text = "Unknown";
			changeConnectionStatusBar(isAlive: false);
		}

		private void enableOperation()
		{
			ToolStripMenuItemGaugeConfig.Enabled = true;
			TabControlMain.Enabled = true;
			ToolStripMenuItemAuto.Enabled = true;
			ToolStripMenuItemGaugeConfig.Enabled = true;
			ButtonPeak.Enabled = true;
			ButtonZero.Enabled = true;
		}

		private void openPortSelectModel()
		{
			disableOperation();
			FormSelectPort formSelectPort = new FormSelectPort();
			if (formSelectPort.ShowDialog(this) == DialogResult.Cancel)
			{
				disableOperation();
				return;
			}
			gauge = AbstractForceGauge.AbstractForceGaugeCreator(formSelectPort.Compatibility);
			switch (formSelectPort.Compatibility)
			{
			case GaugeCompatibility.Dps:
				ToolStripStatusLabelModel.Image = Resources.dps;
				ToolStripStatusLabelModel.Text = "DPS";
				break;
			case GaugeCompatibility.Dpx:
				ToolStripStatusLabelModel.Image = Resources.dpx;
				ToolStripStatusLabelModel.Text = "DPX";
				break;
			case GaugeCompatibility.Dpz:
				ToolStripStatusLabelModel.Image = Resources.dpz;
				ToolStripStatusLabelModel.Text = "DPZ Compatible";
				break;
			case GaugeCompatibility.Ds2:
				ToolStripStatusLabelModel.Image = Resources.ds2;
				ToolStripStatusLabelModel.Text = "DS2";
				break;
			case GaugeCompatibility.Z:
				ToolStripStatusLabelModel.Image = Resources.zp;
				ToolStripStatusLabelModel.Text = "Z Compatible";
				break;
			default:
				disableOperation();
				return;
			}
			gauge.Connect(formSelectPort.PortName);
			gauge.SendRead("D");
			gauge.ClearBuffer();
			switch (gauge.Series)
			{
			case GaugeCompatibility.Dps:
			case GaugeCompatibility.Ds2:
				ToolStripButtonMemoryClear.Enabled = false;
				ToolStripButtonMemoryRequest.Enabled = false;
				break;
			default:
				ToolStripButtonMemoryClear.Enabled = true;
				ToolStripButtonMemoryRequest.Enabled = true;
				break;
			}
			switch (gauge.Series)
			{
			case GaugeCompatibility.Dpz:
			case GaugeCompatibility.Z:
			case GaugeCompatibility.Ds2:
			{
				Dictionary<string, string> unitStrings = gauge.GetUnitStrings();
				unit_strings[0] = unitStrings["K"];
				unit_strings[1] = unitStrings["N"];
				unit_strings[2] = unitStrings["O"];
				break;
			}
			}
			enableOperation();
			timerGet.Enabled = false;
			timerMonitor.Enabled = false;
			TabControlMain.SelectTab(TabPageIndependent);
			update_method = addData;
			initIndependentTab();
		}

		private void dataSetValue_add(string buffer)
		{
			DataRow dataRow = DataTableValue.NewRow();
			dataRow[0] = Conversion.Val(buffer).ToString();
			switch (buffer[6])
			{
			case 'K':
				dataRow[1] = unit_strings[0];
				break;
			case 'N':
				dataRow[1] = unit_strings[1];
				break;
			case 'L':
			case 'O':
				dataRow[1] = unit_strings[2];
				break;
			}
			DataTableValue.Rows.Add(dataRow);
		}

		private void TabControlMain_SelectedIndexChanged(object sender, EventArgs e)
		{
			timerGet.Enabled = false;
			timerMonitor.Enabled = false;
			if (TabControlMain.SelectedTab == TabPageRealTime)
			{
				ToolStripMenuItemConnection.Enabled = false;
				initRealtimeTab();
			}
			else
			{
				ToolStripMenuItemConnection.Enabled = true;
				initIndependentTab();
			}
		}

		private bool isConnectionAlive()
		{
			try
			{
				gauge.ClearBuffer();
				gauge.Send("Dummy");
				string text = gauge.Read();
				gauge.ClearBuffer();
				return true;
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				bool result = false;
				ProjectData.ClearProjectError();
				return result;
			}
		}

		private void changeConnectionStatusBar(bool isAlive)
		{
			if (isAlive)
			{
				ToolStripStatusLabelConnection.Text = Resources.CAPTION_CONNECT;
				ToolStripStatusLabelConnection.Image = Resources.connect;
			}
			else
			{
				ToolStripStatusLabelConnection.Text = Resources.CAPTION_DISCONNECT;
				ToolStripStatusLabelConnection.Image = Resources.con_off_y;
			}
		}

		private void initIndependentTab()
		{
			timerGet.Enabled = false;
			BackgroundWorkerRealTime.CancelAsync();
			ToolStripStatusLabelStatus.Image = Resources.doc;
			ToolStripStatusLabelStatus.Text = Resources.INDEPENDENT;
			reflectDatasetTotal();
			updateTotalDisplay();
			Thread.Sleep(150);
			gauge.ClearBuffer();
			changeConnectionStatusBar(isConnectionAlive());
			gauge.ClearBuffer();
			ToolStripMenuItemGaugeConfig.Enabled = true;
			gauge.EnableResponse = true;
			MouseWheel -= FormMain_MouseWheel;
		}

		private void initRealtimeTab()
		{
			gauge.EnableResponse = false;
			ToolStripStatusLabelStatus.Image = Resources.red_s;
			ToolStripStatusLabelStatus.Text = Resources.STAY;
			changeConnectionStatusBar(isConnectionAlive());
			gauge.ClearBuffer();
			Thread.Sleep(150);
			updateAutoValues();
			timerMonitor.Enabled = true;
			if (!BackgroundWorkerRealTime.IsBusy)
			{
				BackgroundWorkerRealTime.RunWorkerAsync();
			}
			ToolStripMenuItemGaugeConfig.Enabled = false;
			initBuffers();
			drawDetailOnBackG();
			ToolStripComboBoxXaxis.Text = currentScale.XStep.ToString();
			ToolStripComboBoxYaxis.Text = currentScale.YStep.ToString();
			MouseWheel += FormMain_MouseWheel;
		}

		private void ToolStripMenuItemAuto_Click(object sender, EventArgs e)
		{
			if (timerGet.Enabled)
			{
				return;
			}
			FormAutoRecord formAutoRecord = new FormAutoRecord(autoConfig);
			if (formAutoRecord.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			autoConfig = formAutoRecord.Config;
			checked
			{
				uint num = (uint)Math.Round(Conversion.Val(formAutoRecord.TextBoxInterval.Text) * 1000.0);
				if (timerGet.Interval != num)
				{
					switch (MessageBox.Show(Resources.CAUTION_CHANGE_INTERVAL1 + "\r" + Resources.CAUTION_CHANGE_INTERVAL2, Resources.CAUTION, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation))
					{
					case DialogResult.Yes:
						clearAll();
						timerGet.Interval = num;
						break;
					case DialogResult.No:
						timerGet.Interval = num;
						break;
					}
				}
				ToolStripButtonStart.Enabled = !autoConfig.IsTriggerEnable;
				finishCounterLimit = (ulong)Math.Round((float)(double)autoConfig.FinishTime / autoConfig.Interval) + 1uL;
				if (finishCounterLimit < 2)
				{
					finishCounterLimit = 2uL;
				}
				updateAutoValues();
			}
		}

		private void updateAutoValues()
		{
			LabelTrigerTitle.Enabled = autoConfig.IsTriggerEnable;
			LabelTriggerValue.Enabled = autoConfig.IsTriggerEnable;
			LabelTriggerValue.Text = autoConfig.TriggerValue.ToString();
			LabelInterval.Text = ((double)timerGet.Interval / 1000.0).ToString();
			LabelFinishTime.Enabled = autoConfig.IsFinishTimeEnable;
			LabelFinishTimeTitle.Enabled = autoConfig.IsFinishTimeEnable;
			LabelFinishTime.Text = autoConfig.FinishTime.ToString();
		}

		private void ToolStripMenuItemConfig_Click(object sender, EventArgs e)
		{
			bool enableResponse = gauge.EnableResponse;
			gauge.EnableResponse = false;
			FormGaugeConfig formGaugeConfig = new FormGaugeConfig(gauge);
			formGaugeConfig.ShowDialog();
			gauge.EnableResponse = enableResponse;
		}

		public static int HalfAdjust(double dValue)
		{
			checked
			{
				if (dValue > 0.0)
				{
					return (int)Math.Round(Math.Floor(dValue + 0.5));
				}
				return (int)Math.Round(Math.Ceiling(dValue - 0.5));
			}
		}

		private void gauge_DataRecieved(string e)
		{
			try
			{
				BeginInvoke(update_method, e);
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				MessageBox.Show(Resources.ERROR_DATAOVERFLOW, Resources.ERROR_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				gauge.ClearBuffer();
				ProjectData.ClearProjectError();
			}
		}

		private void addData(string e)
		{
			switch (e[0])
			{
			case '!':
			case '"':
			case '#':
			case '$':
			case '%':
			case '&':
			case '\'':
			case '(':
			case ')':
			case '*':
			case ',':
				break;
			case ' ':
			case '+':
			case '-':
				if (e.Length >= 8)
				{
					if ((Operators.CompareString(Conversions.ToString(e[1]), "+", TextCompare: false) == 0) | (Operators.CompareString(Conversions.ToString(e[1]), "-", TextCompare: false) == 0) | (Operators.CompareString(Conversions.ToString(e[1]), " ", TextCompare: false) == 0))
					{
						e = e.Substring(1);
					}
					dataSetValue_add(e);
					stats.Update((float)Conversion.Val(e));
					updateTotalDisplay();
				}
				break;
			}
		}

		private void DataGridViewValue_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
		{
			DataGridViewValue.FirstDisplayedScrollingRowIndex = checked(DataGridViewValue.RowCount - 1);
		}

		private void updateTotalDisplay()
		{
			Statistics statistics = stats;
			if (decimal.Compare(new decimal(statistics.Count), 0m) == 0)
			{
				LabelNumber.Text = "0";
				LabelAverage.Text = "0";
				LabelMaximum.Text = "0";
				LabelMinimum.Text = "0";
			}
			else
			{
				LabelNumber.Text = statistics.Count.ToString();
				LabelAverage.Text = (Math.Round(statistics.Average * 1000f) / 1000.0).ToString();
				LabelMaximum.Text = statistics.Maximum.ToString();
				LabelMinimum.Text = statistics.Minimum.ToString();
			}
			statistics = null;
		}

		private void reflectDatasetTotal()
		{
			List<float> list = new List<float>();
			stats.Clear();
			IEnumerator enumerator = default(IEnumerator);
			try
			{
				enumerator = DataTableValue.Rows.GetEnumerator();
				while (enumerator.MoveNext())
				{
					DataRow dataRow = (DataRow)enumerator.Current;
					stats.Update(Conversion.Val(RuntimeHelpers.GetObjectValue(dataRow[0])));
				}
			}
			finally
			{
				if (enumerator is IDisposable)
				{
					(enumerator as IDisposable).Dispose();
				}
			}
		}

		private void ToolStripButtonGet_Click(object sender, EventArgs e)
		{
			gauge.RequestValue();
		}

		private void ToolStripButtonRequestMemory_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show(Resources.CAUTION_ERASE_BEFORE, Resources.CAUTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				clearAll();
			}
			UseWaitCursor = true;
			gauge.EnableResponse = false;
			gauge.ClearBuffer();
			ToolStripStatusLabelStatus.Image = Resources.t2;
			ToolStripStatusLabelStatus.Text = Resources.CAPTION_MEMREQUEST;
			StatusStripMain.Refresh();
			List<string> memory = gauge.GetMemory();
			foreach (string item in memory)
			{
				addData(item);
			}
			ToolStripStatusLabelStatus.Image = Resources.doc;
			ToolStripStatusLabelStatus.Text = Resources.INDEPENDENT;
			UseWaitCursor = false;
			gauge.EnableResponse = true;
		}

		private void clearAll()
		{
			DataTableValue.Clear();
			stats = new Statistics();
			updateTotalDisplay();
			markedPointNumber0 = -1;
			markedPointNumber1 = -1;
		}

		private void ToolStripButtonClear1_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show(Resources.CAUTION_CLEAR, Resources.CAUTION, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
			{
				clearAll();
			}
		}

		private void ToolStripButtonMemoryClear_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show(Resources.CAUTION_MEMCLEAR, Resources.CAUTION, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
			{
				gauge.ClearMemory();
			}
		}

		private void ToolStripButtonFirst1_Click(object sender, EventArgs e)
		{
			if (DataTableValue.Rows.Count >= 2)
			{
				DataGridViewValue.FirstDisplayedScrollingRowIndex = 0;
			}
		}

		private void ToolStripMenuItemCsvSave_Click(object sender, EventArgs e)
		{
			if (DataTableValue.Rows.Count >= 1 && SaveFileDialogCSV.ShowDialog() == DialogResult.OK)
			{
				saveAsCsv(SaveFileDialogCSV.FileName);
			}
		}

		private void ToolStripMenuItemCsvAdd_Click(object sender, EventArgs e)
		{
			if (DataTableValue.Rows.Count >= 1 && OpenFileDialogCsvAdd.ShowDialog() == DialogResult.OK)
			{
				addToCsv(OpenFileDialogCsvAdd.FileName);
			}
		}

		private void saveAsCsv(string file_name)
		{
			StreamWriter streamWriter = new StreamWriter(file_name);
			streamWriter.WriteLine(RuntimeHelpers.GetObjectValue(DataTableValue.Rows[0][1]));
			streamWriter.WriteLine(timerGet.Interval.ToString());
			IEnumerator enumerator = default(IEnumerator);
			try
			{
				enumerator = DataTableValue.Rows.GetEnumerator();
				while (enumerator.MoveNext())
				{
					DataRow dataRow = (DataRow)enumerator.Current;
					streamWriter.WriteLine(dataRow[0].ToString());
				}
			}
			finally
			{
				if (enumerator is IDisposable)
				{
					(enumerator as IDisposable).Dispose();
				}
			}
			streamWriter.Close();
		}

		private void addToCsv(string file_name)
		{
			//Discarded unreachable code: IL_0117
			string empty = string.Empty;
			empty = empty + DataTableValue.Rows[0][1].ToString() + "\r";
			empty = empty + timerGet.Interval.ToString() + "\r";
			IEnumerator enumerator = default(IEnumerator);
			try
			{
				enumerator = DataTableValue.Rows.GetEnumerator();
				while (enumerator.MoveNext())
				{
					DataRow dataRow = (DataRow)enumerator.Current;
					empty = empty + dataRow[0].ToString() + "\r";
				}
			}
			finally
			{
				if (enumerator is IDisposable)
				{
					(enumerator as IDisposable).Dispose();
				}
			}
			using (StreamReader streamReader = new StreamReader(file_name))
			{
				using (StreamWriter streamWriter = new StreamWriter("imadummy.imd"))
				{
					StringReader stringReader = new StringReader(empty);
					bool flag = true;
					bool flag2 = true;
					while (flag | flag2)
					{
						string text = stringReader.ReadLine();
						if (text == null)
						{
							flag = false;
						}
						streamWriter.Write(text);
						streamWriter.Write(",");
						text = streamReader.ReadLine();
						if (text == null)
						{
							flag2 = false;
						}
						streamWriter.WriteLine(text);
					}
				}
			}
			File.Delete(file_name);
			File.Move("imadummy.imd", file_name);
		}

		private void ToolStripMenuItemOpen_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show(Resources.CAUTION_OPEN_FILE, Resources.CAUTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes || OpenFileDialogCsvRead.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			DataTable original = DataTableValue;
			FormProgressReading formProgressReading = new FormProgressReading(ref original, OpenFileDialogCsvRead.FileName);
			DataTableValue = original;
			FormProgressReading formProgressReading2 = formProgressReading;
			switch (formProgressReading2.ShowDialog())
			{
			case DialogResult.OK:
				DataTableValue = formProgressReading2.Result.Copy();
				timerGet.Interval = formProgressReading2.TimerInterval;
				DataGridViewValue.DataSource = DataTableValue;
				DataGridViewValue.Invalidate();
				reflectDatasetTotal();
				updateTotalDisplay();
				TabControlMain.Invalidate();
				if (TabControlMain.SelectedIndex == 1)
				{
					if (DataTableValue.Rows.Count < 2)
					{
						currentScale.XStart = 0f;
					}
					else
					{
						currentScale = getScalewhole();
					}
					updateDetailGraph();
				}
				break;
			case DialogResult.Abort:
				MessageBox.Show(Resources.MESSAGE_READ_ABORT);
				break;
			case DialogResult.No:
				MessageBox.Show(Resources.ERROR_READ, Resources.ERROR_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				break;
			}
			formProgressReading2.Dispose();
		}

		private void ToolStripMenuItemOpen_Click_old(object sender, EventArgs e)
		{
			//Discarded unreachable code: IL_0127
			if (MessageBox.Show(Resources.CAUTION_OPEN_FILE, Resources.CAUTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes || OpenFileDialogCsvRead.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			DataTable dataTable = DataTableValue.Clone();
			dataTable.Clear();
			int num = 0;
			int num2 = 0;
			checked
			{
				uint interval;
				try
				{
					using (StreamReader streamReader = new StreamReader(OpenFileDialogCsvRead.FileName))
					{
						string text = streamReader.ReadLine();
						string[] array = text.Split(',');
						string value = array[0];
						text = streamReader.ReadLine();
						string[] array2 = text.Split(',');
						interval = (uint)Math.Round(Conversion.Val(array2[0]));
						while (streamReader.Peek() > 0)
						{
							try
							{
								DataRow dataRow = dataTable.NewRow();
								string text2 = streamReader.ReadLine();
								string[] array3 = text2.Split(',');
								dataRow[0] = array3[0];
								dataRow[1] = value;
								dataTable.Rows.Add(dataRow);
								num2++;
							}
							catch (Exception ex)
							{
								ProjectData.SetProjectError(ex);
								Exception ex2 = ex;
								num++;
								ProjectData.ClearProjectError();
							}
						}
					}
				}
				catch (Exception projectError)
				{
					ProjectData.SetProjectError(projectError);
					MessageBox.Show(Resources.ERROR_READ, Resources.ERROR_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Hand);
					ProjectData.ClearProjectError();
					return;
				}
				copy_DataSetValue_from(dataTable);
				timerGet.Interval = interval;
				if (TabControlMain.SelectedIndex == 1)
				{
					if (DataTableValue.Rows.Count < 2)
					{
						currentScale.XStart = 0f;
					}
					else
					{
						currentScale = getScalewhole();
					}
					updateDetailGraph();
				}
			}
		}

		private void PictureBoxChart_SizeChanged(object sender, EventArgs e)
		{
			if (!((PictureBoxChart.Width == 0) | (PictureBoxChart.Height == 0)) && TabControlMain.SelectedIndex == 1)
			{
				if (!timerGet.Enabled)
				{
					initBuffers();
				}
				drawDetailOnBackG();
			}
		}

		private void initBuffers()
		{
			backBuffer = new Bitmap(PictureBoxChart.Width, PictureBoxChart.Height);
			subBuffer = new Bitmap(backBuffer);
			backG = Graphics.FromImage(backBuffer);
			frontG = PictureBoxChart.CreateGraphics();
			scaleBuffer = new Bitmap(PictureBoxChart.Width, PictureBoxChart.Height);
			drawXyScaleOnBack();
		}

		private void PictureBoxChart_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.DrawImage(backBuffer, 0, 0);
		}

		private void PictureBoxScaleX_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.DrawImage(backScalex, 0, 0);
		}

		private void PictureBoxScaleY_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.DrawImage(backScaley, 0, 0);
		}

		private void BackgroundWorkerRealTime_DoWork(object sender, DoWorkEventArgs e)
		{
			string text = "";
			Monitor.Enter(_0024STATIC_0024BackgroundWorkerRealTime_DoWork_002420211C12819_0024error_count_0024Init);
			try
			{
				if (_0024STATIC_0024BackgroundWorkerRealTime_DoWork_002420211C12819_0024error_count_0024Init.State == 0)
				{
					_0024STATIC_0024BackgroundWorkerRealTime_DoWork_002420211C12819_0024error_count_0024Init.State = 2;
					_0024STATIC_0024BackgroundWorkerRealTime_DoWork_002420211C12819_0024error_count = 0;
				}
				else if (_0024STATIC_0024BackgroundWorkerRealTime_DoWork_002420211C12819_0024error_count_0024Init.State == 2)
				{
					throw new IncompleteInitialization();
				}
			}
			finally
			{
				_0024STATIC_0024BackgroundWorkerRealTime_DoWork_002420211C12819_0024error_count_0024Init.State = 1;
				Monitor.Exit(_0024STATIC_0024BackgroundWorkerRealTime_DoWork_002420211C12819_0024error_count_0024Init);
			}
			Monitor.Enter(_0024STATIC_0024BackgroundWorkerRealTime_DoWork_002420211C12819_0024firstError_0024Init);
			try
			{
				if (_0024STATIC_0024BackgroundWorkerRealTime_DoWork_002420211C12819_0024firstError_0024Init.State == 0)
				{
					_0024STATIC_0024BackgroundWorkerRealTime_DoWork_002420211C12819_0024firstError_0024Init.State = 2;
					_0024STATIC_0024BackgroundWorkerRealTime_DoWork_002420211C12819_0024firstError = false;
				}
				else if (_0024STATIC_0024BackgroundWorkerRealTime_DoWork_002420211C12819_0024firstError_0024Init.State == 2)
				{
					throw new IncompleteInitialization();
				}
			}
			finally
			{
				_0024STATIC_0024BackgroundWorkerRealTime_DoWork_002420211C12819_0024firstError_0024Init.State = 1;
				Monitor.Exit(_0024STATIC_0024BackgroundWorkerRealTime_DoWork_002420211C12819_0024firstError_0024Init);
			}
			changeConnectionStatusBar(isConnectionAlive());
			checked
			{
				while (true)
				{
					try
					{
						if (BackgroundWorkerRealTime.CancellationPending)
						{
							e.Cancel = true;
							return;
						}
						text = issueCommand;
						gauge.Send(text);
						string text2 = gauge.Read();
						if ((text2 != null) & (text2.Length >= 8))
						{
							switch (text2[0])
							{
							case ' ':
							case '+':
							case '-':
								updatingValue = text2;
								_0024STATIC_0024BackgroundWorkerRealTime_DoWork_002420211C12819_0024error_count = 0;
								break;
							}
						}
						if (Operators.CompareString(text, "D", TextCompare: false) != 0)
						{
							issueCommand = "D";
						}
					}
					catch (TimeoutException ex)
					{
						ProjectData.SetProjectError(ex);
						TimeoutException ex2 = ex;
						_0024STATIC_0024BackgroundWorkerRealTime_DoWork_002420211C12819_0024error_count++;
						if (_0024STATIC_0024BackgroundWorkerRealTime_DoWork_002420211C12819_0024error_count > 2)
						{
							changeConnectionStatusBar(isAlive: false);
							if (timerGet.Enabled)
							{
								e.Result = "REC";
							}
							else
							{
								e.Result = "DIS";
							}
							ProjectData.ClearProjectError();
							return;
						}
						ProjectData.ClearProjectError();
					}
				}
			}
		}

		private void BackgroundWorkerRealTime_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Cancelled)
			{
				return;
			}
			if (Operators.CompareString(Conversions.ToString(e.Result), "REC", TextCompare: false) == 0)
			{
				endMonitor();
				return;
			}
			DialogResult dialogResult = MessageBox.Show(Resources.ERROR_DISCONNECT, Resources.ERROR_CAPTION, MessageBoxButtons.RetryCancel, MessageBoxIcon.Hand);
			if (dialogResult == DialogResult.Retry)
			{
				BackgroundWorkerRealTime.RunWorkerAsync();
			}
		}

		private void ButtonZero_Click(object sender, EventArgs e)
		{
			issueCommand = "Z";
		}

		private void ButtonPeak_Click(object sender, EventArgs e)
		{
			issueCommand = "P";
		}

		private void ButtonTrack_Click(object sender, EventArgs e)
		{
			issueCommand = "T";
		}

		private void TimerMonitor_Elapsed()
		{
			try
			{
				BeginInvoke(new updateMonitorDelegate(updateMonitor));
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				ProjectData.ClearProjectError();
			}
		}

		private void updateMonitor()
		{
			if ((updatingValue == null) | (updatingValue.Length < 8))
			{
				return;
			}
			LabelValue.Text = updatingValue.Substring(0, 6);
			switch (updatingValue[6])
			{
			case 'K':
				LabelUnit.Text = unit_strings[0];
				break;
			case 'N':
				LabelUnit.Text = unit_strings[1];
				break;
			case 'L':
			case 'O':
				LabelUnit.Text = unit_strings[2];
				break;
			}
			LabelTrigerTitle.Text = Resources.CAPTION_TRIGGER + "(" + LabelUnit.Text + ")";
			if (Operators.CompareString(Conversions.ToString(updatingValue[7]), "P", TextCompare: false) == 0)
			{
				LabelPeak.Enabled = true;
			}
			else
			{
				LabelPeak.Enabled = false;
			}
			if (timerGet.Enabled)
			{
				updateMonitorGraph();
			}
			if (autoConfig.IsTriggerEnable)
			{
				float num = (float)Math.Abs(Conversion.Val(updatingValue));
				if (timerGet.Enabled & (num < autoConfig.TriggerValue))
				{
					endMonitor();
				}
				else if (!timerGet.Enabled & (num >= autoConfig.TriggerValue))
				{
					startMonitor();
				}
			}
			if (isCounterFinished)
			{
				isCounterFinished = false;
				endMonitor();
			}
		}

		private void drawMainScaleOnImage(Image img)
		{
			Graphics graphics = Graphics.FromImage(img);
			graphics.Clear(chartBackColor);
			float num = (float)img.Width / 10f;
			int num2 = 1;
			checked
			{
				do
				{
					graphics.DrawLine(scalePen, num * (float)num2, 0f, num * (float)num2, img.Height);
					num2++;
				}
				while (num2 <= 9);
				num = (float)img.Height / 11f;
				float num3 = (float)img.Height / 2f;
				int num4 = -5;
				do
				{
					float num5 = ScaleInfo.HalfAdjust(currentScale.YCenter - (float)num4 * currentScale.YStep, 3);
					Pen pen = (num5 != 0f) ? scalePen : origin_pen;
					graphics.DrawLine(pen, 0f, num * (float)num4 + num3, img.Width, num * (float)num4 + num3);
					num4++;
				}
				while (num4 <= 5);
				graphics.Dispose();
			}
		}

		private void drawXyScaleOnBack()
		{
			PanelCorner.BackColor = scaleBackColor;
			backScalex = new Bitmap(PictureBoxScaleX.Width, PictureBoxScaleX.Height);
			Graphics graphics = Graphics.FromImage(backScalex);
			float num = (float)PictureBoxScaleX.Width / 5f;
			Font font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point);
			StringFormat stringFormat = new StringFormat();
			graphics.Clear(scaleBackColor);
			stringFormat.Alignment = StringAlignment.Center;
			stringFormat.LineAlignment = StringAlignment.Center;
			int num2 = 1;
			checked
			{
				do
				{
					float num3 = ScaleInfo.HalfAdjust(currentScale.XStart + (float)num2 * currentScale.XStep * 2f, 1);
					graphics.DrawString(layoutRectangle: new RectangleF(num * (float)num2 - scaleFontRectWidth / 2f, 5f, scaleFontRectWidth, scaleFontRectHeight), s: num3.ToString(), font: font, brush: scaleFontBrush, format: stringFormat);
					num2++;
				}
				while (num2 <= 4);
				stringFormat.Alignment = StringAlignment.Far;
				stringFormat.LineAlignment = StringAlignment.Far;
				graphics.DrawString(layoutRectangle: new RectangleF(0f, 0f, backScalex.Width, backScalex.Height), s: Resources.CAPTION_SEC, font: new Font(FontFamily.GenericSerif, 8f, FontStyle.Regular, GraphicsUnit.Point), brush: scaleFontBrush, format: stringFormat);
				graphics.Dispose();
				backScaley = new Bitmap(PictureBoxScaleY.Width, PictureBoxScaleY.Height);
				graphics = Graphics.FromImage(backScaley);
				num = (float)PictureBoxScaleY.Height / 11f;
				stringFormat.Alignment = StringAlignment.Far;
				stringFormat.LineAlignment = StringAlignment.Center;
				float num4 = (float)PictureBoxScaleY.Height / 2f;
				graphics.Clear(scaleBackColor);
				int num5 = -5;
				do
				{
					float num6 = ScaleInfo.HalfAdjust(currentScale.YCenter - (float)num5 * currentScale.YStep, 3);
					graphics.DrawString(brush: (num6 != 0f) ? scaleFontBrush : scaleFontOriginBrush, layoutRectangle: new RectangleF(0f, num4 + num * (float)num5 - scaleFontRectHeight / 2f, PictureBoxScaleY.Width, scaleFontRectHeight), s: num6.ToString(), font: scaleFont, format: stringFormat);
					num5++;
				}
				while (num5 <= 5);
				graphics.Dispose();
			}
		}

		private void TimerGet_Elapsed()
		{
			temp_file_stream.Write((float)Conversion.Val(updatingValue));
			checked
			{
				finishTimeCounter++;
				if (autoConfig.IsFinishTimeEnable && finishTimeCounter >= finishCounterLimit)
				{
					timerGet.Enabled = false;
					isCounterFinished = true;
				}
			}
		}

		private void FirstGet()
		{
			temp_file_stream.Write((float)Conversion.Val(updatingValue));
			checked
			{
				finishTimeCounter++;
				if (autoConfig.IsFinishTimeEnable && finishTimeCounter >= finishCounterLimit)
				{
					timerGet.Enabled = false;
					isCounterFinished = true;
				}
			}
		}

		private void TabControlMain_Selecting(object sender, TabControlCancelEventArgs e)
		{
			if (timerGet.Enabled && object.ReferenceEquals(e.TabPage, TabPageIndependent))
			{
				e.Cancel = true;
			}
		}

		private void startMonitor()
		{
			StatusStripMain.SizingGrip = false;
			MaximizeBox = false;
			FormBorderStyle = FormBorderStyle.FixedSingle;
			ButtonPeak.Enabled = false;
			ButtonTrack.Enabled = false;
			ButtonZero.Enabled = false;
			ToolStripStatusLabelStatus.Image = Resources.brue_s;
			ToolStripStatusLabelStatus.Text = Resources.CAPTION_RECORDING;
			ToolStripMenuItemSoftwareConfigMenu.Enabled = false;
			ToolStripMenuItemFile.Enabled = false;
			ToolStripMenuItemHelp.Enabled = false;
			selectRealtimeButtons(in_acq: false);
			currentScale.XStart = 0f;
			currentScale.XStep = 1f;
			drawMainScaleOnImage(scaleBuffer);
			drawXyScaleOnBack();
			PictureBoxScaleX.CreateGraphics().DrawImage(backScalex, 0, 0);
			PictureBoxScaleY.CreateGraphics().DrawImage(backScaley, 0, 0);
			isFirstLap = true;
			x = 0;
			plotStep = (float)((double)backBuffer.Width / 100.0);
			TempFilePath = Path.GetTempPath() + Path.GetRandomFileName();
			temp_file_stream = new BinaryWriter(File.Open(TempFilePath, FileMode.Create));
			temp_file_stream.Write(LabelUnit.Text.PadRight(8).ToCharArray(0, 8), 0, 8);
			temp_file_stream.Write(checked((int)timerGet.Interval));
			finishTimeCounter = 0uL;
			FirstGet();
			timerGet.Enabled = true;
		}

		private void endMonitor()
		{
			timerMonitor.Enabled = false;
			FormBorderStyle = FormBorderStyle.Sizable;
			MaximizeBox = true;
			StatusStripMain.SizingGrip = true;
			timerGet.Enabled = false;
			ToolStripStatusLabelStatus.Image = Resources.red_s;
			ToolStripStatusLabelStatus.Text = Resources.CAPTION_STAY;
			ToolStripStatusLabelWaiting.Image = Resources.past_b;
			ToolStripStatusLabelWaiting.Text = Resources.CAPTION_PROCESSING;
			LabelProcessing.Visible = true;
			MyProject.Application.DoEvents();
			temp_file_stream.Close();
			BinaryReader binaryReader = new BinaryReader(File.Open(TempFilePath, FileMode.Open));
			binaryToDataTableValue(binaryReader);
			binaryReader.Close();
			File.Delete(TempFilePath);
			ButtonPeak.Enabled = true;
			ButtonTrack.Enabled = true;
			ButtonZero.Enabled = true;
			ToolStripMenuItemSoftwareConfigMenu.Enabled = true;
			ToolStripMenuItemFile.Enabled = true;
			ToolStripMenuItemHelp.Enabled = true;
			ToolStripStatusLabelWaiting.Image = null;
			ToolStripStatusLabelWaiting.Text = null;
			LabelProcessing.Visible = false;
			selectRealtimeButtons(in_acq: true);
			currentScale = getScalewhole();
			updateDetailGraph();
			timerMonitor.Enabled = true;
		}

		private void binaryToCsv(BinaryReader bin, StreamWriter csv)
		{
			char[] value = bin.ReadChars(8);
			float num = bin.ReadInt32();
			csv.WriteLine(new string(value));
			csv.WriteLine(num.ToString());
			while (true)
			{
				try
				{
					csv.WriteLine(bin.ReadSingle().ToString());
				}
				catch (Exception projectError)
				{
					ProjectData.SetProjectError(projectError);
					ProjectData.ClearProjectError();
					return;
				}
			}
		}

		private void binaryToDataTableValue(BinaryReader bin)
		{
			string text = new string(bin.ReadChars(8));
			float num = bin.ReadInt32();
			while (true)
			{
				try
				{
					DataRow dataRow = DataTableValue.NewRow();
					dataRow[0] = bin.ReadSingle().ToString();
					dataRow[1] = text.ToString();
					DataTableValue.Rows.Add(dataRow);
				}
				catch (Exception projectError)
				{
					ProjectData.SetProjectError(projectError);
					ProjectData.ClearProjectError();
					return;
				}
			}
		}

		private void updateMonitorGraph()
		{
			Monitor.Enter(_0024STATIC_0024updateMonitorGraph_00242001_0024monitor_points_0024Init);
			try
			{
				if (_0024STATIC_0024updateMonitorGraph_00242001_0024monitor_points_0024Init.State == 0)
				{
					_0024STATIC_0024updateMonitorGraph_00242001_0024monitor_points_0024Init.State = 2;
					_0024STATIC_0024updateMonitorGraph_00242001_0024monitor_points = new float[101];
				}
				else if (_0024STATIC_0024updateMonitorGraph_00242001_0024monitor_points_0024Init.State == 2)
				{
					throw new IncompleteInitialization();
				}
			}
			finally
			{
				_0024STATIC_0024updateMonitorGraph_00242001_0024monitor_points_0024Init.State = 1;
				Monitor.Exit(_0024STATIC_0024updateMonitorGraph_00242001_0024monitor_points_0024Init);
			}
			float num = backBuffer.Height;
			backG.DrawImage(scaleBuffer, 0, 0);
			float num2 = (float)Conversion.Val(updatingValue);
			checked
			{
				if (!isFirstLap)
				{
					for (int i = x + 1; i <= 100; i++)
					{
						backG.DrawLine(graphPen, (float)(i - 1) * plotStep, _0024STATIC_0024updateMonitorGraph_00242001_0024monitor_points[i - 1], (float)i * plotStep, _0024STATIC_0024updateMonitorGraph_00242001_0024monitor_points[i]);
					}
				}
				_0024STATIC_0024updateMonitorGraph_00242001_0024monitor_points[x] = (currentScale.YCenter - num2) * num / (currentScale.YStep * 11f) + num / 2f;
				backG.DrawLine(graphPen, (float)x * plotStep, 0f, (float)x * plotStep, num);
				int num3 = x;
				for (int j = 1; j <= num3; j++)
				{
					backG.DrawLine(graphPen, (float)(j - 1) * plotStep, _0024STATIC_0024updateMonitorGraph_00242001_0024monitor_points[j - 1], (float)j * plotStep, _0024STATIC_0024updateMonitorGraph_00242001_0024monitor_points[j]);
				}
				x++;
				if (x > 100)
				{
					isFirstLap = false;
					_0024STATIC_0024updateMonitorGraph_00242001_0024monitor_points[0] = _0024STATIC_0024updateMonitorGraph_00242001_0024monitor_points[100];
					x = 0;
				}
				frontG.DrawImage(backBuffer, 0, 0);
			}
		}

		private void selectRealtimeButtons(bool in_acq)
		{
			ToolStripButtonFirst.Enabled = in_acq;
			ToolStripButtonStart.Enabled = (in_acq & !autoConfig.IsTriggerEnable);
			ToolStripButtonClear.Enabled = in_acq;
			ToolStripComboBoxYaxis.Enabled = in_acq;
			ToolStripComboBoxXaxis.Enabled = in_acq;
			ToolStripButtonPlus.Enabled = in_acq;
			ToolStripButtonBoth.Enabled = in_acq;
			ToolStripButtonMinus.Enabled = in_acq;
			ToolStripButtonStop.Enabled = !in_acq;
			ToolStripButtonAll.Enabled = in_acq;
			ToolStripButtonReverse.Enabled = in_acq;
		}

		private void drawDetailOnBackG()
		{
			float num = backBuffer.Height;
			float num2 = backBuffer.Width;
			drawMainScaleOnImage(backBuffer);
			drawXyScaleOnBack();
			int count = DataTableValue.Rows.Count;
			checked
			{
				graphStartNumberOfDetail = (int)Math.Round(Math.Ceiling(currentScale.XStart / (float)(double)timerGet.Interval * 1000f) - 1.0);
				if (graphStartNumberOfDetail < 0)
				{
					graphStartNumberOfDetail = 0;
				}
				List<PointF> list = new List<PointF>();
				List<float> list2 = new List<float>();
				float num3 = default(float);
				for (int i = graphStartNumberOfDetail; i < count; i++)
				{
					if (!(num3 <= num2))
					{
						break;
					}
					float num4 = (float)Conversion.Val(RuntimeHelpers.GetObjectValue(DataTableValue.Rows[i][0]));
					list2.Add(num4);
					float y = (currentScale.YCenter - num4) * num / (currentScale.YStep * 11f) + num / 2f;
					num3 = ((float)(unchecked((long)i) * unchecked((long)timerGet.Interval)) / 1000f - currentScale.XStart) * num2 / (currentScale.XStep * 10f);
					PointF item = new PointF(num3, y);
					list.Add(item);
				}
				graphPoints = list.ToArray();
				if (graphPoints == null || graphPoints.GetLength(0) < 2)
				{
					return;
				}
				backG.DrawLines(graphPen, graphPoints);
				if ((double)currentScale.XStep < 0.3)
				{
					PointF[] array = graphPoints;
					for (int j = 0; j < array.Length; j++)
					{
						PointF pointF = array[j];
						backG.FillEllipse(pointBrush, pointF.X - 2f, pointF.Y - 2f, 4f, 4f);
					}
				}
				if ((indicatedPointNumber >= 0) & (indicatedPointNumber < graphPoints.GetLength(0)))
				{
					drawMarkedPoints(backG, list2[indicatedPointNumber], graphPoints[indicatedPointNumber], verticalPen, Color.Aqua, 0f, backBuffer.Height, backBuffer.Width);
				}
				if (markedPointNumber0 >= 0)
				{
					int num5 = markedPointNumber0 - graphStartNumberOfDetail;
					if ((num5 > 0) & (num5 < graphPoints.GetLength(0)))
					{
						drawMarkedPoints(backG, list2[num5], graphPoints[num5], verticalPen, Color.Yellow, 0f, backBuffer.Height, backBuffer.Width);
					}
				}
				if (markedPointNumber1 >= 0)
				{
					int num6 = markedPointNumber1 - graphStartNumberOfDetail;
					if ((num6 > 0) & (num6 < graphPoints.GetLength(0)))
					{
						drawMarkedPoints(backG, list2[num6], graphPoints[num6], verticalPen, Color.LightGray, 0f, backBuffer.Height, backBuffer.Width);
					}
				}
			}
		}

		private void drawMarkedPoints(Graphics g, float value, PointF plot_point, Pen ver_pen, Color back_col, float start_height, float end_height, float width)
		{
			g.DrawLine(ver_pen, plot_point.X, start_height, plot_point.X, end_height);
			g.FillEllipse(pointBrushBig, plot_point.X - 3f, plot_point.Y - 3f, 6f, 6f);
			string text = value.ToString();
			StringFormat stringFormat = new StringFormat();
			stringFormat.Alignment = StringAlignment.Center;
			stringFormat.LineAlignment = StringAlignment.Center;
			Font font = loadFont;
			SizeF layoutArea = new SizeF(100f, 10f);
			SizeF sizeF = g.MeasureString(text, font, layoutArea, stringFormat);
			RectangleF rectangleF = new RectangleF(plot_point.X - sizeF.Width / 2f - 2f, plot_point.Y - 35f, sizeF.Width, sizeF.Height + 10f);
			if (rectangleF.Left < 0f)
			{
				rectangleF.X = 0f;
			}
			if (rectangleF.Right > width)
			{
				rectangleF.X = width - sizeF.Width;
			}
			if (rectangleF.Top < 0f)
			{
				rectangleF.Y = plot_point.Y + 35f;
			}
			g.FillRectangle(new SolidBrush(back_col), rectangleF);
			g.DrawString(text, loadFont, loadBrush, rectangleF, stringFormat);
		}

		private void updateDetailGraph()
		{
			drawDetailOnBackG();
			frontG.DrawImage(backBuffer, 0, 0);
			PictureBoxScaleX.CreateGraphics().DrawImage(backScalex, 0, 0);
			PictureBoxScaleY.CreateGraphics().DrawImage(backScaley, 0, 0);
			ToolStripComboBoxXaxis.Text = currentScale.XStep.ToString();
			ToolStripComboBoxYaxis.Text = currentScale.YStep.ToString();
		}

		private ScaleInfo getScalewhole()
		{
			checked
			{
				double d = (float)(unchecked((long)DataTableValue.Rows.Count) * unchecked((long)timerGet.Interval)) / 1000f;
				double num = Math.Log10(d);
				Monitor.Enter(_0024STATIC_0024getScalewhole_00242001280D1_0024log10_2_under_0024Init);
				try
				{
					if (_0024STATIC_0024getScalewhole_00242001280D1_0024log10_2_under_0024Init.State == 0)
					{
						_0024STATIC_0024getScalewhole_00242001280D1_0024log10_2_under_0024Init.State = 2;
						_0024STATIC_0024getScalewhole_00242001280D1_0024log10_2_under = Math.Log10(2.0) - Math.Floor(Math.Log10(2.0));
					}
					else if (_0024STATIC_0024getScalewhole_00242001280D1_0024log10_2_under_0024Init.State == 2)
					{
						throw new IncompleteInitialization();
					}
				}
				finally
				{
					_0024STATIC_0024getScalewhole_00242001280D1_0024log10_2_under_0024Init.State = 1;
					Monitor.Exit(_0024STATIC_0024getScalewhole_00242001280D1_0024log10_2_under_0024Init);
				}
				Monitor.Enter(_0024STATIC_0024getScalewhole_00242001280D1_0024log10_5_under_0024Init);
				try
				{
					if (_0024STATIC_0024getScalewhole_00242001280D1_0024log10_5_under_0024Init.State == 0)
					{
						_0024STATIC_0024getScalewhole_00242001280D1_0024log10_5_under_0024Init.State = 2;
						_0024STATIC_0024getScalewhole_00242001280D1_0024log10_5_under = Math.Log10(5.0) - Math.Floor(Math.Log10(5.0));
					}
					else if (_0024STATIC_0024getScalewhole_00242001280D1_0024log10_5_under_0024Init.State == 2)
					{
						throw new IncompleteInitialization();
					}
				}
				finally
				{
					_0024STATIC_0024getScalewhole_00242001280D1_0024log10_5_under_0024Init.State = 1;
					Monitor.Exit(_0024STATIC_0024getScalewhole_00242001280D1_0024log10_5_under_0024Init);
				}
				float num2 = (float)Math.Pow(10.0, Math.Floor(num));
				double num3 = num - Math.Floor(num);
				if (num3 < _0024STATIC_0024getScalewhole_00242001280D1_0024log10_2_under)
				{
					num2 /= 5f;
				}
				else if (num3 < _0024STATIC_0024getScalewhole_00242001280D1_0024log10_5_under)
				{
					num2 /= 2f;
				}
				double num4 = double.MaxValue;
				double num5 = double.MinValue;
				IEnumerator enumerator = default(IEnumerator);
				try
				{
					enumerator = DataTableValue.Rows.GetEnumerator();
					while (enumerator.MoveNext())
					{
						DataRow dataRow = (DataRow)enumerator.Current;
						double num6 = Conversion.Val(RuntimeHelpers.GetObjectValue(dataRow[0]));
						if (num6 > num5)
						{
							num5 = num6;
						}
						if (num6 < num4)
						{
							num4 = num6;
						}
					}
				}
				finally
				{
					if (enumerator is IDisposable)
					{
						(enumerator as IDisposable).Dispose();
					}
				}
				float ystep = (float)Math.Pow(10.0, Math.Floor(Math.Log10(num5 - num4)));
				return new ScaleInfo(num2, ystep, 0f, 0f);
			}
		}

		private void PictureBoxChart_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Clicks > 1)
			{
				if (scaleHistory.Count > 0)
				{
					currentScale = scaleHistory.Pop();
					updateDetailGraph();
					indicatedPointNumber = -1;
				}
			}
			else if (!timerGet.Enabled)
			{
				subBuffer = new Bitmap(backBuffer);
				if ((e.Button == MouseButtons.Left) & !isScaleChanging)
				{
					selection = default(Rectangle);
					selectionStartPosition = e.Location;
					isScaleChanging = true;
				}
				else if ((e.Button == MouseButtons.Right) & !isScaleChanging)
				{
					dragOriginPoint = new PointF(currentScale.XStart, currentScale.YCenter);
					selectionStartPosition = e.Location;
					isScaleDragging = true;
					Cursor = Cursors.SizeAll;
				}
			}
		}

		private void PictureBoxChart_MouseUp(object sender, MouseEventArgs e)
		{
			checked
			{
				if ((e.Button == MouseButtons.Right) & isScaleDragging)
				{
					isScaleDragging = false;
					Cursor = Cursors.Default;
				}
				else
				{
					if (!((e.Button == MouseButtons.Left) & isScaleChanging))
					{
						return;
					}
					isScaleChanging = false;
					if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
					{
						if ((selection.Width < 5) | (selection.Height < 5))
						{
							if (indicatedPointNumber > 0)
							{
								markedPointNumber1 = indicatedPointNumber + graphStartNumberOfDetail;
								updateDetailGraph();
							}
							else
							{
								markedPointNumber1 = -1;
								updateDetailGraph();
							}
							return;
						}
					}
					else if ((selection.Width < 5) | (selection.Height < 5))
					{
						if (indicatedPointNumber > 0)
						{
							markedPointNumber0 = indicatedPointNumber + graphStartNumberOfDetail;
							updateDetailGraph();
						}
						else
						{
							markedPointNumber0 = -1;
							updateDetailGraph();
						}
						return;
					}
					float xstart = currentScale.XStart + currentScale.XStep * (float)Math.Truncate((double)selection.Left / (double)PictureBoxChart.Width * 10.0);
					float num = (float)PictureBoxChart.Height / 2f;
					float num2 = (float)(selection.Bottom + selection.Top) / 2f;
					float ycenter = currentScale.YCenter + currentScale.YStep * (float)Math.Truncate((num - num2) / (float)PictureBoxChart.Height * 11f);
					float num3 = currentScale.XStep;
					int width = selection.Width;
					int width2 = PictureBoxChart.Width;
					float num4 = currentScale.YStep;
					int height = selection.Height;
					int height2 = PictureBoxChart.Height;
					if (!(((double)width > (double)width2 / 2.0) & ((double)height > (double)height2 / 2.0)))
					{
						if (((double)width >= (double)width2 / 20.0) & ((double)width <= (double)width2 / 10.0))
						{
							num3 /= 10f;
						}
						else if ((double)width <= (double)width2 / 5.0)
						{
							num3 /= 5f;
						}
						else if ((double)width <= (double)width2 / 4.0)
						{
							num3 /= 4f;
						}
						else if ((double)width <= (double)width2 / 2.0)
						{
							num3 /= 2f;
						}
						if ((double)num3 < 0.1)
						{
							num3 = 0.1f;
						}
						if (((double)height >= (double)height2 / 20.0) & ((double)height <= (double)height2 / 10.0))
						{
							num4 /= 10f;
						}
						else if ((double)height <= (double)height2 / 5.0)
						{
							num4 /= 5f;
						}
						else if ((double)height <= (double)height2 / 4.0)
						{
							num4 /= 4f;
						}
						else if ((double)height <= (double)height2 / 2.0)
						{
							num4 /= 2f;
						}
						if ((double)num4 < 0.001)
						{
							num4 = 0.001f;
						}
						scaleHistory.Push(currentScale);
						currentScale = new ScaleInfo(num3, num4, xstart, ycenter);
						updateDetailGraph();
					}
				}
			}
		}

		private void PictureBoxChart_MouseMove(object sender, MouseEventArgs e)
		{
			if (timerGet.Enabled)
			{
				return;
			}
			indicatedPointNumber = -1;
			checked
			{
				if (isScaleDragging)
				{
					ScaleInfo scaleInfo = currentScale;
					scaleInfo.XStart = dragOriginPoint.X + scaleInfo.XStep * (float)HalfAdjust((double)(selectionStartPosition.X - e.X) / (double)backBuffer.Width * 10.0);
					if (scaleInfo.XStart < 0f)
					{
						scaleInfo.XStart = 0f;
					}
					scaleInfo.YCenter = dragOriginPoint.Y + scaleInfo.YStep * (float)HalfAdjust((double)(e.Y - selectionStartPosition.Y) / (double)backBuffer.Height * 11.0);
					scaleInfo = null;
					updateDetailGraph();
				}
				else if (isScaleChanging)
				{
					backG.DrawImage(subBuffer, 0, 0);
					int num = (selectionStartPosition.X <= e.X) ? selectionStartPosition.X : e.X;
					int y = (selectionStartPosition.Y <= e.Y) ? selectionStartPosition.Y : e.Y;
					int width = Math.Abs(e.X - selectionStartPosition.X);
					int height = Math.Abs(e.Y - selectionStartPosition.Y);
					selection = new Rectangle(num, y, width, height);
					backG.DrawRectangle(selectionPen, selection);
					backG.FillRectangle(selectionBrush, selection);
					frontG.DrawImage(backBuffer, 0, 0);
				}
				else
				{
					if (graphPoints == null || graphPoints.Length < 2)
					{
						return;
					}
					float num2 = (graphPoints[1].X - graphPoints[0].X) / 2f;
					if (num2 > 10f)
					{
						num2 = 10f;
					}
					int num3 = 0;
					PointF[] array = graphPoints;
					for (int i = 0; i < array.Length; i++)
					{
						PointF pointF = array[i];
						if ((Math.Abs(pointF.X - (float)e.X) <= num2) & (Math.Abs((float)e.Y - pointF.Y) <= 10f))
						{
							indicatedPointNumber = num3;
							break;
						}
						num3++;
					}
					updateDetailGraph();
				}
			}
		}

		private void FormMain_MouseWheel(object sender, MouseEventArgs e)
		{
			if (isScaleChanging | timerGet.Enabled)
			{
				return;
			}
			if (checked(e.X * Height + e.Y * Width < Height * Width))
			{
				if (e.Delta > 0)
				{
					graphScrollUp();
				}
				else
				{
					graphScrollDown();
				}
			}
			else if (e.Delta < 0)
			{
				graphScrollRight();
			}
			else
			{
				graphScrollLeft();
			}
		}

		private void graphScrollUp()
		{
			currentScale.YCenter += currentScale.YStep;
			updateDetailGraph();
		}

		private void graphScrollDown()
		{
			currentScale.YCenter -= currentScale.YStep;
			updateDetailGraph();
		}

		private void graphScrollLeft()
		{
			if (currentScale.XStart > currentScale.XStep)
			{
				currentScale.XStart -= currentScale.XStep;
				updateDetailGraph();
			}
		}

		private void graphScrollRight()
		{
			currentScale.XStart += currentScale.XStep;
			updateDetailGraph();
		}

		private void ToolStripButtonStart_Click(object sender, EventArgs e)
		{
			startMonitor();
		}

		private void ToolStripButtonStop_Click(object sender, EventArgs e)
		{
			endMonitor();
			if (autoConfig.IsTriggerEnable)
			{
				autoConfig.IsTriggerEnable = false;
				updateAutoValues();
				ToolStripButtonStart.Enabled = !autoConfig.IsTriggerEnable;
			}
		}

		private void ToolStripButtonFirst_Click(object sender, EventArgs e)
		{
			currentScale.XStart = 0f;
			updateDetailGraph();
		}

		private void ToolStripButtonAll_Click(object sender, EventArgs e)
		{
			if (DataTableValue.Rows.Count < 2)
			{
				currentScale.XStart = 0f;
			}
			else
			{
				currentScale = getScalewhole();
			}
			updateDetailGraph();
		}

		private void ToolStripButtonClear_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show(Resources.CAUTION_CLEAR_GRAPH, Resources.CAUTION, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
			{
				clearAll();
				updateDetailGraph();
			}
		}

		private void ToolStripButtonPlus_Click(object sender, EventArgs e)
		{
			currentScale.YCenter = currentScale.YStep * 5f;
			updateDetailGraph();
		}

		private void ToolStripButtonBoth_Click(object sender, EventArgs e)
		{
			currentScale.YCenter = 0f;
			updateDetailGraph();
		}

		private void ToolStripButtonMinus_Click(object sender, EventArgs e)
		{
			currentScale.YCenter = (0f - currentScale.YStep) * 5f;
			updateDetailGraph();
		}

		private void ToolStripComboBoxYaxis_SelectedIndexChanged(object sender, EventArgs e)
		{
			currentScale.YStep = (float)Conversion.Val(RuntimeHelpers.GetObjectValue(ToolStripComboBoxYaxis.SelectedItem));
			updateDetailGraph();
		}

		private void ToolStripComboBoxXaxis_SelectedIndexChanged(object sender, EventArgs e)
		{
			currentScale.XStep = (float)Conversion.Val(RuntimeHelpers.GetObjectValue(ToolStripComboBoxXaxis.SelectedItem));
			updateDetailGraph();
		}

		private void ToolStripComboBox_TextChanged(object sender, EventArgs e)
		{
			ToolStripComboBox toolStripComboBox = (ToolStripComboBox)sender;
			toolStripComboBox.Text = Regex.Match(toolStripComboBox.Text, "^[0-9]*\\.?[0-9]*").ToString();
			toolStripComboBox.Select(toolStripComboBox.Text.Length, 0);
		}

		private void ToolStripComboBoxYaxis_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode != Keys.Return)
			{
				e.Handled = true;
				return;
			}
			ToolStripComboBox toolStripComboBoxYaxis = ToolStripComboBoxYaxis;
			float num = (float)Conversion.Val(toolStripComboBoxYaxis.Text);
			if (num > 0f)
			{
				toolStripComboBoxYaxis.SelectedIndex = toolStripComboBoxYaxis.Items.Add(ScaleInfo.HalfAdjust(num, 3).ToString().Replace(',', '.'));
			}
			toolStripComboBoxYaxis = null;
		}

		private void ToolStripComboBoxXaxis_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode != Keys.Return)
			{
				e.Handled = true;
				return;
			}
			ToolStripComboBox toolStripComboBoxXaxis = ToolStripComboBoxXaxis;
			float num = (float)Conversion.Val(toolStripComboBoxXaxis.Text);
			if (num > 0f)
			{
				toolStripComboBoxXaxis.SelectedIndex = toolStripComboBoxXaxis.Items.Add(ScaleInfo.HalfAdjust(num, 1).ToString().Replace(',', '.'));
			}
			toolStripComboBoxXaxis = null;
		}

		private void ToolStripMenuItemConnection_Click(object sender, EventArgs e)
		{
			if (gauge != null)
			{
				if (BackgroundWorkerRealTime.IsBusy)
				{
					BackgroundWorkerRealTime.RunWorkerAsync();
				}
				gauge.Disconnect();
			}
			openPortSelectModel();
		}

		private void ToolStripMenuItemUpDown_Click(object sender, EventArgs e)
		{
			if (ToolStrip1.Dock == DockStyle.Top)
			{
				ToolStrip1.Dock = DockStyle.Bottom;
				ToolStrip2.Dock = DockStyle.Bottom;
			}
			else
			{
				ToolStrip1.Dock = DockStyle.Top;
				ToolStrip2.Dock = DockStyle.Top;
			}
		}

		private void ContextMenuStripUpDown_Opened(object sender, EventArgs e)
		{
			if (ToolStrip1.Dock == DockStyle.Bottom)
			{
				ToolStripMenuItemUpDown.Text = Resources.CAPTION_TOABOVE;
			}
			else
			{
				ToolStripMenuItemUpDown.Text = Resources.CAPTION_TOUNDER;
			}
		}

		private void copy_DataSetValue_from(DataTable dt)
		{
			DataTableValue.Clear();
			IEnumerator enumerator = default(IEnumerator);
			try
			{
				enumerator = dt.Rows.GetEnumerator();
				while (enumerator.MoveNext())
				{
					DataRow dataRow = (DataRow)enumerator.Current;
					DataRow dataRow2 = DataTableValue.NewRow();
					dataRow2[0] = RuntimeHelpers.GetObjectValue(dataRow[0]);
					dataRow2[1] = RuntimeHelpers.GetObjectValue(dataRow[1]);
					DataTableValue.Rows.Add(dataRow2);
				}
			}
			finally
			{
				if (enumerator is IDisposable)
				{
					(enumerator as IDisposable).Dispose();
				}
			}
		}

		private void ToolStripMenuItemQuit_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void ButtonZero_ClickI(object sender, EventArgs e)
		{
			gauge.Send("Z");
		}

		private void ButtonPeak_ClickI(object sender, EventArgs e)
		{
			gauge.Send("P");
		}

		private void ButtonTrack_ClickI(object sender, EventArgs e)
		{
			gauge.Send("T");
		}

		private void ToolStripButtonReverse_Click(object sender, EventArgs e)
		{
			if (DataTableValue.Rows.Count >= 2)
			{
				IEnumerator enumerator = default(IEnumerator);
				try
				{
					enumerator = DataTableValue.Rows.GetEnumerator();
					while (enumerator.MoveNext())
					{
						DataRow dataRow = (DataRow)enumerator.Current;
						dataRow[0] = 0f - (float)Conversion.Val(RuntimeHelpers.GetObjectValue(dataRow[0]));
					}
				}
				finally
				{
					if (enumerator is IDisposable)
					{
						(enumerator as IDisposable).Dispose();
					}
				}
				currentScale.YCenter = 0f - currentScale.YCenter;
				updateDetailGraph();
			}
		}

		private void ToolStripMenuItemPrint_Click(object sender, EventArgs e)
		{
			if (PrintDialog1.ShowDialog() == DialogResult.OK)
			{
				PrintDocument1.DefaultPageSettings.Landscape = true;
				PrintDocument1.Print();
			}
		}

		private void ToolStripMenuItemPreview_Click(object sender, EventArgs e)
		{
			try
			{
				PrintDocument1.DefaultPageSettings.Landscape = true;
				if (PrintPreviewDialog1.ShowDialog() == DialogResult.OK)
				{
				}
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				MessageBox.Show(Resources.PRINTER_MESSAGE, Resources.ERROR_CAPTION, MessageBoxButtons.OK);
				ProjectData.ClearProjectError();
			}
		}

		private void PrintDocument1_PrintPage(object sender, PrintPageEventArgs e)
		{
			reflectDatasetTotal();
			Graphics graphics = e.Graphics;
			float num = (float)((double)e.MarginBounds.Width / 12.0);
			float num2 = (float)((double)e.MarginBounds.Height / 15.0);
			float num3 = (float)e.MarginBounds.Height - num2;
			float num4 = (float)((double)e.MarginBounds.Width / 12.0 * 9.0);
			float num5 = num + num4;
			graphics.TranslateTransform(e.MarginBounds.Left, e.MarginBounds.Top);
			float num6 = num4 / 5f;
			Font font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point);
			StringFormat stringFormat = new StringFormat();
			SolidBrush solidBrush = new SolidBrush(scaleBackColor);
			graphics.FillRectangle(solidBrush, 0f, num3, num + num4, num2);
			stringFormat.Alignment = StringAlignment.Center;
			stringFormat.LineAlignment = StringAlignment.Center;
			int num7 = 1;
			checked
			{
				RectangleF layoutRectangle;
				do
				{
					float num8 = ScaleInfo.HalfAdjust(currentScale.XStart + (float)num7 * currentScale.XStep * 2f, 1);
					layoutRectangle = new RectangleF(num + num6 * (float)num7 - scaleFontRectWidth / 2f, num3 + 10f, scaleFontRectWidth, scaleFontRectHeight);
					graphics.DrawString(num8.ToString(), font, scaleFontBrush, layoutRectangle, stringFormat);
					num7++;
				}
				while (num7 <= 4);
				layoutRectangle = new RectangleF(0f, 0f, num5, e.MarginBounds.Height);
				stringFormat.Alignment = StringAlignment.Far;
				stringFormat.LineAlignment = StringAlignment.Far;
				graphics.DrawString(Resources.CAPTION_SEC, font, scaleFontBrush, layoutRectangle, stringFormat);
				float num9 = num3 / 11f;
				stringFormat.Alignment = StringAlignment.Far;
				stringFormat.LineAlignment = StringAlignment.Center;
				float num10 = num3 / 2f;
				graphics.FillRectangle(solidBrush, 0f, 0f, num, num3 + num2);
				int num11 = -5;
				do
				{
					float num12 = ScaleInfo.HalfAdjust(currentScale.YCenter - (float)num11 * currentScale.YStep, 3);
					Brush brush = (num12 != 0f) ? scaleFontBrush : scaleFontOriginBrush;
					layoutRectangle = new RectangleF(0f, num10 + num9 * (float)num11 - scaleFontRectHeight / 2f, num, scaleFontRectHeight);
					graphics.DrawString(num12.ToString(), scaleFont, brush, layoutRectangle, stringFormat);
					num11++;
				}
				while (num11 <= 5);
				Matrix transform = graphics.Transform;
				graphics.TranslateTransform(num, 0f);
				RectangleF clipBounds = graphics.ClipBounds;
				RectangleF clip = new RectangleF(0f, 0f, num4, num3);
				graphics.SetClip(clip);
				solidBrush.Dispose();
				solidBrush = new SolidBrush(chartBackColor);
				graphics.FillRectangle(new SolidBrush(printBackColor), 0f, 0f, num4, num3);
				num6 = num4 / 10f;
				Pen pen = new Pen(printScalecolor);
				int num13 = 1;
				do
				{
					graphics.DrawLine(pen, num6 * (float)num13, 0f, num6 * (float)num13, num3);
					num13++;
				}
				while (num13 <= 9);
				num6 = num3 / 11f;
				num10 = num3 / 2f;
				int num14 = -5;
				do
				{
					float num15 = ScaleInfo.HalfAdjust(currentScale.YCenter - (float)num14 * currentScale.YStep, 3);
					Pen pen2 = (num15 != 0f) ? pen : origin_pen;
					graphics.DrawLine(pen2, 0f, num6 * (float)num14 + num10, num4, num6 * (float)num14 + num10);
					num14++;
				}
				while (num14 <= 5);
				if (isPrintBars)
				{
					float num16 = (currentScale.YCenter - upperLowerBarValue1) * num3 / (currentScale.YStep * 11f) + num3 / 2f;
					float num17 = (currentScale.YCenter - upperLowerBarValue2) * num3 / (currentScale.YStep * 11f) + num3 / 2f;
					graphics.DrawLine(new Pen(bar1Color), 0f, num16, num4, num16);
					graphics.DrawLine(new Pen(bar2Color), 0f, num17, num4, num17);
				}
				int count = DataTableValue.Rows.Count;
				if (count < 2)
				{
					string mESSAGE_NODATA = Resources.MESSAGE_NODATA;
					Font font2 = new Font("Arial", 36f, FontStyle.Bold, GraphicsUnit.Point);
					StringFormat stringFormat2 = new StringFormat();
					stringFormat2.Alignment = StringAlignment.Center;
					stringFormat2.LineAlignment = StringAlignment.Center;
					SizeF sizeF = graphics.MeasureString(mESSAGE_NODATA, font2, 500, stringFormat2);
					RectangleF rectangleF = new RectangleF(num4 / 2f - sizeF.Width / 2f, num3 / 2f - sizeF.Height / 2f, sizeF.Width, sizeF.Height);
					graphics.FillRectangle(Brushes.White, rectangleF);
					graphics.DrawString(mESSAGE_NODATA, font2, Brushes.Black, rectangleF);
					return;
				}
				int i = (int)Math.Round(Math.Ceiling(currentScale.XStart / (float)(double)timerGet.Interval * 1000f)) - 1;
				if (i < 0)
				{
					i = 0;
				}
				List<PointF> list = new List<PointF>();
				bool flag = false;
				bool flag2 = false;
				float num18 = default(float);
				float value = default(float);
				PointF plot_point = default(PointF);
				float value2 = default(float);
				PointF plot_point2 = default(PointF);
				for (; i < count; i++)
				{
					if (!(num18 <= num4))
					{
						break;
					}
					float num19 = (float)Conversion.Val(RuntimeHelpers.GetObjectValue(DataTableValue.Rows[i][0]));
					float y = (currentScale.YCenter - num19) * num3 / (currentScale.YStep * 11f) + num3 / 2f;
					num18 = ((float)((double)(unchecked((long)i) * unchecked((long)timerGet.Interval)) / 1000.0) - currentScale.XStart) * num4 / (currentScale.XStep * 10f);
					PointF item = new PointF(num18, y);
					list.Add(item);
					if (markedPointNumber0 == i)
					{
						flag = true;
						value = num19;
						plot_point = new PointF(num18, y);
					}
					if (markedPointNumber1 == i)
					{
						flag2 = true;
						value2 = num19;
						plot_point2 = new PointF(num18, y);
					}
				}
				int num20 = list.Count - 2;
				for (int j = 0; j <= num20; j++)
				{
					graphics.DrawLine(graphPen, list[j].X, list[j].Y, list[j + 1].X, list[j + 1].Y);
				}
				if ((double)currentScale.XStep <= 0.3)
				{
					foreach (PointF item2 in list)
					{
						graphics.FillEllipse(pointBrush, item2.X - 2f, item2.Y - 2f, 4f, 4f);
					}
				}
				if (markedPointNumber0 >= 0 && flag)
				{
					drawMarkedPoints(graphics, value, plot_point, verticalPen, Color.Yellow, 0f, num3, num4);
				}
				if (markedPointNumber1 >= 0 && flag2)
				{
					drawMarkedPoints(graphics, value2, plot_point2, verticalPen, Color.LightGray, 0f, num3, num4);
				}
				graphics.SetClip(clipBounds);
				graphics.Transform = transform;
				graphics.DrawRectangle(Pens.Black, 0f, 0f, num5, e.MarginBounds.Height);
				graphics.TranslateTransform(num5, 0f);
				graphics.DrawRectangle(Pens.Black, 0f, 0f, (float)e.MarginBounds.Width - num5, e.MarginBounds.Height);
				float num21 = 10f;
				graphics.TranslateTransform(num21, num21);
				graphics.DrawString(string.Format(Resources.CAPTION_NUM_OF_DATA + ": {0}", stats.Count), new Font("Arial", 10f), Brushes.Black, 0f, 0f);
				graphics.DrawString(string.Format(Resources.CAPTION_UNIT + ": {0}", RuntimeHelpers.GetObjectValue(DataTableValue.Rows[0][1])), new Font("Arial", 10f), Brushes.Black, 0f, 50f);
				graphics.DrawString(string.Format(Resources.CAPTION_MAX + ": {0}", stats.Maximum), new Font("Arial", 10f), Brushes.Black, 0f, 70f);
				graphics.DrawString(string.Format(Resources.CAPTION_MIN + ": {0}", stats.Minimum), new Font("Arial", 10f), Brushes.Black, 0f, 90f);
				graphics.DrawString(string.Format(Resources.CAPTION_AVERAGE + ": {0}", stats.Average), new Font("Arial", 10f), Brushes.Black, 0f, 110f);
				graphics.DrawString(string.Format(Resources.CAPTION_INTERVAL + ": {0} msec", timerGet.Interval), new Font("Arial", 10f), Brushes.Black, 0f, 130f);
				if ((markedPointNumber0 >= 0) & (markedPointNumber1 >= 0) & (markedPointNumber0 != markedPointNumber1))
				{
					if (markedPointNumber0 > markedPointNumber1)
					{
						int num22 = markedPointNumber1;
						markedPointNumber1 = markedPointNumber0;
						markedPointNumber0 = num22;
					}
					float num23 = 0f;
					float num24 = float.MinValue;
					float num25 = float.MaxValue;
					int num26 = markedPointNumber0;
					int num27 = markedPointNumber1;
					for (int k = num26; k <= num27; k++)
					{
						float num28 = (float)Conversion.Val(RuntimeHelpers.GetObjectValue(DataTableValue.Rows[k][0]));
						num23 += num28;
						if (num28 > num24)
						{
							num24 = num28;
						}
						if (num28 < num25)
						{
							num25 = num28;
						}
					}
					num23 /= (float)(markedPointNumber1 - markedPointNumber0 + 1);
					graphics.DrawString(Resources.CAPTION_MAX_BETWEEN + ":", new Font("Arial", 10f), Brushes.Black, 0f, 150f);
					graphics.DrawString($"{num24}", new Font("Arial", 10f), Brushes.Black, 20f, 170f);
					graphics.DrawString(Resources.CAPTION_MIN_BETWEEN + ":", new Font("Arial", 10f), Brushes.Black, 0f, 190f);
					graphics.DrawString($"{num25}", new Font("Arial", 10f), Brushes.Black, 20f, 210f);
					graphics.DrawString(Resources.CAPTION_AVERAGE_BETWEEN + ":", new Font("Arial", 10f), Brushes.Black, 0f, 230f);
					graphics.DrawString($"{num23}", new Font("Arial", 10f), Brushes.Black, 20f, 250f);
				}
				if (isPrintBars)
				{
					font = new Font("Arial", 10f);
					stringFormat.Alignment = StringAlignment.Center;
					stringFormat.LineAlignment = StringAlignment.Center;
					string text = upperLowerBarValue1.ToString();
					Font font3 = font;
					SizeF layoutArea = new SizeF((float)e.MarginBounds.Width - num5 - 20f, 25f);
					RectangleF rectangleF2 = new RectangleF(height: graphics.MeasureString(text, font3, layoutArea, stringFormat).Height + num21, x: 0f, y: 280f - num21 / 2f, width: (float)e.MarginBounds.Width - num5 - num21 * 2f);
					graphics.FillRectangle(new SolidBrush(bar1Color), rectangleF2);
					graphics.DrawString(upperLowerBarValue1.ToString(), font, Brushes.Black, rectangleF2, stringFormat);
					rectangleF2.Offset(0f, 30f);
					graphics.FillRectangle(new SolidBrush(bar2Color), rectangleF2);
					graphics.DrawString(upperLowerBarValue2.ToString(), font, Brushes.Black, rectangleF2, stringFormat);
				}
			}
		}

		private void ToolStripMenuItemPrintConfig_Click(object sender, EventArgs e)
		{
			FormPrintConfig formPrintConfig = new FormPrintConfig();
			FormPrintConfig formPrintConfig2 = formPrintConfig;
			formPrintConfig2.Label1.BackColor = bar1Color;
			formPrintConfig2.Label2.BackColor = bar2Color;
			formPrintConfig2.TextBoxValue1.Text = upperLowerBarValue1.ToString();
			formPrintConfig2.TextBoxValue2.Text = upperLowerBarValue2.ToString();
			formPrintConfig2.CheckBoxBand.Checked = isPrintBars;
			if (formPrintConfig2.ShowDialog() == DialogResult.OK)
			{
				upperLowerBarValue1 = (float)Conversion.Val(formPrintConfig2.TextBoxValue1.Text);
				upperLowerBarValue2 = (float)Conversion.Val(formPrintConfig2.TextBoxValue2.Text);
				isPrintBars = formPrintConfig2.CheckBoxBand.Checked;
			}
			formPrintConfig2 = null;
		}

		private void ToolStripMenuItemAbout_Click(object sender, EventArgs e)
		{
			AboutBox1 aboutBox = new AboutBox1();
			aboutBox.ShowDialog();
		}

		private void ToolStripMenuItemHelpContents_Click(object sender, EventArgs e)
		{
			Help.ShowHelpIndex(this, Resources.FILENAME_HELP);
		}
	}
}
