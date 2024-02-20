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
            BlueProfit_Column = int.Parse(parts[2])-1;
            YellowProfit_Column = int.Parse(parts[3]) - 1;
            int j = 4;
            for (int i = 0; i < 10; i++)
            {
                switch (parts[j]) {
                    case "p1": Red1ArrowColumn = i;
                        break;
                    case "p2": Red2ArrowColumn = i;
                        break;
                    case "s1": Yellow1ArrowColumn = i;
                        break;
                    case "s2": Yellow2ArrowColumn= i;
                        break;
                    case "k1":Blue1ArrowColumn = i;
                        break;
                    case "k2":Blue2ArrowColumn= i;
                        break;
                    case "z1":Green1ArrowColumn = i;
                        break;
                    case "z2":Green2ArrowColumn= i;
                        break;
                    case "0":
                        break;
                }
                j++;
            }

            string[] laps = new string[lines.Length - 1];
            Array.Copy(lines, 1, laps, 0, lines.Length - 1);
            Tasks=new string[laps.Length];
            TruckColors=new string[laps.Length];
            for (int i = 0; i < laps.Length; i++)
            {
                string[] parts2 = laps[i].Split('#');
                TruckColors[i] = parts2[0];
                Tasks[i] = parts2[1];
            }
        }

        public int RedProfit_Column {  get; }
        public int GreenProfit_Column { get;  }
        public int BlueProfit_Column { get; }
        public int YellowProfit_Column {  get; }
        public int Red1ArrowColumn { get; }
        public int Red2ArrowColumn { get; }
        public int Blue1ArrowColumn { get; }
        public int Blue2ArrowColumn { get; }
        public int Green1ArrowColumn { get; }
        public int Green2ArrowColumn { get; }
        public int Yellow1ArrowColumn { get; }
        public int Yellow2ArrowColumn { get; }   
        public string[] Tasks { get; }
        public string[] TruckColors { get; }
    }
}
