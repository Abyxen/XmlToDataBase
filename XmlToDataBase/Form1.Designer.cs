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
            btnLoad = new Button();
            btnDelete = new Button();
            btnRestore = new Button();
            dataGridView1 = new DataGridView();
            txtenterid = new TextBox();
            lblID = new Label();
            textBox1 = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnLoad
            // 
            btnLoad.BackgroundImage = Properties.Resources.Screenshot_2026_05_27_222906;
            btnLoad.Font = new Font("Unispace", 11.25F, FontStyle.Bold);
            btnLoad.Location = new Point(475, 61);
            btnLoad.Margin = new Padding(3, 4, 3, 4);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(130, 37);
            btnLoad.TabIndex = 0;
            btnLoad.Text = "Load";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackgroundImage = Properties.Resources.Screenshot_2026_05_27_222906;
            btnDelete.Font = new Font("Unispace", 11.25F, FontStyle.Bold);
            btnDelete.Location = new Point(350, 493);
            btnDelete.Margin = new Padding(3, 4, 3, 4);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(130, 37);
            btnDelete.TabIndex = 1;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnRestore
            // 
            btnRestore.BackgroundImage = Properties.Resources.Screenshot_2026_05_27_222906;
            btnRestore.Font = new Font("Unispace", 11.25F, FontStyle.Bold);
            btnRestore.Location = new Point(498, 489);
            btnRestore.Margin = new Padding(3, 4, 3, 4);
            btnRestore.Name = "btnRestore";
            btnRestore.Size = new Size(130, 37);
            btnRestore.TabIndex = 2;
            btnRestore.Text = "Restore";
            btnRestore.UseVisualStyleBackColor = true;
            btnRestore.Click += button3_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(14, 136);
            dataGridView1.Margin = new Padding(3, 4, 3, 4);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(659, 325);
            dataGridView1.TabIndex = 3;
            // 
            // txtenterid
            // 
            txtenterid.Location = new Point(155, 500);
            txtenterid.Margin = new Padding(3, 4, 3, 4);
            txtenterid.Name = "txtenterid";
            txtenterid.Size = new Size(114, 27);
            txtenterid.TabIndex = 4;
            // 
            // lblID
            // 
            lblID.AutoSize = true;
            lblID.Font = new Font("Unispace", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblID.Location = new Point(34, 503);
            lblID.Name = "lblID";
            lblID.Size = new Size(106, 23);
            lblID.TabIndex = 5;
            lblID.Text = "Enter ID";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(34, 67);
            textBox1.Margin = new Padding(3, 4, 3, 4);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(416, 27);
            textBox1.TabIndex = 6;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Screenshot_2026_05_27_222906;
            ClientSize = new Size(701, 576);
            Controls.Add(textBox1);
            Controls.Add(lblID);
            Controls.Add(txtenterid);
            Controls.Add(dataGridView1);
            Controls.Add(btnRestore);
            Controls.Add(btnDelete);
            Controls.Add(btnLoad);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private OpenFileDialog openFileDialog1;
        private SaveFileDialog saveFileDialog1;
        private Button btnLoad;
        private Button btnDelete;
        private Button btnRestore;
        private DataGridView dataGridView1;
        private TextBox txtenterid;
        private Label lblID;
        private TextBox textBox1;
    }
}
