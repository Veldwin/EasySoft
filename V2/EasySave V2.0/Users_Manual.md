# EasySoft v2.0 By PROSOFT EN

## User Manual / Manuel d'Utilisation V2.0

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
  4) Add the path of your folder to save by selected on file explorer.
  5) Add the path to your destination folder by selected on file explorer.
  6) For differential backup, you need to put the path to your folder where a full backup was made.
  7) Click on "add backup" 

### Load backup job and execute
To start a backup job, you must select the name of the backup you want to do. 
If you want to run several backup, then you can choose the "All" button for selecting every backup available.
Then you click on "launch Backup" the execution will start. If the backup works fine, you will get a validation message.

### Delete a backup

For deleting a backup, you need to select the backup to erase then use the "Delete Backup" button.

### Log file

The log file is saved here: `\EasySoft\bin`. 

### State file / Fichier State

The state File is saved here: `\EasySoft\bin\Debug\net6.0\State\state.json`

## Authors

### Group 4

- RAAD Camille
- GIRAUDEAU Valentin
- TRENY Edwin
- WAUTERS Mathis

# EasySoft v2.0 By PROSOFT FR

## User Manual / Manuel d'Utilisation V2.0

### Fonctionalités

- Interface Graphique
- Sauvegarde complète
- Sauvegarde différentielle
- Sauvegarde séquentielle
- Suppression de sauvegarde
- Interface multilangue
- Prévention des sauvegardes si application métier tourne
- Choix du format du fichiers de journal

### Installer EasySoft

Pour installer le logiciel, vous pouvez cloner notre dépôt sur votre IDE : `$ git clone https://github.com/Veldwin/EasySoft.git`

### Start the application / Lancer l'Application

Au lancement de l'application, vous serez accueilli par notre interface graphique. Entrez les informations dans les champs correspondants pour que notre application s’occupe de vos sauvegardes.

### Add a backup job / Créer une sauvegarde

Pour effectuer une sauvegarde, vous devez d'abord créer une tâche de sauvegarde.

  1) Choisissez l'extension du fichier journal
  2) Choisissez une sauvegarde complète ou différentielle (une sauvegarde complète copiera tous les fichiers / dossiers tandis qu'une sauvegarde différentielle se compare à une sauvegarde complète et ne copie que les fichiers modifiés), ensuite choisissez le format de vos fichiers de log journaliers.
  3) Donnez un nom à votre sauvegarde.
  4) Ajoutez le chemin de votre dossier à sauvegarder en choississant dans l'explorateur de fichier.
  5) Ajoutez le chemin de votre dossier de destination en choississant dans l'explorateur de fichier.
  6) Pour une sauvegarde différentielle, vous devez mettre le chemin vers votre dossier où une sauvegarde complète a été faite.
  7) Cliquez sur ajouter une sauvegarde

### Charger une sauvegarde et l'éxecuter

Pour démarrer une tâche de sauvegarde, vous devez entrer le nom de la/les sauvegarde(s) que vous voulez faire.
Si vous voulez effectué plusieurs sauvegardes, vous pouvez choisir le bouton "Tout" pour selectionner toutes les sauvegardes disponibles.
Après avoir cliquer sur "lancer la sauvegarde" l'exécution commencera. Si la sauvegarde fonctionne bien, vous obtiendrez un message de validation.

### Supprimer une sauvegarde

Pour supprimer une sauvegarde, vous devez sélectionner la sauvegarde à effacer puis utiliser le bouton "Supprimer la sauvegarde".

### Fichier Log

Le fichier log est enregistré ici : `\EasySoft\bin`.

### Fichier State

Le fichier State est enregistré ici : `\EasySoft\bin\Debug\net6.0\State\state.json`

## Auteurs

### Groupe 4

- RAAD Camille
- GIRAUDEAU Valentin
- TRENY Edwin
- WAUTERS Mathis
