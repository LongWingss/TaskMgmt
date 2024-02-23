SET IDENTITY_INSERT Groups ON;
INSERT INTO [Groups] (GroupId,GroupName, CreatedAt) VALUES
(1,'Software Development', GETDATE()),
(2,'Marketing', GETDATE()),
(3,'Product Design', GETDATE()),
(4,'Human Resources', GETDATE()),
(5,'Finance', GETDATE());
SET IDENTITY_INSERT Groups OFF;

SET IDENTITY_INSERT Users ON;
-- Default Password : pass123
INSERT INTO [Users] (UserId, Username, Email, PasswordHash, CreatedAt, DefaultGroupId) VALUES
(1,'JohnDoe', 'john.doe@example.com', '15513510516416766149154452152195111183635242223172218132543512524014114125391555576', GETDATE(), 1),
(2,'JaneSmith', 'jane.smith@example.com', '15513510516416766149154452152195111183635242223172218132543512524014114125391555576', GETDATE(), 1),
(3,'MikeBrown', 'mike.brown@example.com', '15513510516416766149154452152195111183635242223172218132543512524014114125391555576', GETDATE(), 2),
(4,'SarahConor', 'sarah.conor@example.com', '15513510516416766149154452152195111183635242223172218132543512524014114125391555576', GETDATE(), 2),
(5,'TonyStark', 'tony.stark@example.com', '15513510516416766149154452152195111183635242223172218132543512524014114125391555576', GETDATE(), 3),
(6,'BruceWayne', 'bruce.wayne@example.com', '15513510516416766149154452152195111183635242223172218132543512524014114125391555576', GETDATE(), 1),
(7,'ClarkKent', 'clark.kent@example.com', '15513510516416766149154452152195111183635242223172218132543512524014114125391555576', GETDATE(), 2),
(8,'DianaPrince', 'diana.prince@example.com', '15513510516416766149154452152195111183635242223172218132543512524014114125391555576', GETDATE(), 3),
(9,'BarryAllen', 'barry.allen@example.com', '15513510516416766149154452152195111183635242223172218132543512524014114125391555576', GETDATE(), 4),
(10,'HalJordan', 'hal.jordan@example.com', '15513510516416766149154452152195111183635242223172218132543512524014114125391555576', GETDATE(), 5),
(11,'ArthurCurry', 'arthur.curry@example.com', '15513510516416766149154452152195111183635242223172218132543512524014114125391555576', GETDATE(), 1),
(12,'VictorStone', 'victor.stone@example.com', '15513510516416766149154452152195111183635242223172218132543512524014114125391555576', GETDATE(), 2),
(13,'PeterParker', 'peter.parker@example.com', '15513510516416766149154452152195111183635242223172218132543512524014114125391555576', GETDATE(), 3),
(14,'WadeWilson', 'wade.wilson@example.com', '15513510516416766149154452152195111183635242223172218132543512524014114125391555576', GETDATE(), 4),
(15,'MattMurdock', 'matt.murdock@example.com', '15513510516416766149154452152195111183635242223172218132543512524014114125391555576', GETDATE(), 5);
SET IDENTITY_INSERT Users OFF;

INSERT INTO [UserGroups] (UserId, GroupId, IsAdmin) VALUES
(1, 1, 1), -- John Doe as admin in Group 1
(2, 1, 0), -- Jane Smith in Group 1
(3, 2, 1), -- Alice Johnson as admin in Group 2
(4, 2, 0), -- Bob Brown in Group 2
(5, 3, 1), -- Carol Davis as admin in Group 3
(1, 2, 0), -- John Doe also in Group 2
(2, 3, 0), -- Jane Smith also in Group 3
(6, 1, 0), -- Bruce Wayne in Software Development
(7, 2, 1), -- Clark Kent as admin in Marketing
(8, 3, 0), -- Diana Prince in Product Design
(9, 4, 1), -- Barry Allen as admin in Human Resources
(10, 5, 0), -- Hal Jordan in Finance
(11, 1, 1), -- Arthur Curry as admin in Software Development
(11, 2, 0), -- Arthur Curry also in Marketing
(12, 3, 1), -- Victor Stone as admin in Product Design
(12, 4, 0), -- Victor Stone also in Human Resources
(13, 5, 1), -- Peter Parker as admin in Finance
(13, 1, 0), -- Peter Parker also in Software Development
(14, 2, 1), -- Wade Wilson as admin in Marketing
(14, 3, 0), -- Wade Wilson also in Product Design
(15, 4, 1), -- Matt Murdock as admin in Human Resources
(15, 5, 0); -- Matt Murdock also in Finance

