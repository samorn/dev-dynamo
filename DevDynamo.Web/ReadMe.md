### EF CLI

 

compare and find diff (code vs database).
Generates c# migration scripts.

 

Run this conde on the project that host the DBContext
```
> dotnet ef migrations add <name> -c <database context name>
> dotnet ef migrations remove // undo previous migration
```
#### sample:
```
> dotnet ef migrations add Initial
```

 

## EF Update database
Convert c# migration script => SQL script => Excecute.
Keep log in _EFMigrationHistory table
```
> dotnet ef database update -c <database context name>
```

 

### EF CLI Tool
```
>set DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=True
>dotnet tool install dotnet-ef -g
```

 

---

 

## REST APIs

 

#### Projects
```
GET /api/v1/projects/
```
```
GET /api/v1/projects/5
```

 

```
POST /api/vi/projects/
```

 

Reqeust payload:
```
{
    "name": "Demo 1",
    "workflow": "Default"
}
```
Response :
```
201 Created
```

 

```
400 Bad Request
{
    "error": "Workflow Default is not found"
}
```