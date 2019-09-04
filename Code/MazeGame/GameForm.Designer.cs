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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.WealthBox = new System.Windows.Forms.TextBox();
            this.LevelBox = new System.Windows.Forms.TextBox();
            this.NewGameBtn = new System.Windows.Forms.Button();
            this.NextLevelBtn = new System.Windows.Forms.Button();
            this.Skills = new System.Windows.Forms.ListBox();
            this.MazeDisplayBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.MazeDisplayBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(210, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Current Level";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(430, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Player Wealth";
            // 
            // WealthBox
            // 
            this.WealthBox.Enabled = false;
            this.WealthBox.Location = new System.Drawing.Point(509, 17);
            this.WealthBox.Name = "WealthBox";
            this.WealthBox.ReadOnly = true;
            this.WealthBox.Size = new System.Drawing.Size(100, 20);
            this.WealthBox.TabIndex = 2;
            // 
            // LevelBox
            // 
            this.LevelBox.Enabled = false;
            this.LevelBox.Location = new System.Drawing.Point(286, 17);
            this.LevelBox.Name = "LevelBox";
            this.LevelBox.ReadOnly = true;
            this.LevelBox.Size = new System.Drawing.Size(100, 20);
            this.LevelBox.TabIndex = 3;
            // 
            // NewGameBtn
            // 
            this.NewGameBtn.Enabled = false;
            this.NewGameBtn.Location = new System.Drawing.Point(25, 105);
            this.NewGameBtn.Name = "NewGameBtn";
            this.NewGameBtn.Size = new System.Drawing.Size(99, 50);
            this.NewGameBtn.TabIndex = 4;
            this.NewGameBtn.Text = "Start new game";
            this.NewGameBtn.UseVisualStyleBackColor = true;
            this.NewGameBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // NextLevelBtn
            // 
            this.NextLevelBtn.Enabled = false;
            this.NextLevelBtn.Location = new System.Drawing.Point(25, 179);
            this.NextLevelBtn.Name = "NextLevelBtn";
            this.NextLevelBtn.Size = new System.Drawing.Size(99, 50);
            this.NextLevelBtn.TabIndex = 5;
            this.NextLevelBtn.Text = "Next Level";
            this.NextLevelBtn.UseVisualStyleBackColor = true;
            // 
            // Skills
            // 
            this.Skills.Enabled = false;
            this.Skills.FormattingEnabled = true;
            this.Skills.Location = new System.Drawing.Point(25, 294);
            this.Skills.Name = "Skills";
            this.Skills.Size = new System.Drawing.Size(99, 108);
            this.Skills.TabIndex = 6;
            this.Skills.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // MazeDisplayBox
            // 
            this.MazeDisplayBox.Location = new System.Drawing.Point(130, 43);
            this.MazeDisplayBox.Name = "MazeDisplayBox";
            this.MazeDisplayBox.Size = new System.Drawing.Size(519, 391);
            this.MazeDisplayBox.TabIndex = 1;
            this.MazeDisplayBox.TabStop = false;
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 467);
            this.Controls.Add(this.MazeDisplayBox);
            this.Controls.Add(this.Skills);
            this.Controls.Add(this.NextLevelBtn);
            this.Controls.Add(this.NewGameBtn);
            this.Controls.Add(this.LevelBox);
            this.Controls.Add(this.WealthBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "GameForm";
            this.Text = "GameForm";
            this.Load += new System.EventHandler(this.GameForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MazeDisplayBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox WealthBox;
        private System.Windows.Forms.TextBox LevelBox;
        private System.Windows.Forms.Button NewGameBtn;
        private System.Windows.Forms.Button NextLevelBtn;
        private System.Windows.Forms.ListBox Skills;
        private System.Windows.Forms.PictureBox MazeDisplayBox;
    }
}