SET IDENTITY_INSERT Projects ON;
-- Insert Projects for Software Development Group (Group 1)
INSERT INTO [Projects] (ProjectId, GroupId, ProjectName, ProjectDescription, OwnerId, CreatedAt) VALUES
(1,1, 'Agile Workflow Tool', 'Developing an agile project management tool.', 1, GETDATE()),
(2,1, 'Code Review System', 'Creating a system to streamline code reviews.', 2, GETDATE()),
(3,1, 'DevOps Pipeline', 'Setting up a CI/CD pipeline for automated deployments.', 3, GETDATE()),
(4,1, 'API Development', 'Developing a robust API for external integrations.', 6, GETDATE()),
(5,1, 'Cloud Migration', 'Migrating all services to a cloud-based infrastructure.', 11, GETDATE()),
(6,1, 'Security Enhancement', 'Enhancing security protocols across all platforms.', 6, GETDATE()),
(7,1, 'Mobile App Launch', 'Launching a new mobile application for our services.', 11, GETDATE()),
(8,1, 'Data Analytics Tool', 'Developing an in-house data analytics and visualization tool.', 6, GETDATE()),
(9,1, 'Customer Support Platform', 'Creating a new platform for improved customer support.', 11, GETDATE()),
(10,1, 'Blockchain Integration', 'Exploring blockchain technology for secure transactions.', 6, GETDATE()),
(11,1, 'New Payment Gateway', 'Integrating a new payment gateway to facilitate easier payments.', 11, GETDATE()),
(12,1, 'User Experience Revamp', 'Revamping the user experience for our main product.', 6, GETDATE()),
(13,1, 'Automated Testing System', 'Developing an automated testing system for continuous integration.', 11, GETDATE()),
(14,1, 'Feedback Analysis Tool', 'Creating a tool to analyze customer feedback more efficiently.', 6, GETDATE()),
(15,1, 'Project Management Suite', 'Building a suite of project management tools.', 11, GETDATE()),
(16,1, 'AI-Based Recommendations', 'Implementing AI-based recommendation systems for users.', 6, GETDATE()),
(17,1, 'Performance Optimization', 'Optimizing performance across all user interfaces.', 11, GETDATE()),
(18,1, 'Scalability Project', 'Projects focused on scaling the application for global users.', 6, GETDATE());


-- Group 2: Marketing
INSERT INTO [Projects] (ProjectId,GroupId, ProjectName, ProjectDescription, OwnerId, CreatedAt) VALUES
(19,2, 'Social Media Campaign', 'Launching a new social media campaign for product X.', 7, GETDATE()),
(20,2, 'SEO Optimization Project', 'Project aimed at improving SEO rankings.', 12, GETDATE()),
(21,2, 'Customer Loyalty Program', 'Developing a new customer loyalty program.', 7, GETDATE()),
(22,2, 'Email Marketing Automation', 'Implementing new email marketing automation tools.', 12, GETDATE()),
(23,2, 'Market Research for Product Y', 'Conducting market research for upcoming product Y.', 7, GETDATE()),
(24,2, 'Brand Refresh Initiative', 'Initiative to refresh the brand across all platforms.', 12, GETDATE()),
(25,2, 'Influencer Partnership Program', 'Establishing new partnerships with key influencers.', 7, GETDATE()),
(26,2, 'Content Marketing Strategy', 'Developing a new content marketing strategy.', 12, GETDATE()),
(27,2, 'Video Marketing Project', 'Launching a new video marketing campaign.', 7, GETDATE()),
(28,2, 'International Market Expansion', 'Expanding our product reach to international markets.', 12, GETDATE()),
(29,2, 'Customer Feedback System', 'Implementing a new system for collecting customer feedback.', 7, GETDATE()),
(30,2, 'Digital Ad Campaigns', 'Creating a series of digital ad campaigns for various platforms.', 12, GETDATE()),
(31,2, 'Website User Experience Enhancement', 'Enhancing the user experience of our official website.', 7, GETDATE()),
(32,2, 'Event Sponsorship and Participation', 'Managing event sponsorship and participation for brand visibility.', 12, GETDATE()),
(33,2, 'Marketing Data Analytics Platform', 'Developing a platform for marketing data analytics and insights.', 7, GETDATE());

