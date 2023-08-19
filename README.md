# Book-Tracking
This application allows users to track the book information and update the tracking status.

1. First, the user has to add the book that they would lile to track. Initially the added book will be in Started status.
2. The users can update the book's status as : In Progress(if the user is still reading the book), Completed (if the user has completed reading the book), Got Bored and Quit(if the user gets bored while reading and quits).
3. This application has the functionality of adding categories and category types. However, this information should only be visible to the admin. Since Admin page is out of scope, we have set the visibility for categories and category types to the users.
4. Users can also add, edit, delete and view details of categories.
5. Users can also add, edit, delete and view details of category types.

# Impediments
1. Using scaffolding in creation of controllers and views did not work initially as some of the packages didnt work. However, after thorough research, I was able to scaffold the controllers, controller methods and views.
2. I wanted to implement dependency injection by segregating the database access logic into the manager classes, but i was unable to inject db context. However, I have left the Manager classes in the project itself so that I can work on it later in the future.
3. Scaffolding views that the editor created for SelectListItems didnt work for me as it was not passing the value of the dropdown list. However, I have implemented a workaround for that.
4. I followed the instructions provided during the class to implement the database and entities. I was able to create the migration scripts and successfully update the database with the created migration scripts.

# Screenshots of the UI

## Landing Page
<img width="1440" alt="image" src="https://github.com/rubyiscoding/Book-Tracking/assets/74127503/001b2316-5168-4c11-8d61-2f0669199582">
