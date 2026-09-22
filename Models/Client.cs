namespace Lab2_Baranov.Models
{
    public class Client
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public int CompletedOrders { get; set; }


        public bool IsRegularClient()
        {
            if (CompletedOrders >= 5) {
                return true;
            }
            else {
                return false;
            }
        }

    }
}