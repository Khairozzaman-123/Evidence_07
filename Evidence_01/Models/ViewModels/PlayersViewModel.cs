using Evidence_01.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace Evidence_01.Models.ViewModels
{
    public class PlayersViewModel
    {
        public int PlayerId { get; set; }
        [Required, StringLength(50), Display(Name = "Player Name")]
        public string PlayerName { get; set; }
        [Required, Display(Name = "Join Date")]
        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [LessDate]
        public DateTime JoinDate { get; set; }
        [EnumDataType(typeof(PlayerGrade))]
        public PlayerGrade? Grade { get; set; }
        public string PicturePath { get; set; }
        public HttpPostedFileBase Picture { get; set; }

        [Required]
        [ForeignKey("Sports")]
        public int SportsId { get; set; }
        public virtual Sports Sports { get; set; }
    }
}