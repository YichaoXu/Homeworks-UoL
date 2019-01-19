# COMP327 Review material

## 1. Question about Swift:

### 1.1. Optional:

##### 1.1.1 Forced unwrapping
* Symbol:<br>
    "!" is used to force unwrapping an optional value.
* Situation:<br>
    we are certain that an optional value is non-nil or the program is prepared for it to fail.
* Failure:<br>
    If the value does evaluate to nil, then the program will crash.

##### 1.1.2 Optional chaining
* Symbol:<br>
    "?" is used by Optional Chaining.
* Situation:<br>
    It is used where it is possible for the variable or constant to have a value of nil.
* Failure:<br>
    Optional chaining fails gracefully when the value is nil.

##### 1.1.2 Casting string to int
* Casting Methods:
    - Declaration:<br> `Int.init?(_ description: String)`
    - Parameter:<br>
        if the argument of `description` is an ASCII representation of a number, the string will be converted to an integer.<br>
        Else, the conversion will fail and return nil.
    - Return:<br>
        Because the method will return Nil or Int as result, the compiler will infer the return value as Optional(Int).
* Print method:<br>
    when printing an Optional value, the word `Optional` will be added in front of it.
* Convert Optional: <br>
    Convert `Optional Int` into an `Int` through forced unwrapping

### 1.2 Protocol

##### 1.2.1 Introduction
It is an effective blueprint of class for a particular task or specific functionality.

##### 1.2.2 Contents
It includes the signature of methods, properties and other requirements:
- The signature of methods;
- The definition of properties including the get and set keyword (Read-only or read/write)


##### 1.2.3 Implementation
* A protocol can be adopted by a class, structure or enumeration type.
* The adopter must implement all of the non-optional methods and properties.


## 2. Question about E-commerce:

### 2.1 Apple Pay
It is Apple’s mobile payments service designed to make payment in retail stores through Apple's devices;

##### 2.1.1 Secure Mechanism
* Devices Account Number: <br>
    It is a unique "Token" assigned to each credit or debit card scanned into passbook to protect the card information.
* Secure Element: <br>
    It is a specially dedicated chip used to store all user's payment information (will never be upload online).   
* Once time dynamic security code: <br>
    It is a cryptogram for each transaction to verify the transaction being conducted from a device with a correct device account number.
* Biometric Authentication: <br>
    Each transaction need be authenticated through touch-ID, Face-ID or PIN.

###### 2.1.2 Process
* Both of the DAN and one-time dynamic security code are sent via NFC.  
* DAN and security code are passed to a gateway.    
* Gateway authenticates the DAN and security code,
* and thus transfers the payment and PAN to the bank.

##### 2.1.3 Comparison with traditional payment
* The user’s credit card number is replaced by the DAN during transmission;
* None will see a user’s credit card and personal information at any point;

### 2.2 Peer to Peer Payment
##### 2.2.1 Introduction
payment systems are an efficient and effective replacement for cash.

##### 2.2.2 Scenario:
* Payer opens P2P app and pick the payee;
* And tap out the amount of money;
* Application authenticate the user by PIN/Face/Touch-ID;
* Payee receives a notification about the transaction;
* and the payee can leave the money in the account or send it to the bank account.

##### 2.2.3 Features:
* Security:
    - Transfer of data is encrypted;
    - Authentication is required for accessing system;
* Payment:
    - Some systems notify for payment;
    - Payment is usually rapid;
    - Fees depends on the source of payment.

##### 2.2.2 Issues and example:
* Delay: <br>
There can be payment discard and payment delay.
* Error: <br>
It is possible to accidentally send money to the wrong recipient in the PayPal.
* Security: <br>
PayPal's systems are encrypted, however, there is still hacks and scams.

### 2.3 Micro Payment

##### 2.3.1 Introduction
* Description: <br>
Financial transactions involving very small sums of money;
* Motivation: <br>
Transaction fee of the standard payment systems for small payments is significant;

##### 2.3.1 Types
* Pre-paid accounts: <br>
MicroPayments can be drawn from this pre-paid account.
* Accumulated Balance Payment Systems:<br>
Accumulate small charges, then bill periodically

