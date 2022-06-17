using System.ComponentModel.DataAnnotations;

namespace DeliveryPoints
{
    public enum DeliveryPointTypes
    {
        [Display(Name = "Branch")]
        Branch = 1,
        [Display(Name = "Transfer Center")]
        TransferCenter = 2,
        [Display(Name = "Distribution Center")]
        DistributionCenter = 3
    }
}