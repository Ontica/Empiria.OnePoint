# About Empiria OnePoint

[Empiria OnePoint](http://www.ontica.org/) is a suite of software components that allow governments
to provide services to people and organizations using a single point of contact.

This repository contains the system's domain components, application services (uses cases),
a web API interface to interact with the backend, and connectors to hook other systems and components
to Empiria OnePoint.

The project is based on C# and .NET Framework.

As other Empiria products, this backend runs over [Empiria Framework](https://github.com/Ontica/Empiria.Core)
components and, as usual, needs some of the [Empiria Extensions](https://github.com/Ontica/Empiria.Extensions).

The project can be compiled using Visual Studio 2017 Community Edition.

# Modules

Empiria OnePoint services are packaged into the following software modules:

1. [**Filing Services**](https://github.com/Ontica/Empiria.OnePoint/tree/master/OnePoint.Filing) 
   
   Domain layer that provides services to perform procedure and document filing tasks.
   
   Manages payment orders and has treasury systems connection interfaces.

   Also contains components that describes and defines government's procedures, their data, rules and requirements.

   Additionally offers services for payment orders issuance, record keeping of payment orders linked to each provided service, and connectors to integrate treasury systems. However, this module neither executes payments nor supplies an electronic payment infrastructure.

   Moreover, provides electronic sign services to protect and sign documents, supporting an infrastructure to manage documents and forms fulfilled by people and organizations.


2. **Services Integrator**
   Infrastructure to connect Empiria OnePoint with specific government's software systems and services which manage and provide the actual services.


3. **Knowledge Base Management**  
   Infrastructure used to build and manage a knowledge base about procedures and government services.

   Also contains components that provides help desk services connected to an issue tracking system.


4. **Application Services**  
   Application services layer with a general-purpose entry-level set of use-cases involved in Empiria OnePoint-based solutions.


5. **Web API**  
   Http/Json RESTful type web services interface used to communicate Empiria OnePoint-based solutions with front-end applications and third-party systems.


# Documentation

Folder [**docs**](https://github.com/Ontica/Empiria.OnePoint/tree/master/docs) contains a web site with the full code documentation. It can be downloaded and installed in the web server of your preference.

There, **database.scripts.sql** file contains the full database script for SQL Server 2017, and it includes the full set of tables, views, functions and stored procedures.

**database.structure.pdf** contains a general view map of the database.

**components.pdf** file presents a general view map of the system.

# License

This system is distributed by the GNU AFFERO GENERAL PUBLIC LICENSE.

Óntica always delivers **open source** information systems. We consider that this practice is specially
important in the case of public utility or government systems.

# Copyright

Copyright © 2017-2018. La Vía Óntica SC, Ontica LLC and colaborators.
