using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Models;
using Backend.Models.Entities;
using Backend.Services.Interfaces;
using Backend.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.Api
{
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class MessagesController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly IMapper _mapper;

        public MessagesController(ICatalogService catalogService, IMapper mapper)
        {
            _catalogService = catalogService;
            _mapper = mapper;
        }

        [Authorize(Roles = RoleModel.Admin + ", " + RoleModel.User)]
        [HttpGet("{messageId}")]
        public IActionResult GetMessage(string messageId)
        {
             Message message = _catalogService.GetMessage(messageId);

            return Ok(_mapper.Map<MessageViewModel>(message));
        }

        [Authorize(Roles = RoleModel.Admin)]
        [HttpPost("")]
        public async Task<IActionResult> CreateMessage([FromBody, Bind("Text", "CatalogId", "Subject")]MessageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var message = _mapper.Map<Message>(model);

            await _catalogService.CreateMessage(message);

            return Created("", _mapper.Map<MessageViewModel>(message));
        }

        [Authorize(Roles = RoleModel.Admin)]
        [HttpPut("{messageId}")]
        public async Task<ActionResult> EditMessage(string messageId,[FromBody, Bind("Text", "CatalogId", "Subject")]MessageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var message = _mapper.Map<Message>(model);

            await _catalogService.EditMessage(messageId,message);

            return Ok(_mapper.Map<MessageViewModel>(message));
        }

        [Authorize(Roles = RoleModel.Admin)]
        [HttpDelete("{messageId}")]
        public async Task<ActionResult> DeleteMessage(string messageId)
        {
            await _catalogService.DeleteMessage(messageId);

            //Возможно тут должно быть NotFound
            return Ok();
        }
    }
}