-- Group 3: Product Design
INSERT INTO [Projects] (ProjectId,GroupId, ProjectName, ProjectDescription, OwnerId, CreatedAt) VALUES
(34,3, 'UI/UX Redesign for App Z', 'Complete UI/UX redesign for application Z.', 8, GETDATE()),
(35,3, 'New Product Concept Development', 'Developing concepts for a new product line.', 13, GETDATE()),
(36,3, 'Packaging Design Project', 'Redesigning packaging for our eco-friendly product line.', 8, GETDATE()),
(37,3, 'Design System Overhaul', 'Overhauling our entire design system for consistency.', 13, GETDATE()),
(38,3, 'Accessibility Improvement Project', 'Improving product accessibility for all users.', 8, GETDATE()),
(39,3, '3D Modeling for New Products', 'Creating 3D models for new product prototypes.', 13, GETDATE()),
(40,3, 'User Research for Feature Update', 'Conducting user research for upcoming feature updates.', 8, GETDATE()),
(41,3, 'Virtual Reality Product Demos', 'Developing virtual reality demos for our products.', 13, GETDATE()),
(42,3, 'Design Sprint for Service X', 'Running a design sprint to improve Service X.', 8, GETDATE()),
(43,3, 'Brand Identity Development', 'Creating a new brand identity for market differentiation.', 13, GETDATE()),
(44,3, 'Interactive Design for E-commerce', 'Enhancing e-commerce experience with interactive design.', 8, GETDATE()),
(45,3, 'Mobile App Interface Update', 'Updating the mobile app interface for better user engagement.', 13, GETDATE()),
(46,3, 'Prototype Testing and Feedback', 'Testing prototypes and gathering user feedback for improvements.', 8, GETDATE()),
(47,3, 'Design Collaboration Tool', 'Creating a tool for better collaboration within the design team.', 13, GETDATE()),
(48,3, 'Sustainable Design Practices', 'Incorporating sustainable design practices into our processes.', 8, GETDATE());

-- Group 4: Human Resources
INSERT INTO [Projects] (ProjectId,GroupId, ProjectName, ProjectDescription, OwnerId, CreatedAt) VALUES
(49,4, 'Employee Onboarding System', 'Developing a new employee onboarding system.', 9, GETDATE()),
(50,4, 'Workplace Diversity Initiative', 'Initiative aimed at increasing workplace diversity.', 14, GETDATE()),
(51,4, 'Employee Training Platform', 'Creating a comprehensive platform for employee training.', 9, GETDATE()),
(52,4, 'Performance Review System Overhaul', 'Overhauling the current performance review system.', 14, GETDATE()),
(53,4, 'Health and Wellness Program', 'Implementing a new health and wellness program for employees.', 9, GETDATE()),
(54,4, 'Recruitment Process Automation', 'Automating various aspects of the recruitment process.', 14, GETDATE()),
(55,4, 'Employee Satisfaction Survey', 'Conducting a survey to gauge employee satisfaction.', 9, GETDATE()),
(56,4, 'Leadership Development Program', 'Developing a program for upcoming leaders within the company.', 14, GETDATE()),
(57,4, 'Remote Work Infrastructure Project', 'Improving infrastructure to support remote work.', 9, GETDATE()),
(58,4, 'Team Building Activities', 'Organizing team building activities for better collaboration.', 14, GETDATE()),
(59,4, 'HR Data Analytics Initiative', 'Initiating a project to leverage HR data for insights.', 9, GETDATE()),
(60,4, 'Compensation and Benefits Review', 'Reviewing compensation and benefits to stay competitive.', 14, GETDATE()),
(61,4, 'Conflict Resolution Workshops', 'Hosting workshops on conflict resolution for management.', 9, GETDATE()),
(62,4, 'Employee Retention Strategies', 'Developing strategies to improve employee retention rates.', 14, GETDATE()),
(63,4, 'Organizational Culture Revamp', 'Project aimed at revamping the organizational culture.', 9, GETDATE());


