using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace GemyGames.Models
{
    public class Game : BaseEntity
    {
        [MaxLength(2500)]
        public String Discription { get; set; } = String.Empty;

        [MaxLength(250)]
        public String Cover { get; set; } = String.Empty;

        [ForeignKey("categorie")]
        public int CategorieId { get; set; }

        public Categorie categorie { get; set; } = default!;

        public ICollection<GameDevice> Devices{ get; set; } = new List<GameDevice>();



    }
}

//protected override void OnModelCreating(ModelBuilder modelBuilder)
//{
//    modelBuilder.Entity<Categories>()
//        .HasData(new Categories[]
//        {

//            new Categories {Id=1 , Name ="Sports"},
//            new Categories {Id=2 , Name ="Action"},
//            new Categories {Id=3 , Name ="Adventure"},
//            new Categories {Id=4 , Name ="Racing"},
//            new Categories {Id=5 , Name ="Fighting"},
//            new Categories {Id=6 , Name ="Film"}
//        });

//    modelBuilder.Entity<Devices>()
//        .HasData(new Devices[]
//   {
//       new Devices {Id=1 ,Name="Playstation",Icon="bi bi-playstation" },
//       new Devices {Id=2 ,Name="Xbox",Icon="bi bi-xbox" },
//       new Devices {Id=3 ,Name="Nintendo",Icon="bi bi-nintendo-switch" },
//       new Devices {Id=4 ,Name="Pc",Icon="bi bi-pc-display" }
//   });


//    modelBuilder.Entity<GameDevices>().HasKey(e =>new { e.DeviceId, e.GameId });
//    base.OnModelCreating(modelBuilder);
//}


//var configuration = new ConfigurationBuilder()
//                .AddJsonFile("appsettings.json")
//                .Build();

//var constr = configuration.GetSection("ConnectionStrings").Value;
//optionsBuilder.UseSqlServer(constr);

//base.OnConfiguring(optionsBuilder);