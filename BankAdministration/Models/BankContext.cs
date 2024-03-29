﻿namespace BankAdministration.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public partial class BankContext : IdentityDbContext<IdentityUser>
{
    public BankContext()
    {
    }

    public BankContext(DbContextOptions<BankContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; } = null!;
    public virtual DbSet<Card> Cards { get; set; } = null!;
    public virtual DbSet<Country> Countries { get; set; } = null!;
    public virtual DbSet<Customer> Customers { get; set; } = null!;
    public virtual DbSet<Disposition> Dispositions { get; set; } = null!;
    public virtual DbSet<Loan> Loans { get; set; } = null!;
    public virtual DbSet<PermenentOrder> PermenentOrders { get; set; } = null!;
    public virtual DbSet<Transaction> Transactions { get; set; } = null!;
    public virtual DbSet<User> Users { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Account>(entity =>
        {
            entity.Property(e => e.Balance).HasColumnType("decimal(13, 2)");
            entity.Property(e => e.Created).HasColumnType("date");
            entity.Property(e => e.Frequency).HasMaxLength(50);
        });

        modelBuilder.Entity<Card>(entity =>
        {
            entity.Property(e => e.Ccnumber)
                .HasMaxLength(50)
                .HasColumnName("CCNumber");

            entity.Property(e => e.Cctype)
                .HasMaxLength(50)
                .HasColumnName("CCType");

            entity.Property(e => e.Cvv2)
                .HasMaxLength(10)
                .HasColumnName("CVV2");

            entity.Property(e => e.Issued).HasColumnType("date");

            entity.Property(e => e.Type).HasMaxLength(50);

            entity.HasOne(d => d.Disposition)
                .WithMany(p => p.Cards)
                .HasForeignKey(d => d.DispositionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cards_Dispositions");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.Property(e => e.CountryCode).HasMaxLength(2);
            entity.Property(e => e.CountryName).HasMaxLength(50);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property(e => e.Birthday).HasColumnType("date");
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.CountryId).HasDefaultValueSql("((1))");
            entity.Property(e => e.Emailaddress).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(6);
            entity.Property(e => e.Givenname).HasMaxLength(100);
            entity.Property(e => e.NationalId).HasMaxLength(20);
            entity.Property(e => e.Streetaddress).HasMaxLength(100);
            entity.Property(e => e.Surname).HasMaxLength(100);
            entity.Property(e => e.Telephonecountrycode).HasMaxLength(10);
            entity.Property(e => e.Telephonenumber).HasMaxLength(25);
            entity.Property(e => e.Zipcode).HasMaxLength(15);
            entity.HasOne(d => d.Country)
                .WithMany(p => p.Customers)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Customers_Countries");
        });

        modelBuilder.Entity<Disposition>(entity =>
        {
            entity.Property(e => e.Type).HasMaxLength(50);
            entity.HasOne(d => d.Account)
                .WithMany(p => p.Dispositions)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Dispositions_Accounts");

            entity.HasOne(d => d.Customers)
                .WithMany(p => p.Dispositions)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Dispositions_Customers");
        });

        modelBuilder.Entity<Loan>(entity =>
        {
            entity.Property(e => e.Amount).HasColumnType("decimal(13, 2)");
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.Payments).HasColumnType("decimal(13, 2)");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.HasOne(d => d.Account)
                .WithMany(p => p.Loans)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Loans_Accounts");
        });

        modelBuilder.Entity<PermenentOrder>(entity =>
        {
            entity.HasKey(e => e.OrderId);
            entity.ToTable("PermenentOrder");
            entity.Property(e => e.AccountTo).HasMaxLength(50);
            entity.Property(e => e.Amount).HasColumnType("decimal(13, 2)");
            entity.Property(e => e.BankTo).HasMaxLength(50);
            entity.Property(e => e.Symbol).HasMaxLength(50);
            entity.HasOne(d => d.Account)
                .WithMany(p => p.PermenentOrders)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PermenentOrder_Accounts");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasIndex(e => e.AccountId, "IX_Transactions_AccountId");
            entity.Property(e => e.Account).HasMaxLength(50);
            entity.Property(e => e.Amount).HasColumnType("decimal(13, 2)");
            entity.Property(e => e.Balance).HasColumnType("decimal(13, 2)");
            entity.Property(e => e.Bank).HasMaxLength(50);
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.Operation).HasMaxLength(50);
            entity.Property(e => e.Symbol).HasMaxLength(50);
            entity.Property(e => e.Type).HasMaxLength(50);
            entity.HasOne(d => d.AccountNavigation)
                .WithMany(p => p.Transactions)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transactions_Accounts");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.FirstName).HasMaxLength(40);
            entity.Property(e => e.LastName).HasMaxLength(40);
            entity.Property(e => e.LoginName).HasMaxLength(40);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(64)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
