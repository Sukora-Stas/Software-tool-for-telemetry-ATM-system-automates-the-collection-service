namespace GoogleAPIroutes_GMap.Forms
{
    partial class Registration
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label фИОLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Registration));
            this.button1 = new System.Windows.Forms.Button();
            this.inkasaciaDataSet = new GoogleAPIroutes_GMap.InkasaciaDataSet();
            this.logPasBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.logPasTableAdapter = new GoogleAPIroutes_GMap.InkasaciaDataSetTableAdapters.LogPasTableAdapter();
            this.tableAdapterManager = new GoogleAPIroutes_GMap.InkasaciaDataSetTableAdapters.TableAdapterManager();
            this.логинTextBox = new System.Windows.Forms.TextBox();
            this.фИОTextBox = new System.Windows.Forms.TextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.парольTextBox = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.регистрацияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            фИОLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.inkasaciaDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logPasBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // фИОLabel
            // 
            фИОLabel.AutoSize = true;
            фИОLabel.BackColor = System.Drawing.Color.Transparent;
            фИОLabel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            фИОLabel.Location = new System.Drawing.Point(46, 52);
            фИОLabel.Name = "фИОLabel";
            фИОLabel.Size = new System.Drawing.Size(187, 19);
            фИОLabel.TabIndex = 21;
            фИОLabel.Text = "Фамилия Имя Отчество";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(49, 204);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(188, 37);
            this.button1.TabIndex = 17;
            this.button1.Text = "Регистрация";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // inkasaciaDataSet
            // 
            this.inkasaciaDataSet.DataSetName = "InkasaciaDataSet";
            this.inkasaciaDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // logPasBindingSource
            // 
            this.logPasBindingSource.DataMember = "LogPas";
            this.logPasBindingSource.DataSource = this.inkasaciaDataSet;
            // 
            // logPasTableAdapter
            // 
            this.logPasTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.AutoparkTableAdapter = null;
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.BankiTableAdapter = null;
            this.tableAdapterManager.BankomatTableAdapter = null;
            this.tableAdapterManager.fineryTableAdapter = null;
            this.tableAdapterManager.infotableTableAdapter = null;
            this.tableAdapterManager.LogPasTableAdapter = this.logPasTableAdapter;
            this.tableAdapterManager.LOGTableAdapter = null;
            this.tableAdapterManager.RKCTableAdapter = null;
            this.tableAdapterManager.SotrudnikTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = GoogleAPIroutes_GMap.InkasaciaDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // логинTextBox
            // 
            this.логинTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.логинTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.логинTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.logPasBindingSource, "Логин", true));
            this.логинTextBox.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.логинTextBox.Location = new System.Drawing.Point(78, 115);
            this.логинTextBox.Name = "логинTextBox";
            this.логинTextBox.Size = new System.Drawing.Size(100, 19);
            this.логинTextBox.TabIndex = 18;
            this.логинTextBox.TextChanged += new System.EventHandler(this.логинTextBox_TextChanged);
            // 
            // фИОTextBox
            // 
            this.фИОTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.logPasBindingSource, "ФИО", true));
            this.фИОTextBox.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.фИОTextBox.Location = new System.Drawing.Point(50, 74);
            this.фИОTextBox.Name = "фИОTextBox";
            this.фИОTextBox.Size = new System.Drawing.Size(188, 26);
            this.фИОTextBox.TabIndex = 22;
          
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = global::GoogleAPIroutes_GMap.Properties.Resources.LogTextbox;
            this.pictureBox2.Location = new System.Drawing.Point(49, 106);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(206, 38);
            this.pictureBox2.TabIndex = 29;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::GoogleAPIroutes_GMap.Properties.Resources.PasTextbox;
            this.pictureBox1.Location = new System.Drawing.Point(49, 150);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(206, 38);
            this.pictureBox1.TabIndex = 28;
            this.pictureBox1.TabStop = false;
            // 
            // парольTextBox
            // 
            this.парольTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.парольTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.парольTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.logPasBindingSource, "Пароль", true));
            this.парольTextBox.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.парольTextBox.Location = new System.Drawing.Point(78, 159);
            this.парольTextBox.Name = "парольTextBox";
            this.парольTextBox.PasswordChar = '*';
            this.парольTextBox.Size = new System.Drawing.Size(100, 19);
            this.парольTextBox.TabIndex = 20;
            this.парольTextBox.TextChanged += new System.EventHandler(this.парольTextBox_TextChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.справкаToolStripMenuItem,
            this.выходToolStripMenuItem,
            this.регистрацияToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(296, 27);
            this.menuStrip1.TabIndex = 31;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(83, 23);
            this.справкаToolStripMenuItem.Text = "Справка";
            this.справкаToolStripMenuItem.Click += new System.EventHandler(this.справкаToolStripMenuItem_Click);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(68, 23);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // регистрацияToolStripMenuItem
            // 
            this.регистрацияToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.регистрацияToolStripMenuItem.Name = "регистрацияToolStripMenuItem";
            this.регистрацияToolStripMenuItem.Size = new System.Drawing.Size(63, 23);
            this.регистрацияToolStripMenuItem.Text = "Назад";
            this.регистрацияToolStripMenuItem.Click += new System.EventHandler(this.регистрацияToolStripMenuItem_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.pictureBox3.Image = global::GoogleAPIroutes_GMap.Properties.Resources.eye;
            this.pictureBox3.Location = new System.Drawing.Point(202, 154);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(30, 26);
            this.pictureBox3.TabIndex = 32;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.MouseLeave += new System.EventHandler(this.pictureBox3_MouseLeave);
            this.pictureBox3.MouseHover += new System.EventHandler(this.pictureBox3_MouseHover);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.Location = new System.Drawing.Point(50, 204);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(188, 37);
            this.button2.TabIndex = 33;
            this.button2.Text = "Назад";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // helpProvider1
            // 
            this.helpProvider1.HelpNamespace = "D:\\Преддипломная практика\\Diplom\\GoogleAPIroutes_GMap\\bin\\Debug\\ATM.chm";
            // 
            // Registration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::GoogleAPIroutes_GMap.Properties.Resources._974ac76887e9a13b12e4fd97c617208b;
            this.ClientSize = new System.Drawing.Size(296, 271);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.парольTextBox);
            this.Controls.Add(this.логинTextBox);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(фИОLabel);
            this.Controls.Add(this.фИОTextBox);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.helpProvider1.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Registration";
            this.helpProvider1.SetShowHelp(this, true);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Регистрация";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.inkasaciaDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logPasBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private InkasaciaDataSet inkasaciaDataSet;
        private System.Windows.Forms.BindingSource logPasBindingSource;
        private InkasaciaDataSetTableAdapters.LogPasTableAdapter logPasTableAdapter;
        private InkasaciaDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.TextBox логинTextBox;
        private System.Windows.Forms.TextBox фИОTextBox;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox парольTextBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem регистрацияToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.HelpProvider helpProvider1;
    }
}