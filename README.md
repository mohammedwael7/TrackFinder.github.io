# 🎓 Track Finder

[![Live Demo](https://img.shields.io/badge/Live%20Demo-trackfinder.runasp.net-success?style=for-the-badge&logo=googlechrome&logoColor=white)](https://trackfinder.runasp.net/)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![Entity Framework Core](https://img.shields.io/badge/Entity%20Framework-3dbfb8?style=for-the-badge&logo=dotnet&logoColor=white)
![Bootstrap](https://img.shields.io/badge/Bootstrap-563D7C?style=for-the-badge&logo=bootstrap&logoColor=white)
![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg?style=for-the-badge)

## 📖 Professional Introduction

**Track Finder** is an intelligent, full-stack learning platform designed to help students discover and navigate their ideal career paths. 

- **The Problem:** Many learners struggle to identify learning tracks that align with their skills, wasting time and resources on disconnected materials. 
- **The Solution:** Track Finder provides assessment-based recommendations, structured learning roadmaps, curated courses from trusted instructors, and a supportive community forum. 

---

## 📑 Table of Contents

- [Live Deployment](#-live-deployment)
- [Project Overview](#-project-overview)
- [Features](#-features)
- [Tech Stack](#-tech-stack)
- [System Architecture](#-system-architecture)
- [Database Design](#-database-design)
- [Project Structure](#-project-structure)
- [UI Screenshots & Interfaces](#-ui-screenshots--interfaces)
- [Installation](#-installation)
- [Configuration](#-configuration)
- [Usage](#-usage)
- [API Endpoints](#-api-endpoints)
- [Authentication & Security](#-authentication--security)
- [Future Improvements](#-future-improvements)
- [Contributing](#-contributing)
- [License](#-license)
- [Contributors & Acknowledgments](#-contributors--acknowledgments)

---

## 🌐 Live Deployment

- **Experience the platform live:** [https://trackfinder.runasp.net/](https://trackfinder.runasp.net/)

---

## 🎯 Project Overview

Track Finder is a standalone, web-based e-learning system with three tightly integrated experiences for **Students**, **Instructors**, and **Administrators**. 

- **Active Advising:** Turns a passive course catalog into an active advisor by assessing a learner's baseline.
- **Intelligent Recommendations:** Suggests specific career tracks (e.g., AI, Web Development, Cybersecurity).
- **Centralized Ecosystem:** Combines course consumption, progress tracking, community engagement, and platform management.

---

## ✨ Features

<details>
<summary><strong>🎓 Student Experience</strong></summary>

- **Skill Assessments:** Adaptive questions that measure a learner's baseline to recommend suitable career tracks and technology stacks.
- **Structured Learning:** Browse and enroll in curated paths containing videos, documents, and downloadable offline resources.
- **Achievements & Recognition:** Earn digital mastery badges and verified certificates upon successful course completion.
- **Community Engagement:** Publish posts, ask questions, share code snippets, and follow specific tech communities.
</details>

<details>
<summary><strong>👨‍🏫 Instructor Workspace</strong></summary>

- **Course Management:** Create, edit, and organize courses, lessons, and assignments.
- **Student Monitoring:** Track student engagement, progress, and performance in real-time.
- **Content Delivery:** Upload instructional videos, PDF materials, and quizzes/exams.
</details>

<details>
<summary><strong>🛡️ Admin Control Panel</strong></summary>

- **Centralized Dashboard:** Monitor live statistics, track enrollments, and view system metrics.
- **Content Taxonomy:** Manage courses, structured tracks, and individual technology stacks.
- **User & Role Management:** Ban/unban users, approve instructor accounts, and manage cross-role permissions.
- **Moderation:** Review reported content and ensure community safety.
</details>

---

## 💻 Tech Stack

- **Backend:** C# / ASP.NET Core MVC, Entity Framework Core (ORM)
- **Frontend:** HTML5, CSS3, JavaScript, Razor Pages, Bootstrap CSS
- **Database & Hosting:** Microsoft SQL Server, IIS Hosting Environment
- **Tools:** Git & GitHub, draw.io (System Modeling)

---

## 🏗️ System Architecture

- **Pattern:** Strict layered architecture based on the **Model-View-Controller (MVC)** design pattern. 
- **Separation of Concerns:** Separates the presentation layer (Views/Razor), business logic (Controllers), and data access layers (Models/ViewModels).
- **Goal:** Ensures high maintainability and system scalability.

---

## 🗄️ Database Design

The system utilizes a robust relational database managed via Entity Framework Core. 

| Entity | Description | Relationships |
| :--- | :--- | :--- |
| **User (Abstract)** | Core identity storing credentials, role, and contact info. | Parent to Student/Instructor. |
| **Student** | Tracks GPA, level, and enrollment dates. | 1:N with Grades, Payments, Certificates. |
| **Instructor** | Tracks specialization, salary, and hire date. | 1:N with Courses, Assignments. |
| **Course** | Core educational unit containing title, duration, and credits. | 1:N with Exams, Content, Schedules. |
| **Enrollment** | Join table linking Students and Courses with progress tracking. | N:M link between Student/Course. |
| **Question & Grade** | Stores assessment questions, correct answers, and achieved student scores. | Owned by Exam / Student. |
| **Content** | Video, document, or resource URLs with sequential ordering. | Owned by Course. |

---

## 📁 Project Structure

*Note: Follows standard ASP.NET Core MVC scaffolding conventions.*


TrackFinder/
├── Controllers/       # Handles incoming HTTP requests and business logic routing
├── Models/            # Database entities (User, Course, Enrollment, etc.)
├── Views/             # Razor pages and frontend UI components
├── ViewModels/        # DTOs separating database entities from the presentation layer
├── wwwroot/           # Static assets (CSS, JS, Images, Bootstrap)
├── Data/              # Entity Framework DbContext and Migrations
└── Program.cs         # Application entry point and service registration


---

## 🖥️ UI Screenshots & Interfaces

### Landing Page
<div align="center">
  <img src="https://via.placeholder.com/800x400?text=Landing+Page+Screenshot" alt="Landing Page">
</div>

- **Purpose:** The main entry point to the Track Finder platform.
- **Functionality:** Introduces the platform's value proposition and highlights key services, guiding visitors seamlessly toward registration.

---

### Secure Sign In & Registration
<div align="center">
  <img src="https://via.placeholder.com/800x400?text=Sign+In+Screenshot" alt="Secure Sign In">
</div>

- **Purpose:** Secure, role-based authentication gateway.
- **Functionality:** Dynamically directs users (Students, Instructors, Admins) to personalized dashboards based on their verified credentials.

---

### Student Dashboard
<div align="center">
  <img src="https://via.placeholder.com/800x400?text=Student+Dashboard+Screenshot" alt="Student Dashboard">
</div>

- **Purpose:** The personalized home for learners.
- **Functionality:** Surfaces active courses, roadmap progress, recent achievements, and quick actions in a single, scannable glance.

---

### Instructor Dashboard
<div align="center">
  <img src="https://via.placeholder.com/800x400?text=Instructor+Dashboard+Screenshot" alt="Instructor Dashboard">
</div>

- **Purpose:** A centralized command center for educators.
- **Functionality:** Provides low-friction access to course publishing, real-time student monitoring, and crucial platform revenue/engagement metrics.

---

### Assessment & Track Recommendations
<div align="center">
  <img src="https://via.placeholder.com/800x400?text=Assessment+and+Tracks+Screenshot" alt="Assessment Overview">
</div>

- **Purpose:** The core intelligent advising engine.
- **Functionality:** Offers a distraction-free environment to measure technical baseline skills, outputting personalized technology track and stack suggestions.

---

### Course Materials
<div align="center">
  <img src="https://via.placeholder.com/800x400?text=Course+Materials+Screenshot" alt="Course Materials">
</div>

- **Purpose:** The primary learning interface.
- **Functionality:** Implements a predictable reading path (details ➔ materials ➔ download) for sequential video, document, and resource consumption.

---

### Community Hub
<div align="center">
  <img src="https://via.placeholder.com/800x400?text=Community+Screenshot" alt="Community Hub">
</div>

- **Purpose:** A collaborative ecosystem for shared growth.
- **Functionality:** Enables code snippet sharing, Q&A threads, and topic following to reduce the isolation of self-paced study.

---

### Achievements & Badges
<div align="center">
  <img src="https://via.placeholder.com/800x400?text=Achievements+Screenshot" alt="Achievements">
</div>

- **Purpose:** Motivation and verifiable progression.
- **Functionality:** Acts as a digital portfolio that prominently displays mastery badges (e.g., "Problem Solver") and verified course certificates.

---

### Admin Dashboard
<div align="center">
  <img src="https://via.placeholder.com/800x400?text=Admin+Dashboard+Screenshot" alt="Admin Dashboard">
</div>

- **Purpose:** The administrator's mission control.
- **Functionality:** Centralizes live statistics, user distribution analytics, content moderation, and overarching taxonomy management.

---

## ⚙️ Installation

Follow these steps to get a local development environment up and running:

1. **Clone the repository:**
   ```bash
   git clone https://github.com/yourusername/track-finder.git
   cd track-finder
   ```

2. **Restore .NET dependencies:**
   ```bash
   dotnet restore
   ```

3. **Apply database migrations:**
   ```bash
   dotnet ef database update
   ```

4. **Run the application:**
   ```bash
   dotnet run
   ```

---

## 🛠️ Configuration

Configure your `appsettings.json` file to connect the application to your local SQL Server instance:


{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=TrackFinderDB;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}


---

## 🚀 Usage

- **For New Users:** 
  - Navigate to the [Live Deployment](https://trackfinder.runasp.net/) or your local host.
  - Click `Get Started` to register a new account.
- **For Students:** 
  - Take the *Career Assessment* immediately upon login.
  - Review your tailored technology track recommendations.
  - Browse the catalog, enroll in courses, and begin consuming content.
- **For Instructors:** 
  - Await account verification from an administrator.
  - Access the *Teacher Studio* to design new courses, upload video/PDF materials, and engage with students in the Community Hub.
- **For Administrators:** 
  - Log in with secure admin credentials.
  - Use the dashboard to monitor system health, verify instructor accounts, manage tracks/stacks, and moderate community posts.

---

## 📡 API Endpoints

- **Not Applicable:** The system is documented and built as a traditional **ASP.NET Core MVC** application utilizing Razor Pages, Server-Side Rendering (SSR), and Views, rather than a decoupled RESTful API architecture.

---

## 🔒 Authentication & Security

- **Role-Based Access Control (RBAC):** Strict segregation of privileges across `STUDENT`, `INSTRUCTOR`, and `ADMIN` roles.
- **Password Hashing:** All user credentials are encrypted via `bcrypt` prior to database storage.
- **Session Handling:** Utilizes JWT token issuance for secure, stateless user sessions.
- **Input Validation:** Multi-layered validation prevents common exploits, including SQL Injection and Cross-Site Scripting (XSS).

---

## 🔮 Future Improvements

- [ ] **Adaptive Assessments:** Dynamic questions that adjust in difficulty based on the learner's ongoing performance.
- [ ] **AI-Powered Recommendations:** Enhanced algorithms for smarter matching between students and specific courses/tracks.
- [ ] **Mobile Applications:** Dedicated native clients for iOS and Android devices.
- [ ] **Live Sessions:** Infrastructure for instructor-led live webinars and real-time classes.
- [ ] **Verified Credentials:** Blockchain-anchored digital certificates for unforgeable proof of completion.

---

## 🤝 Contributing

Contributions make the open-source community an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

1. **Fork the Project**
2. **Create your Feature Branch:**
   ```bash
   git checkout -b feature/AmazingFeature
   ```
3. **Commit your Changes:**
   ```bash
   git commit -m 'Add some AmazingFeature'
   ```
4. **Push to the Branch:**
   ```bash
   git push origin feature/AmazingFeature
   ```
5. **Open a Pull Request**

---

## 📜 License

- Distributed under the **MIT License**.
- See `LICENSE` for more information.

---

## 👥 Contributors & Acknowledgments

This platform was proudly developed as a Final Graduation Project for the **FULL-Stack.NET Developer Track** in association with **NEXT Career Development Academy** and **Rowad Masr Digital**. 

| Role | Contributor Name |
| :--- | :--- |
| **👨‍🏫 Instructor & Project Supervisor** | **Eng. Maged Samir** |
| 👑 **Team Leader** | **Mohamed Wael** |
| 💻 Full-Stack .NET Developer | Mohamed Khaled |
| 💻 Full-Stack .NET Developer | Ahmed Ehab |
| 💻 Full-Stack .NET Developer | Abdelrahman Mohamed |
| 💻 Full-Stack .NET Developer | Amr Khaled |
| 💻 Full-Stack .NET Developer | Shymaa Fekry |
| 💻 Full-Stack .NET Developer | Reham Ahmed |

<div align="center">
  <em>Thank you for exploring Track Finder! Questions, feedback, and discussions are always welcome.</em>
</div>
