using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using OzeContract.Databases;
using OzeContract.ViewModels;

namespace OzeApi.Controllers
{
    [Route("api/device")]
    public class DeviceController : Controller
    {
        private readonly IMongoContext mongoContext;

        public DeviceController(IMongoContext mongoContext)
        {
            this.mongoContext = mongoContext;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> All()
        {
            var rawResult = await mongoContext.Set("Main", "Devices").GetAll<DeviceViewModel>();

            var result = rawResult.Select(x => new ShowDeviceViewModel(x));

            Debug.WriteLine(result.ToString());

            return Ok(result);
        }

        [HttpGet]
        [Route("single")]
        public async Task<IActionResult> Single([FromQuery]string objectId)
        {
            var result = await mongoContext.Set("Main", "Devices").GetSingle<DeviceViewModel>(objectId);

            Debug.WriteLine(result.ToString());

            return Ok(result);
        }

        [HttpPut]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody] DeviceViewModel viewModel)
        {
            await mongoContext.Set("Main", "Devices").Add<DeviceViewModel>(viewModel);

            return Ok(new
            {
                message = "Device has been added"
            });
        }

        [HttpDelete]
        [Route("remove")]
        public async Task<IActionResult> Remove([FromBody] IdViewModel viewModel)
        {

            if (viewModel != null)
            {
                await mongoContext.Set("Main", "Devices").Remove<DeviceViewModel>(viewModel.Id);
            }

            return Ok(new
            {
                message = "Device has been removed"
            });
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] EditedDeviceViewModel viewModel)
        {

            var device = new DeviceViewModel(viewModel);

            if (device != null)
            {
                await mongoContext.Set("Main", "Devices").Update<DeviceViewModel>(viewModel.Id, device);
            }

            return Ok(new
            {
                message = "Device has been updated"
            });
        }
    }
}