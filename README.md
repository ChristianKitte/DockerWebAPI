# DockerWebAPI
Bei diesem Projekt handelt es sich um eine Übung zur Vertiefung der Techniken Docker, WebAPI und Entity Framework Core. Im Detail soll folgendes exemplarisch umgesetzt werden:

### 1 - Ein Microsservice, der eine Datenzugriffsschicht darstellt
* Webanwendung WebAPI auf Basis von .net Core 5.0
* Code First Ansatz
* Zugriff auf eine SQLite Datenbank mit EF Core und Linq
* Implementierung einer kleinen, versionierten WebAPI zum Lesen, Löschen, Hinzufügen und Editieren
* Unterstützung und Dokumentation mit Swagger
* Ausführung als Docker Container


  Die Datenbank selbst beinhaltet nur eine einzelne Tabelle mit Personen und den Eigenschaften ID, Vorname, Nachname.

### 2 - Ein Client, der auf diese Datenbank zugreift
* Konsolenanwendung auf Basis von .net Core 5.0
* Nutzt den unter 1 genannten Microservice
* Kann alle Personen oder eine einzelne über deren ID abfragen
* Kann eine Person löschen, hinzufügen oder editieren


  Die Konsole verfügt in einer ersten Iteration über keine anspruchsvollen Interaktionen. Vielmehr geht es um die Techniken.