### 2.4 Premium SMS based transactional payments

##### 2.4.1 Introduction
* Description: <br>
Payment via an SMS message to a short code
* Properties:
    - It can pay charge through the phone-bill.
    - It is suitable for phone-based goods.

#### 2.4.2 Challenges
* Poor Reliability;
* Slow Speed;
* High Setup and Running Costs;
* Low Pay-out Rates (<30%);
* Low Follow-on Sales.


## 3. Question about Mobile Communication

### 3.1 Background

##### 3.1.1 Fixed-line
* Medium:
    - Wired and Private: Each device directly connects with an AP or router.  
* Location:
    - The Internet AP typically will not change.
* Power:
    - Devices are directly connected to the electric source.

##### 3.1.2 Mobile
* Medium:
    - Wireless: The medium is unpredictable and may be eavesdropped;
    - Shared: The medium may be congestion and noisy causing low-connection quality;
* Location:
    - The user is moving and the access point is liable to change;
    - Automatically handling the change is required by a seamless network.
* Power:
    - Battery powered: It impacts the ability to increase the device's signal power or to do signal processing.


### 3.2 Mobile Multiplexing Scheme

##### 3.2.1 Space Division Multiplexing:
* Description:
    Separates out user based on their location.
* Analogue:
    Separate people out so that their conversations will not be interrupted by other people.
* Property:
    Rely on the signal attenuation with the distance to avoid the interference.

##### 3.2.2  Frequency Division Multiplexing:
* Description:
    Partitions the available radio spectrum into a series of frequency bands.
* Analogy:
    Different groups of people talking at different pitches in the same room.
* Property:
    Crosstalk between different channel may cause interference.

##### 3.2.3 Time Division Multiplexing:
* Description:
    Splits a channel into fixed-length time slots and allocated to users.
* Analogy:
    The different people taking turns to talk to one another.
* Property:
    It is Highly effective for the package based communication

##### 3.2.4 Code Division Multiplexing:
* Description:
    Uses different coding schemes for each communication channel.
* Analogy:
    A group of people communicating with one another using different languages
* Property:
    Data is encrypted;
    Different data can be transmitted simultaneously;
    It is complex and requires new hardware.


### 3.3 Communication Switching

##### 3.3.1 Circuits Switching:
Establish a dedicated communication channel through a network before they communicate;

* Advantages: <br>
The communication appears as a direct connection between two nodes:
    - It is durable;
    - uses the full bandwidth


* Disadvantages:
    - The circuits may be insufficient for all users;
    - The charge will be time-based (expensive).

##### 3.3.2 Package Switching:
Communicate through individual packets with extra header information for routing and packet reassembling;

* Advantages: <br>
    It is more efficient to use network resources.


* Disadvantages
    - Network congestion may cause the packet loss (causing a low quality to communicate);
    - The dropped packet will only be resent when it is overtime.


### 3.4 GSM

##### 3.4.1 Cell Capacity
* Background:
    - GSM use both of the TDM and FDM.
    - GSM frame consist of 8 time slots.
* Function:
    - The function for calculate the cell Capacity is c = v*s*t;
    - v: the number of voice time slots;
    - s: the number of sectors;
    - t: the number of transceivers.

##### 3.4.2 Text
* Emoji: 16-bits;
* Octet: 8-bits;
* ASCII: 7-bits.


### 3.5 Handover
It is the process of transferring a connection from one channel to another;<br>
Both of the hard and soft handovers are methods for handover. <br>
Phone, old and new cells determine if and when to handover.

##### 3.5.1 Due to:
* Change of user behaviour;
* Location of a phone is changed;
* Interference between channels occurs;
* Signal becomes degraded due to local conditions;
* Current cell is exhausted, and there is another available cell.

##### 3.5.2 Hard Handover
* Description: <br>
    With hard handover, the channel in the source cell is released before the channel in the target cell is engaged.
* Properties:
    - The call will fail when the new cell fails during handover.
    - It is used by GSM, and typically involves the radio tuner using an alternative coding system for each connection, but the same frequency.

