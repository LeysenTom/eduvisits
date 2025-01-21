De link naar de githubpagina is: https://github.com/it-graduaten/mvc-project-tti-gpr-d2-1-tti-gpr-d2-1-1

Het wachtwoord van iedereen is 'AdminAdmin_1'.

BEHALVE voor KathelijnAchterHuze@School.com. Dit wachtwoord is 'Kathelijn_1'.

BEHEERDER
Login: ArneVanLooyen@School.com
Wachtwoord: AdminAdmin_1

DIRECTIE
Login: InneTijlen@School.com
Wachtwoord: AdminAdmin_1

COORDINATOR
Login: FikBakkers@School.com
Wachtwoord: AdminAdmin_1

SECRETARIAAT
Login: KathelijnAchterHuze@School.com
Wachtwoord: Kathelijn_1

De rest heeft allemaal de rol van LEERKRACHT.
EliseDePoorteren@School.com
DanielMinne@School.com
FonsVermeulen@School.com
JacquesVroens@School.com
ZazaQimbala@School.com
MartheSmets@School.com

Foto en fotoalbum is niet uitgewerkt.

De bijgevoegde SQL-file kan ingeladen worden nadat je een migration en update-database hebt gedaan. De rollen en de toewijzing van de rollen gebeurt bij de eerste opstart van de applicatie. Hiervoor MOET de SQL-file al in de database zitten.

MAILSERVICE
De mailservice kan getest worden doormiddel van de recieverEmail in je eigen emailadres te veranderen. Dit gebeurt in de controller van navormingen en van studiebezoeken.

APISERVICE
De Api kan getest worden met postman en de applicatie in API_app.zip. De url voor de API is https://localhost:xxxx/api/afwezigheid/. Voeg aan deze Url een getal toe om de week terug te krijgen verschillend van de huidige week. Vb. vorige week: https://localhost:xxxx/api/afwezigheid/-1 , twee weken vooruit https://localhost:xxxx/api/afwezigheid/2.
Voor postman voeg onder de authorization-tab type API-KEY toe; KEY: X-API-KEY , value: TestKey, Add to: Header.
Voor de Applicatie voer je de url van de API in : https://localhost:xxxx
De API geeft een lijst van alle afwezigen in die week terug. Indien er geen zijn is de lijst leeg.
In het MVC-project is het mogelijk om meerdere API-keys toe te voegen.

