# Gym & Studio OS

**Gym & Studio OS** is a multi-tenant SaaS application built with **ASP.NET Core MVC**.  
It helps gyms, boxing studios, and fitness centers manage **memberships, class schedules, bookings, payments, and attendance**.  

The project is designed as a **portfolio-ready, backend-focused application** with clean architecture, complex business logic, and practical real-world use cases.

---

## 📌 Features

- **Multi-Tenant & Roles**
  - Each gym/studio has its own environment.
  - Roles: Owner, Manager, Coach, Receptionist, Member.

- **Memberships & Plans**
  - Time-based, class-pack, and drop-in memberships.
  - Expiry dates, visit counters, freezing and extensions.

- **Class Scheduling & Booking**
  - Recurring classes with capacity limits.
  - Waitlists and automatic promotion.

- **Payments & Invoices**
  - Cash and online payments (provider-agnostic).
  - Invoice generation (PDF).

- **Notifications**
  - Email/SMS reminders for bookings, expiring memberships, invoices.
  - Background job scheduling (Hangfire).

- **Analytics & Reports**
  - KPIs: Active members, attendance rates, churn.
  - CSV exports.

- **Audit & Security**
  - Audit logs, concurrency handling, and data protection.

---

## 🛠 Tech Stack

- **Backend:** ASP.NET Core MVC (Clean Architecture)
- **Database:** PostgreSQL / SQL Server with EF Core
- **Architecture:** CQRS + MediatR
- **Validation:** FluentValidation
- **Mapping:** AutoMapper / Mapster
- **Authentication:** Identity (policy-based auth)
- **Background Jobs:** Hangfire
- **Caching:** Redis
- **Real-Time:** SignalR
- **Logging:** Serilog
- **Testing:** xUnit (unit & integration)

---

## 🗄️ Data Model (Essential Entities)

- Tenant, User, Member, Plan, Membership  
- Coach, ClassTemplate, ClassOccurrence, Booking  
- Payment, Invoice, Attendance  
- Notification, AuditLog  

---

## 🎯 MVP Scope (2–3 Weeks)

1. Authentication, tenant onboarding, role-based access.  
2. Plans & memberships (purchase, expiry, visits).  
3. Class templates, occurrences, bookings, and attendance.  
4. Payments (manual/cash), invoice PDFs.  
5. Email reminders for upcoming classes.  
6. Dashboard with KPIs and expiring memberships.  

---

## 📅 Milestones

- **Week 1:** Auth, tenants, plans, memberships (+ unit tests).  
- **Week 2:** Scheduling, bookings, attendance, dashboard.  
- **Week 3:** Invoices, payments, reminders, reports, documentation.  

---

## 🚀 Portfolio Highlights

- Multi-tenant SaaS with clean architecture & DDD principles.  
- Complex domain logic: memberships, bookings, payments.  
- Real-time updates with **SignalR** and background jobs with **Hangfire**.  
- Payment integrations with **webhooks & idempotency handling**.  
- Security: role-based authorization, auditing, structured logging.  
- CI/CD ready (tests + migrations).  

---

## 📖 Documentation

A detailed project documentation PDF is included:  
➡️ [gym_os_project_documentation.pdf](./gym_os_project_documentation.pdf)

---

## ⚡ Getting Started

1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/GymOs.git
   cd GymOs
