using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TodoList.Model;

public partial class TodoContext : DbContext
{
    public TodoContext()
    {
    }

    public TodoContext(DbContextOptions<TodoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cpr> Cprs { get; set; }

    public virtual DbSet<ToDo> ToDos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Todo;Trusted_Connection=True;MultipleActiveResultSets=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cpr>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity
                .ToTable("CPR");

            entity.Property(e => e.Cpr1)
                .HasMaxLength(500)
                .HasColumnName("CPR");
            entity.Property(e => e.User).HasMaxLength(200);
        });

        modelBuilder.Entity<ToDo>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity
                .ToTable("ToDo");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
