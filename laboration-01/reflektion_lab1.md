#Reflektion (Svante Arvedson) ba222ec

##JSON
JSON är ett av flera olika vanligta att lagra data som skall kunna delas och 
spridas. JSON är ett format som kan hanteras i många olika programmeringsspråk 
och data i JSON-format är därför lätt att hantera. jag tror at detta var 
anledningne till att vi skulle spara datat i laborationen i JSON-format.

##Användande av webbskrapor
Alla typer av sajter som behöver strukturerad data. Det kan exempelvis vara 
sajter för prisjämförelse och produktjämförelse, men det kan också vara sajter 
som listar filmer, kulterevent, bokrecentioner, osv. Webbskrapning kan 
naturligtvis vara användbart för applikationer som inte ligger ute på webben, 
exemlevis i forskningssammanhang.

##Hänsyn till serverägaren
I laborationen så har jag inte anropat sidor med information som inte behövs 
(ex undersökte jag så att URL'erna verkligen gick till en kurssida innan jag 
anropade dem). Jag har också skrivit med hjälp av min user-agent sträng i 
HTTP-huvudet gjort det enkelt att identifiera mig, jag skickade med mitt namn 
och min studentkod. Min applikation har en cache på 24 timmar för att servern 
inte ska belastas med alltför många anrop.

##Etiska aspekter
Själva skrapningen är inte så märklig i mina ögon, det ser jag som ett 
rationaliserat sätt att läsa webbsidor (skrapan ersätter en människa). En 
viktigare diskussion är hur informationen används efteråt. Här bör hänsyn 
tas till om informationen var öppen eller om informationen var gömd bakom 
ett lösenord, om informationen innehåller känslig data. Ett enkelt sätt att 
undvika dåliga val är att fråga informationsägaren om lov innan man använder 
den.

##Risker
Risker på macronivå har jag svårt att se, av samma orsak som jag inte ser 
själva tekniken med automatisk skrapning som något märkligt. Däremot finns 
det risker för den enskiljda serverägaren. Servern kan bli överbelastad 
och otillgänglig, eller väldigt mycket dyrare då stora mängder trafik oftast 
kostar mer.

##Problem med ASP.NET Webforms
Man kan få problem när man gör POST-anrop till en applikation skriven i 
ASP.NET Webforms. Detta för att ASP.NET Webforms skickar med en så kallad 
ViewState-sträng med formulärets status (alla ifyllda värden, ibockade 
checkbuttons, osv.) och att applikationen behöver den här strängen för att 
kunna hantera POST-anropet. Glömmer man att skicka med ViewState-strängen 
så fungerar inte applikationen.

##Två saker till diskussion
Jag skulle vilja diskutera hur man kan hantera teckenkodning i samband med 
webbskrapning. Webbsidor är kodade i olika teckenkodning och applikationen 
använder sig av olika teckenkodning. Hur kan man göra en generell lösning 
som tillfredställer både webbskraparutvecklaren och mottagaren av den 
skrapade informationen? En annan punkt jag skulle vilja diskutera är min 
applikationsstruktur. Jag har skrivit applikationen i en MVC-struktur, 
var det ett bra val eller blev det onödigt komplicerat för en webbskrapa?

##Rättsfall
Ett rättsfall som handlade om webbskrapning är rättsfallet mellan *Ebay* 
och *Bidders's Edge* i USA år 2000. Bidder's Edge var en sajt som listade 
aktioner från flera olika aktionssajter, bland annat Ebay, med hjälp av 
robotar som läste av data från aktionssajterna. Bidders's Edge och Ebay 
försökte förhandla fram ett användandeavtal men misslyckades med att komma 
överens om de tekniska detaljerna runt skrapningen. Då Bidder's Edge 
fortsatte att använda robotar för att skrapa Ebay så stämde Ebay Bidder's 
Edge bland annat för intrång och för att ha skadat Ebays egendomar. Domstolen 
dömde till Ebays fördel och kort efter att domen fallit lade Bidder's Edge 
ner sin applikation.

##Vad har jag lärt mig
Jag har lärt mig många nya saker under den här laborationen. Jag har inte 
tidigare arbetat med automatiska anrop till webbsidor så det var intressant 
att testa. Jag har inte heller arbetat med JSON tidigare, också det en nyttig 
erfarenhet. Peer-instruction-tillfällena har varit bra ordnade då det gav 
möjlighet att diskutera de lite mer filosofiska aspekterna av ämnet, och 
hjälpte mig att forma en egen ståndpunkt.