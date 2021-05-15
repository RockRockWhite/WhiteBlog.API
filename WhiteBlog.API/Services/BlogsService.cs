using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using WhiteBlog.API.Models;

namespace WhiteBlog.API.Services
{
    public class BlogsService
    {
        private IMongoCollection<Blog> _blogs;

        public BlogsService(BlogDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _blogs = database.GetCollection<Blog>(settings.BlogCollectionName);
        }

        public Blog Create(Blog blog)
        {
            // 附上时间戳
            blog.CreatedDate = DateTime.Now;
            blog.LastEditedDate = DateTime.Now;

            _blogs.InsertOne(blog);
            return blog;
        }

        public Blog Get(string id) => _blogs.Find(blog => blog.Id == id).FirstOrDefault();

        public List<Blog> Get(int page, int limit = 20)
        {
            var blogs = _blogs.AsQueryable<Blog>().OrderBy(film => film.LastEditedDate).Skip(limit * (page - 1))
                .Take(20).ToList();
            foreach (var blog in blogs)
            {
                // 擦除博客内容数据
                blog.Body = "";
            }

            return blogs;
        }

        public void Update(string id, Blog newBlog)
        {
            newBlog.Id = id;
            // 更新时间戳
            newBlog.LastEditedDate = DateTime.Now;
            _blogs.ReplaceOne(blog => blog.Id == id, newBlog);
        }

        public void Delete(string id) => _blogs.DeleteOne(blog => blog.Id == id);
    }
}