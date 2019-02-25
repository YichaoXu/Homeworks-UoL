## 1. Question about the Design pattern

### 1.1 MVC:

#### 1.1.1 Describe in detail the model view controller design pattern.
* Description:
    - It is an architectural pattern;
    - It splits the code into 3 parts: model, view and controller.
* Model:
    - Implements the business logic;
    - Stores application states and data;
    - Maintains data validation and persistence;
* View:
    - Render the data to a format that can be presented to the user;
* Controller:
    - Interprets user input (press keyboard or click mouse) and sends it to the model;
    - The sent message is typically formatted as a command with name and associated arguments (for example HTTP Get and Post);

#### 1.1.2 Describe the two benefits of the model/view/controller
* Code security:
    - Updates to the interface (view and controller) will not modify the model.
    - Updates to the business logic (model) will not affect the view and controller.
* Multiple interfaces:
    - simpler to support multiple interfaces to an application:
    - The different VCs can be connected to the standard interface provided by the model code.
    - The software can be developed by two teams to implements GUI and the application area with database respectively.

#### 1.1.3 Describe the flow of events for an MVC web request for login for a web-mail application.
* The user goes to the email URL:
    - The VIEW sends a user a login page in HTML5 to the user’s browser (The VIEW can render different the HTML5 code for different screen sizes).
* The user types in their user/name and password and presses the submit button:
    - The CONTROLLER intercepts the submitted Form page and constructs a Login command object. After that, the CONTROLLER sends it to the MODEL.
    - The MODEL checks the user’s credentials against the database and then makes a request to the VIEW.
    - If the user’s credentials fail, the VIEW is requested to generate a failed login page.
    - If the login is successful, the MODEL retrieves the user’s inbox from the database and makes a request to the VIEW to display this data.
    - The VIEW generates the HTML and sent it back to the user’s browser.

### 1.2 Memento

#### 1.2.1 Introduction
It is a behavioural design pattern that provides the ability to restore an object to a previously saved state.

#### 1.2.2 Originator
* Description: <br>
The object that knows how to save internal states of itself.

* Behaviour:
    - generate a memento class for storing its current state;
    - restore stored state from a memento object.

#### 1.2.3 Memento
* Description: <br>
stores the state of originator.
* Properties: .
    - Make the state private but keeps it encapsulated;
    - allows the object to be persisted without breaking its encapsulation;

#### 1.2.3 Care Taker
Manipulates the originator object.


### 1.3 Chain of Responsibility pattern

#### 1.3.1 Introduction
A number of classes work together like a chain to handle a message or process command or request.
    - If a class doesn’t want to handle the message, the class passes it down to the next class in the chain;
    - else, the class handles the message and pass it down for other classes to handle.

#### 1.3.2 Example
* When log-in an application, a logger can deal with the message or send the message to the next logger in the list;
* It depends on the level of the message and the current logging level.

### 1.4 Singleton

#### 1.4.1 Introduction
a class which only supports a single instance.
* has to control its own creation;
* not allow to create new instances external to the class.

#### 1.4.2 Properties
* The constructor is private;
* The instance can be accessed through a static method;
* The instance and its data can be shared by multiple threads.

#### 1.4.3 Example
a helper object which is used to connect to an SQL database.


### 1.5 Double lock checking

#### 1.5.1 Introduction
It a kind of method for concurrency code,
    - First check: the lock is needed BEFORE applying the synchronous keyword.
    - Second check: the shared data does not been overwritten during the locking process.

#### 1.5.2 Motivation
* Sometimes, it is important for a concurrency to use the "Synchronization".
* "Synchronization" will slow down an application significantly.

#### 1.5.3 Benefit
reduces the number of synchronous calls to speed up the code.


### 1.6 Factory Pattern

#### 1.6.1 Introduction
The factory pattern is used when the exact type of an object is known in run time.

#### 1.6.2 Example
* Program need process an image file to create an image object in memory and the image file may be PNG, JPG and GIF format.
* The code need be able to apply different class for different files.
* It will make the code be complex and will not work if the type of the image file is unknown until compile time.
* The solution is to use the factory pattern to return an abstract image class and let all image classes inherit the abstract class.


### 1.7 Builder Pattern

#### 1.7.1 Introduction
It separates the abstract definition of an object from its representation

#### 1.7.2 Properties
* Reduce the complexity of the built item (syntax of XML);
* Reduce the possibility of the error (typo in SQL statement);
* Improve the reusability of the code (eliminate Different SQL statement);
* Make Refectory easier (Extra functionality can be added easily);
* Validation can be an easy implementation (the argument can be validated in helper class)


