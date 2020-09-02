/* logicAstroKPCharts
   Astrology Software
   Author: Nachiappan Narayanan
   *********************************************************/
/* 
   License conditions
   ------------------
   This file is part of logicAstroKPCharts.

   logicAstroKPCharts is distributed with NO WARRANTY OF ANY KIND. No author
   or distributor accepts any responsibility for the consequences of using it,
   or for whether it serves any particular purpose or works at all, unless he
   or she says so in writing.
  
   This program is free software: you can redistribute it and/or modify
   it under the terms of the GNU General Public License as published by
   the Free Software Foundation, either version 2 of the License, or
   (at your option) any later version.

   This program is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
   GNU General Public License for more details.

   You should have received a copy of the GNU General Public License
   along with this program.  If not, see <http://www.gnu.org/licenses/>.
***********************************************************/

namespace logicAstroKPCharts
{
    partial class WelcomeForm
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
            this.rtTermsAndConditions = new System.Windows.Forms.RichTextBox();
            this.lblSoftwareTitle = new System.Windows.Forms.Label();
            this.btnEnter = new System.Windows.Forms.Button();
            this.lblTermsAndConditionsAgree = new System.Windows.Forms.Label();
            this.lblTermsAndConditions = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // rtTermsAndConditions
            // 
            this.rtTermsAndConditions.Location = new System.Drawing.Point(28, 281);
            this.rtTermsAndConditions.Name = "rtTermsAndConditions";
            this.rtTermsAndConditions.Size = new System.Drawing.Size(706, 137);
            this.rtTermsAndConditions.TabIndex = 0;
            this.rtTermsAndConditions.Text = "";
            // 
            // lblSoftwareTitle
            // 
            this.lblSoftwareTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoftwareTitle.ForeColor = System.Drawing.Color.Blue;
            this.lblSoftwareTitle.Location = new System.Drawing.Point(19, 67);
            this.lblSoftwareTitle.Name = "lblSoftwareTitle";
            this.lblSoftwareTitle.Size = new System.Drawing.Size(722, 73);
            this.lblSoftwareTitle.TabIndex = 1;
            this.lblSoftwareTitle.Text = "logicAstroCharts for KP";
            this.lblSoftwareTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnEnter
            // 
            this.btnEnter.Location = new System.Drawing.Point(290, 215);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(182, 43);
            this.btnEnter.TabIndex = 3;
            this.btnEnter.Text = "Enter";
            this.btnEnter.UseVisualStyleBackColor = true;
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // lblTermsAndConditionsAgree
            // 
            this.lblTermsAndConditionsAgree.AutoSize = true;
            this.lblTermsAndConditionsAgree.Location = new System.Drawing.Point(82, 193);
            this.lblTermsAndConditionsAgree.Name = "lblTermsAndConditionsAgree";
            this.lblTermsAndConditionsAgree.Size = new System.Drawing.Size(596, 13);
            this.lblTermsAndConditionsAgree.TabIndex = 4;
            this.lblTermsAndConditionsAgree.Text = "By clicking \"Enter\", you agree to our \"Terms and Conditions\" and that you have fu" +
    "lly read the same at the bottom of this form.";
            // 
            // lblTermsAndConditions
            // 
            this.lblTermsAndConditions.AutoSize = true;
            this.lblTermsAndConditions.Location = new System.Drawing.Point(23, 262);
            this.lblTermsAndConditions.Name = "lblTermsAndConditions";
            this.lblTermsAndConditions.Size = new System.Drawing.Size(109, 13);
            this.lblTermsAndConditions.TabIndex = 5;
            this.lblTermsAndConditions.Text = "Terms and Conditions";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.Location = new System.Drawing.Point(700, 9);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(49, 20);
            this.lblVersion.TabIndex = 6;
            this.lblVersion.Text = "1.0.0";
            // 
            // WelcomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 450);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblTermsAndConditions);
            this.Controls.Add(this.lblTermsAndConditionsAgree);
            this.Controls.Add(this.btnEnter);
            this.Controls.Add(this.lblSoftwareTitle);
            this.Controls.Add(this.rtTermsAndConditions);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WelcomeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Welcome";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtTermsAndConditions;
        private System.Windows.Forms.Label lblSoftwareTitle;
        private System.Windows.Forms.Button btnEnter;
        private System.Windows.Forms.Label lblTermsAndConditionsAgree;
        private System.Windows.Forms.Label lblTermsAndConditions;
        private System.Windows.Forms.Label lblVersion;
    }
}