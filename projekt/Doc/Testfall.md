# Testfall

## Testfall AF1 - Sök väderprognos från formulär

### Testfall 1 - Sökning med flera träffar
+ **1** - Användaren begär url:en "*/*"
+ **2** - Systemet visar startsidan och ett sökformulär
+ **3** - Användaren söker efter orten *Tyringe*
+ **4** - Systemet visar en lista med tre (3) träffar
+ **5** - Användaren väljer den översta träffen (*Tyringe, Skåne*)
+ **6** - Systemet visar en prognos för *Tyringe, Skåne*

### Testfall 2 - Sökning med en (1) träff
+ **1** - Användaren begär url:en "*/*"
+ **2** - Systemet visar startsidan och ett sökformulär
+ **3** - Användaren söker efter orten *Tyringe Kyrka*
+ **4** - Systemet visar en prognos för *Tyringe Kyrka, Skåne*

### Testfall 3 - Sökning med noll (0) träffar
+ **1** - Användaren begär url:en "*/*"
+ **2** - Systemet visar startsidan och ett sökformulär
+ **3** - Användaren söker efter orten *Tjottahejti*
+ **4** - Systemet visar meddelandet *Sökningen på Tjottahejti gav inga träffar*

## Testfall AF2 - Sök platser från url

### Testfall 1 - Sökning med flera träffar
+ **1** - Använaren begär url:en "*/Sök/Tyringe*"
+ **2** - Systemet visar en lista med tre (3) träffar
+ **3** - Användaren väljer den översta träffen (*Tyringe, Skåne*)
+ **4** - Systemet visar en prognos för *Tyringe, Skåne*

### Testfall 2 - Sökning med en (1) träff
+ **1** - Användaren begär url:en "*/Sök/Tyringe Kyrka*"
+ **2** - Systemet visar en prognos för *Tyringe Kyrka, Skåne*

### Testfall 3 - Sökning med noll (0) träffar
+ **1** - Användaren begär url:en "*/Sök/Tjottahejti*"
+ **4** - Systemet visar meddelandet *Sökningen på Tjottahejti gav inga träffar*

## Testfall AF3 - Sök väderprognos från url

### Testfall 1 - Lyckad hämting av väderprognos
+ **1** - Användaren begär url:en "*/Väderlek/Sverige/Skåne/Tyringe*"
+ **2** - Systemet visar en prognos för *Tyringe, Skåne*

### Testfall 2 - Misslyckad hämtning av väderprognos
+ **1** - Användaren begär url:en "*/Väderlek/Sverige/Uppsala/Tyringe*"
+ **2** - Systemet visar en 404-sida

## Testfall AF4 - Registrera nytt konto

### Testfall 1 - Lyckad registrering
+ **1** - Användaren tar sig fram till url:en */Konto/Nytt*
+ **2** - Systemet visar ett registreringsformulär
+ **3** - Användaren fyller i: användarnamn: *prov*, epost: *prov@exempel.nu*, lösenord: *lösenord*
+ **4** - Sytemet gör en redirect till startsidan och visar meddelande *Kontot prov har skapats*, prov är inloggad

### Testfall 2 - Misslyckad registrering inget användarnamn
+ **1** - Användaren tar sig fram till url:en */Konto/Nytt*
+ **2** - Systemet visar ett registreringsformulär
+ **3** - Användaren fyller i: användarnamn: **, epost: *prov@exempel.nu*, lösenord: *lösenord*
+ **4** - Systemet visar ett felmeddelande

### Testfall 3 - Misslyckad registrering upptaget användarnamn
+ **1** - Användaren tar sig fram till url:en */Konto/Nytt*
+ **2** - Systemet visar ett registreringsformulär
+ **3** - Användaren fyller i: användarnamn: *prov*, epost: *test@exempel.nu*, lösenord: *annatLösenord*
+ **4** - Systemet visar ett felmeddelande

### Testfall 4 - Misslyckad registrering felaktig epost
+ **1** - Användaren tar sig fram till url:en */Konto/Nytt*
+ **2** - Systemet visar ett registreringsformulär
+ **3** - Användaren fyller i: användarnamn: *test*, epost: *testexempelnu*, lösenord: *lösenord*
+ **4** - Systemet visar ett felmeddelande

