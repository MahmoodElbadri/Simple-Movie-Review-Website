﻿// <auto-generated />
using System;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Entities.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class MovieDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Entities.Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GenreId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("GenreId");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            GenreId = 1,
                            Name = "Action"
                        },
                        new
                        {
                            GenreId = 2,
                            Name = "Drama"
                        },
                        new
                        {
                            GenreId = 3,
                            Name = "Comedy"
                        });
                });

            modelBuilder.Entity("Entities.Movie", b =>
                {
                    b.Property<int>("MovieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MovieId"));

                    b.Property<float>("AverageRating")
                        .HasColumnType("real");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("MovieId");

                    b.HasIndex("GenreId");

                    b.ToTable("Movies");

                    b.HasData(
                        new
                        {
                            MovieId = 1,
                            AverageRating = 4.8f,
                            Description = "A mind-bending thriller by Christopher Nolan.",
                            GenreId = 1,
                            ReleaseDate = new DateTime(2010, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Inception"
                        },
                        new
                        {
                            MovieId = 2,
                            AverageRating = 4.9f,
                            Description = "A classic mafia drama directed by Francis Ford Coppola.",
                            GenreId = 2,
                            ReleaseDate = new DateTime(1972, 3, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "The Godfather"
                        },
                        new
                        {
                            MovieId = 3,
                            AverageRating = 4.5f,
                            Description = "A comedy about a Las Vegas bachelor party gone wrong.",
                            GenreId = 3,
                            ReleaseDate = new DateTime(2009, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "The Hangover"
                        });
                });

            modelBuilder.Entity("Entities.Review", b =>
                {
                    b.Property<int>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReviewId"));

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<float>("Rating")
                        .HasColumnType("real");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ReviewId");

                    b.HasIndex("MovieId");

                    b.ToTable("Reviews");

                    b.HasData(
                        new
                        {
                            ReviewId = 1,
                            Comment = "Amazing movie! A must-watch for sci-fi fans.",
                            CreatedAt = new DateTime(2024, 10, 14, 22, 28, 27, 52, DateTimeKind.Local).AddTicks(3648),
                            MovieId = 1,
                            Rating = 5f,
                            UserId = "testuser1"
                        },
                        new
                        {
                            ReviewId = 2,
                            Comment = "An iconic masterpiece of cinema.",
                            CreatedAt = new DateTime(2024, 10, 14, 22, 28, 27, 52, DateTimeKind.Local).AddTicks(3704),
                            MovieId = 2,
                            Rating = 5f,
                            UserId = "testuser2"
                        });
                });

            modelBuilder.Entity("Entities.Movie", b =>
                {
                    b.HasOne("Entities.Genre", "Genre")
                        .WithMany("Movies")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("Entities.Review", b =>
                {
                    b.HasOne("Entities.Movie", "Movie")
                        .WithMany("Reviews")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("Entities.Genre", b =>
                {
                    b.Navigation("Movies");
                });

            modelBuilder.Entity("Entities.Movie", b =>
                {
                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
