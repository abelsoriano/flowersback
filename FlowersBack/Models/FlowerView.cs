using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace FlowersBack.Models
{
    [NotMapped]
    public class FlowerView : Flower //: flower lo normal seria heredar, pero en este caso particular no: Flower
    {
        
        //public int FlowerId { get; set; }

        //[Required(ErrorMessage = "Debes entrar una {0}")]
        //[StringLength(30, ErrorMessage = "El campo {0} solo puede contener un maximo de {1} y un minimo de {2} caracteres", MinimumLength = 1)]
        
        //public string Description { get; set; }
        //[DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]

        //[Required(ErrorMessage = "Debes entrar un {0}")]
        //public decimal? Price { get; set; }
        ////   public decimal? Price { get; set; } con la interrogacion significa que permite null

        ////[DataType(DataType.Date)]
        //[Display(Name = "Last purchase")]
        //public DateTime? LastPurchase { get; set; }
        [Display(Name = "Image")]
        public HttpPostedFileBase ImageFile { get; set; }
        //public string Image { get; set; }


        //[Display(Name = "Is active")]
        //public bool IsActive { get; set; }

        //[DataType(DataType.MultilineText)]
        //public string Observation { get; set; }

       
    }
}