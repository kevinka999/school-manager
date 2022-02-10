# Summary
This API is responsible for dealing with essays from a school and its respective students built using .NET Core

## Database
This API is using **MySql** as database.
You can get a script model for the database on this path `\\Database\escola_backup.sql`.

![Alt text](/Database/modelo_relacional.jpg?raw=true "Modelo relacional")

## Unit test
The unit test for this application was developed by using **XUnit** with **NSubstitute** for mocking.

***

# How to run it
1. Clone the repository in a location of your choice.
2. Restore the MySql database of the application on your MySql local server. `\\Database\school_backup.sql`
3. Open the `\\Escola\Escola.sln` project solution with Visual Studio.
4. In "Solution Explorer", right-click on the solution and select the "Build All" option to build the projects and their respective dependencies.
5. In `\\Escola\Escola.API\appsetings.json` change the parameter "DBPadraoMySql" for you respective database information.
6. Run the application using IIS or IIS Express

***

# Endpoint mapping
## Essay (Prova)
### Create an essay
* Route: `api\Prova\Criar`
* Method Type: **POST**
* Body: 
```json
{
    "Nome" : string
    "Gabarito" : [
		{
			"Pergunta" : integer,
			"Resposta" : string
		},
		{
			"Pergunta" : integer,
			"Resposta" : string
		}
	]
}
```
* Response:
```json
  string
```

### Disable an essay
* Route: `api\Prova\Inativar`
* Method Type: **POST**
* Body: 
```json
{
    "Nome" : string
}
```
* Response:
```json
  string
```

## Student (Aluno)
### Create student
* Route: `api\Aluno\Criar`
* Method Type: **POST**
* Body: 
```json
{
    "Nome" : string
}
```
* Response:
```json
  string
```

### Disable one student
* Route: `api\Aluno\Inativar`
* Method Type: **POST**
* Body: 
```json
{
    "Nome" : string
}
```
* Response:
```json
  string
```

### Answer one assay
* Route: `api\Aluno\ResponderProva`
* Method Type: **POST**
* Body: 
```json
{
	"NomeAluno" : string,
    	"NomeProva" : string,
	"Respostas" : [
		{
			"Pergunta" : integer,
			"Resposta" : string
		},
		{
			"Pergunta" : integer,
			"Resposta" : string
		}
	]
}
```
* Response:
```json
  string
```

### Student Average
* Route: `api\Aluno\Media`
* Method Type: **POST**
* Body: 
```json
{
    "Nome" : string
}
```
* Response:
```json
{
    "Nome" : string
    "Media" : decimal
}
```

### Approved students
* Route: `api\Aluno\Aprovados`
* Method Type: **GET**
* Body:
```json
[
  {
    "Nome" : string
    "Media" : decimal
  }
]
```
