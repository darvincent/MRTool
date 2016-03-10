namespace MRTools
{
    partial class ActMRTask
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
            this.TB_Duration = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.TB_TaskName = new System.Windows.Forms.TextBox();
            this.TB_Path = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TB_Port = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.CB_doRestart = new System.Windows.Forms.CheckBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.button_import = new System.Windows.Forms.Button();
            this.TB_ExecuteFile = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.CB_IsInPackage = new System.Windows.Forms.CheckBox();
            this.CB_doShut = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // TB_Duration
            // 
            this.TB_Duration.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(244)))), ((int)(((byte)(232)))));
            this.TB_Duration.Location = new System.Drawing.Point(33, 396);
            this.TB_Duration.MaxLength = 40;
            this.TB_Duration.Name = "TB_Duration";
            this.TB_Duration.Size = new System.Drawing.Size(180, 20);
            this.TB_Duration.TabIndex = 78;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(33, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 85;
            this.label1.Text = "Task Name";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.IndianRed;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.IndianRed;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.IndianRed;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.ForeColor = System.Drawing.Color.Firebrick;
            this.button2.Location = new System.Drawing.Point(58, 485);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(55, 23);
            this.button2.TabIndex = 82;
            this.button2.Text = "Delete";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.SeaGreen;
            this.button1.ForeColor = System.Drawing.Color.DarkGreen;
            this.button1.Location = new System.Drawing.Point(133, 485);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(55, 23);
            this.button1.TabIndex = 81;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TB_TaskName
            // 
            this.TB_TaskName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(244)))), ((int)(((byte)(232)))));
            this.TB_TaskName.Location = new System.Drawing.Point(33, 34);
            this.TB_TaskName.MaxLength = 20;
            this.TB_TaskName.Name = "TB_TaskName";
            this.TB_TaskName.Size = new System.Drawing.Size(180, 20);
            this.TB_TaskName.TabIndex = 76;
            // 
            // TB_Path
            // 
            this.TB_Path.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(244)))), ((int)(((byte)(232)))));
            this.TB_Path.Location = new System.Drawing.Point(33, 169);
            this.TB_Path.MaxLength = 16635;
            this.TB_Path.Name = "TB_Path";
            this.TB_Path.Size = new System.Drawing.Size(180, 20);
            this.TB_Path.TabIndex = 77;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label4.Location = new System.Drawing.Point(33, 379);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 84;
            this.label4.Text = "Execute Duration";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(33, 152);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 83;
            this.label2.Text = "Path";
            // 
            // TB_Port
            // 
            this.TB_Port.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(244)))), ((int)(((byte)(232)))));
            this.TB_Port.Location = new System.Drawing.Point(33, 215);
            this.TB_Port.MaxLength = 10;
            this.TB_Port.Name = "TB_Port";
            this.TB_Port.Size = new System.Drawing.Size(180, 20);
            this.TB_Port.TabIndex = 87;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(33, 198);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 88;
            this.label3.Text = "Port";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Enabled = false;
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePicker2.Location = new System.Drawing.Point(33, 346);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(180, 20);
            this.dateTimePicker2.TabIndex = 92;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label6.Location = new System.Drawing.Point(33, 329);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 90;
            this.label6.Text = "EndTime";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label7.Location = new System.Drawing.Point(33, 244);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 13);
            this.label7.TabIndex = 89;
            this.label7.Text = "StartTime";
            // 
            // CB_doRestart
            // 
            this.CB_doRestart.AutoSize = true;
            this.CB_doRestart.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.CB_doRestart.Location = new System.Drawing.Point(38, 432);
            this.CB_doRestart.Name = "CB_doRestart";
            this.CB_doRestart.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.CB_doRestart.Size = new System.Drawing.Size(78, 18);
            this.CB_doRestart.TabIndex = 93;
            this.CB_doRestart.Text = "doRestart";
            this.CB_doRestart.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePicker1.Location = new System.Drawing.Point(33, 261);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(180, 20);
            this.dateTimePicker1.TabIndex = 94;
            // 
            // button_import
            // 
            this.button_import.BackColor = System.Drawing.Color.Transparent;
            this.button_import.Enabled = false;
            this.button_import.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button_import.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_import.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button_import.Location = new System.Drawing.Point(166, 122);
            this.button_import.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button_import.Name = "button_import";
            this.button_import.Size = new System.Drawing.Size(47, 23);
            this.button_import.TabIndex = 101;
            this.button_import.Text = "Add";
            this.button_import.UseVisualStyleBackColor = false;
            this.button_import.Click += new System.EventHandler(this.button_import_Click);
            // 
            // TB_ExecuteFile
            // 
            this.TB_ExecuteFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(244)))), ((int)(((byte)(232)))));
            this.TB_ExecuteFile.Enabled = false;
            this.TB_ExecuteFile.Location = new System.Drawing.Point(33, 123);
            this.TB_ExecuteFile.MaxLength = 40;
            this.TB_ExecuteFile.Name = "TB_ExecuteFile";
            this.TB_ExecuteFile.Size = new System.Drawing.Size(132, 20);
            this.TB_ExecuteFile.TabIndex = 99;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label5.Location = new System.Drawing.Point(33, 106);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 100;
            this.label5.Text = "Execute File";
            // 
            // CB_IsInPackage
            // 
            this.CB_IsInPackage.AutoSize = true;
            this.CB_IsInPackage.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.CB_IsInPackage.Location = new System.Drawing.Point(38, 70);
            this.CB_IsInPackage.Name = "CB_IsInPackage";
            this.CB_IsInPackage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.CB_IsInPackage.Size = new System.Drawing.Size(92, 18);
            this.CB_IsInPackage.TabIndex = 98;
            this.CB_IsInPackage.Text = "IsInPackage";
            this.CB_IsInPackage.UseVisualStyleBackColor = true;
            this.CB_IsInPackage.CheckedChanged += new System.EventHandler(this.CB_IsInPackage_CheckedChanged);
            // 
            // CB_doShut
            // 
            this.CB_doShut.AutoSize = true;
            this.CB_doShut.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.CB_doShut.Location = new System.Drawing.Point(38, 297);
            this.CB_doShut.Name = "CB_doShut";
            this.CB_doShut.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.CB_doShut.Size = new System.Drawing.Size(106, 18);
            this.CB_doShut.TabIndex = 102;
            this.CB_doShut.Text = "needShutDown";
            this.CB_doShut.UseVisualStyleBackColor = true;
            this.CB_doShut.CheckedChanged += new System.EventHandler(this.CB_doShut_CheckedChanged);
            // 
            // ActMRTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(247, 540);
            this.Controls.Add(this.CB_doShut);
            this.Controls.Add(this.button_import);
            this.Controls.Add(this.TB_ExecuteFile);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.CB_IsInPackage);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.CB_doRestart);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.TB_Port);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TB_Duration);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.TB_TaskName);
            this.Controls.Add(this.TB_Path);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Name = "ActMRTask";
            this.Text = "ActMRTask";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TB_Duration;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox TB_TaskName;
        private System.Windows.Forms.TextBox TB_Path;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TB_Port;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox CB_doRestart;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button button_import;
        private System.Windows.Forms.TextBox TB_ExecuteFile;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox CB_IsInPackage;
        private System.Windows.Forms.CheckBox CB_doShut;
    }
}