-- Group 5: Finance
INSERT INTO [Projects] (ProjectId,GroupId, ProjectName, ProjectDescription, OwnerId, CreatedAt) VALUES
(64,5, 'Financial Reporting System Upgrade', 'Upgrading the system used for financial reporting.', 10, GETDATE()),
(65,5, 'Cost Reduction Initiative', 'Initiative aimed at identifying and implementing cost reductions.', 15, GETDATE()),
(66,5, 'Investment Strategy Revision', 'Revising the company’s investment strategy for better returns.', 10, GETDATE()),
(67,5, 'Budgeting Software Implementation', 'Implementing new software to streamline budgeting processes.', 15, GETDATE()),
(68,5, 'Audit Process Automation', 'Automating various aspects of the audit process.', 10, GETDATE()),
(69,5, 'Financial Compliance Project', 'Ensuring compliance with new financial regulations.', 15, GETDATE()),
(70,5, 'Revenue Growth Strategies', 'Developing strategies to achieve significant revenue growth.', 10, GETDATE()),
(71,5, 'Expense Tracking System', 'Implementing a new system for tracking expenses.', 15, GETDATE()),
(72,5, 'Financial Literacy Program for Employees', 'Launching a financial literacy program for all employees.', 10, GETDATE()),
(73,5, 'Risk Management Framework', 'Developing a comprehensive risk management framework.', 15, GETDATE()),
(74,5, 'Cash Flow Improvement Plan', 'Planning and implementing measures to improve cash flow.', 10, GETDATE()),
(75,5, 'Strategic Financial Planning', 'Engaging in strategic financial planning for the next fiscal year.', 15, GETDATE()),
(76,5, 'Cryptocurrency Investment Analysis', 'Analyzing potential cryptocurrency investments.', 10, GETDATE()),
(77,5, 'Financial Data Visualization Tools', 'Introducing tools for better visualization of financial data.', 15, GETDATE()),
(78,5, 'Mergers and Acquisitions Strategy', 'Formulating a strategy for potential mergers and acquisitions.', 10, GETDATE());
SET IDENTITY_INSERT Projects OFF;

DECLARE @SQL NVARCHAR(MAX) = '';
SELECT @SQL = @SQL + 'INSERT INTO ProjectTaskStatuses (ProjectId, StatusText, StatusColor) VALUES (' + CAST(ProjectId AS VARCHAR) + ', ''Not Started'', ''#FFFFFF''), (' + CAST(ProjectId AS VARCHAR) + ', ''In Progress'', ''#FFFF00''), (' + CAST(ProjectId AS VARCHAR) + ', ''Completed'', ''#00FF00''); '
FROM Projects;
PRINT @SQL
EXECUTE(@SQL)




SET IDENTITY_INSERT  ProjectTasks ON;
DECLARE @ProjectId INT;
DECLARE @StatusId INT;
-- Group 1
-- Tasks for "API Development" Project
SELECT @ProjectId = [ProjectId] FROM [Projects] WHERE [ProjectName] = 'API Development';
SELECT @StatusId = ProjectTaskStatusId FROM ProjectTaskStatuses WHERE StatusText = 'Not Started' AND [ProjectId] = @ProjectId;
INSERT INTO [ProjectTasks] (ProjectTaskId, Description, CreatedAt, DueDate, AssigneeId, CreatorId, ProjectId, CurrentStatusId) VALUES
(1,'Define API requirements', GETDATE(), DATEADD(DAY, 10, GETDATE()), 1, 1, @ProjectId, @StatusId),
(2,'Design API architecture', GETDATE(), DATEADD(DAY, 20, GETDATE()), 2, 1, @ProjectId, @StatusId),
(3,'Implement API endpoints', GETDATE(), DATEADD(DAY, 30, GETDATE()), 3, 1, @ProjectId, @StatusId),
(4,'API documentation', GETDATE(), DATEADD(DAY, 40, GETDATE()), 4, 1, @ProjectId, @StatusId),
(5,'Integration testing', GETDATE(), DATEADD(DAY, 50, GETDATE()), 5, 1, @ProjectId, @StatusId);
GO

