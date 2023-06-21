namespace dotnet_rpg.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }

        // 當模型創建時會插入資料
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skill>().HasData(
                 new Skill { Id = 1, Name = "Fireball", Damage = 30 },
                 new Skill { Id = 2, Name = "Frenzy", Damage = 20 },
                 new Skill { Id = 3, Name = "Blizzard", Damage = 50 }
            );
        }

        // Dbset 為建立 Characters 的資料表
        public DbSet<Character> Characters => Set<Character>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Weapon> Weapons => Set<Weapon>();
        public DbSet<Skill> Skills => Set<Skill>();
    }
}
