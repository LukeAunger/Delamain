using System;
using System.Reflection.Metadata;

namespace Delamain_backend.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Queuemodel>()
            .HasOne(e => e.userdetail)
        .WithOne(e => e.Queuemodel)
            .HasForeignKey<Userdetail>(e => e.queueId)
            .IsRequired();
    }

    public DbSet<userReqmodel> userReqmodels { get; set; }

    public DbSet<HospitalLocation> HospitalLocations { get; set; }

    public DbSet<IcuData> IcuDatas { get; set; }

    public DbSet<Riskmodel> riskmodals { get; set; }

    public DbSet<Queuemodel> queuemodels { get; set; }

    public DbSet<Userdetail> userdetails { get; set; }

    public DbSet<Login> Logins { get; set; }
}
//To open manager you need to go through the console command lines and find the root directory of this project first

//dotnet ef dbcontext scaffold "Host=localhost; Database=AandEBacklog; Username=postgres; Password=1111;"
//Npgsql.EntityFrameworkCore.PostgreSQL -outputdir Models -ContextDir Data -Context AandEContext -dataannotations
//Command used for retreiving new tables created in pgadmin as apposed to this app


