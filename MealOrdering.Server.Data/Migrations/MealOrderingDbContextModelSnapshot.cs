﻿// <auto-generated />
using System;
using MealOrdering.Server.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MealOrdering.Server.Data.Migrations
{
    [DbContext(typeof(MealOrderingDbContext))]
    partial class MealOrderingDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("public")
                .HasAnnotation("ProductVersion", "6.0.22")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MealOrdering.Server.Data.Models.OrderItems", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasDefaultValueSql("public.uuid_generate_v4()");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("createdate")
                        .HasDefaultValueSql("NOW()");

                    b.Property<Guid>("CreateUserId")
                        .HasColumnType("uuid")
                        .HasColumnName("created_user_id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying")
                        .HasColumnName("description");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid")
                        .HasColumnName("order_id");

                    b.HasKey("Id")
                        .HasName("pk_orderItem_id");

                    b.HasIndex("CreateUserId");

                    b.HasIndex("OrderId");

                    b.ToTable("order_items", "public");
                });

            modelBuilder.Entity("MealOrdering.Server.Data.Models.Orders", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasDefaultValueSql("public.uuid_generate_v4()");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("createdate")
                        .HasDefaultValueSql("NOW()");

                    b.Property<Guid>("CreatedUserId")
                        .HasColumnType("uuid")
                        .HasColumnName("created_user_id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying")
                        .HasColumnName("description");

                    b.Property<DateTime>("ExpireDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("expire_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying")
                        .HasColumnName("name");

                    b.Property<Guid>("SupplierId")
                        .HasColumnType("uuid")
                        .HasColumnName("supplier_id");

                    b.HasKey("Id")
                        .HasName("pk_order_id");

                    b.HasIndex("CreatedUserId");

                    b.HasIndex("SupplierId");

                    b.ToTable("orders", "public");
                });

            modelBuilder.Entity("MealOrdering.Server.Data.Models.Suppliers", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasDefaultValueSql("public.uuid_generate_v4()");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("createdate")
                        .HasDefaultValueSql("NOW()");

                    b.Property<bool?>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasColumnName("isactive")
                        .HasDefaultValueSql("true");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying")
                        .HasColumnName("name");

                    b.Property<string>("WebURL")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying")
                        .HasColumnName("web_url");

                    b.HasKey("Id")
                        .HasName("pk_supplier_id");

                    b.ToTable("suppliers", "public");
                });

            modelBuilder.Entity("MealOrdering.Server.Data.Models.Users", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasDefaultValueSql("UUID_GENERATE_V4()");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("create_date")
                        .HasDefaultValueSql("NOW()");

                    b.Property<string>("EMailAddress")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying")
                        .HasColumnName("email_address");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying")
                        .HasColumnName("first_name");

                    b.Property<bool?>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasColumnName("isactive")
                        .HasDefaultValueSql("true");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying")
                        .HasColumnName("last_name");

                    b.HasKey("Id")
                        .HasName("pk_user_id");

                    b.ToTable("user", "public");
                });

            modelBuilder.Entity("MealOrdering.Server.Data.Models.OrderItems", b =>
                {
                    b.HasOne("MealOrdering.Server.Data.Models.Users", "CreatedUser")
                        .WithMany("CreatedOrderItems")
                        .HasForeignKey("CreateUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_orderitems_user_id");

                    b.HasOne("MealOrdering.Server.Data.Models.Orders", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_orderitems_order_id");

                    b.Navigation("CreatedUser");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("MealOrdering.Server.Data.Models.Orders", b =>
                {
                    b.HasOne("MealOrdering.Server.Data.Models.Users", "CreatedUser")
                        .WithMany("Orders")
                        .HasForeignKey("CreatedUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_order_id");

                    b.HasOne("MealOrdering.Server.Data.Models.Suppliers", "Supplier")
                        .WithMany("Orders")
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_supplier_order_id");

                    b.Navigation("CreatedUser");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("MealOrdering.Server.Data.Models.Orders", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("MealOrdering.Server.Data.Models.Suppliers", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("MealOrdering.Server.Data.Models.Users", b =>
                {
                    b.Navigation("CreatedOrderItems");

                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}