using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class WriteData : MonoBehaviour {
    private string dir;
    private string fileName;

    private GameObject carryOverStats;
    private FinalStats finalStats;

    private GameObject motorwaySetup;
    private Parameters parameters;

    private StreamWriter writer;
    private StringBuilder sb;

    private string titles;
    
    // Start is called before the first frame update
    void Start() {
        if (!Directory.Exists(Directory.GetCurrentDirectory() + "/SimulationStats")) {
            Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/SimulationStats");
        }
        
        dir = Directory.GetCurrentDirectory() + "/SimulationStats/";
        
        carryOverStats = GameObject.Find("CarryOverStats");
        finalStats = carryOverStats.GetComponent<FinalStats>();

        motorwaySetup = GameObject.FindWithTag("MotorwaySetup");
        parameters = motorwaySetup.GetComponent<Parameters>();

        titles = "EastVehiclesArrivals," + "EastVehiclesDepartures," + "EastAverageSpeeds," + "EastVehicleNums,"
                 + "EastArrivalRates," + "EastDepartureRates," + "WestVehicleArrivals, " + "WestVehiclesDepartures,"
                 + "WestAverageSpeeds," + "WestVehicleNums," + "WestArrivalRates," + "WestDepartureRates,"
                 + "EastAverageArrivalRate," + "EastAverageDepartureRate," + "EastAverageSpeed," +
                 "EastAverageVehicleNum"
                 + "WestAverageArrivalRate," + "WestAverageDepartureRate," + "WestAverageSpeed," +
                 "WestAverageVehicleNum";

    }

    public void Write() {
        fileName = dir + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm") + "_Simulation_Stats.csv";
        
        
         writer = new StreamWriter(fileName);

         writer.WriteLine(titles);

         for (int i = 0; i < finalStats.eastAverageSpeeds.Count; i++) {
             sb = new StringBuilder();
                 
             if (i < finalStats.eastVehicleArrivals.Count - 1) {
                 sb.Append(finalStats.eastVehicleArrivals[i].ToString() + ",");
             } else {
                 sb.Append(",");
             }
             
             if (i < finalStats.eastVehicleDepartures.Count - 1) {
                 sb.Append(finalStats.eastVehicleDepartures[i].ToString() + ",");
             } else {
                 sb.Append(",");
             }
             
             switch (parameters.speedUnits) {
                 case "Mph":
                     if (float.IsNaN(finalStats.eastAverageSpeeds[i] * 2.237f)) {
                         sb.Append("0,");
                     } else {
                         sb.Append((finalStats.eastAverageSpeeds[i] * 2.237f).ToString() + ",");
                     }
                     break;
                 case "Kph":
                     if (float.IsNaN(finalStats.eastAverageSpeeds[i] * 3.6f)) {
                         sb.Append("0,");
                     } else {
                         sb.Append((finalStats.eastAverageSpeeds[i] * 3.6f).ToString() + ",");
                     }
                     break;
             }

             sb.Append(finalStats.eastVehicleNums[i].ToString() + ",");

             sb.Append(finalStats.eastArrivalRates[i].ToString() + ",");
             
             sb.Append(finalStats.eastDepartureRates[i].ToString() + ",");
             
             if (i < finalStats.westVehicleArrivals.Count - 1) {
                 sb.Append(finalStats.westVehicleArrivals[i].ToString() + ",");
             } else {
                 sb.Append(",");
             }
             
             if (i < finalStats.westVehicleDepartures.Count - 1) {
                 sb.Append(finalStats.westVehicleDepartures[i].ToString() + ",");
             } else {
                 sb.Append(",");
             }
             
             switch (parameters.speedUnits) {
                 case "Mph":
                     if (float.IsNaN(finalStats.westAverageSpeeds[i] * 2.237f)) {
                         sb.Append("0,");
                     } else {
                         sb.Append((finalStats.westAverageSpeeds[i] * 2.237f).ToString() + ",");
                     }
                     break;
                 case "Kph":
                     if (float.IsNaN(finalStats.westAverageSpeeds[i] * 3.6f)) {
                         sb.Append("0,");
                     } else {
                         sb.Append((finalStats.westAverageSpeeds[i] * 3.6f).ToString() + ",");
                     }
                     break;
             }

             sb.Append(finalStats.westVehicleNums[i].ToString() + ",");

             sb.Append(finalStats.westArrivalRates[i].ToString() + ",");
             
             sb.Append(finalStats.westDepartureRates[i].ToString());

             if (i == 0) {
                 sb.Append("," + finalStats.eastAverageArrivalRate.ToString() + ",");
                 sb.Append(finalStats.eastAverageDepartureRate.ToString() + ",");
                 
                 switch (parameters.speedUnits) {
                     case "Mph":
                         sb.Append((finalStats.eastAverageSpeed * 2.237f).ToString() + ",");
                         break;
                     case "Kph":
                         sb.Append((finalStats.eastAverageSpeed * 3.6f).ToString() + ",");
                         break;
                 }
                 
                 sb.Append(finalStats.eastAverageVehicleNum.ToString() + ",");

                 sb.Append(finalStats.westAverageArrivalRate.ToString() + ",");
                 sb.Append(finalStats.westAverageDepartureRate.ToString() + ",");
                 
                 switch (parameters.speedUnits) {
                     case "Mph":
                         sb.Append((finalStats.westAverageSpeed * 2.237f).ToString() + ",");
                         break;
                     case "Kph":
                         sb.Append((finalStats.westAverageSpeed * 3.6f).ToString() + ",");
                         break;
                 }

                 sb.Append(finalStats.westAverageVehicleNum.ToString() + ",");
             }
             
             writer.WriteLine(sb.ToString());
         }
         writer.Close();
         
    }
}