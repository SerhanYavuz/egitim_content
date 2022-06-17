using System.ComponentModel.DataAnnotations;

namespace DeliveryItems
{
    public enum DeliveryItemStatesEnum
    {
         /*
        Created 1
        Loaded into Bag 2
        Loaded 3
        Unloaded 4
        */
        [Display(Name = "Created")]
        Created = 1,
        [Display(Name = "Loaded Into Bag")]
        LoadedIntoBag = 2,
        [Display(Name = "Loaded")]
        Loaded = 3,
        [Display(Name = "Unloaded")]
        Unloaded = 4
    }
}