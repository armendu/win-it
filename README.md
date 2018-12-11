# win-it

Useful commands:

dotnet ef migrations add initial --startup-project ..\..\Presentation\Presentation\Presentation.csproj
dotnet ef database update --startup-project ..\..\Presentation\Presentation\Presentation.csproj

Scaffold-DbContext "server=localhost;userid=root;password=root;database=winit" Pomelo.EntityFrameworkCore.MySql -OutputDir Models