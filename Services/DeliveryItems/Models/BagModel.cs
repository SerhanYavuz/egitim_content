using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryItems.Models
{
    public class BagModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Barcode { get; set; }
        public Guid DeliveryPointId { get; set; }
        public ICollection<PackageModel> Packages { get; set; }
        public DeliveryItemStatesEnum BagStatus { get; set; }

    }
}