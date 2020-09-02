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
            this.lblName = new System.Windows.Forms.Label();
            this.lblGender = new System.Windows.Forms.Label();
            this.lblDOB = new System.Windows.Forms.Label();
            this.lblTOB = new System.Windows.Forms.Label();
            this.lblTimeZone = new System.Windows.Forms.Label();
            this.lblPOB = new System.Windows.Forms.Label();
            this.lblLongitude = new System.Windows.Forms.Label();
            this.lblLatitude = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.cmbGender = new System.Windows.Forms.ComboBox();
            this.txtDD = new System.Windows.Forms.TextBox();
            this.txtMM = new System.Windows.Forms.TextBox();
            this.txtYYYY = new System.Windows.Forms.TextBox();
            this.lblDateFormat = new System.Windows.Forms.Label();
            this.lblSepChar1 = new System.Windows.Forms.Label();
            this.lblSepChar2 = new System.Windows.Forms.Label();
            this.txtHour = new System.Windows.Forms.TextBox();
            this.txtMin = new System.Windows.Forms.TextBox();
            this.txtSec = new System.Windows.Forms.TextBox();
            this.lblSepChar3 = new System.Windows.Forms.Label();
            this.lblSepChar4 = new System.Windows.Forms.Label();
            this.lblTimeFormat = new System.Windows.Forms.Label();
            this.cmbTimeZone = new System.Windows.Forms.ComboBox();
            this.txtPOB = new System.Windows.Forms.TextBox();
            this.cmbLonDirection = new System.Windows.Forms.ComboBox();
            this.txtLonDegrees = new System.Windows.Forms.TextBox();
            this.txtLonMinutes = new System.Windows.Forms.TextBox();
            this.lblSepChar5 = new System.Windows.Forms.Label();
            this.lblLonFormat = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLatMinutes = new System.Windows.Forms.TextBox();
            this.txtLatDegrees = new System.Windows.Forms.TextBox();
            this.cmbLatDirection = new System.Windows.Forms.ComboBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnNewChart = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblLonLatWarning = new System.Windows.Forms.Label();
            this.btnNow = new System.Windows.Forms.Button();
            this.btnWhyLonLat = new System.Windows.Forms.Button();
            this.lblAyanamsa = new System.Windows.Forms.Label();
            this.cmbAyanamsa = new System.Windows.Forms.ComboBox();
            this.btnTimeChart = new System.Windows.Forms.Button();
            this.lblState = new System.Windows.Forms.Label();
            this.txtSOB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCOB = new System.Windows.Forms.TextBox();
            this.btnAboutUs = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(75, 67);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(38, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name:";
            // 
            // lblGender
            // 
            this.lblGender.AutoSize = true;
            this.lblGender.Location = new System.Drawing.Point(401, 67);
            this.lblGender.Name = "lblGender";
            this.lblGender.Size = new System.Drawing.Size(45, 13);
            this.lblGender.TabIndex = 1;
            this.lblGender.Text = "Gender:";
            // 
            // lblDOB
            // 
            this.lblDOB.AutoSize = true;
            this.lblDOB.Location = new System.Drawing.Point(42, 104);
            this.lblDOB.Name = "lblDOB";
            this.lblDOB.Size = new System.Drawing.Size(71, 13);
            this.lblDOB.TabIndex = 2;
            this.lblDOB.Text = "Date Of Birth:";
            // 
            // lblTOB
            // 
            this.lblTOB.AutoSize = true;
            this.lblTOB.Location = new System.Drawing.Point(42, 141);
            this.lblTOB.Name = "lblTOB";
            this.lblTOB.Size = new System.Drawing.Size(71, 13);
            this.lblTOB.TabIndex = 3;
            this.lblTOB.Text = "Time Of Birth:";
            // 
            // lblTimeZone
            // 
            this.lblTimeZone.AutoSize = true;
            this.lblTimeZone.Location = new System.Drawing.Point(52, 178);
            this.lblTimeZone.Name = "lblTimeZone";
            this.lblTimeZone.Size = new System.Drawing.Size(61, 13);
            this.lblTimeZone.TabIndex = 4;
            this.lblTimeZone.Text = "Time Zone:";
            // 
            // lblPOB
            // 
            this.lblPOB.AutoSize = true;
            this.lblPOB.Location = new System.Drawing.Point(38, 216);
            this.lblPOB.Name = "lblPOB";
            this.lblPOB.Size = new System.Drawing.Size(75, 13);
            this.lblPOB.TabIndex = 5;
            this.lblPOB.Text = "Place Of Birth:";
            // 
            // lblLongitude
            // 
            this.lblLongitude.AutoSize = true;
            this.lblLongitude.Location = new System.Drawing.Point(56, 325);
            this.lblLongitude.Name = "lblLongitude";
            this.lblLongitude.Size = new System.Drawing.Size(57, 13);
            this.lblLongitude.TabIndex = 6;
            this.lblLongitude.Text = "Longitude:";
            // 
            // lblLatitude
            // 
            this.lblLatitude.AutoSize = true;
            this.lblLatitude.Location = new System.Drawing.Point(61, 363);
            this.lblLatitude.Name = "lblLatitude";
            this.lblLatitude.Size = new System.Drawing.Size(52, 13);
            this.lblLatitude.TabIndex = 7;
            this.lblLatitude.Text = "Latutude:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(129, 67);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(260, 20);
            this.txtName.TabIndex = 2;
            this.txtName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NameValue_KeyPressed);
            // 
            // cmbGender
            // 
            this.cmbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGender.FormattingEnabled = true;
            this.cmbGender.Items.AddRange(new object[] {
            "Male",
            "Female",
            "Transgender",
            "Unknown"});
            this.cmbGender.Location = new System.Drawing.Point(462, 67);
            this.cmbGender.Name = "cmbGender";
            this.cmbGender.Size = new System.Drawing.Size(80, 21);
            this.cmbGender.TabIndex = 3;
            // 
            // txtDD
            // 
            this.txtDD.Location = new System.Drawing.Point(129, 104);
            this.txtDD.MaxLength = 2;
            this.txtDD.Name = "txtDD";
            this.txtDD.Size = new System.Drawing.Size(33, 20);
            this.txtDD.TabIndex = 4;
            this.txtDD.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DayValue_KeyPressed);
            // 
            // txtMM
            // 
            this.txtMM.Location = new System.Drawing.Point(179, 104);
            this.txtMM.MaxLength = 2;
            this.txtMM.Name = "txtMM";
            this.txtMM.Size = new System.Drawing.Size(33, 20);
            this.txtMM.TabIndex = 5;
            this.txtMM.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MonthValue_KeyPressed);
            // 
            // txtYYYY
            // 
            this.txtYYYY.Location = new System.Drawing.Point(230, 104);
            this.txtYYYY.MaxLength = 4;
            this.txtYYYY.Name = "txtYYYY";
            this.txtYYYY.Size = new System.Drawing.Size(69, 20);
            this.txtYYYY.TabIndex = 6;
            this.txtYYYY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.YearValue_KeyPressed);
            // 
            // lblDateFormat
            // 
            this.lblDateFormat.AutoSize = true;
            this.lblDateFormat.Location = new System.Drawing.Point(304, 104);
            this.lblDateFormat.Name = "lblDateFormat";
            this.lblDateFormat.Size = new System.Drawing.Size(97, 13);
            this.lblDateFormat.TabIndex = 13;
            this.lblDateFormat.Text = "(DD / MM / YYYY)";
            // 
            // lblSepChar1
            // 
            this.lblSepChar1.AutoSize = true;
            this.lblSepChar1.Location = new System.Drawing.Point(164, 107);
            this.lblSepChar1.Name = "lblSepChar1";
            this.lblSepChar1.Size = new System.Drawing.Size(12, 13);
            this.lblSepChar1.TabIndex = 14;
            this.lblSepChar1.Text = "/";
            // 
            // lblSepChar2
            // 
            this.lblSepChar2.AutoSize = true;
            this.lblSepChar2.Location = new System.Drawing.Point(215, 107);
            this.lblSepChar2.Name = "lblSepChar2";
            this.lblSepChar2.Size = new System.Drawing.Size(12, 13);
            this.lblSepChar2.TabIndex = 15;
            this.lblSepChar2.Text = "/";
            // 
            // txtHour
            // 
            this.txtHour.Location = new System.Drawing.Point(129, 141);
            this.txtHour.MaxLength = 2;
            this.txtHour.Name = "txtHour";
            this.txtHour.Size = new System.Drawing.Size(33, 20);
            this.txtHour.TabIndex = 7;
            this.txtHour.Text = "00";
            this.txtHour.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.HoursValue_KeyPressed);
            // 
            // txtMin
            // 
            this.txtMin.Location = new System.Drawing.Point(179, 141);
            this.txtMin.MaxLength = 2;
            this.txtMin.Name = "txtMin";
            this.txtMin.Size = new System.Drawing.Size(33, 20);
            this.txtMin.TabIndex = 8;
            this.txtMin.Text = "00";
            this.txtMin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MinutesValue_KeyPressed);
            // 
            // txtSec
            // 
            this.txtSec.Location = new System.Drawing.Point(230, 141);
            this.txtSec.MaxLength = 2;
            this.txtSec.Name = "txtSec";
            this.txtSec.Size = new System.Drawing.Size(33, 20);
            this.txtSec.TabIndex = 9;
            this.txtSec.Text = "00";
            this.txtSec.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SecondsValue_KeyPressed);
            // 
            // lblSepChar3
            // 
            this.lblSepChar3.AutoSize = true;
            this.lblSepChar3.Location = new System.Drawing.Point(164, 143);
            this.lblSepChar3.Name = "lblSepChar3";
            this.lblSepChar3.Size = new System.Drawing.Size(13, 13);
            this.lblSepChar3.TabIndex = 19;
            this.lblSepChar3.Text = "--";
            // 
            // lblSepChar4
            // 
            this.lblSepChar4.AutoSize = true;
            this.lblSepChar4.Location = new System.Drawing.Point(215, 144);
            this.lblSepChar4.Name = "lblSepChar4";
            this.lblSepChar4.Size = new System.Drawing.Size(13, 13);
            this.lblSepChar4.TabIndex = 20;
            this.lblSepChar4.Text = "--";
            // 
            // lblTimeFormat
            // 
            this.lblTimeFormat.AutoSize = true;
            this.lblTimeFormat.Location = new System.Drawing.Point(271, 141);
            this.lblTimeFormat.Name = "lblTimeFormat";
            this.lblTimeFormat.Size = new System.Drawing.Size(149, 13);
            this.lblTimeFormat.TabIndex = 21;
            this.lblTimeFormat.Text = "(HH - MM - SS / 24 hr Format)";
            // 
            // cmbTimeZone
            // 
            this.cmbTimeZone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTimeZone.FormattingEnabled = true;
            this.cmbTimeZone.Location = new System.Drawing.Point(129, 178);
            this.cmbTimeZone.Name = "cmbTimeZone";
            this.cmbTimeZone.Size = new System.Drawing.Size(413, 21);
            this.cmbTimeZone.TabIndex = 10;
            // 
            // txtPOB
            // 
            this.txtPOB.Location = new System.Drawing.Point(129, 216);
            this.txtPOB.Name = "txtPOB";
            this.txtPOB.Size = new System.Drawing.Size(240, 20);
            this.txtPOB.TabIndex = 11;
            this.txtPOB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.POBValue_KeyPressed);
            // 
            // cmbLonDirection
            // 
            this.cmbLonDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLonDirection.FormattingEnabled = true;
            this.cmbLonDirection.Items.AddRange(new object[] {
            "EAST",
            "WEST"});
            this.cmbLonDirection.Location = new System.Drawing.Point(129, 325);
            this.cmbLonDirection.Name = "cmbLonDirection";
            this.cmbLonDirection.Size = new System.Drawing.Size(98, 21);
            this.cmbLonDirection.TabIndex = 14;
            // 
            // txtLonDegrees
            // 
            this.txtLonDegrees.Location = new System.Drawing.Point(237, 325);
            this.txtLonDegrees.MaxLength = 3;
            this.txtLonDegrees.Name = "txtLonDegrees";
            this.txtLonDegrees.Size = new System.Drawing.Size(45, 20);
            this.txtLonDegrees.TabIndex = 15;
            this.txtLonDegrees.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LonDegreesValue_KeyPressed);
            // 
            // txtLonMinutes
            // 
            this.txtLonMinutes.Location = new System.Drawing.Point(301, 325);
            this.txtLonMinutes.MaxLength = 2;
            this.txtLonMinutes.Name = "txtLonMinutes";
            this.txtLonMinutes.Size = new System.Drawing.Size(49, 20);
            this.txtLonMinutes.TabIndex = 16;
            this.txtLonMinutes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LonMinutesValue_KeyPressed);
            // 
            // lblSepChar5
            // 
            this.lblSepChar5.AutoSize = true;
            this.lblSepChar5.Location = new System.Drawing.Point(286, 328);
            this.lblSepChar5.Name = "lblSepChar5";
            this.lblSepChar5.Size = new System.Drawing.Size(13, 13);
            this.lblSepChar5.TabIndex = 27;
            this.lblSepChar5.Text = "--";
            // 
            // lblLonFormat
            // 
            this.lblLonFormat.AutoSize = true;
            this.lblLonFormat.Location = new System.Drawing.Point(357, 325);
            this.lblLonFormat.Name = "lblLonFormat";
            this.lblLonFormat.Size = new System.Drawing.Size(95, 13);
            this.lblLonFormat.TabIndex = 28;
            this.lblLonFormat.Text = "(000 Deg - 00 Min)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(357, 363);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 33;
            this.label1.Text = "(00 Deg - 00 Min)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(286, 366);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "--";
            // 
            // txtLatMinutes
            // 
            this.txtLatMinutes.Location = new System.Drawing.Point(301, 363);
            this.txtLatMinutes.MaxLength = 2;
            this.txtLatMinutes.Name = "txtLatMinutes";
            this.txtLatMinutes.Size = new System.Drawing.Size(49, 20);
            this.txtLatMinutes.TabIndex = 19;
            this.txtLatMinutes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LatMinutesValue_KeyPressed);
            // 
            // txtLatDegrees
            // 
            this.txtLatDegrees.Location = new System.Drawing.Point(237, 363);
            this.txtLatDegrees.MaxLength = 2;
            this.txtLatDegrees.Name = "txtLatDegrees";
            this.txtLatDegrees.Size = new System.Drawing.Size(45, 20);
            this.txtLatDegrees.TabIndex = 18;
            this.txtLatDegrees.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LatDegreesValue_KeyPressed);
            // 
            // cmbLatDirection
            // 
            this.cmbLatDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLatDirection.FormattingEnabled = true;
            this.cmbLatDirection.Items.AddRange(new object[] {
            "NORTH",
            "SOUTH"});
            this.cmbLatDirection.Location = new System.Drawing.Point(129, 363);
            this.cmbLatDirection.Name = "cmbLatDirection";
            this.cmbLatDirection.Size = new System.Drawing.Size(98, 21);
            this.cmbLatDirection.TabIndex = 17;
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(38, 22);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(242, 449);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(98, 37);
            this.btnGenerate.TabIndex = 21;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnNewChart
            // 
            this.btnNewChart.Location = new System.Drawing.Point(222, 22);
            this.btnNewChart.Name = "btnNewChart";
            this.btnNewChart.Size = new System.Drawing.Size(75, 23);
            this.btnNewChart.TabIndex = 38;
            this.btnNewChart.Text = "New Chart";
            this.btnNewChart.UseVisualStyleBackColor = true;
            this.btnNewChart.Click += new System.EventHandler(this.btnNewChart_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(130, 22);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 39;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblLonLatWarning
            // 
            this.lblLonLatWarning.AutoSize = true;
            this.lblLonLatWarning.BackColor = System.Drawing.SystemColors.Control;
            this.lblLonLatWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLonLatWarning.ForeColor = System.Drawing.Color.Red;
            this.lblLonLatWarning.Location = new System.Drawing.Point(142, 290);
            this.lblLonLatWarning.Name = "lblLonLatWarning";
            this.lblLonLatWarning.Size = new System.Drawing.Size(285, 18);
            this.lblLonLatWarning.TabIndex = 44;
            this.lblLonLatWarning.Text = "Enter Longitude - Latitude Values Manually";
            // 
            // btnNow
            // 
            this.btnNow.Location = new System.Drawing.Point(467, 119);
            this.btnNow.Name = "btnNow";
            this.btnNow.Size = new System.Drawing.Size(75, 23);
            this.btnNow.TabIndex = 48;
            this.btnNow.Text = "Now";
            this.btnNow.UseVisualStyleBackColor = true;
            this.btnNow.Click += new System.EventHandler(this.btnNow_Click);
            // 
            // btnWhyLonLat
            // 
            this.btnWhyLonLat.Location = new System.Drawing.Point(433, 289);
            this.btnWhyLonLat.Margin = new System.Windows.Forms.Padding(2);
            this.btnWhyLonLat.Name = "btnWhyLonLat";
            this.btnWhyLonLat.Size = new System.Drawing.Size(56, 22);
            this.btnWhyLonLat.TabIndex = 49;
            this.btnWhyLonLat.Text = "Why?";
            this.btnWhyLonLat.UseVisualStyleBackColor = true;
            this.btnWhyLonLat.Click += new System.EventHandler(this.btnWhyLonLat_Click);
            // 
            // lblAyanamsa
            // 
            this.lblAyanamsa.AutoSize = true;
            this.lblAyanamsa.Location = new System.Drawing.Point(53, 401);
            this.lblAyanamsa.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAyanamsa.Name = "lblAyanamsa";
            this.lblAyanamsa.Size = new System.Drawing.Size(59, 13);
            this.lblAyanamsa.TabIndex = 50;
            this.lblAyanamsa.Text = "Ayanamsa:";
            // 
            // cmbAyanamsa
            // 
            this.cmbAyanamsa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAyanamsa.FormattingEnabled = true;
            this.cmbAyanamsa.Items.AddRange(new object[] {
            "KP-SE-NewComb",
            "Lahiri",
            "Suryasiddhanta",
            "True Pushya (PVRN Rao)",
            "True Mula (Chandra Hari)",
            "Fagan/Bradley",
            "Tropical"});
            this.cmbAyanamsa.Location = new System.Drawing.Point(129, 401);
            this.cmbAyanamsa.Margin = new System.Windows.Forms.Padding(2);
            this.cmbAyanamsa.Name = "cmbAyanamsa";
            this.cmbAyanamsa.Size = new System.Drawing.Size(221, 21);
            this.cmbAyanamsa.TabIndex = 20;
            // 
            // btnTimeChart
            // 
            this.btnTimeChart.Location = new System.Drawing.Point(315, 22);
            this.btnTimeChart.Name = "btnTimeChart";
            this.btnTimeChart.Size = new System.Drawing.Size(75, 23);
            this.btnTimeChart.TabIndex = 53;
            this.btnTimeChart.Text = "Time Chart";
            this.btnTimeChart.UseVisualStyleBackColor = true;
            this.btnTimeChart.Click += new System.EventHandler(this.btnTimeChart_Click);
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Location = new System.Drawing.Point(391, 216);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(35, 13);
            this.lblState.TabIndex = 54;
            this.lblState.Text = "State:";
            // 
            // txtSOB
            // 
            this.txtSOB.Location = new System.Drawing.Point(432, 216);
            this.txtSOB.Name = "txtSOB";
            this.txtSOB.Size = new System.Drawing.Size(110, 20);
            this.txtSOB.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(63, 253);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 56;
            this.label3.Text = "Country:";
            // 
            // txtCOB
            // 
            this.txtCOB.Location = new System.Drawing.Point(129, 253);
            this.txtCOB.Name = "txtCOB";
            this.txtCOB.Size = new System.Drawing.Size(121, 20);
            this.txtCOB.TabIndex = 13;
            // 
            // btnAboutUs
            // 
            this.btnAboutUs.ForeColor = System.Drawing.Color.MediumBlue;
            this.btnAboutUs.Location = new System.Drawing.Point(467, 22);
            this.btnAboutUs.Name = "btnAboutUs";
            this.btnAboutUs.Size = new System.Drawing.Size(75, 23);
            this.btnAboutUs.TabIndex = 57;
            this.btnAboutUs.Text = "AboutUs";
            this.btnAboutUs.UseVisualStyleBackColor = true;
            this.btnAboutUs.Click += new System.EventHandler(this.btnAboutUs_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 507);
            this.Controls.Add(this.btnAboutUs);
            this.Controls.Add(this.txtCOB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtSOB);
            this.Controls.Add(this.lblState);
            this.Controls.Add(this.btnTimeChart);
            this.Controls.Add(this.cmbAyanamsa);
            this.Controls.Add(this.lblAyanamsa);
            this.Controls.Add(this.btnWhyLonLat);
            this.Controls.Add(this.btnNow);
            this.Controls.Add(this.lblLonLatWarning);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnNewChart);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtLatMinutes);
            this.Controls.Add(this.txtLatDegrees);
            this.Controls.Add(this.cmbLatDirection);
            this.Controls.Add(this.lblLonFormat);
            this.Controls.Add(this.lblSepChar5);
            this.Controls.Add(this.txtLonMinutes);
            this.Controls.Add(this.txtLonDegrees);
            this.Controls.Add(this.cmbLonDirection);
            this.Controls.Add(this.txtPOB);
            this.Controls.Add(this.cmbTimeZone);
            this.Controls.Add(this.lblTimeFormat);
            this.Controls.Add(this.lblSepChar4);
            this.Controls.Add(this.lblSepChar3);
            this.Controls.Add(this.txtSec);
            this.Controls.Add(this.txtMin);
            this.Controls.Add(this.txtHour);
            this.Controls.Add(this.lblSepChar2);
            this.Controls.Add(this.lblSepChar1);
            this.Controls.Add(this.lblDateFormat);
            this.Controls.Add(this.txtYYYY);
            this.Controls.Add(this.txtMM);
            this.Controls.Add(this.txtDD);
            this.Controls.Add(this.cmbGender);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblLatitude);
            this.Controls.Add(this.lblLongitude);
            this.Controls.Add(this.lblPOB);
            this.Controls.Add(this.lblTimeZone);
            this.Controls.Add(this.lblTOB);
            this.Controls.Add(this.lblDOB);
            this.Controls.Add(this.lblGender);
            this.Controls.Add(this.lblName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "logicAstroCharts for KP";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.Label lblDOB;
        private System.Windows.Forms.Label lblTOB;
        private System.Windows.Forms.Label lblTimeZone;
        private System.Windows.Forms.Label lblPOB;
        private System.Windows.Forms.Label lblLongitude;
        private System.Windows.Forms.Label lblLatitude;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.ComboBox cmbGender;
        private System.Windows.Forms.TextBox txtDD;
        private System.Windows.Forms.TextBox txtMM;
        private System.Windows.Forms.TextBox txtYYYY;
        private System.Windows.Forms.Label lblDateFormat;
        private System.Windows.Forms.Label lblSepChar1;
        private System.Windows.Forms.Label lblSepChar2;
        private System.Windows.Forms.TextBox txtHour;
        private System.Windows.Forms.TextBox txtMin;
        private System.Windows.Forms.TextBox txtSec;
        private System.Windows.Forms.Label lblSepChar3;
        private System.Windows.Forms.Label lblSepChar4;
        private System.Windows.Forms.Label lblTimeFormat;
        private System.Windows.Forms.ComboBox cmbTimeZone;
        private System.Windows.Forms.TextBox txtPOB;
        private System.Windows.Forms.ComboBox cmbLonDirection;
        private System.Windows.Forms.TextBox txtLonDegrees;
        private System.Windows.Forms.TextBox txtLonMinutes;
        private System.Windows.Forms.Label lblSepChar5;
        private System.Windows.Forms.Label lblLonFormat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLatMinutes;
        private System.Windows.Forms.TextBox txtLatDegrees;
        private System.Windows.Forms.ComboBox cmbLatDirection;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnNewChart;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblLonLatWarning;
        private System.Windows.Forms.Button btnNow;
        private System.Windows.Forms.Button btnWhyLonLat;
        private System.Windows.Forms.Label lblAyanamsa;
        private System.Windows.Forms.ComboBox cmbAyanamsa;
        private System.Windows.Forms.Button btnTimeChart;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.TextBox txtSOB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCOB;
        private System.Windows.Forms.Button btnAboutUs;
    }
}

