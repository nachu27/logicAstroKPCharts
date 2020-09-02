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
using System.Collections.ObjectModel;
using System.Reflection;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace srlWebCom.Astro.AstroObjects
{
    public static class ExtensionMethods
    {
        public static T DeepClone<T>(this T a)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, a);
                stream.Position = 0;
                return (T)formatter.Deserialize(stream);
            }
        }
    }

    public static class astroGlobal
    {
        public static string AstroSourcePath = "";
    }

    [Serializable]
    public class astroPosition
    {
        #region Private Variables
        private int _TotalSeconds = 0;
        private double _DecimalValue = 0;
        private string _StringPosition = "";
        private int _DegHours = 0;
        private int _Minutes = 0;
        private int _Seconds = 0;
        #endregion

        #region Public Properties
        public int DegHours
        {
            set { _DegHours = value; }
            get { return _DegHours; }
        }

        public int Minutes
        {
            set { _Minutes = value; }
            get { return _Minutes; }
        }

        public int Seconds
        {
            set { _Seconds = value; }
            get { return _Seconds; }
        }

        public int TotalSeconds
        {
            get { return _TotalSeconds; }
        }

        public double DecimalValue
        {
            get { return _DecimalValue; }
        }

        public string Value
        {
            get { return string.Format("{0}.{1:00}.{2:00}", _DegHours, _Minutes, _Seconds); }
        }

        public string ShortValue
        {
            get { return string.Format("{0}.{1:00}", _DegHours, _Minutes); }
        }
        #endregion

        #region Private Methods
        private void CalculateValues()
        {
            _TotalSeconds = (_DegHours * 3600) + (_Minutes * 60) + _Seconds;

            _DecimalValue = (double)_DegHours + ((double)_Minutes / (double)60) + ((double)_Seconds / (double)3600);
        }

        private void ParseTotalSeconds()
        {
            int nMinutesRemaining = 0;
            int nSecondsRemaining = 0;

            nMinutesRemaining = _TotalSeconds % 3600;
            _DegHours = (_TotalSeconds - nMinutesRemaining) / 3600;

            nSecondsRemaining = nMinutesRemaining % 60;
            _Minutes = (nMinutesRemaining - nSecondsRemaining) / 60;

            _Seconds = nSecondsRemaining;
        }

        private void ParseDecimalValue()
        {
            int TempFraction = 0;

            _DegHours = (int)Math.Truncate(_DecimalValue);

            double minFraction = (_DecimalValue - _DegHours) * 60;
            TempFraction = (int)Math.Truncate(minFraction);
            _Minutes = (int)Math.Round(minFraction);

            double secFraction = (minFraction - TempFraction) * 60;
            _Seconds = (int)Math.Round(secFraction);

            CalculateValues();
        }

        private void ParseStringPosition()
        {
            string[] posElements = _StringPosition.Split('.');

            if (posElements.Length != 3)
            {
                throw new Exception("invalid position format");
            }

            _DegHours = Convert.ToInt32(posElements[0]);
            _Minutes = Convert.ToInt32(posElements[1]);
            _Seconds = Convert.ToInt32(posElements[2]);

            CalculateValues();
        }
        #endregion

        #region Public Static Methods
        public static string GetDegHoursMinutesSeconds(double decimalDegHours, double addDegHours)
        {
            double DegHours = Math.Floor(decimalDegHours);

            if (addDegHours > 0)
            {
                DegHours = DegHours + addDegHours;
                if (DegHours > 360)
                {
                    DegHours = DegHours - 360;
                }
            }

            double minutes = (decimalDegHours - Math.Floor(decimalDegHours)) * 60.0;
            double seconds = (minutes - Math.Floor(minutes)) * 60.0;

            return string.Format("{0:000}.{1:00}.{2:00}", DegHours, Math.Floor(minutes), Math.Round(seconds));
        }
        #endregion

        #region Public Methods
        public astroPosition()
        {
            Clear();
        }

        public astroPosition(int DegHours, int Minutes, int Seconds)
        {
            Set(DegHours, Minutes, Seconds);
        }

        public astroPosition(double dblPosition)
        {
            Set(dblPosition);
        }

        public astroPosition(string strPosition)
        {
            Set(strPosition);
        }

        public void Clear()
        {
            _TotalSeconds = 0;
            _DecimalValue = 0;
            _StringPosition = "";
            _DegHours = 0;
            _Minutes = 0;
            _Seconds = 0;
        }

        public void Set(int DegHours, int Minutes, int Seconds)
        {
            Clear();

            _DegHours = DegHours;
            _Minutes = Minutes;
            _Seconds = Seconds;

            CalculateValues();
        }

        public void Set(double dblPosition)
        {
            Clear();

            _DecimalValue = dblPosition;

            ParseDecimalValue();
        }

        public void Set(string strPosition)
        {
            Clear();

            _StringPosition = strPosition;

            ParseStringPosition();
        }

        public void Add(int DegHours, int Minutes, int Seconds)
        {
            int nAddBasicSeconds = 0;

            nAddBasicSeconds = (DegHours * 3600) + (Minutes * 60) + Seconds;

            _TotalSeconds += nAddBasicSeconds;

            if (_TotalSeconds >= 1296000)
            {
                _TotalSeconds -= 1296000;
            }

            ParseTotalSeconds();

            CalculateValues();
        }

        public void Sub(int DegHours, int Minutes, int Seconds)
        {
            int nSubBasicSeconds = 0;

            nSubBasicSeconds = (DegHours * 3600) + (Minutes * 60) + Seconds;

            if (_TotalSeconds < nSubBasicSeconds)
            {
                _TotalSeconds += 1296000;
            }

            _TotalSeconds -= nSubBasicSeconds;

            ParseTotalSeconds();

            CalculateValues();
        }
        #endregion
    }

    public static class astroPlanets
    {
        public static int SUN = 0;
        public static int MOON = 1;
        public static int MERCURY = 2;
        public static int VENUS = 3;
        public static int MARS = 4;
        public static int JUPITER = 5;
        public static int SATURN = 6;
        public static int URANUS = 7;
        public static int NEPTUNE = 8;
        public static int PLUTO = 9;
        public static int MEAN_NODE = 10;

        public static int[] AllPlanets = new[] { 0, 1, 4, 2, 5, 3, 6, 10 };
        public static string[] PlanetNames = new[] { "Sun", "Moon", "Mars", "Mercury", "Jupiter", "Venus", "Saturn", "RAHU" };
        public static string[] PlanetNamesTamil = new[] { "Soorian", "Chandran", "Sevvai", "Budhan", "Guru", "Sukkiran", "Sani", "RAHU" };
        public static string[] PlanetNamesTamilShort = new[] { "Soo", "Ch", "Se", "Bu", "Gu", "Su", "Sa", "Ra" };

        public static int GetDasaYears(string strPlanet)
        {
            int nDasaYears = 0;

            switch (strPlanet.ToUpper())
            {
                case "SUN":
                    nDasaYears = 6;
                    break;
                case "MOON":
                    nDasaYears = 10;
                    break;
                case "MARS":
                    nDasaYears = 7;
                    break;
                case "RAHU":
                    nDasaYears = 18;
                    break;
                case "JUPITER":
                    nDasaYears = 16;
                    break;
                case "SATURN":
                    nDasaYears = 19;
                    break;
                case "MERCURY":
                    nDasaYears = 17;
                    break;
                case "KETHU":
                    nDasaYears = 7;
                    break;
                case "VENUS":
                    nDasaYears = 20;
                    break;
                default:
                    nDasaYears = 0;
                    break;
            }

            return nDasaYears;
        }

        public static string GetTamilPlanetName(string strPlanet)
        {
            string strTamilPlanetName = "";

            switch (strPlanet.ToUpper())
            {
                case "SUN":
                    strTamilPlanetName = "Soorian";
                    break;
                case "MOON":
                    strTamilPlanetName = "Chandran";
                    break;
                case "MARS":
                    strTamilPlanetName = "Sevvai";
                    break;
                case "RAHU":
                    strTamilPlanetName = "Rahu";
                    break;
                case "JUPITER":
                    strTamilPlanetName = "Guru";
                    break;
                case "SATURN":
                    strTamilPlanetName = "Sani";
                    break;
                case "MERCURY":
                    strTamilPlanetName = "Budhan";
                    break;
                case "KETHU":
                    strTamilPlanetName = "Kethu";
                    break;
                case "VENUS":
                    strTamilPlanetName = "Sukkiran";
                    break;
                default:
                    strTamilPlanetName = "";
                    break;
            }

            return strTamilPlanetName;
        }
    }

    [Serializable]
    public class astroObject : IComparable
    {
        public string OName = "";
        public string OType = "";
        public string OShortName = "";
        public string OSign = "";
        public string OSignEnglish = "";
        public string OSignTamil = "";
        public int OSignID = 0;
        public int OHouseID = 0;
        public astroPosition OSignLongitude = new astroPosition();
        public astroPosition OZodiacLongitude = new astroPosition();
        public astroPosition OZodiacLatitude = new astroPosition();
        public string OZodiacLatitudeDirection = " ";
        public astroPosition ODeclination = new astroPosition();
        public string ODeclinationDirection = " ";
        public double ODistance = 0.0;
        public astroPosition OSpeedPerDay = new astroPosition();
        public string OSpeedPerDayDirection = " ";
        public string SignLord = "";
        public string StarLordList = "";
        public string StarLord = "";
        public string StarSubLord = "";
        public string StarSubSubLord = "";
        
        public void Clear()
        {
            OName = "";
            OType = "";
            OShortName = "";
            OSign = "";
            OSignEnglish = "";
            OSignTamil = "";
            OSignID = 0;
            OHouseID = 0;
            OSignLongitude.Clear();
            OZodiacLongitude.Clear();
            OZodiacLatitude.Clear();
            OZodiacLatitudeDirection = "";
            ODeclination.Clear();
            ODistance = 0.0;
            OSpeedPerDay.Clear();
            OSpeedPerDayDirection = "";
            SignLord = "";
            StarLordList = "";
            StarLord = "";
            StarSubLord = "";
            StarSubSubLord = "";
    }

        public void SetAstroNames()
        {
            if (OZodiacLongitude.DegHours >= 0 && OZodiacLongitude.DegHours < 30) { OSignEnglish = "Aries"; OSignTamil = "Mesham"; OSignID = 1; SignLord = "Ma"; }
            if (OZodiacLongitude.DegHours >= 30 && OZodiacLongitude.DegHours < 60) { OSignEnglish = "Taurus"; OSignTamil = "Rishabam"; OSignID = 2; SignLord = "Ve"; }
            if (OZodiacLongitude.DegHours >= 60 && OZodiacLongitude.DegHours < 90) { OSignEnglish = "Gemini"; OSignTamil = "Mithunam"; OSignID = 3; SignLord = "Me"; }
            if (OZodiacLongitude.DegHours >= 90 && OZodiacLongitude.DegHours < 120) { OSignEnglish = "Cancer"; OSignTamil = "Katakam"; OSignID = 4; SignLord = "Mo"; }
            if (OZodiacLongitude.DegHours >= 120 && OZodiacLongitude.DegHours < 150) { OSignEnglish = "Leo"; OSignTamil = "Simham"; OSignID = 5; SignLord = "Su"; }
            if (OZodiacLongitude.DegHours >= 150 && OZodiacLongitude.DegHours < 180) { OSignEnglish = "Virgo"; OSignTamil = "Kanni"; OSignID = 6; SignLord = "Me"; }
            if (OZodiacLongitude.DegHours >= 180 && OZodiacLongitude.DegHours < 210) { OSignEnglish = "Libra"; OSignTamil = "Thulam"; OSignID = 7; SignLord = "Ve"; }
            if (OZodiacLongitude.DegHours >= 210 && OZodiacLongitude.DegHours < 240) { OSignEnglish = "Scorpio"; OSignTamil = "Viruchikam"; OSignID = 8; SignLord = "Ma"; }
            if (OZodiacLongitude.DegHours >= 240 && OZodiacLongitude.DegHours < 270) { OSignEnglish = "Sagittarius"; OSignTamil = "Dhanusu"; OSignID = 9; SignLord = "Ju"; }
            if (OZodiacLongitude.DegHours >= 270 && OZodiacLongitude.DegHours < 300) { OSignEnglish = "Capricorn"; OSignTamil = "Makaram"; OSignID = 10; SignLord = "Sa"; }
            if (OZodiacLongitude.DegHours >= 300 && OZodiacLongitude.DegHours < 330) { OSignEnglish = "Aquarius"; OSignTamil = "Kumbam"; OSignID = 11; SignLord = "Sa"; }
            if (OZodiacLongitude.DegHours >= 330 && OZodiacLongitude.DegHours < 360) { OSignEnglish = "Pisces"; OSignTamil = "Meenam"; OSignID = 12; SignLord = "Ju"; }

            if (OName == "SUN") { OShortName = "Su"; }
            if (OName == "MOON") { OShortName = "Mo"; }
            if (OName == "MARS") { OShortName = "Ma"; }
            if (OName == "MERCURY") { OShortName = "Me"; }
            if (OName == "JUPITER") { OShortName = "Ju"; }
            if (OName == "VENUS") { OShortName = "Ve"; }
            if (OName == "SATURN") { OShortName = "Sa"; }
            if (OName == "RAHU") { OShortName = "Ra"; }
            if (OName == "KETHU") { OShortName = "Ke"; }
            if (OName == "HOUSE  1") { OShortName = "1 "; OHouseID = 1; }
            if (OName == "HOUSE  2") { OShortName = "2 "; OHouseID = 2; }
            if (OName == "HOUSE  3") { OShortName = "3 "; OHouseID = 3; }
            if (OName == "HOUSE  4") { OShortName = "4 "; OHouseID = 4; }
            if (OName == "HOUSE  5") { OShortName = "5 "; OHouseID = 5; }
            if (OName == "HOUSE  6") { OShortName = "6 "; OHouseID = 6; }
            if (OName == "HOUSE  7") { OShortName = "7 "; OHouseID = 7; }
            if (OName == "HOUSE  8") { OShortName = "8 "; OHouseID = 8; }
            if (OName == "HOUSE  9") { OShortName = "9 "; OHouseID = 9; }
            if (OName == "HOUSE 10") { OShortName = "10"; OHouseID = 10; }
            if (OName == "HOUSE 11") { OShortName = "11"; OHouseID = 11; }
            if (OName == "HOUSE 12") { OShortName = "12"; OHouseID = 12; }
        }

        public int CompareTo(Object obj)
        {
            if (obj is astroObject)
            {
                return this.OZodiacLongitude.TotalSeconds.CompareTo((obj as astroObject).OZodiacLongitude.TotalSeconds);
            }
            return 0;
        }

        public void ApplyAyanamsa(astroPosition ayPos)
        {
            try
            {
                // Subtract the ayanamsa position from the Zodiac Longitude
                OZodiacLongitude.Sub(ayPos.DegHours, ayPos.Minutes, ayPos.Seconds);

                SetAstroNames();

                // Reset the sign longitudes
                OSignLongitude.Set(OZodiacLongitude.DegHours, OZodiacLongitude.Minutes, OZodiacLongitude.Seconds);
                OSignLongitude.Sub((OSignID-1) * 30, 0, 0);
            }
            catch
            {
                throw new Exception("Unable to apply IAU2006 Ayanamsa on object : " + OName);
            }
        }

        public void SetStarLords(string strStarLords)
        {
            StarLordList = strStarLords;

            string[] strLords = strStarLords.Split('/');

            if( strLords.Length > 0)
            {
                StarLord = strLords[0];
            }
            if (strLords.Length > 1)
            {
                StarSubLord = strLords[1];
            }
            if (strLords.Length > 2)
            {
                StarSubSubLord = strLords[2];
            }
        }
    }

    public class astroStar
    {
        public int PositionInRasi = 0;
        public int PositionInStar = 0;
        public string StarLord = "";
        public string Star = "";
        public int StartDegrees = 0;
        public int StartMinutes = 0;
        public int EndDegrees = 0;
        public int EndMinutes = 0;
        public string Rasi = "";
        public string Navamsam = "";

        public static ArrayList AstroStarsList = new ArrayList();

        public static bool LoadStars(ref string strErrorMessage)
        {
            AstroStarsList.Clear();
            strErrorMessage = "";
            string strDegreePositionsData = "";

            try
            {
                Assembly _assembly = Assembly.GetExecutingAssembly();
                string ResourcePath = string.Format("{0}.Degree-Positions.txt", astroGlobal.AstroSourcePath);
                StreamReader _textStreamReader = new StreamReader(_assembly.GetManifestResourceStream(ResourcePath));
                strDegreePositionsData = _textStreamReader.ReadToEnd();
                _textStreamReader.Close();

                String DegreePositionData = strDegreePositionsData;
                DegreePositionData = DegreePositionData.Replace("\r\n", "\n");
                string[] DPElements = DegreePositionData.Split('\n');
                foreach (string DPElement in DPElements)
                {
                    string[] StarElements = DPElement.Split('|');

                    if (StarElements.Length == 8)
                    {
                        astroStar AS = new astroStar();

                        AS.PositionInRasi = Convert.ToInt32(StarElements[0]);
                        AS.PositionInStar = Convert.ToInt32(StarElements[1]);
                        AS.StarLord = StarElements[2];
                        AS.Star = StarElements[3];

                        string[] startDegreeElements = StarElements[4].Split('.');
                        AS.StartDegrees = Convert.ToInt32(startDegreeElements[0]);
                        AS.StartMinutes = Convert.ToInt32(startDegreeElements[1]);

                        string[] endDegreeElements = StarElements[5].Split('.');
                        AS.EndDegrees = Convert.ToInt32(endDegreeElements[0]);
                        AS.EndMinutes = Convert.ToInt32(endDegreeElements[1]);

                        AS.Rasi = StarElements[6];
                        AS.Navamsam = StarElements[7];

                        AstroStarsList.Add(AS);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                strErrorMessage = ex.Message;
            }

            return false;
        }

        public static astroStar GetStarForPosition(int nDegrees, int nMinutes)
        {
            astroStar ASO = null;

            foreach (astroStar objStar in AstroStarsList)
            {
                if (nDegrees < objStar.EndDegrees || (nDegrees == objStar.EndDegrees && nMinutes < objStar.EndMinutes))
                {
                    if (nDegrees > objStar.StartDegrees || (nDegrees == objStar.StartDegrees && nMinutes >= objStar.StartMinutes))
                    {
                        ASO = objStar;
                    }
                }
            }

            return ASO;
        }

        public static bool GetStarEndingPosition(string strStar, ref int nDegrees, ref int nMinutes)
        {
            nDegrees = 0;
            nMinutes = 0;

            foreach (astroStar objStar in AstroStarsList)
            {
                if (objStar.Star.ToUpper() == strStar.ToUpper() && objStar.PositionInStar == 4)
                {
                    nDegrees = objStar.EndDegrees;
                    nMinutes = objStar.EndMinutes;
                    return true;
                }
            }

            return false;
        }
    }

    class astroDasaElement
    {
        public string DLord = "";
        public string BLoad = "";
        public int Years = 0;
        public int Months = 0;
        public int Days = 0;

        public astroDasaElement(string dLord, string bLord, int years, int months, int days)
        {
            DLord = dLord;
            BLoad = bLord;
            Years = years;
            Months = months;
            Days = days;
        }
    }

    public class astroDasaPeriod
    {
        public string DasaLord = "";
        public string BukthiLord = "";
        public string StartDate = "";
        public string EndDate = "";
        public bool CurrentPeriod = false;

        public astroDasaPeriod(string strDasaLord, string strBukthiLord, string strStartDate, string strEndDate, bool bCurrentPeriod)
        {
            DasaLord = strDasaLord;
            BukthiLord = strBukthiLord;
            StartDate = strStartDate;
            EndDate = strEndDate;
            CurrentPeriod = bCurrentPeriod;
        }
    }

    public class astroDasa
    {
        #region Private Variables
        private ArrayList _dasaArray = new ArrayList();
        private string[] _dasaLords = { "KETHU", "VENUS", "SUN", "MOON", "MARS", "RAHU", "JUPITER", "SATURN", "MERCURY" };
        private int[] _dasaYears = { 7, 20, 6, 10, 7, 18, 16, 19, 17 };
        #endregion

        #region Public Variables
        public ArrayList DasaChart = new ArrayList();
        #endregion

        #region Public Methods
        public bool LoadDasaChart(ref string strErrorMessage)
        {
            int nLineIndex = 9;
            string DLord = "";
            string BLord = "";
            int Years = 0;
            int Months = 0;
            int Days = 0;
            string strDasaPeriodData = "";

            strErrorMessage = "";

            try
            {
                Assembly _assembly = Assembly.GetExecutingAssembly();
                string ResourcePath = string.Format("{0}.Dasa-Chart.txt", astroGlobal.AstroSourcePath);
                StreamReader _textStreamReader = new StreamReader(_assembly.GetManifestResourceStream(ResourcePath));
                strDasaPeriodData = _textStreamReader.ReadToEnd();
                _textStreamReader.Close();

                String DasaPeriodData = strDasaPeriodData;
                DasaPeriodData = DasaPeriodData.Replace("\r\n", "\n");
                string[] DPElements = DasaPeriodData.Split('\n');
                foreach (string DPElement in DPElements)
                {
                    string[] dasaElements = DPElement.Split('|');

                    if (dasaElements.Length == 1)
                    {
                        if (dasaElements[0].Trim() == "") break;
                    }

                    if (dasaElements.Length != 4)
                    {
                        throw new Exception("Invalid dasa element entry found");
                    }

                    if (nLineIndex == 9)
                    {
                        DLord = dasaElements[0].ToUpper();
                    }

                    BLord = dasaElements[0].ToUpper();
                    Years = Convert.ToInt32(dasaElements[1]);
                    Months = Convert.ToInt32(dasaElements[2]);
                    Days = Convert.ToInt32(dasaElements[3]);

                    _dasaArray.Add(new astroDasaElement(DLord, BLord, Years, Months, Days));

                    nLineIndex--;

                    if (nLineIndex == 0)
                    {
                        nLineIndex = 9;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                strErrorMessage = ex.Message;
            }

            return false;
        }

        public void PrintDasaTemplate()
        {
            foreach (astroDasaElement aDE in _dasaArray)
            {
                Console.WriteLine(aDE.DLord + "|" + aDE.BLoad);
            }
        }

        public void GenerateDasaChart(string DOB, string dasaLord, int years, int months, int days)
        {
            DateTime aDate, sDate, eDate, cDate;
            bool bLordFound = false;
            bool bCurrentPeriod = false;
            
            DasaChart.Clear();

            aDate = DateTime.Parse(DOB);

            int nDLIndex = 0;
            for (nDLIndex = 0; nDLIndex < _dasaLords.Length; nDLIndex++)
            {
                if (_dasaLords[nDLIndex] == dasaLord.ToUpper())
                {
                    bLordFound = true;
                    break;
                }
            }

            if (!bLordFound)
            {
                return;
            }

            sDate = aDate;

            // Go to the end of the dasa
            sDate = sDate.AddYears(years);
            sDate = sDate.AddMonths(months);
            sDate = sDate.AddDays(days);

            // Reset back to the beginning of the Dasa
            sDate = sDate.AddYears(_dasaYears[nDLIndex] * -1);

            eDate = sDate;

            cDate = DateTime.Now;

            for (int i = 0; i < 9; i++)
            {
                foreach (astroDasaElement aDE in _dasaArray)
                {
                    if (aDE.DLord == _dasaLords[nDLIndex])
                    {
                        eDate = eDate.AddYears(aDE.Years);
                        eDate = eDate.AddMonths(aDE.Months);
                        eDate = eDate.AddDays(aDE.Days);

                        bCurrentPeriod = false;

                        if (cDate > sDate && cDate < eDate)
                        {
                            bCurrentPeriod = true;
                        }

                        DasaChart.Add(new astroDasaPeriod(aDE.DLord, aDE.BLoad, sDate.ToString("dd-MM-yyyy"), eDate.ToString("dd-MM-yyyy"), bCurrentPeriod));

                        sDate = eDate;
                    }
                }

                nDLIndex++;
                if (nDLIndex == _dasaLords.Length)
                {
                    nDLIndex = 0;
                }
            }
        }
        #endregion
    }

    public class kpStarObject
    {
        public string Horay = "";
        public string Sign = "";
        public string Lords = "";
        public astroPosition FromPos = new astroPosition();
        public astroPosition ToPos = new astroPosition();

        public void Clear()
        {
            Horay = "";
            Sign = "";
            Lords = "";
            FromPos.Clear();
            ToPos.Clear();
        }
    }

    public class astroHoraAscendant
    {
        public int ZDegHours = 0;
        public int ZMinutes = 0;
        public int ZSeconds = 0;
        public int SDegHours = 0;
        public int SMinutes = 0;
        public int SSeconds = 0;
        public string SignEnglish = "Aries";
        public string StarLord = "Ke";
        public string StarSubLord = "Ke";
        public string StarSubSubLord = "Ke";

        public void Clear()
        {
            ZDegHours = 0;
            ZMinutes = 0;
            ZSeconds = 0;
            SDegHours = 0;
            SMinutes = 0;
            SSeconds = 0;
            SignEnglish = "Aries";
            StarLord = "Ke";
            StarSubLord = "Ke";
            StarSubSubLord = "Ke";
        }
    }

    public class kpAstroObject
    {
        public static bool StarTableLoaded = false;

        public ArrayList KPStarTable = new ArrayList();

        public bool LoadKPStarSubLordsTable()
        {
            try
            {
                StarTableLoaded = false;

                Assembly _assembly = Assembly.GetExecutingAssembly();
                string ResourcePath = string.Format("{0}.KP-Star-Sub-Lords.txt", astroGlobal.AstroSourcePath);
                StreamReader _textStreamReader = new StreamReader(_assembly.GetManifestResourceStream(ResourcePath));
                string strFileLine = "";
                string strPosition = "";

                while (_textStreamReader.EndOfStream == false)
                {
                    strFileLine = _textStreamReader.ReadLine();

                    string[] KPElements = strFileLine.Split('|');
                    if (KPElements.Length != 5)
                    {
                        Console.WriteLine("Error: Invalid KP Start Lord entry found -> {0}", strFileLine);
                        _textStreamReader.Close();
                        return false;
                    }

                    kpStarObject KPSObj = new kpStarObject();
                    KPSObj.Horay = KPElements[0];
                    KPSObj.Sign = KPElements[1];
                    KPSObj.Lords = KPElements[2];
                    strPosition = KPElements[3].Replace(":", ".").Substring(0, 8);
                    KPSObj.FromPos.Set(strPosition);
                    strPosition = KPElements[4].Replace(":", ".").Substring(0, 8);
                    KPSObj.ToPos.Set(strPosition);

                    KPStarTable.Add(KPSObj);
                }

                _textStreamReader.Close();

                StarTableLoaded = true;

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public string GetKPStarSubLords(string strSign, astroPosition asPos, int nType)
        {
            string strLords = "";
            string strShortSign = "";

            try
            {
                strShortSign = strSign.Substring(0, 3);

                foreach (kpStarObject KPObj in KPStarTable)
                {
                    if (KPObj.Sign.IndexOf(strShortSign) != -1)
                    {
                        if (asPos.TotalSeconds >= KPObj.FromPos.TotalSeconds && asPos.TotalSeconds < KPObj.ToPos.TotalSeconds)
                        {
                            strLords = string.Format("{0}", KPObj.Lords);

                            string[] strLordItems = strLords.Split('/');

                            switch (nType)
                            {
                                case 1:
                                    strLords = string.Format("{0}", strLordItems[0]);
                                    break;
                                    
                                case 2:
                                    strLords = string.Format("{0}/{1}", strLordItems[0], strLordItems[1]);
                                    break;

                                default:
                                case 3:
                                    strLords = string.Format("{0}/{1}/{2}", strLordItems[0], strLordItems[1], strLordItems[2]);
                                    break;
                            }

                            break;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return strLords;
        }

        public string GetKPStarSubLordRange(string strSign, astroPosition asPos)
        {
            string strLordsRange = "";
            string strShortSign = "";

            try
            {
                strShortSign = strSign.Substring(0, 3);

                foreach (kpStarObject KPObj in KPStarTable)
                {
                    if (KPObj.Sign.IndexOf(strShortSign) != -1)
                    {
                        if (asPos.TotalSeconds >= KPObj.FromPos.TotalSeconds && asPos.TotalSeconds < KPObj.ToPos.TotalSeconds)
                        {
                            strLordsRange = string.Format("{0} - {1}", KPObj.FromPos.Value, KPObj.ToPos.Value);
                            break;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return strLordsRange.ToUpper();
        }

        public int GetDasaYears(string strLord)
        {
            int nYears = 0;

            switch (strLord.ToUpper())
            {
                case "KE":
                    nYears = 7;
                    break;
                case "VE":
                    nYears = 20;
                    break;
                case "SU":
                    nYears = 6;
                    break;
                case "MO":
                    nYears = 10;
                    break;
                case "MA":
                    nYears = 7;
                    break;
                case "RA":
                    nYears = 18;
                    break;
                case "JU":
                    nYears = 16;
                    break;
                case "SA":
                    nYears = 19;
                    break;
                case "ME":
                    nYears = 17;
                    break;
                default:
                    break;
            }

            return nYears;
        }

        public int GetSignID(string strShortSign)
        {
            int nYears = 0;

            switch (strShortSign.ToUpper())
            {
                default:
                case "ARI":
                    nYears = 0;
                    break;
                case "TAU":
                    nYears = 1;
                    break;
                case "GEM":
                    nYears = 2;
                    break;
                case "CAN":
                    nYears = 3;
                    break;
                case "LEO":
                    nYears = 4;
                    break;
                case "VIR":
                    nYears = 5;
                    break;
                case "LIB":
                    nYears = 6;
                    break;
                case "SCO":
                    nYears = 7;
                    break;
                case "SAG":
                    nYears = 8;
                    break;
                case "CAP":
                    nYears = 9;
                    break;
                case "AQU":
                    nYears = 10;
                    break;
                case "PIS":
                    nYears = 11;
                    break;
            }

            return nYears;
        }

        public string GetSignName(string strShortName)
        {
            string strSignName = "";

            switch (strShortName.ToUpper())
            {
                default:
                case "ARI":
                    strSignName = "Aries";
                    break;
                case "TAU":
                    strSignName = "Taurus";
                    break;
                case "GEM":
                    strSignName = "Gemini";
                    break;
                case "CAN":
                    strSignName = "Cancer";
                    break;
                case "LEO":
                    strSignName = "Leo";
                    break;
                case "VIR":
                    strSignName = "Virgo";
                    break;
                case "LIB":
                    strSignName = "Libra";
                    break;
                case "SCO":
                    strSignName = "Scorpio";
                    break;
                case "SAG":
                    strSignName = "Sagittarius";
                    break;
                case "CAP":
                    strSignName = "Capricorn";
                    break;
                case "AQU":
                    strSignName = "Aquarius";
                    break;
                case "PIS":
                    strSignName = "Pisces";
                    break;
            }

            return strSignName;
        }

        public astroHoraAscendant GetHoraAscendant(string strHoraID)
        {
            astroHoraAscendant horaAscObj = new astroHoraAscendant();
            bool bHoraFound = false;
            int nSignID = 0;
            int nIndex = 0;

            try
            {
                horaAscObj.Clear();

                foreach (kpStarObject KPObj in KPStarTable)
                {
                    string[] strHoraElements = KPObj.Horay.Split('/');
                    string[] strSignNames = KPObj.Sign.Split('/');

                    if (strHoraElements.Length == 3 && strSignNames.Length == 3)
                    {
                        bHoraFound = false;
                        nSignID = 0;

                        for(nIndex=0; nIndex<3; nIndex++)
                        {
                            if (strHoraElements[nIndex] == strHoraID)
                            {
                                horaAscObj.SDegHours = KPObj.FromPos.DegHours;
                                horaAscObj.SMinutes = KPObj.FromPos.Minutes;
                                horaAscObj.SSeconds = KPObj.FromPos.Seconds;

                                nSignID = GetSignID(strSignNames[nIndex]);

                                horaAscObj.SignEnglish = GetSignName(strSignNames[nIndex]);

                                horaAscObj.ZDegHours = (nSignID * 30) + KPObj.FromPos.DegHours;
                                horaAscObj.ZMinutes = KPObj.FromPos.Minutes;
                                horaAscObj.ZSeconds = KPObj.FromPos.Seconds;

                                bHoraFound = true;
                                break;
                            }
                        }

                        if (bHoraFound == true)
                        {
                            string[] strLordItems = KPObj.Lords.Split('/');

                            if (strLordItems.Length == 3)
                            {
                                horaAscObj.StarLord = strLordItems[0];
                                horaAscObj.StarSubLord = strLordItems[1];
                                horaAscObj.StarSubSubLord = strLordItems[2];
                            }

                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return horaAscObj;
        }
    }

    public class kpCommonObject
    {
        public string Type = "";
        public string Name = "";
        public string ZLongitude = "";
        public astroPosition ZLongitudePos = new astroPosition();
        public string SLontitude = "";
        public string ZLatitude = "";
        public string Declination = "";
        public string House = "";
        public int HouseNo = 0;
        public string Sign = "";
        public string SignLord = "";
        public string StarLord = "";
        public string StarSubLord = "";
        public string StarSubSubLord = "";
        public string Strength = "";
        public int PlanetsInStars = 0;
        public astroPosition DegreesFromAsc = new astroPosition();
        public string D1 = "";
        public string D2 = "";
        public string D3 = "";
        public string D4 = "";
        public string D5 = "";
        public string D6 = "";
        public string D7 = "";
        public string D8 = "";
        public string StarName = "";
        public int StarPadham = 0;
        public string Navamsam = "";
        public string PrimeSignificators = "";
        public string GeneralSignificators = "";
    }

    public class swephObject
    {
        #region Private Variables
        private string _ErrorMessage = "";
        private string _SWEConFilePath = "";
        private string _SWEFolderPath = "";
        private string _SWEBaseArguments = "-eswe -true -fPZLBDRS -g` -p0123456789xmtA ";
        private string _AstroData = "";
        private string _DasaLord = "";
        private int _DasaBalanceYears = 0;
        private int _DasaBalanceMonths = 0;
        private int _DasaBalanceDays = 0;
        private string[] _AstroPlanetNames = new string[] { "SUN", "MOON", "MERCURY", "VENUS", "MARS", "JUPITER", "SATURN", "URANUS", "NEPTUNE", "PLUTO", "MEAN NODE", "TRUE NODE", "MEAN APOGEE" };
        #endregion

        #region Public Properties
        public string ErrorMessage { get { return _ErrorMessage; } }
        public string SWEConFilePath { set { _SWEConFilePath = value; } }
        public string SWEFolderPath { set { _SWEFolderPath = value; } }
        public string SWEBaseArguments { set { _SWEBaseArguments = value; } }
        public string AstroData { get { return _AstroData; } }
        public string DasaLord { get { return _DasaLord; } }
        public int DasaBalanceYears { get { return _DasaBalanceYears; } }
        public int DasaBalanceMonths { get { return _DasaBalanceMonths; } }
        public int DasaBalanceDays { get { return _DasaBalanceDays; } }

        public astroPosition Ayanamsa = new astroPosition();
        public string SiderealTime = "";
        public string AyanamsaName = "";
        public ArrayList AstroObjects = new ArrayList();
        public static kpAstroObject KPAstroObject = new kpAstroObject();
        #endregion

        #region Private Methods
        private bool Validate()
        {
            if (!File.Exists(_SWEConFilePath))
            {
                _ErrorMessage = "Unable to find service file " + _SWEConFilePath;
                return false;
            }
            if (!Directory.Exists(_SWEFolderPath))
            {
                _ErrorMessage = "Unable to find ephmeris folder " + _SWEConFilePath;
                return false;
            }

            if (kpAstroObject.StarTableLoaded == false)
            {
                if (!KPAstroObject.LoadKPStarSubLordsTable())
                {
                    _ErrorMessage = "Loading KP Table failed";
                    return false;
                }
            }

            return true;
        }

        private bool ExecuteSWECon(string strAddArguements)
        {
            _AstroData = "";

            // swecon.exe -edirD:\Tools\N\SwissEph\Eph -eswe -true -fPZLBRS -b27.06.1975 -ut16:46 -house78.36,10.07,P -sid1 -topo78.36,10.07,0>> result.txt

            try
            {
                Process myProcess = new Process();

                myProcess.StartInfo.UseShellExecute = false;
                myProcess.StartInfo.FileName = _SWEConFilePath;
                myProcess.StartInfo.Arguments = string.Format(@"-edir{0} {1} {2}", _SWEFolderPath, _SWEBaseArguments, strAddArguements);
                myProcess.StartInfo.CreateNoWindow = true;
                myProcess.StartInfo.UseShellExecute = false;
                myProcess.StartInfo.RedirectStandardOutput = true;
                myProcess.Start();
                myProcess.WaitForExit();

                _AstroData = myProcess.StandardOutput.ReadToEnd();

                if (myProcess.HasExited == false)
                {
                    myProcess.Kill();
                }

                myProcess = null;

                return true;
            }
            catch (Exception ex)
            {
                _ErrorMessage = "Unable to execute service file\r\n";
                _ErrorMessage += ex.Message;
            }

            _AstroData = "";

            return false;
        }

        private bool ParseAstroPosition(string strPosition, ref astroPosition astroPos)
        {
            int asDegrees = 0;
            int asMinutes = 0;
            double asTemp = 0.0;
            int asSeconds = 0;

            try
            {
                astroPos.Clear();

                string strFormattedPosition = "";
                foreach (char chValue in strPosition)
                {
                    if (Char.IsDigit(chValue) == true || chValue == '.')
                    {
                        strFormattedPosition += chValue;
                    }
                    else
                    {
                        strFormattedPosition += "-";
                    }
                }
                strPosition = strFormattedPosition;
                strPosition = strPosition.Replace("--", "-");
                strPosition = strPosition.Replace("--", "-");
                strPosition = strPosition.Replace("--", "-");
                strPosition = strPosition.Replace("--", "-");

                string[] strPositionParts = strPosition.Split('-');
                if (strPositionParts.Length != 3)
                {
                    throw new Exception("Error in Astro Position String : " + strPosition);
                }

                asDegrees = Convert.ToInt32(strPositionParts[0].Trim());
                asMinutes = Convert.ToInt32(strPositionParts[1].Trim());
                asTemp = Convert.ToDouble(strPositionParts[2].Trim());
                asSeconds = (int)Math.Floor(asTemp);

                astroPos.Set(asDegrees, asMinutes, asSeconds);

                return true;
            }
            catch (Exception ex)
            {
                _ErrorMessage = "Unable to parse Astro Position\r\n";
                _ErrorMessage += ex.Message;
            }
            return false;
        }

        private bool IsAstroPlanet(string strValue)
        {
            try
            {
                foreach (string strPlanetName in _AstroPlanetNames)
                {
                    if (strValue.Trim().ToUpper() == strPlanetName)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                _ErrorMessage = "Unable to compare astro planet name\r\n";
                _ErrorMessage += ex.Message;
            }
            return false;
        }

        private bool ParseData()
        {
            string strData = "";

            string strAyanamsa = "";
            int nSPos = -1;
            int nEPos = -1;
            int nLineIndex = 0;

            try
            {
                Ayanamsa.Clear();
                SiderealTime = "";

                string[] strDataLines = _AstroData.Split('\n');

                if (strDataLines.Length < 41)
                {
                    _ErrorMessage = "Invalid astro data";
                    return false;
                }

                #region Parsing Ayanamsa
                strData = strDataLines[3];
                nSPos = strData.IndexOf("ayanamsa =");
                if (nSPos != -1)
                {
                    nSPos += 10;
                    nEPos = strData.IndexOf("\r", nSPos);
                    if (nEPos != -1)
                    {
                        strAyanamsa = strData.Substring(nSPos, nEPos - nSPos).Trim();

                        nSPos = strAyanamsa.IndexOf("(");
                        if (nSPos != -1)
                        {
                            AyanamsaName = strAyanamsa.Substring(nSPos + 1);
                            strAyanamsa = strAyanamsa.Substring(0, nSPos - 1);

                            nSPos = AyanamsaName.IndexOf(")");
                            if (nSPos != -1)
                            {
                                AyanamsaName = AyanamsaName.Substring(0, nSPos);
                            }
                        }

                        if (!ParseAstroPosition(strAyanamsa, ref Ayanamsa)) return false;
                    }
                }
                #endregion

                // Move to Planets
                int nElmentsPos = 0;
                for (nLineIndex = 4; nLineIndex < strDataLines.Length; nLineIndex++)
                {
                    if (string.IsNullOrEmpty(strDataLines[nLineIndex].Trim())) break;

                    string[] planetDetails = strDataLines[nLineIndex].Split('`');

                    if (planetDetails[0].Trim().ToUpper() == "SUN")
                    {
                        nElmentsPos = nLineIndex;
                        break;
                    }
                }

                if (nElmentsPos <= 0)
                {
                    _ErrorMessage = "Astro elements not found";
                    return false;
                }

                #region Parsing Planets
                for (nLineIndex = nElmentsPos; nLineIndex < strDataLines.Length; nLineIndex++)
                {
                    string strPlantPosition = "";

                    if (string.IsNullOrEmpty(strDataLines[nLineIndex].Trim())) break;

                    string[] planetDetails = strDataLines[nLineIndex].Split('`');

                    astroObject AO = new astroObject();

                    if (planetDetails.Length > 0)
                    {
                        AO.OName = planetDetails[0].ToUpper().Trim();
                    }

                    if (IsAstroPlanet(AO.OName))
                    {
                        AO.OType = "Planet";
                    }
                    else
                    {
                        AO.OType = "Location";
                    }

                    if (planetDetails.Length > 1)
                    {
                        if (planetDetails[1].Length > 5)
                        {
                            strPlantPosition = planetDetails[1];
                            AO.OSign = strPlantPosition.Substring(3, 2);
                            strPlantPosition = strPlantPosition.Replace(AO.OSign, "-");
                            AO.OSign = AO.OSign.ToUpper();
                            strPlantPosition = strPlantPosition.Trim();
                            ParseAstroPosition(strPlantPosition, ref AO.OSignLongitude);
                            strPlantPosition = "";
                        }
                    }

                    if (planetDetails.Length > 2)
                    {
                        if (planetDetails[2].Length > 5)
                        {
                            strPlantPosition = planetDetails[2].Trim();
                            ParseAstroPosition(strPlantPosition, ref AO.OZodiacLongitude);
                            strPlantPosition = "";
                        }
                    }

                    if (planetDetails.Length > 3)
                    {
                        if (planetDetails[3].Length > 5)
                        {
                            strPlantPosition = planetDetails[3].Trim();
                            if (strPlantPosition.StartsWith("-"))
                            {
                                AO.OZodiacLatitudeDirection = "South";
                                strPlantPosition = strPlantPosition.Substring(1);
                            }
                            else
                            {
                                AO.OZodiacLatitudeDirection = "North";
                            }
                            ParseAstroPosition(strPlantPosition, ref AO.OZodiacLatitude);
                            strPlantPosition = "";
                        }
                    }

                    if (planetDetails.Length > 4)
                    {
                        if (planetDetails[4].Length > 5)
                        {
                            strPlantPosition = planetDetails[4].Trim();
                            if (strPlantPosition.StartsWith("-"))
                            {
                                AO.ODeclinationDirection = "South";
                                strPlantPosition = strPlantPosition.Substring(1);
                            }
                            else
                            {
                                AO.ODeclinationDirection = "North";
                            }
                            ParseAstroPosition(strPlantPosition, ref AO.ODeclination);
                            strPlantPosition = "";
                        }
                    }

                    if (planetDetails.Length > 5)
                    {
                        if (planetDetails[5].Length > 5)
                        {
                            strPlantPosition = planetDetails[5].Trim();
                            AO.ODistance = Convert.ToDouble(strPlantPosition);
                            strPlantPosition = "";
                        }
                    }

                    if (planetDetails.Length > 6)
                    {
                        if (planetDetails[6].Length > 5)
                        {
                            strPlantPosition = planetDetails[6].Trim();
                            if (strPlantPosition[0] == '-')
                            {
                                AO.OSpeedPerDayDirection = "R";
                                strPlantPosition = strPlantPosition.Substring(1);
                            }
                            else
                            {
                                AO.OSpeedPerDayDirection = "";
                            }
                            ParseAstroPosition(strPlantPosition, ref AO.OSpeedPerDay);
                            strPlantPosition = "";
                        }
                    }

                    AO.SetAstroNames();

                    AstroObjects.Add(AO);
                }
                #endregion

                // Get Sidereal Time
                foreach (astroObject asObj in AstroObjects)
                {
                    if (asObj.OName.ToUpper() == "SIDEREAL TIME")
                    {
                        int raH = (int) Math.Floor(((decimal) asObj.OZodiacLongitude.DegHours / 15));
                        decimal raMTotal = (decimal)asObj.OZodiacLongitude.Minutes + Math.Floor((decimal)(asObj.OZodiacLongitude.DegHours - (raH * 15) ) * 60);
                        int raM = (int)Math.Floor(raMTotal / 15);
                        decimal raSTotal = (decimal)asObj.OZodiacLongitude.Seconds + Math.Floor((decimal)(raMTotal - (raM * 15)) * 60);
                        int raS = (int)Math.Floor(raSTotal / 15); ;
                        SiderealTime = string.Format("{0:00}:{1:00}:{2:00}", raH, raM, raS);
                        break;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                _ErrorMessage = "Unable to parse the astro data\r\n";
                _ErrorMessage += ex.Message;
            }

            return false;
        }
        #endregion

        #region Public Methods
        public static bool IsAstroObjectListed(string strName)
        {
            bool bResult = false;
            switch (strName.ToUpper())
            {
                case "SUN":
                case "MOON":
                case "MARS":
                case "MERCURY":
                case "JUPITER":
                case "VENUS":
                case "SATURN":
                case "MEAN NODE":
                case "TRUE NODE":
                case "HOUSE  1":
                case "HOUSE  2":
                case "HOUSE  3":
                case "HOUSE  4":
                case "HOUSE  5":
                case "HOUSE  6":
                case "HOUSE  7":
                case "HOUSE  8":
                case "HOUSE  9":
                case "HOUSE 10":
                case "HOUSE 11":
                case "HOUSE 12":
                case "ASCENDANT":
                case "MC":
                    bResult = true;
                    break;
                default:
                    break;
            }

            return bResult;
        }

        public static string GetPlanetName(string strShortName)
        {
            string strPlanetName = "";
            switch (strShortName.ToUpper())
            {
                case "SU":
                    strPlanetName = "SUN";
                    break;
                case "MO":
                    strPlanetName = "MOON";
                    break;
                case "MA":
                    strPlanetName = "MARS";
                    break;
                case "ME":
                    strPlanetName = "MERCURY";
                    break;
                case "JU":
                    strPlanetName = "JUPITER";
                    break;
                case "VE":
                    strPlanetName = "VENUS";
                    break;
                case "SA":
                    strPlanetName = "SATURN";
                    break;
                case "RA":
                    strPlanetName = "RAHU";
                    break;
                case "KE":
                    strPlanetName = "KETHU";
                    break;
                default:
                    strPlanetName = "";
                    break;
            }

            return strPlanetName;
        }

        public bool Process(DateTime birthDateTimeUTC, int longitudeDegrees, int longitudeMinutes, int latitudeDegrees, int latitudeMinutes, string housingType, int nAyanamsa, bool bTopoCentric)
        {
            _ErrorMessage = "";
            try
            {
                if (!Validate()) return false;

                double longitudeValue = longitudeDegrees + (double)longitudeMinutes / 60.0;
                double latitudeValue = latitudeDegrees + (double)latitudeMinutes / 60.0;

                string strSidereal = "";
                string strArguments = "";
                _DasaLord = "";
                _DasaBalanceYears = 0;
                _DasaBalanceMonths = 0;
                _DasaBalanceDays = 0;

                strSidereal = "";
                if (nAyanamsa >= 0)
                {
                    strSidereal = string.Format("-sid{0}", nAyanamsa);
                }

                if (bTopoCentric)
                {
                    strArguments = string.Format("-b{0}.{1}.{2} -ut{3}:{4} -house{5},{6},{7} {8} -topo{5},{6},0", birthDateTimeUTC.Day, birthDateTimeUTC.Month, birthDateTimeUTC.Year, birthDateTimeUTC.Hour, birthDateTimeUTC.Minute, longitudeValue, latitudeValue, housingType, strSidereal);
                }
                else
                {
                    strArguments = string.Format("-b{0}.{1}.{2} -ut{3}:{4} -house{5},{6},{7} {8} -geopos{5},{6},0", birthDateTimeUTC.Day, birthDateTimeUTC.Month, birthDateTimeUTC.Year, birthDateTimeUTC.Hour, birthDateTimeUTC.Minute, longitudeValue, latitudeValue, housingType, strSidereal);
                }

                if (!ExecuteSWECon(strArguments)) return false;

                if (!ParseData()) return false;

                for (int nAIndex = 0; nAIndex < AstroObjects.Count; nAIndex++)
                {
                    astroObject AO = (astroObject)AstroObjects[nAIndex];

                    #region Calculate Dasa Balance
                    if (AO.OName.IndexOf("MOON") != -1)
                    {
                        string strDasaLord = KPAstroObject.GetKPStarSubLords(AO.OSignEnglish, AO.OSignLongitude, 1);

                        _DasaLord = swephObject.GetPlanetName(strDasaLord);

                        int nDasaYears = KPAstroObject.GetDasaYears(strDasaLord);

                        int nStarCounts = (AO.OZodiacLongitude.TotalSeconds / 48000);

                        int nMinutesRemaining = (((nStarCounts + 1) * 48000) - AO.OZodiacLongitude.TotalSeconds) / 60;

                        int nDaysBalance = (int) (nMinutesRemaining * nDasaYears * 365.25) / 800;

                        int nMonths = (int) (nDaysBalance / 30.4375);
                        _DasaBalanceDays = (int) Math.Ceiling(nDaysBalance - (nMonths * 30.4375));
                        _DasaBalanceYears = nMonths / 12;
                        _DasaBalanceMonths = nMonths - (DasaBalanceYears * 12);
                    }
                    #endregion
                }

                return true;
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.Message;
            }
            return false;
        }
        #endregion
    }

    class astroPerson
    {
        #region Public Memembers
        public string Name = "";
        public string Sex = "";
        public DateTime BirthDateTime = DateTime.Parse("1900-01-01 00:00:00");
        public string TimeZoneValue = "";
        public TimeSpan TimeZoneDifference = new TimeSpan();
        public string PlaceOfBirth = "";
        public string Longitude = "";
        public int LongitudeDegrees = 0;
        public int LongitudeMinutes = 0;
        public string Latitude = "";
        public int LatitudeDegrees = 0;
        public int LatitudeMinutes = 0;
        public string TimeZoneName = "";
        public int HourCorrection = 0;
        public DateTime BirthDateTimeUTC = DateTime.Parse("1900-01-01 00:00:00");
        #endregion
        public void Clear()
        {
            Name = "";
            Sex = "";
            BirthDateTime = DateTime.Parse("1900-01-01 00:00:00");
            TimeZoneValue = "";
            TimeZoneDifference = TimeSpan.Parse("0.00:00:00");
            PlaceOfBirth = "";
            Longitude = "";
            LongitudeDegrees = 78;
            LongitudeMinutes = 36;
            Latitude = "";
            LatitudeDegrees = 10;
            LatitudeMinutes = 7;
            TimeZoneName = "";
            HourCorrection = 0;
            BirthDateTimeUTC = DateTime.Parse("1900-01-01 00:00:00");
        }
    }
}
