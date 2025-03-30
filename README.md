# West City Bank - Console App

A basic interactive **banking application** built with [C#](https://learn.microsoft.com/en-us/dotnet/csharp/) and powered by the beautiful [Spectre.Console](https://spectreconsole.net/) UI library.

---

## Features

- Text-based UI with colors, panels, and menus
- Add, view, and manage customers
- Deposit & withdraw money
- View transaction history
- Built-in calendar with current date highlighted
- Load initial customers from `customers.json`

---

## Technologies

- **.NET 6+**
- **Spectre.Console** for rich UI
- Basic file handling with `System.Text.Json`
- Clean C# class-based structure using partial classes

---

## Purpose

This project is built as a **learning resource** for beginners in:

- Object-oriented programming with C#
- Class structure, methods, and encapsulation
- Console app building with modern UI
- Reading data from external files (JSON)
  This program especially designed for my students who learn C#

---

## Getting Started

1. Clone the repo

   ```bash
   git clone https://github.com/devrimsavas/BANKCONSOLE/tree/main/Bank1

   ```

2. Restore Packages
   ```bash
   dotnet restore
   ```
3. Run the app
   I recommend you open a seperate, clean terminal by using cmd command to see full functionality of this program


# PART 2 STUDENT TASKS 
This project can be **enhanced and extended** by students who want to practice their skills with:

-  C# classes & objects  
-  File operations (reading/writing JSON)  
-  Simple banking logic  
-  Real-world thinking in programming  

---

###  Tasks to Complete

1. ** Save New Customers to File**  
   Currently, the app only loads customers from `customers.json`.  
   ➤ Your task is to **write back to the file** whenever a new customer is added, so data is not lost between runs.

2. ** Save Transaction History to File**  
   Right now, deposits/withdrawals are only in memory.  
   ➤ Add functionality to **save transactions** to disk and load them on startup.

3. ** Send Money Between Customers**  
   Add a feature where one customer can send money to another using their **account number**.  
   - If both are in **the same bank**, it's free.  
   - If the receiver is with a **different bank**, deduct **1% fee** from the sender.  
   ➤ Tip: This requires finding customers by account ID, not just customer ID!

4. ** Use Account Numbers for Transfers**  
   When sending money, customers should **enter the account number** of the receiver — not the customer ID.  
   ➤ Add validations to make sure account numbers are typed correctly and actually exist.

---

###  Extra (Optional)

- Add login system (by ID or PIN)
- Export customer and transaction data to a `.csv`
- Add a menu option to view total bank funds 💰

---

 These tasks are great for improving your understanding of:
- Classes and encapsulation
- Data serialization
- Real-world logic & user flows
- Error handling and validation

Good luck, and happy hacking! 💻🎉




## Screenshots

![Bank Main ](/screenshots/bank1.png)
![Customer Detail ](/screenshots/bank2.png)
![All Customers ](/screenshots/bank3.png)
