# how to run

- first create the 'project-network'

```
docker network create project-network
```

- go to images directory and load all the images or create the images yourself


dotnet aspnet-codegenerator controller -name DeliveryPointController -async -api -m DeliveryPointModel -dc AppDbContext -outDir Controllers