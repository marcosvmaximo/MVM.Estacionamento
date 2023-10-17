using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MVM.Estacionamento.Data.Context;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt) : base(opt)
    {
    }
}


public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        var conectString = "Server=localhost;Port=3307;Database=meu-mysql;Uid=root;Pwd=8837;";

        optionsBuilder.UseMySql(conectString, ServerVersion.AutoDetect(conectString));

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}