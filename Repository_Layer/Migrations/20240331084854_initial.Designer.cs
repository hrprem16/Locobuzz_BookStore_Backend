﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repository_Layer.Context;

#nullable disable

namespace RepositoryLayer.Migrations
{
    [DbContext(typeof(BookStoreContext))]
    [Migration("20240331084854_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Repository_Layer.Entity.BookEntity", b =>
                {
                    b.Property<int>("Book_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Book_id"));

                    b.Property<string>("Book_Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Book_Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Book_Discount_Price")
                        .HasColumnType("int");

                    b.Property<int>("Book_Price")
                        .HasColumnType("int");

                    b.Property<int>("Book_Quantity")
                        .HasColumnType("int");

                    b.Property<string>("Book_image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Book_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("Book_id");

                    b.HasIndex("userId");

                    b.ToTable("BookTable");
                });

            modelBuilder.Entity("Repository_Layer.Entity.CartEntity", b =>
                {
                    b.Property<int>("CartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CartId"));

                    b.Property<int>("Book_id")
                        .HasColumnType("int");

                    b.Property<bool>("IsPurchase")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("OrderAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("CartId");

                    b.HasIndex("Book_id");

                    b.HasIndex("userId");

                    b.ToTable("CartTable");
                });

            modelBuilder.Entity("Repository_Layer.Entity.UserEntity", b =>
                {
                    b.Property<int>("userId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("userId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobileNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserRole")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("userId");

                    b.ToTable("UserTable");
                });

            modelBuilder.Entity("Repository_Layer.Entity.WishlistEntity", b =>
                {
                    b.Property<int>("Wishlist_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Wishlist_Id"));

                    b.Property<int>("Book_id")
                        .HasColumnType("int");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("Wishlist_Id");

                    b.HasIndex("Book_id");

                    b.HasIndex("userId");

                    b.ToTable("WishlistTable");
                });

            modelBuilder.Entity("Repository_Layer.Entity.BookEntity", b =>
                {
                    b.HasOne("Repository_Layer.Entity.UserEntity", "AddedBy")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("AddedBy");
                });

            modelBuilder.Entity("Repository_Layer.Entity.CartEntity", b =>
                {
                    b.HasOne("Repository_Layer.Entity.BookEntity", "AddedFor")
                        .WithMany()
                        .HasForeignKey("Book_id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Repository_Layer.Entity.UserEntity", "AddedBy")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("AddedBy");

                    b.Navigation("AddedFor");
                });

            modelBuilder.Entity("Repository_Layer.Entity.WishlistEntity", b =>
                {
                    b.HasOne("Repository_Layer.Entity.BookEntity", "WishlistFor")
                        .WithMany()
                        .HasForeignKey("Book_id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Repository_Layer.Entity.UserEntity", "WishlistBy")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("WishlistBy");

                    b.Navigation("WishlistFor");
                });
#pragma warning restore 612, 618
        }
    }
}