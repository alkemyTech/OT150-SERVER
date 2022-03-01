﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using OngProject.Repositories.Interfaces;
using System.Collections.Generic;

namespace OngProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly ICommentBusiness _commentBusiness;
        private readonly INewsBusiness _newsBusiness;
        private readonly IUnitOfWork _unitOfWork;

        public NewsController(ICommentBusiness commentBusiness, INewsBusiness newsBusiness, IUnitOfWork unitOfWork)
        {
            _commentBusiness = commentBusiness;
            _newsBusiness = newsBusiness;
            _unitOfWork = unitOfWork;
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet("{id:int}/comments")]
        public ActionResult<List<CommentDto>> GetCommentFilterByNews(int id)
        {
            var listaComments = _commentBusiness.showListCommentDto(id);
            return Ok(listaComments);
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet("{id:int}")]
        public ActionResult<NewsDto> GetNew(int id)
        {
            return Ok(_newsBusiness.GetNews(id));
        }

    }
}
