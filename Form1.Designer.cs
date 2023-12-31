namespace RedisAdmin
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnConnect = new Button();
            txtHostPort = new TextBox();
            lstKeys = new ListBox();
            txtValue = new TextBox();
            txtKeySearch = new TextBox();
            lblType = new Label();
            btnRefresh = new Button();
            lstEndpoints = new ListBox();
            btnAdd = new Button();
            cboKeyType = new ComboBox();
            btnSave = new Button();
            SuspendLayout();
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(693, 12);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(95, 23);
            btnConnect.TabIndex = 0;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // txtHostPort
            // 
            txtHostPort.Location = new Point(12, 12);
            txtHostPort.Name = "txtHostPort";
            txtHostPort.Size = new Size(149, 23);
            txtHostPort.TabIndex = 1;
            txtHostPort.Text = "127.0.0.1:6379";
            // 
            // lstKeys
            // 
            lstKeys.FormattingEnabled = true;
            lstKeys.ItemHeight = 15;
            lstKeys.Location = new Point(167, 71);
            lstKeys.Name = "lstKeys";
            lstKeys.Size = new Size(120, 364);
            lstKeys.TabIndex = 4;
            lstKeys.SelectedIndexChanged += lstKeys_SelectedIndexChanged;
            // 
            // txtValue
            // 
            txtValue.Location = new Point(293, 41);
            txtValue.Multiline = true;
            txtValue.Name = "txtValue";
            txtValue.Size = new Size(495, 397);
            txtValue.TabIndex = 5;
            // 
            // txtKeySearch
            // 
            txtKeySearch.Location = new Point(167, 12);
            txtKeySearch.Name = "txtKeySearch";
            txtKeySearch.Size = new Size(120, 23);
            txtKeySearch.TabIndex = 6;
            txtKeySearch.TextChanged += txtKeySearch_TextChanged;
            // 
            // lblType
            // 
            lblType.AutoSize = true;
            lblType.Location = new Point(293, 16);
            lblType.Name = "lblType";
            lblType.Size = new Size(62, 15);
            lblType.TabIndex = 7;
            lblType.Text = "DataType";
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(592, 11);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(95, 23);
            btnRefresh.TabIndex = 8;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // lstEndpoints
            // 
            lstEndpoints.FormattingEnabled = true;
            lstEndpoints.ItemHeight = 15;
            lstEndpoints.Location = new Point(12, 41);
            lstEndpoints.Name = "lstEndpoints";
            lstEndpoints.Size = new Size(149, 394);
            lstEndpoints.TabIndex = 9;
            lstEndpoints.SelectedIndexChanged += lstEndpoints_SelectedIndexChanged;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(265, 43);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(22, 23);
            btnAdd.TabIndex = 11;
            btnAdd.Text = "+";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // cboKeyType
            // 
            cboKeyType.FormattingEnabled = true;
            cboKeyType.Location = new Point(167, 43);
            cboKeyType.Name = "cboKeyType";
            cboKeyType.Size = new Size(92, 23);
            cboKeyType.TabIndex = 12;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(361, 11);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 13;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 449);
            Controls.Add(btnSave);
            Controls.Add(cboKeyType);
            Controls.Add(btnAdd);
            Controls.Add(lstEndpoints);
            Controls.Add(btnRefresh);
            Controls.Add(lblType);
            Controls.Add(txtKeySearch);
            Controls.Add(txtValue);
            Controls.Add(lstKeys);
            Controls.Add(txtHostPort);
            Controls.Add(btnConnect);
            Name = "Form1";
            Text = "Redis Admin";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnConnect;
        private TextBox txtHostPort;
        private ListBox lstKeys;
        private TextBox txtValue;
        private TextBox txtKeySearch;
        private Label lblType;
        private Button btnRefresh;
        private ListBox lstEndpoints;
        private Button btnAdd;
        private ComboBox cboKeyType;
        private Button btnSave;
    }
}
