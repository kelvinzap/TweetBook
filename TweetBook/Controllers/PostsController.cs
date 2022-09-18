using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TweetBook.Cache;
using TweetBook.Contracts;
using TweetBook.Contracts.V1.Request;
using TweetBook.Contracts.V1.Response;
using TweetBook.Data;
using TweetBook.Domain;
using TweetBook.Extensions;
using TweetBook.Services;

namespace TweetBook.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PostsController : Controller
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public PostsController(DataContext context, IPostService postService, IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }

        [HttpGet(ApiRoutes.Posts.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var posts = await _postService.GetAllAsync();
            return Ok(_mapper.Map<List<PostResponse>>(posts));
        }

        [HttpGet(ApiRoutes.Posts.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid postId)
        {
            var post = await _postService.GetPostByIdAsync(postId);
            
            return (post == null) ? NotFound() : Ok(post);
        }
        
        [HttpPost(ApiRoutes.Posts.Create)]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest request)
        {
        
            
            if (request == null || string.IsNullOrWhiteSpace(request.Name))
                return BadRequest(new {error = "Invalid Request" });
            var tags = request.Tags.Select(tagName => new PostTag { TagName = tagName }).ToList();

            var post = new Post
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                UserId = HttpContext.GetUserById(),
                Tags = tags
            };
            foreach (var postTag in post.Tags)
            {
                postTag.PostId = post.Id; 
            }
            
            var created = await _postService.CreateAsync(post);
            
            if (!created)
                return BadRequest(new {error = "Invalid Request" });

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Posts.GetAll.Replace("{postId}", post.Id.ToString());
            
            return Created(locationUri, _mapper.Map<PostResponse>(post));
        }

        [HttpPut(ApiRoutes.Posts.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid postId, [FromBody] UpdatePostRequest request)
        {
            var userOwnsPost = await _postService.UserOwnsPost(postId, HttpContext.GetUserById());
            
            if (!userOwnsPost)
            {
                return BadRequest(new {errors = "You cannot perform this action"});
            }
            
            if (request == null || string.IsNullOrWhiteSpace(request.Name))
                return BadRequest(new {error = "Invalid Request" });

            var post = await _postService.GetPostByIdAsync(postId);

            if (post == null)
                return NotFound();

            post.Name = request.Name;

            var updated = await _postService.UpdatePostAsync(post);
            
            return (updated) ? Ok(_mapper.Map<PostResponse>(post)) : NotFound();

        }

        [HttpDelete(ApiRoutes.Posts.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid postId)
        {
            var userOwnsPost = await _postService.UserOwnsPost(postId, HttpContext.GetUserById());
            
            if (!userOwnsPost)
            {
                return BadRequest(new {errors = "You cannot perform this action"});
            }
            
            var post = await _postService.GetPostByIdAsync(postId);

            if (post == null)
                return NotFound();
            
            var deleted = await _postService.DeletePostAsync(post);

            return (deleted) ? NoContent() : NotFound();
        }
    }
    
}