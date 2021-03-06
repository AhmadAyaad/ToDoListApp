﻿using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ToDoList.API.Data;
using ToDoList.API.Dtos;
using ToDoList.API.Helpers;
using ToDoList.API.Models;
using ToDoList.API.Repository;

namespace ToDoList.API.Controllers
{
    [Authorize]
    [Route("api/user/{userId}/[Controller]")]
    [ApiController]
    public class DairyController : ControllerBase
    {
        private readonly IDairyRepository _dairyRepository;
        private readonly IUserRepository _userRepository;
        readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        readonly Cloudinary _cloudinary;
        public DairyController(IDairyRepository dairyRepository, IUserRepository userRepository,
            IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _cloudinaryConfig = cloudinaryConfig;
            _dairyRepository = dairyRepository;
            _userRepository = userRepository;

            Account account = new Account
            {
                Cloud = _cloudinaryConfig.Value.CloudName,
                ApiKey = _cloudinaryConfig.Value.ApiKey,
                ApiSecret = _cloudinaryConfig.Value.ApiSecret
            };


            _cloudinary = new Cloudinary(account);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetDairy(int id, int userId)
        {
            var uId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (userId != uId)
                return Unauthorized();

            var dairy = await _dairyRepository.GetDairy(id, userId);

            return Ok(dairy);
        }



        [HttpGet]
        public async Task<IActionResult> GetDairies(int userId)
        {
            var uId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (userId != uId)
                return Unauthorized();
            var dairies = await _dairyRepository.GetAll(uId);
            return Ok(dairies);
        }





        [HttpPost]
        public IActionResult CreateDairy([FromForm] DairyForCreateDto dairyForCreate, int userId)
        {
            var uId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (userId != uId)
                return Unauthorized();


            var user =  _userRepository.GetUser(uId);

            var file = dairyForCreate.File;
            //this is the reponse that will be recvied from cloudinary
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        //3shan n3dl fl sora lw hya akbr mn 500 s3tha hy2os mn 3la el 7rof 
                        //we yrkz 3lael wsh 
                        Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
                    };
                    //hna el sora et3mlha upload kda
                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }
            //bn3ml set ll url bta3 el sora bl dto bta3y elly hyro7 el db
            dairyForCreate.Url = uploadResult.Url.ToString();
            dairyForCreate.PublicId = uploadResult.PublicId;


            Dairy dairy = new Dairy
            {
                UserId = userId,
                Date = dairyForCreate.Date,
                Text = dairyForCreate.Text,
                Time = dairyForCreate.Time,


            };

            Photo photo = new Photo
            {
                DairyId = dairyForCreate.DairyId,
                DataAdded = DateTime.Now,
                PublicId = dairyForCreate.PublicId,
                Url = dairyForCreate.Url
            };

            User newUser = new User { UserId = userId };


            _dairyRepository.CreateDairy(dairy, photo , newUser);
            return Ok(dairyForCreate);
        }


       
    }
}
