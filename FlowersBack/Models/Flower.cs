using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowersBack.Models
{
    public class Flower
    {
        [Key]
        public int FlowerId { get; set; }

        [Required(ErrorMessage = "Debes entrar una {0}")]
        [StringLength(30, ErrorMessage = "El campo {0} solo puede contener un maximo de {1} y un minimo de {2} caracteres", MinimumLength = 1)]
        [Index ("Flower_Description_Index",IsUnique =true)]
        public string Description { get; set; }
        [DisplayFormat(DataFormatString ="{0:C2}",ApplyFormatInEditMode =false)]

        [Required(ErrorMessage = "Debes entrar un {0}")]
        public decimal? Price { get; set; }
        //   public decimal? Price { get; set; } con la interrogacion significa que permite null

        //[DataType(DataType.Date)]
        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString  = "{0:yyyy/MM/dd}",ApplyFormatInEditMode= false  )]
        public DateTime? LastPurchase { get; set; }

        public string Image { get; set; }


        [Display(Name ="Is active")]
        public bool IsActive { get; set; }

        [DataType(DataType.MultilineText)]
        public string Observation { get; set; }


    }
}