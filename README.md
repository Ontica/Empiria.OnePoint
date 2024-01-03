# Empiria OnePoint

[![Codacy Badge](https://app.codacy.com/project/badge/Grade/5b3ebddf0bce44188ad00877840a18f6)](https://www.codacy.com/gh/Ontica/Empiria.OnePoint/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=Ontica/Empiria.OnePoint&amp;utm_campaign=Badge_Grade) &nbsp; &nbsp; [![Maintainability](https://api.codeclimate.com/v1/badges/a66f2027efa4d2091661/maintainability)](https://codeclimate.com/github/Ontica/Empiria.OnePoint/maintainability)

[Empiria OnePoint](http://www.ontica.org/) is a suite of software components that allow governments
to provide services to people and organizations using a single point of contact. Also provides services
for integrated security and organizations management.

This repository contains the system's domain components, application services (uses cases),
a web API interface to interact with the backend, and connectors to hook other systems and components
to Empiria OnePoint.

This project is based on C# and .NET Framework and can be compiled using Visual Studio 2022 Community Edition.

As other Empiria products, this backend runs over [Empiria Framework](https://github.com/Ontica/Empiria.Core)
components and, as usual, needs some of the [Empiria Extensions](https://github.com/Ontica/Empiria.Extensions).

## Modules

Empiria OnePoint services are packaged into the following software modules:

1.  **Core**

    Domain layer that provides services to perform procedure and document filing tasks.

    Additionally offers services for payment orders issuance, record keeping of payment orders linked to each
    provided service, and connectors to integrate it with treasury systems. However, this module neither executes
    payments nor supplies an electronic payment infrastructure.

    Also contains components that describes and defines government's procedures, their data, rules and requirements.

    Empiria OnePoint has a Services Integrator, which is a pluggable infrastructure to connect Empiria OnePoint
    with external government's software systems and services that performs the actual services.

    Moreover, to control the workflow, it can be connected with Empiria Steps in a natural way or with third-party
    workflow management systems to adequately control each task's process that may involve one or many public
    dependencies.

2.  **Document Management**

    Infrastructure to securely manage documents and forms fulfilled by people, business and organizations.

    Provides electronic sign services to protect and sign documents.

3.  **Knowledge Base Management**

    Infrastructure used to build and manage a knowledge base about procedures and government services.

    Also contains components that provides help desk services connected to an issue tracking system.

4.  **Application Services**

    Application services layer with a general-purpose entry-level set of use-cases involved in
    Empiria OnePoint-based solutions.

5.  **Web API**

    Http/Json RESTful type web services interface used to communicate Empiria OnePoint-based solutions with
    front-end applications and third-party systems.

## Documentation

Folder [**docs**](https://github.com/Ontica/Empiria.OnePoint/tree/master/docs) contains a web site with
the full code documentation. It can be downloaded and installed in the web server of your preference.

There, **database.scripts.sql** file contains the full database script for SQL Server 2017, and it includes
the full set of tables, views, functions and stored procedures.

**database.structure.pdf** contains a general view map of the database.

**components.pdf** file presents a general view map of the system.

## License

This system is distributed by the GNU AFFERO GENERAL PUBLIC LICENSE.

Óntica always delivers **open source** information systems. We consider that this practice is specially
important in the case of public utility or government systems.

## Copyright

Copyright © 2017-2024. La Vía Óntica SC, Ontica LLC and colaborators.
