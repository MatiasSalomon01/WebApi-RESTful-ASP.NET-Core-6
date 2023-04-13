﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PeliculasApi;

#nullable disable

namespace PeliculasApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "postgis");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("PeliculasApi.Entidades.Actor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Foto")
                        .HasColumnType("text");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("character varying(120)");

                    b.HasKey("Id");

                    b.ToTable("Actores");

                    b.HasData(
                        new
                        {
                            Id = 5,
                            FechaNacimiento = new DateTime(1962, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Nombre = "Jim Carrey"
                        },
                        new
                        {
                            Id = 6,
                            FechaNacimiento = new DateTime(1965, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Nombre = "Robert Downey Jr."
                        },
                        new
                        {
                            Id = 7,
                            FechaNacimiento = new DateTime(1981, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Nombre = "Chris Evans"
                        });
                });

            modelBuilder.Entity("PeliculasApi.Entidades.Genero", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Generos");

                    b.HasData(
                        new
                        {
                            Id = 4,
                            Nombre = "Aventura"
                        },
                        new
                        {
                            Id = 5,
                            Nombre = "Animación"
                        },
                        new
                        {
                            Id = 6,
                            Nombre = "Suspenso"
                        },
                        new
                        {
                            Id = 7,
                            Nombre = "Romance"
                        });
                });

            modelBuilder.Entity("PeliculasApi.Entidades.Pelicula", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("EnCines")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("FechaEstreno")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Poster")
                        .HasColumnType("text");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Peliculas");

                    b.HasData(
                        new
                        {
                            Id = 2,
                            EnCines = true,
                            FechaEstreno = new DateTime(2019, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Titulo = "Avengers: Endgame"
                        },
                        new
                        {
                            Id = 3,
                            EnCines = false,
                            FechaEstreno = new DateTime(2019, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Titulo = "Avengers: Infinity Wars"
                        },
                        new
                        {
                            Id = 4,
                            EnCines = false,
                            FechaEstreno = new DateTime(2020, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Titulo = "Sonic the Hedgehog"
                        },
                        new
                        {
                            Id = 5,
                            EnCines = false,
                            FechaEstreno = new DateTime(2020, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Titulo = "Emma"
                        },
                        new
                        {
                            Id = 6,
                            EnCines = false,
                            FechaEstreno = new DateTime(2020, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Titulo = "Wonder Woman 1984"
                        });
                });

            modelBuilder.Entity("PeliculasApi.Entidades.PeliculaActor", b =>
                {
                    b.Property<int>("ActorId")
                        .HasColumnType("integer");

                    b.Property<int>("PeliculaId")
                        .HasColumnType("integer");

                    b.Property<int>("Orden")
                        .HasColumnType("integer");

                    b.Property<string>("Personaje")
                        .HasColumnType("text");

                    b.HasKey("ActorId", "PeliculaId");

                    b.HasIndex("PeliculaId");

                    b.ToTable("PeliculasActores");

                    b.HasData(
                        new
                        {
                            ActorId = 6,
                            PeliculaId = 2,
                            Orden = 1,
                            Personaje = "Tony Stark"
                        },
                        new
                        {
                            ActorId = 7,
                            PeliculaId = 2,
                            Orden = 2,
                            Personaje = "Steve Rogers"
                        },
                        new
                        {
                            ActorId = 6,
                            PeliculaId = 3,
                            Orden = 1,
                            Personaje = "Tony Stark"
                        },
                        new
                        {
                            ActorId = 7,
                            PeliculaId = 3,
                            Orden = 2,
                            Personaje = "Steve Rogers"
                        },
                        new
                        {
                            ActorId = 5,
                            PeliculaId = 4,
                            Orden = 1,
                            Personaje = "Dr. Ivo Robotnik"
                        });
                });

            modelBuilder.Entity("PeliculasApi.Entidades.PeliculaGenero", b =>
                {
                    b.Property<int>("GeneroId")
                        .HasColumnType("integer");

                    b.Property<int>("PeliculaId")
                        .HasColumnType("integer");

                    b.HasKey("GeneroId", "PeliculaId");

                    b.HasIndex("PeliculaId");

                    b.ToTable("PeliculasGeneros");

                    b.HasData(
                        new
                        {
                            GeneroId = 6,
                            PeliculaId = 2
                        },
                        new
                        {
                            GeneroId = 4,
                            PeliculaId = 2
                        },
                        new
                        {
                            GeneroId = 6,
                            PeliculaId = 3
                        },
                        new
                        {
                            GeneroId = 4,
                            PeliculaId = 3
                        },
                        new
                        {
                            GeneroId = 4,
                            PeliculaId = 4
                        },
                        new
                        {
                            GeneroId = 6,
                            PeliculaId = 5
                        },
                        new
                        {
                            GeneroId = 7,
                            PeliculaId = 5
                        },
                        new
                        {
                            GeneroId = 6,
                            PeliculaId = 6
                        },
                        new
                        {
                            GeneroId = 4,
                            PeliculaId = 6
                        });
                });

            modelBuilder.Entity("PeliculasApi.Entidades.PeliculaSalaDeCine", b =>
                {
                    b.Property<int>("PeliculaId")
                        .HasColumnType("integer");

                    b.Property<int>("SalaDeCineId")
                        .HasColumnType("integer");

                    b.HasKey("PeliculaId", "SalaDeCineId");

                    b.HasIndex("SalaDeCineId");

                    b.ToTable("PeliculasSalasDeCines");
                });

            modelBuilder.Entity("PeliculasApi.Entidades.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Comentario")
                        .HasColumnType("text");

                    b.Property<int>("PeliculaId")
                        .HasColumnType("integer");

                    b.Property<int>("Puntuacion")
                        .HasColumnType("integer");

                    b.Property<string>("UsuarioId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PeliculaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("PeliculasApi.Entidades.SalaDeCine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("character varying(120)");

                    b.Property<Point>("Ubicacion")
                        .HasColumnType("geometry");

                    b.HasKey("Id");

                    b.ToTable("SalaDeCine");

                    b.HasData(
                        new
                        {
                            Id = 4,
                            Nombre = "Sambil",
                            Ubicacion = (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (-69.9118804 18.4826214)")
                        },
                        new
                        {
                            Id = 5,
                            Nombre = "Megacentro",
                            Ubicacion = (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (-69.856427 18.506934)")
                        },
                        new
                        {
                            Id = 6,
                            Nombre = "Village East Cinema",
                            Ubicacion = (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (-73.986227 40.730898)")
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PeliculasApi.Entidades.PeliculaActor", b =>
                {
                    b.HasOne("PeliculasApi.Entidades.Actor", "Actor")
                        .WithMany("PeliculaActores")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PeliculasApi.Entidades.Pelicula", "Pelicula")
                        .WithMany("PeliculaActores")
                        .HasForeignKey("PeliculaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Actor");

                    b.Navigation("Pelicula");
                });

            modelBuilder.Entity("PeliculasApi.Entidades.PeliculaGenero", b =>
                {
                    b.HasOne("PeliculasApi.Entidades.Genero", "Genero")
                        .WithMany("PeliculaGeneros")
                        .HasForeignKey("GeneroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PeliculasApi.Entidades.Pelicula", "Pelicula")
                        .WithMany("PeliculaGeneros")
                        .HasForeignKey("PeliculaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genero");

                    b.Navigation("Pelicula");
                });

            modelBuilder.Entity("PeliculasApi.Entidades.PeliculaSalaDeCine", b =>
                {
                    b.HasOne("PeliculasApi.Entidades.Pelicula", "Pelicula")
                        .WithMany("PeliculasSalaDeCines")
                        .HasForeignKey("PeliculaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PeliculasApi.Entidades.SalaDeCine", "SalaDeCine")
                        .WithMany("PeliculasSalaDeCines")
                        .HasForeignKey("SalaDeCineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pelicula");

                    b.Navigation("SalaDeCine");
                });

            modelBuilder.Entity("PeliculasApi.Entidades.Review", b =>
                {
                    b.HasOne("PeliculasApi.Entidades.Pelicula", "Pelicula")
                        .WithMany()
                        .HasForeignKey("PeliculaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId");

                    b.Navigation("Pelicula");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("PeliculasApi.Entidades.Actor", b =>
                {
                    b.Navigation("PeliculaActores");
                });

            modelBuilder.Entity("PeliculasApi.Entidades.Genero", b =>
                {
                    b.Navigation("PeliculaGeneros");
                });

            modelBuilder.Entity("PeliculasApi.Entidades.Pelicula", b =>
                {
                    b.Navigation("PeliculaActores");

                    b.Navigation("PeliculaGeneros");

                    b.Navigation("PeliculasSalaDeCines");
                });

            modelBuilder.Entity("PeliculasApi.Entidades.SalaDeCine", b =>
                {
                    b.Navigation("PeliculasSalaDeCines");
                });
#pragma warning restore 612, 618
        }
    }
}
