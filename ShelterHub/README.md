# Shelter Hub

This application was created to assist homeless and/or domestic violence shelters with keeping track of their clients' progress from the time they come in to the shelter to when they leave.

## Motivation

Most homeless and domestic violence shelters are funded through grants and private donations. Many of them struggle to keep the doors open, let alone provide services to their clients. However, most clients require in-depth counseling and step-work in order to become self-sufficent and not return to the shelter again and again after leaving. It is my hope that Shelter Hub will help shelters to more easily track the progression of clients, which in turn will open up more time for counseling and step-work, thereby increasing client self-sufficiency and cutting down on client return rates.

<!-- ![](src/components/images/homeScreen.png)
TODO: Put screen shot of home page here-->


## Tech/Framework Used

C# Programming Language
Entity Framework

## Features

- **Client Details** allows users to input client details into the database so that they can see a photo of the client and important details about the client, including an emergency contact, client phone number, and whether the client's intake is complete.
<!-- - **Client Progression** TODO: Update this section with app detailsis generated each time a user inputs a new allergic reaction. Researchers can use the database to track what percentage of users had to take medication for allergic reactions, the percentage of users who had to go to the ER for allergic reactions, the percentage of users who are formally diagnosed, etc. The data shown on the *Data Results* page are just a scratch on the surface of how the data collected through this project can be used. -->

## Installation

This project was created using Entity and Identity Authentication??? It was built in Visual Studio 2019.

### Installing the Project

Clone the source locally:
```
git clone https://github.com/jessicaayerst/Shelter-Hub.git
```

Then:
```
cd ShelterHub/
```

In the project directory, you can run:

```
start ShelterHub.sln
```
This will open the project in Visual Studio 2019

### Running the Project
Create a new database in SQL Server Object Explorer. Name it ShelterHub. Then open up Package Manager Console and run the following command:

```
PM> Update-Database
```

This will put seed data in the database so that when the app runs, there will already be clients, steps, and groups to view/edit/delete.

Now, run the app using the PLAY button in Visual Studio Code. This will open the browser to the application. Log in using the username: admin@admin.com and password: Admin123! .


### Installing/Using Auth0
 <!-- TODO: Not sure if I need to added anything about installing IDentity or if the user will automatically be able to use it. ???Since the app uses Auth0 as the login framework, you will need to create an Auth0 account by clicking [here](https://auth0.com/signup) and then do this: -->


## How to Use?

1. Once you are logged in, go to the Clients page, by clicking "Clients". Here, you will see a list of current clients' first and last names, room number, an option to "Create a New Client" and options to Edit, Delete, and View Details of each client. Users can also SEARCH for a client by name in the search bar and view the list in alphabetical order by last name or chronological order by intake date.
<!-- TODO: Place GIF here of user clicking on client page -->
2. Go to the Client Details page by clicking on "Details" next to the name of the client you want to view. On the Details page, you will see a photo of the client, all of the client's information, a list of groups and steps in which the client is enrolled. Users can also add more steps and groups to the client, edit a step to include a completion date, or remove a group from the client.
<!-- TODO: Place GIF here![Alt Text](src/components/images/secondGif.gif) -->
3. The "Groups" page will show you a list of Groups available to the clients at the shelter. Users can Create a new group or Edit/Delete/ViewDetails of a group. Users can also add a group to a client from the Groups page.
<!-- TODO: Place GIF here![Alt Text](src/components/images/thirdGif.gif) -->
4. The "Steps" page will show a list of Steps that a client will progress through during their time at the shelter. Users can Create a new step or Edit/Delete/View Details of steps that have already been created. Users can also add a step to a client from the Steps page.
<!-- TODO: Place GIF here![Alt Text](src/components/images/lastGif.gif) -->

## How to Contribute?

1. Clone repo and create a new branch: $ git checkout https://github.com/jessicaayerst/Shelter-Hub.git -b name_for_new_branch.
2. Make changes and test
3. Submit Pull Request with comprehensive description of changes.


