# Escola API
Escola API Restful tem a funcionalidade de gerenciar provas de uma escola e seus respectivos alunos.
A aplicação foi desenvolvida inteiramente **.NET**.

## Banco de Dados
Escola API utiliza **MySql** padrão para banco de dados.
Uma base modelo do sistema está disponivel em `\\Database\escola_backup.sql`.

![Alt text](/Database/modelo_relacional.jpg?raw=true "Modelo relacional")

## Testes automatizados
Testes unitários da aplicação desenvolvido em XUnit com **NSubstitute** para Mock.

***

# Guia de instalação
Para rodar a aplicação localmente, iremos utilizar o serviço IIS Express da Microsoft, para isso, você irá precisar:

* Visual Studio 2010 > ou superior
* .NET Core SDK 3.1
* MySql Server

A aplicação utiliza dependência de terceiros instaladas via gerenciador de pacotes NuGet próprio do Visual Studio.
* Pomelo.EntityFrameWorkCore.Mysql
* NSubstitute

## Passo-a-passo
1. Clone o repositorio Escola API em um local de sua preferência.
2. Restaure a base MySql da aplicação em um servidor local ou de sua preferencia MySql. `\\Database\escola_backup.sql`
3. Abra a solution do projeto `\\Escola\Escola.sln` com o Visual Studio.
4. Em "Solution Explorer", clique com o botão direito na solution e selecione a opção "Build All" para buildar os projetos e suas respectivas dependência.
5. Em `\\Escola\Escola.API\appsetings.json` altere o parâmetro "DBPadraoMySql" com suas respectivas informações da base.
6. Rode a aplicação utilizando IIS Express

***

# Métodos
Para acessar os métodos da API, você deverá pegar o caminho https da sua aplicação `https:\\ip:porta` e adicionar o sufixo e suas respectivas informações. Exemplo: `https:\\127.0.0.1:3000\api\Aluno\Criar`

## Prova
### Criar
* Sufixo: `api\Prova\Criar`
* Tipo: **POST**
* Corpo Requisição: 
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
* Resposta Requisição:
```json
  string
```

### Inativar
* Sufixo: `api\Prova\Inativar`
* Tipo: **POST**
* Corpo Requisição: 
```json
{
    "Nome" : string
}
```
* Resposta Requisição:
```json
  string
```

## Aluno
### Criar
* Sufixo: `api\Aluno\Criar`
* Tipo: **POST**
* Corpo Requisição: 
```json
{
    "Nome" : string
}
```
* Resposta Requisição:
```json
  string
```

### Inativar
* Sufixo: `api\Aluno\Inativar`
* Tipo: **POST**
* Corpo Requisição: 
```json
{
    "Nome" : string
}
```
* Resposta Requisição:
```json
  string
```

### Responder Prova
* Sufixo: `api\Aluno\ResponderProva`
* Tipo: **POST**
* Corpo Requisição: 
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
* Resposta Requisição:
```json
  string
```

### Media
* Sufixo: `api\Aluno\Media`
* Tipo: **POST**
* Corpo Requisição: 
```json
{
    "Nome" : string
}
```
* Resposta Requisição:
```json
{
    "Nome" : string
    "Media" : decimal
}
```

### Aprovados
* Sufixo: `api\Aluno\Aprovados`
* Tipo: **GET**
* Resposta Requisição:
```json
[
  {
    "Nome" : string
    "Media" : decimal
  }
]
```