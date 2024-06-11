﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneShopApi.Auth.Data;
using PhoneShopApi.Auth.Dto.Phone.Color;
using PhoneShopApi.Auth.Interfaces.IRepository;
using PhoneShopApi.Auth.Mappers;
using System;

namespace PhoneShopApi.Auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhonecolorController(
        PhoneShopDbContext context,
        IPhoneColorRepository phoneColorRepo,
        IWebHostEnvironment environment) : Controller
    {
        private readonly PhoneShopDbContext _context = context;
        private readonly IPhoneColorRepository _phoneColorRepo = phoneColorRepo;
        private readonly IWebHostEnvironment _environment = environment;

        [HttpGet]
        [Route("GetAllPhoneColor")]
        public async Task<IActionResult> GetAllPhoneColors()
        {
            var phoneColors = await _phoneColorRepo.GetAllPhoneColorsAsync();
            var phoneColorDTOs = phoneColors.Select(pc => pc.ToPhoneColorDto());

            return Ok(phoneColorDTOs);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var phoneColor = await _phoneColorRepo.GetByIdAsync(id);
            if (phoneColor is null)
            {
                return NotFound();
            }

            var phoneColorDTOs = phoneColor.ToPhoneColorDto();

            return Ok(phoneColorDTOs);
        }

        [HttpPost]
        [Route("UpdateImage/{phoneColorId:int}")]
        public async Task<IActionResult> UploadFile(IFormFile file, int phoneColorId)
        {
            var phoneColor = _context.PhoneColors.Find(phoneColorId);
            if (phoneColor == null) return NotFound("Phone color not found");

            var imageUrl = await WriteFile(file);
            string HostUrl = $"{Request.Scheme}://{Request.Host}/";
            phoneColor.ImageUrl = HostUrl + imageUrl;

            await _context.SaveChangesAsync();

            return Ok(phoneColor.ToPhoneColorDto());
        }

        private async Task<string> WriteFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentNullException(nameof(file), "The uploaded file cannot be null or empty.");
            }

            string filename = "";
            string exactpath = "";

            try
            {
                filename = file.FileName; // Generate unique filename with GUID
                var uploadsFolderPath = Path.Combine(_environment.WebRootPath, "Uploads", "PhoneImages"); // Clearer path construction

                // Create uploads folder if it doesn't exist
                if (!Directory.Exists(uploadsFolderPath))
                {
                    Directory.CreateDirectory(uploadsFolderPath);
                }

                exactpath = Path.Combine(uploadsFolderPath, filename);

                using (var stream = new FileStream(exactpath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
            }

            return Path.Combine("Uploads", "PhoneImages", filename); // Return relative path
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreatePhoneColorRequestDto createPhoneColorRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var newPhoneColor = createPhoneColorRequestDto.ToPhoneColorFromCreatePhoneColorRequestDto();
            newPhoneColor.ImageUrl = $"{Request.Scheme}://{Request.Host}/Uploads/PhoneImages/NotFound.jpg";
            await _phoneColorRepo.CreateAsync(newPhoneColor);

            return CreatedAtAction(
                nameof(Post),
                new { id = newPhoneColor.Id },
                newPhoneColor.ToPhoneColorDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(
            [FromRoute] int id,
            [FromBody] UpdatePhoneColorRequestDto updatePhoneColorRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var phoneColorToUpdated = await _phoneColorRepo.UpdateAsync(id, updatePhoneColorRequestDto);
            if (phoneColorToUpdated == null) return NotFound();

            return Ok(phoneColorToUpdated.ToPhoneColorDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid) return BadRequest();

            var deletedPhoneColor = await _phoneColorRepo.DeleteAsync(id);
            if (deletedPhoneColor == null) return NotFound();

            return Ok(deletedPhoneColor.ToPhoneColorDto());
        }

    }
}