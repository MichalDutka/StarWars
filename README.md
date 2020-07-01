#Star Wars
Create "Star Wars" RESTful API service. Implement CRUD (Create, Read, Update, Delete) operations for managing 'Star Wars' characters. Persistence layer should be implemented using some ORM and relational database.
Start with something simple (e.g. create enpoint that will return hardcoded list of characters) and progressively enhance the service. Try to use best practices and architecture patterns (e.g. SOLID principle, clean architecture).

Extra tasks:
a) example unit and/or integration and/or functional tests
b) swagger support
c) pagination

Example data structure in JSON:

```json
{
   "characters":[
      {
         "name":"Luke Skywalker",
         "episodes":[
            "NEWHOPE",
            "EMPIRE",
            "JEDI"
         ],
         "friends":[
            "Han Solo",
            "Leia Organa",
            "C-3PO",
            "R2-D2"
         ]
      },
      {
         "name":"Darth Vader",
         "episodes":[
            "NEWHOPE",
            "EMPIRE",
            "JEDI"
         ],
         "friends":[
            "Wilhuff Tarkin"
         ]
      },
      {
         "name":"Han Solo",
         "episodes":[
            "NEWHOPE",
            "EMPIRE",
            "JEDI"
         ],
         "friends":[
            "Luke Skywalker",
            "Leia Organa",
            "R2-D2"
         ]
      },
      {
         "name":"Leia Organa",
         "episodes":[
            "NEWHOPE",
            "EMPIRE",
            "JEDI"
         ],
         "planet":"Alderaan",
         "friends":[
            "Luke Skywalker",
            "Han Solo",
            "C-3PO",
            "R2-D2"
         ]
      },
      {
         "name":"Wilhuff Tarkin",
         "episodes":[
            "NEWHOPE"
         ],
         "friends":[
            "Darth Vader"
         ]
      },
      {
         "name":"C-3PO",
         "episodes":[
            "NEWHOPE",
            "EMPIRE",
            "JEDI"
         ],
         "friends":[
            "Luke Skywalker",
            "Han Solo",
            "Leia Organa",
            "R2-D2"
         ]
      },
      {
         "name":"R2-D2",
         "episodes":[
            "NEWHOPE",
            "EMPIRE",
            "EMPIRE",
            "JEDI"
         ],
         "friends":[
            "Luke Skywalker",
            "Han Solo",
            "Leia Organa"
         ]
      }
   ]
}
```
