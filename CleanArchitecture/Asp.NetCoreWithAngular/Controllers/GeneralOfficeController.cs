using Application.Interfaces;
using Application.ViewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetCoreWithAngular.Controllers
{
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class GeneralOfficeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private IGeneralOfficeService IGeneralOfficeService;

        public GeneralOfficeController(IGeneralOfficeService IGeneralOfficeService)
        {
            this.IGeneralOfficeService = IGeneralOfficeService;
        }


        [HttpPost]
        [Route("RegisterGeneralOffice")]
        public async Task<IActionResult> RegisterGeneralOffice([FromBody] GeneralOfficeViewModel GeneralOfficeViewModel)
        {
            try
            {
                GeneralOfficeViewModel = await this.IGeneralOfficeService.AddAsync(GeneralOfficeViewModel);
                return Json(GeneralOfficeViewModel);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        [HttpPost]
        [Route("EditGeneralOffice")]
        public async Task<IActionResult> EditGeneralOffice([FromBody] GeneralOfficeViewModel GeneralOfficeViewModel)
        {
            try
            {
                GeneralOfficeViewModel = await this.IGeneralOfficeService.EditAsync(GeneralOfficeViewModel);
                return Json(GeneralOfficeViewModel);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        //[HttpGet]
        [Route("FetchListGeneralOffice")]
        public async Task<IActionResult> FetchListGeneralOffice([FromBody]GeneralOfficeViewModel GeneralOfficeViewModel )
        {
            try
            {
                List<GeneralOfficeViewModel> GeneralOfficeViewModelList = await this.IGeneralOfficeService.GetListAsync(GeneralOfficeViewModel);
                return Json(GeneralOfficeViewModelList);
            }
            catch(Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        //[HttpGet]
        [Route("RemoveGeneralOffice")]
        public async Task<IActionResult> RemoveGeneralOffice([FromBody] GeneralOfficeViewModel GeneralOfficeViewModel)
        {
            try
            {
                int Row  = await this.IGeneralOfficeService.RemoveAsync(GeneralOfficeViewModel);
                return Json(Row);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

    }
}