### Testfall 5 - Misslyckad registrering kort lösenord
+ **1** - Användaren tar sig fram till url:en */Konto/Nytt*
+ **2** - Systemet visar ett registreringsformulär
+ **3** - Användaren fyller i: användarnamn: *test*, epost: *test@exempel.nu*, lösenord: *lö*
+ **4** - Systemet visar ett felmeddelande

## Testfall AF5 - Registrera nytt konto med Google+

### Testfall 1 - Lyckad registrering
+ **1** - Användaren tar sig fram till url:en */Konto/Nytt* och klickar på *Registrera konto med Google+*
+ **2** - Systemet gör en redirect till Google+'s inloggningssida
+ **3** - Användaren fyller i uppgifter för ett befintligt Google+-konto
+ **4** - Sytemet gör en redirect till startsidan och visar ett rättmeddelande, användaren är inloggad

## Testfall AF6 - Logga in

### Testfall 1 - Lyckad inloggning (gör testfall AF4, test 1 först)
+ **1** - Användaren tar sig till url:en */Konto/Inloggning*
+ **2** - Systemet visar ett inloggningsformulär
+ **3** - Användaren skriver in uppgifter: användarnamn: *prov*, lösenord: *lösenord*
+ **4** - Sytemet gör en redirect till startsidan, prov är inloggad

### Testfall 2 - Misslyckad inloggning felaktigt användarnamn
+ **1** - Användaren tar sig till url:en */Konto/Inloggning*
+ **2** - Systemet visar ett inloggningsformulär
+ **3** - Användaren skriver in uppgifter: användarnamn: *brov*, lösenord: *lösenord*
+ **4** - Sytemet visar ett felmeddelande

### Testfall 3 - Misslyckad inloggning felaktigt lösenord
+ **1** - Användaren tar sig till url:en */Konto/Inloggning*
+ **2** - Systemet visar ett inloggningsformulär
+ **3** - Användaren skriver in uppgifter: användarnamn: *prov*, lösenord: *password*
+ **4** - Sytemet visar ett felmeddelande

## Testfall AF7 - Logga in med Google+

### Testfall 1 - Lyckad inloggning (gör testfall AF5, test 1 först)
+ **1** - Användaren tar sig till url:en */Konto/Inloggning*
+ **2** - Systemet visar upp ett inloggningsformulär och en knapp för inloggning med Google+
+ **3** - Användaren klickar på knappen *Logga in med Google+*
+ **4** - Systemet gör en redirect till Google+'s inloggningssida
+ **5** - Användaren fyller i inloggningsuppgifter för kontot
+ **6** - Sytemet gör en redirect till startsidan, användaren är inloggad

## Testfall AF8 - Se väderleksprognos offline, tidigare sedd sida

### Testfall 1
+ **1** - Användaren begär url:en */Väderlek/Sverige/Skåne/Tyringe*
+ **2** - Systemet visar en prognos för *Tyringe, Skåne*
+ **3** - Användaren lägger på sin internetuppkoppling och laddar om sidan
+ **4** - Systemet visar upp samma prognos och ett informationsmeddelande ("Offline")

## Testfall AF9 - Se väderleksprognos offline, tidigare osedd sida

### Testfall 1
+ **1** - Användaren lägger på sin internetuppkoppling och begär url:en *Väderlek/Sverige/Skåne/Åhus* (se till att denna sida inte finns i webbläsarens appcache)
+ **2** - Systemet visar en felsida (*Offline!*)

## Övriga testfall

### Testfall 1 - Begär url som inte finns
+ **1** - Användaren begär url:en "*/Forecast/Search/Tyringe*"
+ **2** - Systemet visar upp en 404-sida

### Testfall 2 - Begär url med felaktigt format
+ **1** - Användaren begär url:en "*/Väderlek/Sverige/Tyringe*"
+ **2** - Systemet visar upp en 400-sida

### Testfall 3 - Begär en väderleksrapport när databasen är nere
+ **1** - Användaren begär url:en "*/Väderlek/Sverige/Skåne/Tyringe*" (Stäng av databasen före det här testet)
+ **2** - Systemet visar upp en 500-sida