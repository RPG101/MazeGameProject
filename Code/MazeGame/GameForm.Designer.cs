namespace MazeGame
{
    partial class GameForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.WealthBox = new System.Windows.Forms.TextBox();
            this.NewGameBtn = new System.Windows.Forms.Button();
            this.MazeDisplayBox = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Healthbox = new System.Windows.Forms.TextBox();
            this.Healthlbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.MazeDisplayBox)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(287, 21);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Player Wealth";
            // 
            // WealthBox
            // 
            this.WealthBox.Enabled = false;
            this.WealthBox.Location = new System.Drawing.Point(391, 18);
            this.WealthBox.Margin = new System.Windows.Forms.Padding(4);
            this.WealthBox.Name = "WealthBox";
            this.WealthBox.ReadOnly = true;
            this.WealthBox.Size = new System.Drawing.Size(132, 22);
            this.WealthBox.TabIndex = 2;
            // 
            // NewGameBtn
            // 
            this.NewGameBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.NewGameBtn.Location = new System.Drawing.Point(4, 4);
            this.NewGameBtn.Margin = new System.Windows.Forms.Padding(4);
            this.NewGameBtn.Name = "NewGameBtn";
            this.NewGameBtn.Size = new System.Drawing.Size(132, 62);
            this.NewGameBtn.TabIndex = 4;
            this.NewGameBtn.TabStop = false;
            this.NewGameBtn.Text = "Start new game";
            this.NewGameBtn.UseVisualStyleBackColor = true;
            this.NewGameBtn.Click += new System.EventHandler(this.button1_MouseClick);
            // 
            // MazeDisplayBox
            // 
            this.MazeDisplayBox.Location = new System.Drawing.Point(163, 65);
            this.MazeDisplayBox.Margin = new System.Windows.Forms.Padding(4);
            this.MazeDisplayBox.Name = "MazeDisplayBox";
            this.MazeDisplayBox.Size = new System.Drawing.Size(500, 297);
            this.MazeDisplayBox.TabIndex = 1;
            this.MazeDisplayBox.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.NewGameBtn);
            this.panel1.Location = new System.Drawing.Point(12, 81);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(144, 71);
            this.panel1.TabIndex = 5;
            // 
            // Healthbox
            // 
            this.Healthbox.Enabled = false;
            this.Healthbox.Location = new System.Drawing.Point(134, 18);
            this.Healthbox.Margin = new System.Windows.Forms.Padding(4);
            this.Healthbox.Name = "Healthbox";
            this.Healthbox.ReadOnly = true;
            this.Healthbox.Size = new System.Drawing.Size(132, 22);
            this.Healthbox.TabIndex = 6;
            // 
            // Healthlbl
            // 
            this.Healthlbl.AutoSize = true;
            this.Healthlbl.Location = new System.Drawing.Point(30, 21);
            this.Healthlbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Healthlbl.Name = "Healthlbl";
            this.Healthlbl.Size = new System.Drawing.Size(93, 17);
            this.Healthlbl.TabIndex = 7;
            this.Healthlbl.Text = "Player Health";
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 374);
            this.Controls.Add(this.Healthlbl);
            this.Controls.Add(this.Healthbox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.MazeDisplayBox);
            this.Controls.Add(this.WealthBox);
            this.Controls.Add(this.label2);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "GameForm";
            this.Text = "GameForm";
            this.Load += new System.EventHandler(this.GameForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MazeDisplayBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox WealthBox;
        private System.Windows.Forms.Button NewGameBtn;
        private System.Windows.Forms.PictureBox MazeDisplayBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox Healthbox;
        private System.Windows.Forms.Label Healthlbl;
    }
}