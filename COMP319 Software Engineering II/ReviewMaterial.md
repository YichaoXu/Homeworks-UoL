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

#### 1.1.2 Describe the TWO benefits of the model/view/controller
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
    -  Low anchors will generally come from sales marketing or other product owners.
    - High anchors will generally come from development team members.

* Planning poker:
    - It can help avoid anchoring.
    - It less was less optimistic and more accurate than the simple combination of individual estimates.

### 2.2 Pair Programming：

#### 2.2.1 Introduction
Two programmers work together to complete the programming task:
* Driver writes the code;
* Reviewer reviews the code (makes suggestion and comments on the approach)
* The two programmer will periodically swap the role.

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
    - important for critical quality criteria project;
    - Found 7% errors overall and 73% errors for the juniors;
    - The more complex problem is, the higher the code quality becomes.  
* Conclusion:
    - it seems to be able to deliver code slightly faster, of higher quality but at considerable cost;
    - However, the quality Improvement earlier can save time later on.
    - Then, it may be hard to evaluate in a short time.

#### 2.2.4 Dependency Graph
* Control Line
* def-order dependency

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
    - Estimation by the development team;


### 2.4 XP

#### 2.4.1 user stories
* Description: <br>
It is a set of descriptions of functionality which have value to the end user.
    - It is easier for end customers to get to understand;
    - It provides an easy communication channel between the end user and the development team.

* Properties:
    - Short enough so that easy to estimate and test;
    - Independent from one another so they can be implemented separately.


#### 2.4.2 User Case descriptions

* Description: <br>
It has more structure and defined in more detail than user stories.
    - "User case descriptions" provide more detail to the developer (such as how UI work)
    - It makes reusability can be easier implements by the conception of inclusion in the UCD.

* Added Details:
    - UC exceptions (describe what happens in exceptional conditions);
    - Pre-conditions before the case can happen;
    - And what will happen afterwards?

* In practice:
user stories are easier for end customers to get to grips with and provide an easy communication channel between the end user and development team. User case descriptions provide more detail to the developer. Without user case descriptions details such as how the UI works in detail could be lost. User cases also have the concept of inclusion which makes it easier to see re-use within the development.

## 3. Question about Estimation of the code quality

### 3.1 Program Slicing
#### 3.1.1 Definition of the Program Slicing?
A technique used to help simplify the reading of code for the purpose to make it more readable for maintenance and debugging.
#### 3.1.2 What is Dependency graph?
A representation of the dependencies within a piece of code.
* It shows the relationship between code statements;
* how some code statements can affect the execution of other code statements.

#### 3.1.3 Describe the main applications of both forward and backward slicing?

* Forward slicing: <br>
it is used to make it easier to maintain code, for example:
    - Start-point: It is the line that user is going to modify;
    - The slice makes the user are able to see what lines are affected by changing of the start point.

* Backward slicing: <br>
it is used to make it easier to debug code and discover the cause of bugs:
    - Start-point: The line where the faulty data has been discovered;
    - The slice reveals all the lines which have an effect on the start point.

## 4. Question about Actor Model:

### 4.1 Background:

#### 4.1.1 The problem of Multithread:
* Deadlock: a state in which each member of a group is waiting for some other member to take action.
* Starvation: a process is perpetually waiting for service and not getting any CPU time.

#### 4.1.2 Description:
A way of modelling concurrency where the state data is not shared between different threads.

### 4.2 Actor Model Conception:

#### 4.2.1 Actor Behaviours:
* Create other Actors;
* Send a message to other Actors;
* Receive and handle the messages from other Actors;

#### 4.2.2 Message:
* It should be immutable: the message cannot be modified after sending;
* The message can be sent in two possible way:
    - By reference: it is the fastest approach. Only the pointer of the message is sent but do not work for the actor on physically separate systems
    - By value: The message must be copied to the other Actor.

#### 4.2.3 Features:
* Each Actor has a working thread for processing the incoming messages.
* Each Actor has its own data which cannot be directly read or write by others.
* Actor for an application can run in the different operating system and different location communicating through the internet.

#### 4.2.4 Benefits:
* No deadlock: it's impossible to get a deadlock.
* Scalable: it's scalable.
* Actor addressing: Message addressing is transparent (done by the broker).
* Fair scheduling: each actor in the system get a fair amount of processing time.

#### 4.2.5 Actor mobility:
* Description: <br>
it is important in the actor model to be able to move actors between systems
* Reason:
    - The locality of reference;
    - Load sharing;
    - Improve Reliability
* Types:
    - Strong: Actor and all its state can be transferred to the new machine.
    - Week: Actor can be transferred to a new machine without all its state.
* Processes:
    - stop the actor;
    - capture the state of the actor;
    - creation of a new actor and delete the old one;
    - restoring the state to the new actor and re-direct messages.

### 4.4 Example:
#### 4.4.1 Request Balance:
An actor representing the client gets the account balance from an actor representing bank account:
* Client-Actor sends message Request Balance to Account-Actor's inbox;
* Account Actor handles the message stored in the inbox in order;
* After that, it will send the message back to the client actor containing the Balance.