DECLARE @ProjectId INT;
DECLARE @StatusId INT;
-- Tasks for "Cloud Migration" Project
SELECT @ProjectId = [ProjectId] FROM [Projects] WHERE [ProjectName] = 'Cloud Migration';
SELECT @StatusId = ProjectTaskStatusId FROM ProjectTaskStatuses WHERE StatusText = 'Not Started' AND ProjectId = @ProjectId;
INSERT INTO [ProjectTasks] (ProjectTaskId,Description, CreatedAt, DueDate, AssigneeId, CreatorId, ProjectId, CurrentStatusId) VALUES
(6,'Evaluate cloud providers', GETDATE(), DATEADD(DAY, 15, GETDATE()), 6, 1, @ProjectId, @StatusId),
(7,'Plan migration strategy', GETDATE(), DATEADD(DAY, 25, GETDATE()), 11, 1, @ProjectId, @StatusId),
(8,'Migrate development environment', GETDATE(), DATEADD(DAY, 35, GETDATE()), 6, 1, @ProjectId, @StatusId),
(9,'Perform security assessments', GETDATE(), DATEADD(DAY, 45, GETDATE()), 11, 1, @ProjectId, @StatusId),
(10,'Conduct performance testing', GETDATE(), DATEADD(DAY, 55, GETDATE()), 6, 1, @ProjectId, @StatusId);
 GO

DECLARE @ProjectId INT;
DECLARE @StatusId INT;
-- Group 2
-- Tasks for "Social Media Campaign" Project
SELECT @ProjectId = [ProjectId] FROM [Projects] WHERE [ProjectName] = 'Social Media Campaign';
SELECT @StatusId = ProjectTaskStatusId FROM ProjectTaskStatuses WHERE StatusText = 'Not Started' AND ProjectId = @ProjectId;
INSERT INTO [ProjectTasks] (ProjectTaskId,Description, CreatedAt, DueDate, AssigneeId, CreatorId, ProjectId, CurrentStatusId) VALUES
(11,'Identify target audience', GETDATE(), DATEADD(DAY, 10, GETDATE()), 7, 1,@ProjectId, @StatusId),
(12,'Create campaign content', GETDATE(), DATEADD(DAY, 20, GETDATE()), 12, 1,@ProjectId, @StatusId),
(13,'Schedule campaign posts', GETDATE(), DATEADD(DAY, 30, GETDATE()), 7, 1,@ProjectId, @StatusId),
(14,'Monitor engagement metrics', GETDATE(), DATEADD(DAY, 40, GETDATE()), 12, 1,@ProjectId, @StatusId),
(15,'Analyze campaign performance', GETDATE(), DATEADD(DAY, 50, GETDATE()), 7, 1,@ProjectId, @StatusId);
GO

DECLARE @ProjectId INT;
DECLARE @StatusId INT;
-- Tasks for "SEO Optimization Project"
SELECT @ProjectId = [ProjectId] FROM [Projects] WHERE [ProjectName] = 'SEO Optimization Project';
SELECT @StatusId = ProjectTaskStatusId FROM ProjectTaskStatuses WHERE StatusText = 'Not Started' AND ProjectId = @ProjectId;
INSERT INTO [ProjectTasks] (ProjectTaskId,Description, CreatedAt, DueDate, AssigneeId, CreatorId, ProjectId, CurrentStatusId) VALUES
(16,'Keyword research', GETDATE(), DATEADD(DAY, 12, GETDATE()), 7, 1,@ProjectId, @StatusId),
(17,'Optimize website content', GETDATE(), DATEADD(DAY, 24, GETDATE()), 12, 1,@ProjectId, @StatusId),
(18,'Improve site loading speed', GETDATE(), DATEADD(DAY, 36, GETDATE()), 7, 1,@ProjectId, @StatusId),
(19,'Build quality backlinks', GETDATE(), DATEADD(DAY, 48, GETDATE()), 12, 1,@ProjectId, @StatusId),
(20,'Track ranking improvements', GETDATE(), DATEADD(DAY, 60, GETDATE()), 7, 1,@ProjectId, @StatusId);
GO

