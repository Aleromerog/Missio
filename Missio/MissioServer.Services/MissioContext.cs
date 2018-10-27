using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Missio.Users;
using MissioServer.Services.Services;

namespace MissioServer.Services
{
    public class MissioContext : DbContext
    {
        private readonly IPasswordHasher<User> _passwordService;
        private readonly IWebClientService _webClientService;

        public DbSet<User> Users { get; set; }

        public MissioContext(DbContextOptions<MissioContext> options, IPasswordHasher<User> passwordService, IWebClientService webClientService) : base(options)
        {
            _webClientService = webClientService;
            _passwordService = passwordService;
        }


        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var grecoImage = _webClientService.DownloadData("https://scontent.felp1-1.fna.fbcdn.net/v/t1.0-9/18342049_1371435649562155_317149840395279012_n.jpg?_nc_cat=0&oh=74b6c0226537899a74f499c25b3ddb07&oe=5C00CF82");
            var jorgeImage = _webClientService.DownloadData("https://scontent.felp1-1.fna.fbcdn.net/v/t1.0-9/26168930_10208309305130065_9014358028033259242_n.jpg?_nc_cat=0&oh=a6dc6203053aa3c830edffd107f346e4&oe=5BF1FC2B");
            var greco = new User("Francisco Greco", _passwordService.HashPassword("ElPass"), grecoImage, "myEmail@gmail.com", -1);
            var jorge = new User("Jorge Romero", _passwordService.HashPassword("Yolo"), jorgeImage, "anotherEmail@gmail.com", -2);
            modelBuilder.Entity<User>().HasData(greco, jorge);
        }
    }
}