namespace MRTools
{
    partial class ActDeployTask
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
            this.CB_IsInPackage = new System.Windows.Forms.CheckBox();
            this.TB_Sequence = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TB_Duration = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.TB_TaskName = new System.Windows.Forms.TextBox();
            this.TB_Path = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button_import = new System.Windows.Forms.Button();
            this.TB_ExecuteFile = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // CB_IsInPackage
            // 
            this.CB_IsInPackage.AutoSize = true;
            this.CB_IsInPackage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CB_IsInPackage.Location = new System.Drawing.Point(33, 71);
            this.CB_IsInPackage.Name = "CB_IsInPackage";
            this.CB_IsInPackage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.CB_IsInPackage.Size = new System.Drawing.Size(83, 17);
            this.CB_IsInPackage.TabIndex = 68;
            this.CB_IsInPackage.Text = "IsInPackage";
            this.CB_IsInPackage.UseVisualStyleBackColor = true;
            this.CB_IsInPackage.CheckedChanged += new System.EventHandler(this.CB_IsInPackage_CheckedChanged);
            // 
            // TB_Sequence
            // 
            this.TB_Sequence.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(244)))), ((int)(((byte)(232)))));
            this.TB_Sequence.Location = new System.Drawing.Point(31, 259);
            this.TB_Sequence.MaxLength = 2;
            this.TB_Sequence.Name = "TB_Sequence";
            this.TB_Sequence.Size = new System.Drawing.Size(180, 20);
            this.TB_Sequence.TabIndex = 67;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label5.Location = new System.Drawing.Point(31, 242);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 75;
            this.label5.Text = "Sequence";
            // 
            // TB_Duration
            // 
            this.TB_Duration.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(244)))), ((int)(((byte)(232)))));
            this.TB_Duration.Location = new System.Drawing.Point(31, 214);
            this.TB_Duration.MaxLength = 40;
            this.TB_Duration.Name = "TB_Duration";
            this.TB_Duration.Size = new System.Drawing.Size(180, 20);
            this.TB_Duration.TabIndex = 66;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(31, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 74;
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
            this.button2.Location = new System.Drawing.Point(55, 320);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(55, 23);
            this.button2.TabIndex = 71;
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
            this.button1.Location = new System.Drawing.Point(133, 320);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(55, 23);
            this.button1.TabIndex = 70;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TB_TaskName
            // 
            this.TB_TaskName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(244)))), ((int)(((byte)(232)))));
            this.TB_TaskName.Location = new System.Drawing.Point(31, 37);
            this.TB_TaskName.MaxLength = 20;
            this.TB_TaskName.Name = "TB_TaskName";
            this.TB_TaskName.Size = new System.Drawing.Size(180, 20);
            this.TB_TaskName.TabIndex = 64;
            // 
            // TB_Path
            // 
            this.TB_Path.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(244)))), ((int)(((byte)(232)))));
            this.TB_Path.Location = new System.Drawing.Point(31, 169);
            this.TB_Path.MaxLength = 16635;
            this.TB_Path.Name = "TB_Path";
            this.TB_Path.Size = new System.Drawing.Size(180, 20);
            this.TB_Path.TabIndex = 65;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label4.Location = new System.Drawing.Point(31, 197);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 73;
            this.label4.Text = "Execute Duration";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(31, 152);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 72;
            this.label2.Text = "Path";
            // 
            // button_import
            // 
            this.button_import.BackColor = System.Drawing.Color.Transparent;
            this.button_import.Enabled = false;
            this.button_import.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button_import.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_import.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button_import.Location = new System.Drawing.Point(164, 123);
            this.button_import.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button_import.Name = "button_import";
            this.button_import.Size = new System.Drawing.Size(47, 23);
            this.button_import.TabIndex = 104;
            this.button_import.Text = "Add";
            this.button_import.UseVisualStyleBackColor = false;
            this.button_import.Click += new System.EventHandler(this.button_import_Click);
            // 
            // TB_ExecuteFile
            // 
            this.TB_ExecuteFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(244)))), ((int)(((byte)(232)))));
            this.TB_ExecuteFile.Enabled = false;
            this.TB_ExecuteFile.Location = new System.Drawing.Point(31, 124);
            this.TB_ExecuteFile.MaxLength = 40;
            this.TB_ExecuteFile.Name = "TB_ExecuteFile";
            this.TB_ExecuteFile.Size = new System.Drawing.Size(132, 20);
            this.TB_ExecuteFile.TabIndex = 102;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(31, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 103;
            this.label3.Text = "Execute File";
            // 
            // ActDeployTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(243, 379);
            this.Controls.Add(this.button_import);
            this.Controls.Add(this.TB_ExecuteFile);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CB_IsInPackage);
            this.Controls.Add(this.TB_Sequence);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.TB_Duration);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.TB_TaskName);
            this.Controls.Add(this.TB_Path);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Name = "ActDeployTask";
            this.Text = "ActDeployTask";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox CB_IsInPackage;
        private System.Windows.Forms.TextBox TB_Sequence;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TB_Duration;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox TB_TaskName;
        private System.Windows.Forms.TextBox TB_Path;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_import;
        private System.Windows.Forms.TextBox TB_ExecuteFile;
        private System.Windows.Forms.Label label3;
    }
}