## 2. Question about the Agile:

### 2.1 Poker Planning:

#### 2.1.1 What is poker planning?
Poker planning is a cost estimation technique:
* Allocate each developer a pack of the card with numbers on.
* The project manager:
    - Explains the project;
    - Answer the question from the developer (to clarify any assumptions with the project);
    - Discuss any possible risks.
* Each member picks a card as their estimate and show their cards at the same time.
* The members (who make the lowest and highest estimation) justify their decision to the whole group.
* Repeat the process until a consensus is reached.

#### 2.1.2 Benefits and anchoring?
* Anchor:
    In an open discussion, some members of the team strongly advocate a particular estimation and this persuades other team members to follow their estimation.
    - Low anchors: from sales marketing or other product owners.
    - High anchors: from development team members.

* Planning poker:
    - avoid anchoring.
    - less optimistic and more accurate than the simple combination of individual estimates.

### 2.2 Pair Programming：

#### 2.2.1 Introduction
Two programmers work together to complete the programming task:
* Driver writes the code;
* Reviewer reviews the code (makes suggestion and comments on the approach)
* periodically swap the role.

#### 2.2.2 Benefits
* Reduce risk
    - Reduce the number of a design error or serious flaw;
    - Reduce the probability of structurally unsound.
* Improve the code quality through code reviewing
* Improve the code ownership.
    - Spreads the responsibility of code;
    - The partner can replace the developer at any time.

#### 2.2.3 Research Finding:
* decreases 15~30% project developing times;
    - Normally it can decrease to 84% of the previous time;
    - For the juniors, it may be increased to 110%;
    - The simpler tasks are, the more time can be reduced.
* increases 15~60% programmer working hours;
* increases 15% correctness
    - It is important for critical quality criteria project;
    - It can find 7% errors overall and 73% errors for the juniors;
    - The more complex problem is, the higher the code quality becomes.  
* Conclusion:
    - it seems to be able to deliver code slightly faster, of higher quality but at considerable cost;
    - However, the quality Improvement earlier can save time later on.
    - Then, it may be hard to evaluate in a short time.

### 2.3 Scrum

#### 2.3.1 Introduction
* Description: <br>
In the SCRUM (The name comes from the game of rugby), The team work together and do the development in a series of development steps called sprints.

* Daily SCRUM: <br>
This is a short meeting in which all developers answer the following questions:
    - What have you done since yesterday?
    - What are you planning to do today?
    - Any impediments/stumbling blocks?

#### 2.3.2 Sprint
* Description: <br>
The SCRUM the lifecycle splits the development into a series of efforts called Sprints which
    - lasts around 1 to 4 weeks;
    - is preceded by a sprint planning meeting.

* Sprint planning:
It is a meeting for a sprint to decide the scope of work and the required time of it. Then, this work is added to the sprint backlog.
    - The amount of work is carefully selected so that it is enough to fill the sprint;
    - It is done by evaluating the workload of previous sprints and the projects-velocity.
    - The uncompleted work in the sprint backlog will be returned to the product backlog.

* Burn down chart:
the burn-down chart is used to measure the progress of the project ;

#### 2.3.3 Product Backlog
* Description: <br>
A document contains all work to complete the project;

* Item Types: <br>
The backlog can include anything
    - from product features
    - to debugging tasks
    - and technical tasks.
* Item Content: <br>
Each item in the product backlog has
    - Description/specification;
    - Score in terms of its business value;
    - Estimation from the development team;


### 2.4 XP

#### 2.4.1 user stories
* Description: <br>
It is a set of descriptions of functionality which have value to the end user.
    - Make the end customers easier to understand;
    - Make communication between the end user and the development team easier.

* Properties:
    - Short: easy to estimate and test;
    - Independent: can be implemented separately.


#### 2.4.2 User Case descriptions

* Description: <br>
It has more structure and defined in more detail than user stories.
    - "User case descriptions" provide more detail to the developer (such as how UI work)
    - It makes reusability can be easier implements by the conception of inclusion in the UCD.

* Added Details:
    - UC exceptions (describe what happens in exceptional conditions);
    - Pre-conditions before the case happen;
    - And what will happen afterwards?


## 3. Question about Estimation

### 3.1 Cost Evaluation

#### 3.1.1 Introduction
* Description: <br>
It is a difficult task in software engineering, due to a number of factors, for example poorly understand about what will be involved.

