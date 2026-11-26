using System;
using FailSafeWeatherUppgift.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FailSafeWeatherUppgift.Migrations;

[DbContext(typeof(WeatherDbContext))]
partial class WeatherDbContextModelSnapshot : ModelSnapshot
{
    public override string LastMigrationId => "20261125141123_InitialCreate";

    protected override void BuildModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "11.0.0")
            .HasAnnotation("Relational:MaxIdentifierLength", 128);

        SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

        modelBuilder.Entity("FailSafeWeatherUppgift.Models.WeatherDataPoint", b =>
        {
            var id = b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("int");

            SqlServerPropertyBuilderExtensions.UseIdentityColumn(id);

            b.Property<string>("DataSource")
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            b.Property<double>("Humidity")
                .HasColumnType("float");

            b.Property<string>("StationName")
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            b.Property<double>("Temperature")
                .HasColumnType("float");

            b.Property<DateTime>("Timestamp")
                .HasColumnType("datetime2");

            b.HasKey("Id");

            b.ToTable("WeatherDataPoints");
        });
#pragma warning restore 612, 618
    }
}