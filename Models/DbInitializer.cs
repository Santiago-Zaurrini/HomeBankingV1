namespace HomeBanking.Models
{
    public class DbInitializer
    {
        //Método para inicializar la base de datos
        public static void Initialize(HomeBankingContext context)
        {
            if (!context.Clients.Any())
            {
                var clients = new Client[]
                {
                    new Client { Email = "vcoronado@gmail.com", FirstName="Victor", LastName="Coronado", Password="123456"},
                    new Client { Email = "jperez@gmail.com", FirstName="Juan", LastName="Perez", Password="123457"},
                    new Client { Email = "mlopez@gmail.com", FirstName="Maria", LastName="Lopez", Password="123458"},
                    new Client { Email = "szaurrini@gmail.com", FirstName="Santiago", LastName="Zaurrini", Password="31912"}
                };

                context.Clients.AddRange(clients);

                //Guardar Cambios
                context.SaveChanges();
            }

        }
    }
}
