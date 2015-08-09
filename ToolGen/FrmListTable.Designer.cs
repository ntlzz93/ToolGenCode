namespace LotusGenCode
{
    partial class FrmListTable
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBoxAllTable = new System.Windows.Forms.CheckBox();
            this.lbAbsObj = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTemplatePath = new System.Windows.Forms.TextBox();
            this.btnOpenTemplate = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.ChListboxTable = new System.Windows.Forms.CheckedListBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.checkBoxAllTable);
            this.panel1.Controls.Add(this.lbAbsObj);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtFileName);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtTemplatePath);
            this.panel1.Controls.Add(this.btnOpenTemplate);
            this.panel1.Controls.Add(this.btnNext);
            this.panel1.Controls.Add(this.ChListboxTable);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(613, 285);
            this.panel1.TabIndex = 0;
            // 
            // checkBoxAllTable
            // 
            this.checkBoxAllTable.AutoSize = true;
            this.checkBoxAllTable.Location = new System.Drawing.Point(10, 142);
            this.checkBoxAllTable.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxAllTable.Name = "checkBoxAllTable";
            this.checkBoxAllTable.Size = new System.Drawing.Size(106, 17);
            this.checkBoxAllTable.TabIndex = 10;
            this.checkBoxAllTable.Text = "Check All Tables";
            this.checkBoxAllTable.UseVisualStyleBackColor = true;
            this.checkBoxAllTable.CheckedChanged += new System.EventHandler(this.checkBoxAllTable_CheckedChanged);
            // 
            // lbAbsObj
            // 
            this.lbAbsObj.AutoSize = true;
            this.lbAbsObj.Location = new System.Drawing.Point(389, 10);
            this.lbAbsObj.Name = "lbAbsObj";
            this.lbAbsObj.Size = new System.Drawing.Size(0, 13);
            this.lbAbsObj.TabIndex = 9;
            this.lbAbsObj.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(86, 219);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(174, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Tên file có thể để ở dạng Template";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 197);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Output File :";
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(88, 195);
            this.txtFileName.Margin = new System.Windows.Forms.Padding(2);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(279, 20);
            this.txtFileName.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 174);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Template File :";
            // 
            // txtTemplatePath
            // 
            this.txtTemplatePath.Location = new System.Drawing.Point(88, 171);
            this.txtTemplatePath.Margin = new System.Windows.Forms.Padding(2);
            this.txtTemplatePath.Name = "txtTemplatePath";
            this.txtTemplatePath.Size = new System.Drawing.Size(238, 20);
            this.txtTemplatePath.TabIndex = 3;
            // 
            // btnOpenTemplate
            // 
            this.btnOpenTemplate.Location = new System.Drawing.Point(329, 171);
            this.btnOpenTemplate.Margin = new System.Windows.Forms.Padding(2);
            this.btnOpenTemplate.Name = "btnOpenTemplate";
            this.btnOpenTemplate.Size = new System.Drawing.Size(38, 20);
            this.btnOpenTemplate.TabIndex = 2;
            this.btnOpenTemplate.Text = "...";
            this.btnOpenTemplate.UseVisualStyleBackColor = true;
            this.btnOpenTemplate.Click += new System.EventHandler(this.btnOpenTemplate_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(166, 255);
            this.btnNext.Margin = new System.Windows.Forms.Padding(2);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(82, 19);
            this.btnNext.TabIndex = 1;
            this.btnNext.Text = "Gen Code";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // ChListboxTable
            // 
            this.ChListboxTable.FormattingEnabled = true;
            this.ChListboxTable.Location = new System.Drawing.Point(9, 10);
            this.ChListboxTable.Margin = new System.Windows.Forms.Padding(2);
            this.ChListboxTable.Name = "ChListboxTable";
            this.ChListboxTable.Size = new System.Drawing.Size(359, 124);
            this.ChListboxTable.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(296, 246);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // FrmListTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 285);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FrmListTable";
            this.Text = "List table";
            this.Load += new System.EventHandler(this.FrmListTable_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.CheckedListBox ChListboxTable;
        private System.Windows.Forms.TextBox txtTemplatePath;
        private System.Windows.Forms.Button btnOpenTemplate;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbAbsObj;
        private System.Windows.Forms.CheckBox checkBoxAllTable;
        private System.Windows.Forms.Button button1;
    }
}