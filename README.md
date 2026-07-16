# 🎓 Track Finder

[![Live Demo](https://img.shields.io/badge/Live%20Demo-trackfinder.runasp.net-success?style=for-the-badge&logo=googlechrome&logoColor=white)](https://trackfinder.runasp.net/)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![Entity Framework Core](https://img.shields.io/badge/Entity%20Framework-3dbfb8?style=for-the-badge&logo=dotnet&logoColor=white)
![Bootstrap](https://img.shields.io/badge/Bootstrap-563D7C?style=for-the-badge&logo=bootstrap&logoColor=white)
![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg?style=for-the-badge)

## 📖 Professional Introduction

**Track Finder** is an intelligent, full-stack learning platform designed to help students discover and navigate their ideal career paths. Many learners struggle to identify learning tracks that align with their skills, wasting time and resources on disconnected materials. Track Finder solves this by providing assessment-based recommendations, structured learning roadmaps, curated courses from trusted instructors, and a supportive community forum. 

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

**Experience the platform live:** [https://trackfinder.runasp.net/](https://trackfinder.runasp.net/)

---

## 🎯 Project Overview

Track Finder is a standalone, web-based e-learning system with three tightly integrated experiences for **Students**, **Instructors**, and **Administrators**. The platform turns a passive course catalog into an active advisor by assessing a learner's baseline and intelligently recommending a specific career track (such as AI, Web Development, or Cybersecurity). It centralizes course consumption, progress tracking, community engagement, and platform management into a single cohesive ecosystem.

---

## ✨ Features

<details>
<summary><strong>🎓 Student Experience</strong></summary>

* **Skill Assessments:** Adaptive questions that measure a learner's baseline to recommend suitable career tracks and technology stacks.
* **Structured Learning:** Browse and enroll in curated paths containing videos, documents, and downloadable offline resources.
* **Achievements & Recognition:** Earn digital mastery badges and verified certificates upon successful course completion.
* **Community Engagement:** Publish posts, ask questions, share code snippets, and follow specific tech communities.
</details>

<details>
<summary><strong>👨‍🏫 Instructor Workspace</strong></summary>

* **Course Management:** Create, edit, and organize courses, lessons, and assignments.
* **Student Monitoring:** Track student engagement, progress, and performance in real-time.
* **Content Delivery:** Upload instructional videos, PDF materials, and quizzes/exams.
</details>

<details>
<summary><strong>🛡️ Admin Control Panel</strong></summary>

* **Centralized Dashboard:** Monitor live statistics, track enrollments, and view system metrics.
* **Content Taxonomy:** Manage courses, structured tracks, and individual technology stacks.
* **User & Role Management:** Ban/unban users, approve instructor accounts, and manage cross-role permissions.
* **Moderation:** Review reported content and ensure community safety.
</details>

---

## 💻 Tech Stack

**Backend**
* C# / ASP.NET Core MVC
* Entity Framework Core (ORM)

**Frontend**
* HTML5, CSS3, JavaScript
* Razor Pages
* Bootstrap CSS

**Database & Hosting**
* Microsoft SQL Server
* IIS Hosting Environment

**Tools**
* Git & GitHub
* draw.io (System Modeling)

---

## 🏗️ System Architecture

Track Finder follows a strict layered architecture based on the **Model-View-Controller (MVC)** design pattern. This separates the presentation layer (Views/Razor), business logic (Controllers), and data access layers (Models/ViewModels) to ensure high maintainability and scalability. 

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

*Note: The exact directory tree is Not specified in the documentation. However, the system strictly follows standard ASP.NET Core MVC scaffolding conventions.*


TrackFinder/
├── Controllers/       # Handles incoming HTTP requests and business logic routing
├── Models/            # Database entities (User, Course, Enrollment, etc.)
├── Views/             # Razor pages and frontend UI components
├── ViewModels/        # DTOs separating database entities from the presentation layer
├── wwwroot/           # Static assets (CSS, JS, Images, Bootstrap)
├── Data/              # Entity Framework DbContext and Migrations
└── Program.cs         # Application entry point and service registration

🖥️ UI Screenshots & Interfaces
Landing Page

<img width="1920" height="2880" alt="1(0)" src="https://github.com/user-attachments/assets/46e6c941-9ec6-4e7d-af3d-73b02f1055b6" />

 The Landing Page serves as the main entry point to the Track Finder platform, introducing users to the platform's purpose and highlighting its key features. It is designed with an attractive and clean interface to encourage visitors to explore and begin their learning journey.
 
Secure Sign In

<img width="2275" height="737" alt="mm" src="https://github.com/user-attachments/assets/bf5cffce-c86f-4202-a410-e956fee7c2f0" />

 The Sign In page provides secure authentication for registered users to access their accounts. It dynamically directs users to personalized features based on their assigned role (Student, Instructor, or Admin).
 
Student Dashboard

<img width="1920" height="1868" alt="5" src="https://github.com/user-attachments/assets/f8e4aca9-e650-465c-9ffe-99b5c2528f07" />


 The Student Dashboard provides a personalized overview of all learning activities. It prominently displays enrolled courses, learning progress, recent achievements, and upcoming tasks for quick access.
 
Instructor Dashboard

<img width="1920" height="3528" alt="6" src="https://github.com/user-attachments/assets/78ecfe22-cf11-4af0-9fc8-aa40753fc88b" />

 The Instructor Dashboard acts as a centralized command center for educators to maintain complete control over their content. It provides streamlined access to course management, student performance monitoring, and platform statistics.

 
Assessment & Recommended Tracks

<img width="1920" height="1432" alt="12" src="https://github.com/user-attachments/assets/1fc6ec9c-fc24-4bbf-96f2-4cc1ac8b64db" />


 Users are presented with a distraction-free assessment environment to accurately measure their technical skills. Based on the results, the platform's algorithm suggests the most suitable learning tracks and customized technology stacks for the student's career goals.
 
Community Hub

<img width="1920" height="2021" alt="9" src="https://github.com/user-attachments/assets/3550ed1a-9484-41f9-8566-1aaa57958208" />

 The Community page fosters a collaborative environment where students and instructors can interact. Users can publish posts, share technical knowledge, ask questions, and engage in ongoing discussions.
Achievements & Badges

Admin Dashboard

<img width="1920" height="1907" alt="21" src="https://github.com/user-attachments/assets/4dfbd965-309c-443c-998d-0eead6f4248d" />

 The centralized Admin Dashboard provides administrators with a complete, top-down overview of the entire system. It offers rapid access to management tools, user activity logs, and detailed platform statistics.
 
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
New Users: Navigate to the Landing Page and click Get Started to register as either a Student or Instructor.
Students: Begin by taking the Career Assessment to receive a tailored technology track recommendation, then browse and enroll in recommended courses.
Instructors: Await admin verification. Once approved, use the Teacher Studio to create new courses, upload video/document materials, and monitor community discussions.
Admins: Log in with administrative credentials to access the centralized dashboard to review reported content, issue badges, and manage technical tracks.
📡 API Endpoints
Not specified. (The system is documented as a traditional ASP.NET Core MVC application utilizing Razor Pages and Views rather than a standalone RESTful API architecture).
🔒 Authentication & Security
Role-Based Access Control (RBAC): Strict separation of privileges for STUDENT, INSTRUCTOR, and ADMIN roles.
Password Encryption: All user passwords are encrypted using bcrypt before database storage.
Tokens: JWT token issuance is utilized for secure, stateless session handling during login.
Vulnerability Protection: Built-in safeguards against common web exploits including SQL Injection and Cross-Site Scripting (XSS).
🔮 Future Improvements
The development roadmap for Track Finder includes the following future enhancements:
Adaptive Assessments: Dynamic questions that adjust in difficulty based on the learner's ongoing performance.
AI-Powered Recommendations: Smarter algorithms for matching students to exact courses and tracks.
Mobile Applications: Dedicated native clients for iOS and Android devices.
Live Sessions: Instructor-led live webinars and real-time classes.
Verified Credentials: Blockchain-anchored digital certificates for unforgeable proof of completion.
---
##🤝 Contributing

Contributions make the open-source community an amazing place to learn, inspire, and create. Any contributions you make are greatly appreciated.
Fork the Project
Create your Feature Branch (git checkout -b feature/AmazingFeature)
Commit your Changes (git commit -m 'Add some AmazingFeature')
Push to the Branch (git push origin feature/AmazingFeature)
Open a Pull Request

📜 License
Distributed under the MIT License.
👥 Contributors & Acknowledgments
This platform was developed as a Final Graduation Project for the FULL-Stack.NET Developer Track
.
👨‍🏫 Eng. Maged Samir
Role: Instructor / Project Supervisor
👑 Mohamed Wael
Role: Team Leader
💻 Mohamed Khaled
Role: Team Member (Full-Stack .NET Developer)
💻 Ahmed Ehab
Role: Team Member (Full-Stack .NET Developer)
💻 Abdelrahman Mohamed
Role: Team Member (Full-Stack .NET Developer)
💻 Amr Khaled
Role: Team Member (Full-Stack .NET Developer)
💻 Shymaa Fekry
Role: Team Member (Full-Stack .NET Developer)
💻 Reham Ahmed
Role: Team Member (Full-Stack .NET Developer)
