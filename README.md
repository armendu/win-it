# win-it

<h2>Useful commands</h2>

<h3>For database migrations</h3>
Open CLI inside the project and change the directory to the DataAccess project using:

`cd .\Data\DataAccess\`

Then execute the two following commands:

`dotnet ef migrations add initial --startup-project ..\..\Presentation\Presentation\Presentation.csproj`


`dotnet ef database update --startup-project ..\..\Presentation\Presentation\Presentation.csproj`

<h3>For Database scaffolding</h3>

Use the following commands for database scaffolding:

`Scaffold-DbContext "server=localhost;userid=root;password=root;database=winit" Pomelo.EntityFrameworkCore.MySql -OutputDir Models`
