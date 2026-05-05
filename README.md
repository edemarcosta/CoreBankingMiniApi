# 💳 Core Banking API (.NET)

![.NET](https://img.shields.io/badge/.NET-7%2F8-blue)
![Status](https://img.shields.io/badge/status-active-success)
![License](https://img.shields.io/badge/license-MIT-green)

A simple Core Banking API built with .NET and Entity Framework, simulating real-world financial operations such as account management, deposits, withdrawals, and transfers.

---

## 🚀 Features

- Customer registration
- Account creation
- Deposit and withdrawal operations
- Transfer between accounts
- Transaction history (audit trail)
- Business rules validation (balance control, account validation)

---

## 🛠 Technologies

- .NET 7 / 8
- Entity Framework Core
- SQLite
- REST API
- Swagger / Postman / Bruno

---

## 📦 Getting Started

### 1. Clone repository

git clone https://github.com/edemarcosta/core-banking-api-dotnet.git
cd core-banking-api-dotnet

---

### 2. Run database migrations

dotnet ef database update

---

### 3. Run the application

dotnet run

---

### 4. Access Swagger UI

https://localhost:7012/swagger

---

## 📌 API Endpoints

### 👤 Create Customer

POST /api/customers

{
  "fullName": "John Carter",
  "documentNumber": "12345678900",
  "email": "john@email.com"
}

---

### 🏦 Create Account

POST /api/accounts?customerId=1

---

### 💰 Deposit

POST /api/accounts/deposit?accountId=1&amount=1000

---

### 💸 Withdraw

POST /api/accounts/withdraw?accountId=1&amount=200

---

### 🔄 Transfer

POST /api/accounts/transfer

{
  "fromAccountId": 1,
  "toAccountId": 2,
  "amount": 250,
  "description": "Test transfer"
}

---

### 📊 Transaction History

GET /api/accounts/{accountId}/transactions

---

## 🧠 Business Rules Implemented

- Prevent negative balance
- Validate transfer amount
- Ensure accounts exist
- Prevent transfers to the same account
- Maintain transactional consistency (atomic operations)

---

## 🧱 Project Structure

CoreBankingMiniApi

- Controllers
- Models
- DTOs
- Data
- Program.cs
- appsettings.json

---

## 📈 Future Improvements

- JWT Authentication
- Logging (Serilog)
- Clean Architecture (Service Layer)
- Docker support
- Unit tests (xUnit)
- API versioning

---

## 👨‍💻 Author

Edemar Costa Oliveira

GitHub: https://github.com/edemarcosta  
LinkedIn: https://www.linkedin.com/in/edemar-costa-oliveira

---

## ⭐ Project Purpose

This project was created as part of a professional portfolio to demonstrate backend development skills focused on financial systems and business rules implementation.


