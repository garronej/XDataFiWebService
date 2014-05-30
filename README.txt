HelloServiceExemple/ Contient un exemple simple de service WCF.

Partie Service : 

L'API WCF ( qui n'es constituer que d'une fonction triviale prenant un string et retou
rant "Hello string" est implémenté dans HelloService/HelloWcfService.

HelloService/HelloWcfService est le répertoir du projet qui prend en charge l'exibition
des end point par le quelle vas intéragire les application clientes, recupération des meta data
( prototipe des classe de l'API et transit des requette/reponces"
C'est un programe console, pour que le webservice soit fonctionelle.

Procedure pour lancer le server : 
1) Ouvrir avec Visual Studio HelloServiceExemple/HelloService.sln puis click droit
sur Solution 'HelloService' puis 'Build Solution'
2)executer en tant qu'administrateur
\HelloService\HelloServiceSelfHostConsole\bin\Debug\HelloServiceSelfHostConsole.exe. Le service 
est désormais en écoute.

Partie Client :

HelloWebClient contien deux projet un en ASP.NET l'autre en WPF. 

Procedure pour l'execution des client : 
1) Compiler la solution comme pour la vertion server.
2.1) Pour lancer la vertion WPF il sufit d'aller dans le répértoire des source 
du projet puis dans bin\debug et de lansé l'executable HelloClientWpf.exe.
2.2) Pour la vertion ASP.NET il faut dabor lancer le server IIS expresse, pour
cela allez dans Visual Studio apres avoir ouvert HelloWebClien.sln puis click droit sur
HelloWebClientAspNet puis 'debug' puis 'Start new instance' votre navigateur devrais
s'ouvrir avec un message d'érreur. Rendez vous sur la page http://localhost:8290/WebForm1.aspx

XdataFiWPF/ contient les sources du projet de l'anée dernière.

NOTE: Les source du document de suivie se trouve sur le OneDrive que l'on a mis en comun pour le projet. 
Modifiez le par là et exporter éventuellement le en PDF dans le dépot git. 

Conseil: Prenez le temps de regarer comment fonctione l'application gitHub pour ordinateur. Commit corréspont 
à comit pour les fichier coché, Sync a pull/push.


