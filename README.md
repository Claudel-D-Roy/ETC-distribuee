[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-24ddc0f5d75046c5622901739e7c5dd533143b0c8e959d652212380cedb1ea36.svg)](https://classroom.github.com/a/kDHznpg1)
# Épreuve terminale de cours

## CONTEXTE :
Un riche philanthrope vous donne la chance de développer votre propre projet personnel, toute dépense payés (wow!).
Vous avez jusqu’au 20 décembre pour assembler un MVP (Minimum Viable Product), et s’il est assez impressionné il vous engagera comme CTO (Chief Technical Officer) pour une startup qu’il fondera à ce moment-là.

## SPÉCIFICATIONS :
Afin de pouvoir démontrer que votre serveur puisse être mis en grappe, la seule contrainte de l’application sera qu’au minimum 2 utilisateurs puissent interagir ensemble. Ainsi, si un des utilisateurs se connecte à 1 nœud de la grappe et qu’un autre se connecte sur le deuxième nœud, les deux devraient pouvoir utiliser les fonctionnalités comme s’il n’y avait qu’un seul serveur.
Vous devrez choisir un sujet pour votre épreuve et venir le réserver avec moi. Il ne sera pas possible de choisir le même sujet qu’un autre étudiant, donc premier arrivé premier servi! Plus votre application est "ambitieuse", plus je serai généreux sur la correction, et à l'inverse plus l'application est simple plus je serai pénalisant si vous coupez les coins ronds.
Côté architecture, vous êtes libres de choisir la technologie que vous désirez. Vous devrez cependant nous démontrer en personne que votre serveur puisse marcher en grappe (cluster), que si une des nœuds de votre grappe se met soudainement hors service les autres nœuds puissent prendre le relais, et que vous pouvez ajouter un autre nœud à la grappe si besoin.
Il devra être possible d’interagir avec votre serveur à l’aide d’un interface utilisateur (UI), mais il n’est pas nécessaire que l’interface soit servie par votre cluster si vous préférez séparer votre serveur de votre interface. Par exemple : si vous décidez de développer une application mobile séparée comme interface utilisateur, vous pourrez développer votre serveur sans frontend.
 
## BARÈMES DE CORRECTION :
1. (30%) Application fonctionnelle
2. (30%) Votre serveur fonctionne en grappe (2 utilisateurs sur 2 nœuds différents interagissent ensemble)
3. (20%) Si un nœud est hors-service, le reste marche correctement
4. (20%) Il est possible d’ajouter un nœud à la grappe
5. (-40%) Manquements à la traçabilité de votre démarche à travers les traces d'évolution du travail sur GitHub Classroom. (Je dois voir la progression évoluer à chaque cours : faites des push souvent!)
6. (-10%) Fautes de français
