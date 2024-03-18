using KFitServer.BusinessLogic.DBContext.Models;
using Microsoft.EntityFrameworkCore;

namespace KFitServer.BusinessLogic.DBContext;

public partial class KfitContext : DbContext
{
    public KfitContext()
    {
    }

    public KfitContext(DbContextOptions<KfitContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductsInNutrition> ProductsInNutritions { get; set; }

    public virtual DbSet<Target> Targets { get; set; }

    public virtual DbSet<TypeOfMeal> TypeOfMeals { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserPersonalStatistic> UserPersonalStatistics { get; set; }

    public virtual DbSet<UserTrainingStatistic> UserTrainingStatistics { get; set; }

    public virtual DbSet<UsersNutrition> UsersNutritions { get; set; }

    public virtual DbSet<UsersParameter> UsersParameters { get; set; }

    public virtual DbSet<WeekDay> WeekDays { get; set; }

    public virtual DbSet<YoutubeVideo> YoutubeVideos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=kfit;Username=postgres;Password=nb^teg62");

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
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("gender_name");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("products_pk");

            entity.ToTable("products");

            entity.Property(e => e.Id)
                .HasColumnType("character varying")
                .HasColumnName("id");
            entity.Property(e => e.Calories).HasColumnName("calories");
            entity.Property(e => e.Carbohydrates).HasColumnName("carbohydrates");
            entity.Property(e => e.ImageUrl)
                .HasColumnType("character varying")
                .HasColumnName("image_url");
            entity.Property(e => e.Lipids).HasColumnName("lipids");
            entity.Property(e => e.ProductName)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("product_name");
            entity.Property(e => e.Proteins).HasColumnName("proteins");
        });

        modelBuilder.Entity<ProductsInNutrition>(entity =>
        {
            entity.HasKey(e => new { e.ProductId, e.NutritionId }).HasName("products_in_nutrition_pk");

            entity.ToTable("products_in_nutrition");

            entity.Property(e => e.ProductId)
                .HasColumnType("character varying")
                .HasColumnName("product_id");
            entity.Property(e => e.NutritionId).HasColumnName("nutrition_id");
            entity.Property(e => e.MealId).HasColumnName("meal_id");
            entity.Property(e => e.ProductCount).HasColumnName("product_count");

            entity.HasOne(d => d.Meal).WithMany(p => p.ProductsInNutritions)
                .HasForeignKey(d => d.MealId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("products_in_nutrition_fk");

            entity.HasOne(d => d.Nutrition).WithMany(p => p.ProductsInNutritions)
                .HasForeignKey(d => d.NutritionId)
                .HasConstraintName("products_in_nutrition_fk_1");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductsInNutritions)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("products_in_nutrition_fk_2");
        });

        modelBuilder.Entity<Target>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("target_pk");

            entity.ToTable("target");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.TargetName)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("target_name");
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

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_pk");

            entity.ToTable("user");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AuthToken)
                .HasColumnType("character varying")
                .HasColumnName("auth_token");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.Login)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("login");
            entity.Property(e => e.PasswordHash)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("password_hash");
            entity.Property(e => e.Salt)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("salt");
        });

        modelBuilder.Entity<UserPersonalStatistic>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_personal_statistic_pk");

            entity.ToTable("user_personal_statistic");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CaloriesNorm).HasColumnName("calories_norm");
            entity.Property(e => e.Carbohydrates).HasColumnName("carbohydrates");
            entity.Property(e => e.Lipids).HasColumnName("lipids");
            entity.Property(e => e.Proteins).HasColumnName("proteins");
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
            entity.Property(e => e.WaterNorm)
                .HasPrecision(3, 2)
                .HasColumnName("water_norm");
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
        });

        modelBuilder.Entity<UsersNutrition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_nutrition_statistics_pk");

            entity.ToTable("users_nutrition");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.NutritionDate).HasColumnName("nutrition_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Water)
                .HasPrecision(3, 2)
                .HasColumnName("water");

            entity.HasOne(d => d.User).WithMany(p => p.UsersNutritions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("users_nutrition_statistics_fk");
        });

        modelBuilder.Entity<UsersParameter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_parameters_pk");

            entity.ToTable("users_parameters");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CaloriesNorm).HasColumnName("calories_norm");
            entity.Property(e => e.CarbohydratesNorm).HasColumnName("carbohydrates_norm");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.Gender)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("gender");
            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.LipidsNorm).HasColumnName("lipids_norm");
            entity.Property(e => e.ProteinsNorm).HasColumnName("proteins_norm");
            entity.Property(e => e.Target).HasColumnName("target");
            entity.Property(e => e.TargetWeight).HasColumnName("target_weight");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.WaterNorm).HasColumnName("water_norm");

            entity.HasOne(d => d.GenderNavigation).WithMany(p => p.UsersParameters)
                .HasForeignKey(d => d.Gender)
                .HasConstraintName("users_personal_parameters_fk");

            entity.HasOne(d => d.TargetNavigation).WithMany(p => p.UsersParameters)
                .HasForeignKey(d => d.Target)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("users_parameters_fk_target");

            entity.HasOne(d => d.User).WithMany(p => p.UsersParameters)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("users_parameters_fk");
        });

        modelBuilder.Entity<WeekDay>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("week_day_pk");

            entity.ToTable("week_day");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<YoutubeVideo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("youtube_videos_pk");

            entity.ToTable("youtube_videos");

            entity.Property(e => e.Id)
                .HasColumnType("character varying")
                .HasColumnName("id");
            entity.Property(e => e.ThumbnailUrl)
                .HasColumnType("character varying")
                .HasColumnName("thumbnail_url");
            entity.Property(e => e.Title)
                .HasColumnType("character varying")
                .HasColumnName("title");
            entity.Property(e => e.WeekDay).HasColumnName("week_day");

            entity.HasOne(d => d.WeekDayNavigation).WithMany(p => p.YoutubeVideos)
                .HasForeignKey(d => d.WeekDay)
                .HasConstraintName("youtube_videos_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
