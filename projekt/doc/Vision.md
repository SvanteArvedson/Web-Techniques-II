#Vision

<table>
	<tr>
		<th>Datum</th>
		<th>Händelse</th>
		<th>Författare</th>
	</tr>
	<tr>
		<td>15/12 2014</td>
		<td>Visionen skapades</td>
		<td>Svante Arvedson</td>
	</tr>
</table>

##Beskrivning
Ibland är det svårt att veta var den närmaste mataffären ligger någonstans, och 
ingen vill ju inte gå längre än nödvändigt för att handla. Då skulle en 
enkel, välfungerande och mobilvänlig webbapplikation som, utifrån din aktuella 
position eller en annan position som du själv väljer, räknar ut var den 
närmaste matbutiken ligger samt ger dig vägbeskrivning dit vara av stor nytta. 
Min vision är att bygga en sådan applikation.

##Användare
Användare av applikationen är konsumenter i god fysisk form som behöver veta 
vart man snabbast kan gå för att köpa mat. Med fysisk form menar jag personer 
som kan promenera själva utan hjälp. Situationer där applikationen kan hjälpa 
med är exemelvis på resan, på promenaden eller då man bekantar sig med ett 
nytt ställe. Det är även personer med viss vana att använda mobilen för att 
lösa vardagsproblem.

##Liknande system
Ett liknande system är applikationen [varligger.se](http://www.varligger.se/) 
och inte bara visar vägen till matbutiken utan även till andra typer av 
service. En stor nackdel med den här applikationen är att den inte fungerar 
offline och alltså inte fungerar exemlevis när man sitter på tunnelbanan.

##Tekniker
+ Som serversidespråk kommer jag att använda [PHP](http://php.net/) och 
    ramverket [Symfony](http://symfony.com/).    
+ Klientkoden kommer jag att skriva i [JavaScript](http://www.ecmascript.org/) 
    och i ramverket [Bootstrap](http://getbootstrap.com/).    
+ För att komma åt adresser till matbutiker kommer jag att använda webbskrapning 
    (naturligtvis efter att ha frågat om lov först) och för kartor och 
    vägbeskrivningar kommer jag att använda 
    [Google Maps Api](https://developers.google.com/maps/).

##Baskrav
+ **B1** - Applikationen ska hitta den mataffär som ligger närmast dig där 
    du befinner dig eller från en annan position som du själv anger.
+ **B2** - Applikationen ska fungera väl i mobila enheter.
+ **B3** - Applikationen ska med begränsad funktionalitet fungera offline.