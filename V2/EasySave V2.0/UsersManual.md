# EasySoft v2.0 By PROSOFT EN

## User Manual

### Features 

- Graphic interface
- Full Backup
- Differential Backup
- Sequential Backup
- Backup deletion
- Plurilingual interface
- Backup prevention if business application is running
- Multiple log file formats

### Install EasySoft

To install the software, you can clone our repository on your IDE : `$ git clone https://github.com/Veldwin/EasySoft.git`

### Start the application

Upon launching the application, you will be greeted by our GUI. From there, enter the information in the corresponding fields so that our application takes care of your backups.

### Add a backup job

To perform a backup, you must first create a backup job.

  1) Choose your Log format file
  2) Choose a full or a differential Backup (A full backup will copy every files / folder while a differential one compares itself to a full and copy only modified files),then choose the format of your daily log file.
  3) Give your backup a name.
  4) Add the path of your folder to save by selected a folder in file explorer.
  5) Add the path to your destination folder by selected a folder in file explorer.
  6) For differential backup, you need to put the path to your folder where a full backup was made.
  7) Click on "add backup" 

### Load backup job and execute

To start a backup job, choose the name of the backup(s) you want to make.
If you want to run all backups, you can choose the "All" button to select all available backups.
After you click on "Start Backup" the execution will start. If the backup works well, you will get a validation message.

### Add a business application in blacklist processes

To prevent a backup from being made while a business application is running, you must add the name of the process to the blacklist. To do this, you must click on the "Blacklist App" button and enter the name of the process.

### Delete a backup

For deleting a backup, you need to select the backup to erase then use the "Delete Backup" button.

### Log file

The log file is saved here: `\EasySoft\bin`. 

### State file

The state File is saved here: `\EasySoft\bin\Debug\net6.0\State\state.json`

## Authors

### Group 4

- RAAD Camille
- GIRAUDEAU Valentin
- TRENY Edwin
- WAUTERS Mathis

# EasySoft v2.0 By PROSOFT FR

## Manuel d'Utilisation

### Fonctionalit??s

- Interface Graphique
- Sauvegarde compl??te
- Sauvegarde diff??rentielle
- Sauvegarde s??quentielle
- Suppression de sauvegarde
- Interface multilangue
- Pr??vention des sauvegardes si application m??tier tourne
- Choix du format du fichiers de journal

### Installer EasySoft

Pour installer le logiciel, vous pouvez cloner notre d??p??t sur votre IDE : `$ git clone https://github.com/Veldwin/EasySoft.git`

### Lancer l'Application

Au lancement de l'application, vous serez accueilli par notre interface graphique. Entrez les informations dans les champs correspondants pour que notre application s???occupe de vos sauvegardes.

### Cr??er une sauvegarde

Pour effectuer une sauvegarde, vous devez d'abord cr??er une t??che de sauvegarde.

  1) Choisissez l'extension du fichier journal
  2) Choisissez une sauvegarde compl??te ou diff??rentielle (une sauvegarde compl??te copiera tous les fichiers / dossiers tandis qu'une sauvegarde diff??rentielle se compare ?? une sauvegarde compl??te et ne copie que les fichiers modifi??s), ensuite choisissez le format de vos fichiers de log journaliers.
  3) Donnez un nom ?? votre sauvegarde.
  4) Ajoutez le chemin de votre dossier ?? sauvegarder en choississant le dossier dans l'explorateur de fichier.
  5) Ajoutez le chemin de votre dossier de destination en choississant le dossier dans l'explorateur de fichier.
  6) Pour une sauvegarde diff??rentielle, vous devez mettre le chemin vers votre dossier o?? une sauvegarde compl??te a ??t?? faite.
  7) Cliquez sur "ajouter une sauvegarde"

### Charger une sauvegarde et l'??xecuter

Pour d??marrer une t??che de sauvegarde, choisir le nom de la ou les sauvegardes que vous souhaitez faire.
Si vous voulez effectu?? toutes les sauvegardes, vous pouvez choisir le bouton "Tout" pour selectionner toutes les sauvegardes disponibles.
Apr??s avoir cliquer sur "lancer la sauvegarde" l'ex??cution commencera. Si la sauvegarde fonctionne bien, vous obtiendrez un message de validation.

### Supprimer une sauvegarde

Pour supprimer une sauvegarde, vous devez s??lectionner la sauvegarde ?? effacer puis utiliser le bouton "Supprimer la sauvegarde".

### Ajouter une application m??tier dans la liste noire des processus

Pour ??viter qu'une sauvegarde soit effectu??e alors qu'une application m??tier est en cours d'ex??cution, vous devez ajouter le nom du processus ?? la liste noire. Pour ce faire, vous devez cliquer sur le bouton "Blacklist App" et entrer le nom du processus.

### Fichier Log

Le fichier log est enregistr?? ici : `\EasySoft\bin`.

### Fichier State

Le fichier State est enregistr?? ici : `\EasySoft\bin\Debug\net6.0\State\state.json`

## Auteurs

### Groupe 4

- RAAD Camille
- GIRAUDEAU Valentin
- TRENY Edwin
- WAUTERS Mathis
