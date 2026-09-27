# Execution and async observations

| Question / command | Expected behavior | Backend relevance |
| --- | --- | --- |
| `sequential` | The one-second and two-second delays add to roughly three seconds | Dependencies require ordering |
| `concurrent` / `when-all` | Starting both operations first overlaps the delays, roughly two seconds | Independent I/O can overlap; shared resources may still prohibit concurrency |
| `await` / `result` | Await can suspend the method; Result blocks the calling thread | Avoid blocking request-processing threads |
| `status` | The delayed exception faults the task while the caller continues | Starting a task is not handling its failure |
| `exceptions` | Await surfaces the exception inside the surrounding catch | Handle failures where the result is consumed |

The status sample checks after a two-second delay. This is an observation window,
not synchronization with task completion; scheduling may change what it sees.
It deliberately does not await the failed task. The exception sample does.
Task status and continuation thread IDs must not be treated as fixed sequences.
Concurrency here is overlapping simulated I/O, not proof of parallel CPU work.

`TaskAndWaiting.cs` remains an original, empty exercise placeholder. WhenAny,
race conditions, thread-pool behavior and an async console processor remain future work.

## Observed in local validation

With the .NET 8 runtime, sequential execution took 3024 ms, concurrent execution
2005 ms, and WhenAll 2014 ms. The status sample printed WaitingForActivation then
Faulted, and the awaited exception sample printed `Caught: User service failed`.
These are sample observations, not timing or scheduling assertions.
