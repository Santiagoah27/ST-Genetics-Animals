namespace STGeneticsAnimals.Queries
{
    public static class AnimalQueries
    {
        public const string CreateAnimal = "INSERT INTO Animal (Name, Breed, BirthDate, Sex, Price, Status) VALUES (@Name, @Breed, @BirthDate, @Sex, @Price, @Status)";
        public const string UpdateAnimal = "UPDATE Animal SET Name = @Name, Breed = @Breed, BirthDate = @BirthDate, Sex = @Sex, Price = @Price, Status = @Status WHERE AnimalId = @AnimalId";
        public const string GetAnimalById = "SELECT * FROM Animal WHERE AnimalId = @AnimalId";
        public const string DeleteAnimal = "DELETE FROM Animal WHERE AnimalId = @AnimalId";
    }
}
