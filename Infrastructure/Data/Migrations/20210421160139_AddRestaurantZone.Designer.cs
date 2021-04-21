﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Data.Migrations
{
    [DbContext(typeof(TitsDbContext))]
    [Migration("20210421160139_AddRestaurantZone")]
    partial class AddRestaurantZone
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Models.Db.Account.AccountBase", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Login")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Accounts");

                    b.HasDiscriminator<string>("Discriminator").HasValue("AccountBase");
                });

            modelBuilder.Entity("Models.Db.Account.AccountToRole", b =>
                {
                    b.Property<long>("WorkerRoleId")
                        .HasColumnType("bigint");

                    b.Property<long>("WorkerAccountId")
                        .HasColumnType("bigint");

                    b.Property<long?>("AccountBaseId")
                        .HasColumnType("bigint");

                    b.HasKey("WorkerRoleId", "WorkerAccountId");

                    b.HasIndex("AccountBaseId");

                    b.HasIndex("WorkerAccountId");

                    b.ToTable("WorkerAccountToRoles");
                });

            modelBuilder.Entity("Models.Db.Account.WorkerRole", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("TitleEn")
                        .HasColumnType("text");

                    b.Property<string>("TitleRu")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("WorkerRoles");
                });

            modelBuilder.Entity("Models.Db.CourierMessage", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<long>("CourierAccountId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreationDateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsFromCourier")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("CourierAccountId");

                    b.ToTable("CourierMessages");
                });

            modelBuilder.Entity("Models.Db.CourierSession", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime?>("CloseDateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("CourierAccountId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsClosed")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("OpenDateTime")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("CourierAccountId");

                    b.ToTable("WorkerSessions");
                });

            modelBuilder.Entity("Models.Db.Delivery", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("CourierAccountId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("OrderId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("Status")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CourierAccountId");

                    b.HasIndex("OrderId");

                    b.ToTable("Deliveries");
                });

            modelBuilder.Entity("Models.Db.LatLng", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long?>("DeliveryId")
                        .HasColumnType("bigint");

                    b.Property<float>("Lat")
                        .HasColumnType("real");

                    b.Property<float>("Lng")
                        .HasColumnType("real");

                    b.Property<long?>("OrderId")
                        .HasColumnType("bigint");

                    b.Property<long?>("RestaurantLocationId")
                        .HasColumnType("bigint");

                    b.Property<long?>("RestaurantZoneId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("DeliveryId");

                    b.HasIndex("OrderId");

                    b.HasIndex("RestaurantZoneId");

                    b.ToTable("LatLngs");
                });

            modelBuilder.Entity("Models.Db.Order", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("AddressAdditional")
                        .HasColumnType("text");

                    b.Property<string>("AddressString")
                        .HasColumnType("text");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreationDateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("DestinationLatLngId")
                        .HasColumnType("bigint");

                    b.Property<long>("RestaurantId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("DestinationLatLngId");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Models.Db.Restaurant", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("AddressString")
                        .HasColumnType("text");

                    b.Property<long>("LocationLatLngId")
                        .HasColumnType("bigint");

                    b.Property<bool>("UseAutoDeliveryServer")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("LocationLatLngId")
                        .IsUnique();

                    b.ToTable("Restaurants");
                });

            modelBuilder.Entity("Models.Db.SosRequest", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("CourierAccountId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreationDateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("ResolveDateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long?>("ResolverManagerAccountId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CourierAccountId");

                    b.HasIndex("ResolverManagerAccountId");

                    b.ToTable("SosRequests");
                });

            modelBuilder.Entity("Models.Db.TokenSessions.TokenSessionBase", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Token")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("TokenSessions");

                    b.HasDiscriminator<string>("Discriminator").HasValue("TokenSessionBase");
                });

            modelBuilder.Entity("Models.Db.Account.CourierAccount", b =>
                {
                    b.HasBaseType("Models.Db.Account.AccountBase");

                    b.Property<long>("AssignedToRestaurantId")
                        .HasColumnType("bigint");

                    b.Property<long?>("LastCourierSessionId")
                        .HasColumnType("bigint");

                    b.Property<long?>("LastLatLngId")
                        .HasColumnType("bigint");

                    b.Property<long?>("LastTokenSessionId")
                        .HasColumnType("bigint");

                    b.HasIndex("AssignedToRestaurantId");

                    b.HasIndex("LastCourierSessionId");

                    b.HasIndex("LastLatLngId");

                    b.HasIndex("LastTokenSessionId");

                    b.HasDiscriminator().HasValue("CourierAccount");
                });

            modelBuilder.Entity("Models.Db.Account.ManagerAccount", b =>
                {
                    b.HasBaseType("Models.Db.Account.AccountBase");

                    b.Property<long?>("LastTokenSessionId")
                        .HasColumnType("bigint")
                        .HasColumnName("ManagerAccount_LastTokenSessionId");

                    b.HasIndex("LastTokenSessionId");

                    b.HasDiscriminator().HasValue("ManagerAccount");
                });

            modelBuilder.Entity("Models.Db.TokenSessions.CourierTokenSession", b =>
                {
                    b.HasBaseType("Models.Db.TokenSessions.TokenSessionBase");

                    b.Property<long>("CourierAccountId")
                        .HasColumnType("bigint");

                    b.HasIndex("CourierAccountId");

                    b.HasDiscriminator().HasValue("CourierTokenSession");
                });

            modelBuilder.Entity("Models.Db.TokenSessions.ManagerTokenSession", b =>
                {
                    b.HasBaseType("Models.Db.TokenSessions.TokenSessionBase");

                    b.Property<long>("ManagerAccountId")
                        .HasColumnType("bigint");

                    b.HasIndex("ManagerAccountId");

                    b.HasDiscriminator().HasValue("ManagerTokenSession");
                });

            modelBuilder.Entity("Models.Db.Account.AccountToRole", b =>
                {
                    b.HasOne("Models.Db.Account.AccountBase", null)
                        .WithMany("WorkerRoles")
                        .HasForeignKey("AccountBaseId");

                    b.HasOne("Models.Db.Account.CourierAccount", "CourierAccount")
                        .WithMany()
                        .HasForeignKey("WorkerAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Db.Account.WorkerRole", "WorkerRole")
                        .WithMany("Users")
                        .HasForeignKey("WorkerRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CourierAccount");

                    b.Navigation("WorkerRole");
                });

            modelBuilder.Entity("Models.Db.CourierMessage", b =>
                {
                    b.HasOne("Models.Db.Account.CourierAccount", "CourierAccount")
                        .WithMany("CourierMessages")
                        .HasForeignKey("CourierAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CourierAccount");
                });

            modelBuilder.Entity("Models.Db.CourierSession", b =>
                {
                    b.HasOne("Models.Db.Account.CourierAccount", "CourierAccount")
                        .WithMany()
                        .HasForeignKey("CourierAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CourierAccount");
                });

            modelBuilder.Entity("Models.Db.Delivery", b =>
                {
                    b.HasOne("Models.Db.Account.CourierAccount", "CourierAccount")
                        .WithMany("Deliveries")
                        .HasForeignKey("CourierAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Db.Order", "Order")
                        .WithMany("Deliveries")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CourierAccount");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Models.Db.LatLng", b =>
                {
                    b.HasOne("Models.Db.Delivery", "Delivery")
                        .WithMany("LatLngs")
                        .HasForeignKey("DeliveryId");

                    b.HasOne("Models.Db.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId");

                    b.HasOne("Models.Db.Restaurant", "RestaurantZone")
                        .WithMany("ZoneLatLngs")
                        .HasForeignKey("RestaurantZoneId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Delivery");

                    b.Navigation("Order");

                    b.Navigation("RestaurantZone");
                });

            modelBuilder.Entity("Models.Db.Order", b =>
                {
                    b.HasOne("Models.Db.LatLng", "DestinationLatLng")
                        .WithMany()
                        .HasForeignKey("DestinationLatLngId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Db.Restaurant", "Restaurant")
                        .WithMany("Orders")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DestinationLatLng");

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("Models.Db.Restaurant", b =>
                {
                    b.HasOne("Models.Db.LatLng", "LocationLatLng")
                        .WithOne("RestaurantLocation")
                        .HasForeignKey("Models.Db.Restaurant", "LocationLatLngId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.Navigation("LocationLatLng");
                });

            modelBuilder.Entity("Models.Db.SosRequest", b =>
                {
                    b.HasOne("Models.Db.Account.CourierAccount", "CourierAccount")
                        .WithMany("SosRequests")
                        .HasForeignKey("CourierAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Db.Account.ManagerAccount", "ResolverManagerAccount")
                        .WithMany()
                        .HasForeignKey("ResolverManagerAccountId");

                    b.Navigation("CourierAccount");

                    b.Navigation("ResolverManagerAccount");
                });

            modelBuilder.Entity("Models.Db.Account.CourierAccount", b =>
                {
                    b.HasOne("Models.Db.Restaurant", "AssignedToRestaurant")
                        .WithMany("AssignedCouriers")
                        .HasForeignKey("AssignedToRestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Db.CourierSession", "LastCourierSession")
                        .WithMany()
                        .HasForeignKey("LastCourierSessionId");

                    b.HasOne("Models.Db.LatLng", "LastLatLng")
                        .WithMany()
                        .HasForeignKey("LastLatLngId");

                    b.HasOne("Models.Db.TokenSessions.CourierTokenSession", "LastTokenSession")
                        .WithMany()
                        .HasForeignKey("LastTokenSessionId");

                    b.Navigation("AssignedToRestaurant");

                    b.Navigation("LastCourierSession");

                    b.Navigation("LastLatLng");

                    b.Navigation("LastTokenSession");
                });

            modelBuilder.Entity("Models.Db.Account.ManagerAccount", b =>
                {
                    b.HasOne("Models.Db.TokenSessions.ManagerTokenSession", "LastTokenSession")
                        .WithMany()
                        .HasForeignKey("LastTokenSessionId");

                    b.Navigation("LastTokenSession");
                });

            modelBuilder.Entity("Models.Db.TokenSessions.CourierTokenSession", b =>
                {
                    b.HasOne("Models.Db.Account.CourierAccount", "CourierAccount")
                        .WithMany()
                        .HasForeignKey("CourierAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CourierAccount");
                });

            modelBuilder.Entity("Models.Db.TokenSessions.ManagerTokenSession", b =>
                {
                    b.HasOne("Models.Db.Account.ManagerAccount", "ManagerAccount")
                        .WithMany()
                        .HasForeignKey("ManagerAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ManagerAccount");
                });

            modelBuilder.Entity("Models.Db.Account.AccountBase", b =>
                {
                    b.Navigation("WorkerRoles");
                });

            modelBuilder.Entity("Models.Db.Account.WorkerRole", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Models.Db.Delivery", b =>
                {
                    b.Navigation("LatLngs");
                });

            modelBuilder.Entity("Models.Db.LatLng", b =>
                {
                    b.Navigation("RestaurantLocation");
                });

            modelBuilder.Entity("Models.Db.Order", b =>
                {
                    b.Navigation("Deliveries");
                });

            modelBuilder.Entity("Models.Db.Restaurant", b =>
                {
                    b.Navigation("AssignedCouriers");

                    b.Navigation("Orders");

                    b.Navigation("ZoneLatLngs");
                });

            modelBuilder.Entity("Models.Db.Account.CourierAccount", b =>
                {
                    b.Navigation("CourierMessages");

                    b.Navigation("Deliveries");

                    b.Navigation("SosRequests");
                });
#pragma warning restore 612, 618
        }
    }
}
