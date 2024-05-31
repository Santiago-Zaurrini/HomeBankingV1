using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Drawing;

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
                    new Client { Email = "szaurrini@gmail.com", FirstName="Santiago", LastName="Zaurrini", Password="31912"},
                    new Client { Email = "vcoronado@gmail.com", FirstName="Victor", LastName="Coronado", Password="123456"},
                    new Client { Email = "jperez@gmail.com", FirstName="Juan", LastName="Perez", Password="123457"},
                    new Client { Email = "mlopez@gmail.com", FirstName="Maria", LastName="Lopez", Password="123458"}
                    
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

            if (!context.Loans.Any())
            {
                var loans = new Loan[]
                {
                    new Loan { Name = "Hipotecario", MaxAmount = 500000, Payments = "12,24,36,48,60" },
                    new Loan { Name = "Personal", MaxAmount = 100000, Payments = "6,12,24" },
                    new Loan { Name = "Automotriz", MaxAmount = 300000, Payments = "6,12,24,36" },
                };
                context.Loans.AddRange(loans);
                context.SaveChanges();

                var client1 = context.Clients.FirstOrDefault(c => c.Email == "szaurrini@gmail.com");
                if (client1 != null)
                {
                    var loan1 = context.Loans.FirstOrDefault(l => l.Name == "Hipotecario");
                    if (loan1 != null)
                    {
                        var clientLoan1 = new ClientLoan
                        {
                            Amount = 400000,
                            ClientId = client1.Id,
                            LoanId = loan1.Id,
                            Payments = "60"
                        };
                        context.ClientLoans.Add(clientLoan1);
                    }
                    var loan2 = context.Loans.FirstOrDefault(l => l.Name == "Personal");
                    if (loan2 != null)
                    {
                        var clientLoan2 = new ClientLoan
                        {
                            Amount = 50000,
                            ClientId = client1.Id,
                            LoanId = loan2.Id,
                            Payments = "12"
                        };
                        context.ClientLoans.Add(clientLoan2);
                    }
                    var loan3 = context.Loans.FirstOrDefault(l => l.Name == "Automotriz");
                    if (loan3 != null)
                    {
                        var clientLoan3 = new ClientLoan
                        {
                            Amount = 100000,
                            ClientId = client1.Id,
                            LoanId = loan3.Id,
                            Payments = "24"
                        };
                        context.ClientLoans.Add(clientLoan3);
                    }

                    context.SaveChanges();
                }
            }
            if (!context.Cards.Any())
            {
                var client1 = context.Clients.FirstOrDefault(cl => cl.Email == "szaurrini@gmail.com");
                if (client1 != null)
                {
                    var cards = new Card[]
                    {
                        new Card {
                                ClientId= client1.Id,
                                CardHolder = client1.FirstName + " " + client1.LastName,
                                Type = CardType.DEBIT.ToString(),
                                Color = CardColor.GOLD.ToString(),
                                Number = "3325-6745-7876-4445",
                                Cvv = 990,
                                FromDate= DateTime.Now,
                                ThruDate= DateTime.Now.AddYears(4)
                        },
                        new Card {
                                ClientId= client1.Id,
                                CardHolder = client1.FirstName + " " + client1.LastName,
                                Type = CardType.CREDIT.ToString(),
                                Color = CardColor.TITANIUM.ToString(),
                                Number = "2234-6745-552-7888",
                                Cvv = 750,
                                FromDate= DateTime.Now,
                                ThruDate= DateTime.Now.AddYears(5),
                        },
                    };
                    context.Cards.AddRange(cards);
                    context.SaveChanges();
                }
            }
        }
    }
}
