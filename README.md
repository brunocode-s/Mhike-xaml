MHikePrototype

MHikePrototype is a cross-platform hiking tracker app built using .NET MAUI and SQLite. It allows users to log hikes, track recent hikes, view hike statistics, and navigate through settings.

Features

Dashboard/MainPage

Hero section welcoming the user.

Quick stats showing total hikes and total distance.

Quick actions: Add Hike, View Hikes, Settings.

Recent Hikes collection displaying the latest 5 hikes.

AddHikePage

Form to add a new hike:

Hike Name

Location

Date

Parking

Length (km)

Difficulty (Easy / Moderate / Hard)

Description

Save button stores hike in SQLite database.

HikeListPage

Displays all logged hikes.

Navigation from MainPage.

SettingsPage

Placeholder for user settings (navigation implemented).

Data Storage

Uses SQLite to store hikes and observations.

Hike model contains details like Name, Location, Date, Length, Difficulty, Terrain, EstimatedTime.

Observation model stores notes for each hike.

Project Structure
MHikePrototype/
│
├── App.xaml # App-level resources and styles
├── App.xaml.cs # App initialization, sets MainPage to AppShell
│
├── MainPage.xaml # Dashboard UI
├── MainPage.xaml.cs # Dashboard logic, navigation, load recent hikes
│
├── Pages/
│ ├── AddHikePage.xaml # Add Hike form UI
│ ├── AddHikePage.xaml.cs # Form submission, validation, database insertion
│ ├── HikeListPage.xaml # List of all hikes
│ ├── HikeListPage.xaml.cs # Loads hikes from SQLite
│ └── SettingsPage.xaml # Placeholder settings UI
│
├── Models/
│ ├── Hike.cs # Hike data model
│ └── Observation.cs # Observation data model
│
├── Data/
│ └── DbService.cs # SQLite database service: CRUD operations
│
└── Resources/
└── Styles/ # Colors and style resources

Setup Instructions

Prerequisites

.NET 9 SDK

Visual Studio 2022/2023 with MAUI workload

Mac (for Mac Catalyst / iOS builds)

Clone the repository

git clone
cd MHikePrototype

Build and run

dotnet build -t:Run -f net9.0-maccatalyst /p:RuntimeIdentifier=maccatalyst-x64
