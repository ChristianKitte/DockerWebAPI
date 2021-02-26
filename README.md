# DockerWebAPI
Bei diesem Projekt handelt es sich um eine Übung zur Vertiefung der Techniken Docker, WebAPI und Entity Framework Core. Daher wurde auch mehr Wert auf einen einfachen und leicht nachvollziehbaren Code gelegt. Die Kommunikation erfolgt mit JSON, auch da dies nativ unterstützt wird. Die Klasse Person muss in beiden Projekten bekannt sein. Um das Besipiel einfach zu halten, wurde hier die Klasse daher in beiden Projekten definiert. Im Detail soll folgendes exemplarisch umgesetzt werden:

### 1 - Ein Microsservice, der eine Datenzugriffsschicht darstellt
* Webanwendung WebAPI auf Basis von .net Core 5.0
* Code First Ansatz
* Zugriff auf eine SQLite Datenbank mit EF Core und Linq
* Implementierung einer kleinen, versionierten WebAPI zum Lesen, Löschen, Hinzufügen und Editieren
* Unterstützung und Dokumentation mit Swagger (offen)
* Ausführung als Docker Container

  Die Datenbank selbst beinhaltet nur eine einzelne Tabelle mit Personen und den Eigenschaften ID, Vorname, Nachname. 

### 2 - Ein Client, der auf diese Datenbank zugreift
* Konsolenanwendung auf Basis von .net Core 5.0
* Nutzt den unter 1 genannten Microservice
* Kann alle Personen oder eine einzelne über deren ID abfragen
* Kann eine Person löschen, hinzufügen oder editieren

  Die Konsole verfügt in einer ersten Iteration über keine anspruchsvollen Interaktionen. Vielmehr geht es um die Techniken.

## Docker
Das Beispiel ist als Docker Container **ckitte/dbserver** verfügbar. Nach dem herunterladen kann er mit dem Befehl **docker run -p 8080:80 ckitte/dbserver** 
gestartet werden. Hierbei ist 8080 der Port außerhalb des Containers, hier localhost:8080, und 80 der Port innerhalb des Containers. **Somit horcht 
der Service im Container am Port 80 und ich spreche ihn von außen mit 8080 an**. Das Buildscript ist teil des Codes.

```
docker pull ckitte/dbserver
docker run -d -p 8080:80 ckitte/dbserver
```

## DBClient
Der DBClient ist in erster Linie als eine Übung zum Zugriff auf eine WebAPI mit Hilfe des neuen HttpClient Objektes sowie für das Testen des DBServers erstellt worden. Er sollte in dieser Art nicht produktiv zum Einsatz kommen. Sofern der DBServer läuft und auf dem Port 8080 horcht, führt die Ausführung des DBClient zu der hier gezeigten Ausgabe, welche einen Test des DBServers und seiner Funktionen entspricht.

```
Keine Personen vorhanden...

Füge Person Alpha Beta hinzu...
Person mit der ID 12 und dem Namen Alpha Beta wurde geholt

Hole alle Personen...
Liste aller Personen:
Person mit der ID 12 und dem Namen Alpha Beta wurde geholt

Hole einzige Person mit ID 12...
Person mit der ID 12 und dem Namen Alpha Beta wurde geholt

Ändere einzige Person mit ID 12...

Hole einzige Person mit ID 12...
Person mit der ID 12 und dem Namen Alpha - Geändert Beta - Geändert wurde geholt

Lösche einzige Person mit ID 12...

Hole alle Personen...
Keine Personen vorhanden...

Fertig!
```
