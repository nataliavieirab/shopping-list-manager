# Shopping List

Maria does the family's grocery shopping every week, but she often forgets items or buys things she already has at home.  
To solve this problem, the **Shopping List** system was created, a simple application to register products, organize lists, and track purchases.

---

## 1. Categories Module

### Functional Requirements
- The system must allow registering new categories
- The system must allow editing existing categories
- The system must allow deleting categories
- The system must allow viewing all categories

### Business Rules
- Required fields:
  - Name (unique text, maximum 50 characters)
  - Color (palette selection or hexadecimal value)
- Categories cannot have duplicate names
- A category cannot be deleted if it has linked products

---

## 2. Products Module

### Functional Requirements
- The system must allow registering new products
- The system must allow editing existing products
- The system must allow deleting products
- The system must allow viewing all registered products

### Business Rules
- Required fields:
  - Name (2 to 100 characters)

---

## 3. Shopping Lists Module

### Functional Requirements
- The system must allow creating new shopping lists
- The system must allow editing existing lists
- The system must allow deleting lists
- The system must allow viewing all lists

### Business Rules
- Required fields:
  - List name (minimum 3 characters, maximum 100)
  - Creation date (automatic)
- Possible statuses: Open / Completed
- A list cannot be deleted if it has linked items
- The system must display the total number of items and the estimated total cost of each list

---

## 4. List Items Module

### Functional Requirements
- The system must allow adding items to a shopping list
- The system must allow removing items from a list
- The system must allow viewing all items in a list
- The system must display the product category when selecting an item for the list

### Business Rules
- Required fields:
  - Product (mandatory selection)
  - Quantity (positive number)
- The same product cannot be added twice in the same list
- The total value of the list must be automatically calculated (sum of estimated price × quantity)

---

## How to Use

1. Clone the repository or download the source code.
2. Open the terminal or command prompt and navigate to the root folder.
3. Run the command below to restore dependencies:

```bash
dotnet restore
```

4. Run the project with live compilation:

```bash
dotnet run --project ShoppingListManager.ConsoleApp
```

## Requirements

- .NET 10.0 SDK
