using System.Collections.Generic;

namespace ProjetoBlog.Controllers
{
    public interface IBlogService
    {
        List<BlogPost> GetLatestPosts();
        string GetPostText(string link);

        List<BlogPost> GetOlderPosts(int oldestPostId);
    }
}