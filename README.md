# Contacts Application

## Setup Instructions
1. Run backend API
   - Open solution in Visual Studio
   - Run ContactsApplication.Server
   - https://localhost:7123/swagger/index.html will open
2. Install dependencies
   - Navigate to contactsapplication.client
   - In command line run: `npm install`
3. Run frontend application
   - Navigate to contactsapplication.client if you changed directories
   - In command line run: `npm run start-custom`
   - http://localhost:57545/ will open

## Design Decisions
1. This application utilizes Angular 17.0.0, and .net 6.0
   - I decided to go with the most recent version of Angular so that I could learn a little bit more about the most recent features
3. Why not use html table element?
   - I originally planned to use the existing html table element, but I was having difficulty when trying to get it to work nicely with Angular components. In the future, when I have more time, I would like to convert it to using the table element instead of a sort of handmade one as it doesn't feel fully responsive in certain areas.
4. appsettings.json
   - I decided to use a "Contacts" object in appsettings.json in order to allow for the use of IOptions<T>. This would help the application to scale further in the future by allowing for the use of additional files. For example, if a request was made to be able to read additional contact information from a separate file, the contacts option could be reconfigured to include additional file paths. `"Contacts": {
  "FilePath": "./contacts.json"
}`

## Future Implementations
In the future I woud like to implement several more features to thoroughly flesh out the application.
1. Pagination
  - Implement pagination so that if the list of contacts exceeds a certain number, the remaining are sent to the next page. This would allow the application to look more clean because if currently an excessive number of contacts are added, the page continues indefinitely.
2. Search
   - Implement search functionality to allow a user to search for contacts by Id, First Name, Last Name, or Email
3. Sorting
   - Allow a user to sort columns by ascending or descending order
4. Bulk Manipulation
   - Implement bulk manipulation functionality to allow a user to select multiple contacts and edit or delete
5. Contact Picture
   - Allow contacts to have a picture attached
6. View Contact
   - When clicking on a contact row, open a new page to that contact's profile which would display more in depth information about them including: their profile picture, small description, age, address, etc.
7. Admin and User
   - Create a sign in page to allow admins access to manipulate all users, and only allow other users to manipulate users that they have added.
8. Navigation Bar
   - Implement a navigation bar to allow a user to view their own contact profile and update their information
9. Google Sign In
   - Implement the use of signing in with Google
