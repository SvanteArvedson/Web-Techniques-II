#Laborationsrapport - ba222ec

##Säkerhet

###Man behöver inget konto för att logga in
Funktionen *isUser()* i filen *sec.php* är felaktigt implementerad, den 
returnerar **true/false eller string** men borde endast returnera **true/false**. 
Den felaktiga implementationengör att man kan logga in i applikationen med 
ett konto som inte existerar. Jag har lagat funktionen genom att se till 
att den endast returnerar **true/false**.

###Icke inloggade användare kan lägga till meddelanden
I filen *functions.php* undersäks inte om användaren är autentisierad innan 
ett nytt meddelande läggs till. Detta gör att vem som helst kan lägga till 
ett nytt meddelande genom att anropa url'en 
*functions.php?function=add&name=ettNamn&message=EttMeddelande*. Detta är 
ju inte bra, tanken är ju att användaren ska vara inloggad för att kunna 
skicka meddelanden. Säkerhetshålet kan utnyttigas av hackare för att 
exempelvis utföra XSS-attacker. Jag har täckt till säkerhetshålet genom 
att innan et meddelande läggs till undersöka om användaren är inloggad 
genom att använda funktionen *checkUser()* i filen *sec.php*.

###Icke inloggade användare kan hämta ut meddelanden
En icke inloggad användare kan med ett URL-anrop få ut alla meddelanden som 
finns sparade i databasen utan att vara inloggad. Detta kan användas av en 
hackare för att stjäla hemlig information och avlyssna användarnas 
meddelandekonversationer. Jag har delvis täppt till säkerhetshålet genom att 
kolla om efterfrågaren är inloggad innan meddelanden skickas ut, men i ett 
fleranvändarsystem skulle detta naturligtvis inte räckt, då skulle exempelvis 
rättigheter till enskilda meddelanden fått undersökas.

###CSRF attack möjlig
En användare kan men hjälp av en CSRF attack posta och hämta meddelanden till 
databasen. För att hindra detta har jag lagt till en CSRFtoken som kontrolleras 
varje gång ett AJAX-anrop görs och jag har även ändrat AJAX-anropet från en 
GET till en POST för att hålla token-nyckeln dold.

###PDO's felmeddelanden ekas ut till klienten
I klasserna *get.php*, *post.php* och *sec.php* så ekas PDO's felmeddelanden 
ut till klienten. Detta är inte bra så det avslöjar information om 
applikationen som kan utnyttjas av en eventuell hackare för att planera och 
genomföra en attack. Jag har lagat säkerhetshålet genom att eka ut andra 
felmeddelanden som inte avslöjar några implementationsdetaljer.

###Icke parametriserade sql-frågor mot databasen
I klasserna *post.php* och *sec.php* så ställs SQL-frågor till databasen 
utan att frågorna har blivit parametriserade. Detta är ett säkerhetshål 
som möjliggör en SQL-injection attack. Jag har lagat säkerhetshålet genom 
att parametrisera frågorna innan de körs.

###Lösenorden sparas i klartext i databasen
I applikationen så sparas användarnas lösenord i klartext i databasen. Detta 
är inte bra, det är lätt för hackare och andra att bryta sig in på 
användarkonton om de skulle få tag på databasen. För att lösa problemet så har 
jag lagt till fältet *salt* i databastabellen, jag har hashat de sparade 
lösenorden tillsammans med ett unikt salt. Jag har skrivit om 
inloggningsfunktionen så att den hämtar ut användarens uppgifter, hashar det 
angivna lösenordet med saltet från databasen och jämför sedan det hashade 
lösenordet med lösenordet som är lagrat i databasen.

###Man kan posta kod meddelandeformuläret
Man kan posta kod i meddelandeformuläret och på så sätt exekvera egen kod i 
applikationen. Detta kan utnyttjas av hackare för att utföra en injektion- 
eller XSS-attack. Jag har löst problemet genom att strippa inläggen på taggar 
innan de skickas in i databasen.

##Optimering
Efter att mina ändringar så laddas sidan mess.php ca 1000 ms snabbare än före 
ändringarna.

###Minska anralet HTTP-requests
+ Jag har lagt koden i *MessageBoard.js* och *Message.js* i en 
gemensam fil döpt till *site.js*. Med detta har jag sparat ett HTTP-anrop.
+ Jag har tagit bort css-raden i *mess.php* där bilden *b.jpg* begärs som 
bakgrundsbild.
+ Jag har tagit bort attributet *background* på body-taggen i *mess.php*.
+ Jag har tagit bort onödiga script-taggar i *mess.php*.
+ Jag har tagit bort *bootstrap.min.js* från *index.php* och *mess.php*. 
Scriptet användes inte.

###Lägg till Expires och Cache-Controller i request-headern
Jag har en apache-server som utvecklingsmiljö. Jag har skrivit en htaccess-fil 
som lägger till *Expires* och *Cache-control* och som låter klienten spara 
alla filer utom **html** och **json** i 10 år från anropet. Jag har inte skrivit 
koden i htaccess filen helt själv, utan kollat hur man gör på handledningar på 
webben.

###Komprimera CSS- och JavaScript-filer innan de skickas ut
Jag har angett i min htaccess-fil att css- och javascript-filer ska komprimeras 
innan de skickas ut till klienten.

###Lägg CSS-filerna i head-taggen
Jag har flyttat alla css-filer till head-taggen i filerna *index.php* och *mess.php*.

###Lägg JavaScript-filerna i längst ner i body-taggen
Jag har flyttat alla js-filerna til botten av sidornas body-tagg.

###Lägg all CSS och JavaScript i seperata filer
Jag har flyttat all inline-kod till seperata filer (och minimerat dem).

###Minimera CSS- och JavaScript-filer
Jag har minimerat alla css- och js-filer.

###Tag bort duplicerade js-filer
Jag har tagit bort duplicerade filer från *mess.php*.

##Long polling

###Min implementation
För att implementera long polling så har jag skapat en ny funktion 
*getNewMessages($timestamp)* som tar emot ett timestamp och som söker efter 
meddelanden som är postade efter att tidpunkten som gavs som argument. 
Funktionen kollar i databasen om nya meddelande finns, om inte så sover 
exekveringen i en sekund och föröker därefter igen. Funktionen exekverar max 
25 gånger innan den avbryter, detta för att undvka fel när PHPs maximala 
exekveringstid passeras. Hittar finktionen nya meddelanden så skickas dessa 
ut till klienten.    
Ett AJAX-anrop från klienten anropar *getNewMessages($timestamp)* och väntar 
därefter på svar. När ett svar kommer som innehåller nya meddelanden så 
renderas dessa ut i DOM'en. Därefter görs ett nytt anrop till 
*getNewMessages*. Tidpunkten som skickas som argument hämtar klienten från 
det senaste meddelandet som renderats ut.    
För att unvika problem med att anrop (hämta nya meddelanden, skicka nytt 
meddelande) så delade jag upp anropen till två olika script istället för som 
i applikationen ursprungsskick där anropen gick till endast ett script. Detta 
gör att anropen inte blockerar varandra.

###Kommentarer
Long polling fungerar men verkar slösa stort på serverns resurser. Jag gissar 
dessutom att min lösning inte är den mest optimala, den slösar inte bara på 
serverns resurser utan också på databasuppkopplingar. Skulle jag implementera 
detta fler gånger så skulle jag leta efter ett bra ramverk som gör det här 
bättre, men aldra helst skulle jag nog vilja kolla på någon av de andra 
tillgängliga teknikerna som exemlevis *web sockets*.