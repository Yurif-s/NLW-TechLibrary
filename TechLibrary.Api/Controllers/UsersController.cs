﻿using Microsoft.AspNetCore.Mvc;

namespace TechLibrary.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    [HttpPost]
    public IActionResult Create()
    {
        return Created();
    }
}
