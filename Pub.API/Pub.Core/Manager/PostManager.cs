using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pub.Core.Common;
using Pub.Core.Interface;
using Pub.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pub.Core.Manager {
    public class PostManager : CoreManager, IPostManager {
        public PostManager(IConfiguration configuration, IServiceScopeFactory scopeFactory) : base(configuration, scopeFactory) {
        }

        public async Task<(bool success, string message)> AddPostAsync(string postHeader, string postBody, string? picturesSourceRoute, int eventId, int accountId) {
            Post post = new Post {
                AccountId = accountId,
                EventId = eventId,
                PostHeader = postHeader,
                PicturesSourceRoute = picturesSourceRoute,
                PostBody = postBody
            };

            try {
                await provider.Posts.AddAsync(post);
                await provider.SaveChangesAsync();
                return (true, Messages.Post.PostCreatedSuccessfully);
            } catch (Exception) {
                return (false, Messages.InternalServerError);
            }
        
        }

        public async Task<(bool success, string message)> DeletePostAsync(int postId) {
            var result = await provider.Posts.Where(x => x.PostId == postId).ExecuteDeleteAsync();

            try {
                await provider.SaveChangesAsync();
                return result == 1 ? (true, Messages.Post.PostDeletedSuccessfully) : (false, Messages.Post.PostDoesntExistsOrDeleted);
            } catch (Exception) {
                return (false, Messages.InternalServerError);
            }
        }

        public async Task<IEnumerable<object>> GetAllPostsAsync() {
            return await provider.Posts.ToListAsync();
        }

        public async Task<IEnumerable<object>> GetAllPostsByEventAsync(int eventId) {
            return await provider.Posts.Where(x => x.EventId == eventId).ToListAsync();
        }

        public async Task<object?> GetPostAsync(int postId) {
            return await provider.Posts.FirstOrDefaultAsync(x=>x.PostId == postId);
        }

        public async Task<(bool success, string message)> UpdatePostContentAsync(int postId,string? postHeader, string? postBody) {
            var post=await provider.Posts.FirstOrDefaultAsync(x=>x.PostId == postId);

            if (post == null) {
                return (false, Messages.Post.PostDoesntExistsOrDeleted);
            }

            post.PostHeader = postHeader;
            post.PostBody = postBody;

            try {
                provider.Posts.Update(post);
                await provider.SaveChangesAsync();
                return (true, Messages.Post.PostUpdatedSuccessfully);
            }catch(Exception) {
                return (false, Messages.InternalServerError);
            }
        }
    }
}
