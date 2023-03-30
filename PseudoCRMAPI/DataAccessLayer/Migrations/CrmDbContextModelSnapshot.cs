﻿// <auto-generated />
using System;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccessLayer.Migrations
{
    [DbContext(typeof(CrmDbContext))]
    partial class CrmDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Core.Email.Additional.ServerInformation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Port")
                        .HasColumnType("int");

                    b.Property<int>("SecureSocketOptions")
                        .HasColumnType("int");

                    b.Property<string>("Server")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ServerProtocol")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ServerInformation");
                });

            modelBuilder.Entity("Core.Email.EmailCredentials", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PublicName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ServerProtocols")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("EmailCredentials");
                });

            modelBuilder.Entity("Core.Email.EmailCredentialsServerInformation", b =>
                {
                    b.Property<int>("EmailCredentialsId")
                        .HasColumnType("int");

                    b.Property<int>("ServerInformationId")
                        .HasColumnType("int");

                    b.HasKey("EmailCredentialsId", "ServerInformationId");

                    b.HasIndex("ServerInformationId");

                    b.ToTable("EmailCredentialsServerInformation");
                });

            modelBuilder.Entity("Core.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Core.Email.EmailCredentials", b =>
                {
                    b.HasOne("Core.User", null)
                        .WithMany("Emails")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Core.Email.EmailCredentialsServerInformation", b =>
                {
                    b.HasOne("Core.Email.EmailCredentials", "EmailCredentials")
                        .WithMany("ServerInformations")
                        .HasForeignKey("EmailCredentialsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Email.Additional.ServerInformation", "ServerInformation")
                        .WithMany()
                        .HasForeignKey("ServerInformationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EmailCredentials");

                    b.Navigation("ServerInformation");
                });

            modelBuilder.Entity("Core.Email.EmailCredentials", b =>
                {
                    b.Navigation("ServerInformations");
                });

            modelBuilder.Entity("Core.User", b =>
                {
                    b.Navigation("Emails");
                });
#pragma warning restore 612, 618
        }
    }
}