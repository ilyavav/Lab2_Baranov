namespace Lab2_Baranov.Models
{
    public class Car
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public Client? Client { get; set; }

        public string Brand { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        public int Year { get; set; }

        public string LicensePlate { get; set; } = string.Empty;

        public int Mileage { get; set; }

        public bool UpdateMileage(int newMileage)
        {
            if (newMileage >= Mileage)
            {
                Mileage = newMileage;
                return true;
            }

            return false;
        }
    }
}