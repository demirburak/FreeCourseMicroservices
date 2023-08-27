# FreeCourseMicroservices
Microservis eğitimi // Microservice example

1. Frontend -> ASP.NET Core MVC
2. Order service -> Orm is EntityFramework, CQRS pattern, MSSQL Db
3. Basket service -> REDIS DB
4. Dicount service -> Orm is Dapper, Postgresql DB
5. Catalog/Course service -> Mongo Db
6. PictureStock service -> saving pictures in local
7. Fakepayment service
8. Gateway service -> Ocelot
9. Identity service -> IdentityServer4, MSSQL Db
10. Event bus -> RabbitMQ

If you want to run the project on vs, you can run the docker compose file in the MicroserviceDatabase folder. This docker compose will only run the required db containers.
Otherwise you want to run this project without code, you can run docker compose file in main folder. This docker compose will run all db and api containers.
