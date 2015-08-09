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
            this.label1 = new System.Windows.Forms.Label();
            this.txtTemplatePath = new System.Windows.Forms.TextBox();
            this.btnOpenTemplate = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.ChListboxTable = new System.Windows.Forms.CheckedListBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.checkBoxAllTable);
            this.panel1.Controls.Add(this.lbAbsObj);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtTemplatePath);
            this.panel1.Controls.Add(this.btnOpenTemplate);
            this.panel1.Controls.Add(this.btnNext);
            this.panel1.Controls.Add(this.ChListboxTable);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(566, 438);
            this.panel1.TabIndex = 0;
            // 
            // checkBoxAllTable
            // 
            this.checkBoxAllTable.AutoSize = true;
            this.checkBoxAllTable.Location = new System.Drawing.Point(15, 218);
            this.checkBoxAllTable.Name = "checkBoxAllTable";
            this.checkBoxAllTable.Size = new System.Drawing.Size(152, 24);
            this.checkBoxAllTable.TabIndex = 10;
            this.checkBoxAllTable.Text = "Check All Tables";
            this.checkBoxAllTable.UseVisualStyleBackColor = true;
            // 
            // lbAbsObj
            // 
            this.lbAbsObj.AutoSize = true;
            this.lbAbsObj.Location = new System.Drawing.Point(584, 15);
            this.lbAbsObj.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbAbsObj.Name = "lbAbsObj";
            this.lbAbsObj.Size = new System.Drawing.Size(0, 20);
            this.lbAbsObj.TabIndex = 9;
            this.lbAbsObj.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(129, 337);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(253, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Tên file có thể để ở dạng Template";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 268);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Template File :";
            // 
            // txtTemplatePath
            // 
            this.txtTemplatePath.Location = new System.Drawing.Point(132, 263);
            this.txtTemplatePath.Name = "txtTemplatePath";
            this.txtTemplatePath.Size = new System.Drawing.Size(355, 26);
            this.txtTemplatePath.TabIndex = 3;
            // 
            // btnOpenTemplate
            // 
            this.btnOpenTemplate.Location = new System.Drawing.Point(494, 263);
            this.btnOpenTemplate.Name = "btnOpenTemplate";
            this.btnOpenTemplate.Size = new System.Drawing.Size(57, 26);
            this.btnOpenTemplate.TabIndex = 2;
            this.btnOpenTemplate.Text = "...";
            this.btnOpenTemplate.UseVisualStyleBackColor = true;
            this.btnOpenTemplate.Click += new System.EventHandler(this.btnOpenTemplate_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(249, 392);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(123, 29);
            this.btnNext.TabIndex = 1;
            this.btnNext.Text = "Gen Code";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // ChListboxTable
            // 
            this.ChListboxTable.FormattingEnabled = true;
            this.ChListboxTable.Location = new System.Drawing.Point(16, 17);
            this.ChListboxTable.Name = "ChListboxTable";
            this.ChListboxTable.Size = new System.Drawing.Size(536, 172);
            this.ChListboxTable.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // FrmListTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 438);
            this.Controls.Add(this.panel1);
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbAbsObj;
        private System.Windows.Forms.CheckBox checkBoxAllTable;
    }
}