using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using taskTracker.Data;

namespace taskTracker.Migrations
{
    [DbContext(typeof(taskTrackerContext))]
    partial class taskTrackerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

            modelBuilder.Entity("taskTracker.Models.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CompletedOn")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("strftime('%Y-%m-%d %H:%M:%S', 'now', 'localtime')");

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("TaskStatus");

                    b.HasKey("Id");

                    b.ToTable("Task");
                });
        }
    }
}
