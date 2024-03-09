# Read API Problems

Building RESTful APIs often involves creating endpoints that allow clients to retrieve data based on various filtering
criteria. In many cases, this requires creating multiple endpoints to handle different filtering scenarios. For example,
imagine you have an API that allows clients to retrieve a list of objects based on their age, another API that allows
clients to retrieve a list of objects based on their height, and another API that allows clients to retrieve a list of
objects based on their name.

This approach can quickly become unwieldy as the number of filtering options increases. It also leads to a poor user
experience, as clients have to make multiple requests to retrieve the data they need. Moreover, it leads to a poor
maintainability as each endpoint needs to be tested and updated separately.

### Example endpoints

- https://domain.com/entity/get-by-id
- https://domain.com/entity/get-by-id-with-nested-entity
- https://domain.com/entity/get-by-id-and-by-custom-property
- https://domain.com/entity/get-by-id-and-by-custom-property-with-nested-entity
- https://domain.com/entity/get-by-id-and-by-custom-property-with-nested-entity-only-name
- https://domain.com/entity/get-all
- https://domain.com/entity/get-all-by-ids
- https://domain.com/entity/get-all-by-ids-with-nested-entity
- https://domain.com/entity/get-all-by-ids-and-by-custom-property
- https://domain.com/entity/get-all-by-ids-and-by-custom-property-with-nested-entity
- https://domain.com/entity/get-all-by-ids-and-by-custom-property-with-nested-entity-only-names
- etc

or

- https://domain.com/entity/get ?filter-id ?filter-custom-property ?include-nested-entity
- https://domain.com/entity/get-all ?filter-ids ?filter-custom-properties ?order-by-name
  ?include-nested-entity ?select-only-names ?take-10
- https://domain.com/entity2/get ?filter-id ?filter-custom-property ?include-nested-entity
- https://domain.com/entity2/get-all ?filter-ids ?filter-custom-properties ?order-by-name
  ?include-nested-entity ?select-only-names ?take-10
- https://domain.com/entity3/get ?filter-id ?filter-custom-property ?include-nested-entity
- https://domain.com/entity3/get-all ?filter-ids ?filter-custom-properties ?order-by-name
  ?include-nested-entity ?select-only-names ?take-10
- etc

# Solution: --- Transfer Responsibility To Consumer ---



## EntityFramework

### Model

- Entity Name
- Include
    - Nested Entities Names
    - As Join or As Split Queries
- Filter
    - Comparison cases (==, >, <, >=, <=, Contains)
    - Value or Values
- Order
    - Property Name or Property Names
    - Ascending or Descending
- Select
    - Property names
- Take
    - Count

### Endpoints

- https://domain.com/get
- https://domain.com/get-all

### Pipeline

1. Retrieve Model
2. Parse Model To EF Query
    1. Define Entity
    2. Build lambdas with Expression API
3. Query Database
4. Return Entity Model

## Dapper

Raw sql - that's all we need