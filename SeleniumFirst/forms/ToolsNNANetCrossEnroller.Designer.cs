
namespace SeleniumFirst.forms
{
    partial class ToolsNNANetCrossEnroller
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
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Vacaville Nissan");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("North Bay Nissan");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Yuba City Nissan");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Sacramento Nissan");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Marin Infiniti");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Vallejo Nissan");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Golden State Nissan");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Golden State Infiniti");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("NNANet (Nissan/Infiniti)", new System.Windows.Forms.TreeNode[] {
            treeNode10,
            treeNode11,
            treeNode12,
            treeNode13,
            treeNode14,
            treeNode15,
            treeNode16,
            treeNode17});
            this.tv_nna = new System.Windows.Forms.TreeView();
            this.button1 = new System.Windows.Forms.Button();
            this.tb_tools_username = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cb_tools_role = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // tv_nna
            // 
            this.tv_nna.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tv_nna.CheckBoxes = true;
            this.tv_nna.Location = new System.Drawing.Point(12, 65);
            this.tv_nna.Name = "tv_nna";
            treeNode10.Name = "Node16";
            treeNode10.Text = "Vacaville Nissan";
            treeNode11.Name = "Node17";
            treeNode11.Text = "North Bay Nissan";
            treeNode12.Name = "Node24";
            treeNode12.Text = "Yuba City Nissan";
            treeNode13.Name = "Node23";
            treeNode13.Text = "Sacramento Nissan";
            treeNode14.Name = "Node21";
            treeNode14.Text = "Marin Infiniti";
            treeNode15.Name = "Node20";
            treeNode15.Text = "Vallejo Nissan";
            treeNode16.Name = "Node18";
            treeNode16.Text = "Golden State Nissan";
            treeNode17.Name = "Node19";
            treeNode17.Text = "Golden State Infiniti";
            treeNode18.Name = "Node1";
            treeNode18.Text = "NNANet (Nissan/Infiniti)";
            this.tv_nna.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode18});
            this.tv_nna.Size = new System.Drawing.Size(220, 101);
            this.tv_nna.TabIndex = 15;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 172);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(220, 34);
            this.button1.TabIndex = 16;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tb_tools_username
            // 
            this.tb_tools_username.Location = new System.Drawing.Point(107, 12);
            this.tb_tools_username.Name = "tb_tools_username";
            this.tb_tools_username.Size = new System.Drawing.Size(125, 20);
            this.tb_tools_username.TabIndex = 17;
            this.tb_tools_username.Text = "XD123456";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "NNANet User ID:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "Role:";
            // 
            // cb_tools_role
            // 
            this.cb_tools_role.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cb_tools_role.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cb_tools_role.FormattingEnabled = true;
            this.cb_tools_role.Location = new System.Drawing.Point(55, 38);
            this.cb_tools_role.MaxLength = 50;
            this.cb_tools_role.Name = "cb_tools_role";
            this.cb_tools_role.Size = new System.Drawing.Size(177, 21);
            this.cb_tools_role.TabIndex = 25;
            // 
            // ToolsNNANetCrossEnroller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 215);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cb_tools_role);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_tools_username);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tv_nna);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ToolsNNANetCrossEnroller";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ToolsNNANetCrossEnroller";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tv_nna;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tb_tools_username;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cb_tools_role;
    }
}