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

            if (!context.Equipments.Any() && !context.Records.Any()
                && !context.Users.Any())
            {
                context.Users.AddRange(
                    new User
                    {
                        FirstName = "User",
                        LastName = "One",
                        Username = "username1",
                        PasswordHash = "hashed_password1",
                        Role = "User" 
                    },
                    new User
                    {
                        FirstName = "User",
                        LastName = "Two",
                        Username = "username2",
                        PasswordHash = "hashed_password2",
                        Role = "Moderator"
                    },
                    new User
                    {
                        FirstName = "User",
                        LastName = "Three",
                        Username = "username3",
                        PasswordHash = "hashed_password3",
                        Role = "Administrator"
                    });

                context.SaveChanges();
            }
        }
    }
}
