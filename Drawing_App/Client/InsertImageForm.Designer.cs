namespace Client
{
    partial class InsertImageForm
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblUrl = new System.Windows.Forms.Label();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.lblSize = new System.Windows.Forms.Label();
            this.numWidth = new System.Windows.Forms.NumericUpDown();
            this.lblX = new System.Windows.Forms.Label();
            this.numHeight = new System.Windows.Forms.NumericUpDown();
            this.lblHint = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHeight)).BeginInit();
            this.SuspendLayout();
            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(18, 16);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(180, 21);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Insert image from URL";
            //
            // lblUrl
            //
            this.lblUrl.AutoSize = true;
            this.lblUrl.Location = new System.Drawing.Point(20, 58);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(64, 15);
            this.lblUrl.TabIndex = 1;
            this.lblUrl.Text = "Image URL:";
            //
            // txtUrl
            //
            this.txtUrl.Location = new System.Drawing.Point(110, 55);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(300, 23);
            this.txtUrl.TabIndex = 0;
            //
            // lblSize
            //
            this.lblSize.AutoSize = true;
            this.lblSize.Location = new System.Drawing.Point(20, 95);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(82, 15);
            this.lblSize.TabIndex = 3;
            this.lblSize.Text = "Resize to (px):";
            //
            // numWidth
            //
            this.numWidth.Location = new System.Drawing.Point(110, 92);
            this.numWidth.Maximum = new decimal(new int[] { 5000, 0, 0, 0 });
            this.numWidth.Name = "numWidth";
            this.numWidth.Size = new System.Drawing.Size(80, 23);
            this.numWidth.TabIndex = 1;
            //
            // lblX
            //
            this.lblX.AutoSize = true;
            this.lblX.Location = new System.Drawing.Point(196, 95);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(15, 15);
            this.lblX.TabIndex = 5;
            this.lblX.Text = "x";
            //
            // numHeight
            //
            this.numHeight.Location = new System.Drawing.Point(217, 92);
            this.numHeight.Maximum = new decimal(new int[] { 5000, 0, 0, 0 });
            this.numHeight.Name = "numHeight";
            this.numHeight.Size = new System.Drawing.Size(80, 23);
            this.numHeight.TabIndex = 2;
            //
            // lblHint
            //
            this.lblHint.AutoSize = true;
            this.lblHint.ForeColor = System.Drawing.Color.Gray;
            this.lblHint.Location = new System.Drawing.Point(110, 120);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(160, 15);
            this.lblHint.TabIndex = 7;
            this.lblHint.Text = "0 = keep the original size";
            //
            // btnOk
            //
            this.btnOk.Location = new System.Drawing.Point(200, 165);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(100, 34);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "Insert";
            this.btnOk.UseVisualStyleBackColor = true;
            //
            // btnCancel
            //
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(310, 165);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 34);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            //
            // InsertImageForm
            //
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(434, 220);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblHint);
            this.Controls.Add(this.numHeight);
            this.Controls.Add(this.lblX);
            this.Controls.Add(this.numWidth);
            this.Controls.Add(this.lblSize);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.lblUrl);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InsertImageForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Insert image";
            ((System.ComponentModel.ISupportInitialize)(this.numWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHeight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblUrl;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.NumericUpDown numWidth;
        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.NumericUpDown numHeight;
        private System.Windows.Forms.Label lblHint;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}
