using System;
using System.Collections.Generic;
using KFitServer.DBContext.Models;
using Microsoft.EntityFrameworkCore;

namespace KFitServer.DBContext;

public partial class KfitContext : DbContext
{
    public KfitContext()
    {
    }

    public KfitContext(DbContextOptions<KfitContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Target> Targets { get; set; }

    public virtual DbSet<TrainCategory> TrainCategories { get; set; }

    public virtual DbSet<TrainingMetadatum> TrainingMetadata { get; set; }

    public virtual DbSet<TypeOfMeal> TypeOfMeals { get; set; }

    public virtual DbSet<UserPersonalStatistic> UserPersonalStatistics { get; set; }

    public virtual DbSet<UserTrainingStatistic> UserTrainingStatistics { get; set; }

    public virtual DbSet<UsersNutritionStatistic> UsersNutritionStatistics { get; set; }

    public virtual DbSet<UsersPersonalParameter> UsersPersonalParameters { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=kfit;Username=postgres;Password=tyuKo467");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Gender>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("gender_pk");

            entity.ToTable("gender");

            entity.Property(e => e.Id)
                .HasColumnType("character varying")
                .HasColumnName("id");
            entity.Property(e => e.GenderName)
                .HasColumnType("character varying")
                .HasColumnName("gender_name");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("products_pk");

            entity.ToTable("products");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.ProductCalories)
                .HasPrecision(6, 2)
                .HasColumnName("product_calories");
            entity.Property(e => e.ProductCarbohydrates)
                .HasPrecision(6, 2)
                .HasColumnName("product_carbohydrates");
            entity.Property(e => e.ProductDefaultG)
                .HasPrecision(6, 2)
                .HasColumnName("product_default_g");
            entity.Property(e => e.ProductFats)
                .HasPrecision(6, 2)
                .HasColumnName("product_fats");
            entity.Property(e => e.ProductName)
                .HasColumnType("character varying")
                .HasColumnName("product_name");
            entity.Property(e => e.ProductProtein)
                .HasPrecision(6, 2)
                .HasColumnName("product_protein");
        });

        modelBuilder.Entity<Target>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("target_pk");

            entity.ToTable("target");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.TargetName)
                .HasColumnType("character varying")
                .HasColumnName("target_name");
        });

        modelBuilder.Entity<TrainCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("train_categories_pk");

            entity.ToTable("train_categories");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.TrainCategoryName)
                .HasColumnType("character varying")
                .HasColumnName("train_category_name");
        });

        modelBuilder.Entity<TrainingMetadatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("training_metadata_pk");

            entity.ToTable("training_metadata");

            entity.Property(e => e.Id)
                .HasColumnType("character varying")
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasColumnType("character varying")
                .HasColumnName("description");
            entity.Property(e => e.ThumbnailUrl)
                .HasColumnType("character varying")
                .HasColumnName("thumbnail_url");
            entity.Property(e => e.Titlle)
                .HasColumnType("character varying")
                .HasColumnName("titlle");
            entity.Property(e => e.TrainCategoryId).HasColumnName("train_category_id");

            entity.HasOne(d => d.TrainCategory).WithMany(p => p.TrainingMetadata)
                .HasForeignKey(d => d.TrainCategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("training_metadata_fk_category");
        });

        modelBuilder.Entity<TypeOfMeal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("type_of_meal_pk");

            entity.ToTable("type_of_meal");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.TypeName)
                .HasColumnType("character varying")
                .HasColumnName("type_name");
        });

        modelBuilder.Entity<UserPersonalStatistic>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_personal_statistic_pk");

            entity.ToTable("user_personal_statistic");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.RecordDate).HasColumnName("record_date");
            entity.Property(e => e.UserChestGirth)
                .HasPrecision(5, 2)
                .HasColumnName("user_chest_girth");
            entity.Property(e => e.UserHipGirth)
                .HasPrecision(5, 2)
                .HasColumnName("user_hip_girth");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.UserWaistCircumference)
                .HasPrecision(5, 2)
                .HasColumnName("user_waist_circumference");
            entity.Property(e => e.UserWeight)
                .HasPrecision(5, 2)
                .HasColumnName("user_weight");

            entity.HasOne(d => d.User).WithMany(p => p.UserPersonalStatistics)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("user_personal_statistic_fk");
        });

        modelBuilder.Entity<UserTrainingStatistic>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_training_statistic_pk");

            entity.ToTable("user_training_statistic");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.TrainingDate).HasColumnName("training_date");
            entity.Property(e => e.TrainingId)
                .HasColumnType("character varying")
                .HasColumnName("training_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Training).WithMany(p => p.UserTrainingStatistics)
                .HasForeignKey(d => d.TrainingId)
                .HasConstraintName("user_training_statistic_fk");

            entity.HasOne(d => d.User).WithMany(p => p.UserTrainingStatistics)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("user_training_statistic_fk_user");
        });

        modelBuilder.Entity<UsersNutritionStatistic>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_nutrition_statistics_pk");

            entity.ToTable("users_nutrition_statistics");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.NutritionDate).HasColumnName("nutrition_date");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.TypeOfMealId).HasColumnName("type_of_meal_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Product).WithMany(p => p.UsersNutritionStatistics)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("users_nutrition_statistics_fk_product");

            entity.HasOne(d => d.TypeOfMeal).WithMany(p => p.UsersNutritionStatistics)
                .HasForeignKey(d => d.TypeOfMealId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("users_nutrition_statistics_fk_meal_type");

            entity.HasOne(d => d.User).WithMany(p => p.UsersNutritionStatistics)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("users_nutrition_statistics_fk_user");
        });

        modelBuilder.Entity<UsersPersonalParameter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_personal_parameters_pkey");

            entity.ToTable("users_personal_parameters");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.UserDateOfBirth).HasColumnName("user_date_of_birth");
            entity.Property(e => e.UserGender)
                .HasColumnType("character varying")
                .HasColumnName("user_gender");
            entity.Property(e => e.UserHeight)
                .HasPrecision(5, 2)
                .HasColumnName("user_height");
            entity.Property(e => e.UserTarget).HasColumnName("user_target");
            entity.Property(e => e.UserToken)
                .HasColumnType("character varying")
                .HasColumnName("user_token");

            entity.HasOne(d => d.UserGenderNavigation).WithMany(p => p.UsersPersonalParameters)
                .HasForeignKey(d => d.UserGender)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("users_personal_parameters_fk_gender");

            entity.HasOne(d => d.UserTargetNavigation).WithMany(p => p.UsersPersonalParameters)
                .HasForeignKey(d => d.UserTarget)
                .HasConstraintName("users_personal_parameters_fk_target");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
