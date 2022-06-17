using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryItems.Models
{
    public class PackageModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public float VolumetricWeight { get; set; }
        public DeliveryItemStatesEnum PackageStatus { get; set; }
        public Guid DeliveryPointId { get; set; }
        public string Barcode { get; set; }

        public BagModel? AssignedBag { get; set; }
        //package should be assigned to same delivery point bag
    }
}