namespace SapData_Automation
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.label1 = new System.Windows.Forms.Label();
            this.crystalButton6 = new SapData_Automation.CrystalButton();
            this.crystalButton4 = new SapData_Automation.CrystalButton();
            this.crystalButton3 = new SapData_Automation.CrystalButton();
            this.crystalButton2 = new SapData_Automation.CrystalButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 433);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 16;
            this.label1.Text = "label1";
            // 
            // crystalButton6
            // 
            this.crystalButton6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.crystalButton6.BackColor = System.Drawing.Color.Red;
            this.crystalButton6.Font = new System.Drawing.Font("新宋体", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.crystalButton6.Location = new System.Drawing.Point(534, 351);
            this.crystalButton6.Name = "crystalButton6";
            this.crystalButton6.Size = new System.Drawing.Size(129, 58);
            this.crystalButton6.TabIndex = 15;
            this.crystalButton6.Text = "退出系统";
            this.crystalButton6.UseVisualStyleBackColor = false;
            this.crystalButton6.Click += new System.EventHandler(this.crystalButton6_Click);
            // 
            // crystalButton4
            // 
            this.crystalButton4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.crystalButton4.BackColor = System.Drawing.Color.DarkTurquoise;
            this.crystalButton4.Font = new System.Drawing.Font("隶书", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.crystalButton4.Location = new System.Drawing.Point(143, 245);
            this.crystalButton4.Name = "crystalButton4";
            this.crystalButton4.Size = new System.Drawing.Size(400, 58);
            this.crystalButton4.TabIndex = 14;
            this.crystalButton4.Text = "DISFLOW";
            this.crystalButton4.UseVisualStyleBackColor = false;
            // 
            // crystalButton3
            // 
            this.crystalButton3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.crystalButton3.BackColor = System.Drawing.Color.DarkTurquoise;
            this.crystalButton3.Font = new System.Drawing.Font("隶书", 20F, System.Drawing.FontStyle.Bold);
            this.crystalButton3.Location = new System.Drawing.Point(143, 177);
            this.crystalButton3.Name = "crystalButton3";
            this.crystalButton3.Size = new System.Drawing.Size(400, 58);
            this.crystalButton3.TabIndex = 13;
            this.crystalButton3.Text = "PANDA";
            this.crystalButton3.UseVisualStyleBackColor = false;
            // 
            // crystalButton2
            // 
            this.crystalButton2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.crystalButton2.BackColor = System.Drawing.Color.DarkTurquoise;
            this.crystalButton2.Font = new System.Drawing.Font("隶书", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.crystalButton2.Location = new System.Drawing.Point(143, 109);
            this.crystalButton2.Name = "crystalButton2";
            this.crystalButton2.Size = new System.Drawing.Size(400, 58);
            this.crystalButton2.TabIndex = 12;
            this.crystalButton2.Text = "SAPTIS";
            this.crystalButton2.UseVisualStyleBackColor = true;
            this.crystalButton2.Click += new System.EventHandler(this.crystalButton2_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 454);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.crystalButton6);
            this.Controls.Add(this.crystalButton4);
            this.Controls.Add(this.crystalButton3);
            this.Controls.Add(this.crystalButton2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "复杂工程力学高性能计算平台";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CrystalButton crystalButton6;
        private CrystalButton crystalButton4;
        private CrystalButton crystalButton3;
        private CrystalButton crystalButton2;
        private System.Windows.Forms.Label label1;
    }
}