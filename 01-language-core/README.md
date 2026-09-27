# Language core observations

**Question:** Does assigning a variable copy a value or share an object?
The mutability example predicts and prints `5` after changing the copied integer,
and `Changed` after modifying the person through a second reference. The string
equality example prints `True`, `False`, `True`: equal content does not require
object identity. This distinction matters when passing mutable data between services.

**Question:** When does a LINQ predicate execute?
The expected behavior is no predicate output at query construction, repeated
predicate output on each enumeration, and one evaluation when materializing with
`ToList`. Run `LanguageCore` and follow the `Checking` and `Filtering` lines.
A list also implements `IEnumerable<T>`: the interface alone does not imply laziness.
These examples use memory, not a database; database behavior depends on the provider.

**Question:** What happens when `First` has no match?
The example now calls `First` with an absent name and catches
`InvalidOperationException`. `FirstOrDefault` returns the type's default instead.
Choosing between them expresses whether absence violates an expectation.

StringBuilder examples illustrate mutation and equivalent output; they are not
benchmarks. Their diagnostic `ToString` calls create strings during the loop.

Local execution matched the value/reference and deferred-enumeration observations
above. The missing-user First example printed the caught no-match exception and
the remaining language experiments continued successfully.