DECLARE @ProjectId INT;
DECLARE @StatusId INT;
--Group 3
-- Tasks for "UI/UX Redesign for App Z" Project
SELECT @ProjectId = [ProjectId] FROM [Projects] WHERE [ProjectName] = 'UI/UX Redesign for App Z';
SELECT @StatusId = ProjectTaskStatusId FROM ProjectTaskStatuses WHERE StatusText = 'Not Started' AND ProjectId = @ProjectId;
INSERT INTO [ProjectTasks] (ProjectTaskId,Description, CreatedAt, DueDate, AssigneeId, CreatorId, ProjectId, CurrentStatusId) VALUES
(21,'Gather user feedback', GETDATE(), DATEADD(DAY, 10, GETDATE()), 8, 1,@ProjectId, @StatusId),
(22,'Create wireframes', GETDATE(), DATEADD(DAY, 20, GETDATE()), 13, 1,@ProjectId, @StatusId),
(23,'Develop new UI prototypes', GETDATE(), DATEADD(DAY, 30, GETDATE()), 8, 1,@ProjectId, @StatusId),
(24,'User testing of prototypes', GETDATE(), DATEADD(DAY, 40, GETDATE()), 13, 1,@ProjectId, @StatusId),
(25,'Finalize UI designs', GETDATE(), DATEADD(DAY, 50, GETDATE()), 8, 1,@ProjectId, @StatusId);
GO

DECLARE @ProjectId INT;
DECLARE @StatusId INT;
-- Tasks for "New Product Concept Development"
SELECT @ProjectId = [ProjectId] FROM [Projects] WHERE [ProjectName] = 'New Product Concept Development';
SELECT @StatusId = ProjectTaskStatusId FROM ProjectTaskStatuses WHERE StatusText = 'Not Started' AND ProjectId = @ProjectId;
INSERT INTO [ProjectTasks] (ProjectTaskId,Description, CreatedAt, DueDate, AssigneeId, CreatorId, ProjectId, CurrentStatusId) VALUES
(26,'Market analysis for new concept', GETDATE(), DATEADD(DAY, 12, GETDATE()), 8, 1,@ProjectId, @StatusId),
(27,'Sketch initial concept ideas', GETDATE(), DATEADD(DAY, 24, GETDATE()), 13, 1,@ProjectId, @StatusId),
(28,'Develop 3D models of concepts', GETDATE(), DATEADD(DAY, 36, GETDATE()), 8, 1,@ProjectId, @StatusId),
(29,'Concept presentation to stakeholders', GETDATE(), DATEADD(DAY, 48, GETDATE()), 13, 1,@ProjectId, @StatusId),
(30,'Gather feedback and select final concept', GETDATE(), DATEADD(DAY, 60, GETDATE()), 8, 1,@ProjectId, @StatusId);
GO

DECLARE @ProjectId INT;
DECLARE @StatusId INT;
-- Group 4
-- Tasks for "Employee Onboarding System" Project
SELECT @ProjectId = [ProjectId] FROM [Projects] WHERE [ProjectName] = 'Employee Onboarding System';
SELECT @StatusId = ProjectTaskStatusId FROM ProjectTaskStatuses WHERE StatusText = 'Not Started' AND ProjectId = @ProjectId;
INSERT INTO [ProjectTasks] (ProjectTaskId,Description, CreatedAt, DueDate, AssigneeId, CreatorId, ProjectId, CurrentStatusId) VALUES
(31,'Identify onboarding process gaps', GETDATE(), DATEADD(DAY, 15, GETDATE()), 9, 1,@ProjectId, @StatusId),
(32,'Design new onboarding workflow', GETDATE(), DATEADD(DAY, 25, GETDATE()), 14, 1,@ProjectId, @StatusId),
(33,'Develop onboarding system prototype', GETDATE(), DATEADD(DAY, 35, GETDATE()), 9, 1,@ProjectId, @StatusId),
(34,'Test prototype with new hires', GETDATE(), DATEADD(DAY, 45, GETDATE()), 14, 1,@ProjectId, @StatusId),
(35,'Launch new onboarding system', GETDATE(), DATEADD(DAY, 55, GETDATE()), 9, 1,@ProjectId, @StatusId);
GO

