namespace plc_sachul
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.serialPort2 = new System.IO.Ports.SerialPort(this.components);
            this.l_state = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.l_time = new System.Windows.Forms.Label();
            this.l_data = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // l_state
            // 
            this.l_state.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.l_state.BackColor = System.Drawing.Color.White;
            this.l_state.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.l_state.ForeColor = System.Drawing.Color.White;
            this.l_state.Location = new System.Drawing.Point(791, 9);
            this.l_state.Name = "l_state";
            this.l_state.Size = new System.Drawing.Size(127, 35);
            this.l_state.TabIndex = 0;
            this.l_state.Text = "접속확인";
            this.l_state.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(21, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(237, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "DB Connection State : ";
            // 
            // l_time
            // 
            this.l_time.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.l_time.BackColor = System.Drawing.Color.White;
            this.l_time.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.l_time.ForeColor = System.Drawing.Color.Black;
            this.l_time.Location = new System.Drawing.Point(750, 436);
            this.l_time.Name = "l_time";
            this.l_time.Size = new System.Drawing.Size(168, 23);
            this.l_time.TabIndex = 2;
            this.l_time.Text = "시간";
            this.l_time.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // l_data
            // 
            this.l_data.Location = new System.Drawing.Point(12, 53);
            this.l_data.Multiline = true;
            this.l_data.Name = "l_data";
            this.l_data.Size = new System.Drawing.Size(906, 371);
            this.l_data.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(930, 468);
            this.Controls.Add(this.l_data);
            this.Controls.Add(this.l_time);
            this.Controls.Add(this.l_state);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Timer timer1;
        private System.IO.Ports.SerialPort serialPort2;
        private System.Windows.Forms.Label l_state;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label l_time;
        private System.Windows.Forms.TextBox l_data;
    }
}

