# TaskProject
Task:
Create a solution for managing product catalog. Users must use a web application (MVC) to add, edit, remove, view, search and export(Excel) product catalog items.

Technologies:
Asp.net MVC
MSSQL & Entity Framework (Code First)
Jquery
Ajax 

How to work as a user?
_this site is a one page site
run your project in localhost the main and only page in path /Home/Index
you will find a List of Products which is currently added to your database
you can use the arrow above at each main coloumn and search label to search from available products 
by click on create button pop up window will show up to add your product data 
after submitting alert section will show with green background at the right side of the page and product will show in the table 
there are 3 buttons in each row one for details , edit, delete the product fromyour list 

Workflow of the code mechanism:
for each submit we called a function in javascript using "onclick" and each function take item ID as its input 
for 3 functions that responsable of (Edit/Delete/Show Details)  each one of them pass the id to the Action in controller that responsable of backend function and return the result as json which we use it again in the respond.
