# Integration experiments

These console pipelines combine language features without external services.

- **OrderImportPipeline:** asks how raw rows become a processable sequence. The
  expected result reports the malformed line, removes duplicate ORD-101, filters
  the zero-total order, and prints ORD-103 before ORD-101 (220 then 150). Parsing
  uses invariant culture so the input format is independent of machine locale.
  This is a small parsing exercise, not complete CSV or email validation.
- **RequestFilteringPipeline:** composes Func predicates, removes duplicate IDs
  with a HashSet, orders and projects results, then passes an Action to process
  them. The current fixture leaves only request 0; it contains no duplicate IDs,
  so deduplication exists in the code but is not demonstrated by that fixture.
- **RequestValidationDraft:** retains sample requests and a commented method
  signature. It builds but performs no validation and prints nothing. It is
  preserved as unfinished work, not represented as a completed pipeline.

Backend relevance: keeping parsing, selection and processing decisions explicit
helps explain a data flow before introducing application-level abstractions.

Local execution matched these expected outputs: only request 0 survived filtering;
order import reported INVALID-LINE and processed ORD-103 before ORD-101. The
validation draft exited successfully with no output, consistent with its unfinished state.