* Reason for Overestimate:
    - The manager wants to overestimate to the project to avoid the overrun;
    - It may be the more financial benefit (payment from the customer) to overestimate.
* Reason for Underestimate:
    - Lack of experience of a project manager;
    - Pressure from management.

* Effects of overestimation:
    - waste and over-resourcing of projects
    - lead the business to lose trade.
* Effects of underestimation:
    - over-runs of the project;
    - lose reputation;
    - contract penalty.


#### 3.1.2 Cone of Uncertainty
* Description: <br>
The diagram measures (figures estimates / actual final project time) in different phase of the project.

* Properties:
    - The Left ~ is early estimates and the Right ~ is late estimates;
    - The range of f/a was wide at the start of the project and it is narrowing down as the project progressed

* Result:
    - f/a = 1: Perfect estimation;
    - f/a < 1: underestimation;
    - f/a > 1: Overestimation.

#### 3.1.3 Estimation Quality Factor
* Description: <br>
It is a technique used to determine the quality of estimation.
    - High EQF: the estimate is accurate with little deviation.
    - Low EQF: the estimate is bad.

* Effect Factors:
    - estimation skills;
    - knowledge about the project;
    - Pressure from manager.

#### 3.1.4 Estimation Bias
* Description: <br>
It measures a number of estimates to shows if the estimator in general over- or under- estimates.

* Results:
    - Bias = 0: the overestimates equal the underestimates;
    - Bias > 0: the estimator is generally overestimated;
    - Bias < 0: the estimator is generally underestimated.


### 3.2 Quality Evaluation - Program Slicing

#### 3.2.1 Definition of the Program Slicing?
A technique used to help simplify the reading of code for the purpose to make it more readable for maintenance and debugging.

#### 3.2.2 What is Dependency graph?
* Description: <br>
A representation of the dependencies within a piece of code.
    - the relationship between code statements;
    - the effect of execution of code to other statements.

* Dependency Types:
    - Control Line
    - def-order dependency

#### 3.2.3 Describe the main applications of both forward and backward slicing?

* Forward slicing: <br>
it is used to make it easier to maintain code, for example:
    - Start-point: It is the line that user is going to modify;
    - It makes the user be able to see the lines that are affected by changing of the start point.

* Backward slicing: <br>
it is used to make it easier to debug code and discover the cause of bugs:
    - Start-point: The line where the faulty data has been discovered;
    - It reveals all the lines which have an effect on the start point.

## 4. Question about Actor Model:

### 4.1 Background:

#### 4.1.1 The problem of Multithread:
* Deadlock: a state in which each member of a group is waiting for some other member to take action.
* Starvation: a process is perpetually waiting for service and not getting any CPU time.

#### 4.1.2 Description:
A way of modelling concurrency where the state data is not shared between different threads.

### 4.2 Actor Model Conception:

#### 4.2.1 Actor Behaviours:
* Create an Actors;
* Send a message to others;
* Receive and handle the incoming messages;

#### 4.2.2 Message:
* It should be immutable: the message cannot be modified after sending;
* The message can be sent in two possible way:
    - By reference: it is the fastest approach. Only the pointer of the message is sent but do not work for the actor on physically separate systems
    - By value: The message must be copied to the other Actor.

#### 4.2.3 Features:
* An individual working-thread will process the incoming messages.
* Each Actor has its private data which cannot be read or write from outside.
* The Actors of an application can run in the different OSs and in the different device on the Internet.

#### 4.2.4 Benefits:
* No deadlock
* Scalable
* Actor addressing: Message addressing is transparent (done by the broker).
* Fair scheduling: the actors in a system are fairly scheduled.

#### 4.2.5 Actor mobility:
* Description: <br>
it is important in the actor model to be able to move actors between systems
* Reason:
    - localize reference;
    - Load sharing;
    - Improve Reliability.
* Types:
    - Strong: Actor and all its state can be transferred to the new machine.
    - Week: Actor can be transferred to a new machine without all its state.
* Processes:
    - stop the actor;
    - capture its state;
    - create a new actor and delete the old one;
    - restore the state to the new actor and re-direct messages.

### 4.4 Example:
#### 4.4.1 Request Balance:
An actor representing the client gets the account balance from an actor representing bank account:
* Client-Actor sends message Request Balance to Account-Actor's inbox;
* Account Actor handles the message stored in the inbox in order;
* After that, it will send the message back to the client actor containing the Balance.
