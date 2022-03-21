 

 



<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a href="https://github.com/helibertoarias/muvids">
    <img src="images/logo.jpg" alt="Logo" width="80" height="80">
  </a>

  <h3 align="center"Muvids Web API</h3>
 


</div>
<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#contact">Contact</a></li>
   
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

This is a Web API for handling user and reservations in the Muvids hotel.

<p align="right">(<a href="#top">back to top</a>)</p>



### Built With

This Web API has build using ASP.NET Core Web API using .NET 6.0. This project presents an elegant architecture using CQRS combine with Mediator Pattern. 

You can use Postman to run the collection of requestd included into this repository. 

Some of the Nuget Packages used here are:

- Entity Framework Core for SQL Server
- Autommaper
- Serilog
- Fluent Validation
- MediatR
- Asp.Net Core Authentication
- Newtonsoft
- Swashbuckle
 
<p align="right">(<a href="#top">back to top</a>)</p>



<!-- GETTING STARTED -->
## Getting Started

Here some steps to run the application.


### Prerequisites

- Visual Studio 2020 
- SDK .NET 6
- SQL Server Express 2019 or other version will works.

### Installation

_To start using the application you should._

1. Clone the repo.
   ```sh
   git clone https://github.com/helibertoarias/Muvids.git
   ```
2. Open the solution MuvidsHotel.sln and build to restore the Nuget Packages.
3. Set the project Muvids.Web.API as startup project.
4. Open the file ./Muvids.Web.API/appsettings.json and update the user and password. This API use two databases, one called Muvids for the application and other called MuvidsIdentity for the users.
    ```json
    "ConnectionStrings": {
      "MuvidsHotelConnectionString": "Server=localhost;Database=MuvidsHotel;User Id=<user>;Password=<password>",
      "MuvidsIdentityConnectionString": "Server=localhost;Database=MuvidsIdentity;User Id=<user>;password=<password>" }
     ```
5. In VS 2020 open a new terminal and create the database for the application and the users.
    ```cmd
    Update-Database -Context MuvidsDbContext
    Update-Database -Context MuvidsIdentityDbContext
     ```
6. After this you can run the application. The Swagger page will show up
 [https://localhost:7098/swagger/index.html](https://localhost:7098/swagger/index.html)


<p align="right">(<a href="#top">back to top</a>)</p>



<!-- USAGE EXAMPLES -->
## Usage

To validate the Web API you need to import the file **Muvids Hotel Collection.postman_collection.json** into [POSTMAN](https://www.postman.com/downloads/) and run the test collection. 

Here some screenshots.

   - Select the imported file and hit the *Run* button.

   <br />
   <img src="images/postman-before-runs-collection.jpg" alt="Logo" width="900" >
  <br /> <br />


 
   - After a seconds you should the test result all passed.

   <br />
   <img src="images/postman-after-runs-collection.jpg" alt="Logo" width="900" >

 
<p align="right">(<a href="#top">back to top</a>)</p>


 
 

 



<!-- CONTACT -->
## Contact

Heliberto Arias - [@helibertoarias](https://twitter.com/helibertoarias) - helibertoarias@gmail.com

Project Link: [https://github.com/helibertoarias/MuvidsHotel](https://github.com/helibertoarias/muvidshotel)

<p align="right">(<a href="#top">back to top</a>)</p>


 
 