# Ticky


### Developement

#### Database Migration using `dotnet ef`
1. Add Migration
   
   `dotnet ef migrations add "Initial" -o "Persistence/Migrations" -s "../Ticky.API.Admin/Ticky.API.Admin.csproj"`

1. Update database

   `dotnet ef database update -s "../Ticky.API.Admin/Ticky.API.Admin.csproj"`