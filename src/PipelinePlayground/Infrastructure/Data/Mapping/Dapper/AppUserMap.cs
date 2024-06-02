using Dapper.FluentMap.Mapping;
using PipelinePlayground.Entities;

namespace PipelinePlayground.Infrastructure.Data.Mapping.Dapper
{
    public class UserMap : EntityMap<AppUser>
    {
        public UserMap()
        {
            Map(u => u.Id).ToColumn("id");
            Map(u => u.CategoryId).ToColumn("category_id");
            Map(u => u.Name).ToColumn("name");
            Map(u => u.Email).ToColumn("email");
            Map(u => u.Password).ToColumn("password");
        }
    }
}
