HelloServiceExemple/ Contient un exemple simple de service WCF.

Partie Service : 

L'API WCF ( qui n'est constitué que d'une fonction triviale prenant un string et retournant
 "Hello string" est implémenté dans HelloService/HelloWcfService.

HelloService/HelloWcfService est le répertoir du projet qui prend en charge l'exhibition
des end point par lequel va intéragir les applications clientes, recupération des meta data
( prototipe des classe de l'API et transit des requette/reponses"
C'est un programme console, pour que le webservice soit fonctionel.

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


NOTE: Les source du document de suivie se trouve sur le OneDrive que l'on a mis en comun pour le projet. 
Modifiez le par là et exporter éventuellement le en PDF dans le dépot git. 

-------------------------------------
MAJ 30/05 :

ProjetHerite/ contient les sources du projet de l'anée dernière.

 Compilation de la doxigène du proget des anée antérieur. Plus présisément l'équivalent de la doxigène dans cette
evironement et pour çe langage. Les sites sont dans DocSandCastel/{docAPI,docInterface}/help/intex.aspx (ouvrir avec un navigateur) 
il faut bien pencée a autoriser l'execution du code.

/XdatafiWS contien les source de notre travaille.

-------------------------------------

Conseil: Prenez le temps de regarer comment fonctione l'application gitHub pour ordinateur. Commit corréspont 
à comit pour les fichier coché, Sync a pull/push.

Lien vers la page du projet des anée précédentes :
http://ensiwiki.ensimag.fr/index.php/Finance:Outil_de_r%C3%A9cup%C3%A9ration_de_donn%C3%A9es_financi%C3%A8res/AppliFiMag
