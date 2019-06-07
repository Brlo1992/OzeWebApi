using System;
using MongoDB.Bson;

namespace OzeContract.ViewModels
{
    public class MeasurementViewModel
    {
        public string Date { get; set; } = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
        public double Ac { get; set; } 
        public double Dc { get; set; } 
        public double Fraquency { get; set; } 
        public double Temp { get; set; } 
        public double Energy { get; set; } 
        public double Electricity { get; set; } 
        public double Tension { get; set; }
        public double CosFi { get; set; } 
        public double Power { get; set; } 
    }
}