DECLARE @ProjectId INT;
DECLARE @StatusId INT;
-- Tasks for "Workplace Diversity Initiative"
SELECT @ProjectId = [ProjectId] FROM [Projects] WHERE [ProjectName] = 'Workplace Diversity Initiative';
SELECT @StatusId = ProjectTaskStatusId FROM ProjectTaskStatuses WHERE StatusText = 'Not Started' AND ProjectId = @ProjectId;
INSERT INTO [ProjectTasks] (ProjectTaskId,Description, CreatedAt, DueDate, AssigneeId, CreatorId, ProjectId, CurrentStatusId) VALUES
(36,'Conduct diversity training sessions', GETDATE(), DATEADD(DAY, 18, GETDATE()), 9, 1,@ProjectId, @StatusId),
(37,'Review hiring practices for bias elimination', GETDATE(), DATEADD(DAY, 28, GETDATE()), 9, 1,@ProjectId, @StatusId),
(38,'Develop inclusive job descriptions', GETDATE(), DATEADD(DAY, 42, GETDATE()), 14, 1,@ProjectId, @StatusId),
(39,'Outreach to diverse talent pools', GETDATE(), DATEADD(DAY, 56, GETDATE()), 9, 1,@ProjectId, @StatusId),
(40,'Implement diversity-focused recruitment', GETDATE(), DATEADD(DAY, 70, GETDATE()), 14, 1,@ProjectId, @StatusId),
(41,'Evaluate and adjust recruitment metrics', GETDATE(), DATEADD(DAY, 84, GETDATE()), 9, 1,@ProjectId, @StatusId);
GO

DECLARE @ProjectId INT;
DECLARE @StatusId INT;
--Group 5
-- Continuing with "Financial Reporting System Upgrade"
SELECT @ProjectId = [ProjectId] FROM [Projects] WHERE [ProjectName] = 'Financial Reporting System Upgrade';
SELECT @StatusId = ProjectTaskStatusId FROM ProjectTaskStatuses WHERE StatusText = 'Not Started' AND ProjectId = @ProjectId;
INSERT INTO [ProjectTasks] (ProjectTaskId,Description, CreatedAt, DueDate, AssigneeId, CreatorId, ProjectId, CurrentStatusId) VALUES
(42,'Define system requirements', GETDATE(), DATEADD(DAY, 20, GETDATE()), 15, 1,@ProjectId, @StatusId),
(43,'Select new software solution', GETDATE(), DATEADD(DAY, 35, GETDATE()), 10, 1,@ProjectId, @StatusId),
(44,'Migrate data to new system', GETDATE(), DATEADD(DAY, 50, GETDATE()), 15, 1,@ProjectId, @StatusId),
(45,'Train finance team on new system', GETDATE(), DATEADD(DAY, 65, GETDATE()), 10, 1,@ProjectId, @StatusId),
(46,'Go live with new reporting system', GETDATE(), DATEADD(DAY, 80, GETDATE()), 15, 1,@ProjectId, @StatusId);
GO

DECLARE @ProjectId INT;
DECLARE @StatusId INT;
-- Tasks for "Cost Reduction Initiative"
SELECT @ProjectId = [ProjectId] FROM [Projects] WHERE [ProjectName] = 'Cost Reduction Initiative';
SELECT @StatusId = ProjectTaskStatusId FROM ProjectTaskStatuses WHERE StatusText = 'Not Started' AND ProjectId = @ProjectId;
INSERT INTO [ProjectTasks] (ProjectTaskId,Description, CreatedAt, DueDate, AssigneeId, CreatorId, ProjectId, CurrentStatusId) VALUES
(47,'Audit current expenses', GETDATE(), DATEADD(DAY, 12, GETDATE()), 10, 1,@ProjectId, @StatusId),
(48,'Identify cost-saving opportunities', GETDATE(), DATEADD(DAY, 24, GETDATE()), 15, 1,@ProjectId, @StatusId),
(49,'Negotiate with suppliers for better rates', GETDATE(), DATEADD(DAY, 36, GETDATE()), 10, 1,@ProjectId, @StatusId),
(50,'Implement energy-saving measures', GETDATE(), DATEADD(DAY, 48, GETDATE()), 15, 1,@ProjectId, @StatusId),
(51,'Review and optimize software subscriptions', GETDATE(), DATEADD(DAY, 60, GETDATE()), 10, 1,@ProjectId, @StatusId),
(52,'Train staff on cost awareness', GETDATE(), DATEADD(DAY, 72, GETDATE()), 15, 1,@ProjectId, @StatusId);
GO

SET IDENTITY_INSERT  ProjectTasks OFF;
