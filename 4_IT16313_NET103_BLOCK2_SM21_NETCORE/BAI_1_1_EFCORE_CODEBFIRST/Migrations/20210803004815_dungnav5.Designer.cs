// <auto-generated />
using System;
using BAI_1_1_EFCORE_CODEBFIRST.DBContext_FPOLY;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BAI_1_1_EFCORE_CODEBFIRST.Migrations
{
    [DbContext(typeof(DBContext_Dungna))]
    [Migration("20210803004815_dungnav5")]
    partial class dungnav5
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BAI_1_1_EFCORE_CODEBFIRST.Models.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Acc")
                        .HasMaxLength(28)
                        .HasColumnType("nvarchar(28)");

                    b.Property<Guid?>("IdRole")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Pass")
                        .HasMaxLength(28)
                        .HasColumnType("nvarchar(28)");

                    b.Property<bool?>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("IdRole");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("BAI_1_1_EFCORE_CODEBFIRST.Models.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Name")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("BAI_1_1_EFCORE_CODEBFIRST.Models.Account", b =>
                {
                    b.HasOne("BAI_1_1_EFCORE_CODEBFIRST.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("IdRole");

                    b.Navigation("Role");
                });
#pragma warning restore 612, 618
        }
    }
}
