using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
namespace Evidence_01.Models
{
    public enum PlayerGrade
    {
        G01, G02, G03,
    }
    public class Sports
    {
        public Sports()
        {
            this.Players = new List<Player>();
        }
        public int SportsId { get; set; }
        [Required, StringLength(50)]
        [Display(Name = "Sports Name")]
        public string SportsName { get; set; }
        public virtual ICollection<Player> Players { get; set; }

    }
    public class Player
    {
        public int PlayerId { get; set; }
        [Required, StringLength(50), Display(Name = "Player Name")]
        public string PlayerName { get; set; }
        [Required, Display(Name = "Join Date")]
        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime JoinDate { get; set; }
        [EnumDataType(typeof(PlayerGrade))]
        public PlayerGrade? Grade { get; set; }
        public string PicturePath { get; set; }

        [Required]
        [ForeignKey("Sports")]
        public int SportsId { get; set; }
        public virtual Sports Sports { get; set; }

    }
    public class ClubDbContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            Database.SetInitializer(new DbInitializer());
        }
        public DbSet<Sports> Sports { get; set; }
        public DbSet<Player> Players { get; set; }

    }
    public class DbInitializer : DropCreateDatabaseIfModelChanges<ClubDbContext>
    {
        protected override void Seed(ClubDbContext context)
        {
            Sports s1 = new Sports { SportsName = "Cricket" };
            Sports s2 = new Sports { SportsName = "Football" };
        
            s1.Players.Add(new Player { PlayerName = "Shakib", Grade = PlayerGrade.G01, JoinDate = DateTime.Now.AddYears(-16), PicturePath = "~/Images/p.jpg" });
            s1.Players.Add(new Player { PlayerName = "Tamim", Grade = PlayerGrade.G01, JoinDate = DateTime.Now.AddYears(-15), PicturePath = "~/Images/pic.jpg" });

            s2.Players.Add(new Player { PlayerName = "Messi", Grade = PlayerGrade.G02, JoinDate = DateTime.Now.AddYears(-24), PicturePath = "~/Images/istockphoto-1193994027-612x612.jpg" });
            context.Sports.AddRange(new Sports[] { s1, s2,});
            context.SaveChanges();
            base.Seed(context);
        }
    }
}