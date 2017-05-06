using System.ComponentModel.DataAnnotations.Schema;

namespace FlowersBack.Models
{
    [NotMapped]
    public class FlowerRequest :Flower
    {
        public byte[] ImageArray { get; set; }
    }
}