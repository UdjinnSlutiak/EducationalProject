namespace EquipmentControll.Domain.Models
{
    using System.Linq;
    using Microsoft.EntityFrameworkCore;

    public class SeedData
    {
        public static void EnsureDataPopulated(ProjectContext context)
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (context.Equipments.Count() == 0 && context.Records.Count() == 0
                && context.Users.Count() == 0)
            {
                context.Users.AddRange(
                    new User
                    {
                        FirstName = "User",
                        LastName = "One",
                        Username = "username1",
                        Password = "password1",
                        Role = "User" 
                    },
                    new User
                    {
                        FirstName = "User",
                        LastName = "Two",
                        Username = "username2",
                        Password = "password2",
                        Role = "Moderator"
                    },
                    new User
                    {
                        FirstName = "User",
                        LastName = "Three",
                        Username = "username3",
                        Password = "password3",
                        Role = "Administrator"
                    });

                context.SaveChanges();
            }
        }
    }
}
