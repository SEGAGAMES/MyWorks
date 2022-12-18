# MyWorks
UniWorksC#
Task(translated)
It is necessary to develop two applications running on the basis of a client-server
architecture. To do this, you need to rework the application from laboratory work No. 6 
(serialization). Divide it into two components: the server and the client part.
After launching, the server application waits for incoming connections and
creates a separate thread for each of them, designed to communicate with the connected 
a client. Thus, the ability to connect
multiple users to the server at the same time should be supported.
At the beginning of communication, the server must authenticate by requesting a username and
password. If the login and password combination is known to the server, then it allows the client
to send commands implemented in the previous laboratory (No. 3 and No. 6).
The client application is designed to connect to the server, to send
messages to it, and to output server responses.
