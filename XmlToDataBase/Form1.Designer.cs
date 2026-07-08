namespace XmlToDataBase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            openFileDialog1 = new OpenFileDialog();
            saveFileDialog1 = new SaveFileDialog();
            btnSearch = new Button();
            btnBackup = new Button();
            btnRestore = new Button();
            dataGridView1 = new DataGridView();
            txtenterid = new TextBox();
            lblID = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // btnSearch
            // 
            btnSearch.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold);
            btnSearch.Location = new Point(265, 25);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(114, 28);
            btnSearch.TabIndex = 1;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += BtnSearch_Click;
            // 
            // btnBackup
            // 
            btnBackup.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold);
            btnBackup.Location = new Point(385, 25);
            btnBackup.Name = "btnBackup";
            btnBackup.Size = new Size(114, 28);
            btnBackup.TabIndex = 2;
            btnBackup.Text = "Backup";
            btnBackup.UseVisualStyleBackColor = true;
            btnBackup.Click += BtnBackup_Click;
            // 
            // btnRestore
            // 
            btnRestore.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold);
            btnRestore.Location = new Point(505, 25);
            btnRestore.Name = "btnRestore";
            btnRestore.Size = new Size(114, 28);
            btnRestore.TabIndex = 3;
            btnRestore.Text = "Restore";
            btnRestore.UseVisualStyleBackColor = true;
            btnRestore.Click += BtnRestore_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 70);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(607, 350);
            dataGridView1.TabIndex = 4;
            // 
            // txtenterid
            // 
            txtenterid.Location = new Point(136, 30);
            txtenterid.Name = "txtenterid";
            txtenterid.Size = new Size(123, 23);
            txtenterid.TabIndex = 0;
            // 
            // lblID
            // 
            lblID.AutoSize = true;
            lblID.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblID.Location = new Point(30, 32);
            lblID.Name = "lblID";
            lblID.Size = new Size(69, 18);
            lblID.TabIndex = 5;
            lblID.Text = "User ID:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(631, 432);
            Controls.Add(lblID);
            Controls.Add(txtenterid);
            Controls.Add(dataGridView1);
            Controls.Add(btnRestore);
            Controls.Add(btnBackup);
            Controls.Add(btnSearch);
            Name = "Form1";
            Text = "Database Manager";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private OpenFileDialog openFileDialog1;
        private SaveFileDialog saveFileDialog1;
        private Button btnSearch;
        private Button btnBackup;
        private Button btnRestore;
        private DataGridView dataGridView1;
        private TextBox txtenterid;
        private Label lblID;
    }
}
