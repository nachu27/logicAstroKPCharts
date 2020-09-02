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
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Font;
using iText.Kernel.Colors;
using iText.IO.Font;
using iText.IO.Font.Constants;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout.Borders;
using iText.Layout.Renderer;


namespace srlWebCom.Astro.AstroObjects
{
    public class vmDasaElement
    {
        public bool DasaBeginning = false;
        public string DasaLord = "";
        public string BukthiLord = "";
        public string AntharaLord = "";
        public string StartDate = "";
        public string EndDate = "";
        public DateTime dtStartDate = DateTime.Now;
        public DateTime dtEndDate = DateTime.Now;

        public vmDasaElement()
        {
            Clear();
        }

        public void Clear()
        {
            DasaBeginning = false;
            DasaLord = "";
            BukthiLord = "";
            AntharaLord = "";
            StartDate = "";
            EndDate = "";
            dtStartDate = DateTime.Now;
            dtEndDate = DateTime.Now;
        }
    }

    public class vmDasaChart
    {
        #region Private Variables
        private string[] DasaPlanets = { "KE", "VE", "SU", "MO", "MA", "RA", "JU", "SA", "ME" };
        private int[] DasaYears = { 7, 20, 6, 10, 7, 18, 16, 19, 17 };
        private static ArrayList DasaList = new ArrayList();
        private PdfFont PdfFontObj = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
        private PdfFont PdfFontBoldObj = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
        #endregion

        #region Public Variables
        #endregion

        #region Private Methods
        private int GetDasaPlanetIndex(string strPlanet)
        {
            int pIndex = 0;

            for (int i = 0; i < DasaPlanets.Length; i++)
            {
                if (strPlanet.ToUpper() == DasaPlanets[i])
                {
                    pIndex = i;
                    break;
                }
            }

            return pIndex;
        }

        private string GetDasaPlanetName(string strPlanet)
        {
            string strPlanetFullName = "";

            switch (strPlanet)
            {
                case "KE":
                    strPlanetFullName = "Kethu";
                    break;

                case "VE":
                    strPlanetFullName = "Venus";
                    break;

                case "SU":
                    strPlanetFullName = "Sun";
                    break;

                case "MO":
                    strPlanetFullName = "Moon";
                    break;

                case "MA":
                    strPlanetFullName = "Mars";
                    break;

                case "RA":
                    strPlanetFullName = "Rahu";
                    break;

                case "JU":
                    strPlanetFullName = "Jupiter";
                    break;

                case "SA":
                    strPlanetFullName = "Saturn";
                    break;

                case "ME":
                    strPlanetFullName = "Mercury";
                    break;
            }

            return strPlanetFullName;
        }

        private void CalculateVMDasa(DateTime dtStarDate, string strSartDasaLord)
        {
            DateTime dtAStartDate = dtStarDate;
            DateTime dtAEndDate = dtStarDate;
            DateTime dtDasaEndDate = dtStarDate;

            DasaList.Clear();

            // Dummuy Object
            vmDasaElement vmDummyDasaObj = new vmDasaElement();
            vmDummyDasaObj.DasaLord = "--";
            vmDummyDasaObj.BukthiLord = "--";
            vmDummyDasaObj.AntharaLord = "--";
            vmDummyDasaObj.StartDate = "--";
            vmDummyDasaObj.EndDate = "--";
            DasaList.Add(vmDummyDasaObj);

            int dpIndex = GetDasaPlanetIndex(strSartDasaLord);
            for (int i = 0; i < 9; i++)
            {
                string strDasaLord = DasaPlanets[dpIndex];
                int nDasaYears = DasaYears[dpIndex];

                dtDasaEndDate = dtAStartDate;
                dtDasaEndDate = dtDasaEndDate.AddYears(nDasaYears);

                double nDasaTotalDays = (dtDasaEndDate - dtAStartDate).TotalDays;

                int bpIndex = dpIndex;
                for (int j = 0; j < 9; j++)
                {
                    string strBukthiLord = DasaPlanets[bpIndex];
                    int nBukthiYears = DasaYears[bpIndex];

                    double nBukthiTotalDays = nDasaTotalDays * nBukthiYears / 120.0;

                    int apIndex = bpIndex;
                    for (int k = 0; k < 9; k++)
                    {
                        string strAntharaLord = DasaPlanets[apIndex];
                        int nAntharaYears = DasaYears[apIndex];

                        double nAntharaTotalDays = nBukthiTotalDays * nAntharaYears / 120.0;

                        dtAEndDate = dtAStartDate.AddDays(nAntharaTotalDays);

                        vmDasaElement vmDasaObj = new vmDasaElement();
                        vmDasaObj.DasaLord = strDasaLord;
                        vmDasaObj.BukthiLord = strBukthiLord;
                        vmDasaObj.AntharaLord = strAntharaLord;
                        vmDasaObj.StartDate = dtAStartDate.ToString("dd-MM-yyyy");
                        vmDasaObj.dtStartDate = dtAStartDate;
                        vmDasaObj.EndDate = dtAEndDate.ToString("dd-MM-yyyy");
                        vmDasaObj.dtEndDate = dtAEndDate;
                        if (strDasaLord == strBukthiLord && strDasaLord == strAntharaLord)
                        {
                            vmDasaObj.DasaBeginning = true;
                        }
                        DasaList.Add(vmDasaObj);

                        dtAStartDate = dtAEndDate;

                        apIndex++;
                        if (apIndex > 8)
                        {
                            apIndex = 0;
                        }
                    }

                    bpIndex++;
                    if (bpIndex > 8)
                    {
                        bpIndex = 0;
                    }
                }

                dpIndex++;
                if (dpIndex > 8)
                {
                    dpIndex = 0;
                }
            }
        }
        #endregion

