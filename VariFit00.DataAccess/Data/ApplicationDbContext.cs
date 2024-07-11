using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VariFit00.Models;

namespace VariFit00.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Muscle> Muscles { get; set; }
        public DbSet<MuscleExercise> MuscleExercises { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<WorkoutExercise> WorkoutExercises { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<UserFavoriteWorkout> UserFavoriteWorkouts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<WorkoutExercise>()
                .HasKey(we => new { we.WorkoutId, we.ExerciseId });
            modelBuilder.Entity<WorkoutExercise>()
                .HasOne(w => w.Workout)
                .WithMany(w => w.WorkoutExercises)
                .HasForeignKey(w => w.WorkoutId);
            modelBuilder.Entity<WorkoutExercise>()
                .HasOne(e => e.Exercise)
                .WithMany(e => e.WorkoutExercises)
                .HasForeignKey(e => e.ExerciseId);

            modelBuilder.Entity<MuscleExercise>()
                .HasKey(me => new { me.MuscleId, me.ExerciseId });
            modelBuilder.Entity<MuscleExercise>()
                .HasOne(m => m.Muscle)
                .WithMany(me => me.MuscleExercises)
                .HasForeignKey(m => m.MuscleId);
            modelBuilder.Entity<MuscleExercise>()
                .HasOne(e => e.Exercise)
                .WithMany(me => me.MuscleExercises)
                .HasForeignKey(e=> e.ExerciseId);



            modelBuilder.Entity<UserFavoriteWorkout>()
                .HasKey(ufw => new { ufw.ApplicationUserId, ufw.WorkoutId });
            modelBuilder.Entity<UserFavoriteWorkout>()
                .HasOne(u => u.ApplicationUser)
                .WithMany(uf => uf.UserFavoriteWorkouts)
                .HasForeignKey(ufw => ufw.ApplicationUserId);
            modelBuilder.Entity<UserFavoriteWorkout>()
                .HasOne(ufw => ufw.Workout)
                .WithMany(w => w.UserFavoriteWorkouts)
                .HasForeignKey(ufw => ufw.WorkoutId);

            modelBuilder.Entity<Muscle>().HasData(
                new Muscle { Id = 1, Name = "Bicep", GroupOfMuscle = "Arms" },
                new Muscle { Id = 2, Name = "Tricep", GroupOfMuscle = "Arms" },
                new Muscle { Id = 3, Name = "Chest", GroupOfMuscle = "Chest" },
                new Muscle { Id = 4, Name = "Shoulders", GroupOfMuscle = "Back" },
                new Muscle { Id = 5, Name = "Traps", GroupOfMuscle = "Back" },
                new Muscle { Id = 6, Name = "Back", GroupOfMuscle = "Back" },
                new Muscle { Id = 7, Name = "Quadriceps", GroupOfMuscle = "Legs" },
                new Muscle { Id = 8, Name = "Glutes", GroupOfMuscle = "Legs" },
                new Muscle { Id = 9, Name = "Hamstrings", GroupOfMuscle = "Legs" },
                new Muscle { Id = 10, Name = "Calves", GroupOfMuscle = "Legs" },
                new Muscle { Id = 11, Name = "Abs", GroupOfMuscle = "Core" }
            );

            modelBuilder.Entity<Equipment>().HasData(
                new Equipment { Id = 1, Name = "Bodyweight" },
                new Equipment { Id = 2, Name = "Dumbbell" },
                new Equipment { Id = 3, Name = "Barbell" },
                new Equipment { Id = 4, Name = "Kettlebell" },
                new Equipment { Id = 5, Name = "Band" },
                new Equipment { Id = 6, Name = "Bench" }
            );

            modelBuilder.Entity<Exercise>().HasData(
                new Exercise
                {
                    Id = 1,
                    Name = "Xbody_hammer_curl",
                    Level = 3,
                    EquipmentId = 2
                },
                new Exercise
                {
                    Id = 2,
                    Name = "Db_trifecta",
                    Level = 4,
                    EquipmentId = 2
                },
                new Exercise
                {
                    Id = 3,
                    Name = "Standing_curl",
                    Level = 2,
                    EquipmentId = 2
                },
                new Exercise
                {
                    Id = 4,
                    Name = "Alternate_st_curl",
                    Level = 3,
                    EquipmentId = 2
                },
                new Exercise
                {
                    Id = 5,
                    Name = "Lying_tricep_ext",
                    Level = 2,
                    EquipmentId = 2
                },
                new Exercise
                {
                    Id = 6,
                    Name = "Tricep_kickback",
                    Level = 1,
                    EquipmentId = 2
                },
                new Exercise
                {
                    Id = 7,
                    Name = "Bench_dip",
                    Level = 2,
                    EquipmentId = 2
                },
                new Exercise
                {
                    Id = 8,
                    Name = "Close_grip_db_pushup",
                    Level = 1,
                    EquipmentId = 1
                },
                new Exercise
                {
                    Id = 9,
                    Name = "Cobra_pushup",
                    Level = 2,
                    EquipmentId = 1
                },
                new Exercise
                {
                    Id = 10,
                    Name = "Pushup",
                    Level = 1,
                    EquipmentId = 1
                },
                new Exercise
                {
                    Id = 11,
                    Name = "Floor_fly",
                    Level = 3,
                    EquipmentId = 2
                },
                new Exercise
                {
                    Id = 12,
                    Name = "Underhand_press",
                    Level = 2,
                    EquipmentId = 2
                },
                new Exercise
                {
                    Id = 13,
                    Name = "Decline_pushup",
                    Level = 1,
                    EquipmentId = 1
                },
                new Exercise
                {
                    Id = 14,
                    Name = "Ucv_raise",
                    Level = 3,
                    EquipmentId = 2
                },
                new Exercise
                {
                    Id = 15,
                    Name = "Overhead_press",
                    Level = 3,
                    EquipmentId = 2
                },
                new Exercise
                {
                    Id = 16,
                    Name = "Hip_hugger",
                    Level = 2,
                    EquipmentId = 2
                },
                new Exercise
                {
                    Id = 17,
                    Name = "Lateral_raise",
                    Level = 3,
                    EquipmentId = 2
                },
                new Exercise
                {
                    Id = 18,
                    Name = "Front_raise",
                    Level = 2,
                    EquipmentId = 2
                },
                new Exercise
                {
                    Id = 19,
                    Name = "Scoop_press",
                    Level = 4,
                    EquipmentId = 2
                },
                new Exercise
                {
                    Id = 20,
                    Name = "Reverse_fly",
                    Level = 3,
                    EquipmentId = 2
                },
                new Exercise
                {
                    Id = 21,
                    Name = "Figure_8",
                    Level = 2,
                    EquipmentId = 2
                },
                new Exercise
                {
                    Id = 22,
                    Name = "Press_out",
                    Level = 3,
                    EquipmentId = 2
                },
                new Exercise
                {
                    Id = 23,
                    Name = "Renegade_row",
                    Level = 3,
                    EquipmentId = 2
                },
                new Exercise
                {
                    Id = 24,
                    Name = "Tripod_row",
                    Level = 4,
                    EquipmentId = 2
                },
                new Exercise
                {
                    Id = 25,
                    Name = "High_pull",
                    Level = 3,
                    EquipmentId = 2
                },
                new Exercise
                {
                    Id = 26,
                    Name = "Man_maker",
                    Level = 4,
                    EquipmentId = 2
                },
                new Exercise
                {
                    Id = 27,
                    Name = "W_raise",
                    Level = 2,
                    EquipmentId = 2
                },
                new Exercise
                {
                    Id = 28,
                    Name = "Standing_row",
                    Level = 3,
                    EquipmentId = 2
                },
                new Exercise
                {
                    Id = 29,
                    Name = "Bulgarian_split_squat",
                    Level = 3,
                    EquipmentId = 1
                },
                new Exercise
                {
                    Id = 30,
                    Name = "Reverse_lunge",
                    Level = 3,
                    EquipmentId = 1
                },
                new Exercise
                {
                    Id = 31,
                    Name = "Squat",
                    Level = 4,
                    EquipmentId = 1
                },
                new Exercise
                {
                    Id = 32,
                    Name = "Calf_raise",
                    Level = 2,
                    EquipmentId = 1
                },
                new Exercise
                {
                    Id = 33,
                    Name = "Never_ending_squat",
                    Level = 5,
                    EquipmentId = 1
                },
                new Exercise
                {
                    Id = 34,
                    Name = "Reverse_creeping_lunge",
                    Level = 4,
                    EquipmentId = 1
                },
                new Exercise
                {
                    Id = 35,
                    Name = "Deadlift",
                    Level = 5,
                    EquipmentId = 3
                },
                new Exercise
                {
                    Id = 36,
                    Name = "Lunge",
                    Level = 3,
                    EquipmentId = 1
                },
                new Exercise
                {
                    Id = 37,
                    Name = "Reverse_crunch",
                    Level = 2,
                    EquipmentId = 1
                },
                new Exercise
                {
                    Id = 38,
                    Name = "Mountain_climber",
                    Level = 3,
                    EquipmentId = 1
                },
                new Exercise
                {
                    Id = 39,
                    Name = "Halos",
                    Level = 2,
                    EquipmentId = 1
                },
                new Exercise
                {
                    Id = 40,
                    Name = "Crunch",
                    Level = 1,
                    EquipmentId = 1
                }
            );
            modelBuilder.Entity<MuscleExercise>().HasData(
                new MuscleExercise { MuscleId = 1, ExerciseId = 1 }, // Bicep <-> Xbody_hammer_curl
                new MuscleExercise { MuscleId = 1, ExerciseId = 2 }, // Bicep <-> Db_trifecta
                new MuscleExercise { MuscleId = 1, ExerciseId = 3 }, // Bicep <-> Standing_curl
                new MuscleExercise { MuscleId = 1, ExerciseId = 4 }, // Bicep <-> Alternate_st_curl

                new MuscleExercise { MuscleId = 2, ExerciseId = 5 }, // Tricep <-> Lying_tricep_ext
                new MuscleExercise { MuscleId = 2, ExerciseId = 6 }, // Tricep <-> Tricep_kickback
                new MuscleExercise { MuscleId = 2, ExerciseId = 7 }, // Tricep <-> Bench_dip
                new MuscleExercise { MuscleId = 2, ExerciseId = 8 }, // Tricep <-> Close_grip_db_pushup
                new MuscleExercise { MuscleId = 2, ExerciseId = 9 }, // Tricep <-> Cobra_pushup
                new MuscleExercise { MuscleId = 2, ExerciseId = 10 }, // Tricep <-> Pushup

                new MuscleExercise { MuscleId = 3, ExerciseId = 8 }, // Chest <-> Close_grip_db_pushup
                new MuscleExercise { MuscleId = 3, ExerciseId = 10 }, // Chest <-> Pushup
                new MuscleExercise { MuscleId = 3, ExerciseId = 11 }, // Chest <-> Floor_fly
                new MuscleExercise { MuscleId = 3, ExerciseId = 12 }, // Chest <-> Underhand_press
                new MuscleExercise { MuscleId = 3, ExerciseId = 13 }, // Chest <-> Decline_pushup
                new MuscleExercise { MuscleId = 3, ExerciseId = 14 }, // Chest <-> Ucv_raise

                new MuscleExercise { MuscleId = 4, ExerciseId = 10 }, // Shoulders <-> Pushup
                new MuscleExercise { MuscleId = 4, ExerciseId = 11 }, // Shoulders <-> Floor_fly
                new MuscleExercise { MuscleId = 4, ExerciseId = 13 }, // Shoulders <-> Decline_pushup
                new MuscleExercise { MuscleId = 4, ExerciseId = 14 }, // Shoulders <-> Ucv_raise
                new MuscleExercise { MuscleId = 4, ExerciseId = 15 }, // Shoulders <-> Overhead_press
                new MuscleExercise { MuscleId = 4, ExerciseId = 16 }, // Shoulders <-> Hip_hugger
                new MuscleExercise { MuscleId = 4, ExerciseId = 17 }, // Shoulders <-> Lateral_raise
                new MuscleExercise { MuscleId = 4, ExerciseId = 18 }, // Shoulders <-> Front_raise
                new MuscleExercise { MuscleId = 4, ExerciseId = 19 }, // Shoulders <-> Scoop_press
                new MuscleExercise { MuscleId = 4, ExerciseId = 20 }, // Shoulders <-> Reverse_fly
                new MuscleExercise { MuscleId = 4, ExerciseId = 21 }, // Shoulders <-> Figure_8
                new MuscleExercise { MuscleId = 4, ExerciseId = 22 }, // Shoulders <-> Press_out
                new MuscleExercise { MuscleId = 4, ExerciseId = 23 }, // Shoulders <-> Renegade_row
                new MuscleExercise { MuscleId = 4, ExerciseId = 25 }, // Shoulders <-> High_pull
                new MuscleExercise { MuscleId = 4, ExerciseId = 26 }, // Shoulders <-> Man_maker
                new MuscleExercise { MuscleId = 4, ExerciseId = 27 }, // Shoulders <-> W_raise
                new MuscleExercise { MuscleId = 4, ExerciseId = 28 }, // Shoulders <-> Standing_row

                new MuscleExercise { MuscleId = 5, ExerciseId = 14 }, // Traps <-> Ucv_raise
                new MuscleExercise { MuscleId = 5, ExerciseId = 17 }, // Traps <-> Lateral_raise

                new MuscleExercise { MuscleId = 6, ExerciseId = 14 }, // Back <-> Ucv_raise
                new MuscleExercise { MuscleId = 6, ExerciseId = 23 }, // Back <-> Renegade_row
                new MuscleExercise { MuscleId = 6, ExerciseId = 24 }, // Back <-> Tripod_row
                new MuscleExercise { MuscleId = 6, ExerciseId = 25 }, // Back <-> High_pull
                new MuscleExercise { MuscleId = 6, ExerciseId = 26 }, // Back <-> Man_maker
                new MuscleExercise { MuscleId = 6, ExerciseId = 28 }, // Back <-> Standing_row

                new MuscleExercise { MuscleId = 7, ExerciseId = 29 }, // Quadriceps <-> Bulgarian_split_squat
                new MuscleExercise { MuscleId = 7, ExerciseId = 30 }, // Quadriceps <-> Reverse_lunge
                new MuscleExercise { MuscleId = 7, ExerciseId = 31 }, // Quadriceps <-> Squat
                new MuscleExercise { MuscleId = 7, ExerciseId = 33 }, // Quadriceps <-> NeverEndingSquat

                new MuscleExercise { MuscleId = 8, ExerciseId = 31 }, // Glutes <-> Squat

                new MuscleExercise { MuscleId = 9, ExerciseId = 32 }, // Hamstrings <-> Calf_raise
                new MuscleExercise { MuscleId = 9, ExerciseId = 34 }, // Hamstrings <-> Reverse_creeping_lunge
                new MuscleExercise { MuscleId = 9, ExerciseId = 35 }, // Hamstrings <-> Deadlift

                new MuscleExercise { MuscleId = 10, ExerciseId = 32 }, // Calves <-> Calf_raise

                new MuscleExercise { MuscleId = 11, ExerciseId = 36 }, // Abs <-> Lunge
                new MuscleExercise { MuscleId = 11, ExerciseId = 37 }, // Abs <-> Reverse_crunch
                new MuscleExercise { MuscleId = 11, ExerciseId = 38 }, // Abs <-> Mountain_climber
                new MuscleExercise { MuscleId = 11, ExerciseId = 39 }, // Abs <-> Halo
                new MuscleExercise { MuscleId = 11, ExerciseId = 40 } // Abs <-> Crunch
            );
        }

    }
}

