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
using System.Reflection;
using System.Globalization;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Font;
using iText.Kernel.Colors;
using iText.IO.Font.Constants;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout.Borders;

namespace srlWebCom.Astro.AstroObjects
{
    public class astroTimeZone
    {
        public int Minutes = 0;
        public string Name = "";
    }

    class astroChart
    {
        #region Private Variable Declarations
        private ArrayList AstroElements = new ArrayList();
        private astroStar StarList = new astroStar();
        private PdfFont PdfFontObj = PdfFontFactory.CreateFont(StandardFonts.HELVETICA, true);
        private PdfFont PdfFontBoldObj = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD, true);
        private PdfFont PdfFontObliqueObj = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_OBLIQUE, true);
        private float PdfBiggerFontSize = 12.0f;
        private float PdfDefaultFontSize = 10.0f;
        private float PdfMinorFontSize = 9.4f;
        // private string[] PlanetTable = new string[] { "SU", "MO", "MA", "ME", "JU", "VE", "SA", "RA", "KE" };
        private string[] PlanetTable = new string[] { "SU", "MO", "MA", "RA", "JU", "SA", "ME", "KE", "VE" };
        private ArrayList KPAstroObjectList = new ArrayList();
        private ArrayList KPAstroTable = new ArrayList();
        private astroPosition AscendantPos = new astroPosition();
        private string[] DasaPlanets = { "KE", "VE", "SU", "MO", "MA", "RA", "JU", "SA", "ME" };
        private int[] DasaYears = { 7, 20, 6, 10, 7, 18, 16, 19, 17 };
        private int VMTotalDasaBalanceDays = 0;
        private string VMDasaLord = "";
        private DateTime dtDasaStartDate = DateTime.Now;

        // Color Codes
        DeviceRgb colorPlanet = new DeviceRgb(0, 0, 204);
        DeviceRgb colorCusp = new DeviceRgb(204, 0, 0);
        DeviceRgb colorNoPlanetInStars = new DeviceRgb(246, 4, 4);
        DeviceRgb colorSubColumnBackground = new DeviceRgb(240, 233, 216);
        DeviceRgb colorHeaderBackgroundGreen = new DeviceRgb(180, 230, 99);
        DeviceRgb colorHeaderBackgroundBrown = new DeviceRgb(221, 140, 8);
        DeviceRgb colorHeaderRed = new DeviceRgb(0, 0, 204);
        #endregion

        #region Public Variable Declarations
        static public string SwissPath = "";
        static public string SWEConFilePath = "";
        static public string SWEFolderPath = "";
        static public DateTime DTZeroYear = DateTime.Parse("291-03-21 04:09:00");
        static public int PrintAyanamsaCalculation = 0;
        static public string HousingSystem = "P";
        static public int PrintAstroTables = 0;
        static public int PrintDasaTables = 0;

        #endregion

        #region Private Methods
        private string GetPositionalSignName(string strOrgSignName)
        {
            string strSignName = "";

            switch (strOrgSignName)
            {
                case "Mesham":
                    strSignName = " 1.Mesham";
                    break;
                case "Rishabam":
                    strSignName = " 2.Rishabam";
                    break;
                case "Mithunam":
                    strSignName = " 3.Mithunam";
                    break;
                case "Katakam":
                    strSignName = " 4.Katakam";
                    break;
                case "Simham":
                    strSignName = " 5.Simham";
                    break;
                case "Kanni":
                    strSignName = " 6.Kanni";
                    break;
                case "Thulam":
                    strSignName = " 7.Thulam";
                    break;
                case "Viruchikam":
                    strSignName = " 8.Viruchikam";
                    break;
                case "Dhanusu":
                    strSignName = " 9.Dhanusu";
                    break;
                case "Makaram":
                    strSignName = "10.Makaram";
                    break;
                case "Kumbam":
                    strSignName = "11.Kumbam";
                    break;
                case "Meenam":
                    strSignName = "12.Meenam";
                    break;
            }

            return strSignName;
        }

        private string GetPlanetDisplayName(string strName)
        {
            string strPlanetName = "";
            if (strName.Length >= 2)
            {
                strPlanetName += strName[0];
                strPlanetName += Char.ToLower(strName[1]);
            }
            return strPlanetName;
        }

        private string FormatRasiData(string strRasiData, bool bReverse)
        {
            string strRasiDataFormatted = "";
            int i = 0;

            try
            {
                string[] strRasiElements = strRasiData.Split('|');

                if (!bReverse)
                {
                    for (i = 0; i < strRasiElements.Length; i++)
                    {
                        if (string.IsNullOrEmpty(strRasiElements[i]) == false)
                        {
                            strRasiDataFormatted += strRasiElements[i];
                            strRasiDataFormatted += "\n";
                        }
                    }
                }
                else
                {
                    for (i = strRasiElements.Length - 1; i >= 0; i--)
                    {
                        if (string.IsNullOrEmpty(strRasiElements[i]) == false)
                        {
                            strRasiDataFormatted += strRasiElements[i];
                            strRasiDataFormatted += "\n";
                        }
                    }
                }

                return strRasiDataFormatted;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void AddChartCell(ref iText.Layout.Element.Table astroTable, string strRasiData)
        {
            float cellHeight = 115.0f;
            float cellWidth = 10.0f;

            try
            {
                Paragraph cellData = new Paragraph("");
                cellData.SetFont(PdfFontObj);
                cellData.SetFontSize(PdfDefaultFontSize);

                string[] strRasiParts = strRasiData.Split('\n');
                for(int i=0; i<strRasiParts.Length; i++)
                {
                    string strRasiPart = strRasiParts[i];
                    if (string.IsNullOrEmpty(strRasiPart) == true) continue;

                    string[] strRasiElements = strRasiPart.Split('-');

                    if (strRasiElements.Length == 2)
                    {
                        iText.Layout.Element.Text objPartText = new Text("");
                        iText.Layout.Element.Text posPartText = new Text("");

                        string strObjectName = strRasiElements[0];
                        if (strObjectName.Length > 0 && strObjectName[0] == '~')
                        {
                            strObjectName = strObjectName.Substring(1);
                            objPartText.SetText(strObjectName);
                            objPartText.SetFont(PdfFontBoldObj);
                            objPartText.SetFontSize(PdfDefaultFontSize);
                            objPartText.SetFontColor(colorCusp);
                        }
                        else
                        {
                            objPartText.SetText(strObjectName);
                            objPartText.SetFont(PdfFontBoldObj);
                            objPartText.SetFontSize(PdfDefaultFontSize);
                            objPartText.SetFontColor(colorPlanet);
                        }

                        posPartText.SetText(strRasiElements[1]);
                        posPartText.SetFont(PdfFontObj);
                        posPartText.SetFontSize(PdfDefaultFontSize);

                        cellData.Add(objPartText);
                        cellData.Add(posPartText);
                        if (i < strRasiParts.Length-1)
                        {
                            cellData.Add("\n");
                        }

                        objPartText = null;
                        posPartText = null;
                    }
                    else
                    {
                        throw new Exception("Invalid rasi parts found while formatting!");
                    }
                }

                Cell cellObj = new Cell();
                cellObj.SetPaddingLeft(8);
                cellObj.SetWidth(cellWidth);
                cellObj.SetHeight(cellHeight);
                cellObj.SetHorizontalAlignment(HorizontalAlignment.CENTER);
                cellObj.SetVerticalAlignment(VerticalAlignment.MIDDLE);
                cellObj.Add(cellData);

                astroTable.AddCell(cellObj);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void AddChartInfo(ref iText.Layout.Element.Table astroTable, iText.Layout.Element.Table astroChartInfo, int colSpan, int hideTopBorder, int hideBottomBorder)
        {
            float cellHeight = 115.0f;
            float cellWidth = 10.0f;

            try
            {
                Cell cellObj = new Cell(1, colSpan);
                cellObj.SetWidth(cellWidth);
                cellObj.SetHeight(cellHeight);
                cellObj.SetHorizontalAlignment(HorizontalAlignment.CENTER);
                cellObj.SetVerticalAlignment(VerticalAlignment.MIDDLE);
                if (hideTopBorder == 1)
                {
                    cellObj.SetBorderTop(iText.Layout.Borders.Border.NO_BORDER);
                }
                if (hideBottomBorder == 1)
                {
                    cellObj.SetBorderBottom(iText.Layout.Borders.Border.NO_BORDER);
                }
                cellObj.Add(astroChartInfo);

                astroTable.AddCell(cellObj);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void AddHeaderCell(ref Table table, string strHeading)
        {
            try
            {
                Cell cell = new Cell();
                cell.SetTextAlignment(TextAlignment.CENTER);
                cell.SetFont(PdfFontObj);
                cell.SetPadding(5);
                cell.SetVerticalAlignment(VerticalAlignment.MIDDLE);
                cell.SetBackgroundColor(colorHeaderBackgroundBrown);
                cell.Add(new Paragraph(strHeading));
                table.AddCell(cell);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void AddDataCell(ref Table table, string strData, int nAlignment)
        {
            try
            {
                Cell cell = new Cell();
                cell.SetTextAlignment(TextAlignment.LEFT);
                if (nAlignment == 1)
                {
                    cell.SetTextAlignment(TextAlignment.CENTER);
                }
                cell.SetFont(PdfFontObj);
                cell.SetPadding(5);
                cell.SetVerticalAlignment(VerticalAlignment.MIDDLE);
                cell.Add(new Paragraph(strData));
                table.AddCell(cell);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private astroObject GetAstroObject(string strShortName)
        {
            for (int iDIndex = 0; iDIndex < AstroElements.Count; iDIndex++)
            {
                astroObject AO = (astroObject)AstroElements[iDIndex];

                if (AO.OShortName.ToUpper().Trim() == strShortName.ToUpper().Trim())
                {
                    return AO;
                }
            }
            return null;
        }

        private bool LoadKPAstroObjectList()
        {
            int iDIndex = 0;
            string strCurrentHouse = "";

            try
            {
                AscendantPos.Clear();

                for (iDIndex = 0; iDIndex < AstroElements.Count; iDIndex++)
                {
                    astroObject AO = (astroObject)AstroElements[iDIndex];

                    if (AO.OShortName.Trim() == "1")
                    {
                        AscendantPos.Set(AO.OZodiacLongitude.DegHours, AO.OZodiacLongitude.Minutes, AO.OZodiacLongitude.Seconds);
                        AO = null;
                        break;
                    }

                    AO = null;
                }

                int iPCount = 0;

                while (iPCount < AstroElements.Count)
                {
                    astroObject AO = (astroObject)AstroElements[iDIndex];

                    if (AO.OType.ToUpper() == "HOUSE")
                    {
                        strCurrentHouse = AO.OShortName.Trim();
                    }

                    if (AO.OType.ToUpper() == "PLANET" || AO.OType.ToUpper() == "HOUSE" || AO.OName.ToUpper() == "ASCENDANT")
                    {
                        kpCommonObject kpAstroObj = new kpCommonObject();

                        kpAstroObj.Type = AO.OType.Trim();
                        if (AO.OName.ToUpper() == "ASCENDANT")
                        {
                            kpAstroObj.Name = "As";
                        }
                        else
                        {
                            kpAstroObj.Name = AO.OShortName.Trim();
                        }
                        kpAstroObj.ZLongitude = AO.OZodiacLongitude.Value;
                        kpAstroObj.ZLongitudePos.Clear();
                        kpAstroObj.ZLongitudePos.Set(AO.OZodiacLongitude.DegHours, AO.OZodiacLongitude.Minutes, AO.OZodiacLongitude.Seconds);
                        kpAstroObj.SLontitude = AO.OSignLongitude.Value;
                        kpAstroObj.ZLatitude = AO.OZodiacLatitude.ShortValue + " " + AO.OZodiacLatitudeDirection.Substring(0, 1);
                        kpAstroObj.Declination = AO.ODeclination.ShortValue + " " + AO.ODeclinationDirection.Substring(0, 1);
                        kpAstroObj.House = strCurrentHouse.Trim();
                        kpAstroObj.HouseNo = Convert.ToInt32(strCurrentHouse.Trim());
                        kpAstroObj.Sign = AO.OSignEnglish.Trim();
                        kpAstroObj.SignLord = AO.SignLord.Trim();
                        kpAstroObj.StarLord = AO.StarLord.Trim();
                        kpAstroObj.StarSubLord = AO.StarSubLord.Trim();
                        kpAstroObj.StarSubSubLord = AO.StarSubSubLord.Trim();

                        astroStar starObj = astroStar.GetStarForPosition(AO.OZodiacLongitude.DegHours, AO.OZodiacLongitude.Minutes);
                        kpAstroObj.StarName = starObj.Star;
                        kpAstroObj.StarPadham = starObj.PositionInStar;
                        kpAstroObj.Navamsam = starObj.Navamsam;

                        kpAstroObj.DegreesFromAsc.Set(AO.OZodiacLongitude.DegHours, AO.OZodiacLongitude.Minutes, AO.OZodiacLongitude.Seconds);
                        kpAstroObj.DegreesFromAsc.Sub(AscendantPos.DegHours, AscendantPos.Minutes, AscendantPos.Seconds);

                        KPAstroObjectList.Add(kpAstroObj);
                    }

                    iDIndex++;

                    if (iDIndex >= AstroElements.Count)
                    {
                        iDIndex = 0;
                    }

                    iPCount++;

                    AO = null;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("LoadKPAstroObjectList:" + ex.Message);
            }
        }

        private kpCommonObject GetRawKPAstroCommonObject(string strObject)
        {
            for (int iDIndex = 0; iDIndex < KPAstroObjectList.Count; iDIndex++)
            {
                kpCommonObject AO = (kpCommonObject)KPAstroObjectList[iDIndex];

                if (AO.Name.ToUpper() == strObject.ToUpper())
                {
                    return AO;
                }
            }

            return null;
        }

        private int GetPlanetsInTheStarsOf(string strPlanet, ref string strPlanetsInStars)
        {
            int nPlanetsInStars = 0;

            strPlanetsInStars = "";

            for (int iDIndex = 0; iDIndex < KPAstroObjectList.Count; iDIndex++)
            {
                kpCommonObject AO = (kpCommonObject)KPAstroObjectList[iDIndex];

                if (AO.Type.ToUpper() == "PLANET")
                {
                    if (AO.Name.ToUpper() != strPlanet.ToUpper() && AO.StarLord.ToUpper() == strPlanet.ToUpper())
                    {
                        if (strPlanetsInStars.Length > 0)
                        {
                            strPlanetsInStars += ",";
                        }

                        strPlanetsInStars += AO.Name;

                        nPlanetsInStars++;
                    }
                }

                AO = null;
            }

            return nPlanetsInStars;
        }

        private string GetHouseOccupiedBy(string strPlanet)
        {
            string strResult = "";

            for (int iDIndex = 0; iDIndex < KPAstroObjectList.Count; iDIndex++)
            {
                kpCommonObject AO = (kpCommonObject)KPAstroObjectList[iDIndex];

                if (AO.Type.ToUpper() == "PLANET")
                {
                    if (AO.Name.ToUpper() == strPlanet.ToUpper())
                    {
                        if (strResult.Length > 0)
                        {
                            strResult += ",";
                        }
                        strResult += Convert.ToString(AO.HouseNo);
                    }
                }

                AO = null;
            }

            return strResult;
        }

        private string GetHousesOwnedBy(string strPlanet)
        {
            string strResult = "";

            for (int iDIndex = 0; iDIndex < KPAstroObjectList.Count; iDIndex++)
            {
                kpCommonObject AO = (kpCommonObject)KPAstroObjectList[iDIndex];

                if (AO.Type.ToUpper() == "HOUSE")
                {
                    if (AO.SignLord.ToUpper() == strPlanet.ToUpper())
                    {
                        if (strResult.Length > 0)
                        {
                            strResult += ",";
                        }
                        strResult += Convert.ToString(AO.HouseNo);
                    }
                }

                AO = null;
            }

            return strResult;
        }

        private string GetPlanetsInTheStarsOfPlanetsIn(int nHouseNo)
        {
            string strResult = "";
            int nPlanetsInStars = 0;
            string strPlanetsInStars = "";

            for (int iDIndex = 0; iDIndex < KPAstroObjectList.Count; iDIndex++)
            {
                kpCommonObject AO = (kpCommonObject)KPAstroObjectList[iDIndex];

                if (AO.Type.ToUpper() == "PLANET" && AO.HouseNo == nHouseNo)
                {
                    strPlanetsInStars = "";
                    nPlanetsInStars = 0;
                    nPlanetsInStars = GetPlanetsInTheStarsOf(AO.Name, ref strPlanetsInStars);
                    if (nPlanetsInStars > 0)
                    {
                        if (strResult.Length > 0)
                        {
                            strResult += ",";
                        }
                        strResult += strPlanetsInStars;
                    }
                }

                AO = null;
            }

            return strResult;
        }

        private string GetPlanetsIn(int nHouseNo)
        {
            string strResult = "";

            for (int iDIndex = 0; iDIndex < KPAstroObjectList.Count; iDIndex++)
            {
                kpCommonObject AO = (kpCommonObject)KPAstroObjectList[iDIndex];

                if (AO.Type.ToUpper() == "PLANET" && AO.HouseNo == nHouseNo)
                {
                    if (strResult.Length > 0)
                    {
                        strResult += ",";
                    }
                    strResult += AO.Name;
                }

                AO = null;
            }

            return strResult;
        }

        private void GetHouseDetails(int nHouseNo, string strPlanet, ref int nPlanetsInHouse, ref int nOwnHouse)
        {
            nPlanetsInHouse = 0;
            nOwnHouse = 0;
            string strCurrentHouseOwner = "";
            int nCurrentHouse = 0;

            for (int iDIndex = 0; iDIndex < KPAstroObjectList.Count; iDIndex++)
            {
                kpCommonObject AO = (kpCommonObject)KPAstroObjectList[iDIndex];

                if (AO.Type.ToUpper() == "HOUSE" && nCurrentHouse != AO.HouseNo)
                {
                    nCurrentHouse = AO.HouseNo;
                    strCurrentHouseOwner = AO.SignLord;
                }

                if (AO.Type.ToUpper() == "HOUSE")
                {
                    if (AO.HouseNo == nHouseNo && strCurrentHouseOwner.ToUpper() == strPlanet.ToUpper())
                    {
                        nOwnHouse = 1;
                    }
                }

                if (AO.Type.ToUpper() == "PLANET")
                {
                    if (AO.HouseNo == nHouseNo && AO.Name.ToUpper() != strPlanet.ToUpper())
                    {
                        nPlanetsInHouse++;
                    }
                }

                AO = null;
            }
        }

        private void AssignSignificators(ref kpCommonObject plObj, int nPlanetsInStars, string strHouse, int nPlanetsInHouse, int nOwnHouse, int nRuleType)
        {
            /*
                nRuleType
                1 - House Owned by Star Lord
                2 - House in which Planet is Deposited
                3 - House Owned by Planet
            */

            switch (nRuleType )
            {
                case 1:
                    {
                        #region House Owned by Star Lord
                        if (nPlanetsInHouse <= 0)
                        {
                            if (plObj.PrimeSignificators.Length > 0)
                            {
                                plObj.PrimeSignificators += ",";
                            }

                            plObj.PrimeSignificators += string.Format("{0}", strHouse);
                        }
                        else
                        {
                            if (plObj.GeneralSignificators.Length > 0)
                            {
                                plObj.GeneralSignificators += ",";
                            }

                            plObj.GeneralSignificators += string.Format("{0}", strHouse);
                        }
                        #endregion
                    }
                    break;

                case 2:
                    {
                        #region House in which Planet is Deposited
                        if (nPlanetsInStars <= 0)
                        {
                            if (plObj.PrimeSignificators.Length > 0)
                            {
                                plObj.PrimeSignificators += ",";
                            }

                            plObj.PrimeSignificators += string.Format("D.{0}", strHouse);
                        }
                        else
                        {
                            if (plObj.GeneralSignificators.Length > 0)
                            {
                                plObj.GeneralSignificators += ",";
                            }

                            plObj.GeneralSignificators += string.Format("D.{0}", strHouse);
                        }
                        #endregion
                    }
                    break;

                case 3:
                    {
                        #region House Owned by Planet
                        if (nPlanetsInStars <= 0)
                        {
                            if (nPlanetsInHouse <= 0)
                            {
                                if (plObj.PrimeSignificators.Length > 0)
                                {
                                    plObj.PrimeSignificators += ",";
                                }

                                if (nOwnHouse == 1)
                                {
                                    plObj.PrimeSignificators += string.Format("O.{0}", strHouse);
                                }
                                else
                                {
                                    plObj.PrimeSignificators += string.Format("{0}", strHouse);
                                }
                            }
                            else
                            {
                                if (plObj.GeneralSignificators.Length > 0)
                                {
                                    plObj.GeneralSignificators += ",";
                                }

                                if (nOwnHouse == 1)
                                {
                                    plObj.GeneralSignificators += string.Format("O.{0}", strHouse);
                                }
                                else
                                {
                                    plObj.GeneralSignificators += string.Format("{0}", strHouse);
                                }
                            }
                        }
                        else
                        {
                            if (plObj.GeneralSignificators.Length > 0)
                            {
                                plObj.GeneralSignificators += ",";
                            }

                            if (nOwnHouse == 1)
                            {
                                plObj.GeneralSignificators += string.Format("O.{0}", strHouse);
                            }
                            else
                            {
                                plObj.GeneralSignificators += string.Format("{0}", strHouse);
                            }
                        }
                        #endregion
                    }
                    break;
            }
        }

        private bool PrepareKPAstroTables()
        {
            int iDIndex = 0;
            int nPlanetsInStars = 0;
            string strPlanetsInStars = "";
            int nHouseNo = 0;
            int nPlanetsInHouse = 0;
            int nOwnHouse = 0;

            try
            {
                for (iDIndex = 0; iDIndex < KPAstroObjectList.Count; iDIndex++)
                {
                    kpCommonObject AO = (kpCommonObject)KPAstroObjectList[iDIndex];

                    AO.PrimeSignificators = "";
                    AO.GeneralSignificators = "";

                    #region Planet D1, D2, D3, D4
                    if (AO.Type.ToUpper() == "PLANET")
                    {
                        nPlanetsInStars = 0;
                        nPlanetsInStars = GetPlanetsInTheStarsOf(AO.Name, ref strPlanetsInStars);
                        if (nPlanetsInStars == 0)
                        {
                            AO.Strength = "*";
                            AO.PlanetsInStars = 0;
                        }
                        else
                        {
                            AO.Strength = "";
                            AO.PlanetsInStars = nPlanetsInStars;
                        }

                        #region Star Lord Deposited In 
                        AO.D1 = GetHouseOccupiedBy(AO.StarLord);
                        AO.PrimeSignificators += AO.D1;
                        #endregion

                        #region Star Lord Owns Houses
                        AO.D2 = GetHousesOwnedBy(AO.StarLord);
                        string[] StarLordOwnHouses = AO.D2.Split(',');
                        foreach (string strHouse in StarLordOwnHouses)
                        {
                            nHouseNo = 0;
                            nPlanetsInHouse = 0;
                            nOwnHouse = 0;

                            if (string.IsNullOrEmpty(strHouse) == true) continue;

                            if (strHouse.Trim() != AO.D1.Trim())
                            {
                                nHouseNo = Convert.ToInt32(strHouse);

                                GetHouseDetails(nHouseNo, AO.StarLord, ref nPlanetsInHouse, ref nOwnHouse);

                                AssignSignificators(ref AO, nPlanetsInStars, strHouse, nPlanetsInHouse, nOwnHouse, 1);
                            }
                        }
                        #endregion

                        #region Planet Deposited In
                        AO.D3 = Convert.ToString(AO.HouseNo);
                        nHouseNo = 0;
                        nPlanetsInHouse = 0;
                        nOwnHouse = 0;
                        nHouseNo = Convert.ToInt32(AO.D3);

                        GetHouseDetails(nHouseNo, AO.Name, ref nPlanetsInHouse, ref nOwnHouse);

                        AssignSignificators(ref AO, nPlanetsInStars, AO.D3, nPlanetsInHouse, nOwnHouse, 2);
                        #endregion

                        #region Planet Owns Houses
                        AO.D4 = GetHousesOwnedBy(AO.Name);
                        string[] PlanetOwnHouses = AO.D4.Split(',');
                        foreach (string strHouse in PlanetOwnHouses)
                        {
                            nHouseNo = 0;
                            nPlanetsInHouse = 0;
                            nOwnHouse = 0;

                            if (string.IsNullOrEmpty(strHouse) == true) continue;

                            nHouseNo = Convert.ToInt32(strHouse);

                            GetHouseDetails(nHouseNo, AO.Name, ref nPlanetsInHouse, ref nOwnHouse);

                            AssignSignificators(ref AO, nPlanetsInStars, strHouse, nPlanetsInHouse, nOwnHouse, 3);
                        }
                        #endregion
                    }
                    #endregion

                    #region House D1, D2, D3, D4
                    if (AO.Type.ToUpper() == "HOUSE")
                    {
                        AO.D1 = GetPlanetsInTheStarsOfPlanetsIn(AO.HouseNo);

                        nPlanetsInStars = 0;
                        nPlanetsInStars = GetPlanetsInTheStarsOf(AO.SignLord, ref strPlanetsInStars);
                        if (nPlanetsInStars > 0)
                        {
                            AO.D2 = strPlanetsInStars;
                        }

                        AO.D3 = GetPlanetsIn(AO.HouseNo);

                        AO.D4 = AO.SignLord;

                        AO.D5 = "";
                        AO.D6 = "";
                    }
                    #endregion

                    #region Sub Lord D5, D6
                    AO.D5 = GetHouseOccupiedBy(AO.StarSubLord);

                    if (AO.StarSubLord.ToUpper() == "RA" || AO.StarSubLord.ToUpper() == "KE")
                    {
                        kpCommonObject subNodeObj = GetRawKPAstroCommonObject(AO.StarSubLord);
                        if (subNodeObj != null)
                        {
                            AO.D6 = GetHouseOccupiedBy(subNodeObj.SignLord);
                            AO.D6 += ",";
                            AO.D6 += GetHousesOwnedBy(subNodeObj.SignLord);
                        }
                        subNodeObj = null;
                    }
                    else
                    {
                        AO.D6 = GetHousesOwnedBy(AO.StarSubLord);
                    }
                    #endregion

                    #region Sign Lord D7, D8
                    AO.D7 = GetHouseOccupiedBy(AO.SignLord);
                    AO.D8 = GetHousesOwnedBy(AO.SignLord);
                    #endregion

                    KPAstroTable.Add(AO);

                    AO = null;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("PrepareKPAstroTables:" + ex.Message);
            }
        }

        private kpCommonObject GetFormattedKPAstroCommonObject(string strObject)
        {
            for (int iDIndex = 0; iDIndex < KPAstroTable.Count; iDIndex++)
            {
                kpCommonObject AO = (kpCommonObject)KPAstroTable[iDIndex];

                if (AO.Name.ToUpper() == strObject.ToUpper())
                {
                    return AO;
                }
            }

            return null;
        }

        private int GetAstpectType(int nDegree)
        {
            int nResult = 0;

            // Conjunction 0 Major – Neutral
            if (nDegree >= 0 && nDegree <= 15)
            {
                return 1;
            }

            // Sextile 60 Major – Soft / Coaxing
            if (nDegree >= 52 && nDegree <= 74)
            {
                return 2;
            }

            // Square 90 Major – Hard / Active
            if (nDegree >= 75 && nDegree <= 105)
            {
                return 3;
            }

            // Trine 120 Major – Soft / Passive
            if (nDegree >= 106 && nDegree <= 128)
            {
                return 2;
            }

            // Opposition 180 Good or Bad
            if (nDegree >= 165 && nDegree <= 180)
            {
                return 3;
            }

            return nResult;
        }

        private void ProcessComparison(kpCommonObject AO, ref iText.Layout.Element.Table aspectMaster)
        {
            astroPosition aspDiff = new astroPosition();

            try
            {
                foreach (string strPlanet in PlanetTable)
                {
                    kpCommonObject RefObj = GetFormattedKPAstroCommonObject(strPlanet);

                    if (RefObj != null)
                    {
                        aspDiff.Clear();
                        if (AO.ZLongitudePos.DegHours > RefObj.ZLongitudePos.DegHours)
                        {
                            aspDiff.Set(AO.ZLongitudePos.DegHours, AO.ZLongitudePos.Minutes, AO.ZLongitudePos.Seconds);
                            aspDiff.Sub(RefObj.ZLongitudePos.DegHours, RefObj.ZLongitudePos.Minutes, RefObj.ZLongitudePos.Seconds);
                        }
                        else
                        {
                            aspDiff.Set(RefObj.ZLongitudePos.DegHours, RefObj.ZLongitudePos.Minutes, RefObj.ZLongitudePos.Seconds);
                            aspDiff.Sub(AO.ZLongitudePos.DegHours, AO.ZLongitudePos.Minutes, AO.ZLongitudePos.Seconds);
                        }

                        int nDifferenceDeg = 0;
                        if (aspDiff.DegHours > 180)
                        {
                            nDifferenceDeg = 360 - aspDiff.DegHours;
                        }
                        else
                        {
                            nDifferenceDeg = aspDiff.DegHours;
                        }

                        int nAspectType = GetAstpectType(nDifferenceDeg);

                        DeviceRgb aspectColor = null;
                        switch (nAspectType)
                        {
                            case 1:
                                aspectColor = new DeviceRgb(255, 153, 51);
                                break;

                            case 2:
                                aspectColor = new DeviceRgb(51, 204, 51);
                                break;

                            case 3:
                                aspectColor = colorNoPlanetInStars;
                                break;

                            default:
                                break;
                        }

                        aspectMaster.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHeight(25).Add(new Paragraph(Convert.ToString(nDifferenceDeg)).SetFont(PdfFontObj).SetFontSize(PdfDefaultFontSize)).SetBackgroundColor(aspectColor));
                    }
                    else
                    {
                        aspectMaster.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("").SetFont(PdfFontObj).SetFontSize(PdfDefaultFontSize)));
                    }

                    RefObj = null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string GetShortAyanamsaName(string strAyanamsaCode)
        {
            string strAyanamsaName = "";

            switch (strAyanamsaCode)
            {
                case "-1": strAyanamsaName = "Tropical"; break;  // "Tropical"
                case "0": strAyanamsaName = "Fag/Brad"; break;  // "Fagan/Bradley"
                case "1": strAyanamsaName = "Lahiri"; break;  // "Lahiri"
                case "2": strAyanamsaName = "De Luce"; break;  // "De Luce"
                case "3": strAyanamsaName = "Raman"; break;  // "Raman"
                case "4": strAyanamsaName = "Usha/Shashi"; break;  // "Usha/Shashi"
                case "5": strAyanamsaName = "Krishnamurti"; break;  // "Krishnamurti"
                case "6": strAyanamsaName = "Djwhal Khul"; break;  // "Djwhal Khul"
                case "7": strAyanamsaName = "Yukteshwar"; break;  // "Yukteshwar"
                case "8": strAyanamsaName = "J.N. Bhasin"; break;  // "J.N. Bhasin"
                case "9": strAyanamsaName = "BL/Kugler 1"; break;  // "Babylonian/Kugler 1"
                case "10": strAyanamsaName = "BL/Kugler 2"; break;  // "Babylonian/Kugler 2"
                case "11": strAyanamsaName = "BL/Kugler 3"; break;  // "Babylonian/Kugler 3"
                case "12": strAyanamsaName = "BL/Huber"; break;  // "Babylonian/Huber"
                case "13": strAyanamsaName = "BL/Eta Pis"; break;  // "Babylonian/Eta Piscium"
                case "14": strAyanamsaName = "BL/Ald=15.Tau"; break;  // "Babylonian/Aldebaran = 15 Tau"
                case "15": strAyanamsaName = "Hipparchos"; break;  // "Hipparchos"
                case "16": strAyanamsaName = "Sassanian"; break;  // "Sassanian"
                case "17": strAyanamsaName = "GC=0.Sag"; break;  // "Galact. Center = 0 Sag"
                case "18": strAyanamsaName = "J2000"; break;  // "J2000"
                case "19": strAyanamsaName = "J1900"; break;  // "J1900"
                case "20": strAyanamsaName = "B1950"; break;  // "B1950"
                case "21": strAyanamsaName = "Sy.Sidd"; break;  // "Suryasiddhanta"
                case "22": strAyanamsaName = "Sy.Sidd/Mean"; break;  // "Suryasiddhanta, mean Sun"
                case "23": strAyanamsaName = "AryBat"; break;  // "Aryabhata"
                case "24": strAyanamsaName = "AryBat/Mean"; break;  // "Aryabhata, mean Sun"
                case "25": strAyanamsaName = "SS Revati"; break;  // "SS Revati"
                case "26": strAyanamsaName = "SS Citra"; break;  // "SS Citra"
                case "27": strAyanamsaName = "True Citra"; break;  // "True Citra"
                case "28": strAyanamsaName = "True Revati"; break;  // "True Revati"
                case "29": strAyanamsaName = "True Pushya"; break;  // "True Pushya (PVRN Rao) "
                case "30": strAyanamsaName = "GC (Gil Brand) "; break;  // "Galactic Center (Gil Brand) "
                case "31": strAyanamsaName = "GE (IAU1958) "; break;  // "Galactic Equator (IAU1958) "
                case "32": strAyanamsaName = "GE"; break;  // "Galactic Equator"
                case "33": strAyanamsaName = "GE Mid-Mula"; break;  // "Galactic Equator mid-Mula"
                case "34": strAyanamsaName = "Skydram"; break;  // "Skydram (Mardyks) "
                case "35": strAyanamsaName = "True Mula"; break;  // "True Mula (Chandra Hari) "
                case "36": strAyanamsaName = "DRV/GC/Mula"; break;  // "Dhruva/Gal.Center/Mula (Wilhelm) "
                case "37": strAyanamsaName = "AryBat-522"; break;  // "Aryabhata 522"
                case "38": strAyanamsaName = "BL/Britton"; break;  // "Babylonian/Britton"
                case "39": strAyanamsaName = "Vedic Sheoran"; break;  // "Vedic Sheoran
                case "40": strAyanamsaName = "Cochrane/GC=0.Cap"; break;  // "Cochrane(Gal.Center = 0 Cap)
                case "41": strAyanamsaName = "GE (Fiorenza)"; break;  // "Galactic Equator (Fiorenza)"
                case "42": strAyanamsaName = "Vettius Valens"; break;  // "Vettius Valens"
                case "43": strAyanamsaName = "Lahiri-1940"; break;  // "Lahiri 1940"
                case "44": strAyanamsaName = "Lahiri-VP285"; break;  // "Lahiri VP285"
                case "45": strAyanamsaName = "KP-SE-NewComb"; break;  // "Krishnamurti-Senthilathiban"
                case "46": strAyanamsaName = "Lahiri-ICRC"; break;  // "Lahiri ICRC"
            }

            return strAyanamsaName;
        }

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

        private string FormatSignificatorEntries(string strListIn)
        {
            string strListOut = "";
            Hashtable hashHSig = new Hashtable();
            hashHSig.Clear();

            string[] strElements = strListIn.Split(',');
            foreach (string strElement in strElements)
            {
                if (strElement.Length > 0)
                {
                    if (hashHSig.Contains(strElement) == false)
                    {
                        hashHSig.Add(strElement, strElement);
                        if (strListOut.Length > 0)
                        {
                            strListOut += ",";
                        }
                        strListOut += strElement;
                    }
                }
            }

            return strListOut;
        }

        private string FormatStarName(string strStarIn)
        {
            string strStarOut = "";

            if (strStarIn == null)
                return null;

            strStarIn = strStarIn.ToLower();

            if (strStarIn.Length > 1)
            {
                strStarOut = char.ToUpper(strStarIn[0]) + strStarIn.Substring(1);
            }

            return strStarOut;
        }

        private string ConvertToRomanLetters(string strHouseNo)
        {
            string strRomanHouse = "";

            switch (strHouseNo.Trim())
            {
                case "1":
                    // strRomanHouse = "I";
                    strRomanHouse = "Lag";
                    break;

                case "2":
                    strRomanHouse = "II";
                    break;

                case "3":
                    strRomanHouse = "III";
                    break;

                case "4":
                    strRomanHouse = "IV";
                    break;

                case "5":
                    strRomanHouse = "V";
                    break;

                case "6":
                    strRomanHouse = "VI";
                    break;

                case "7":
                    strRomanHouse = "VII";
                    break;

                case "8":
                    strRomanHouse = "VIII";
                    break;

                case "9":
                    strRomanHouse = "IX";
                    break;

                case "10":
                    strRomanHouse = "X";
                    break;

                case "11":
                    strRomanHouse = "XI";
                    break;

                case "12":
                    strRomanHouse = "XII";
                    break;
            }

            return strRomanHouse;
        }

        private iText.Layout.Element.Paragraph FormatSignificator(string strPrimeSignificators, string strGeneralSignificators)
        {
            int i;
            Paragraph paraSignificators = new Paragraph("");
            paraSignificators.SetFont(PdfFontObj);
            paraSignificators.SetFontSize(PdfBiggerFontSize);

            try
            {
                #region Prime Significators
                string[] strPrimeSignificatorEntries = strPrimeSignificators.Split(',');
                for (i = 0; i < strPrimeSignificatorEntries.Length; i++)
                {
                    string strSignificator = strPrimeSignificatorEntries[i];
                    if (string.IsNullOrEmpty(strSignificator) == true) continue;

                    if (strSignificator.IndexOf("O.") == 0)
                    {
                        strSignificator = strSignificator.Substring(2);
                        iText.Layout.Element.Text objOwnerText = new Text(strSignificator);
                        objOwnerText.SetFont(PdfFontBoldObj);
                        objOwnerText.SetFontSize(PdfBiggerFontSize);
                        objOwnerText.SetFontColor(colorPlanet);
                        paraSignificators.Add(objOwnerText);
                    }
                    else if (strSignificator.IndexOf("D.") == 0)
                    {
                        strSignificator = strSignificator.Substring(2);
                        iText.Layout.Element.Text objDepositedText = new Text(strSignificator);
                        objDepositedText.SetFont(PdfFontBoldObj);
                        objDepositedText.SetFontSize(PdfBiggerFontSize);
                        objDepositedText.SetFontColor(colorCusp);
                        paraSignificators.Add(objDepositedText);
                    }
                    else
                    {
                        iText.Layout.Element.Text objPrimeText = new Text(strSignificator);
                        objPrimeText.SetFont(PdfFontBoldObj);
                        objPrimeText.SetFontSize(PdfBiggerFontSize);
                        paraSignificators.Add(objPrimeText);
                    }

                    if (i < strPrimeSignificatorEntries.Length - 1)
                    {
                        paraSignificators.Add(", ");
                    }
                }
                #endregion

                paraSignificators.Add(" (");

                #region General Significators
                string[] strGeneralSignificatorEntries = strGeneralSignificators.Split(',');
                for (i = 0; i < strGeneralSignificatorEntries.Length; i++)
                {
                    string strSignificator = strGeneralSignificatorEntries[i];
                    if (string.IsNullOrEmpty(strSignificator) == true) continue;

                    if (strSignificator.IndexOf("O.") == 0)
                    {
                        strSignificator = strSignificator.Substring(2);
                        iText.Layout.Element.Text objOwnerText = new Text(strSignificator);
                        objOwnerText.SetFont(PdfFontBoldObj);
                        objOwnerText.SetFontSize(PdfBiggerFontSize);
                        objOwnerText.SetFontColor(colorPlanet);
                        paraSignificators.Add(objOwnerText);
                    }
                    else if (strSignificator.IndexOf("D.") == 0)
                    {
                        strSignificator = strSignificator.Substring(2);
                        iText.Layout.Element.Text objDepositedText = new Text(strSignificator);
                        objDepositedText.SetFont(PdfFontBoldObj);
                        objDepositedText.SetFontSize(PdfBiggerFontSize);
                        objDepositedText.SetFontColor(colorCusp);
                        paraSignificators.Add(objDepositedText);
                    }
                    else
                    {
                        paraSignificators.Add(strSignificator);
                    }

                    if (i < strGeneralSignificatorEntries.Length - 1)
                    {
                        paraSignificators.Add(", ");
                    }
                }
                #endregion

                paraSignificators.Add(")");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return paraSignificators;
        }
        #endregion

        #region Public Methods
        public bool AstroSwissEphemerisService(ref astroPerson apObj, int nAyanamsa, ref iText.Layout.Document pdfDocObj, ref string strErrorMessage)
        {
            #region Variable Declaration
            DateTime birthDateTimeUTC = apObj.BirthDateTimeUTC;
            int longitudeDegrees = apObj.LongitudeDegrees;
            int longitudeMinutes = apObj.LongitudeMinutes;
            int latitudeDegrees = apObj.LatitudeDegrees;
            int latitudeMinutes = apObj.LatitudeMinutes;
            int iDIndex = 0;
            astroDasa dasaObj = new astroDasa();
            swephObject SEObj = new swephObject();
            string[] RasiDataArray = new string[12];
            int SEAyanamsa = 0;
            string strAyanamsaCalculationDetails = "";
            string strStarLords = "";
            string DasaLord = "";
            int DasaBalanceYears = 0;
            int DasaBalanceMonths = 0;
            int DasaBalanceDays = 0;
            string strDasaLord = "";
            string strBukthiLord = "";
            #endregion

            try
            {
                AstroElements.Clear();
                KPAstroObjectList.Clear();

                #region Astrology Chart Data Construction

                #region Prepare and Construct Astronomical Data for Chart
                if (!dasaObj.LoadDasaChart(ref strErrorMessage))
                {
                    return false;
                }

                SEAyanamsa = nAyanamsa;

                if (!swephObject.KPAstroObject.LoadKPStarSubLordsTable())
                {
                    strErrorMessage = "Unable to load KP Start Table";
                    return false;
                }

                SEObj.SWEConFilePath = SWEConFilePath;
                SEObj.SWEFolderPath = SWEFolderPath;

                if (!SEObj.Process(birthDateTimeUTC, longitudeDegrees, longitudeMinutes, latitudeDegrees, latitudeMinutes, HousingSystem, SEAyanamsa, false))
                {
                    strErrorMessage = SEObj.ErrorMessage;
                    return false;
                }
                #endregion

                #region Prepare Astro Objects
                AstroElements.Clear();

                foreach (astroObject AO in SEObj.AstroObjects)
                {
                    strStarLords = string.Format("{0}", swephObject.KPAstroObject.GetKPStarSubLords(AO.OSignEnglish, AO.OSignLongitude, 3));
                    AO.SetStarLords(strStarLords);

                    #region Calculate Dasa Balance
                    if (AO.OName.IndexOf("MOON") != -1)
                    {
                        strDasaLord = swephObject.KPAstroObject.GetKPStarSubLords(AO.OSignEnglish, AO.OSignLongitude, 1);

                        VMDasaLord = strDasaLord;

                        DasaLord = swephObject.GetPlanetName(strDasaLord).ToUpper();

                        int nDasaYears = swephObject.KPAstroObject.GetDasaYears(strDasaLord);

                        int nStarCounts = (AO.OZodiacLongitude.TotalSeconds / 48000);

                        int nMinutesRemaining = (((nStarCounts + 1) * 48000) - AO.OZodiacLongitude.TotalSeconds) / 60;

                        int nDaysBalance = (int)Math.Ceiling((nMinutesRemaining * nDasaYears * 365.25) / 800);

                        VMTotalDasaBalanceDays = nDaysBalance;

                        int nMonths = (int)(nDaysBalance / 30.4375);
                        DasaBalanceDays = (int)Math.Ceiling(nDaysBalance - (nMonths * 30.4375));
                        DasaBalanceYears = nMonths / 12;
                        DasaBalanceMonths = nMonths - (DasaBalanceYears * 12);

                        dtDasaStartDate = apObj.BirthDateTime.AddDays(VMTotalDasaBalanceDays);
                        dtDasaStartDate = dtDasaStartDate.AddYears((int)(-1 * nDasaYears));
                    }
                    #endregion

                    if (swephObject.IsAstroObjectListed(AO.OName))
                    {
                        if (AO.OName == "TRUE NODE")
                        {
                            AO.OName = "RAHU";
                            AO.OType = "Planet";
                            AO.OShortName = "Ra";
                            AstroElements.Add(AO);
                            astroObject Kethu = new astroObject();
                            Kethu = AO.DeepClone();
                            Kethu.OName = "KETHU";
                            Kethu.OType = "Planet";
                            Kethu.OShortName = "Ke";
                            Kethu.OZodiacLongitude.Add(180, 0, 0);
                            Kethu.SetAstroNames();
                            strStarLords = string.Format("{0}", swephObject.KPAstroObject.GetKPStarSubLords(Kethu.OSignEnglish, Kethu.OSignLongitude, 3));
                            Kethu.SetStarLords(strStarLords);
                            AstroElements.Add(Kethu);
                        }
                        else
                        {
                            if (AO.OName != "MEAN NODE")
                            {
                                string strOName = AO.OName.ToUpper();
                                if (strOName.Length > 5)
                                {
                                    if (strOName.Substring(0, 5) == "HOUSE")
                                    {
                                        AO.OType = "HOUSE";
                                    }
                                }
                                AstroElements.Add(AO);
                            }
                        }
                    }
                }

                AstroElements.Sort();

                dasaObj.GenerateDasaChart(apObj.BirthDateTime.ToString("yyyy-MM-dd"), DasaLord, DasaBalanceYears, DasaBalanceMonths, DasaBalanceDays);
                #endregion

                #region Astrology Chart Signs and Objects
                string strPlanetName = "";
                string strSignName = "";
                string strRasiData = "";
                int nPadChars = 0;

                for (iDIndex = 0; iDIndex < AstroElements.Count; iDIndex++)
                {
                    astroObject AO = (astroObject)AstroElements[iDIndex];

                    if (AO.OType.ToUpper() == "PLANET" || AO.OType.ToUpper() == "HOUSE")
                    {
                        strPlanetName = AO.OShortName;

                        nPadChars = 3;
                        if (AO.OType.ToUpper() == "HOUSE")
                        {
                            nPadChars = 4;
                            strPlanetName = "~" + ConvertToRomanLetters(AO.OShortName);
                        }

                        astroStar starObj = astroStar.GetStarForPosition(AO.OZodiacLongitude.DegHours, AO.OZodiacLongitude.Minutes);

                        strSignName = GetPositionalSignName(AO.OSignTamil);

                        if (AO.OSpeedPerDayDirection.PadLeft(1, ' ') == "R" && strPlanetName.ToUpper() != "RA" && strPlanetName.ToUpper() != "KE")
                        {
                            // strRasiData = string.Format("{0}! - {1} / {2}({3})|", strPlanetName, AO.OSignLongitude.ShortValue, AO.StarLord, starObj.PositionInStar);
                            strRasiData = string.Format("{0}(R) - {1}|", strPlanetName.PadRight(nPadChars), AO.OSignLongitude.Value);
                        }
                        else
                        {
                            // strRasiData = string.Format("{0}  - {1} / {2}({3})|", strPlanetName, AO.OSignLongitude.ShortValue, AO.StarLord, starObj.PositionInStar);
                            strRasiData = string.Format("{0} - {1}|", strPlanetName.PadRight(nPadChars), AO.OSignLongitude.Value);
                        }

                        switch (strSignName.ToUpper())
                        {
                            case " 1.MESHAM":
                                RasiDataArray[0] += strRasiData;
                                break;

                            case " 2.RISHABAM":
                                RasiDataArray[1] += strRasiData;
                                break;

                            case " 3.MITHUNAM":
                                RasiDataArray[2] += strRasiData;
                                break;

                            case " 4.KATAKAM":
                                RasiDataArray[3] += strRasiData;
                                break;

                            case " 5.SIMHAM":
                                RasiDataArray[4] += strRasiData;
                                break;

                            case " 6.KANNI":
                                RasiDataArray[5] += strRasiData;
                                break;

                            case " 7.THULAM":
                                RasiDataArray[6] += strRasiData;
                                break;

                            case " 8.VIRUCHIKAM":
                                RasiDataArray[7] += strRasiData;
                                break;

                            case " 9.DHANUSU":
                                RasiDataArray[8] += strRasiData;
                                break;

                            case "10.MAKARAM":
                                RasiDataArray[9] += strRasiData;
                                break;

                            case "11.KUMBAM":
                                RasiDataArray[10] += strRasiData;
                                break;

                            case "12.MEENAM":
                                RasiDataArray[11] += strRasiData;
                                break;
                        }
                    }

                    AO = null;
                }
                #endregion

                #endregion

                #region Astrology Chart PDF Construction
                #region Variables and Initializtion
                string strSingleRasiInfo = "";
                string strTimeZoneValue = "";
                string strTableHeader = "";
                string strName = "";
                string strSex = "";
                string strDateTimeOfBirth = "";
                string strPlaceOfBirth = "";
                string strLongitude = "";
                string strLatitude = "";
                string strMoonStarInfo = "";
                string strDasaBalance = "";
                string strAyanamsa = "";
                KPAstroObjectList.Clear();
                KPAstroTable.Clear();
                #endregion

                #region Prepare Planetary Tables
                string strHeaderLable = "Chart created from logicAstroCharts";
                Paragraph headerData = new Paragraph(strHeaderLable).SetFont(PdfFontObj).SetFontSize(PdfDefaultFontSize);

                if (LoadKPAstroObjectList() == false)
                {
                    throw new Exception("Unable to load KP Astro Objects List");
                }

                if (PrepareKPAstroTables() == false)
                {
                    throw new Exception("Unable to prepare KP Astro Table");
                }
                #endregion

                #region Preparing display information
                strSingleRasiInfo = "";
                strTimeZoneValue = apObj.TimeZoneValue;
                if (strTimeZoneValue.Length > 0)
                {
                    if (strTimeZoneValue[0] == '-')
                    {
                        strTimeZoneValue = strTimeZoneValue.Replace('-', '+');
                    }
                    else
                    {
                        if (strTimeZoneValue[0] == '+')
                        {
                            strTimeZoneValue = strTimeZoneValue.Replace('+', '-');
                        }
                    }
                }

                strSex = "-";
                if (apObj.Sex.ToUpper() == "MALE")
                {
                    strSex = "Male";
                }
                if (apObj.Sex.ToUpper() == "FEMALE")
                {
                    strSex = "Female";
                }
                if (apObj.Sex.ToUpper() == "TRANSIT")
                {
                    strSex = "Transit";
                }

                strName = apObj.Name;
                if (strName.Length > 23)
                {
                    strName = strName.Substring(0, 23);
                }
                strDateTimeOfBirth = string.Format("{0}  ({1})", apObj.BirthDateTime.ToString("dd-MM-yyyy  HH:mm:ss"), strTimeZoneValue);
                strPlaceOfBirth = apObj.PlaceOfBirth;
                if (strPlaceOfBirth.Length > 25)
                {
                    strPlaceOfBirth = strPlaceOfBirth.Substring(0, 25);
                }
                strLongitude = string.Format("{0}", apObj.Longitude);
                strLongitude = strLongitude.Replace("EAST", "E");
                strLongitude = strLongitude.Replace("WEST", "W");
                strLatitude = string.Format("{0}", apObj.Latitude);
                strLatitude = strLatitude.Replace("NORTH", "N");
                strLatitude = strLatitude.Replace("SOUTH", "S");
                strDasaBalance = string.Format("{0}   {1:00} Y - {2:00} M - {3:00} D", DasaLord, DasaBalanceYears, DasaBalanceMonths, DasaBalanceDays);
                strAyanamsa = string.Format("{0} ({1})", SEObj.Ayanamsa.Value, GetShortAyanamsaName(Convert.ToString(nAyanamsa)));
                kpCommonObject moonObj = GetFormattedKPAstroCommonObject("MO");
                if (moonObj != null)
                {
                    strMoonStarInfo = string.Format("{0} - {1}", moonObj.StarName, moonObj.StarPadham);
                }
                moonObj = null;
                #endregion

                #region Information and Main Planetary Tables Preparation
                #region Rasi Chart Cells

                #region Upper Panel
                Paragraph paraChartUpperPanel = new Paragraph("");
                paraChartUpperPanel.SetFont(PdfFontObj);
                paraChartUpperPanel.SetFontSize(PdfDefaultFontSize);
                paraChartUpperPanel.Add(new iText.Layout.Element.Text(string.Format("{0}, {1}\n", strName, strSex)).SetFont(PdfFontBoldObj).SetFontSize(PdfDefaultFontSize).SetFontColor(colorPlanet));
                paraChartUpperPanel.Add(new iText.Layout.Element.Text(strDateTimeOfBirth + "\n").SetFont(PdfFontObj).SetFontSize(PdfDefaultFontSize));
                paraChartUpperPanel.Add(new iText.Layout.Element.Text(strPlaceOfBirth + "\n\n").SetFont(PdfFontObj).SetFontSize(PdfDefaultFontSize));
                paraChartUpperPanel.Add(new iText.Layout.Element.Text("Star: ").SetFont(PdfFontBoldObj).SetFontSize(PdfDefaultFontSize));
                paraChartUpperPanel.Add(new iText.Layout.Element.Text(strMoonStarInfo + "\n").SetFont(PdfFontObj).SetFontSize(PdfDefaultFontSize));
                paraChartUpperPanel.Add(new iText.Layout.Element.Text("Dasa Balance\n").SetFont(PdfFontBoldObj).SetFontSize(PdfDefaultFontSize));
                paraChartUpperPanel.Add(new iText.Layout.Element.Text(strDasaBalance).SetFont(PdfFontObj).SetFontSize(PdfDefaultFontSize));
                #endregion

                #region Bottom Panel
                Paragraph paraChartBottomPanel = new Paragraph("");
                paraChartBottomPanel.SetFont(PdfFontObj);
                paraChartBottomPanel.SetFontSize(PdfDefaultFontSize);
                paraChartBottomPanel.Add(new iText.Layout.Element.Text("Long: ").SetFont(PdfFontBoldObj).SetFontSize(PdfDefaultFontSize));
                paraChartBottomPanel.Add(new iText.Layout.Element.Text(strLongitude + "\n").SetFont(PdfFontObj).SetFontSize(PdfDefaultFontSize));
                paraChartBottomPanel.Add(new iText.Layout.Element.Text("  Lat: ").SetFont(PdfFontBoldObj).SetFontSize(PdfDefaultFontSize));
                paraChartBottomPanel.Add(new iText.Layout.Element.Text(strLatitude + "\n").SetFont(PdfFontObj).SetFontSize(PdfDefaultFontSize));
                paraChartBottomPanel.Add(new iText.Layout.Element.Text("Ayanamsa (True): ").SetFont(PdfFontBoldObj).SetFontSize(PdfDefaultFontSize));
                paraChartBottomPanel.Add(new iText.Layout.Element.Text(SEObj.Ayanamsa.Value + "\n").SetFont(PdfFontObj).SetFontSize(PdfDefaultFontSize));
                paraChartBottomPanel.Add(new iText.Layout.Element.Text("(" + GetShortAyanamsaName(Convert.ToString(nAyanamsa)) + ")\n").SetFont(PdfFontObj).SetFontSize(PdfDefaultFontSize));
                paraChartBottomPanel.Add(new iText.Layout.Element.Text("SiderealTime:  ").SetFont(PdfFontBoldObj).SetFontSize(PdfDefaultFontSize));
                paraChartBottomPanel.Add(new iText.Layout.Element.Text(SEObj.SiderealTime).SetFont(PdfFontObj).SetFontSize(PdfDefaultFontSize));
                paraChartBottomPanel.Add(new iText.Layout.Element.Text("\n\n").SetFont(PdfFontObj).SetFontSize(PdfDefaultFontSize));
                paraChartBottomPanel.Add(new iText.Layout.Element.Text("logicAstroCharts / logicAstro.com").SetFont(PdfFontBoldObj).SetFontSize(PdfDefaultFontSize).SetFontColor(colorHeaderBackgroundBrown));
                #endregion

                float[] columnInfoRasi = { 6 };
                iText.Layout.Element.Table astroChartUpperPanel = new iText.Layout.Element.Table(UnitValue.CreatePercentArray(columnInfoRasi)).UseAllAvailableWidth();
                iText.Layout.Element.Table astroChartBottomPanel = new iText.Layout.Element.Table(UnitValue.CreatePercentArray(columnInfoRasi)).UseAllAvailableWidth();

                astroChartUpperPanel.AddCell(new Cell().SetHeight(115.0f).SetWidth(10.0f).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.TOP).SetBorder(Border.NO_BORDER).Add(paraChartUpperPanel));
                astroChartBottomPanel.AddCell(new Cell().SetHeight(115.0f).SetWidth(10.0f).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.TOP).SetBorder(Border.NO_BORDER).Add(paraChartBottomPanel));

                float[] additionalInfoPanel = { 3, 2.5f, 2.5f, 2.5f, 2.5f };
                iText.Layout.Borders.Border DottedBorderObj = new DottedBorder(1);
                iText.Layout.Element.Table astroChartInfo1 = new iText.Layout.Element.Table(UnitValue.CreatePercentArray(additionalInfoPanel)).UseAllAvailableWidth();
                iText.Layout.Element.Table astroChartInfo2 = new iText.Layout.Element.Table(UnitValue.CreatePercentArray(additionalInfoPanel)).UseAllAvailableWidth();
                iText.Layout.Element.Table astroChartInfo3 = new iText.Layout.Element.Table(UnitValue.CreatePercentArray(additionalInfoPanel)).UseAllAvailableWidth();
                iText.Layout.Element.Table astroChartInfo4 = new iText.Layout.Element.Table(UnitValue.CreatePercentArray(additionalInfoPanel)).UseAllAvailableWidth();

                astroChartInfo1.SetBorder(Border.NO_BORDER);
                astroChartInfo2.SetBorder(Border.NO_BORDER);
                astroChartInfo3.SetBorder(Border.NO_BORDER);
                astroChartInfo4.SetBorder(Border.NO_BORDER);

                astroChartInfo1.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(DottedBorderObj).SetBackgroundColor(colorHeaderBackgroundGreen).Add(new Paragraph("Pl").SetFont(PdfFontBoldObj).SetFontSize(PdfMinorFontSize)));
                astroChartInfo1.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(DottedBorderObj).SetBackgroundColor(colorHeaderBackgroundGreen).Add(new Paragraph("Sgn").SetFont(PdfFontBoldObj).SetFontSize(PdfMinorFontSize)));
                astroChartInfo1.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(DottedBorderObj).SetBackgroundColor(colorHeaderBackgroundGreen).Add(new Paragraph("Str").SetFont(PdfFontBoldObj).SetFontSize(PdfMinorFontSize)));
                astroChartInfo1.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(DottedBorderObj).SetBackgroundColor(colorHeaderBackgroundGreen).Add(new Paragraph("Sub").SetFont(PdfFontBoldObj).SetFontSize(PdfMinorFontSize)));
                astroChartInfo1.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(DottedBorderObj).SetBackgroundColor(colorHeaderBackgroundGreen).Add(new Paragraph("SS").SetFont(PdfFontBoldObj).SetFontSize(PdfMinorFontSize)));

                for (int pIndex = 0; pIndex < 9; pIndex++)
                {
                    kpCommonObject plObj = GetFormattedKPAstroCommonObject(PlanetTable[pIndex]);

                    if (plObj != null)
                    {
                        if (plObj.Name.ToUpper() == "SU" || plObj.Name.ToUpper() == "MO" || plObj.Name.ToUpper() == "MA" || plObj.Name.ToUpper() == "ME" || plObj.Name.ToUpper() == "JU")
                        {
                            if (plObj.PlanetsInStars == 0)
                            {
                                astroChartInfo1.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(DottedBorderObj).Add(new Paragraph(plObj.Name + "*").SetFont(PdfFontBoldObj).SetFontSize(PdfMinorFontSize).SetFontColor(colorNoPlanetInStars)));
                            }
                            else
                            {
                                astroChartInfo1.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(DottedBorderObj).Add(new Paragraph(plObj.Name).SetFont(PdfFontBoldObj).SetFontSize(PdfMinorFontSize).SetFontColor(colorPlanet)));
                            }
                            astroChartInfo1.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(DottedBorderObj).Add(new Paragraph(plObj.SignLord).SetFont(PdfFontObj).SetFontSize(PdfMinorFontSize)));
                            astroChartInfo1.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(DottedBorderObj).Add(new Paragraph(plObj.StarLord).SetFont(PdfFontObj).SetFontSize(PdfMinorFontSize)));
                            astroChartInfo1.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(DottedBorderObj).SetBackgroundColor(colorSubColumnBackground).Add(new Paragraph(plObj.StarSubLord).SetFont(PdfFontObj).SetFontSize(PdfMinorFontSize)));
                            astroChartInfo1.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(DottedBorderObj).Add(new Paragraph(plObj.StarSubSubLord).SetFont(PdfFontObj).SetFontSize(PdfMinorFontSize)));
                        }
                        else if (plObj.Name.ToUpper() == "VE" || plObj.Name.ToUpper() == "SA" || plObj.Name.ToUpper() == "RA" || plObj.Name.ToUpper() == "KE")
                        {
                            if (plObj.PlanetsInStars == 0)
                            {
                                astroChartInfo2.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(DottedBorderObj).Add(new Paragraph(plObj.Name + "*").SetFont(PdfFontBoldObj).SetFontSize(PdfMinorFontSize).SetFontColor(colorNoPlanetInStars)));
                            }
                            else
                            {
                                astroChartInfo2.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(DottedBorderObj).Add(new Paragraph(plObj.Name).SetFont(PdfFontBoldObj).SetFontSize(PdfMinorFontSize).SetFontColor(colorPlanet)));
                            }
                            astroChartInfo2.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(DottedBorderObj).Add(new Paragraph(plObj.SignLord).SetFont(PdfFontObj).SetFontSize(PdfMinorFontSize)));
                            astroChartInfo2.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(DottedBorderObj).Add(new Paragraph(plObj.StarLord).SetFont(PdfFontObj).SetFontSize(PdfMinorFontSize)));
                            astroChartInfo2.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(DottedBorderObj).SetBackgroundColor(colorSubColumnBackground).Add(new Paragraph(plObj.StarSubLord).SetFont(PdfFontObj).SetFontSize(PdfMinorFontSize)));
                            astroChartInfo2.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(DottedBorderObj).Add(new Paragraph(plObj.StarSubSubLord).SetFont(PdfFontObj).SetFontSize(PdfMinorFontSize)));
                        }
                    }
                    plObj = null;
                }

                for (int nHouseIdx = 1; nHouseIdx <= 12; nHouseIdx++)
                {
                    kpCommonObject hoObj = GetFormattedKPAstroCommonObject(Convert.ToString(nHouseIdx));
                    if (hoObj != null)
                    {
                        kpCommonObject hoSubObj = GetFormattedKPAstroCommonObject(hoObj.StarSubLord);

                        if (hoSubObj != null)
                        {
                            if (nHouseIdx == 1 )
                            {
                                astroChartInfo2.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(DottedBorderObj).SetBackgroundColor(colorHeaderBackgroundGreen).Add(new Paragraph("Cus").SetFont(PdfFontBoldObj).SetFontSize(PdfMinorFontSize)));
                                astroChartInfo2.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(DottedBorderObj).SetBackgroundColor(colorHeaderBackgroundGreen).Add(new Paragraph("Sgn").SetFont(PdfFontBoldObj).SetFontSize(PdfMinorFontSize)));
                                astroChartInfo2.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(DottedBorderObj).SetBackgroundColor(colorHeaderBackgroundGreen).Add(new Paragraph("Str").SetFont(PdfFontBoldObj).SetFontSize(PdfMinorFontSize)));
                                astroChartInfo2.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(DottedBorderObj).SetBackgroundColor(colorHeaderBackgroundGreen).Add(new Paragraph("Sub").SetFont(PdfFontBoldObj).SetFontSize(PdfMinorFontSize)));
                                astroChartInfo2.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(DottedBorderObj).SetBackgroundColor(colorHeaderBackgroundGreen).Add(new Paragraph("SS").SetFont(PdfFontBoldObj).SetFontSize(PdfMinorFontSize)));

                                astroChartInfo2.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(DottedBorderObj).Add(new Paragraph(hoObj.Name).SetFont(PdfFontBoldObj).SetFontSize(PdfMinorFontSize).SetFontColor(colorPlanet)));
                                astroChartInfo2.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(DottedBorderObj).Add(new Paragraph(hoObj.SignLord).SetFont(PdfFontObj).SetFontSize(PdfMinorFontSize)));
                                astroChartInfo2.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(DottedBorderObj).Add(new Paragraph(hoObj.StarLord).SetFont(PdfFontObj).SetFontSize(PdfMinorFontSize)));
                                if (hoSubObj.PlanetsInStars == 0)
                                {
                                    astroChartInfo2.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(DottedBorderObj).SetBackgroundColor(colorSubColumnBackground).Add(new Paragraph(hoObj.StarSubLord + "*").SetFont(PdfFontBoldObj).SetFontSize(PdfMinorFontSize).SetFontColor(colorNoPlanetInStars)));
                                }
                                else
                                {
                                    astroChartInfo2.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(DottedBorderObj).SetBackgroundColor(colorSubColumnBackground).Add(new Paragraph(hoObj.StarSubLord).SetFont(PdfFontObj).SetFontSize(PdfMinorFontSize)));
                                }
                                astroChartInfo2.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(DottedBorderObj).Add(new Paragraph(hoObj.StarSubSubLord).SetFont(PdfFontObj).SetFontSize(PdfMinorFontSize)));
                            }

                            if (nHouseIdx >= 2 && nHouseIdx <= 7)
                            {
                                astroChartInfo3.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(DottedBorderObj).Add(new Paragraph(hoObj.Name).SetFont(PdfFontBoldObj).SetFontSize(PdfMinorFontSize).SetFontColor(colorPlanet)));
                                astroChartInfo3.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(DottedBorderObj).Add(new Paragraph(hoObj.SignLord).SetFont(PdfFontObj).SetFontSize(PdfMinorFontSize)));
                                astroChartInfo3.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(DottedBorderObj).Add(new Paragraph(hoObj.StarLord).SetFont(PdfFontObj).SetFontSize(PdfMinorFontSize)));
                                if (hoSubObj.PlanetsInStars == 0)
                                {
                                    astroChartInfo3.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(DottedBorderObj).SetBackgroundColor(colorSubColumnBackground).Add(new Paragraph(hoObj.StarSubLord + "*").SetFont(PdfFontBoldObj).SetFontSize(PdfMinorFontSize).SetFontColor(colorNoPlanetInStars)));
                                }
                                else
                                {
                                    astroChartInfo3.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(DottedBorderObj).SetBackgroundColor(colorSubColumnBackground).Add(new Paragraph(hoObj.StarSubLord).SetFont(PdfFontObj).SetFontSize(PdfMinorFontSize)));
                                }
                                astroChartInfo3.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(DottedBorderObj).Add(new Paragraph(hoObj.StarSubSubLord).SetFont(PdfFontObj).SetFontSize(PdfMinorFontSize)));
                            }

                            if (nHouseIdx >= 8 && nHouseIdx <= 12)
                            {
                                astroChartInfo4.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(DottedBorderObj).Add(new Paragraph(hoObj.Name).SetFont(PdfFontBoldObj).SetFontSize(PdfMinorFontSize).SetFontColor(colorPlanet)));
                                astroChartInfo4.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(DottedBorderObj).Add(new Paragraph(hoObj.SignLord).SetFont(PdfFontObj).SetFontSize(PdfMinorFontSize)));
                                astroChartInfo4.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(DottedBorderObj).Add(new Paragraph(hoObj.StarLord).SetFont(PdfFontObj).SetFontSize(PdfMinorFontSize)));
                                if (hoSubObj.PlanetsInStars == 0)
                                {
                                    astroChartInfo4.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(DottedBorderObj).SetBackgroundColor(colorSubColumnBackground).Add(new Paragraph(hoObj.StarSubLord + "*").SetFont(PdfFontBoldObj).SetFontSize(PdfMinorFontSize).SetFontColor(colorNoPlanetInStars)));
                                }
                                else
                                {
                                    astroChartInfo4.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(DottedBorderObj).SetBackgroundColor(colorSubColumnBackground).Add(new Paragraph(hoObj.StarSubLord).SetFont(PdfFontObj).SetFontSize(PdfMinorFontSize)));
                                }
                                astroChartInfo4.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(DottedBorderObj).Add(new Paragraph(hoObj.StarSubSubLord).SetFont(PdfFontObj).SetFontSize(PdfMinorFontSize)));
                            }
                        }
                        hoSubObj = null;
                    }

                    hoObj = null;
                }

                astroChartInfo4.AddCell(new Cell(1, 6).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(DottedBorderObj).Add(new Paragraph("* No planets in its stars").SetFont(PdfFontBoldObj).SetFontSize(PdfMinorFontSize).SetFontColor(colorNoPlanetInStars)));

                #endregion

                #region Signification Table
                float[] columnSignification = { 8f, 2f, 3f, 2f, 8f };
                float significationRowHeight = 19f;

                iText.Layout.Element.Table astroSignificationTable = new iText.Layout.Element.Table(UnitValue.CreatePercentArray(columnSignification)).UseAllAvailableWidth();

                astroSignificationTable.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHeight(significationRowHeight).Add(new Paragraph("Star-Wise-Significations").SetFont(PdfFontBoldObj).SetFontSize(PdfDefaultFontSize)));
                astroSignificationTable.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHeight(significationRowHeight).SetBackgroundColor(colorHeaderBackgroundGreen).Add(new Paragraph("Star").SetFont(PdfFontObj).SetFontSize(PdfDefaultFontSize)));
                astroSignificationTable.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHeight(significationRowHeight).SetBackgroundColor(colorHeaderBackgroundGreen).Add(new Paragraph("Planet").SetFont(PdfFontBoldObj).SetFontSize(PdfDefaultFontSize)));
                astroSignificationTable.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHeight(significationRowHeight).SetBackgroundColor(colorHeaderBackgroundGreen).Add(new Paragraph("Sub").SetFont(PdfFontObj).SetFontSize(PdfDefaultFontSize)));
                astroSignificationTable.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHeight(significationRowHeight).Add(new Paragraph("Sub-Wise-Significations").SetFont(PdfFontBoldObj).SetFontSize(PdfDefaultFontSize)));

                for (int pIndex = 0; pIndex < 9; pIndex++)
                {
                    kpCommonObject plObj = GetFormattedKPAstroCommonObject(PlanetTable[pIndex]);
                    if (plObj != null)
                    {
                        Paragraph paraStarWiseSignificators = FormatSignificator(plObj.PrimeSignificators, plObj.GeneralSignificators);
                        Paragraph paraSubWiseSignificators = new Paragraph("");

                        kpCommonObject plSubObj = GetFormattedKPAstroCommonObject(plObj.StarSubLord);
                        if (plSubObj != null)
                        {
                            paraSubWiseSignificators = FormatSignificator(plSubObj.PrimeSignificators, plSubObj.GeneralSignificators);
                        }
                        plSubObj = null;

                        astroSignificationTable.AddCell(new Cell().SetTextAlignment(TextAlignment.RIGHT).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHeight(significationRowHeight).SetPaddingRight(8).Add(paraStarWiseSignificators));

                        astroSignificationTable.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHeight(significationRowHeight).Add(new Paragraph(plObj.StarLord).SetFont(PdfFontObj).SetFontSize(PdfBiggerFontSize)));

                        if (plObj.PlanetsInStars == 0)
                        {
                            astroSignificationTable.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHeight(significationRowHeight).Add(new Paragraph(plObj.Name + "*").SetFont(PdfFontBoldObj).SetFontSize(PdfBiggerFontSize).SetFontColor(colorNoPlanetInStars)));
                        }
                        else
                        {
                            astroSignificationTable.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHeight(significationRowHeight).Add(new Paragraph(plObj.Name).SetFont(PdfFontBoldObj).SetFontSize(PdfBiggerFontSize).SetFontColor(colorPlanet)));
                        }

                        astroSignificationTable.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHeight(significationRowHeight).Add(new Paragraph(plObj.StarSubLord).SetFont(PdfFontObj).SetFontSize(PdfBiggerFontSize)));

                        astroSignificationTable.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHeight(significationRowHeight).SetPaddingLeft(8).Add(paraSubWiseSignificators));
                    }

                    plObj = null;
                }

                Paragraph paraSignificationDetails = new Paragraph("");
                paraSignificationDetails.SetFont(PdfFontObj);
                paraSignificationDetails.SetFontSize(PdfDefaultFontSize);
                paraSignificationDetails.Add(new iText.Layout.Element.Text("Outside Brackets - Prime Significations | Inside Brackets - Other Significations\n").SetFont(PdfFontObj).SetFontSize(PdfDefaultFontSize));
                paraSignificationDetails.Add(new iText.Layout.Element.Text("Red Color - Deposited Bhava").SetFont(PdfFontObj).SetFontSize(PdfDefaultFontSize).SetFontColor(colorCusp));
                paraSignificationDetails.Add(new iText.Layout.Element.Text(" | ").SetFont(PdfFontObj).SetFontSize(PdfDefaultFontSize));
                paraSignificationDetails.Add(new iText.Layout.Element.Text("Blue Color - Ownership Bhavas").SetFont(PdfFontObj).SetFontSize(PdfDefaultFontSize).SetFontColor(colorPlanet));
                #endregion
                #endregion

                #region Rasi Chart
                // pdfDocObj.Add(headerData);

                float[] columnZodiac = { 3, 3, 3, 3, 5 };
                iText.Layout.Element.Table astroTable = new iText.Layout.Element.Table(UnitValue.CreatePercentArray(columnZodiac)).UseAllAvailableWidth();

                // Meenam
                strSingleRasiInfo = "";
                if (string.IsNullOrEmpty(RasiDataArray[11]) == false) strSingleRasiInfo = FormatRasiData(RasiDataArray[11], true);
                AddChartCell(ref astroTable, strSingleRasiInfo);

                // Mesham
                strSingleRasiInfo = "";
                if (string.IsNullOrEmpty(RasiDataArray[0]) == false) strSingleRasiInfo = FormatRasiData(RasiDataArray[0], false);
                AddChartCell(ref astroTable, strSingleRasiInfo);

                // Rishabham
                strSingleRasiInfo = "";
                if (string.IsNullOrEmpty(RasiDataArray[1]) == false) strSingleRasiInfo = FormatRasiData(RasiDataArray[1], false);
                AddChartCell(ref astroTable, strSingleRasiInfo);

                // Mithunam
                strSingleRasiInfo = "";
                if (string.IsNullOrEmpty(RasiDataArray[2]) == false) strSingleRasiInfo = FormatRasiData(RasiDataArray[2], false);
                AddChartCell(ref astroTable, strSingleRasiInfo);

                // Information Panel 1
                strSingleRasiInfo = "";
                AddChartInfo(ref astroTable, astroChartInfo1, 1, 0, 1);

                // Kumbam
                strSingleRasiInfo = "";
                if (string.IsNullOrEmpty(RasiDataArray[10]) == false) strSingleRasiInfo = FormatRasiData(RasiDataArray[10], true);
                AddChartCell(ref astroTable, strSingleRasiInfo);

                AddChartInfo(ref astroTable, astroChartUpperPanel, 2, 0, 1);

                // Katakam
                strSingleRasiInfo = "";
                if (string.IsNullOrEmpty(RasiDataArray[3]) == false) strSingleRasiInfo = FormatRasiData(RasiDataArray[3], false);
                AddChartCell(ref astroTable, strSingleRasiInfo);

                // Information Panel 2
                strSingleRasiInfo = "";
                AddChartInfo(ref astroTable, astroChartInfo2, 1, 1, 1);

                // Makaram
                strSingleRasiInfo = "";
                if (string.IsNullOrEmpty(RasiDataArray[9]) == false) strSingleRasiInfo = FormatRasiData(RasiDataArray[9], true);
                AddChartCell(ref astroTable, strSingleRasiInfo);

                AddChartInfo(ref astroTable, astroChartBottomPanel, 2, 1, 0);

                // Simham
                strSingleRasiInfo = "";
                if (string.IsNullOrEmpty(RasiDataArray[4]) == false) strSingleRasiInfo = FormatRasiData(RasiDataArray[4], false);
                AddChartCell(ref astroTable, strSingleRasiInfo);

                // Information Panel 3
                strSingleRasiInfo = "";
                AddChartInfo(ref astroTable, astroChartInfo3, 1, 1, 1);

                // Thanusu
                strSingleRasiInfo = "";
                if (string.IsNullOrEmpty(RasiDataArray[8]) == false) strSingleRasiInfo = FormatRasiData(RasiDataArray[8], true);
                AddChartCell(ref astroTable, strSingleRasiInfo);

                // Viruchikam
                strSingleRasiInfo = "";
                if (string.IsNullOrEmpty(RasiDataArray[7]) == false) strSingleRasiInfo = FormatRasiData(RasiDataArray[7], true);
                AddChartCell(ref astroTable, strSingleRasiInfo);

                // Thulam
                strSingleRasiInfo = "";
                if (string.IsNullOrEmpty(RasiDataArray[6]) == false) strSingleRasiInfo = FormatRasiData(RasiDataArray[6], true);
                AddChartCell(ref astroTable, strSingleRasiInfo);

                // Kanni
                strSingleRasiInfo = "";
                if (string.IsNullOrEmpty(RasiDataArray[5]) == false) strSingleRasiInfo = FormatRasiData(RasiDataArray[5], false);
                AddChartCell(ref astroTable, strSingleRasiInfo);

                // Information Panel 4
                strSingleRasiInfo = "";
                AddChartInfo(ref astroTable, astroChartInfo4, 1, 1, 0);

                pdfDocObj.Add(astroTable);

                pdfDocObj.Add(new Paragraph(""));

                pdfDocObj.Add(astroSignificationTable);

                pdfDocObj.Add(paraSignificationDetails);

                pdfDocObj.Add(new AreaBreak());
                #endregion

                #region Astro, Astpect, Dasa, Ayanamsa Tables
                if (PrintAstroTables == 1)
                {
                    #region Astronomy & Aspect Tables
                    kpCommonObject HObj = GetRawKPAstroCommonObject("1");

                    #region Astronomy Table
                    float[] columnAstronomy = { 3, 4, 5, 5, 5, 5 };
                    iText.Layout.Element.Table astronomyMaster = new iText.Layout.Element.Table(UnitValue.CreatePercentArray(columnAstronomy)).UseAllAvailableWidth();
                    astronomyMaster.SetMarginTop(0);
                    astronomyMaster.SetMarginBottom(0);

                    strTableHeader = string.Format("Astronomy Table");
                    Cell astronomyHeaderCell = new Cell(1, 6).Add(new Paragraph(strTableHeader));
                    astronomyHeaderCell.SetTextAlignment(TextAlignment.CENTER);
                    astronomyHeaderCell.SetVerticalAlignment(VerticalAlignment.MIDDLE);
                    astronomyHeaderCell.SetFontSize(PdfDefaultFontSize);
                    astronomyHeaderCell.SetPadding(5);
                    astronomyHeaderCell.SetHeight(25);
                    astronomyHeaderCell.SetBackgroundColor(colorHeaderBackgroundGreen);
                    astronomyMaster.AddCell(astronomyHeaderCell);

                    astronomyMaster.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHeight(25).Add(new Paragraph("Obj").SetFontSize(PdfDefaultFontSize).SetFont(PdfFontBoldObj)).SetBackgroundColor(colorHeaderBackgroundBrown));
                    astronomyMaster.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHeight(25).Add(new Paragraph("Sign").SetFontSize(PdfDefaultFontSize).SetFont(PdfFontBoldObj)).SetBackgroundColor(colorHeaderBackgroundBrown));
                    astronomyMaster.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHeight(25).Add(new Paragraph("S.Lon").SetFontSize(PdfDefaultFontSize).SetFont(PdfFontBoldObj)).SetBackgroundColor(colorHeaderBackgroundBrown));
                    astronomyMaster.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHeight(25).Add(new Paragraph("Z.Lon").SetFontSize(PdfDefaultFontSize).SetFont(PdfFontBoldObj)).SetBackgroundColor(colorHeaderBackgroundBrown));
                    astronomyMaster.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHeight(25).Add(new Paragraph("Z.Lat").SetFontSize(PdfDefaultFontSize).SetFont(PdfFontBoldObj)).SetBackgroundColor(colorHeaderBackgroundBrown));
                    astronomyMaster.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHeight(25).Add(new Paragraph("Decl.").SetFontSize(PdfDefaultFontSize).SetFont(PdfFontBoldObj)).SetBackgroundColor(colorHeaderBackgroundBrown));

                    if (HObj != null)
                    {
                        astronomyMaster.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHeight(25).Add(new Paragraph(HObj.Name).SetFontSize(PdfDefaultFontSize).SetFont(PdfFontBoldObj).SetFontColor(colorPlanet)));
                        astronomyMaster.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHeight(25).Add(new Paragraph(HObj.Sign).SetFontSize(PdfDefaultFontSize).SetFont(PdfFontObj)).SetPaddingLeft(5));
                        astronomyMaster.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHeight(25).Add(new Paragraph(HObj.SLontitude).SetFontSize(PdfDefaultFontSize).SetFont(PdfFontObj)));
                        astronomyMaster.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHeight(25).Add(new Paragraph(HObj.ZLongitude).SetFontSize(PdfDefaultFontSize).SetFont(PdfFontObj)));
                        astronomyMaster.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHeight(25).Add(new Paragraph(HObj.ZLatitude).SetFontSize(PdfDefaultFontSize).SetFont(PdfFontObj)));
                        astronomyMaster.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHeight(25).Add(new Paragraph(HObj.Declination).SetFontSize(PdfDefaultFontSize).SetFont(PdfFontObj)));
                    }

                    foreach (string strPlanet in PlanetTable)
                    {
                        kpCommonObject PObj = GetRawKPAstroCommonObject(strPlanet);

                        if (PObj != null)
                        {
                            if (PObj.PlanetsInStars == 0)
                            {
                                astronomyMaster.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHeight(25).Add(new Paragraph(PObj.Name + "*").SetFontSize(PdfDefaultFontSize).SetFont(PdfFontBoldObj).SetFontColor(colorNoPlanetInStars)));
                            }
                            else
                            {
                                astronomyMaster.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHeight(25).Add(new Paragraph(PObj.Name).SetFontSize(PdfDefaultFontSize).SetFont(PdfFontBoldObj).SetFontColor(colorPlanet)));
                            }

                            astronomyMaster.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHeight(25).Add(new Paragraph(PObj.Sign).SetFontSize(PdfDefaultFontSize).SetFont(PdfFontObj)).SetPaddingLeft(5));
                            astronomyMaster.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHeight(25).Add(new Paragraph(PObj.SLontitude).SetFontSize(PdfDefaultFontSize).SetFont(PdfFontObj)));
                            astronomyMaster.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHeight(25).Add(new Paragraph(PObj.ZLongitude).SetFontSize(PdfDefaultFontSize).SetFont(PdfFontObj)));
                            astronomyMaster.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHeight(25).Add(new Paragraph(PObj.ZLatitude).SetFontSize(PdfDefaultFontSize).SetFont(PdfFontObj)));
                            astronomyMaster.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHeight(25).Add(new Paragraph(PObj.Declination).SetFontSize(PdfDefaultFontSize).SetFont(PdfFontObj)));
                        }

                        PObj = null;
                    }

                    pdfDocObj.Add(astronomyMaster);
                    #endregion

                    #region Aspects Table
                    float[] columnAspects = { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 };
                    iText.Layout.Element.Table aspectMaster = new iText.Layout.Element.Table(UnitValue.CreatePercentArray(columnAspects)).UseAllAvailableWidth();
                    aspectMaster.SetMarginTop(0);
                    aspectMaster.SetMarginBottom(0);

                    pdfDocObj.Add(new Paragraph(" "));

                    strTableHeader = string.Format("Aspects Table (Degrees)");
                    Cell aspectHeaderCell = new Cell(1, 10).Add(new Paragraph(strTableHeader));
                    aspectHeaderCell.SetTextAlignment(TextAlignment.CENTER);
                    aspectHeaderCell.SetVerticalAlignment(VerticalAlignment.MIDDLE);
                    aspectHeaderCell.SetFontSize(PdfDefaultFontSize);
                    aspectHeaderCell.SetPadding(5);
                    aspectHeaderCell.SetHeight(25);
                    aspectHeaderCell.SetBackgroundColor(colorHeaderBackgroundGreen);
                    aspectMaster.AddCell(aspectHeaderCell);

                    aspectMaster.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHeight(25).Add(new Paragraph("Obj").SetFont(PdfFontBoldObj).SetFontSize(PdfDefaultFontSize)).SetBackgroundColor(colorHeaderBackgroundBrown));
                    aspectMaster.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHeight(25).Add(new Paragraph("Su").SetFont(PdfFontBoldObj).SetFontSize(PdfDefaultFontSize)).SetBackgroundColor(colorHeaderBackgroundBrown));
                    aspectMaster.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHeight(25).Add(new Paragraph("Mo").SetFont(PdfFontBoldObj).SetFontSize(PdfDefaultFontSize)).SetBackgroundColor(colorHeaderBackgroundBrown));
                    aspectMaster.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHeight(25).Add(new Paragraph("Ma").SetFont(PdfFontBoldObj).SetFontSize(PdfDefaultFontSize)).SetBackgroundColor(colorHeaderBackgroundBrown));
                    aspectMaster.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHeight(25).Add(new Paragraph("Me").SetFont(PdfFontBoldObj).SetFontSize(PdfDefaultFontSize)).SetBackgroundColor(colorHeaderBackgroundBrown));
                    aspectMaster.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHeight(25).Add(new Paragraph("Ju").SetFont(PdfFontBoldObj).SetFontSize(PdfDefaultFontSize)).SetBackgroundColor(colorHeaderBackgroundBrown));
                    aspectMaster.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHeight(25).Add(new Paragraph("Ve").SetFont(PdfFontBoldObj).SetFontSize(PdfDefaultFontSize)).SetBackgroundColor(colorHeaderBackgroundBrown));
                    aspectMaster.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHeight(25).Add(new Paragraph("Sa").SetFont(PdfFontBoldObj).SetFontSize(PdfDefaultFontSize)).SetBackgroundColor(colorHeaderBackgroundBrown));
                    aspectMaster.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHeight(25).Add(new Paragraph("Ra").SetFont(PdfFontBoldObj).SetFontSize(PdfDefaultFontSize)).SetBackgroundColor(colorHeaderBackgroundBrown));
                    aspectMaster.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHeight(25).Add(new Paragraph("Ke").SetFont(PdfFontBoldObj).SetFontSize(PdfDefaultFontSize)).SetBackgroundColor(colorHeaderBackgroundBrown));

                    if (HObj != null)
                    {
                        aspectMaster.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHeight(25).Add(new Paragraph(HObj.Name).SetFont(PdfFontBoldObj).SetFontSize(PdfDefaultFontSize).SetFontColor(colorPlanet)));

                        ProcessComparison(HObj, ref aspectMaster);
                    }

                    foreach (string strPlanet in PlanetTable)
                    {
                        kpCommonObject PObj = GetRawKPAstroCommonObject(strPlanet);

                        if (PObj != null)
                        {
                            if (PObj.PlanetsInStars == 0)
                            {
                                aspectMaster.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHeight(25).Add(new Paragraph(PObj.Name + "*").SetFont(PdfFontBoldObj).SetFontSize(PdfDefaultFontSize).SetFontColor(colorNoPlanetInStars)));
                            }
                            else
                            {
                                aspectMaster.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHeight(25).Add(new Paragraph(PObj.Name).SetFont(PdfFontBoldObj).SetFontSize(PdfDefaultFontSize).SetFontColor(colorPlanet)));
                            }

                            ProcessComparison(PObj, ref aspectMaster);
                        }

                        PObj = null;
                    }

                    pdfDocObj.Add(aspectMaster);
                    #endregion

                    HObj = null;

                    pdfDocObj.Add(new AreaBreak());
                    #endregion
                }

                if (PrintDasaTables == 1)
                {
                    #region Dasa Table Construction
                    int SimpleDasaSystem = 0;
                    if (SimpleDasaSystem == 1)
                    {
                        #region Simple Dasa System
                        int nDasaTableColumns = 4;
                        Table dasaTable = new Table(new float[nDasaTableColumns]).UseAllAvailableWidth();
                        dasaTable.SetMarginTop(0);
                        dasaTable.SetMarginBottom(0);

                        strTableHeader = string.Format("Vimsottari Dasa");
                        Cell dsCell1 = new Cell(1, nDasaTableColumns).Add(new Paragraph(strTableHeader));
                        dsCell1.SetTextAlignment(TextAlignment.CENTER);
                        dsCell1.SetVerticalAlignment(VerticalAlignment.MIDDLE);
                        dsCell1.SetPadding(5);
                        dsCell1.SetBackgroundColor(colorHeaderBackgroundGreen);
                        dasaTable.AddCell(dsCell1);

                        #region Dasa Table
                        AddHeaderCell(ref dasaTable, "Dasa Lord");
                        AddHeaderCell(ref dasaTable, "Bukthi Lord");
                        AddHeaderCell(ref dasaTable, "Start Date");
                        AddHeaderCell(ref dasaTable, "End Date");

                        foreach (object dasaEntry in dasaObj.DasaChart)
                        {
                            astroDasaPeriod dasaPeriod = (astroDasaPeriod)dasaEntry;

                            strDasaLord = "";
                            strDasaLord += dasaPeriod.DasaLord[0];
                            strDasaLord += dasaPeriod.DasaLord.Substring(1).ToLower();

                            strBukthiLord = "";
                            strBukthiLord += dasaPeriod.BukthiLord[0];
                            strBukthiLord += dasaPeriod.BukthiLord.Substring(1).ToLower();

                            AddDataCell(ref dasaTable, strDasaLord, 1);
                            AddDataCell(ref dasaTable, strBukthiLord, 1);
                            AddDataCell(ref dasaTable, dasaPeriod.StartDate, 1);
                            AddDataCell(ref dasaTable, dasaPeriod.EndDate, 1);
                        }
                        #endregion

                        pdfDocObj.Add(dasaTable);
                        #endregion
                    }
                    else
                    {
                        vmDasaChart vmObj = new vmDasaChart();
                        vmObj.CreateVMDasaChart(ref pdfDocObj, dtDasaStartDate, VMDasaLord, ref strErrorMessage);
                    }

                    pdfDocObj.Add(new AreaBreak());
                    #endregion
                }
                    
                if (PrintAyanamsaCalculation == 1)
                {
                    #region Ayanamsa Calculation
                    pdfDocObj.Add(new Paragraph(strAyanamsaCalculationDetails));

                    pdfDocObj.Add(new AreaBreak());
                    #endregion
                }
                #endregion
                #endregion

                return true;
            }
            catch (Exception ex)
            {
                strErrorMessage = ex.Message;
            }
            finally
            {
                SEObj = null;
                dasaObj = null;
            }

            return false;
        }
        #endregion
    }
}