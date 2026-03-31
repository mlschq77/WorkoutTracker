# Workout Tracker

## Relacje

- **1..N**: WorkoutPlan → WorkoutSessions
- **1..N**: WorkoutSession → SessionExercises
- **N..M**: WorkoutSessions ↔ Exercises (SessionExercise)

## Migracje

```
Add-Migration InitialSchema
Update-Database
```

## Schemat

- **Exercises** (ExerciseId PK, Name*, MuscleGroup*, Description)
- **WorkoutPlans** (WorkoutPlanId PK, Name*, Description)
- **WorkoutSessions** (WorkoutSessionId PK, Date*, Notes, WorkoutPlanId FK*)
- **SessionExercises** (SessionExerciseId PK, Sets*, Reps*, Weight, WorkoutSessionId FK*, ExerciseId FK*)

## Funkcjonalności

- Pełny CRUD: Ćwiczenia, Plany treningowe, Sesje treningowe
- Strona główna z listą sesji i filtrowaniem po planie
- Walidacja danych wejściowych

## Technologie

- ASP.NET Core MVC (.NET 8)
- Entity Framework Core 8
- SQL Server LocalDB
- Bootstrap 5
