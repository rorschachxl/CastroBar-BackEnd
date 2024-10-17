using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CASTROBAR_API.Dtos;
using CASTROBAR_API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CASTROBAR_API.Controllers
{
    public class MesaController: ControllerBase
    {
        [HttpPost]
        [Route("SendOrder/{id}")]
        public async Task<IActionResult> SendOrder(string id,RecoveryPassDto NumMesa, Orden orden)
         {
            return Ok();
         }
    }
}