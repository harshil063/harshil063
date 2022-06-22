Microsoft.VisualStudio.Web.CodeGeneration.Design

Microsoft.EntityFrameworkCore.Tools

Microsoft.EntityFrameworkCore.SqlServer

Microsoft.VisualStudio.Web.CodeGeneration

Microsoft.VisualStudio.Web.CodeGeneration.Util


Scaffold-DbContext "Connection String;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models


,
  "ConnectionStrings": {
    "DBConnectionStrings": "Connection String;"
  }


services.AddDbContext<DICTDBContext>(o => o.UseSqlServer(Configuration.GetConnectionString("DBConnectionStrings")));



ConfigurationBuilder confBuilder = new ConfigurationBuilder();
                optionsBuilder.UseSqlServer(confBuilder.Build().GetSection("ConnectionStrings:DBConnectionStrings").Value);
