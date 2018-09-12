namespace IdleUserLogoff_WinForms
{
  partial class MainForm
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
      this.MainPanel = new System.Windows.Forms.Panel();
      this.MainMessage = new System.Windows.Forms.Label();
      this.MainPanel.SuspendLayout();
      this.SuspendLayout();
      // 
      // MainPanel
      // 
      this.MainPanel.Controls.Add(this.MainMessage);
      this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.MainPanel.Location = new System.Drawing.Point(0, 0);
      this.MainPanel.Name = "MainPanel";
      this.MainPanel.Size = new System.Drawing.Size(504, 171);
      this.MainPanel.TabIndex = 0;
      // 
      // MainMessage
      // 
      this.MainMessage.BackColor = System.Drawing.SystemColors.Control;
      this.MainMessage.Dock = System.Windows.Forms.DockStyle.Fill;
      this.MainMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.MainMessage.ForeColor = System.Drawing.SystemColors.ControlText;
      this.MainMessage.Location = new System.Drawing.Point(0, 0);
      this.MainMessage.Name = "MainMessage";
      this.MainMessage.Size = new System.Drawing.Size(504, 171);
      this.MainMessage.TabIndex = 0;
      this.MainMessage.Text = "LOGGING OFF IN 30 SECONDS\r\n(Any unsaved work will not be saved.)\r\n\r\nPlease move t" +
    "he mouse to cancel.\r\n";
      this.MainMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.ControlDark;
      this.ClientSize = new System.Drawing.Size(504, 171);
      this.Controls.Add(this.MainPanel);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "MainForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "LOGGING OFF";
      this.TopMost = true;
      this.MainPanel.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel MainPanel;
    private System.Windows.Forms.Label MainMessage;
  }
}

