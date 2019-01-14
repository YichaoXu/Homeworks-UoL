# COMP327 Review material

## 1. Question about Swift:

### 1.1. Optional:

##### 1.1.1 Forced unwrapping
* Symbol:
    "!" is used to force unwrap an optional value.
* Situation:
    we are certain that an optional value is non-nil or the program is prepared for it to fail.
* Failure:
    If the value does evaluate to nil, then the program will crash.

##### 1.1.2 Optional chaining
* Symbol:
    "?" is used by Optional Chaining.
* Situation:
    It is used where it is possible for the variable or constant to have a value of nil.
* Failure:
    Optional chaining fails gracefully when the value is nil.

##### 1.1.2 Casting string to int
* Casting Methods:
    - Declaration: `Int.init?(_ description: String)`
    - Parameter:
        if `description` is an ASCII representation of a number, the string will be converted to an integer.
        Else, the conversion will fail and return nil.
    - Return:
        Because the method will return Nil or Int as result, the compiler will infer the return value as Optional(Int).
* print method:
    when printing an Optional value, the word `Optional` will be added in front of it.
* Convert Optional:
    Convert `Optional Int` into an `Int` through forced unwrapping

### 1.2 Protocol

##### 1.2.1 Introduction
It is an effective blueprint of class for a particular task or specific functionality.

##### 1.2.2 Contents
It includes the signature of methods, properties and other requirements.
* The type of each property;
* The return value of each method
* The name of the properties and methods;
* The access modifier of the properties and methods;


##### 1.2.3 Implementation
* A protocol can be adopted by a class, structure or enumeration type.
* The adopter must implement all of the non-optional methods and properties.


## 2. Question about E-commerce:

### 2.1 Apple Pay
It is Apple’s mobile payments service designed to make payment in retail stores through Apple's devices;

##### 2.1.1 Secure Mechanism
* Devices Account Number:
    It is a unique "Token" assigned to each credit or debit card scanned into passbook to protect the card information.
* Secure Element:
    It is a specially dedicated chip used to store all user's payment information (will never be upload online).   
* Once time dynamic security code:
    It is a cryptogram for each transaction to verify the transaction being conducted from a device with a correct device account number.
* Biometric Authentication:
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

##### 2.2.2 Issues and example:
* Delay: There can be payment discard and payment delay.
* Error: It is possible to accidentally send money to the wrong recipient in the PayPal.
* Security: PayPal's systems are encrypted, however, there is still hacks and scams.
* Different Fee: Fees of the PayPal payment are different between the credit card and debit card.


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
* Advantages:
    - Establish a dedicated communication channel through a network before they communicate;
    - This communication channel is durable and uses the full bandwidth (appearing as a direct connection between two nodes).


* Disadvantages:
    - There may not be sufficient circuits for a number of users;
    - The expend of the connection may be time-based and expensive.

##### 3.3.2 Package Switching:
* Advantages:
    - Communicate through individual packets with extra header information for routing and packet reassembling;
    - It is more efficient to use network resources.


* Disadvantages
    - Traffic congestion may cause the packet loss;
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
* ASCII: 7-bits;
* Emoji: 17-bits;
* Octet: 8-bits.


### 3.5 Handover
It is the process of transferring a connection from one channel to another; Both of the hard and soft handovers are methods for handover.

##### 3.5.1 Hard Handover
* Description:
    With hard handover, the channel in the source cell is released before the channel in the target cell is engaged.
* Properties:
    - The call will fail when the new cell fails during handover.
    - Involves the radio tuner using the alternative code system.
* Usage: GSM.

##### 3.5.2 Soft Handover
* Description:
    With soft handover, the old channel is only released when the new connection is considered reliable.
* Properties:
    - The device may hold two channels simultaneously until there is no advantage in using two.
    - It is more complex and expensive for implementation;
    - It will reduce cell capacity while multiple channels are used.


### 3.6 Wi-Fi and 3G
##### 3.6.1 Comparison
* Wi-Fi is faster than 3G in general;
* Wi-Fi in smaller venues where many users share single ADSL may be slower than 3G or 4G.
    - bandwidth is shared;
    - wireless access point need to coordinate communication from multiple users simultaneously;
    - wireless access point is not as power as the cell tower.



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

##### 4.1.2 Heckel’s law
* Description:
    The quality of the user interface of an appliance is relatively unimportant in determining its adoption if the perceived value is high.
* Example:
    The physical keyboards on a mobile phone often too small to be used conventionally, but it makes the typing simpler (eg: For email).

### 4.2  Virtual Keyboard

##### 4.2.1 Introduction
Present a keyboard on the screen, which the user can then interact with.

##### 4.2.2 Challenge:
* Traditionally, a virtual keyboard should be made as small as possible;
* The resistive screen requires its user to accurately hit the correct location.

##### 4.2.3 Auto-correct
* Description:
    It is the predictive texting and dictionary-based function adopted from SMS application to predicate what did user want to type and to offer the completed words.
* Motivation:
    The use of a capacitance-based screen causes a higher probability for the typo.


## 5. Question about the Context-Aware System

### 5.1 Spatial Context Aware System

##### 5.1.1 Introduction
Systems are often referred to as Location-Aware

##### 5.1.2 Relevance of the Devices
* Services can be ordered spatially in a list.
* P2P services can relate the proximity of users to a service.
* Services may be discovered locally and then use a local PAN.

##### 5.1.3 Application
* Navigation: The Google Map navigate user from source to destination.
* Context Change: The "Trip Advisor" can provide information about local hotel and restaurant based on the user's location.
* Personal Emergency: When the cad is broken down, the application can send a message with a precise GPS location to alert the emergency services.
