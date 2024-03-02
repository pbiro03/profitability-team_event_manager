using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prof1
{
    internal class DataManager
    {
        public DataManager(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            
            string[] parts = lines[0].Split('#');
            
            RedProfit_Column = int.Parse(parts[0])-1;
            GreenProfit_Column = int.Parse(parts[1]) - 1;
            BlueProfit_Column = int.Parse(parts[2]) - 1;
            YellowProfit_Column = int.Parse(parts[3]) - 1;
            int j = 4;
            arrowImages = new ArrowImage[12];
            for (int i = 1; i < 12; i++)
            {
                switch (parts[j].Substring(0,1)) {
                    case "p":
                        arrowImages[i] = new ArrowImage();
                        arrowImages[i].Color = "red";
                        arrowImages[i].Column = i;
                        if (int.Parse(parts[j].Substring(1,1))==1)
                            arrowImages[i].IsMirrored = true;
                        break;                    
                    case "s":
                        arrowImages[i] = new ArrowImage();
                        arrowImages[i].Color = "yellow";
                        arrowImages[i].Column = i;
                        if (int.Parse(parts[j].Substring(1, 1)) == 1)
                            arrowImages[i].IsMirrored = true;
                        break;
                    case "k":
                        arrowImages[i] = new ArrowImage();
                        arrowImages[i].Color = "blue";
                        arrowImages[i].Column = i;
                        if (int.Parse(parts[j].Substring(1, 1)) == 1)
                            arrowImages[i].IsMirrored = true;
                        break;
                    case "z":
                        arrowImages[i] = new ArrowImage();
                        arrowImages[i].Color = "green";
                        arrowImages[i].Column = i;
                        if (int.Parse(parts[j].Substring(1, 1)) == 1)
                            arrowImages[i].IsMirrored = true;
                        break;
                    case "0":
                        break;
                }
                j++;
            }
            PlaceNameandDate = parts[j];

            string[] laps = new string[lines.Length - 1];
            Array.Copy(lines, 1, laps, 0, lines.Length - 1);
            Tasks=new string[laps.Length];
            TruckColors=new string[laps.Length];
            WhichTeamsRound=new int[laps.Length];
            for (int i = 0; i < laps.Length; i++)
            {
                string[] parts2 = laps[i].Split('#');
                TruckColors[i] = parts2[0];
                Tasks[i] = parts2[1];
                WhichTeamsRound[i] = int.Parse(parts2[2]);
            }
        }
        public string PlaceNameandDate { get; }
        public ArrowImage[] arrowImages { get; }
        public int RedProfit_Column {  get; }
        public int GreenProfit_Column { get;  }
        public int BlueProfit_Column { get; }
        public int YellowProfit_Column {  get; }  
        public string[] Tasks { get; }
        public string[] TruckColors { get; }
        public int[] WhichTeamsRound {  get; }
    }
}
