#Användningsfall

##AF1 - Sök väderprognos från formulär

###Huvudscenario
+ **1** - Använaren begär url:en "*/*"
+ **2** - Systemet visar ett sökformulär
+ **3** - Användaren anger ett sökord
+ **4** - Systemet visar upp en lista på platser som matchar sökordet
+ **5** - Användaren väljer en plats från listan
+ **6** - Systemet visar upp en femdygnprognos för den valda platsen

###Alternativscenario
+ **4a** - Endast en träff på det angivna sökordet
    + **1** - *Gå till punkt 6*
+ **4b** - Ingen träff på det angivna sökordet
    + **1** - Systemet meddelar att ortsnamnet inte gav någon träff
    + **2** - *Återgå till punkt 2*

##AF2 - Sök platser från url

###Huvudscenario
+ **1** - Använaren begär url:en "*/Sök/{sökord}*"
+ **2** - Systemet visar upp en lista på platser som matchar sökordet
+ **3** - Användaren väljer en plats från listan
+ **4** - Systemet visar upp en femdygnprognos för den valda platsen

###Alternativscenario
+ **2a** - Endast en träff på det angivna sökordet
    + **1** - *Gå till punkt 4*
+ **2b** - Ingen träff på det angivna sökordet
    + **1** - Systemet meddelar att ortsnamnet inte gav någon träff

##AF3 - Sök väderprognos från url

###Huvudscenario
+ **1** - Använaren begär url:en "*/Väderlek/Sverige/{region}/{plats}*"
+ **2** - Systemet visar upp en femdygnprognos för den valda platsen

###Alternativscenario
+ **2a** - Den begärda platsen finns inte
    + **1** - Systemet visar en 404-sida

##AF4 - Registrera nytt konto

###Huvudscenario
+ **1** - Användaren klickar på länken *Logga in* på startsidan
+ **2** - Systemet visar upp en inloggningsida
+ **3** - Användaren klickar på länken *Registrera nytt konto*
+ **4** - Systemet visar upp ett registreringsforulär
+ **5** - Användaren skriver in sitt användarnamn, sin epostadress och sitt lösenord
+ **6** - Systemet sparar kontouppgifterna, loggar in användaren och visar feedback

###Alternativscenario
+ **6a** - De lämnade uppgifterna bryter mot domärreglerna
    + **1** - Systemet visar ett felmeddelande
    + **2** - *Gå till punkt 5*

##AF5 - Registrera nytt konto med Google+

###Huvudscenario
+ **1** - Användaren klickar på länken *Logga in* på startsidan
+ **2** - Systemet visar upp en inloggningsida
+ **3** - Användaren klickar på länken *Registrera nytt konto*
+ **4** - Systemet visar upp ett registreringsforulär
+ **5** - Användaren klickar på länken *Registrera konto med Google+*
+ **6** - Systemet skickar användaren till Google+'s inloggningssida
+ **7** - Använadaren loggar in på Google+'s inloggningssida
+ **8** - Systemet sparar kontouppgifterna, loggar in användaren och visar feedback

###Alternativscenario
+ **7a** - Felaktigt användarnamn eller lösenord
    + **1** - Systemet visar ett felmeddelande
    + **2** - *Gå till punkt 5*
+ **8a** - Ett fel inträffar
    + **1** - Systemet visar ett felmeddelande
    + **2** - *Gå till punkt 1*

##AF6 - Logga in

###Huvudscenario
+ **1** - Användaren klickar på länken *Logga in* på startsidan
+ **2** - Systemet visar upp en inloggningsida
+ **3** - Användaren skriver in sitt användarnamn och lösenord
+ **4** - Systemet loggar in användaren

###Alternativscenario
+ **3a** - Felaktigt användarnamn eller lösenord
    + **1** - Systemet visar ett felmeddelande
    + **2** - *Gå till punkt 2*

##AF7 - Logga in med Google+

###Huvudscenario
+ **1** - Användaren klickar på länken *Logga in* på startsidan
+ **2** - Systemet visar upp en inloggningsida
+ **3** - Användaren klickar på länken *Logga in med Google+*
+ **4** - Systemet vidarebefordrar användaren till Google+'s inloggningssida
+ **5** - Använadern loggar in på Google+'s inloggningssida
+ **6** - Systemet loggar in användaren och visar feedback

###Alternativscenario
+ **5a** - Felaktigt användarnamn eller lösenord
    + **1** - Systemet visar ett felmeddelande
    + **2** - *Gå till punkt 5*
+ **7a** - Ett fel inträffar när användaren loggar in på Google+
    + **1** - Systemet visar ett felmeddelande
    + **2** - *Gå till punkt 1*

##AF8 - Se väderleksprognos offline, tidigare sedd sida

###Huvudscenario
+ **1** - Användaren begär url:en */Väderlek/Sverige/{region}/{platsnamn}*
+ **2** - Systemet visar en väderleksprognos för den valda platsen
+ **3** - Användaren lägger på sin internetuppkoppling och laddar om sidan
+ **4** - Systemet visar upp samma väderleksprognos

###Alternativscenario
+ **4a** - Webbläsaren har inte stöd för offlineapplikationer
    + **1** - Webbläsaren visar upp en felsida (uppkoppling saknas)

##AF9 - Se väderleksprognos offline, tidigare osedd sida

###Huvudscenario
+ **1** - Användaren lägger på sin internetuppkoppling och begär url:en */Väderlek/Sverige/{region}/{platsnamn}* (se till att sidan inte har begärts tidigare)
+ **2** - Systemet visar felsida (*Offline!*)