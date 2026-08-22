# GS1.ApiService

## Vereisten
- VisualStudio
- Docker
- Aspire CLI

## Technische keuzes

- Voor architectuur is er gekozen voor Vertical Slice Architecture. Onderdeel hiervan is het gebruik van minimal API om de functionele slices te ondersteunen.
- Daarnaast wordt er gebruik gemaakt van een result pattern om results uit handlers terug te kunnen geven.
- Voor de project opzet is er gekozen voor Aspire dit om gemakkelijk een API en SQL Server naast elkaar te kunnen draaien. 
- Aspire brengt ook automatisch OTEL logging mee. Dit is aan het groeien naar de standaard voor logging.
- Aan deze standaard logging heb ik HttpRequestLogging toegevoegd.
- Aspire brengt ook automatisch de GlobalExceptionHandler en ProblemDetails mee. ProblemDetails is de standaard voor het teruggeven van foutmeldingen.
- Op dit moment worden bepaalde validaties en checks in de handler gedaan. Deze zouden in de toekomst naar generieke middleware moeten kunnen.
- Voor authenticatie / autorisatie is een simpele header toegepast. 
- Voor het testen van de applicatie heb ik gebruik gemaakt van `.http` bestanden. 

## GTIN

- GTIN bestaat uit 14 karakters. 
- De eerste 4 karakters zijn een bedrijfscode bestaande uitalphanumerieke waarde
- De laatste 10 karakters zijn getallen- 
- GTIN is niet te wijzigen, omdat dit de unieke sleutel is en het niet gewenst is dat deze wijzigt.
- GTIN wordt in zijn geheel opgeslagen. In de toekomst is het mogelijk om ze ook gesplitst op te slaan met een los veld voor bedrijfscode en een los veld voor het opvolgnummer. Als dat bijvoorbeeld nodig is voor het sorteren of zoeken binnen alle GTINs (items).

## Toekomstige wijzigingen
- Applicatie uitbreiden met een `.editorconfig`, `.Directory.Packages.props` voor CPM, `Directory.Build.props` voor solution brede configuratie.
- Authenticatie / autorisatie uitbreiden op basis van de gewenste IDP.
- Het configureren van logging levels om de logging te beperken in verband met o.a. kosten.
- Toevoegen van integratietesten om de applicatie automatisch te kunnen testen in plaats van `.http` files te gebruiken.
- Folder structuur aanpassen met o.a. `src`, `test`, etc.
