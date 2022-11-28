# EasySoft v1.0 By PROSOFT

Easysave is a back up folder software. It's a school project created by a team of 4 students.

Easysave est un logiciel de sauvegarde de dossiers. C'est un projet scolaire créé par une équipe de 4 étudiants.

## User Documentation / Documentation Utilisateur

### Tree structure / Arborescence

Here is the folder structure of the project :

Voici l'arborescence des dossiers du projet :

```bash
├───bin
│   └───DebugDailyLogs.json
│   ├───Debug
│   │   └───net6.0
│   │       ├───State
│   │       │   └───state.json
│   │       └───Works
│   │       │   └───backupList.json
│   └───Release
│       └───net6.0
├───controler
│   └───controller.cs
├───model
│   └───model.cs
├───obj
│   ├───Debug
│   │   └───net6.0
│   │       ├───ref
│   │       └───refint
│   └───Release
│       └───net6.0
│           ├───ref
│           └───refint
└───view
    └───view.cs
```

### Minimum characteristics / Caractéristiques minimales

To run EasySoft, you need at least :
- Microsoft Visual Studio 2022
- Windows 10 or later
- 8 Gb of ram
- 1Gb of storage

Pour executer le logiciel EasySoft, il est necessaire d'avoir au minimum :
- Microsoft Visual Studio 2022
- Windows 10 ou ulterieur
- 8 Gb de ram
- 1Gb de stockage

### Next Versions / Prochaines Version

A version 2 of the software is planned in the next few days.
This version will have a new interface with more features.

Une version 2 du logiciel est prévue dans les prochains jours.
Cette version possédera une nouvelle interface avec plus de fonctionalités

## User Manual / Manuel d'Utilisation V1.0

### Features / Fonctionalités

### Install EasySoft / Installer EasySoft

To install the software, you can clone our repository on you're IDE : `https://github.com/Veldwin/EasySoft.git`

Pour installer le logiciel, vous pouvez cloner notre dépôt sur votre IDE : `https://github.com/Veldwin/EasySoft.git`

### Start the application / Lancer l'Application

Once you have run our programme, with the help of our Getting Started solution you will have multiple choices.

Une fois que vous aurez exécuté notre programme, avec l'aide de notre solution Getting Started, vous aurez plusieurs choix.

### Add a backup job / Créer une sauvegarde

To perform a backup, you must first create a backup job.

  1) Choose a full or a differential Backup (A full backup will copy every files / folder while a differential one compares itself to a full and copy only modified files).
  2) Give your backup a name.
  3) Add the path of your folder to save.
  4) Put the path to your destination folder.
  5) For differential backup, you need to put the path to your folder where a full backup was made.

Pour effectuer une sauvegarde, vous devez d'abord créer une tâche de sauvegarde.

  1) Choisissez une sauvegarde complète ou différentielle (une sauvegarde complète copiera tous les fichiers / dossiers tandis qu'une sauvegarde différentielle se compare à une sauvegarde complète et ne copie que les fichiers modifiés).
  2) Donnez un nom à votre sauvegarde.
  3) Ajoutez le chemin de votre dossier à sauvegarder.
  4) Mettez le chemin de votre dossier de destination.
  5) Pour une sauvegarde différentielle, vous devez mettre le chemin vers votre dossier où une sauvegarde complète a été faite.

### Load backup job and execute / Charger une sauvegarde et l'éxecuter

To start a backup job, you must enter the name of the backup you want to do.
After filling in the name of the backup, the execution will start. If the backup works fine, you will get a validation message.

Pour démarrer une tâche de sauvegarde, vous devez entrer le nom de la sauvegarde que vous voulez faire.
Après avoir rempli le nom de la sauvegarde, l'exécution commencera. Si la sauvegarde fonctionne bien, vous obtiendrez un message de validation.

### Log file / Fichier Log

The log file is saved here: `\EasySoft\bin`
Le fichier log est enregistré ici : `\EasySoft\bin`

### State file / Fichier State

The state File is saved here: `\EasySoft\bin\Debug\net6.0\State\state.json`
Le fichier State est enregistré ici : `\EasySoft\bin\Debug\net6.0\State\state.json`
