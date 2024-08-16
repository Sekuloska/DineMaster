namespace AdminApplication.Models
{
    public class MenuItem
    {
        public Guid Id { get; set; }
        public Guid MenuId { get; set; }
        public Menu Menu { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public bool IsAvailable { get; set; }
        public string ItemImage { get; set; }
        public virtual ICollection<ItemInOrder>? ItemInOrders { get; set; }
    }
}
