using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using OzeContract.ViewModels;

namespace OzeApi.Hub
{
    public class ApiHub : Microsoft.AspNetCore.SignalR.Hub
    {
        public async Task SendMessage()
        {
            Debug.WriteLine("Call send message method");

            await Clients.All.SendAsync("Measurements", new MeasurementViewModel());
        }
    }
}