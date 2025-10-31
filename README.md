# Company Rate Api

Api for rating companies.

![GitHub top language](https://img.shields.io/github/languages/top/cccaaannn/CompanyRatingApi?color=blue&style=flat-square) ![GitHub repo size](https://img.shields.io/github/repo-size/cccaaannn/CompanyRatingApi?color=orange&style=flat-square) [![GitHub](https://img.shields.io/github/license/cccaaannn/CompanyRatingApi?color=green&style=flat-square)](https://github.com/cccaaannn/CompanyRatingApi/blob/master/LICENSE)

---

## Development

### Setup
1. Install dependencies
    ```shell
    dotnet restore
    ```
2. Create and update `appsettings.Development.json`
    ```shell
   cp appsettings.json appsettings.Development.json
    ```
3. Apply migrations (application applies automatically on startup)
    ```shell
    dotnet ef database update
    ```
4. Run
    ```shell
    dotnet run
    ```

### Migrations
- Add migration
    ```shell
    dotnet ef migrations add <migration_name>
    ```
