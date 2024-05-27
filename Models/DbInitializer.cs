namespace HomeBanking.Models
{
    public class DbInitializer
    {
        public static void Initialize(HomeBankingContext context)
        {
            if (!context.Clients.Any())
            {
                var clients = new Client[]
                {
                    new Client { Email = "vcoronado@gmail.com", FirstName="Victor", LastName="Coronado", Password="123456"},
                    new Client { Email = "jperez@gmail.com", FirstName="Juan", LastName="Perez", Password="123457"},
                    new Client { Email = "mlopez@gmail.com", FirstName="Maria", LastName="Lopez", Password="123458"}
                };

                context.Clients.AddRange(clients);

                //Guardar Cambios
                context.SaveChanges();
            }

        }
    }
}
