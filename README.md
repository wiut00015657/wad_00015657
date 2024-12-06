# **Issue Tracker Web Application**

## **Introduction**
This application was developed for the Web Application Development module, as coursework portfolio project @ WIUT by student ID: **00015657**.

The purpose of this project is to create a web-based **Issue Tracker** application using **ASP.Net Core** and **Angular**, featuring an API with CRUD operations and a Single Page Application (SPA). It demonstrates the use of software design patterns, Entity Framework, and modern web development practices.

---

## **Student ID Calculation**
- **Student ID**: **00015657**
- **Calculation**: `00015657 % 20 = 17`
- **Matched Topic**: **Issue Tracker**

---

## **Prerequisites**
Before running the application, ensure the following are installed:
1. **.NET SDK (version 6 or higher)**
2. **Node.js (version 16 or higher)**
3. **Angular CLI**
4. **SQL Server**

---

## **Features**
- **Employee Management**: Add, view, update, and delete employees.
- **Issue Management**: Track issues with details like description, priority, and status.
- **Database Relationships**: Issues are assigned to employees using a foreign key relationship.
- **Responsive Design**: SPA with a clean and user-friendly interface.
- **RESTful API**: Backend supports full CRUD operations with Swagger documentation.

---

## **How to Run**

### **Backend (API)**
1. Clone the repository:  
   ```bash
   git clone https://github.com/00015657/wad_00015657.git
   cd wad_00015657/IssueTrackerAPI
   ```
2. Install dependencies and restore packages:
```bash
dotnet restore
```
3. Apply migrations and create the database:
```bash
dotnet ef database update
```
4. Run the application:
```bash
dotnet run
```
Frontend (Angular SPA)
1. Navigate to the Frontend directory:
```bash
cd IssueTrackerSPA
```
2. Install dependencies:
```bash
npm install
```
3. Start the application:
```bash
ng serve
```



