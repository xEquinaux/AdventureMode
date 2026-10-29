namespace gui;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        checkedListBox1 = new CheckedListBox();
        button1 = new Button();
        comboBox1 = new ComboBox();
        SuspendLayout();
        // 
        // checkedListBox1
        // 
        checkedListBox1.FormattingEnabled = true;
        checkedListBox1.Location = new Point(12, 12);
        checkedListBox1.Name = "checkedListBox1";
        checkedListBox1.Size = new Size(495, 202);
        checkedListBox1.TabIndex = 0;
        // 
        // button1
        // 
        button1.Location = new Point(275, 220);
        button1.Name = "button1";
        button1.Size = new Size(232, 23);
        button1.TabIndex = 1;
        button1.Text = "Generate";
        button1.UseVisualStyleBackColor = true;
        button1.Click += button1_Click;
        // 
        // comboBox1
        // 
        comboBox1.FormattingEnabled = true;
        comboBox1.Items.AddRange(new object[] { "Select how many bounties per act", "1", "2", "3", "4", "5", "6", "7", "8" });
        comboBox1.Location = new Point(12, 220);
        comboBox1.Name = "comboBox1";
        comboBox1.Size = new Size(257, 23);
        comboBox1.TabIndex = 2;
        comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(518, 249);
        Controls.Add(comboBox1);
        Controls.Add(button1);
        Controls.Add(checkedListBox1);
        Name = "Form1";
        Text = "Adventure Mode";
        ResumeLayout(false);
    }

    #endregion

    private CheckedListBox checkedListBox1;
    private Button button1;
    private ComboBox comboBox1;
}
