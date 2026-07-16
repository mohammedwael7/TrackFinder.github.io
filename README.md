# 🎓 Track Finder

![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![Entity Framework Core](https://img.shields.io/badge/Entity%20Framework-3dbfb8?style=for-the-badge&logo=dotnet&logoColor=white)
![Bootstrap](https://img.shields.io/badge/Bootstrap-563D7C?style=for-the-badge&logo=bootstrap&logoColor=white)
![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg?style=for-the-badge)

## 📖 Professional Introduction

**Track Finder** is an intelligent, full-stack learning platform designed to help students discover and navigate their ideal career paths [1, 2]. Many learners struggle to identify learning tracks that align with their skills, wasting time and resources on disconnected materials [2]. Track Finder solves this by providing assessment-based recommendations, structured learning roadmaps, curated courses from trusted instructors, and a supportive community forum [3, 4]. 

---

## 📑 Table of Contents

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
- [Acknowledgments](#-acknowledgments)

---

## 🎯 Project Overview

Track Finder is a standalone, web-based e-learning system with three tightly integrated experiences for **Students**, **Instructors**, and **Administrators** [5, 6]. The platform turns a passive course catalog into an active advisor by assessing a learner's baseline and intelligently recommending a specific career track (such as AI, Web Development, or Cybersecurity) [7, 8]. It centralizes course consumption, progress tracking, community engagement, and platform management into a single cohesive ecosystem [9-11].

---

## ✨ Features

<details>
<summary><strong>🎓 Student Experience</strong></summary>

* **Skill Assessments:** Adaptive questions that measure a learner's baseline to recommend suitable career tracks and technology stacks [8, 9].
* **Structured Learning:** Browse and enroll in curated paths containing videos, documents, and downloadable offline resources [10].
* **Achievements & Recognition:** Earn digital mastery badges and verified certificates upon successful course completion [12-14].
* **Community Engagement:** Publish posts, ask questions, share code snippets, and follow specific tech communities [15].
</details>

<details>
<summary><strong>👨‍🏫 Instructor Workspace</strong></summary>

* **Course Management:** Create, edit, and organize courses, lessons, and assignments [5, 16].
* **Student Monitoring:** Track student engagement, progress, and performance in real-time [16, 17].
* **Content Delivery:** Upload instructional videos, PDF materials, and quizzes/exams [16, 18].
</details>

<details>
<summary><strong>🛡️ Admin Control Panel</strong></summary>

* **Centralized Dashboard:** Monitor live statistics, track enrollments, and view system metrics [11, 17].
* **Content Taxonomy:** Manage courses, structured tracks, and individual technology stacks [19, 20].
* **User & Role Management:** Ban/unban users, approve instructor accounts, and manage cross-role permissions [21-23].
* **Moderation:** Review reported content and ensure community safety [24].
</details>

---

## 💻 Tech Stack

**Backend**
* C# / ASP.NET Core MVC [6, 25]
* Entity Framework Core (ORM) [6, 26]

**Frontend**
* HTML5, CSS3, JavaScript [25]
* Razor Pages [25]
* Bootstrap CSS [25]

**Database & Hosting**
* Microsoft SQL Server [6, 26]
* IIS Hosting Environment [27]

**Tools**
* Git & GitHub [25]
* draw.io (System Modeling) [25]

---

## 🏗️ System Architecture

Track Finder follows a strict layered architecture based on the **Model-View-Controller (MVC)** design pattern [6, 25]. This separates the presentation layer (Views/Razor), business logic (Controllers), and data access layers (Models/ViewModels) to ensure high maintainability and scalability [6, 28]. 

---

## 🗄️ Database Design

The system utilizes a robust relational database managed via Entity Framework Core [26]. 

| Entity | Description | Relationships |
| :--- | :--- | :--- |
| **User (Abstract)** | Core identity storing credentials, role, and contact info [26]. | Parent to Student/Instructor [29]. |
| **Student** | Tracks GPA, level, and enrollment dates [30]. | 1:N with Grades, Payments, Certificates [29, 31]. |
| **Instructor** | Tracks specialization, salary, and hire date [30, 32]. | 1:N with Courses, Assignments [29, 31]. |
| **Course** | Core educational unit containing title, duration, and credits [32]. | 1:N with Exams, Content, Schedules [29, 31]. |
| **Enrollment** | Join table linking Students and Courses with progress tracking [33]. | N:M link between Student/Course [29]. |
| **Question & Grade** | Stores assessment questions, correct answers, and achieved student scores [33, 34]. | Owned by Exam / Student [29]. |
| **Content** | Video, document, or resource URLs with sequential ordering [34]. | Owned by Course [29]. |

---

## 📁 Project Structure

*Note: The exact directory tree is **Not specified** in the documentation. However, the system strictly follows standard ASP.NET Core MVC scaffolding conventions.*

```text
TrackFinder/
├── Controllers/       # Handles incoming HTTP requests and business logic routing
├── Models/            # Database entities (User, Course, Enrollment, etc.)
├── Views/             # Razor pages and frontend UI components
├── ViewModels/        # DTOs separating database entities from the presentation layer
├── wwwroot/           # Static assets (CSS, JS, Images, Bootstrap)
├── Data/              # Entity Framework DbContext and Migrations
└── Program.cs         # Application entry point and service registration
🖥️ UI Screenshots & Interfaces
The platform features over 30 fully designed, responsive interfaces
.
Landing Page: Highlights the platform's value proposition, tracks, and business value
.
Sign In & Registration: Role-separated pathways for Students and Instructors with secure validation
.
Student Dashboard: Surfaces active courses, roadmap progress, and quick actions
.
Instructor Dashboard: A command center for course publishing, revenue metrics, and student tracking
.
Assessment Questions: Distraction-free environment for skill measurement and track matching
.
Course Materials: A predictable, three-step reading path (details ➔ materials ➔ preview/download)
.
Community Hub: Dedicated spaces to share code snippets, ask questions, and follow trending #DotNet topics
.
User Profile & Achievements: Displays mastery badges (e.g., "Bug Hunter"), verified certificates, and public identity
.
Admin Dashboard: Centralized statistics, user distribution graphs, and quick management links
.
⚙️ Installation
Note: Specific environment variables or connection strings are Not specified in the documentation. Below are standard execution steps based on the provided tech stack.
Clone the repository:
Restore .NET dependencies:
Update the database (Entity Framework Core migrations):
Run the application:
🛠️ Configuration
Configure your appsettings.json file with your local SQL Server instance:
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=TrackFinderDB;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
🚀 Usage
New Users: Navigate to the Landing Page and click Get Started to register as either a Student or Instructor
.
Students: Begin by taking the Career Assessment to receive a tailored technology track recommendation, then browse and enroll in recommended courses
.
Instructors: Await admin verification. Once approved, use the Teacher Studio to create new courses, upload video/document materials, and monitor community discussions
.
Admins: Log in with administrative credentials to access the centralized dashboard to review reported content, issue badges, and manage technical tracks
.
📡 API Endpoints
Not specified. (The system is documented as a traditional ASP.NET Core MVC application utilizing Razor Pages and Views rather than a standalone RESTful API architecture
).
🔒 Authentication & Security
Role-Based Access Control (RBAC): Strict separation of privileges for STUDENT, INSTRUCTOR, and ADMIN roles
.
Password Encryption: All user passwords are encrypted using bcrypt before database storage
.
Tokens: JWT token issuance is utilized for secure, stateless session handling during login
.
Vulnerability Protection: Built-in safeguards against common web exploits including SQL Injection and Cross-Site Scripting (XSS)
.
🔮 Future Improvements
The development roadmap for Track Finder includes the following future enhancements:
Adaptive Assessments: Dynamic questions that adjust in difficulty based on the learner's ongoing performance
.
AI-Powered Recommendations: Smarter algorithms for matching students to exact courses and tracks
.
Mobile Applications: Dedicated native clients for iOS and Android devices
.
Live Sessions: Instructor-led live webinars and real-time classes
.
Verified Credentials: Blockchain-anchored digital certificates for unforgeable proof of completion
.
🤝 Contributing
Contributions make the open-source community an amazing place to learn, inspire, and create. Any contributions you make are greatly appreciated.
Fork the Project
Create your Feature Branch (git checkout -b feature/AmazingFeature)
Commit your Changes (git commit -m 'Add some AmazingFeature')
Push to the Branch (git push origin feature/AmazingFeature)
Open a Pull Request
📜 License
Distributed under the MIT License.
🙏 Acknowledgments
This platform was developed as a 2026 Graduation Project in association with NEXT Career Development Academy and Rowad Masr Digital
.
Project Supervisor: Eng. Maged Samir
Development Team:
Mohamed Wael (Team Leader)
Mohamed Khaled
Ahmed Ehab
Abdelrahman Mohamed
Amr Khaled
Shymaa Fekry
Reham Ahmed
