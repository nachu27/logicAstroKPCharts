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

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization.Formatters.Binary;
using srlWebCom.Astro.AstroObjects;
using iText.Kernel.Geom;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Font;
using iText.IO.Font.Constants;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout.Borders;
using iText.Layout.Renderer;

namespace logicAstroKPCharts
{
    public partial class MainForm : Form
    {
        private ArrayList m_listTimeZone = new ArrayList();
        private string m_strSWEParentFolder = "";
        private string m_strSWEConFilePath = "";
        private string m_strSWEFolderPath = "";
        private string m_strChartStorePath = "";
        private string m_strChartFilePath = "";
        private string m_strPDFStorePath = "";
        private string m_strObjectName = "";
        private string m_strGender = "";
        private int m_nDay = 0;
        private int m_nMonth = 0;
        private int m_nYear = 0;
        private int m_nHours = 0;
        private int m_nMinutes = 0;
        private int m_nSeconds = 0;
        private int m_nTimeZoneMinutes = 0;
        private string m_strTimeZone = "";
        private string m_strPOB = "";
        private string m_strSOB = "";
        private string m_strCOB = "";
        private string m_strLonDirection = "";
        private int m_nLonDegrees = 0;
        private int m_nLonMinutes = 0;
        private string m_strLatDirection = "";
        private int m_nLatDegrees = 0;
        private int m_nLatMinutes = 0;
        private string m_strAyanamsa = "KP-SE-NewComb";
        private int m_nAyanamsa = 999;

        public MainForm()
        {
            InitializeComponent();

            if (!InitApplication())
            {
                Application.Exit();
                this.Close();
            }
        }

