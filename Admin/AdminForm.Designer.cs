namespace Admin
{
    partial class AdminForm
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
            this.lblServer = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.lblConn = new System.Windows.Forms.Label();
            this.lblCountTitle = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.lblBadge = new System.Windows.Forms.Label();
            this.lblClientsTitle = new System.Windows.Forms.Label();
            this.dgvClients = new System.Windows.Forms.DataGridView();
            this.lblEventsTitle = new System.Windows.Forms.Label();
            this.dgvEvents = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClients)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEvents)).BeginInit();
            this.SuspendLayout();
            //
            // lblServer
            //
            this.lblServer.AutoSize = true;
            this.lblServer.Location = new System.Drawing.Point(14, 18);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(45, 15);
            this.lblServer.TabIndex = 0;
            this.lblServer.Text = "Server:";
            //
            // txtServer
            //
            this.txtServer.Location = new System.Drawing.Point(70, 15);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(230, 23);
            this.txtServer.TabIndex = 0;
            this.txtServer.Text = "localhost:5000";
            //
            // btnConnect
            //
            this.btnConnect.Location = new System.Drawing.Point(310, 14);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(90, 26);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            //
            // lblConn
            //
            this.lblConn.AutoSize = true;
            this.lblConn.ForeColor = System.Drawing.Color.Gray;
            this.lblConn.Location = new System.Drawing.Point(410, 18);
            this.lblConn.Name = "lblConn";
            this.lblConn.Size = new System.Drawing.Size(86, 15);
            this.lblConn.TabIndex = 2;
            this.lblConn.Text = "Not connected";
            //
            // lblCountTitle
            //
            this.lblCountTitle.AutoSize = true;
            this.lblCountTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblCountTitle.Location = new System.Drawing.Point(16, 58);
            this.lblCountTitle.Name = "lblCountTitle";
            this.lblCountTitle.Size = new System.Drawing.Size(119, 15);
            this.lblCountTitle.TabIndex = 3;
            this.lblCountTitle.Text = "CONNECTED CLIENTS";
            //
            // lblCount
            //
            this.lblCount.AutoSize = true;
            this.lblCount.Font = new System.Drawing.Font("Segoe UI", 34F, System.Drawing.FontStyle.Bold);
            this.lblCount.Location = new System.Drawing.Point(12, 76);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(120, 61);
            this.lblCount.TabIndex = 4;
            this.lblCount.Text = "0 / 5";
            //
            // lblBadge
            //
            this.lblBadge.AutoSize = true;
            this.lblBadge.BackColor = System.Drawing.Color.ForestGreen;
            this.lblBadge.ForeColor = System.Drawing.Color.White;
            this.lblBadge.Location = new System.Drawing.Point(16, 150);
            this.lblBadge.Name = "lblBadge";
            this.lblBadge.Padding = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this.lblBadge.Size = new System.Drawing.Size(52, 23);
            this.lblBadge.TabIndex = 5;
            this.lblBadge.Text = "OPEN";
            //
            // lblClientsTitle
            //
            this.lblClientsTitle.AutoSize = true;
            this.lblClientsTitle.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblClientsTitle.Location = new System.Drawing.Point(238, 58);
            this.lblClientsTitle.Name = "lblClientsTitle";
            this.lblClientsTitle.Size = new System.Drawing.Size(126, 17);
            this.lblClientsTitle.TabIndex = 6;
            this.lblClientsTitle.Text = "Clients in the room";
            //
            // dgvClients
            //
            this.dgvClients.AllowUserToAddRows = false;
            this.dgvClients.AllowUserToDeleteRows = false;
            this.dgvClients.AllowUserToResizeRows = false;
            this.dgvClients.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvClients.BackgroundColor = System.Drawing.Color.White;
            this.dgvClients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClients.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvClients.Location = new System.Drawing.Point(238, 78);
            this.dgvClients.MultiSelect = false;
            this.dgvClients.Name = "dgvClients";
            this.dgvClients.ReadOnly = true;
            this.dgvClients.RowHeadersVisible = false;
            this.dgvClients.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvClients.Size = new System.Drawing.Size(548, 150);
            this.dgvClients.TabIndex = 7;
            //
            // lblEventsTitle
            //
            this.lblEventsTitle.AutoSize = true;
            this.lblEventsTitle.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblEventsTitle.Location = new System.Drawing.Point(14, 245);
            this.lblEventsTitle.Name = "lblEventsTitle";
            this.lblEventsTitle.Size = new System.Drawing.Size(90, 17);
            this.lblEventsTitle.TabIndex = 8;
            this.lblEventsTitle.Text = "Notifications";
            //
            // dgvEvents
            //
            this.dgvEvents.AllowUserToAddRows = false;
            this.dgvEvents.AllowUserToDeleteRows = false;
            this.dgvEvents.AllowUserToResizeRows = false;
            this.dgvEvents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvEvents.BackgroundColor = System.Drawing.Color.White;
            this.dgvEvents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEvents.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvEvents.Location = new System.Drawing.Point(14, 266);
            this.dgvEvents.MultiSelect = false;
            this.dgvEvents.Name = "dgvEvents";
            this.dgvEvents.ReadOnly = true;
            this.dgvEvents.RowHeadersVisible = false;
            this.dgvEvents.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEvents.Size = new System.Drawing.Size(772, 368);
            this.dgvEvents.TabIndex = 9;
            //
            // AdminForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 650);
            this.Controls.Add(this.dgvEvents);
            this.Controls.Add(this.lblEventsTitle);
            this.Controls.Add(this.dgvClients);
            this.Controls.Add(this.lblClientsTitle);
            this.Controls.Add(this.lblBadge);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.lblCountTitle);
            this.Controls.Add(this.lblConn);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.txtServer);
            this.Controls.Add(this.lblServer);
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "AdminForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Whiteboard - Admin";
            ((System.ComponentModel.ISupportInitialize)(this.dgvClients)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEvents)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label lblConn;
        private System.Windows.Forms.Label lblCountTitle;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label lblBadge;
        private System.Windows.Forms.Label lblClientsTitle;
        private System.Windows.Forms.DataGridView dgvClients;
        private System.Windows.Forms.Label lblEventsTitle;
        private System.Windows.Forms.DataGridView dgvEvents;
    }
}