        #region Public Methods
        public bool CreateVMDasaChart(ref iText.Layout.Document pdfDocObj, DateTime dtDasaStartDate, string strDasaLord, ref string errorMessage)
        {
            try
            {
                CalculateVMDasa(dtDasaStartDate, strDasaLord);

                float PdfDefaultFontSize = 10.0f;
                float[] columnHouses = { 4f, 4f, 4f, 4f, 4f, 4f };

                iText.Layout.Element.Table vmDasaTable = new iText.Layout.Element.Table(UnitValue.CreatePercentArray(columnHouses)).UseAllAvailableWidth();

                int nLineNumber = 0;
                bool bPrintTitle = true;
                for (int nDasaEntry = 1; nDasaEntry <= DasaList.Count; nDasaEntry++)
                {
                    if (nDasaEntry >= 729) break;

                    if (bPrintTitle == true)
                    {
                        if (((vmDasaElement)DasaList[nDasaEntry]).DasaBeginning == true)
                        {
                            int nDasaPlanetIndex = GetDasaPlanetIndex(((vmDasaElement)DasaList[nDasaEntry]).DasaLord);
                            DateTime dtVMDasaEndDate = ((vmDasaElement)DasaList[nDasaEntry]).dtStartDate;
                            dtVMDasaEndDate = dtVMDasaEndDate.AddYears(DasaYears[nDasaPlanetIndex]);
                            string strVMDasaTitle = string.Format("{0} Dasa from {1} to {2} (Anthara Ending Dates)", GetDasaPlanetName(((vmDasaElement)DasaList[nDasaEntry]).DasaLord), ((vmDasaElement)DasaList[nDasaEntry]).dtStartDate.ToString("dd-MM-yyyy"), dtVMDasaEndDate.ToString("dd-MM-yyyy"));
                            vmDasaTable.AddCell(new Cell(1, 6).SetHeight(20).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph(strVMDasaTitle).SetFont(PdfFontBoldObj).SetFontSize(PdfDefaultFontSize)).SetBackgroundColor(new DeviceRgb(180, 230, 99)));
                        }

                        vmDasaTable.AddCell(new Cell(1, 2).SetHeight(20).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph(GetDasaPlanetName(((vmDasaElement)DasaList[nDasaEntry]).BukthiLord) + " Bukthi").SetFont(PdfFontBoldObj).SetFontSize(PdfDefaultFontSize)));
                        vmDasaTable.AddCell(new Cell(1, 2).SetHeight(20).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph(GetDasaPlanetName(((vmDasaElement)DasaList[nDasaEntry + 9]).BukthiLord) + " Bukthi").SetFont(PdfFontBoldObj).SetFontSize(PdfDefaultFontSize)));
                        vmDasaTable.AddCell(new Cell(1, 2).SetHeight(20).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph(GetDasaPlanetName(((vmDasaElement)DasaList[nDasaEntry + 18]).BukthiLord) + " Bukthi").SetFont(PdfFontBoldObj).SetFontSize(PdfDefaultFontSize)));
                        nLineNumber++;
                        bPrintTitle = false;
                    }

                    vmDasaTable.AddCell(new Cell().SetHeight(20).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph(GetDasaPlanetName(((vmDasaElement)DasaList[nDasaEntry]).AntharaLord)).SetFont(PdfFontObj).SetFontSize(PdfDefaultFontSize)));
                    vmDasaTable.AddCell(new Cell().SetHeight(20).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph(((vmDasaElement)DasaList[nDasaEntry]).EndDate).SetFont(PdfFontObj).SetFontSize(PdfDefaultFontSize)));

                    vmDasaTable.AddCell(new Cell().SetHeight(20).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph(GetDasaPlanetName(((vmDasaElement)DasaList[nDasaEntry + 9]).AntharaLord)).SetFont(PdfFontObj).SetFontSize(PdfDefaultFontSize)));
                    vmDasaTable.AddCell(new Cell().SetHeight(20).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph(((vmDasaElement)DasaList[nDasaEntry + 9]).EndDate).SetFont(PdfFontObj).SetFontSize(PdfDefaultFontSize)));

                    vmDasaTable.AddCell(new Cell().SetHeight(20).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph(GetDasaPlanetName(((vmDasaElement)DasaList[nDasaEntry + 18]).AntharaLord)).SetFont(PdfFontObj).SetFontSize(PdfDefaultFontSize)));
                    vmDasaTable.AddCell(new Cell().SetHeight(20).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph(((vmDasaElement)DasaList[nDasaEntry + 18]).EndDate).SetFont(PdfFontObj).SetFontSize(PdfDefaultFontSize)));
                    nLineNumber++;

                    if ((nDasaEntry + 18) % 27 == 0)
                    {
                        nDasaEntry = nDasaEntry + 18;
                        bPrintTitle = true;
                    }
                }

                pdfDocObj.Add(vmDasaTable);

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            return false;
        }
        #endregion
    }
}