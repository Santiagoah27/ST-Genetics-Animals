namespace STGeneticsAnimals.Models
{
    public class Animal
    {
        public Animal()
        {
            Name = string.Empty;
            Breed = string.Empty;
            Sex = string.Empty;
            Status = string.Empty;
        }

        public int AnimalId { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public DateTime BirthDate { get; set; }
        public string Sex { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
    }

}
