using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OzeApi.Hub;
using OzeContract.Databases;
using OzeContract.ViewModels;

namespace OzeApi.Controllers
{
    [Route("api/measurement")]
    public class MeasurementController : Controller
    {
        private readonly IHubContext<ApiHub> hubContext;
        private readonly IMongoContext mongoContext;

        public MeasurementController(IHubContext<ApiHub> hubContext, IMongoContext mongoContext)
        {
            this.hubContext = hubContext;
            this.mongoContext = mongoContext;
        }

        [HttpGet]
        [Route("show")]
        public async Task<IActionResult> Show()
        {
            await hubContext.Clients.All.SendAsync("Measurements", new MeasurementViewModel());

            return Ok();
        }

        [HttpPut]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody]AddMeasurementViewModel viewModel)
        {
            var device = await mongoContext.Set("Main", "Devices").GetSingle<DeviceViewModel>(viewModel.ObjectId);

            if (device != null && viewModel.Measurements != null)
            {
                if (device.Measurements != null)
                {
                    device.Measurements.Add(viewModel.Measurements);
                }
                else
                {
                    device.Measurements = new List<MeasurementViewModel> { viewModel.Measurements };
                }

                await mongoContext.Set("Main", "Devices").Update<DeviceViewModel>(viewModel.ObjectId, device);
            }

            return Ok();
        }
    }
}