        private bool LoadTimeZoneList()
        {
            m_listTimeZone.Clear();
            string strTimeZoneData = "";

            try
            {
                Assembly _assembly = Assembly.GetExecutingAssembly();
                string strResourceFile = string.Format("{0}.TimeZoneList.txt", astroGlobal.AstroSourcePath);
                StreamReader _textStreamReader = new StreamReader(_assembly.GetManifestResourceStream(strResourceFile));
                strTimeZoneData = _textStreamReader.ReadToEnd();
                _textStreamReader.Close();

                strTimeZoneData = strTimeZoneData.Replace("\r", "\n");
                strTimeZoneData = strTimeZoneData.Replace("\n\n", "\n");
                string[] TSElements = strTimeZoneData.Split('\n');
                foreach (string TSElement in TSElements)
                {
                    string[] TimeZoneElements = TSElement.Split('|');

                    if (TimeZoneElements.Length == 2)
                    {
                        astroTimeZone TS = new astroTimeZone();

                        TS.Minutes = Convert.ToInt32(TimeZoneElements[0]);
                        TS.Name = TimeZoneElements[1];
                        m_listTimeZone.Add(TS);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "LoadTimeZoneList() - Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }

        private bool SetTimeZoneMinutes()
        {
            try
            {
                m_nTimeZoneMinutes = 0;

                foreach (object TSItem in m_listTimeZone)
                {
                    astroTimeZone TSObj = (astroTimeZone)TSItem;

                    if (TSObj.Name.ToUpper() == m_strTimeZone.ToUpper())
                    {
                        m_nTimeZoneMinutes = TSObj.Minutes;
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GetTimeZoneMinutes() - Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }

        private bool InitApplication()
        {
            string strErrorMessage = "";

            try
            {
                astroGlobal.AstroSourcePath = "logicAstroKPCharts.AstroSource";

                if (!LoadTimeZoneList())
                {
                    return false;
                }

                if (astroStar.LoadStars(ref strErrorMessage) == false)
                {
                    MessageBox.Show("strErrorMessage", "InitApplicationControls() - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                m_strSWEParentFolder = string.Format(@"{0}\swiss-eph", Directory.GetCurrentDirectory());
                if (Directory.Exists(m_strSWEParentFolder) == false)
                {
                    MessageBox.Show("Unable to find Swiss Ephemeris application folder at " + m_strSWEParentFolder, "InitApplicationControls() - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                m_strSWEConFilePath = string.Format(@"{0}\swiss-eph\swecon.exe", Directory.GetCurrentDirectory());
                if (File.Exists(m_strSWEConFilePath) == false)
                {
                    MessageBox.Show("Unable to find Swiss Ephemeris application at " + m_strSWEConFilePath, "InitApplicationControls() - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                m_strSWEFolderPath = string.Format(@"{0}\swiss-eph\Eph", Directory.GetCurrentDirectory());
                if (Directory.Exists(m_strSWEFolderPath) == false)
                {
                    MessageBox.Show("Unable to find Swiss Ephemeris (Data) folder at " + m_strSWEFolderPath, "InitApplicationControls() - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                // Create a local directory for saving chart files if not existing
                m_strChartStorePath = string.Format(@"{0}\Store", Directory.GetCurrentDirectory());

                if (Directory.Exists(m_strChartStorePath) == false)
                {
                    Directory.CreateDirectory(m_strChartStorePath);
                }

                if (Directory.Exists(m_strChartStorePath) == false)
                {
                    MessageBox.Show("Unable to find / create store path at " + m_strChartStorePath, "InitApplicationControls() - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                // Create a local directory for saving pdf results if not existing
                m_strPDFStorePath = string.Format(@"{0}\PDF", Directory.GetCurrentDirectory());

                if (Directory.Exists(m_strPDFStorePath) == false)
                {
                    Directory.CreateDirectory(m_strPDFStorePath);
                }

                if (Directory.Exists(m_strPDFStorePath) == false)
                {
                    MessageBox.Show("Unable to find / create pdf path at " + m_strPDFStorePath, "InitApplicationControls() - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (ResetControls() == false)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "InitApplicationControls() - Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }

        private bool ResetControls()
        {
            try
            {
                txtName.Text = "";

                cmbGender.SelectedIndex = 0;

                txtDD.Text = "";
                txtMM.Text = "";
                txtYYYY.Text = "";

                txtHour.Text = "00";
                txtMin.Text = "00";
                txtSec.Text = "00";

                cmbTimeZone.Items.Clear();

                int i = 0;
                foreach (object TSItem in m_listTimeZone)
                {
                    astroTimeZone TSObj = (astroTimeZone)TSItem;

                    cmbTimeZone.Items.Insert(i, TSObj.Name);

                    i++;
                }

                if (i > 95)
                {
                    cmbTimeZone.SelectedIndex = 90;
                }

                txtPOB.Text = "";
                txtSOB.Text = "";
                txtCOB.Text = "";

                cmbLonDirection.SelectedIndex = 0;
                txtLonDegrees.Text = "000";
                txtLonMinutes.Text = "00";

                cmbLatDirection.SelectedIndex = 0;
                txtLatDegrees.Text = "00";
                txtLatMinutes.Text = "00";

                m_strChartFilePath = "";

                m_nTimeZoneMinutes = 0;

                cmbAyanamsa.SelectedIndex = 0;
                m_strAyanamsa = "KP-SE-NewComb";
                m_nAyanamsa = 999;

                DateTime dtNow = DateTime.Now;
                txtDD.Text = Convert.ToString(dtNow.Day);
                txtMM.Text = Convert.ToString(dtNow.Month);
                txtYYYY.Text = Convert.ToString(dtNow.Year);
                txtHour.Text = Convert.ToString(dtNow.Hour);
                txtMin.Text = Convert.ToString(dtNow.Minute);
                txtSec.Text = Convert.ToString(dtNow.Second);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "InitApplicationControls() - Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }

        private bool ValidateData()
        {
            int nValue = 0;

            try
            {
                m_strObjectName = txtName.Text.Trim();
                if (string.IsNullOrEmpty(m_strObjectName))
                {
                    MessageBox.Show("Name is empty.", "Invalid entry!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                m_strGender = cmbGender.GetItemText(cmbGender.SelectedItem);
                if (string.IsNullOrEmpty(m_strGender))
                {
                    MessageBox.Show("Gender is not selected.", "Invalid entry!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                nValue = -1;
                if ( string.IsNullOrEmpty(txtDD.Text) != true)
                {
                    nValue = Convert.ToInt32(txtDD.Text);
                }
                if (nValue < 1 || nValue > 31)
                {
                    MessageBox.Show("Day value range 1 - 31", "Invalid entry!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                m_nDay = nValue;

                nValue = -1;
                if (string.IsNullOrEmpty(txtMM.Text) != true)
                {
                    nValue = Convert.ToInt32(txtMM.Text);
                }
                if (nValue < 1 || nValue > 12)
                {
                    MessageBox.Show("Month value range 1 - 12", "Invalid entry!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                m_nMonth = nValue;

                nValue = -1;
                if (string.IsNullOrEmpty(txtYYYY.Text) != true)
                {
                    nValue = Convert.ToInt32(txtYYYY.Text);
                }
                if (nValue < 1 || nValue > 5300)
                {
                    MessageBox.Show("Year value range 1 - 5300", "Invalid entry!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                m_nYear = nValue;

                nValue = -1;
                if (string.IsNullOrEmpty(txtHour.Text) != true)
                {
                    nValue = Convert.ToInt32(txtHour.Text);
                }
                if (nValue < 0 || nValue > 23)
                {
                    MessageBox.Show("Hour value range 0 - 23", "Invalid entry!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                m_nHours = nValue;

                nValue = -1;
                if (string.IsNullOrEmpty(txtMin.Text) != true)
                {
                    nValue = Convert.ToInt32(txtMin.Text);
                }
                if (nValue < 0 || nValue > 59)
                {
                    MessageBox.Show("Minutes value range 0 - 59", "Invalid entry!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                m_nMinutes = nValue;

                nValue = -1;
                if (string.IsNullOrEmpty(txtSec.Text) != true)
                {
                    nValue = Convert.ToInt32(txtSec.Text);
                }
                if (nValue < 0 || nValue > 59)
                {
                    MessageBox.Show("Seconds value range 0 - 59", "Invalid entry!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                m_nSeconds = nValue;

                m_strTimeZone = cmbTimeZone.GetItemText(cmbTimeZone.SelectedItem);
                if (string.IsNullOrEmpty(m_strTimeZone))
                {
                    MessageBox.Show("TimeZone is not selected.", "Invalid entry!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                m_nTimeZoneMinutes = 0;
                if (!SetTimeZoneMinutes())
                {
                    MessageBox.Show("Unable to identify the TimeZone.", "Invalid entry!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                m_strPOB = txtPOB.Text.Trim();
                if (string.IsNullOrEmpty(m_strPOB))
                {
                    MessageBox.Show("Place of Birth is empty.", "Invalid entry!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                m_strSOB = txtSOB.Text.Trim();
                if (string.IsNullOrEmpty(m_strSOB))
                {
                    MessageBox.Show("State of Birth is empty.", "Invalid entry!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                m_strCOB = txtCOB.Text.Trim();
                if (string.IsNullOrEmpty(m_strCOB))
                {
                    MessageBox.Show("Country of Birth is empty.", "Invalid entry!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                m_strLonDirection = cmbLonDirection.GetItemText(cmbLonDirection.SelectedItem);
                if (string.IsNullOrEmpty(m_strLonDirection))
                {
                    MessageBox.Show("Longitude direction is not selected.", "Invalid entry!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                nValue = -1;
                if (string.IsNullOrEmpty(txtLonDegrees.Text) != true)
                {
                    nValue = Convert.ToInt32(txtLonDegrees.Text);
                }
                if (nValue < 0 || nValue > 180)
                {
                    MessageBox.Show("Longitude degree value range 0 - 180", "Invalid entry!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                m_nLonDegrees = nValue;

                nValue = -1;
                if (string.IsNullOrEmpty(txtLonMinutes.Text) != true)
                {
                    nValue = Convert.ToInt32(txtLonMinutes.Text);
                }
                if (nValue < 0 || nValue > 59)
                {
                    MessageBox.Show("Longitude minutes value range 0 - 59", "Invalid entry!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                m_nLonMinutes = nValue;

                m_strLatDirection = cmbLatDirection.GetItemText(cmbLatDirection.SelectedItem);
                if (string.IsNullOrEmpty(m_strLatDirection))
                {
                    MessageBox.Show("Latitude direction is not selected.", "Invalid entry!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                nValue = -1;
                if (string.IsNullOrEmpty(txtLatDegrees.Text) != true)
                {
                    nValue = Convert.ToInt32(txtLatDegrees.Text);
                }
                if (nValue < 0 || nValue > 90)
                {
                    MessageBox.Show("Latitude degree value range 0 - 90", "Invalid entry!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                m_nLatDegrees = nValue;

                nValue = -1;
                if (string.IsNullOrEmpty(txtLatMinutes.Text) != true)
                {
                    nValue = Convert.ToInt32(txtLatMinutes.Text);
                }
                if (nValue < 0 || nValue > 59)
                {
                    MessageBox.Show("Latitude minutes value range 0 - 59", "Invalid entry!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                m_nLatMinutes = nValue;

                m_strAyanamsa = cmbAyanamsa.GetItemText(cmbAyanamsa.SelectedItem).Trim();
                if (string.IsNullOrEmpty(m_strAyanamsa))
                {
                    MessageBox.Show("Ayanamsa is not selected.", "Invalid entry!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                m_nAyanamsa = 999;
                switch (m_strAyanamsa)
                {
                    default:
                    case "KP-SE-NewComb":
                        m_nAyanamsa = 45;
                        break;
                    case "KP (IAU-2006)":
                        m_nAyanamsa = 999;
                        break;
                    case "Lahiri":
                        m_nAyanamsa = 1;
                        break;
                    case "Suryasiddhanta":
                        m_nAyanamsa = 21;
                        break;
                    case "True Pushya (PVRN Rao)":
                        m_nAyanamsa = 29;
                        break;
                    case "True Mula (Chandra Hari)":
                        m_nAyanamsa = 35;
                        break;
                    case "Fagan/Bradley":
                        m_nAyanamsa = 0;
                        break;
                    case "Tropical":
                        m_nAyanamsa = -1;
                        break;
                }

                return true;
             }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ValidateData() - Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }

        private bool LoadChartFile()
        {
            int nValue = -1;

            try
            {
                openFileDialog.InitialDirectory = m_strChartStorePath;

                if ( openFileDialog.ShowDialog() != DialogResult.OK )
                {
                    return false;
                }

                m_strChartFilePath = openFileDialog.FileName;

                string strFileData = File.ReadAllText(m_strChartFilePath);

                string[] strChartItems = strFileData.Split('|');

                if (strChartItems.Length < 16)
                {
                    MessageBox.Show("File is not in compliance, might have been altered!", "LoadChartFile() - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                m_strObjectName = strChartItems[0];
                txtName.Text = m_strObjectName;

                m_strGender = strChartItems[1];
                nValue = cmbGender.FindStringExact(m_strGender);
                if (nValue != -1)
                {
                    cmbGender.SelectedIndex = nValue;
                }
                else
                {
                    cmbGender.SelectedIndex = 0;
                }
                
                m_nDay = Convert.ToInt32(strChartItems[2]);
                txtDD.Text = Convert.ToString(m_nDay);

                m_nMonth = Convert.ToInt32(strChartItems[3]);
                txtMM.Text = Convert.ToString(m_nMonth);

                m_nYear = Convert.ToInt32(strChartItems[4]);
                txtYYYY.Text = Convert.ToString(m_nYear);

                m_nHours = Convert.ToInt32(strChartItems[5]);
                txtHour.Text = Convert.ToString(m_nHours);

                m_nMinutes = Convert.ToInt32(strChartItems[6]);
                txtMin.Text = Convert.ToString(m_nMinutes);

                m_nSeconds = Convert.ToInt32(strChartItems[7]);
                txtSec.Text = Convert.ToString(m_nSeconds);

                m_strTimeZone = strChartItems[8];
                nValue = cmbTimeZone.FindStringExact(m_strTimeZone);
                if (nValue != -1)
                {
                    cmbTimeZone.SelectedIndex = nValue;
                }
                else
                {
                    if (cmbTimeZone.Items.Count > 95)
                    {
                        cmbTimeZone.SelectedIndex = 90;
                    }
                }
                SetTimeZoneMinutes();

                string strLocationOfBirth = strChartItems[9];
                string[] strLocationParts = strLocationOfBirth.Split('~');
                if (strLocationParts.Length > 0 ) m_strPOB = strLocationParts[0];
                if (strLocationParts.Length > 1) m_strSOB = strLocationParts[1];
                if (strLocationParts.Length > 2) m_strCOB = strLocationParts[2];
                txtPOB.Text = m_strPOB;
                txtSOB.Text = m_strSOB;
                txtCOB.Text = m_strCOB;

                m_strLonDirection = strChartItems[10];
                nValue = cmbLonDirection.FindStringExact(m_strLonDirection);
                if (nValue != -1)
                {
                    cmbLonDirection.SelectedIndex = nValue;
                }
                else
                {
                    cmbLonDirection.SelectedIndex = 0;
                }

                m_nLonDegrees = Convert.ToInt32(strChartItems[11]);
                txtLonDegrees.Text = Convert.ToString(m_nLonDegrees);

                m_nLonMinutes = Convert.ToInt32(strChartItems[12]);
                txtLonMinutes.Text = Convert.ToString(m_nLonMinutes);

                m_strLatDirection = strChartItems[13];
                nValue = cmbLatDirection.FindStringExact(m_strLatDirection);
                if (nValue != -1)
                {
                    cmbLatDirection.SelectedIndex = nValue;
                }
                else
                {
                    cmbLatDirection.SelectedIndex = 0;
                }

                m_nLatDegrees = Convert.ToInt32(strChartItems[14]);
                txtLatDegrees.Text = Convert.ToString(m_nLatDegrees);

                m_nLatMinutes = Convert.ToInt32(strChartItems[15]);
                txtLatMinutes.Text = Convert.ToString(m_nLatMinutes);

                if (strChartItems.Length > 16)
                {
                    m_strAyanamsa = Convert.ToString(strChartItems[16]);
                    nValue = cmbAyanamsa.FindStringExact(m_strAyanamsa);
                    if (nValue != -1)
                    {
                        cmbAyanamsa.SelectedIndex = nValue;
                    }
                    else
                    {
                        cmbAyanamsa.SelectedIndex = 0;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "LoadChartFile() - Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }

        private bool SaveChartFile()
        {
            try
            {
                if (!ValidateData())
                {
                    return false;
                }

                saveFileDialog.InitialDirectory = m_strChartStorePath;
                saveFileDialog.FileName = txtName.Text.Trim();

                if (saveFileDialog.ShowDialog() != DialogResult.OK)
                {
                    return false;
                }

                m_strChartFilePath = saveFileDialog.FileName;

                string strLocationOfBirth = string.Format("{0}~{1}~{2}", m_strPOB.Trim(), m_strSOB.Trim(), m_strCOB.Trim());

                string strChartFileData = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}|{13}|{14}|{15}|{16}", m_strObjectName, m_strGender, m_nDay, m_nMonth, m_nYear, m_nHours, m_nMinutes, m_nSeconds, m_strTimeZone, strLocationOfBirth, m_strLonDirection, m_nLonDegrees, m_nLonMinutes, m_strLatDirection, m_nLatDegrees, m_nLatMinutes, m_strAyanamsa);

                StreamWriter SW = new StreamWriter(m_strChartFilePath);
                SW.Write(strChartFileData);
                SW.Close();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SaveChartFile() - Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                if( LoadChartFile() == false )
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Open() - Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string strErrorMessage = "";
            try
            {
                if (ValidateData() == false)
                {
                    return;
                }

                string strTimeZoneSign = "";
                int nTimeZoneMinutes = m_nTimeZoneMinutes;
                if (m_nTimeZoneMinutes < 0)
                {
                    strTimeZoneSign = "+";
                    nTimeZoneMinutes = nTimeZoneMinutes * -1;
                }
                else
                {
                    strTimeZoneSign = "-";
                }

                TimeSpan timeZoneDiff = new TimeSpan(0, nTimeZoneMinutes, 0);

                string strLocationOfBirth = string.Format("{0}, {1}, {2}", m_strPOB.Trim(), m_strSOB.Trim(), m_strCOB.Trim());

                string strAstroData = string.Format("{0}|{1}|{2}-{3}-{4} {5}.{6}.{7}|{8}{9:00}.{10:00}|{11}|{12} {13:000}.{14:00}|{15} {16:00}.{17:00}|Standard Time|0\n", m_strObjectName, m_strGender, m_nYear, m_nMonth, m_nDay, m_nHours, m_nMinutes, m_nSeconds, strTimeZoneSign, timeZoneDiff.Hours, timeZoneDiff.Minutes, strLocationOfBirth, m_strLonDirection, m_nLonDegrees, m_nLonMinutes, m_strLatDirection, m_nLatDegrees, m_nLatMinutes);

                string strPDFFile = string.Format(@"{0}\{1}.pdf", m_strPDFStorePath, m_strObjectName);

                if (BuildAstroCharts(strAstroData, strPDFFile, ref strErrorMessage) == false)
                {
                    MessageBox.Show(strErrorMessage, "Generate() - Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    // open the pdf file
                    System.Diagnostics.Process.Start(strPDFFile);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Generate() - Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DayValue_KeyPressed(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == '\b')
            {
                e.Handled = false; //Do not reject the input
            }
            else
            {
                e.Handled = true; //Reject the input
            }
        }

        private void MonthValue_KeyPressed(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == '\b')
            {
                e.Handled = false; //Do not reject the input
            }
            else
            {
                e.Handled = true; //Reject the input
            }
        }

        private void YearValue_KeyPressed(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == '\b')
            {
                e.Handled = false; //Do not reject the input
            }
            else
            {
                e.Handled = true; //Reject the input
            }
        }

        private void HoursValue_KeyPressed(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == '\b')
            {
                e.Handled = false; //Do not reject the input
            }
            else
            {
                e.Handled = true; //Reject the input
            }
        }

        private void MinutesValue_KeyPressed(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == '\b')
            {
                e.Handled = false; //Do not reject the input
            }
            else
            {
                e.Handled = true; //Reject the input
            }
        }

        private void SecondsValue_KeyPressed(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == '\b')
            {
                e.Handled = false; //Do not reject the input
            }
            else
            {
                e.Handled = true; //Reject the input
            }
        }

        private void LonDegreesValue_KeyPressed(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == '\b')
            {
                e.Handled = false; //Do not reject the input
            }
            else
            {
                e.Handled = true; //Reject the input
            }
        }

        private void LonMinutesValue_KeyPressed(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == '\b')
            {
                e.Handled = false; //Do not reject the input
            }
            else
            {
                e.Handled = true; //Reject the input
            }
        }

        private void LatDegreesValue_KeyPressed(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == '\b')
            {
                e.Handled = false; //Do not reject the input
            }
            else
            {
                e.Handled = true; //Reject the input
            }
        }

        private void LatMinutesValue_KeyPressed(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == '\b')
            {
                e.Handled = false; //Do not reject the input
            }
            else
            {
                e.Handled = true; //Reject the input
            }
        }

        private void btnNewChart_Click(object sender, EventArgs e)
        {
            try
            {
                ResetControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SaveChartFile() - Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NameValue_KeyPressed(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetterOrDigit(e.KeyChar) || e.KeyChar == '\b' || e.KeyChar == ' ' || e.KeyChar == '.' || e.KeyChar == '-')
            {
                e.Handled = false; //Do not reject the input
            }
            else
            {
                e.Handled = true; //Reject the input
            }
        }

        private void POBValue_KeyPressed(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetterOrDigit(e.KeyChar) || e.KeyChar == '\b' || e.KeyChar == ' ' || e.KeyChar == '.' || e.KeyChar == '-')
            {
                e.Handled = false; //Do not reject the input
            }
            else
            {
                e.Handled = true; //Reject the input
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if( SaveChartFile() == false)
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Generate() - Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private bool BuildAstroCharts(string strAstroData, string strPDFFile, ref string strErrorMessage)
        {
            bool bResult = false;
            string strData = "";
            astroPerson apObj = new astroPerson();

            PdfWriter pdfWriterObj = null;
            PdfDocument pdfDoc = null;
            iText.Layout.Document docObj = null;

            try
            {
                strErrorMessage = "";

                #region Data File Processing
                pdfWriterObj = new PdfWriter(strPDFFile);
                pdfDoc = new PdfDocument(pdfWriterObj);
                pdfDoc.SetDefaultPageSize(PageSize.A4);
                pdfDoc.GetCatalog().SetPageLayout(PdfName.SinglePage);
                docObj = new iText.Layout.Document(pdfDoc);

                string[] strFileLineElements = strAstroData.Split('|');

                if (strFileLineElements.Length != 9)
                {
                    strErrorMessage = string.Format("Invalid record found in data\r\nRecord:" + strAstroData);
                    return false;
                }

                apObj.Clear();

                apObj.Name = strFileLineElements[0].Trim();
                apObj.Sex = strFileLineElements[1].Trim();
                apObj.BirthDateTime = DateTime.Parse(strFileLineElements[2].Trim().Replace(".", ":"), CultureInfo.CreateSpecificCulture("ta-IN"), DateTimeStyles.None);

                strData = strFileLineElements[3].Trim();
                apObj.TimeZoneValue = strData;
                int nTZHours = Convert.ToInt32(strData.Substring(1, 2));
                int nTZMinutes = Convert.ToInt32(strData.Substring(4, 2));
                if (strData[0] == '-')
                {
                    apObj.TimeZoneDifference -= TimeSpan.Parse(string.Format("0.{0}:{1}:00", nTZHours, nTZMinutes));
                }
                else
                {
                    apObj.TimeZoneDifference += TimeSpan.Parse(string.Format("0.{0}:{1}:00", nTZHours, nTZMinutes));
                }

                apObj.PlaceOfBirth = strFileLineElements[4].Trim();

                strData = strFileLineElements[5].Trim();
                apObj.Longitude = strData;
                string x = strData.Substring(5, 3);
                apObj.LongitudeDegrees = Convert.ToInt32(strData.Substring(5, 3));
                apObj.LongitudeMinutes = Convert.ToInt32(strData.Substring(9, 2));
                if (strData.Substring(0, 4) == "WEST")
                {
                    apObj.LongitudeDegrees *= -1;
                    apObj.LongitudeMinutes *= -1;
                }

                strData = strFileLineElements[6].Trim();
                apObj.Latitude = strData;
                apObj.LatitudeDegrees = Convert.ToInt32(strData.Substring(6, 2));
                apObj.LatitudeMinutes = Convert.ToInt32(strData.Substring(9, 2));
                if (strData.Substring(0, 5) == "SOUTH")
                {
                    apObj.LatitudeDegrees *= -1;
                    apObj.LatitudeMinutes *= -1;
                }

                apObj.TimeZoneName = strFileLineElements[7].Trim();
                apObj.HourCorrection = Convert.ToInt32(strFileLineElements[8].Trim());

                apObj.BirthDateTimeUTC = apObj.BirthDateTime + apObj.TimeZoneDifference;
                if (apObj.HourCorrection > 0)
                {
                    apObj.BirthDateTimeUTC -= new TimeSpan(apObj.HourCorrection, 0, 0);
                }

                astroChart acObj = new astroChart();

                astroChart.SwissPath = m_strSWEParentFolder;
                astroChart.SWEConFilePath = m_strSWEConFilePath;
                astroChart.SWEFolderPath = m_strSWEFolderPath;
                astroChart.HousingSystem = "P";
                astroChart.PrintAyanamsaCalculation = 0;
                astroChart.PrintAstroTables = 0;
                astroChart.PrintDasaTables = 1;

                if (acObj.AstroSwissEphemerisService(ref apObj, m_nAyanamsa, ref docObj, ref strErrorMessage) == true)
                {
                    bResult = true;
                }
                else
                {
                    bResult = false;
                }

                return bResult;

                #endregion
            }
            catch (Exception ex)
            {
                strErrorMessage = ex.Message;
            }
            finally
            {
                if (docObj != null)
                {
                    docObj.Close();
                }
                docObj = null;
                pdfWriterObj = null;
                pdfDoc = null;
            }

            return false;
        }

        private void HoraNumber_KeyPressed(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == '\b')
            {
                e.Handled = false; //Do not reject the input
            }
            else
            {
                e.Handled = true; //Reject the input
            }
        }

        private void btnNow_Click(object sender, EventArgs e)
        {
            DateTime dtNow = DateTime.Now;
            txtDD.Text = Convert.ToString(dtNow.Day);
            txtMM.Text = Convert.ToString(dtNow.Month);
            txtYYYY.Text = Convert.ToString(dtNow.Year);
            txtHour.Text = Convert.ToString(dtNow.Hour);
            txtMin.Text = Convert.ToString(dtNow.Minute);
            txtSec.Text = Convert.ToString(dtNow.Second);
        }

        private void btnWhyLonLat_Click(object sender, EventArgs e)
        {
            string strWhyReason = "Longitude & Latitude values for a city is not always the actual location of the birth.\nA small change can affect the Asc position.\nUse the web to find out the nearest possible longitude and latitude values and enter to get the correct Ascendant.";

            MessageBox.Show(strWhyReason, "Why should you enter longitude and latitude manullay?", MessageBoxButtons.OK);
        }

        private void btnTimeChart_Click(object sender, EventArgs e)
        {
            try
            {
                if (LoadChartFile() == false)
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "TimeChart() - Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            DateTime dtNow = DateTime.Now;
            txtDD.Text = Convert.ToString(dtNow.Day);
            txtMM.Text = Convert.ToString(dtNow.Month);
            txtYYYY.Text = Convert.ToString(dtNow.Year);
            txtHour.Text = Convert.ToString(dtNow.Hour);
            txtMin.Text = Convert.ToString(dtNow.Minute);
            txtSec.Text = Convert.ToString(dtNow.Second);
        }

        private void btnAboutUs_Click(object sender, EventArgs e)
        {
            AboutUs abObj = new AboutUs();

            abObj.ShowDialog();
        }
    }
}
