# API ASP.NET Core

## Description

Ce projet est une API RESTful développée avec ASP.NET Core. L'API fournit des endpoints pour gérer les opérations CRUD (Create, Read, Update, Delete) pour diverses entités telles que les `Incidents`, `Contacts`, etc.
 

## Installation de SQL Server

1. **Télécharger SQL Server** :
   - Visitez [la page de téléchargement de SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads).
   - Choisissez la version qui vous convient (par exemple, SQL Server Express) et téléchargez le fichier d'installation.

2. **Installer SQL Server Management Studio (SSMS)** :
   - Téléchargez SSMS depuis [cette page](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms).
   - Exécutez le fichier d'installation et suivez les instructions à l'écran.

3. **Configurer SQL Server** :
   - Assurez-vous que SQL Server et SSMS sont correctement installés et configurés.

## Configuration de l'application

1. **Cloner le dépôt** :

   ```bash
   git clone https://gitlab.com/projet-ticketing-b2b-webapi/g2tdb.git
## Modifier la chaîne de connexion :

 **Ouvrez le fichier appsettings.json** :
Modifiez la chaîne de connexion dans la section ConnectionStrings pour qu'elle corresponde à votre serveur SQL Server. Par exemple :

"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=YourDatabaseName;User Id=your-username;Password=your-password;"
}
## Générer la base de données :

Ouvrez Visual Studio.

Accédez à Outils -> Gestionnaire de package NuGet -> Console du Gestionnaire de package.

Exécutez la commande suivante
   ```bash
  update-database
