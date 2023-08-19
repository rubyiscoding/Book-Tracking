# Book-Tracking
This application allows users to track the book information and update the tracking status.

1. First, the user has to add the book that they would like to track. Initially the added book will be in Started status.
2. The users can update the book's status as : In Progress(if the user is still reading the book), Completed (if the user has completed reading the book), Got Bored and Quit(if the user gets bored while reading and quits).
3. This application has the functionality of adding categories and category types. However, this information should only be visible to the admin. Since Admin page is out of scope, we have set the visibility for categories and category types to the users.
4. Users can also add, edit, delete and view details of Books.
5. Users can also add, edit, delete and view details of Categories.
6. Users can also add, edit, delete and view details of Category Types.

# Impediments
1. Using scaffolding in creation of controllers and views did not work initially as some of the packages didnt work. However, after thorough research, I was able to scaffold the controllers, controller methods and views.
2. I wanted to implement dependency injection by segregating the database access logic into the manager classes, but i was unable to inject db context. However, I have left the Manager classes in the project itself so that I can work on it later in the future.
3. Scaffolding views that the editor created for SelectListItems didnt work for me as it was not passing the value of the dropdown list. However, I have implemented a workaround for that.
4. I followed the instructions provided during the class to implement the database and entities. I was able to create the migration scripts and successfully update the database with the created migration scripts.
5. fontawesome icons sometimes doesnt work on my system. I am not sure what is causing that. Sometimes it gets displayed and sometimes it doesnt. It could be because of caches.
6. I did not get a chance to implement the unit test for the controllers. (ran out of time)

# Screenshots of Books UI

## Landing Page
<img width="1440" alt="image" src="https://github.com/rubyiscoding/Book-Tracking/assets/74127503/001b2316-5168-4c11-8d61-2f0669199582">

## Book's Index Page
<img width="1435" alt="image" src="https://github.com/rubyiscoding/Book-Tracking/assets/74127503/41225d01-b70e-4070-8f8f-024483022cdc">

## Book's Create Page
![image](https://github.com/rubyiscoding/Book-Tracking/assets/74127503/17506d93-b869-48a5-a7dd-d7ec34fe0ba2)

## Book's Edit Page 
<img width="1435" alt="image" src="https://github.com/rubyiscoding/Book-Tracking/assets/74127503/2d694397-950d-4f26-aaaf-7784a50b9780">

## Book's Details Page
<img width="1435" alt="image" src="https://github.com/rubyiscoding/Book-Tracking/assets/74127503/41fa82b2-f00a-4da4-99c3-41f7a698cc25">

## Book's Delete Page
<img width="1435" alt="image" src="https://github.com/rubyiscoding/Book-Tracking/assets/74127503/4298c85a-6c34-49d7-b677-4065894f5e30">

# Screenshots of Book Categories UI

## Book Category's Index Page
<img width="1435" alt="image" src="https://github.com/rubyiscoding/Book-Tracking/assets/74127503/27cd9e94-76a4-4e3e-847a-a7d6341cfc4d">

## Book Category's Create Page
<img width="1435" alt="image" src="https://github.com/rubyiscoding/Book-Tracking/assets/74127503/66b24c5f-ad74-4530-8ea8-e9ea43d57fa5">

## Book Category's Edit Page
<img width="1435" alt="image" src="https://github.com/rubyiscoding/Book-Tracking/assets/74127503/8cb266f2-17a3-4c45-9739-a1592414a369">

## Book Category's Details Page
<img width="1435" alt="image" src="https://github.com/rubyiscoding/Book-Tracking/assets/74127503/54096095-477c-4caa-b020-3659bb20554c">

## Book Category's Delete Page
<img width="1435" alt="image" src="https://github.com/rubyiscoding/Book-Tracking/assets/74127503/5efda78d-303c-4a9e-ad1b-ed10ef22af5b">

# Screenshots of Book Category Types UI

## Book Category Types Index Page
<img width="1435" alt="image" src="https://github.com/rubyiscoding/Book-Tracking/assets/74127503/fa6b919c-a1fd-4a62-b4a6-9ea6882f6807">

## Book Category Types Create Page
<img width="1435" alt="image" src="https://github.com/rubyiscoding/Book-Tracking/assets/74127503/401e4b36-c518-4a74-b74b-7f6453ed2ea7">

## Book Category Types Edit Page
<img width="1435" alt="image" src="https://github.com/rubyiscoding/Book-Tracking/assets/74127503/5f75913e-91a4-4d52-85ce-c97179085891">

## Book Category Types Details Page
<img width="1435" alt="image" src="https://github.com/rubyiscoding/Book-Tracking/assets/74127503/31aca61c-e9eb-42a3-bf57-301c5acb8ad7">

## Book Category Types Delete Page
<img width="1435" alt="image" src="https://github.com/rubyiscoding/Book-Tracking/assets/74127503/9e3ec5c7-7752-4315-9379-4b0cb2fc77e2">

## Lessons learnt 

* Using Visual Studio Scaffolding helps create controllers and views based on model provided and db context.
* Being a newbie, vscode is a pain, I am unable to use shortcuts of Visual Studio 2022, I prefer Visual Studio.
* We can implement dependency injection by injecting the Manager or manager classes, DAL classes in Program.cs class in this way:
  builder.Services.AddSingleton<ICategoryManager, CategoryManager>();
* We can specify the application to use Sqlite db by using the code below in Program.cs class:
        builder.Services.AddDbContext<BookTrackerContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
* We can also specify other database types like options.UseSqlServer(....)
* In order to build a Dropdown list in MVC razor view, we can pass the data in SelectListItem datatype. However, their are also other ways     such as adding data into ViewBag(), ViewData[].
* We can inject db context into controllers' constructor method.
* I found Bootstrap classes to be easy and sleek to learn and implement. 
