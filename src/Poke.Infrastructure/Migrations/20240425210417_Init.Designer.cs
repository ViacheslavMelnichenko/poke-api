// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Poke.Infrastructure;

#nullable disable

namespace Poke.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240425210417_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Poke.Domain.Entities.Room", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CreatedByUserId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByUserId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("Poke.Domain.Entities.Session", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("RoomId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<int>("UserRole")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.HasIndex("UserId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("Poke.Domain.Entities.Ticket", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CreatedByUserId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<Guid>("RoomId")
                        .HasColumnType("uuid");

                    b.Property<int>("State")
                        .HasColumnType("integer");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("Poke.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NickName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Poke.Domain.Entities.Vote", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Mark")
                        .HasColumnType("integer");

                    b.Property<Guid>("TicketId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TicketId");

                    b.HasIndex("UserId");

                    b.ToTable("Votes");
                });

            modelBuilder.Entity("Poke.Domain.Entities.Room", b =>
                {
                    b.HasOne("Poke.Domain.Entities.User", "CreatedByUser")
                        .WithMany("RoomsCreated")
                        .HasForeignKey("CreatedByUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedByUser");
                });

            modelBuilder.Entity("Poke.Domain.Entities.Session", b =>
                {
                    b.HasOne("Poke.Domain.Entities.Room", "Room")
                        .WithMany("Sessions")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Poke.Domain.Entities.User", "User")
                        .WithMany("Sessions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Room");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Poke.Domain.Entities.Ticket", b =>
                {
                    b.HasOne("Poke.Domain.Entities.Room", "Room")
                        .WithMany("Tickets")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");
                });

            modelBuilder.Entity("Poke.Domain.Entities.Vote", b =>
                {
                    b.HasOne("Poke.Domain.Entities.Ticket", "Ticket")
                        .WithMany("Votes")
                        .HasForeignKey("TicketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Poke.Domain.Entities.User", "User")
                        .WithMany("Votes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Ticket");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Poke.Domain.Entities.Room", b =>
                {
                    b.Navigation("Sessions");

                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("Poke.Domain.Entities.Ticket", b =>
                {
                    b.Navigation("Votes");
                });

            modelBuilder.Entity("Poke.Domain.Entities.User", b =>
                {
                    b.Navigation("RoomsCreated");

                    b.Navigation("Sessions");

                    b.Navigation("Votes");
                });
#pragma warning restore 612, 618
        }
    }
}