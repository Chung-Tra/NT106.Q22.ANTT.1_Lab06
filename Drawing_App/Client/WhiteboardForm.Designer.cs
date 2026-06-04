namespace Client
{
    partial class WhiteboardForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.picCanvas = new System.Windows.Forms.PictureBox();
            this.btnEnd = new System.Windows.Forms.Button();
            this.btnColor = new System.Windows.Forms.Button();
            this.pnlColorPreview = new System.Windows.Forms.Panel();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnInsertImage = new System.Windows.Forms.Button();
            this.grpWidth = new System.Windows.Forms.GroupBox();
            this.rb1 = new System.Windows.Forms.RadioButton();
            this.rb2 = new System.Windows.Forms.RadioButton();
            this.rb3 = new System.Windows.Forms.RadioButton();
            this.rb4 = new System.Windows.Forms.RadioButton();
            this.rb5 = new System.Windows.Forms.RadioButton();
            this.lblClients = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picCanvas)).BeginInit();
            this.grpWidth.SuspendLayout();
            this.SuspendLayout();
            //
            // picCanvas
            //
            this.picCanvas.BackColor = System.Drawing.Color.White;
            this.picCanvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picCanvas.Cursor = System.Windows.Forms.Cursors.Cross;
            this.picCanvas.Location = new System.Drawing.Point(12, 12);
            this.picCanvas.Name = "picCanvas";
            this.picCanvas.Size = new System.Drawing.Size(900, 520);
            this.picCanvas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal;
            this.picCanvas.TabIndex = 0;
            this.picCanvas.TabStop = false;
            //
            // btnEnd
            //
            this.btnEnd.Location = new System.Drawing.Point(12, 545);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(80, 40);
            this.btnEnd.TabIndex = 1;
            this.btnEnd.Text = "END";
            this.btnEnd.UseVisualStyleBackColor = true;
            //
            // btnColor
            //
            this.btnColor.Location = new System.Drawing.Point(100, 545);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(80, 40);
            this.btnColor.TabIndex = 2;
            this.btnColor.Text = "COLOR";
            this.btnColor.UseVisualStyleBackColor = true;
            //
            // pnlColorPreview
            //
            this.pnlColorPreview.BackColor = System.Drawing.Color.Black;
            this.pnlColorPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlColorPreview.Location = new System.Drawing.Point(186, 553);
            this.pnlColorPreview.Name = "pnlColorPreview";
            this.pnlColorPreview.Size = new System.Drawing.Size(24, 24);
            this.pnlColorPreview.TabIndex = 9;
            //
            // btnClear
            //
            this.btnClear.Location = new System.Drawing.Point(224, 545);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(80, 40);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "CLEAR";
            this.btnClear.UseVisualStyleBackColor = true;
            //
            // btnInsertImage
            //
            this.btnInsertImage.Location = new System.Drawing.Point(312, 545);
            this.btnInsertImage.Name = "btnInsertImage";
            this.btnInsertImage.Size = new System.Drawing.Size(124, 40);
            this.btnInsertImage.TabIndex = 4;
            this.btnInsertImage.Text = "INSERT IMAGE";
            this.btnInsertImage.UseVisualStyleBackColor = true;
            //
            // grpWidth
            //
            this.grpWidth.Controls.Add(this.rb1);
            this.grpWidth.Controls.Add(this.rb2);
            this.grpWidth.Controls.Add(this.rb3);
            this.grpWidth.Controls.Add(this.rb4);
            this.grpWidth.Controls.Add(this.rb5);
            this.grpWidth.Location = new System.Drawing.Point(448, 538);
            this.grpWidth.Name = "grpWidth";
            this.grpWidth.Size = new System.Drawing.Size(250, 54);
            this.grpWidth.TabIndex = 5;
            this.grpWidth.TabStop = false;
            this.grpWidth.Text = "Width";
            //
            // rb1
            //
            this.rb1.AutoSize = true;
            this.rb1.Location = new System.Drawing.Point(14, 22);
            this.rb1.Name = "rb1";
            this.rb1.Size = new System.Drawing.Size(33, 19);
            this.rb1.TabIndex = 0;
            this.rb1.Text = "1";
            this.rb1.UseVisualStyleBackColor = true;
            //
            // rb2
            //
            this.rb2.AutoSize = true;
            this.rb2.Location = new System.Drawing.Point(60, 22);
            this.rb2.Name = "rb2";
            this.rb2.Size = new System.Drawing.Size(33, 19);
            this.rb2.TabIndex = 1;
            this.rb2.Text = "2";
            this.rb2.UseVisualStyleBackColor = true;
            //
            // rb3
            //
            this.rb3.AutoSize = true;
            this.rb3.Location = new System.Drawing.Point(106, 22);
            this.rb3.Name = "rb3";
            this.rb3.Size = new System.Drawing.Size(33, 19);
            this.rb3.TabIndex = 2;
            this.rb3.Text = "3";
            this.rb3.UseVisualStyleBackColor = true;
            //
            // rb4
            //
            this.rb4.AutoSize = true;
            this.rb4.Checked = true;
            this.rb4.Location = new System.Drawing.Point(152, 22);
            this.rb4.Name = "rb4";
            this.rb4.Size = new System.Drawing.Size(33, 19);
            this.rb4.TabIndex = 3;
            this.rb4.TabStop = true;
            this.rb4.Text = "4";
            this.rb4.UseVisualStyleBackColor = true;
            //
            // rb5
            //
            this.rb5.AutoSize = true;
            this.rb5.Location = new System.Drawing.Point(198, 22);
            this.rb5.Name = "rb5";
            this.rb5.Size = new System.Drawing.Size(33, 19);
            this.rb5.TabIndex = 4;
            this.rb5.Text = "5";
            this.rb5.UseVisualStyleBackColor = true;
            //
            // lblClients
            //
            this.lblClients.AutoSize = true;
            this.lblClients.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblClients.Location = new System.Drawing.Point(712, 548);
            this.lblClients.Name = "lblClients";
            this.lblClients.Size = new System.Drawing.Size(150, 19);
            this.lblClients.TabIndex = 6;
            this.lblClients.Text = "Connected clients: ...";
            //
            // lblStatus
            //
            this.lblStatus.AutoSize = true;
            this.lblStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblStatus.Location = new System.Drawing.Point(712, 573);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(96, 15);
            this.lblStatus.TabIndex = 7;
            this.lblStatus.Text = "Status: starting...";
            //
            // WhiteboardForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 604);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblClients);
            this.Controls.Add(this.grpWidth);
            this.Controls.Add(this.btnInsertImage);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.pnlColorPreview);
            this.Controls.Add(this.btnColor);
            this.Controls.Add(this.btnEnd);
            this.Controls.Add(this.picCanvas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "WhiteboardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Whiteboard";
            ((System.ComponentModel.ISupportInitialize)(this.picCanvas)).EndInit();
            this.grpWidth.ResumeLayout(false);
            this.grpWidth.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.PictureBox picCanvas;
        private System.Windows.Forms.Button btnEnd;
        private System.Windows.Forms.Button btnColor;
        private System.Windows.Forms.Panel pnlColorPreview;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnInsertImage;
        private System.Windows.Forms.GroupBox grpWidth;
        private System.Windows.Forms.RadioButton rb1;
        private System.Windows.Forms.RadioButton rb2;
        private System.Windows.Forms.RadioButton rb3;
        private System.Windows.Forms.RadioButton rb4;
        private System.Windows.Forms.RadioButton rb5;
        private System.Windows.Forms.Label lblClients;
        private System.Windows.Forms.Label lblStatus;
    }
}
