- Create a user storage system that allows to store users entities.
- A user is an entity with first/last name, date of birth, personal id (like in passport), gender (enum), an array of visa records (struct { country, start, end }). Add functionality to compare users, and get a hashcode of the entity.
- There should be several ways to store users (for ex. In DB), but we need only one implementation – in memory. But there should be a possibility to add an another implementation.
- Methods for storage:
	o Add a new user: Add(User user) -> returns User ID
	o Search for an user: SearchForUser(ISearchCriteria criteria) -> returns User IDs. At least 3 criteria.
	o Delete an user: void Delete(...)
- When creating a new there should be a possibility to change the strategy to generate an ID.
- When adding a new user there should be a way to set a different set of rules for validating an user entity before adding it: Add(user) -> validation -> exception if not valid or generate and return a new id.
- Add tests.
-Create a repository for your tasks on the github like EPAM.RD.2016S.[your last name].
-Add an ability to store service state
	o A service should have an ability to store it's state in an XML file on disk.
	o A service should have an ability to store a last generated ID, so the service can continue generating id from where it stops.
	o A filename for XML get from App.config.
- Add an ability to log all requests to a service.
	o Use System.Diagnostics.BooleanSwitch for enabling/disabling logging.
	o Add a possibility to configure log listeners via App.Config: ConsoleTraceListener, and others.
- Add a replication ability for user service nodes:
	o Add an ability to have several instances of user service.
	o One service should be Master.
	o Other services should be Slaves.
	o When Master receives a Add or Delete command, it should send a notification to slave services for updating data.
	o If Slave receives Add or Delete command it throws an exception.
	o All configuration is stored in App.config: a number of services, their types.
- Create a custom section in App.config file, so the number of instances and their role can be changed there.
	o Create each instance of the user storage service in new application domain, and add support for communication between all .
- Create each instance of the user storage service in new application domain, and add support for communication between all .
- Add functionality that allows master send notifications to slaves via network.
	o You will need a new message class that will contain all the data related to event: Add or Delete, User data or id. Make it serializable.
	o It's okay if you will establish connection between master and slave when the master node starts.
	o Implementations is simpler when master establish connection to slaves, and then maintain it. This implementations assumes that slaves are passive listeners.
	o Use NetworkStream to create a channel, and you can use Socket class or TcpListener/TcpClient. (Heorhi said that TcpListener and TcpClient have lack of functionality, and it's simpler to use Socket class. You can consult him if you have any questions).
	o It's possible to start slaves first, and then start master to avoid conflicts when master is running and there are no slaves.
	o Store all configuration informations about hosts/ports in custom section in App.config.
- Protect your user services from concurrent calls.
	o For master – calling Add/Delete, Add/Search, Delete/Search simultaneously.
	o For slaves – calling Search and updating an user repository simultaneously.
	o Check how your code works by making calls to master service in several threads.
- Make your application run in several threads, one thread per user service. For example:
	o T1 is making calls to master service: Add, Delete, Search in cycle with Sleep().
	o T2 is making calls to slave #1: Search in cycle with Sleep().
	o T3 is making calls to slave #2: Search in cycle with Sleep().
	o Changes (Add/Delete) on master node should lead to changes in Search output on slaves.
- Make custom serialization 
