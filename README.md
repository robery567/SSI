# SSI #
# Smart Self Improvement # 

Aceasta este branch'ul de development al proiectului [SSI](http://community.infoeducatie.ro/t/ssi-smart-self-improvement-utilitar-caras-severin-lucrari-2015-nationala/3864).

Smart Self Improvement (SSI) este un program care, asa cum numele sugereaza, reprezinta un ajutor pentru utilizator
in imbunatatirea propriilor sale aptitudini si abilitati. In viata de zi cu zi, foarte des ne confruntam cu asa-zisa
"procastinare" , prin compunerea unui plan clar de lucru/executare a unei activitati complexe aceasta poate fi evitata ,
aici intervine SSI, prin oferirea unei interfete interactive , posibilitatea creeri unor evenimente ,
impartasirea propriilor ganduri,sfaturi pentru viitor , chiar si imagini

# Componente: #
- [API](https://github.com/robery567/SSI/tree/master/SSI-API)
- [Aplicatie Desktop](https://github.com/robery567/SSI/tree/master/SSI)
- [Site Prezentare](https://github.com/robery567/SSI/tree/master/SSI-WEB)
- Android (soon)

# Cerinte Sistem #
# Aplicatie Windows:
- .NET Framework 4.5
- Dependinte:
 ```
 Facebook.dll
 Newtonsoft.Json.dll
 ```
 
# API: 
- Apache/Nginx
- PHP >= 5.4
- MongoDB
- Mysql / MariaDB / PerconaDB
- Spatiu de stocare pentru imagini 

# Configurare API:
- Configurarea se realizeaza in fisierul `config.php` din directorul `settings`
```php

define ('development_mode', true | false); # Daca development_mode este setat pe true vor fi afisate toate erorile
define ('mongodb_store', true | false);  # Daca mongodb_store este setat pe true imaginile vor fi stocate in colectii MongoDB

$mysql = [
    'driver' => 'mysqli',  # A nu se modifica
    'hostname' => 'host_gazda', # ip-ul gazdei serverului mysql/mariadb
    'username' => 'user_gazda', # username-ul autorizat de catre serverul gazda MySQL 
    'password' => '', # Parola aferenta user-ului
    'database' => '' # numele bazei de date unde vor fi stocate datele trimise si primite de catre aplicatie
];

```
- Se importa fisierul SQL `ssipdbFINAL.sql` in baza de date dorita

SSH: 
```
# mysql - u nume_user -p 
mysql> use nume_database;
mysql> source cale/ssipdbFINAL.sql;
```
# Configurare aplicatie Windows:
- Se creeaza un fisier `apilocation` (fara extensie) in folderul aplicatiei
- In `apilocation` se scrie locatia api-ului (`cale/director/`) , de ex:

apilocation:
```
localhost/ssi/
```

# Mediu de dezvoltare utilizat :
- FreeBSD 10.1 `MariaDB 10`
- Ubuntu-Server 14.04.1 LTS 
   ``` 
     Server  web :
      - Apache 2.2
      - PHP 5.5.9 + Mongo extension + MySQL Extension + Memcached
      
     MongoDB-Server
   ```
  - Windows 10/8.1/7 `working`
