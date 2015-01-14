#Projektrapport
Projektrapport för slutprojekt i kurserna *ASP.NET MVC (1dv409)* och *Webbteknik II (1dv449)*.

##Inledning
Som slutprojekt valde jag att göra en väderapplikation. Med applikationen kan ev användare 
leta upp 5-dygnsprognoser för platser i Sverige. Jag valde att göra den här applikationen 
för att det var det rekommenderade projektet i kursen *ASP.NET MVC* och jag såg det som 
möjligt att skriva applikationen på ett sätt som skulle uppfylla kraven också för kursen 
*Webbteknik II*.    
Det finns flera andra liknande applikationer och jag har låtit mig inspireras av några av 
dessa exempelvis vad gäller design.

##Applikationsschema
Ett schema över applikationen finns här: [Schema-applikation](https://github.com/ba222ec/IDV449_ba222ec/blob/master/projekt/Doc/Schema-applikation.png)

##Server
Serverkoden är skriven med ramverket *ASP.NET MVC 5* tillsammans med ramver inom ASP.NET 
som exempelvis *Entity Framework* för databaskommunikation och *Identity* för hantering 
av användarkonton. Applikationens databas är en *MS SQL Server*. Applikationen är skriven 
i en MVC struktur och programspråket är *C#*.

###Model
Modellen är i huvudsak uppdelad i tre delar.    
Den första är mina domänobjekt, exempelvis väderleksprognoser, sökresultat och platser, 
dessa finns i namnrymden *Entities*.    
Den andra delen tar hand om kommunikationen med databasen och validering av objekt som 
ska sparas i databasen. Det är denna del som arbetar med ramverket *Entity Framework*. 
Klasserna som har med databasen att göra finns i namnrymden *PersistentStorage*.    
Den tredje delen är klasser som gör anrop mot de externa api:er som applikationen 
använder, api:erna *yr.no* och *geonames.org*. Från *yr* hämtar applikationen 
väderleksprognoser och från *geonames* hämtar den information om geografiska platser. 
Dessa klasser ligger i namnrymden *Services*.    
I basnamespacet *Weather.Domain* ligger klassen WeatherService, det är denna klass som 
kontrollermetoderna för väderdata har kontakt med. Vid ett anrop till den här klassen 
undersöks först databasen, om ett giltigt svar på förfrågan redan finns lagrat där 
(cachat) så är det databasens innehåll som returneras. Om det inte finns någon sparad 
data i databasen, eller om datan är för gammal så görs istället en ny förfrågan till 
det externa api:et och svaret därifrån sparas ner till databasen (cachas) innan det 
returneras.

###Controller
Kontrollklasserna finns i namnrymden *Weather.App.Controllers* och är skrivna enligt 
syntax för ramverket *ASP.NET MVC 5*. Kontrollmetoderna är fördelade på fyra klasser 
enligt vilken funktionalitet de är en del av. Klasserna är *AccountController*, 
*ErrorController*, *ForecastController* och *OfflineController*. Kontrollklasserna har 
i uppgift att validera indata från klienten och att välja vilken typ av svar som ska 
skickas tillbaka (vilken vy, ev. meddelanden, felsidor, osv.). I de fall som det inte 
går att få något svar från modellklasserna, exempelvis på grund av felaktig url, att 
platsen som efterfrågas inte existerar eller att ett av de externa api:erna ligger 
nere så ser kontrollern till att lämpligt felmeddelande visas.

###View
Applikationens vyer är skrivna enligt syntax för ramverket *ASP.NET MVC 5* och återfinns 
i katalogen *Weather.App/Views*.

##Klient
Klientsidan använder sig av javascriptramverket *JQuery* och cssramverket *Bootstrap*. 
Programspråken som används på klientsida är *HTML*, *CSS* och *JavaScript*. På 
klientsidan används api:et *Google Maps* för att visa platsen som väderprognosen gäller 
för. Cachning av resurser sköts av appcachen (se mer under rubriken *Offline* nedan).

###Design
Designen är responsiv för att kunna användas på flera typer av skärmar och på olika 
skärmstorlekar och det fungerar bra förutom för de smalaste skärmarna där man 
ev. måste scrolla något i sidled. Man kan även använda applikationen med CSS 
inaktiverat.

###Tillgänglighet
Applikationen är skriven för att ha hög tillgänglighet. Man kan använda applikationen 
utan css aktiverat, man kan använda applikationen enbart med tab-tangenten och alla 
bilderna har alt-texter. Formulärfält har labels som är gömda när css är aktiverat 
med som används av exempelvis skärmläsare.

##Offline
Jag hade först en annan plan på vilket projekt jag skulle göra, det var först en 
bit in i projekttiden som jag bestämde mig för att bygga på slutprojektet i 
kursen ASP.NET MVC. Därför är den här applikationen inte designad med tanken 
*offline-first*, men lyckligtvis kunde jag använda samma implementation i den här 
applikationen som jag hade tänkt göra i den första idén jag hade.    
Min manifestfil listar alla de statiska resurser (js, css, png) som behövs för att 
köra applikationen, och dessa filerna hämtas även från appcachen i onlineläge och 
fungerar även browser cache. Varje url som hämtas i onlineläge lagrs i appcachen 
och är tillgänglig i offlineläge. När användaren visar en väderprognos så sparas 
url:en till den prognosen i localStorage. När användaren går offline så modifieras 
utseendet på applikationen, alla formuär inaktiveras, ett infomeddelande 
("Du är offline") visas och på startsidan listas alla väderleksprognoser som 
tidigare har visats i onlineläge i form av en lista med url:er (dessa hämtas från 
localStorage). Även utseendet på väderlksprognoserna ändras, kartan visas inte 
(Google's api är endast tillgängligt online) och istället för väderikoner så visas 
bildernas alt-texter (eftersom ikonerna hämtas från yr.no).    
Varje gång ett online eller offline vent inträffar så laddas sidan automatisk om 
för att anpassa sig till online- respektive offline-läge.

##Säkerhet och optimering

###Säkerhet
Applikationen skyddas mot CSRF attacker med tokens på varje formulär som gör en 
POST-request till servern. Sök-formuläret gör en GET-förfrågan och tanken är att 
användaren ska kunna spara url:en om man vill komma ihåg den. Därför behövs inget 
CSRF skydd i sökformuläret.    
Ramverket innehåller ett skydd mot riskfyllt indata (taggar, sql-osv) och 
applikationen skyddas därmed mot XSS-attacker.    
I kommunikationen med databasen använder jag min av *Entity Framework* i 
kombination med frågespråket *LINQ* som är en del av .NET. Detta gör att 
applikationen inte är sårbar mot traditionella sql-injection-attacker.    
Applikationen loggar in i databasen som användaren *appUser* som har begränsade 
databasrättigheter.

###Optimering
Alla css och javascriptfiler som tillhör något av klientsideramverken är 
minifierade. Jag använde tidigare ASP.NET:s egna optimeringsklasser som bakar 
ihop alla css och javascriptfiler till en fil, men detta var svårt att kombinera 
med en manifestfil och en appcache. Alla statiska filerna som applikationen använder 
sig av är listade i manifestfilen och lagras därför i appcachen på klienten.    
Alla css-filerna ligger i sidhuvudet och alla javascriptfilerna ligger sist i 
body-taggen.    
Data från de externa api:erna, *yr* och *geonames* cachas på serversidan i en 
relationsdatabas.

##Risker
Risker med att använda sig av externa api:er är om api:et ändras eller om villkoren 
för att avända api:et ändras. Detta inträffade faktiskt under slutet av utvecklingen 
av den här applikationen, vilket medförde visar att detta är en risk att ta på allvar. 
Det som hände var att Google skar ner på sitt stöd för inloggning med OpenID 2.0 för 
att få sina användare att gå över till OAuth 2.0, nu kan man inte längre lägga till 
nya domäner som använder sig av OpenID (kör man applikationen nu så fungerar 
inloggning med Google om man sitter på domänen *localhost*, met fungerar inte om 
man sitter med domänen *vhost9.lnu.se*).    
Det finns också risker med att alltför mycket lita på ramverk som andra har skrivit, 
det kan ju vara så att ramverken innehåller säkerhetshål som du inte själv känner 
till. Dessutom bör applikatione uppdateras när en uppdatering av ramverken publiceras, 
vilket naturligtvis medför extra underhållsarbete.    
En risk som jag har upptäckt när jag arbetat med ASP.NET MVC är att ramverket helt är 
anpassat för engelskspråkiga applikationer, och att vissa beteenden, exemelvis 
formulering av felmeddelanden på engelska, sitter långt in i ramverket och är väldigt 
svårt att ändra på (*ASP.NET* är inte open source och insynen är liten). Detta märks 
på några ställen i applikationen då vissa felmeddelanden formuleras på engelska. Denna 
risken kan unvikas genom att man undersöker ramverket och ser om det är lämpligt för 
applikationen.

##Reflektion
Arbetet med projektet har på det stora hela gått bra och jag känner mig nöjd med 
slutresultatet. Jag hann inte med allt jag hade planerat, jag hade kvar att skriva 
en funktion för att inloggade användare skulle kunna lista sina favoritplatser och 
att dessa skulle visas på applikationens startsida. En databastabell och klasser 
i min domän finns klara för detta men jag hann inte implementera funktionen till 
slut.    
Jag har under arbetet med slutprojektet lärt mig många nya saker, framförallt om 
ramverken *ASP.NET MVC 5* och *Bootstrap*. Jag har inte tidigare arbetat i större 
skala med de här ramverken och det har antagligen givit mig nyttig kunskap. Jag har 
också sett vikten av att testa sin applikation på produktionsserver ofta, hade jag 
gjort det så hade jag möjligtvis hunnit skriva om inloggningsfunktionen med Google 
så att den också skulle ha fungerat på produktionsservern.    
Även offline-applikationer har varit okänd mark för mig tidigare och jag har lärt 
mig mycket nytt om detta. Jag kan dock hålla med kritikerna om att manifest-syntaxen 
och appcache-beteendet inte är den lättaste att förstå sig på och använda.    
Jag kommer i farmtiden tänka efter en extra gång innan jag väljer *ASP.NET MVC 5* 
till projekt (om jag kan välja), farmförallt för att det är skrivet för applikationer 
på engelska och att vissa av beteendena är svåra att ändra på, exempelvis formulering 
av felmeddelanden. Jag misstänker att det finns många andra ramverk som är lättare 
att använda om applikationen är på ett annat språk än engelska. *Bootstrap* däremot 
kändes väldigt bra att använda, har tidigare använt klientramverket *Foundation* 
men kommer antagligen att försöka använda Bootstrap i fortsättningen.    
Jag kommer antagligen inte att arbeta vidare med den här applikationen, det finns 
många bra väderapplikationer redan. Men kunskaperna från det här projektet kommer 
jag att ha nytta av andra projekt också.

/Svante Arvedson, ba222ec