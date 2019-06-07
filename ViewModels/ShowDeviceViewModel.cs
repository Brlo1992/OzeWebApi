using System;
using System.Collections.Generic;
using System.Linq;

namespace OzeContract.ViewModels
{
    public class ShowDeviceViewModel
    {
        public ShowDeviceViewModel(DeviceViewModel viewModel)
        {
            Id = viewModel.Id.ToString();
            Address = viewModel.Address;
            Name = viewModel.Name;
            Interval = viewModel.Interval;
            Desc = viewModel.Desc;
            MeasurementCount = GetMeasurements(viewModel.Measurements);
            LastMeasurementDate = GetLastMeasurementData(viewModel.Measurements);
        }

        private string GetLastMeasurementData(IList<MeasurementViewModel> measurements) => measurements != null ? measurements.Last().Date : string.Empty;

        private int GetMeasurements(IList<MeasurementViewModel> measurements) => measurements != null ? measurements.Count : 0;

        public string Id { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public int Interval { get; set; }
        public int MeasurementCount { get; set; }
        public string LastMeasurementDate { get;  set; }
    }
}