##### 3.5.3 Soft Handover
* Description:<br>
    With soft handover, the old channel is only released when the new connection is considered reliable.
* Properties:
    - The device may hold two channels simultaneously until there is no advantage in using two.
    - It is more complex and expensive for implementation;
    - It will reduce cell capacity while multiple channels are used.


### 3.6 Wi-Fi and 3G

##### 3.6.1 Tech:
* 2G: GSM
* 2.5G: GPRS (Improvement of GSM)
* 3G: IMT-2000 (GSM EDGE, UMTS, CMDA, HSPA)
* 4G: LTE (HSPA+)
*
##### 3.6.2 Comparison
* Wi-Fi (433Mbit/s) is faster than 3G even 4G(20Mbit/s) in general;
* Wi-Fi in smaller venues where many users share single ADSL may be slower than 3G or 4G.
    - Bandwidth is shared;
    - The wireless access point is not as powerful as the cell tower.
    - and it needs to coordinate communication from multiple users simultaneously;

### 3.7 WAP

##### 3.7.1 Introduction
* Description: <br>
Wireless Application Protocol is An open international standard to support access to the Mobile Web.
##### 3.7.2 Deck of Cards Metaphor
* Description: <br>
It is a model used in WML for content display and navigation.
    - Card: A dialogue-based interaction with many options;
    - Deck: Many cards bundled together;

* Motivation: <br>
A deck can be sent in one transmission so that the time and delays could be reduced.

##### 3.7.3 Issue
* Criticisms:
    - WML language
    - Unclear requirements
    - Constrained User Interface
    - Lack of Good Authoring Tools

* Why fail:
    - Technical limitations due to GSM;
    - Poor site design (Bad user experiment);
    - Poor content and management
    - Device limitations


## 4. Question about HCI

### 4.1 Introduction
HCI traditionally refers to the processes and models for the design of the operational interface

##### 4.1.1 Motivation

* poorly designed user interfaces lead to:
    - Higher training costs;
    - Higher usage costs;
    - And lower adoption.
* To support more effective use, an interface should be:
    - useful: accomplishes the required task ;
    - usable: easy and reliable to use;
    - used: enriches the user experience.
* and to be adopted, a system should be:
    - fit for purpose;
    - provide some value to the customer over the usage cost (both effort and financial).

##### 4.1.2 Apple's Principle:
* Responsiveness: <br>
    Actions should lead to a direct result in a reasonable time;
* Permissiveness: <br>
    Users should be able to do anything which is reasonable;
* Consistency: <br>
    Mechanisms should be used in the same way in wherever they occur.

##### 4.1.3 Heckel’s law
* Description: <br>
    The quality of the user interface of an appliance is relatively unimportant in determining its adoption if the perceived value is high.
* Example: <br>
    The physical keyboards on a mobile phone often too small to be used conventionally, but it makes the typing simpler (eg: For email).

### 4.2  Virtual Keyboard

##### 4.2.1 Introduction
Present a keyboard on the screen, which the user can then interact with.

##### 4.2.2 Challenge:
* Traditionally, a virtual keyboard should be made as small as possible;
* The resistive screen requires its user to accurately hit the correct location.

##### 4.2.3 Auto-correct
* Description: <br>
    It is the predictive texting and dictionary-based function adopted from SMS application to predicate what did user want to type and to offer the completed words.
* Motivation: <br>
    The use of a capacitance-based screen causes a higher probability for the typo.


## 5. Question about the Context-Aware System

### 5.1 Spatial Context Aware System

##### 5.1.1 Relevance
* Location Awareness;
* Orientation;
* Nearby Object.

##### 5.1.2 Possible
* Ordering the local services by distances;
* Discovering local service or objects;
* Providing service based on location.

##### 5.1.3 Application
* Navigation: <br>
The Google Map navigates user from source to destination.
* Context Change: <br>
The Google Map provides user a best route based on the current traffic.
* Personal Emergency: <br>
When the cad is broken down, the application can send a message with a precise GPS location to alert the emergency services.
