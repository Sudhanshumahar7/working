# ⚙ Workflow Engine API (Infonetica Assignment)

This project presents a minimal backend system designed by *Sudhanshu* using C# and .NET 8. It serves as a dynamic, in-memory workflow engine with RESTful APIs to create, manage, and operate workflow definitions and their lifecycle states.

---

## 🔧 Highlights
This system allows you to:
- Register custom workflows with defined states and transitions
- Instantiate workflows dynamically
- Apply actions to advance states
- Retrieve execution history and status

Frameworks and tools:
- 🧠 .NET 8 Minimal API
- 📄 Swagger / OpenAPI for testing
- 💻 C# 12

---

## 🗂 Folder Layout

api/
├── Models/                 # Domain models
│   ├── State.cs
│   ├── ActionTransition.cs
│   ├── WorkflowDefinition.cs
│   ├── WorkflowInstance.cs
├── Services/              # Core logic engine
│   └── WorkflowService.cs
├── Program.cs             # Entrypoint and API endpoints


---

## ⚙ Setup Instructions

### 📦 Requirements
- [.NET SDK 8.0+](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- A code editor (like VS Code) or CLI

### ▶ Running the Engine
bash
git clone <your-repo-url>
cd api
dotnet restore
dotnet watch run

Access Swagger UI at: http://localhost:5014/swagger

---

## 📌 Sample Input

### Create a Workflow
json
{
  "id": "LeaveRequest",
  "states": [
    { "id": "Draft", "isInitial": true, "isFinal": false, "enabled": true },
    { "id": "ManagerReview", "isInitial": false, "isFinal": false, "enabled": true },
    { "id": "Approved", "isInitial": false, "isFinal": true, "enabled": true },
    { "id": "Rejected", "isInitial": false, "isFinal": true, "enabled": true }
  ],
  "actions": [
    { "id": "Submit", "enabled": true, "fromStates": ["Draft"], "toState": "ManagerReview" },
    { "id": "Approve", "enabled": true, "fromStates": ["ManagerReview"], "toState": "Approved" },
    { "id": "Reject", "enabled": true, "fromStates": ["ManagerReview"], "toState": "Rejected" }
  ]
}


---

## 🧪 Testing Instructions
- Access Swagger at http://localhost:5014/swagger
- Use the UI to send requests:
  - Create a workflow
  - Launch an instance
  - Transition through states
- Advanced: Use Postman or curl for API scripting

---

## 📌 Next Steps
- Add persistent storage (file or database)
- Role-based user control
- Add middleware for logging/validation
- Optional: Frontend UI for business users

---

## 👤 Developer
*Sudhanshu*  
GitHub: [https://github.com/sudhanshu-dev](https://github.com/sudhanshumahar7)

---

## 📄 License
MIT (or as specified in the assignment)
