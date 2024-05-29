
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
                //Agregar todos los clientes
                context.Clients.AddRange(clients);

                //Guardar Cambios
                context.SaveChanges();
            }



            if (!context.Accounts.Any())
            {
                Client santiClient = context.Clients.FirstOrDefault(cl => cl.Email == "szaurrini@gmail.com");
                if (santiClient != null)
                {
                    var santiAccounts = new Account[]
                    {
                        new Account {Number = "VIN001",CreationDate = DateTime.Now, ClientId = santiClient.Id, Balance=150000 },
                        new Account {Number = "VIN002",CreationDate = DateTime.Now, ClientId = santiClient.Id, Balance=912000 },
                    };
                    context.Accounts.AddRange(santiAccounts);

                    context.SaveChanges();
                }
            }

            if (!context.Transactions.Any())
            {
                Account account1 = context.Accounts.FirstOrDefault(ac => ac.Number == "VIN001");
                if(account1 != null)
                {
                    var accounTransactions = new Transaction[]
                    {
                        new Transaction { AccountId= account1.Id, Amount = 10000, Date= DateTime.Now.AddHours(-2),
                            Description = "Transferencia recibida", Type = TransactionType.CREDIT.ToString()},
                        new Transaction { AccountId= account1.Id, Amount = -5000, Date= DateTime.Now.AddMinutes(-1),
                            Description = "Compra exitosa!", Type = TransactionType.DEBIT.ToString()},
                        new Transaction { AccountId= account1.Id, Amount = 5000, Date= DateTime.Now.AddHours(-1),
                            Description = "Transferencia recibida", Type = TransactionType.CREDIT.ToString()},
                    };

                    context.Transactions.AddRange(accounTransactions);
                    context.SaveChanges();
                }
            }
        }
    }
}
