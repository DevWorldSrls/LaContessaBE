# LaContessaBE
This microservice manage a APIs call from Flutter project

# Create migrations
1. Go to Package Manager Console
2. Execute this command to add a new migration (replace '<migration-name>'):
   ```shell
   add-migration <migration-name> -Project "DevWorld.LaContessa.Persistance.Migrations" -StartupProject "DevWorld.LaContessa.API" -Context LaContessaDbContext -o MigrationsScripts
   ```
# Apply migrations
1. Go to Package Manager Console
2. Execute this command to update your database:
   ```shell
   update-database -args "UserID=postgres;Password=lacontessa;Host=localhost;Port=5432;Database=lacontessadb" -Project "DevWorld.LaContessa.Persistance.Migrations" -StartupProject "DevWorld.LaContessa.API" -Context LaContessaDbContext
   ```

# Important Note!
Until we release a first version of this BE, replace <migration-name> with "Initial" and remember to clean the contents of the folder "MigrationScripts" in project "DevWorld.LaContessa.Persistance.Migrations"
