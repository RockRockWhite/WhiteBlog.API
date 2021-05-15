namespace WhiteBlog.API.Models
{
    public interface IError
    {
        public int errorCode { set; get; }
        public string errorMsg { set; get; }
    }

    public class Ok : IError
    {
        public Ok()
        {
            errorCode = 0;
            errorMsg = "";
        }

        public int errorCode { get; set; }
        public string errorMsg { get; set; }
    }


    public class PasswordError : IError
    {
        public PasswordError()
        {
            errorCode = 1;
            errorMsg = "password is not correct.";
        }

        public int errorCode { get; set; }
        public string errorMsg { get; set; }
    }

    public class BlogIdError : IError
    {
        public BlogIdError()
        {
            errorCode = 2;
            errorMsg = "Blog id is not existed.";
        }

        public int errorCode { get; set; }
        public string errorMsg { get; set; }
    }
}