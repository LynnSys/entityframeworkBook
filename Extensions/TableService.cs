using BookEntityFramework.Interfaces;
using BookEntityFramework.Repository;
using BookEntityFramework.Services;
using Microsoft.AspNetCore.Identity.Data;

namespace BookEntityFramework.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IUser, LoginRepository>();
            services.AddScoped<ISearch, SearchRepository>();
        }
    }
}
