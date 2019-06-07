using System.Collections.Generic;
using MongoDB.Bson;

namespace OzeContract.ViewModels
{
    public class DeviceViewModel
    {
        public DeviceViewModel()
        {
            
        }
        public DeviceViewModel(EditedDeviceViewModel viewModel)
        {
            Id = ObjectId.Parse(viewModel.Id);
            Address = viewModel.Address;
            Name = viewModel.Name;
            Desc = viewModel.Desc;
            Interval = viewModel.Interval;
        }

        public ObjectId Id { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public IList<MeasurementViewModel> Measurements { get; set; }
        public int Interval { get; set; }
        public int MyProperty { get; set; }
    }
}