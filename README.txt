HelloServiceExemple/ Contient un exemple simple de service WCF.

Partie Service : 

L'API WCF ( qui n'est constitué que d'une fonction triviale prenant un string et retournant
 "Hello string" est implémenté dans HelloService/HelloWcfService.

HelloService/HelloWcfService est le répertoire du projet qui prend en charge l'exhibition
des end point par lequel va intéragir les applications clientes, recupération des meta data
( prototype des classes de l'API et transit des requette/reponses"
C'est un programme console, pour que le webservice soit fonctionel.

Procedure pour lancer le server : 
1) Ouvrir avec Visual Studio HelloServiceExemple/HelloService.sln puis clic droit
sur Solution 'HelloService' puis 'Build Solution'
2)executer en tant qu'administrateur
\HelloService\HelloServiceSelfHostConsole\bin\Debug\HelloServiceSelfHostConsole.exe. Le service 
est désormais en écoute.

Partie Client :

HelloWebClient contien deux projets un en ASP.NET l'autre en WPF. 

Procedure pour l'execution des clients : 
1) Compiler la solution comme pour la version server.
2.1) Pour lancer la version WPF il suffit d'aller dans le répertoire des sources 
du projet puis dans bin\debug et de lancer l'executable HelloClientWpf.exe.
2.2) Pour la version ASP.NET il faut d'abord lancer le server IIS expresse, pour
cela aller dans Visual Studio après avoir ouvert HelloWebClien.sln puis clic droit sur
HelloWebClientAspNet puis 'debug' puis 'Start new instance' votre navigateur devrait
s'ouvrir avec un message d'erreur. Rendez vous sur la page http://localhost:8290/WebForm1.aspx


NOTE: Les sources du document de suivi se trouvent sur le OneDrive que l'on a mis en commun pour le projet. 
Modifier le par là et exporter éventuellement le en PDF dans le dépot git. 

-------------------------------------
MAJ 30/05 :

ProjetHerite/ contient les sources du projet de l'année dernière.

 Compilation de la doxigène du projet des années antérieures. Plus précisément l'équivalent de la doxigène dans cet
environnement et pour çe langage. Les sites sont dans DocSandCastel/{docAPI,docInterface}/help/intex.aspx (ouvrir avec un navigateur) 
il faut bien penser à autoriser l'execution du code.

/XdatafiWS contient les sources de notre travail.

-------------------------------------

Conseil: Prenez le temps de regarder comment fonctionne l'application gitHub pour ordinateur. Commit correspond 
à commit pour les fichiers cochés, Sync a pull/push.

Lien vers la page du projet des années précédentes :
http://ensiwiki.ensimag.fr/index.php/Finance:Outil_de_r%C3%A9cup%C3%A9ration_de_donn%C3%A9es_financi%C3%A8res/AppliFiMag
