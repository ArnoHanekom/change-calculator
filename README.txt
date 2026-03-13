Welcome to the Change-Calculator App.

## Assumptions
As it was not specified, this is done as a proof-of-concept and not to be production ready.
This means that it is written without all the bells and whistles one would ideally have in 
a production ready application such as:
 Backend
    - xUnit tests (with WebApplicationFactory and database setup and seeding)
    - Proper error/exception handling
    - Logging
    - Proper entiy framework configuration and definitions
    - Proper middleware separation (to make the Program.cs less congested and to have middleware separated and easily maintainable)
    - Caching where needed (in this case probably only on the transaction history)
    - Potentially the need for Docker and Docker compose to have it be easily deployable and contained
 Frontend
    - Error/exception handling
    - Unit/component tests
    - Api health checking OR proper api call handling to provide the user clear, understandable notifications should the api be unreachable
    - Proper styling and layout

It is also assumed that currencies added on the fly are valid to whoever uses the app
and that no exchange information is required or currency code validity is required.
It is also assumed that no currency symbol is needed and that the user will understand
the change breakdown correctly.
It is also assumed that the history does not need to be filterable or sortable.
It is assumed that the history should display the results ordered by date descending with the newest transaction at the top and oldest at the bottom.
It is assumed that the frontend will need more time to review things like putting the detailed transaction view, on the history page, in a modal pop-up
or where to adjust the layout for cosmetic reasons.

It is assumed that any developer wanting to run this knows and understands what is
prerequisites of both the .Net Core, the Angular and the Angular Material versions used.

## SETUP 
After pulling or downloading the repository from GitHub, the backend code (solution and projects)
can be found within the Backend folder. Inside this folder, the API's root folder called "ChangeCalculatorApi" can be found.
Running "dotnet run --project Presentation" inside the API's root folder, should start the API and make it available to the frontend.

The database is a sqlite version with the EnsureCreated property set to true upon application startup.

The frontend can be found within the Frontend folder. Inside this folder is the Angular application's root folder called "change-calculator-app".
Running "ng serve" in the Angular application's root folder, should start the frontend and make it available to browse.

It is encouraged to review the startup ports of both the backend and frontend apps.
Should it be different from port "5188" for the backend and "4200" for the frontend, please
ensure to update the following:
 - in the backend, update the CORS policy to specify and allow the new frontend port.
 - in the frontend, update the environment.ts file to the correct port for the backend API.

** DISCLAIMER:
As this is an assessment and submitted to be used for review and to determine skill only 
and should not be distributed or shared for any other purposes, I would finally just like to add:
    - This week has been personally very difficult for me and although it is no excuse, our personal life
        unfortunately does have a great impact on clarity, focus and performance.
    - I know this is not completed to the extend that is required.
    - My sincere appologies for the unexpected, unavoidable delay in submitting my assessment.