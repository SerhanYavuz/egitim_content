using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Vehicle.Models
{
    public class VehicleModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set;}
        public string LicencePlate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        //TODO add created by
    }
}