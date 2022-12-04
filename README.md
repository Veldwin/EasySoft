# EsaySave Project, by PROSOFT

## Introduction

Easysave is a back up folder software. It's a school project created by a team of 4 students - Group 4.

## Built with

* Visual Studio 2022 (enterprise version)
* Visual Pardigm (for diagram)
* Git Repos

## Version 1.0

The specifications of the first version of the software are as follows : 

The software is a Console application using .Net Core. It must allow the creation of up to 5 backup jobs.

### A backup job is defined by :

An appellation
A source directory
A target directory
A mirror directory
Full save
Differential save
the software is understandable by english and french person
The user may request the execution of one of the backup jobs or the sequential execution of the jobs. The directories (sources and targets) can be on local, external or network drives. All the elements of the source directory are concerned by the backup.

### Daily Log File :

The software write in real time in a daily log file (Xml or Json) the history of the actions of the backup jobs. The minimum expected information is :

Timestamp  
Naming the backup job
Full address of the Source file
Full address of the destination file
File Size 
File transfer time in ms    

## Version 2.0

The version 2.0 has been released with the following improvements : 

### Graphic Interface

Abandoning Console mode. The application is now developed in WPF under .Net Core 6.0.

### Unlimited number of jobs

The number of backup jobs is now unlimited. 

### Evolution of the Daily Log file

The daily log file can be generate with a json or xml extension at the user's choice.

### Business software

If the presence of business software is detected, the software must prohibit the launch of a backup job. In the case of sequential jobs, the software must complete the current job and stop before launching the next job. The user will be able to define the business software in the general settings of the software. (Note: the calculator application can substitute the business software during demonstrations). 

## Clone our project

Getting Started To get a local copy up and running follow these simple example steps. But before anything else you should be sure to have Visual Studio 2022 in order to run the program.

Once you will have set up your computer You will have to clone this project.

With Git:

`git clone https://github.com/Veldwin/EasySoft.git`
Then refer to the usage section and you will be ready to go !

## EasySave Documentation

* Version 1.0 : [Documentation User](V1/UserDocumentation.md)
* Version 1.0 : [User's Manual](V1/Readme.md)
* Version 2.0 : [Documentation User](V2/EasySave%20V2.0/UserDocumentation.md)
* Version 2.0 : [User's Manual](V2/EasySave%20V2.0/UsersManual.md)

## The versions of EasySave

Version	Available
| Version | Available          |
| ------- | ------------------ |
|   [1.0](V1/)   | :white_check_mark: |
|   [1.1](https://github.com/Veldwin/EasySoft/tree/V1_1/V1)   | :white_check_mark: |
|   [2.0](V2/EasySave%20V2.0)   | :white_check_mark: |
|   [3.0](V3/EasySave%20V3.0)   | ❎ |

## Authors

*RAAD Camille
*GIRAUDEAU Valentin
*TRENY Edwin
*WAUTERS Mathis
