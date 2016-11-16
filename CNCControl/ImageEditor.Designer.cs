namespace CNCControl
{
    partial class ImageEditor
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbBrightness = new System.Windows.Forms.TrackBar();
            this.tbContrast = new System.Windows.Forms.TrackBar();
            this.tbGamma = new System.Windows.Forms.TrackBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblGamma = new System.Windows.Forms.Label();
            this.lblContrast = new System.Windows.Forms.Label();
            this.lblBrightness = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbBrightness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbContrast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbGamma)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(700, 700);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(718, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(420, 699);
            this.panel1.TabIndex = 1;
            // 
            // tbBrightness
            // 
            this.tbBrightness.Location = new System.Drawing.Point(6, 32);
            this.tbBrightness.Maximum = 127;
            this.tbBrightness.Minimum = -127;
            this.tbBrightness.Name = "tbBrightness";
            this.tbBrightness.Size = new System.Drawing.Size(330, 45);
            this.tbBrightness.TabIndex = 0;
            this.tbBrightness.TickFrequency = 10;
            this.tbBrightness.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // tbContrast
            // 
            this.tbContrast.Location = new System.Drawing.Point(6, 96);
            this.tbContrast.Maximum = 127;
            this.tbContrast.Minimum = -127;
            this.tbContrast.Name = "tbContrast";
            this.tbContrast.Size = new System.Drawing.Size(330, 45);
            this.tbContrast.TabIndex = 1;
            this.tbContrast.TickFrequency = 10;
            this.tbContrast.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // tbGamma
            // 
            this.tbGamma.Location = new System.Drawing.Point(6, 164);
            this.tbGamma.Maximum = 500;
            this.tbGamma.Name = "tbGamma";
            this.tbGamma.Size = new System.Drawing.Size(330, 45);
            this.tbGamma.TabIndex = 2;
            this.tbGamma.TickFrequency = 22;
            this.tbGamma.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.tbGamma.Value = 50;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblBrightness);
            this.groupBox1.Controls.Add(this.lblContrast);
            this.groupBox1.Controls.Add(this.lblGamma);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbBrightness);
            this.groupBox1.Controls.Add(this.tbGamma);
            this.groupBox1.Controls.Add(this.tbContrast);
            this.groupBox1.Location = new System.Drawing.Point(24, 35);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(342, 220);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Brightness";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Contrast";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 148);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Gamma";
            // 
            // lblGamma
            // 
            this.lblGamma.AutoSize = true;
            this.lblGamma.Location = new System.Drawing.Point(163, 148);
            this.lblGamma.Name = "lblGamma";
            this.lblGamma.Size = new System.Drawing.Size(19, 13);
            this.lblGamma.TabIndex = 6;
            this.lblGamma.Text = "00";
            this.lblGamma.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblContrast
            // 
            this.lblContrast.AutoSize = true;
            this.lblContrast.Location = new System.Drawing.Point(151, 80);
            this.lblContrast.Name = "lblContrast";
            this.lblContrast.Size = new System.Drawing.Size(31, 13);
            this.lblContrast.TabIndex = 7;
            this.lblContrast.Text = "0000";
            this.lblContrast.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblBrightness
            // 
            this.lblBrightness.AutoSize = true;
            this.lblBrightness.Location = new System.Drawing.Point(151, 16);
            this.lblBrightness.Name = "lblBrightness";
            this.lblBrightness.Size = new System.Drawing.Size(31, 13);
            this.lblBrightness.TabIndex = 8;
            this.lblBrightness.Text = "0000";
            this.lblBrightness.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ImageEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1150, 724);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "ImageEditor";
            this.Text = "ImageEditor";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tbBrightness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbContrast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbGamma)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TrackBar tbBrightness;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblBrightness;
        private System.Windows.Forms.Label lblContrast;
        private System.Windows.Forms.Label lblGamma;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar tbGamma;
        private System.Windows.Forms.TrackBar tbContrast;
    }
}