namespace Lab2_Baranov.Models
{
    public class RepairOrder
    {
        public int Id { get; set; }

        public int CarId { get; set; }

        public Car? Car { get; set; }

        public string Description { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }

        public decimal WorkCost { get; set; }

        public decimal PartsCost { get; set; }

        public string Status { get; set; } = string.Empty;

        public decimal GetTotalCost()
        {
            return WorkCost + PartsCost;
        }

        public void CompleteOrder()
        {
            Status = "Completed";
        }
    }
}