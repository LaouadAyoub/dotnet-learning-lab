# .NET Learning Lab

This repository documents my hands-on process for consolidating C# and .NET fundamentals. I use small experiments to investigate how the language and runtime behave, explain the results, and apply the lessons to backend development.

I am Ayoub Laouad, a .NET software engineer focused on backend systems. This lab gives me a place to revisit concepts from professional work, isolate questions, and check my understanding in executable code.

## The Spiral method

Each spiral revisits foundations with greater depth: ask a question, predict the result, run a small example, explain what happened, then use the lesson in an application. Progress means being able to explain and recreate an example independently, not simply reaching the end of a topic list.

| Spiral | Focus | Status / completion target |
| --- | --- | --- |
| 1 — Language core clarity | Value/reference types, mutation, strings, collections, LINQ, exceptions, generics, delegates | Examples present; continue revisiting until I can explain and recreate them independently |
| 2 — Execution and async | Tasks, await, blocking, concurrency, exceptions; then threads, WhenAny, races, thread pool, I/O vs CPU work | **Current focus.** Build a small asynchronous console processor confidently |
| 3 — Application fundamentals | DI, interfaces/abstract classes, middleware, configuration, logging, validation, lifetimes, unit tests | Planned |
| 4 — Runtime and performance | Stack/heap, GC, boxing, disposal, value/reference costs, StringBuilder, profiling | Planned; basic StringBuilder examples already appear in Spiral 1 |
| 5 — Rebuild with mastery | Apply clearer immutability, async, DI, error handling and testing decisions to AyShort | Planned |

## Repository map

```text
DotNetLearningLab.sln
01-language-core/
  Mutability/                 Value copies and shared references
  ValueAndReferenceTypes/     String equality; mutation/reassignment sketches
  LanguageCore/               Collections, LINQ, exceptions, generics, delegates
02-async-execution/
  AsyncExecution/             Task status, exceptions and overlapping operations
03-application-fundamentals/  Roadmap notes
04-runtime-performance/      Roadmap notes
integration-experiments/
  OrderImportPipeline/        Parse, deduplicate, filter, project and process
  RequestFilteringPipeline/   Compose validation rules and processing actions
  RequestValidationDraft/     Preserved unfinished exercise
```

## Experiments to start with

- **Deferred execution:** predicate output shows when `Where` runs, what repeated enumeration does, and how `ToList` changes reuse. These are in-memory examples, with implications for repeated backend data access.
- **Async execution:** simulated calls compare sequential awaits, overlapping tasks and `Task.WhenAll`. Separate examples show blocking with `.Result`, task faults and catching failures through `await`.
- **Integration pipelines:** small console exercises combine parsing, collections, duplicate detection, LINQ, `Func` and `Action`. They do not call external APIs or databases.
- **Generics and constraints:** examples contrast `object` with typed methods and show the compiler guarantees provided by interface and `new()` constraints. Intentionally invalid versions remain commented out.

See the [language notes](01-language-core/README.md), [async notes](02-async-execution/README.md) and [pipeline notes](integration-experiments/README.md).

## Visual notes

I also keep visual notes to revisit the concepts alongside the code.
[Browse the nine-image gallery](docs/visual-notes/README.md) for delegates,
generics, exceptions, async execution and the Spiral learning method.

<a href="docs/visual-notes/README.md"><img src="docs/visual-notes/delegates.png" alt="Visual notes on delegates, Func, Action and lambdas" width="260"></a>
<a href="docs/visual-notes/README.md"><img src="docs/visual-notes/task-async-await.png" alt="Visual notes on Task, async and await" width="260"></a>

## Run locally

Install the .NET 8 SDK, or a newer SDK with .NET 8 targeting support and the .NET 8 runtime. All seven projects target `net8.0`; there are no third-party package dependencies or required services.

From the repository root:

```sh
dotnet restore DotNetLearningLab.sln
dotnet build DotNetLearningLab.sln -c Release
dotnet run --project 01-language-core/LanguageCore
dotnet run --project 01-language-core/Mutability
dotnet run --project 01-language-core/ValueAndReferenceTypes
dotnet run --project integration-experiments/OrderImportPipeline
dotnet run --project integration-experiments/RequestFilteringPipeline
dotnet run --project integration-experiments/RequestValidationDraft
```

The validation draft currently creates sample data and produces no output. In `ValueAndReferenceTypes`, string equality runs by default; the commented alternatives are retained for hands-on editing.

Choose an async experiment:

```sh
dotnet run --project 02-async-execution/AsyncExecution -- status
dotnet run --project 02-async-execution/AsyncExecution -- exceptions
dotnet run --project 02-async-execution/AsyncExecution -- sequential
dotnet run --project 02-async-execution/AsyncExecution -- concurrent
dotnet run --project 02-async-execution/AsyncExecution -- when-all
dotnet run --project 02-async-execution/AsyncExecution -- await
dotnet run --project 02-async-execution/AsyncExecution -- result
```

Omitting the argument runs `status`. Delays simulate asynchronous I/O; timing and thread IDs are observations, not guarantees. The status experiment intentionally leaves a failed task unawaited to inspect its state; use `exceptions` to compare catching the failure with `await`.

Open `DotNetLearningLab.sln` in Visual Studio to explore and change individual examples. Formatting can be checked with:

```sh
dotnet format whitespace DotNetLearningLab.sln --verify-no-changes
```

There is no automated test suite yet. Validation currently consists of building every project and running the console examples.

## Principles and current limits

Small focused experiments; understanding before abstraction; explainable code; revisiting concepts; integrating lessons into real applications.

This is a work in progress, actively exploring Spiral 2. `TaskAndWaiting.cs` is still an empty placeholder and request validation is an unfinished draft. Later spirals are a roadmap, not completed work. Some intentionally failing examples require uncommenting code and may stop execution.

[AyShort](https://github.com/LaouadAyoub/AyShort) is the companion application project: a complete .NET backend where these lessons can be applied and evaluated together.
