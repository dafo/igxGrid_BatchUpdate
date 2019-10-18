# igxGrid_BatchUpdate

## Running the Web API project
1. Open `CityService.sln` in Visual Studio
2. Build the solution to restore the NuGet packages
3. Run the Package Manager Console and execute the following commands to generate the data:
 ```
> Add-Migration Initial
> Update-Database
```
4. Run the project (F5)

## Running the Angular project
1. Run `npm install` inside the "Batch Update" folder
2. Run `npm run start`

**Note**: The Web API project should be running when you start the Angular project