namespace Client
{
    partial class ConnectForm
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
            this.lblServer = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblHint = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(18, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(258, 21);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Connect to Whiteboard server";
            //
            // lblServer
            //
            this.lblServer.AutoSize = true;
            this.lblServer.Location = new System.Drawing.Point(20, 64);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(88, 15);
            this.lblServer.TabIndex = 1;
            this.lblServer.Text = "Server address:";
            //
            // txtServer
            //
            this.txtServer.Location = new System.Drawing.Point(140, 61);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(260, 23);
            this.txtServer.TabIndex = 0;
            this.txtServer.Text = "localhost:5000";
            //
            // lblName
            //
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(20, 99);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(82, 15);
            this.lblName.TabIndex = 3;
            this.lblName.Text = "Display name:";
            //
            // txtName
            //
            this.txtName.Location = new System.Drawing.Point(140, 96);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(260, 23);
            this.txtName.TabIndex = 1;
            this.txtName.Text = "User";
            //
            // lblHint
            //
            this.lblHint.AutoSize = true;
            this.lblHint.ForeColor = System.Drawing.Color.Gray;
            this.lblHint.Location = new System.Drawing.Point(20, 130);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(380, 15);
            this.lblHint.TabIndex = 5;
            this.lblHint.Text = "Examples:  localhost   •   localhost:5000   •   192.168.1.10:5000";
            //
            // btnConnect
            //
            this.btnConnect.Location = new System.Drawing.Point(178, 168);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(105, 35);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            //
            // btnCancel
            //
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(295, 168);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(105, 35);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            //
            // ConnectForm
            //
            this.AcceptButton = this.btnConnect;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(420, 225);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.lblHint);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtServer);
            this.Controls.Add(this.lblServer);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConnectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Whiteboard - Connect";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblHint;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnCancel;
    }
}
