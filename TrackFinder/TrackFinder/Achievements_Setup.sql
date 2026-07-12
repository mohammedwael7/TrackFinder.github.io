USE TrackFinderDb;
GO

-- =========================================================================
-- أولاً: البيانات الأساسية للسيستم (جداول الشارات والشهادات الأصلية)
-- =========================================================================

-- 1. كود إدخال البادجز الأساسية المتاحة في السيستم
INSERT INTO [Badges] ([BadgeName], [BadgeDescription], [Level], [UnlockCondition], [BadgeIconClass])
VALUES 
('First Login', 'Welcome to Track Finder!', 1, 'Log in for the first time', 'bi bi-patch-check'),
('Course Starter', 'Your journey begins!', 2, 'Enroll in your first learning track', 'bi bi-lightning'),
('Community Helper', 'Supporting peers.', 1, 'Post 5 helpful comments', 'bi bi-people');

-- 2. كود إدخال شهادات المسارات التعليمية المتاحة في السيستم
INSERT INTO [Certificates] ([Title], [Description], [CertificateUrl], [UnlockRequirement])
VALUES 
('Frontend Foundations', 'Mastery of modern HTML, CSS grids, and layouts.', '/certificates/templates/frontend.pdf', 'Complete HTML & CSS Path'),
('Backend APIs', 'Proficiency in .NET MVC, controllers, and routing.', '/certificates/templates/backend.pdf', 'Complete .NET Core Path');


-- =========================================================================
-- ثانياً: بيانات ربط وتست الديمو (عشان الكروت تنور أزرق عل طول عند التيم)
-- =========================================================================

-- 3. منح أول طالب في جدول الـ Students البادج رقم 1 ورقم 2 عشان ينوروا أزرق
INSERT INTO [UserBadges] ([UserId], [BadgeId], [EarnedAt], [IsEarned])
VALUES 
((SELECT TOP 1 [UserId] FROM [Students]), 1, GETUTCDATE(), 1),
((SELECT TOP 1 [UserId] FROM [Students]), 2, GETUTCDATE(), 1);

-- 4. منح أول طالب شهادات مكتسبة حقيقية عشان تظهر في الـ Dashboard بتاعته
INSERT INTO [StudentCertificates] ([CredentialsId], [StudentId], [CertificateId], [CourseId], [IssuedAt], [IsFeatured])
VALUES 
(NEWID(), (SELECT TOP 1 [UserId] FROM [Students]), 1, (SELECT TOP 1 [Id] FROM [Courses]), GETUTCDATE(), 1),
(NEWID(), (SELECT TOP 1 [UserId] FROM [Students]), 2, (SELECT TOP 1 [Id] FROM [Courses]), GETUTCDATE(), 0);
