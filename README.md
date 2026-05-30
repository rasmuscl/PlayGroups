# PlayGroups

Generates fair playgroup rotations for a fixed set of children across a list of dates, then assigns a host family to each group on each day.

The solver picks who plays together so that, over the season, every child plays with every other child roughly the same number of times — avoiding repeats on adjacent days and minimizing pairs that never meet.

## Projects

- `Src/PlayGroupSolver` — core library: domain model, branch-and-bound solver, ASCII/Excel formatters.
- `Src/PlayGroupSolverConsole` — console runner. Loads input, solves the plan, assigns hosts, writes output files.
- `Src/VisualPlan` — WinForms UI for viewing a generated plan.

## How it works

Two sequential branch-and-bound passes:

1. **Plan** — assign children to groups per day, minimizing a cost made of pair-frequency and same-week-repeat penalties.
2. **Hosting** — given the plan, pick a host child for each group on each day.

## Input

Configured via the static `InputData` class:

- `NumberOfBoys`, `NumberOfGirls`
- `GroupLayout` — list of groups, each a list of boy/girl slots.
- `Names` — boys first, then girls.
- `Dates` — one entry per play day (`dd-MM-yyyy`).

## Build & run

Open `Src/PlayGroups.sln` in Visual Studio and build. Run `PlayGroupSolverConsole` to produce the plan, or `VisualPlan` to view one.

## Output

Written to the console runner's working directory:

- `plan_solution.txt` — best plan found (raw indices).
- `plan_best_solution.txt` — formatted plan.
- `stats_solution.txt` — pair-frequency matrix.
- `final.txt` — plan with hosts assigned.
