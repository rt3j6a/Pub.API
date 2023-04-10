using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pub.Core.Interface {
    public interface IPostManager {
        Task<(bool success, string message)> AddPostAsync(string postHeader, string postBody, string? picturesSourceRoute, int eventId,int accountId );

        Task<(bool success, string message)> DeletePostAsync(int postId);

        Task<(bool success, string message)> UpdatePostContentAsync(int postId,string? postHeader, string? postBody);

        Task<IEnumerable<object>> GetAllPostsAsync();

        Task<IEnumerable<object>> GetAllPostsByEventAsync(int eventId);

        Task<object?> GetPostAsync(int postId);
    }
}
