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
            this.ControlsTxt = new System.Windows.Forms.RichTextBox();
            this.Healthbox = new System.Windows.Forms.TextBox();
            this.Healthlbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.MazeDisplayBox)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(215, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Player Wealth";
            // 
            // WealthBox
            // 
            this.WealthBox.Enabled = false;
            this.WealthBox.Location = new System.Drawing.Point(293, 15);
            this.WealthBox.Name = "WealthBox";
            this.WealthBox.ReadOnly = true;
            this.WealthBox.Size = new System.Drawing.Size(100, 20);
            this.WealthBox.TabIndex = 2;
            // 
            // NewGameBtn
            // 
            this.NewGameBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.NewGameBtn.Location = new System.Drawing.Point(27, 22);
            this.NewGameBtn.Name = "NewGameBtn";
            this.NewGameBtn.Size = new System.Drawing.Size(99, 50);
            this.NewGameBtn.TabIndex = 4;
            this.NewGameBtn.TabStop = false;
            this.NewGameBtn.Text = "Start new game";
            this.NewGameBtn.UseVisualStyleBackColor = true;
            this.NewGameBtn.Click += new System.EventHandler(this.button1_MouseClick);
            // 
            // MazeDisplayBox
            // 
            this.MazeDisplayBox.Location = new System.Drawing.Point(165, 53);
            this.MazeDisplayBox.Name = "MazeDisplayBox";
            this.MazeDisplayBox.Size = new System.Drawing.Size(375, 241);
            this.MazeDisplayBox.TabIndex = 1;
            this.MazeDisplayBox.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ControlsTxt);
            this.panel1.Controls.Add(this.NewGameBtn);
            this.panel1.Location = new System.Drawing.Point(9, 40);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(151, 254);
            this.panel1.TabIndex = 5;
            // 
            // ControlsTxt
            // 
            this.ControlsTxt.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.ControlsTxt.Location = new System.Drawing.Point(13, 99);
            this.ControlsTxt.Name = "ControlsTxt";
            this.ControlsTxt.ReadOnly = true;
            this.ControlsTxt.Size = new System.Drawing.Size(135, 152);
            this.ControlsTxt.TabIndex = 8;
            this.ControlsTxt.TabStop = false;
            this.ControlsTxt.Text = "";
            // 
            // Healthbox
            // 
            this.Healthbox.Enabled = false;
            this.Healthbox.Location = new System.Drawing.Point(100, 15);
            this.Healthbox.Name = "Healthbox";
            this.Healthbox.ReadOnly = true;
            this.Healthbox.Size = new System.Drawing.Size(100, 20);
            this.Healthbox.TabIndex = 6;
            // 
            // Healthlbl
            // 
            this.Healthlbl.AutoSize = true;
            this.Healthlbl.Location = new System.Drawing.Point(22, 17);
            this.Healthlbl.Name = "Healthlbl";
            this.Healthlbl.Size = new System.Drawing.Size(70, 13);
            this.Healthlbl.TabIndex = 7;
            this.Healthlbl.Text = "Player Health";
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 314);
            this.Controls.Add(this.Healthlbl);
            this.Controls.Add(this.Healthbox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.MazeDisplayBox);
            this.Controls.Add(this.WealthBox);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
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
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox Healthbox;
        private System.Windows.Forms.Label Healthlbl;
        private System.Windows.Forms.RichTextBox ControlsTxt;
        public System.Windows.Forms.PictureBox MazeDisplayBox;
    }
}