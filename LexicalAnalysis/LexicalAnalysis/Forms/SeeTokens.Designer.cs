namespace LexicalAnalysis
{
    partial class SeeTokens
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
            this.logBox = new System.Windows.Forms.RichTextBox();
            this.Log = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // logBox
            // 
            this.logBox.Location = new System.Drawing.Point(12, 63);
            this.logBox.Name = "logBox";
            this.logBox.Size = new System.Drawing.Size(604, 489);
            this.logBox.TabIndex = 0;
            this.logBox.Text = "";
            // 
            // Log
            // 
            this.Log.AutoSize = true;
            this.Log.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Log.Location = new System.Drawing.Point(12, 40);
            this.Log.Name = "Log";
            this.Log.Size = new System.Drawing.Size(39, 20);
            this.Log.TabIndex = 1;
            this.Log.Text = "Log";
            // 
            // SeeTokens
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 577);
            this.Controls.Add(this.Log);
            this.Controls.Add(this.logBox);
            this.Name = "SeeTokens";
            this.Text = "SeeTokens";
            this.Load += new System.EventHandler(this.SeeTokens_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox logBox;
        private System.Windows.Forms.Label Log;
    }
}