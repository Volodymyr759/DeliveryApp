The company's internal website for tracking postal items in the information systems of delivery services.
Departments (sales, supply, warehouse, etc.) can authenticate in the system as users of the internal service (sales@company.com, warehouse@company.com etc.) to receive information about the shipments in one place and in a unified format.
Contains base backend with standard design.

Purpose of the application:
1. Storage of shipments information in own server database
- used ORM - MS Sql Server;
- expansion posibilities: adding I...Repository interfaces and corresponding implementations;
2. Update the current status and location of shipments by:
- API - integration with post operators services;
- Html - query to search pages of postal operators;
- expansion posibilities: creating new post operators in the DeliveryApp by adding implementations for interface ISearchAgent;

Planned user roles and uses:
Sales / Supply / Warehouse. Support and tracking online shipments status.

Technical description:
- Asp.Net (NET Framework 4.7.2);
- Tests: MSTest (Moq);
- Patterns: MVC, DI (Unity), Factory method;
- Validation: FluentValidation, ComponentModel.DataAnnotations;
