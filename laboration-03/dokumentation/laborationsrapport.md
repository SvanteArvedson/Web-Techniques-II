#Laborationsrapport 3 - Svante Arvedson (ba222ec)

##Vad finns det för krav du måste anpassa dig efter i de olika API:erna?

###SR
SR:s öppna api har väldigt generösa villkor för både sitt api och sin data. Det 
finns ingen egentlig begränsning över hur många anrop man får lov att 
göra mot api:et och det finns inte några speciella villkor att förhålla sig mot 
för användande av informationen och datan. Däremot så ber de sina användare att 
vara ekonomiska med sina anrop för att inte belasta deras server i onödan.    

###Google
Användare av Google's api har fler villkor att följa, specificerat i deras 
användande-avtal. Det var ett långt dokument på juridiska med innehåll i stora 
drag att man som för att använda api:et måste ha ett konto 
(för att få ut en api-nyckel) hos Google och man får inte låsa in tjänsten där 
api:et används i en betaltjänst, exemelvis genom att kräva att användaren köper 
ett lösenord för att komma åt den. Man får inte heller kopiera koden för att 
använda i egna produkter.    
Antalet anrop är inte begränsade, men man måste betala för att använda api:et 
om man gör fler än 25000 anrop om dagen under en period på 90 dagar.

##Hur och hur länga cachar du ditt data för att slippa anropa API:erna i onödan?

###SR
Under utvecklingen av applikationen arbetade jag mot en nedsparad fil för att 
slippa göra anrop under tiden som applikationen skrevs.    
I den färdiga applikationen cachas datat på servern i en .json-fil, och datan 
cachas i 3 minuter. Tiden kollas genom att kolla på filens "last modified".    

###Google
Google's kod begörs från klienten och cachas om klientens inställningar 
tillåter det.

##Vad finns det för risker med din applikation? Hur har du tänkt kring säkerheten i din applikation?
Då min applikation inte är lösenordsskyddad så finns risken att min server 
överbelastas av anrop utan att jag vet vem eller vilka som gör anropen 
(men skulle den bli så populär så skulle jag nog snarare bli glad).    
Risken finns också att någon skulle stjäla min google api-nyckel för att 
själva kunna göra anrop och därmed göra mig betalningsskyldig till google, 
men detta kan stoppas genom att jag vid publicering av applikation skapar 
en lista på mitt google-konto på de domäner som är tillåtna att göra anrop.    
Naturligtvis finns en injection-risk både från datat som skickas från SR 
och koden som skickas från google. Jag skyddar mig mot html-injections och 
javascript-injections genom att html-encoda datat innan jag skriver ut den 
i DOM:en.

##Hur har du tänkt kring optimeringen i din applikation?
Jag har minimerat mina css och javascriptfiler. Jag har använt CDN för att 
länka in jQuery och Bootstrap. Jag har lagt till en *ajax-loader* som visas 
innan kartan har laddats in, egentligen inte en optimeringsåtgärd men det 
gör att applikationen blir mer användarvänlig.