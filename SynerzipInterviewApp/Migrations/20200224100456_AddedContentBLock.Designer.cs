﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SynerzipInterviewApp.Models;

namespace SynerzipInterviewApp.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20200224100456_AddedContentBLock")]
    partial class AddedContentBLock
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SynerzipInterviewApp.Models.ContentBlock", b =>
                {
                    b.Property<long>("ContentBlockId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<string>("Query");

                    b.Property<string>("VisualizationType");

                    b.HasKey("ContentBlockId");

                    b.ToTable("ContentBlocks");
                });

            modelBuilder.Entity("SynerzipInterviewApp.Models.Interview", b =>
                {
                    b.Property<long>("InterviewId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CandidateName");

                    b.Property<string>("ContactPerson");

                    b.Property<string>("MobileNo");

                    b.Property<DateTime?>("dateofInterview");

                    b.HasKey("InterviewId");

                    b.ToTable("Interviews");
                });
#pragma warning restore 612, 618
        }
    }
}
