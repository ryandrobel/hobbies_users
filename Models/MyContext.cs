using exam2.Models;
using Microsoft.EntityFrameworkCore;

public class Context : DbContext {
        public Context(DbContextOptions options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
        
        }
        public DbSet<LoginUser> Logins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Enthusiast> Enthusiasts  { get; set; }
        public DbSet<Hobby> Hobbies{ get; set; }
}