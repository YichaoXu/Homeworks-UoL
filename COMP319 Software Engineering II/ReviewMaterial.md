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
