# win-it

<h2>Useful commands</h2>

<h3>For database migrations</h3> </br>

`dotnet ef migrations add initial --startup-project ..\..\Presentation\Presentation\Presentation.csproj`
`dotnet ef database update --startup-project ..\..\Presentation\Presentation\Presentation.csproj`</br>


<h3>For Database scaffolding</h3>

`Scaffold-DbContext "server=localhost;userid=root;password=root;database=winit" Pomelo.EntityFrameworkCore.MySql -OutputDir Models`
