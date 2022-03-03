
namespace SeleniumFirst.forms
{
    partial class MessageBox_dropdown
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
            this.lbl_message = new System.Windows.Forms.Label();
            this.cb_dropdown = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_message
            // 
            this.lbl_message.Location = new System.Drawing.Point(12, 22);
            this.lbl_message.Name = "lbl_message";
            this.lbl_message.Size = new System.Drawing.Size(275, 42);
            this.lbl_message.TabIndex = 0;
            this.lbl_message.Text = "(default)";
            this.lbl_message.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cb_dropdown
            // 
            this.cb_dropdown.FormattingEnabled = true;
            this.cb_dropdown.Location = new System.Drawing.Point(15, 77);
            this.cb_dropdown.Name = "cb_dropdown";
            this.cb_dropdown.Size = new System.Drawing.Size(275, 21);
            this.cb_dropdown.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(215, 112);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MessageBox_dropdown
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(299, 147);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cb_dropdown);
            this.Controls.Add(this.lbl_message);
            this.Name = "MessageBox_dropdown";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "";
            this.Load += new System.EventHandler(this.MessageBox_dropdown_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_message;
        private System.Windows.Forms.ComboBox cb_dropdown;
        private System.Windows.Forms.Button button1;
    }
}