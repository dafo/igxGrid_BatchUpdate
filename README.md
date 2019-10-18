# igxGrid_BatchUpdate

## Running the WebAPI project
1. Open `CityService.sln` in Visual Studio
2. Build the solution to restore the NuGet packages
3. Run the Package Manager Console and execute the following commands to generate the data:
 ```
> Add-Migration Initial
> Update-Database
```
4. Run the project (F5)