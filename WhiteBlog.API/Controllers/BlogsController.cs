using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WhiteBlog.API.Models;
using WhiteBlog.API.Services;

namespace WhiteBlog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly BlogsService _blogsService;

        public BlogsController(BlogsService blogsService)
        {
            _blogsService = blogsService;
        }

        [HttpPost]
        public ActionResult CreateNewBlog([FromHeader] string password, [FromBody] Blog blog)
        {
            if (password != "rockrockwhite")
            {
                return BadRequest(new Response<string>(new PasswordError()) {resultBody = ""});
            }

            _blogsService.Create(blog);
            return Ok(new Response<Blog>(new Ok()) {resultBody = blog});
        }

        [HttpGet("{id}")]
        public ActionResult GetBlogById(string id)
        {
            var blog = _blogsService.Get(id);
            if (blog == null)
            {
                return NotFound(new Response<string>(new BlogIdError()) {resultBody = ""});
            }

            return Ok(new Response<Blog>(new Ok()) {resultBody = blog});
        }

        [HttpGet]
        public ActionResult GetFilmsByPage([FromQuery] int page, [FromQuery] int limit)
        {
            /* 分页获得博客 */
            var blogs = _blogsService.Get(page, limit);
            return Ok(new Response<List<Blog>>(new Ok()) {resultBody = blogs});
        }

        [HttpPut("{id}")]
        public ActionResult UpdateBlog(string id, [FromHeader] string password, [FromBody] Blog newBlog)
        {
            if (password != "rockrockwhite")
            {
                return BadRequest(new Response<string>(new PasswordError()) {resultBody = ""});
            }

            var blog = _blogsService.Get(id);
            if (blog == null)
            {
                return NotFound(new Response<string>(new BlogIdError()) {resultBody = ""});
            }

            _blogsService.Update(id, newBlog);

            return Ok(new Response<Blog>(new Ok()) {resultBody = blog});
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteBlog(string id, [FromHeader] string password)
        {
            if (password != "rockrockwhite")
            {
                return BadRequest(new Response<string>(new PasswordError()) {resultBody = ""});
            }

            var blog = _blogsService.Get(id);
            if (blog == null)
            {
                return NotFound(new Response<string>(new BlogIdError()) {resultBody = ""});
            }

            _blogsService.Delete(id);

            return Ok(new Response<Blog>(new Ok()) {resultBody = blog});
        }
    }
}