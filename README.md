To test the application you should first run the UdpSenddatagram application.
The Udp listener application should listen and proceess what the second app is sending and write it on MudTable.
Every data that receive has 3 second to be shown on table unless it will be deleted if not getting updated within this 3 seconds.

Sending upd packets application example:
![image](https://user-images.githubusercontent.com/35465069/210110107-2b610d75-5d73-411b-a8b9-23a3073c8c1e.png)
![image](https://user-images.githubusercontent.com/35465069/210110110-5e7b5255-72fd-45c9-8472-db7ca83293b7.png)


Udp listener example:
![image](https://user-images.githubusercontent.com/35465069/210110133-5e625029-cb01-41c2-bf31-378f1108cdf8.png)

in the listener application we are writing to console all the data (packets and logs)
and in the table the card data
