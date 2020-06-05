namespace Scanner
{
    partial class Form1
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
            this.codeLinesTextBox = new System.Windows.Forms.TextBox();
            this.DataGridView = new System.Windows.Forms.DataGridView();
            this.TokenType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TokenValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Go = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // codeLinesTextBox
            // 
            this.codeLinesTextBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.codeLinesTextBox.Location = new System.Drawing.Point(2, 0);
            this.codeLinesTextBox.Multiline = true;
            this.codeLinesTextBox.Name = "codeLinesTextBox";
            this.codeLinesTextBox.Size = new System.Drawing.Size(335, 322);
            this.codeLinesTextBox.TabIndex = 0;
            this.codeLinesTextBox.TextChanged += new System.EventHandler(this.codeLinesTextBox_TextChanged);
            // 
            // DataGridView
            // 
            this.DataGridView.AllowUserToDeleteRows = false;
            this.DataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TokenType,
            this.TokenValue});
            this.DataGridView.Location = new System.Drawing.Point(343, 0);
            this.DataGridView.Name = "DataGridView";
            this.DataGridView.ReadOnly = true;
            this.DataGridView.RowHeadersVisible = false;
            this.DataGridView.Size = new System.Drawing.Size(270, 376);
            this.DataGridView.TabIndex = 2;
            // 
            // TokenType
            // 
            this.TokenType.HeaderText = "Token Type";
            this.TokenType.Name = "TokenType";
            this.TokenType.ReadOnly = true;
            // 
            // TokenValue
            // 
            this.TokenValue.HeaderText = "Token Value";
            this.TokenValue.Name = "TokenValue";
            this.TokenValue.ReadOnly = true;
            // 
            // Go
            // 
            this.Go.Location = new System.Drawing.Point(2, 328);
            this.Go.Name = "Go";
            this.Go.Size = new System.Drawing.Size(335, 48);
            this.Go.TabIndex = 3;
            this.Go.Text = "Scan and Parse";
            this.Go.UseVisualStyleBackColor = true;
            this.Go.Click += new System.EventHandler(this.Go_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 381);
            this.Controls.Add(this.Go);
            this.Controls.Add(this.DataGridView);
            this.Controls.Add(this.codeLinesTextBox);
            this.Name = "Form1";
            this.Text = "Scanner";
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox codeLinesTextBox;
        private System.Windows.Forms.DataGridView DataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn TokenType;
        private System.Windows.Forms.DataGridViewTextBoxColumn TokenValue;
        private System.Windows.Forms.Button Go;
    }
}