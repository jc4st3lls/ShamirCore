# ShamirCore
Shamir Secrets implements in Net Core 3.1

Resumint molt, aquest algoritme criptogràfic ens permet compartir un secret entre un número d’actors, i establir un mínim d’aquests per recuperar-lo o reconstruir-lo.

Per entendre-ho millor, imaginem que tenim un document confidencial xifrat amb una clau secreta que no sap ningú, però que hi ha una serie d’actors que tenen una clau cada un que ajuntant-les d’alguna manera, poden reconstruir la clau secreta del document xifrat. Però per acabar de arrodonir-ho, que només amb un cert número d’aquest actors, ja en tenim prou. 
Si ens parem a pensar una mica. això dins el món del blockchain, on la finalitat entre d’altres, és posar valor al contingut digital, aquest algoritme pot ser de molt ús, de fet ho és (compartir valor).
Imaginem que, existeix un Sistema de Salut, amb dades de pacients, les quals només és poden visualitzar amb el consentiment de certes parts. Si aquestes dades estan xifrades, i per desxifrar-les és requereix que tots els actors (pacient + metge + sistema) o una part d’ells (pacient + sistema o metge + sistema) estiguin d’”acord”, una manera de controlar aquest accés podria ser amb l’ Schema Shamir. El mateix es pot aplicar a documents confidencials, contrasenyes amb privilegis alts, etc.

