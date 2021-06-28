# DIOCatalogoDeJogos
A project made as final test of a bootcamp from DIO

## What does it does?
It has a CRUD for a database of games.

## Which Database?
It was written to work with sqlServer, but it has no connection string in appSettings.
It need to be changed to adapt to your database.

### Game Class
(Guid)id          = Primary Key
(string)nome      = Name of the game
(string)produtora = Name of the company that has created the game.
(double)preco     = It is how much it cost.
