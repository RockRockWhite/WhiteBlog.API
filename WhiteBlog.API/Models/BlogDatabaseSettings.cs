namespace WhiteBlog.API.Models
{
    public class BlogDatabaseSettings : IBlogDatabaseSettings
    {
        /* 用于保存连接数据库相关设置的类*/
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string BlogCollectionName { get; set; }
    }

    public interface IBlogDatabaseSettings
    {
        /* 用于保存连接数据库相关设置的interface 类*/
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string BlogCollectionName { get; set; }
    }
}