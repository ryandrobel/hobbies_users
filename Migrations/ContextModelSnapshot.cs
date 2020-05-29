﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace exam2.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Enthusiast", b =>
                {
                    b.Property<int>("EnthusiastId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("HobbyId");

                    b.Property<int>("UserId");

                    b.HasKey("EnthusiastId");

                    b.HasIndex("HobbyId");

                    b.HasIndex("UserId");

                    b.ToTable("Enthusiasts");
                });

            modelBuilder.Entity("exam2.Models.Hobby", b =>
                {
                    b.Property<int>("HobbyId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<int>("UserId");

                    b.HasKey("HobbyId");

                    b.HasIndex("UserId");

                    b.ToTable("Hobbies");
                });

            modelBuilder.Entity("exam2.Models.LoginUser", b =>
                {
                    b.Property<int>("LoginUserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.HasKey("LoginUserId");

                    b.ToTable("Logins");
                });

            modelBuilder.Entity("exam2.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Enthusiast", b =>
                {
                    b.HasOne("exam2.Models.Hobby", "Hobbies")
                        .WithMany("Enthusiasts")
                        .HasForeignKey("HobbyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("exam2.Models.User", "User")
                        .WithMany("AdoptingHobby")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("exam2.Models.Hobby", b =>
                {
                    b.HasOne("exam2.Models.User", "Creator")
                        .WithMany("CreatedHobbies")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
